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
                MessageBox.Show("LogOut Failed");
            }
        }

        private async void Quizer_ContentRendered(object sender, EventArgs e)
        {
            var quizes = await API.GetQuizes();
            if (quizes is null)
            {
                MessageBox.Show("null");
            } else
            {
                MessageBox.Show(quizes[1].title);
            }
            //string json = @"{""quizes"": { ""id"":39,""title"":""JavaScript MCQs"",""description"":""Explore the most useful concepts of javascript"",""points"":800,""owner_id"":1,""username"":""yasithnp7""}}";
            //var data = JsonSerializer.Deserialize<QuizJson>(json);
            //if (data is not null)
            //{
            //    MessageBox.Show(data.ToString());
            //}
            //else
            //{
            //    MessageBox.Show("null");
            //}
        }
    }
}
