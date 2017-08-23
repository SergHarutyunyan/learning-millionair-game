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
using System.Threading;

namespace WhoWantsToBeAMillioner
{

    public partial class GamePage : Window
    {

        static List<Categories> categoryList = new List<Categories>();
        DataManager dataManager = new DataManager();
        static int answer = 0;
        static bool filled = false;
        static bool classic5050Clicked = false;
        static int price = 0;

        public GamePage()
        {
            InitializeComponent();
        }

        private void Status_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GoBack()
        {
            MainWindow MW = new MainWindow();
            MW.Show();
            this.Hide();
            Think.Stop();

        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            GoBack();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Think.Play();

            categoryList = dataManager.GetCategoryList();
            dataManager.GetNextQuestion(categoryList.FirstOrDefault().Id);
            FillBoxes();
            price = 0;

        }


        private void Blinking(Button winning, Button selected, bool isTrue)
        {
            AnswerSound(isTrue);
            if (winning.Content == selected.Content)
            {
                selected.Background = Brushes.Green;
                WaitNSeconds(0.2);
                selected.Background = Brushes.DarkOrange;
                WaitNSeconds(0.2);
                selected.Background = Brushes.Green;
                WaitNSeconds(0.2);
                selected.Background = Brushes.DarkOrange;
                WaitNSeconds(0.2);
                selected.Background = Brushes.Green;
                WaitNSeconds(1);
            }
            else
            {
                winning.Background = Brushes.Green;
                WaitNSeconds(0.2);
                winning.Background = Brushes.Black;
                WaitNSeconds(0.2);
                winning.Background = Brushes.Green;
                WaitNSeconds(0.2);
                winning.Background = Brushes.Black;
                WaitNSeconds(0.2);
                winning.Background = Brushes.Green;
                WaitNSeconds(1);
            }

        }

        private void FillBoxes()
        {

            MakeNotClickable();

            if (filled)
            {
                QuestionTexBox.Text = "";
                A.Content = "";
                B.Content = "";
                C.Content = "";
                D.Content = "";
            }

            WaitNSeconds(1);
            QuestionTexBox.Text = Questions.Question;
            WaitNSeconds(1);
            A.Content = Questions.VariantA;
            WaitNSeconds(1);
            B.Content = Questions.VariantB;
            WaitNSeconds(1);
            C.Content = Questions.VariantC;
            WaitNSeconds(1);
            D.Content = Questions.VariantD;
            answer = Questions.Answer;
            filled = true;
            MakeClickable();

        }

        private void CleanUpButtons(Button button)
        {
            A.IsHitTestVisible = true;
            B.IsHitTestVisible = true;
            C.IsHitTestVisible = true;
            D.IsHitTestVisible = true;
            if(!classic5050Clicked)
                Classic5050.IsHitTestVisible = true;
            button.Background = Brushes.Black;
        }

        private void MakeClickable()
        {
            A.IsHitTestVisible = true;
            B.IsHitTestVisible = true;
            C.IsHitTestVisible = true;
            D.IsHitTestVisible = true;
            if (!classic5050Clicked)
                Classic5050.IsHitTestVisible = true;
        }

        private void MakeNotClickable()
        {
            A.IsHitTestVisible = false;
            B.IsHitTestVisible = false;
            C.IsHitTestVisible = false;
            D.IsHitTestVisible = false;
            Classic5050.IsHitTestVisible = false;
        }

        private void AnswerSound(bool correct)
        {
            Think.Stop();

            if (correct) { Correct.Stop(); Correct.Play();  }
            else Loose.Play();           
        }

        private void WaitNSeconds(double segundos)
        {
            DateTime _desired = DateTime.Now.AddSeconds(segundos);
            while (DateTime.Now < _desired)
            {
                System.Windows.Forms.Application.DoEvents();
            }
        }

        private void WaitingAnswer(Button button)
        {
            button.Background = Brushes.DarkOrange;
            MakeNotClickable();
            WaitNSeconds(3);

        }

        private void ShowAnswer(Button selected, bool isTrue)
        {

            switch (answer)
            {
                case 1:
                    Blinking(A, selected, isTrue);
                    break;
                case 2:
                    Blinking(B, selected, isTrue);
                    break;
                case 3:
                    Blinking(C, selected, isTrue);
                    break;
                case 4:
                    Blinking(D, selected, isTrue);
                    break;
            }
        }


        private void ClickHandling(Button button, byte answerId)
        {
            WaitingAnswer(button);

            if (answer == answerId)
            {
                price = categoryList.FirstOrDefault().Price;
                ShowAnswer(button, true);
                WaitNSeconds(2);
                CleanUpButtons(button);
                categoryList.RemoveAt(0);
                dataManager.GetNextQuestion(categoryList.FirstOrDefault().Id);

                if (categoryList.Count != 0)
                {
                    FillBoxes();
                }
                else
                {
                    GameOver GO = new GameOver();
                    GO.PriceBox.Text = "Winning price: " + price + "$";
                    this.Close();
                    GO.Show();
                }
            }
            else
            {
                ShowAnswer(button, false);
                WaitNSeconds(3);
                GameOver GO = new GameOver();
                GO.PriceBox.Text = "Winning price: " + price + "$";
                this.Close();
                GO.Show();

            }
        }

        private void A_Click(object sender, RoutedEventArgs e)
        {
            ClickHandling(A,1);
        }

        private void B_Click(object sender, RoutedEventArgs e)
        {
            ClickHandling(B,2);
        }

        private void C_Click(object sender, RoutedEventArgs e)
        {
            ClickHandling(C,3);
        }

        private void D_Click(object sender, RoutedEventArgs e)
        {
            ClickHandling(D,4);
        }

        Random rand = new Random();
        static int previousRandom = -1;

        private int GetUnnecessaryVariant()
        {
            int randNumber = rand.Next(1, 4);

            if (answer != randNumber && previousRandom != randNumber) { previousRandom = randNumber; return randNumber; }
            else return GetUnnecessaryVariant();
        }


        private void Classic5050_Click(object sender, RoutedEventArgs e)
        {
            classic5050Clicked = true;
            Think.Stop();
            Help50.Play();

            Classic5050.Visibility = Visibility.Hidden;
            Classic5050used.Visibility = Visibility.Visible;
            Classic5050used.IsHitTestVisible = false;
         
            int firstVariant = GetUnnecessaryVariant();
            int secondVariant = GetUnnecessaryVariant();

            if (firstVariant == 1 || secondVariant == 1) { A.Content = ""; A.IsHitTestVisible = false; }
            if (firstVariant == 2 || secondVariant == 2) { B.Content = ""; B.IsHitTestVisible = false; }
            if (firstVariant == 3 || secondVariant == 3) { C.Content = ""; C.IsHitTestVisible = false; }
            if (firstVariant == 4 || secondVariant == 4) { D.Content = ""; D.IsHitTestVisible = false; }

            WaitNSeconds(2);
            Think.Play();

        }
    }
}
