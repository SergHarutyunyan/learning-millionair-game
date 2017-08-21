using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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

namespace WhoWantsToBeAMillioner
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {

            mediaElement1.Stop();
            GamePage GP = new GamePage();
            this.Hide();
            GP.Show();
            


        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            mediaElement1.Source = new Uri(@"C:\Users\Sergey.Harutyunyan\Documents\GIT\MillionerWPF\WhoWantsToBeAMillioner\WhoWantsToBeAMillioner\Mp3\millionaire.mp3");
            mediaElement1.Play();

        }
    }
}
