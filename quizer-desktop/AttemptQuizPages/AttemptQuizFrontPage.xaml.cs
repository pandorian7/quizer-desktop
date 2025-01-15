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

namespace quizer_desktop.AttemptQuizPages
{
    /// <summary>
    /// Interaction logic for AttemptQuizFrontPage.xaml
    /// </summary>
    public partial class AttemptQuizFrontPage : Page
    {
        public event EventHandler<EventArgs>? OnNext;
        public AttemptQuizFrontPage(Quiz quiz)
        {
            InitializeComponent();
            QuizTitleLabel.Text = quiz.title;
            QuizDescriptionLabel.Text = quiz.description;
            QuizPointsLabel.Text = $"{quiz.points} Points";
        }

        private void AttemptBtn_Click(object sender, RoutedEventArgs e)
        {
            OnNext?.Invoke(this, EventArgs.Empty);
        }
    }
}
