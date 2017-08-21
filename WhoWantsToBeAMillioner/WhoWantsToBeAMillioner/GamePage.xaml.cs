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
        static int FirstAnswer = 0;
        static int price = 0;

        public GamePage()
        {
            InitializeComponent();
        }

        private void soundPlay(MediaElement M, string uri)
        {
            M.Source = new Uri(uri);
            M.Play();
        }

        private void soundStop(MediaElement M)
        {
            M.Stop();
        }


        private void Status_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GoBack()
        {
            MainWindow MW = new MainWindow();
            MW.Show();
            this.Hide();
            soundStop(GamePlaySound);

        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            GoBack();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            soundPlay(GamePlaySound, @"C:\Users\Sergey.Harutyunyan\Documents\GIT\MillionerWPF\WhoWantsToBeAMillioner\WhoWantsToBeAMillioner\Mp3\Think.mp3");

            categoryList = dataManager.GetCategoryList();
            dataManager.GetNextQuestion(categoryList.FirstOrDefault().Id);
            FillBoxes();
            price = 0;

        }

        private void FillBoxes()
        {
            MakeNotClickable();

            if(filled)
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
            soundPlay(GamePlaySound, @"C:\Users\Sergey.Harutyunyan\Documents\GIT\MillionerWPF\WhoWantsToBeAMillioner\WhoWantsToBeAMillioner\Mp3\Think.mp3");



        }

        private void CleanUpButtons(Button button)
        {
            A.IsHitTestVisible = true;
            B.IsHitTestVisible = true;
            C.IsHitTestVisible = true;
            D.IsHitTestVisible = true;
            button.Background = Brushes.Black;        
        }

        private void MakeClickable()
        {
            A.IsHitTestVisible = true;
            B.IsHitTestVisible = true;
            C.IsHitTestVisible = true;
            D.IsHitTestVisible = true;

        }

        private void MakeNotClickable()
        {
            A.IsHitTestVisible = false;
            B.IsHitTestVisible = false;
            C.IsHitTestVisible = false;
            D.IsHitTestVisible = false;

        }

        private void AnswerSound(bool correct)
        {
            if (correct)
            {
                soundStop(GamePlaySound);
                soundPlay(GamePlaySound, @"C:\Users\Sergey.Harutyunyan\Documents\GIT\MillionerWPF\WhoWantsToBeAMillioner\WhoWantsToBeAMillioner\Mp3\Correct.mp3");
            }
            else
            {
                soundStop(GamePlaySound);
                soundPlay(GamePlaySound, @"C:\Users\Sergey.Harutyunyan\Documents\GIT\MillionerWPF\WhoWantsToBeAMillioner\WhoWantsToBeAMillioner\Mp3\Loosing.mp3");
            }
        }

        private void WaitNSeconds(int segundos)
        {
            if (segundos < 1) return;
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

        private void showAnswer()
        {
            switch (answer)
            {
                case 1:
                    A.Background = Brushes.Green;
                    break;
                case 2:
                    B.Background = Brushes.Green;
                    break;
                case 3:
                    C.Background = Brushes.Green;
                    break;
                case 4:
                    D.Background = Brushes.Green;
                    break;
            }
        }


        private void ClickHandling(Button button)
        {
            WaitingAnswer(button);
            showAnswer();


            if (answer == Convert.ToInt32(button.Uid))
            {
                price = categoryList.FirstOrDefault().Price;

                AnswerSound(true);
                WaitNSeconds(3);
                CleanUpButtons(button);
                categoryList.RemoveAt(0);
                dataManager.GetNextQuestion(categoryList.FirstOrDefault().Id);
               
                if (categoryList.Count != 0)
                {                   
                    FillBoxes();                  
                }
                else
                {
                    MessageBox.Show("You have won " + price + "$");
                    GoBack();                  
                }

                
            }
            else
            {
                AnswerSound(false);
                WaitNSeconds(3);
                MessageBox.Show("You have won " + price + "$");
                GoBack();
            }
        }

        private void A_Click(object sender, RoutedEventArgs e)
        {
            ClickHandling(A);
        }

        private void B_Click(object sender, RoutedEventArgs e)
        {
            ClickHandling(B);
        }

        private void C_Click(object sender, RoutedEventArgs e)
        {
            ClickHandling(C);
        }

        private void D_Click(object sender, RoutedEventArgs e)
        {
            ClickHandling(D);
        }

    }
}
