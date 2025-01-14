using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Windows;

namespace quizer_desktop
{
    static class API
    {
        public static async Task Login(string username, string password)
        {
            var creds = new Credentials { username = username, password = password };
            var content = Utils.JSONContent(ref creds);
            var res = await App.HTTP.PostAsync("/api/login", content);
            Utils.EnsureSvelteSuccess(res);
        }

        public static async Task Register(string username, string password)
        {
            var creds = new Credentials { username = username, password = password };
            var content = Utils.JSONContent(ref creds);
            var res = await App.HTTP.PostAsync("/api/register", content);
            Utils.EnsureSvelteSuccess(res);
        }

        public static async Task LogOut()
        {
            var res = await App.HTTP.PostAsync("/api/logout", null);
            Utils.EnsureSvelteSuccess(res);
        }

        public static async Task<List<QuizJson>> GetQuizes() {
            var res = await App.HTTP.GetAsync("/api/quizes");
            Utils.EnsureSvelteSuccess(res);
            var quizes = JsonSerializer.Deserialize<QuizResultsJson>(res.Content.ReadAsStringAsync().Result);
            if (quizes is not null)
            {

                return quizes.quizes;
            } else
            {
                throw new Exception("Failed to parse quizes from JSON");
            }
        }

        public static async Task CreateQuiz(string title)
        {
            var newquiz = new NewQuiz { title = title};
            var res = await App.HTTP.PostAsync("/api/quizes", Utils.JSONContent(ref newquiz));
            Utils.EnsureSvelteSuccess(res);
        }

        public static async Task DeleteQuiz(int quiz_id)
        {
            var res = await App.HTTP.DeleteAsync($"/api/quizes/{quiz_id}");
            Utils.EnsureSvelteSuccess(res);
        }

        public static async Task<Quiz> GetQuiz(int quiz_id)
        {
            var res = await App.HTTP.GetAsync($"/api/quizes/{quiz_id}");
            Utils.EnsureSvelteSuccess(res);
            var quiz = JsonSerializer.Deserialize<Quiz>(res.Content.ReadAsStringAsync().Result);
            if (quiz is not null)
            {

                return quiz;
            }
            else
            {
                throw new Exception("Failed to parse quizes from JSON");
            }
        }

        public static async Task UpdateQuiz(Quiz quiz)
        {
            UpdateQuiz update = new()
            {
                id = quiz.id,
                title = quiz.title,
                description = quiz.description,
                points = quiz.points
            };

            var res = await App.HTTP.PutAsync($"/api/quizes/{quiz.id}", Utils.JSONContent(ref update));
            
            Utils.EnsureSvelteSuccess(res);
        }

        public static async Task UpdateQuestion(Question q)
        {
            UpdateQuestion update = new()
            {
                question = new() { question = q.question, multiple_answers = q.multiple_answers, duration = q.duration },
                answers = q.answers
            };

            var res = await App.HTTP.PutAsync($"/api/questions/{q.id}", Utils.JSONContent(ref update));
            Utils.EnsureSvelteSuccess(res);
        }

        public static async Task CreateQuestion(int quiz_id, Question q)
        {
            AddQuestion add = new()
            {
                quiz_id = quiz_id,
                question = new() { question = q.question, multiple_answers = q.multiple_answers, duration = q.duration },
                answers = q.answers
            };
            var res = await App.HTTP.PostAsync("/api/questions/", Utils.JSONContent(ref add));
            Utils.EnsureSvelteSuccess(res);
        }

        public static async Task DeleteQuestion(int question_id)
        {
            var res = await App.HTTP.DeleteAsync($"/api/questions/{question_id}");
            Utils.EnsureSvelteSuccess(res);
        }

    }
}
