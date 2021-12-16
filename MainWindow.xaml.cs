using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace RedGreenLight
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double playerPosition;
        private double roudnTime;
        DispatcherTimer gameTimer = new DispatcherTimer();
        Random randomColor = new Random();

        public MainWindow()
        {
            InitializeComponent();
            RoundTimer.Content = "45";
            gameTimer.Interval = TimeSpan.FromSeconds(1);
            gameTimer.Tick += MainEventTimer;
            StartGame();
        }

        private void MainEventTimer(object sender, EventArgs e)
        {

            roudnTime -= TimeSpan.FromSeconds(1).TotalSeconds;
            RoundTimer.Content = roudnTime;
            if (roudnTime == 0)
                EndGame();
            else if (randomColor.Next(10) >= 5)
            {
                LightMessage.Content = "Беги!";
                GreenLight.Visibility = Visibility.Visible;
                RedLight.Visibility = Visibility.Hidden;
            }
            else
            {
                LightMessage.Content = "Стой!";
                GreenLight.Visibility = Visibility.Hidden;
                RedLight.Visibility = Visibility.Visible;
            }
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (RedLight.Visibility == Visibility.Visible)
            {
                GameMessage.Content = "Проиграл!";
                EndGame();
            }
            else
            {
                playerPosition += 15;
                Canvas.SetBottom(MainCanvas.Children[3], playerPosition);
                if (50 + Canvas.GetBottom(Player) >= Canvas.GetBottom(Finish))
                {
                    GameMessage.Content = "Ты выйграл!";
                    EndGame();
                }

            }
        }

        private void StartGame()
        {
            Canvas.SetBottom(Player, 30);
            LightMessage.Content = "";
            GreenLight.Visibility = Visibility.Hidden;
            RedLight.Visibility = Visibility.Hidden;
            playerPosition = 30;
            roudnTime = 45;
            RoundTimer.Content = roudnTime;
            gameTimer.Start();
        }

        private void EndGame()
        {
            gameTimer.Stop();
            NewGame.Visibility = Visibility.Visible;
            GameMessage.Visibility = Visibility.Visible;
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            NewGame.Visibility = Visibility.Hidden;
            GameMessage.Visibility = Visibility.Hidden;
            StartGame();
        }
    }
}
