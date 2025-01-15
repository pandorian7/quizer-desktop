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
    /// Interaction logic for AttemptQuizLastPage.xaml
    /// </summary>
    public partial class AttemptQuizLastPage : Page
    {
        public AttemptQuizLastPage(EvaluationResults results)
        {
            InitializeComponent();
            ScoreBox.Text = $"{(int)Math.Round(results.score)}/{results.total}";
            TimeTakenBox.Text = $"Time Taken: {Utils.FormatTime(results.time_taken)}";
        }
    }
}
