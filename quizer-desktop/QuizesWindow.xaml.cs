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
                Utils.ErrorMsg("Login Failed");
            }
        }

        public record DataGridRecord(string Title, string Description, int Points, string Creator);
        private async void LoadData()
        {
            var quizes = await API.GetQuizes();
            if (quizes is null)
            {
                Utils.ErrorMsg("Failed to Load data");
            }
            else
            {
                QuizesDataGrid.ItemsSource = quizes.Select(q => new DataGridRecord(q.title, q.description, q.points, q.username));
            }
        }

        private void Quizer_ContentRendered(object sender, EventArgs e)
        {
            LoadData();

        }

        private void QuizesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (QuizesDataGrid.SelectedItem is not DataGridRecord selected)
            {
                return;
            } else
            {
                AttemptButton.IsEnabled = true;
                if (selected.Creator == Data.username)
                {
                    DeleteButton.IsEnabled = true;
                    EditButton.IsEnabled = true;
                } else
                {
                    DeleteButton.IsEnabled = false;
                    EditButton.IsEnabled = false;
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "This Action Will Permenently Delete the quiz. And it is not recoverable. Do you want to Continue?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );
        }
    }
}
