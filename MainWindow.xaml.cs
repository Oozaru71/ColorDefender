using ColorDefender.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ColorDefender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int cols = 7;
        private int rows = 6;

        private int score;
        private Board b;

        private Enemy enemy;

        private int turnCounter;

        private int deadaliens;

        public MainWindow()
        {
            InitializeComponent();
            buildGame();
        }

        public void buildGame()
        {



            GameView.ShowGridLines = true;

            for (int i = 0; i < cols; i++)
            {
                GameView.ColumnDefinitions.Add(new ColumnDefinition());

               
            }
            for (int i = 0; i < rows; i++)
            {
                GameView.RowDefinitions.Add(new RowDefinition());
            }
            b = new Board(rows, cols);
                b.showMe();

            addTiles();
            
            enemy=new Enemy("first");

            enemyView.Source =enemy.Source;
            updateStats();

        }
        public void addTiles()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Tile t = b.tileAt(i, j);
                    Grid.SetRow(t, i);
                    Grid.SetColumn(t, j);
                    GameView.Children.Add(t);
                    t.Click += buttonclick;
                }
            }
        }

        private void buttonclick(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            Tile t = (Tile)sender;

            HashSet<Tile> validN = new HashSet<Tile>();

            b.locateNexts(t.getRow(), t.getColumn(), t.getShape(), validN);
            if (validN.Count >= 3)
            {

                score++;
                if ((validN.Count) >=6)
                {
                    score++;
                }
                if ((validN.Count) >= 9)
                {
                    score++;
                }

                //System.Diagnostics.Debug.WriteLine("Score increased");
                // b.showMe();
                b.removeMatchingTiles(validN);
                // b.showMe();

                b.collapseColumns();
                //   b.showMe();


                // 
                updateBoard();

            }

        }

        private void updateBoard()
        {
            scoreView.Content = "Score: " + score;

            checkDeadAlien();

            GameView.Children.Clear();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (b.hasTileAt(i, j))
                    {
                        //  MessageBox.Show(b.tileAt(i,j).getShapeName()+" old");
                        Tile t = b.tileAt(i, j);
                        Grid.SetRow(t, i);
                        Grid.SetColumn(t, j);
                        GameView.Children.Add(t);
                        t.Click += buttonclick;
                    }
                    else
                    {
                        //MessageBox.Show(b.tileAt(i, j).getShapeName() + " new");
                        Tile t = new Tile(i, j);
                        Grid.SetRow(t, i);
                        Grid.SetColumn(t, j);
                        GameView.Children.Add(t);
                        t.Click += buttonclick;
                    }
                }
            }
            if (turnCounter < enemy.getTCounter())
            {
                turnCounter++;
            }
            else
            {
                turnCounter = 0;
                b.turnTile();
            }
            updateStats();
            if(!b.isMoveTrue()&&score<5)
            {
                MessageBox.Show("There are no more moves and you don't have the points to continue or make rainbow spheres!" +
                    "\n Game over! Exit and open the application to try again.");
                Environment.Exit(0);
            }
       

        }
        private void checkDeadAlien()
        {
            updateStats();
            if (enemy.getCHP() <= 0)
            {
                MessageBox.Show("You have destroyed this alien!\n" +
                    " You have gained +" +10+" points\n"+
                    "Another approaches!");
                score = score + 10;
                scoreView.Content = "Score: " + score;
                enemy = new Enemy();
                enemyView.Source = enemy.Source;
                deadaliens++;
                if (deadaliens > 5)
                {
                    MessageBox.Show("You have killed all the aliens!" +
                    "\n Game over! Exit and open the application to try again.");
                    Environment.Exit(0);
                
                }
                else
                    b.startBoard(); ;
            }
        }
        private void updateStats() 
        {
            eStats.Content = "A foe is here " +
                "\nHP ="+enemy.getCHP()+"/"+ enemy.getTHP()+
                "\nTurns before attack= "+(enemy.getTCounter()-turnCounter);
            scoreView.Content = "Score: " + score;
        }
        private void refreshClick(object sender, MouseEventArgs k)
        {
            MessageBoxResult result = MessageBox.Show("Would you like to use 30 points to refresh the board? ", "GameTip", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    if (score - 30 < 0)
                    {
                        MessageBox.Show("You don't have enough points!");
                    }
                    else
                    {
                        score = score - 30;
                        b.startBoard();
                        updateBoard();
                    }
                    break;
                case MessageBoxResult.No:

                    break;
            }
        }
        private void lEvent(object sender, RoutedEventArgs l)
        {
            if (score >= 5)
            {
                score=score - 5;
                b.turnRainbow();
                b.turnRainbow();
                updateBoard();
            }
            else
                MessageBox.Show("You don't have enough points!");
        }
        private void pcEvent(object sender, RoutedEventArgs l)
        {
            if (score >= 15)
            {
                score=score-15;
                enemy.setCHP(enemy.getCHP() - 20);
                updateBoard();

            }
            else
                MessageBox.Show("You don't have enough points!");
        }
        private void pbEvent(object sender, RoutedEventArgs l)
        {
            if (score >= 20)
            {
                score= score - 20;
                enemy.setCHP(enemy.getCHP() - 25);
                updateBoard();
            }
            else
                MessageBox.Show("You don't have enough points!");
        }

    }
}
