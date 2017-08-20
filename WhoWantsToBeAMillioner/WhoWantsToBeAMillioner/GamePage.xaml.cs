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
using DataAccess;
using BusinessObjectModels;

namespace WhoWantsToBeAMillioner
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Window
    {

        DataManager dataManager = new DataManager();

        public GamePage()
        {
            InitializeComponent();
        }



        private void Status_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MW = new MainWindow();
            MW.Show();
            this.Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<int> categoryList = dataManager.getCategoryList();

            dataManager.GetNextQuestion(categoryList.FirstOrDefault());

            QuestionTexBox.Text = Questions.Question;
            A.Content = Questions.VariantA;
            B.Content = Questions.VariantB;
            C.Content = Questions.VariantC;
            D.Content = Questions.VariantD;

        }


        private void A_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void B_Click(object sender, RoutedEventArgs e)
        {

        }

        private void C_Click(object sender, RoutedEventArgs e)
        {

        }

        private void D_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
