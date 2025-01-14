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

namespace quizer_desktop.EditQuizPages
{
    /// <summary>
    /// Interaction logic for EditQuizQuestionPage.xaml
    /// </summary>
    public partial class EditQuizQuestionPage : Page
    {
        public event EventHandler<EventArgs>? OnDelete;
        public EditQuizQuestionPage()
        {
            InitializeComponent();
        }

        private void DeleteQuestionBtn_Click(object sender, RoutedEventArgs e)
        {
            OnDelete?.Invoke(this, EventArgs.Empty);
        }
    }
}
