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
    /// Interaction logic for AddQuizWindow.xaml
    /// </summary>
    public partial class AddQuizWindow : Window
    {
        public AddQuizWindow()
        {
            InitializeComponent();
        }

        private async void QuizAddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await API.CreateQuiz(QuizTitle.Text);
                DialogResult = true;
                Close();
                
            } catch (SvelteError err)
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
