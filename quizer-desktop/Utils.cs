using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace quizer_desktop
{
    public class Utils
    {
        public static void HandleError(Exception error)
        {
            MessageBox.Show($"Error: {error.Message}");
        }

        public static void CreateSvelteError(HttpResponseMessage res) {
            if (!res.IsSuccessStatusCode)
            {
                var json_err = JsonSerializer.Deserialize<SvelteJSONError>(res.Content.ReadAsStringAsync().Result);
                if (json_err is not null)
                {
                    throw new SvelteError(json_err.message);
                }
                else
                {
                    throw new SvelteError("Unknown Error");
                }
            } else
            {
                throw new Exception("request is successful. yet passed to Create Svelte Error function.");
            }
        }

        public static void EnsureSvelteSuccess(HttpResponseMessage res)
        {
            if (!res.IsSuccessStatusCode)
            {
                CreateSvelteError(res);
            }
        }

        public static StringContent JSONContent<T> (ref T obj) {
            string jsonString = JsonSerializer.Serialize(obj);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            return content;
        }
    }
}
