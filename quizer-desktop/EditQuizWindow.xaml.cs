using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for EditQuizWindow.xaml
    /// </summary>
    public partial class EditQuizWindow : Window
    {
        public Quiz Quiz;
        public int QuestionIndex = -1;
        public List<int> DeletedQuestions;

        private void RenderNavigation()
        {
            if (QuestionIndex == -1)
            {
                PrvBtn.IsEnabled = false;
                if (Quiz.questions.Count > 0)
                {
                    NxtBtn.IsEnabled = true;
                    NxtBtn.Content = "Question 1";
                } else
                {
                    NxtBtn.IsEnabled = false;
                    NxtBtn.Content = String.Empty;
                }

                PrvBtn.Content = string.Empty;
                
            }
            else if (QuestionIndex == 0)
            {
                PrvBtn.IsEnabled = true;
                if (Quiz.questions.Count > 1)
                {
                    NxtBtn.IsEnabled = true;
                    NxtBtn.Content = "Question 2";
                }
                else
                {
                    NxtBtn.IsEnabled = false;
                    NxtBtn.Content = String.Empty;
                }
                PrvBtn.Content = "Quiz Metadata";
                
            }
            else if (QuestionIndex == Quiz.questions.Count - 1)
            {
                PrvBtn.IsEnabled = true;
                NxtBtn.IsEnabled = false;

                PrvBtn.Content = $"Question {QuestionIndex}";
                NxtBtn.Content = string.Empty;
            }
            else
            {
                PrvBtn.IsEnabled = true;
                NxtBtn.IsEnabled = true;

                PrvBtn.Content = $"Question {QuestionIndex}";
                NxtBtn.Content = $"Question {QuestionIndex + 2}";
            }

            QuestionSelectorDropDown.SelectionChanged -= QuestionSelectorDropDown_SelectionChanged;

            QuestionSelectorDropDown.Items.Clear();
            QuestionSelectorDropDown.Items.Add(new ComboBoxItem { Content = "Quiz Metadata", Tag="-1"});
            for (int i = 0; i < Quiz.questions.Count; i++)
            {
                QuestionSelectorDropDown.Items.Add(new ComboBoxItem { Content = $"Question {i + 1}", Tag = i.ToString() });
            }
            QuestionSelectorDropDown.SelectedValuePath = "Tag";
            QuestionSelectorDropDown.SelectedValue = QuestionIndex;
            QuestionSelectorDropDown.SelectionChanged += QuestionSelectorDropDown_SelectionChanged;

        }
        private void RenderPage()
        {
            if (QuestionIndex == -1)
            {
                var metadataEditPage = new EditQuizPages.EditQuizMetadataPage(Quiz);
                metadataEditPage.DataContext = Quiz;
                QuizEditFrame.Content = metadataEditPage;
            }
            else
            {
                var questionEditPage = new EditQuizPages.EditQuizQuestionPage();
                questionEditPage.OnDelete += OnDelete;
                questionEditPage.DataContext = Quiz.questions[QuestionIndex];
                QuizEditFrame.Content = questionEditPage;
            }
            RenderNavigation();
        }

        public void SetInitPage(int id)
        {
            QuestionIndex = id;
            RenderPage();
        }
        public EditQuizWindow(Quiz quiz)
        {
            InitializeComponent();
            DeletedQuestions = new();
            Quiz = quiz;
            RenderPage();
        }

        private void PrvBtn_Click(object sender, RoutedEventArgs e)
        {
            QuestionIndex -= 1;
            RenderPage();
        }

        private void NxtBtn_Click(object sender, RoutedEventArgs e)
        {
            QuestionIndex += 1;
            RenderPage();
        }

        private void QuestionSelectorDropDown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selection = Convert.ToInt32(QuestionSelectorDropDown.SelectedValue);
            QuestionIndex = selection;
            RenderPage();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var newQuestion = new Question() { 
                question = "New Question", 
                multiple_answers = 0, 
                duration = 30, 
                answers = new List<Answer>() { new Answer() { 
                        answer = "New Answer", 
                        is_correct = 1 
                } } };
            Quiz.questions.Add(newQuestion);
            QuestionIndex = Quiz.questions.Count - 1;
            RenderPage();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void OnDelete(object? sender, EventArgs e)
        {
            if (Quiz.questions[QuestionIndex].id.HasValue)
            {
                DeletedQuestions.Add(Quiz.questions[QuestionIndex].id!.Value);
            }
            Quiz.questions.RemoveAt(QuestionIndex);
            QuestionIndex -= 1;
            RenderPage();
        }
    }
}
