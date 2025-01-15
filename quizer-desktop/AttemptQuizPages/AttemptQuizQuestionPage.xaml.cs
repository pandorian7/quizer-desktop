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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace quizer_desktop.AttemptQuizPages
{
    /// <summary>
    /// Interaction logic for AttemptQuizQuestionPage.xaml
    /// </summary>
    public partial class AttemptQuizQuestionPage : Page
    {
        public event EventHandler<EventArgs>? OnNext;
        private Question Question;
        private DispatcherTimer timer;
        public int ElapsedTime = 0;
        public AttemptQuizQuestionPage(Question question, int questionIndex)
        {
            InitializeComponent();

            Question = question;

            DataContext = question;

            QuestionBlock.Text = question.question;
            QuestionIndexBlock.Text = $"#{questionIndex+1}";
            if (question.multiple_answers == 1)
            {
                NAnswersBlock.Text = "This is a Multiple Answers Question";
            } else
            {
                NAnswersBlock.Text = "This is a Single Answer Question";
            }
            AnswersDataGrid.ItemsSource = question.answers;

            timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            
            timer.Tick += OnTick;

            timer.Start();

            RenderTime();
        }

        private void OnTick(object? sender, EventArgs e)
        {
            ElapsedTime += 1;
            RenderTime();
            if (ElapsedTime >= Question.duration)
            {
                Next();
            }
            
            
        }

        private void RenderTime()
        {
            TimeRemainingBlock.Text = Utils.FormatTime(Question.duration - ElapsedTime);
        }
        
        private void Next()
        {
            timer.Stop();
            OnNext?.Invoke(this, EventArgs.Empty);
        }
        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            NextBtn.IsEnabled = false;
            Next();
        }

        private void AnswersDataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            if (Question.multiple_answers == 1) {
                return;
            }
            // from chat gpt
            if (AnswersDataGrid.CurrentCell.Column is DataGridCheckBoxColumn)
            {
                AnswersDataGrid.CommitEdit(DataGridEditingUnit.Cell, true);

                int rowIndex = AnswersDataGrid.Items.IndexOf(AnswersDataGrid.CurrentItem);

                foreach (var answer in Question.answers)
                {
                    answer.is_correct = 0;
                }

                var cellValue = ((CheckBox)AnswersDataGrid.CurrentCell.Column.GetCellContent(AnswersDataGrid.CurrentItem)).IsChecked;
                var answerState = cellValue == true ? 1 : 0;

                Question.answers[rowIndex].is_correct = answerState;


                AnswersDataGrid.ItemsSource = null;
                AnswersDataGrid.ItemsSource = Question.answers;
            }
        }
    }
}
