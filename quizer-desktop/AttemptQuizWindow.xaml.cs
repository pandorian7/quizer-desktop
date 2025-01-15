using quizer_desktop.AttemptQuizPages;
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
    /// Interaction logic for AttemptQuizWindow.xaml
    /// </summary>
    public partial class AttemptQuizWindow : Window
    {
        UserAnswers UserAns;
        Quiz Quiz;
        int Pointer = -1;

        private async void OnNext(object? sender, EventArgs e)
        {
            var page = sender as AttemptQuizQuestionPage;

            if (page is not null)
            {
                int question_id = Quiz.questions[Pointer].id!.Value;
                UserAns.time[question_id] = page.ElapsedTime;
                foreach (var answer in Quiz.questions[Pointer].answers)
                {
                    if (answer.is_correct == 1)
                    {
                        UserAns.selected[question_id].Add(answer.id!.Value);
                    }
                }
            }

            Pointer += 1;
            if (Pointer < Quiz.questions.Count)
            {
                var questionPage = new AttemptQuizQuestionPage(Quiz.questions[Pointer], Pointer);
                questionPage.OnNext += OnNext;
                QuizAttemptFrame.Content = questionPage;
            } else
            {
                EvaluationResults results = new();
                try
                {
                    results = await API.Evaluate(UserAns, Quiz);
                }
                catch (SvelteError err)
                {
                    Utils.HandleError(err);
                }
                catch (Exception)
                {
                    Utils.ErrorMsg("Unknown error Occured");
                }
                var endPage = new AttemptQuizLastPage(results);
                QuizAttemptFrame.Content = endPage;
            }
        }
        public AttemptQuizWindow(Quiz quiz)
        {
            InitializeComponent();
            UserAns = new UserAnswers(quiz);
            Quiz = quiz;

            foreach(var question in Quiz.questions)
            {
                foreach(var answer in question.answers)
                {
                    answer.is_correct = 0;
                }
            }

            var frontPage = new AttemptQuizFrontPage(quiz);
            frontPage.OnNext += OnNext;
            QuizAttemptFrame.Content = frontPage;
        }
    }
}
