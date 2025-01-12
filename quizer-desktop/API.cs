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

    }
}
