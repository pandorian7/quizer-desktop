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

    }
}
