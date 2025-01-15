using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace quizer_desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    
        public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }


        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await API.Login(UserNameBox.Text, PasswordBox.Password);
                Data.username = UserNameBox.Text;
                var quizesWindow = new QuizesWindow();
                quizesWindow.Show();
                Close();
            } catch(SvelteError err)
            {
                Utils.HandleError(err);
            } catch (Exception)
            {
                Utils.ErrorMsg("Unknown error Occured");
            }
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await API.Register(UserNameBox.Text, PasswordBox.Password);
                MessageBox.Show("Successfully Registered", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SvelteError err)
            {
                Utils.HandleError(err);
            }
            catch (Exception)
            {
                Utils.ErrorMsg("Unknown error Occured");
            }
        }
    }
}