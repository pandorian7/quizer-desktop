using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace quizer_desktop
{
    /// <summary>
    /// Interaction logic for QuizesWindow.xaml
    /// </summary>
    public partial class QuizesWindow : Window
    {


        private QuizJson? Selected = null;
        public QuizesWindow()
        {
            InitializeComponent();
            UsernameLabel.Text = Data.username;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await API.LogOut();
                var logInWindow = new LoginWindow();
                logInWindow.Show();
                Close();
            }
            catch (Exception)
            {
                Utils.ErrorMsg("Logout Failed");
            }
        }

        private void EnableEdit()
        {
            EditButton.IsEnabled = true;
        }

        private void DisableEdit()
        {
            EditButton.IsEnabled = false;
        }

        private void EnableDelete()
        {
            DeleteButton.IsEnabled = true;
            DeleteButton.Foreground = Brushes.White;
        }


        private void DisableDelete()
        {
            DeleteButton.IsEnabled = false;
            DeleteButton.Foreground = Brushes.Black;
        }

        private void EnableAttempt()
        {
            AttemptButton.IsEnabled = true;
            AttemptButton.Foreground = Brushes.White;
        }

        private void DisableAttempt()
        {
            AttemptButton.IsEnabled = false;
            AttemptButton.Foreground = Brushes.Black;
        }

        private async void LoadData()
        {
            var quizes = await API.GetQuizes();
            if (quizes is null)
            {
                Utils.ErrorMsg("Failed to Load data");
            }
            else
            {
                QuizesDataGrid.ItemsSource = quizes;
            }
            DisableEdit();
            DisableDelete();
            DisableAttempt();
        }

        private void Quizer_ContentRendered(object sender, EventArgs e)
        {
            LoadData();

        }

        private void QuizesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Selected = QuizesDataGrid.SelectedItem as QuizJson;
            if (Selected is null)
            {
                Selected = null;
                return;
            } else
            {
                EnableAttempt();
                if (Selected.username == Data.username)
                {
                    EnableDelete();
                    EnableEdit();
                } else
                {
                    DisableDelete();
                    DisableEdit();
                }
            }
        }

        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var init_page = -1;
            var original = await API.GetQuiz(Selected!.id);
            List<int> existingQuestionIds = original.questions.Select(q => q.id!.Value).ToList();
            var quiz = await API.GetQuiz(Selected!.id);
            while (true)
            {
                var editWindow = new EditQuizWindow(quiz);
                editWindow.SetInitPage(init_page);
                if (editWindow.ShowDialog() == true)
                {
                    var res = await Quizer.SafeUpdateQuiz(quiz, editWindow.DeletedQuestions);
                    if (res is not null)
                    {
                        init_page = res.Value;
                    }
                    else {
                        MessageBox.Show("Quiz Saved", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }
                } else
                {

                    await Quizer.SafeUpdateQuiz(original, editWindow.DeletedQuestions);
                    break;
                }
            }
            LoadData();

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addquizWindow = new AddQuizWindow();
            if (addquizWindow.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "This Action Will Permenently Delete the quiz. And it is not recoverable. Do you want to Continue?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );
            if (result == MessageBoxResult.Yes && Selected is not null) {
                try
                {
                    await API.DeleteQuiz(Selected.id);
                    LoadData();
                } 
                catch (SvelteError err)
                {
                    Utils.HandleError(err);
                }
                catch (Exception)
                {
                    Utils.ErrorMsg("Unknown error Occured");
                }
            }
            
        }

        private async void AttemptButton_Click(object sender, RoutedEventArgs e)
        {
            var quiz = await API.GetQuiz(Selected!.id);
            var attemptWindow = new AttemptQuizWindow(quiz);
            attemptWindow.ShowDialog();
        }
    }
}
