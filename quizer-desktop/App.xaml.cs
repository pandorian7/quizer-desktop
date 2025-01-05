using System.Configuration;
using System.Data;
using System.Net.Http;
using System.Windows;

namespace quizer_desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static HttpClient HTTP { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            HTTP = new HttpClient()
            {
                BaseAddress = new Uri(Data.BaseUrl)
            };
            HTTP.DefaultRequestHeaders.Accept.Clear();
            HTTP.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
    }


}
