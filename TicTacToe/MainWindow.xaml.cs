using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Player CurrentPlayer = Player.X;
        private int Player1_Score = 0;
        private int Player2_Score = 0;
        private List<Button> AllBtns = new List<Button>();
        private List<Button> BtnTopRow = new List<Button>();        //---
        private List<Button> BtnMidRow = new List<Button>();        //---
        private List<Button> BtnBotRow = new List<Button>();        //---
        private List<Button> BtnLeftLine = new List<Button>();      //|||
        private List<Button> BtnMidLine = new List<Button>();       //|||
        private List<Button> BtnRightLine = new List<Button>();     //|||
        private List<Button> BtnDiagonalLeft = new List<Button>();  // \
        private List<Button> BtnDiagonalRight = new List<Button>(); // /

        public MainWindow()
        {
            InitializeComponent();

            AllBtns.Add(topLeftBtn);        //1
            AllBtns.Add(topMiddleBtn);      //2
            AllBtns.Add(topRightBtn);       //3
            AllBtns.Add(middleLeftBtn);     //4
            AllBtns.Add(middleMiddleBtn);   //5
            AllBtns.Add(middleRightBtn);    //6
            AllBtns.Add(bottomLeftBtn);     //7
            AllBtns.Add(bottomMiddleBtn);   //8
            AllBtns.Add(bottomRightBtn);    //9

            FillWinningLists();

            restartBtn.Click += RestartClick;
            resetCounterBtn.Click += ResetCounterClick;

            AddClicks();
        }

        private void ResetCounterClick(object Sender, EventArgs e) {
            Player1_Score = 0;
            Player2_Score = 0;
            player1Score.Content = 0;
            player2Score.Content = 0;
        }

        //Damit die Gewinnsituation leichter zu berechnen ist, befuelle extra Listen fuer vertikale Reihen,
        //horizontale Reihen und Diagonalen
        private void FillWinningLists() {
            foreach(Button EachButton in AllBtns)
            {
                //1 topLeftBtn
                if (EachButton.Name == "topLeftBtn")
                {
                    BtnTopRow.Add(EachButton);
                    BtnLeftLine.Add(EachButton);
                    BtnDiagonalLeft.Add(EachButton);
                }

                //2 topMiddleBtn
                if (EachButton.Name == "topMiddleBtn")
                {
                    BtnTopRow.Add(EachButton);
                    BtnMidLine.Add(EachButton);
                }

                //3 topRightBtn
                if (EachButton.Name == "topRightBtn")
                {
                    BtnTopRow.Add(EachButton);
                    BtnRightLine.Add(EachButton);
                    BtnDiagonalRight.Add(EachButton);
                }

                //4 middleLeftBtn
                if (EachButton.Name == "middleLeftBtn")
                {
                    BtnMidRow.Add(EachButton);
                    BtnLeftLine.Add(EachButton);
                }

                //5 middleMiddleBtn
                if (EachButton.Name == "middleMiddleBtn")
                {
                    BtnMidRow.Add(EachButton);
                    BtnMidLine.Add(EachButton);
                    BtnDiagonalLeft.Add(EachButton);
                    BtnDiagonalRight.Add(EachButton);
                }

                //6 middleRightBtn
                if (EachButton.Name == "middleRightBtn")
                {
                    BtnMidRow.Add(EachButton);
                    BtnRightLine.Add(EachButton);
                }

                //7 bottomLeftBtn
                if (EachButton.Name == "bottomLeftBtn")
                {
                    BtnBotRow.Add(EachButton);
                    BtnLeftLine.Add(EachButton);
                    BtnDiagonalRight.Add(EachButton);
                }

                //8 bottomMiddleBtn
                if (EachButton.Name == "bottomMiddleBtn")
                {
                    BtnBotRow.Add(EachButton);
                    BtnMidLine.Add(EachButton);
                }

                //9 bottomRightBtn
                if (EachButton.Name == "bottomRightBtn")
                {
                    BtnBotRow.Add(EachButton);
                    BtnRightLine.Add(EachButton);
                    BtnDiagonalLeft.Add(EachButton);
                }
            }
        }

        private void RestartGame() {
            foreach (Button button in AllBtns)
            {
                button.Content = " ";
                button.IsEnabled = true;
            }
            CurrentPlayer = Player.X;
        }

        private void RestartClick(object Sender, EventArgs e) {
            RestartGame();
        }

        private void AddClicks() {
            foreach(Button button in AllBtns)
            {
                button.Click += ButtonClick;
            }
        }

        //Spielfelder Knopf Event
        private void ButtonClick(object Sender, EventArgs e)
        {
            Button PlayfieldButton = (Button)Sender;
            PlayfieldButton.Content = CurrentPlayer.ToString();
            PlayfieldButton.IsEnabled = false;
            CheckIfWinner();

            CurrentPlayer = CurrentPlayer == Player.X ? Player.O : Player.X;
        }

        private void CheckIfWinner()
        {
            if (CheckLines(BtnTopRow, BtnMidRow, BtnBotRow) ||
                CheckLines(BtnLeftLine, BtnMidLine, BtnRightLine) ||
                CheckLines(BtnDiagonalLeft, BtnDiagonalRight))
            {
                if (CurrentPlayer.ToString() == "X")
                {
                    Player1_Score++;
                    player1Score.Content = Player1_Score;
                } else
                {
                    Player2_Score++;
                    player2Score.Content = Player2_Score;
                }
                RestartGame();
            }
        }

        private bool CheckLines(List<Button> List1, List<Button> List2)
        {
            if(List1.All(o => o.Content.Equals(CurrentPlayer.ToString())) ||
                List2.All(o => o.Content.Equals(CurrentPlayer.ToString())))
            {
                return true;
            }

            return false;
        }

        private bool CheckLines(List<Button> List1, List<Button> List2, List<Button> List3) {
            if(List1.All(o => o.Content.Equals(CurrentPlayer.ToString())) ||
                List2.All(o => o.Content.Equals(CurrentPlayer.ToString())) ||
                List3.All(o => o.Content.Equals(CurrentPlayer.ToString())))
            {
                return true;
            }

            return false;
        }

        private enum Player {
            X,
            O
        }
    }
}