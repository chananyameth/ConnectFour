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
using ארבע_בשורה;

namespace Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MyColor currentColor;
        public Board board;
        public int moves;

        public MainWindow()
        {
            InitializeComponent();
            initVariables();
        }

        private void initVariables()
        {
            currentColor = MyColor.Red;
            board = new Board();
            moves = 0;

            ImageBrush brush = new ImageBrush();
            ImageBrush brush1 = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri("../../src/arrow-down.png", UriKind.Relative));//C:/חנניה/סתם דברים/C# פרוייקטים/ארבע בשורה/Game
            brush1.ImageSource = new BitmapImage(new Uri("../../src/table.png", UriKind.Relative));
            c0.Background = brush;
            c1.Background = brush;
            c2.Background = brush;
            c3.Background = brush;
            c4.Background = brush;
            c5.Background = brush;
            c6.Background = brush;

            c0.Visibility = Visibility.Visible;
            c1.Visibility = Visibility.Visible;
            c2.Visibility = Visibility.Visible;
            c3.Visibility = Visibility.Visible;
            c4.Visibility = Visibility.Visible;
            c5.Visibility = Visibility.Visible;
            c6.Visibility = Visibility.Visible;

            tableImage.Background = brush1;

            currColor.Fill = new SolidColorBrush((currentColor == MyColor.Red ? Colors.Red : Colors.Yellow));
            reset.Background = new SolidColorBrush(Colors.Red);
        }

        private MyColor replaceColor()
        {
            if (currentColor == MyColor.Red)
                currentColor = MyColor.Yellow;
            else if (currentColor == MyColor.Yellow)
                currentColor = MyColor.Red;

            currColor.Fill = new SolidColorBrush((currentColor == MyColor.Red ? Colors.Red : Colors.Yellow));

            return currentColor;
        }

        private void hideButton(int column)
        {
            switch (column)
            {
                case 0:
                    c0.Visibility = Visibility.Hidden;
                    break;
                case 1:
                    c1.Visibility = Visibility.Hidden;
                    break;
                case 2:
                    c2.Visibility = Visibility.Hidden;
                    break;
                case 3:
                    c3.Visibility = Visibility.Hidden;
                    break;
                case 4:
                    c4.Visibility = Visibility.Hidden;
                    break;
                case 5:
                    c5.Visibility = Visibility.Hidden;
                    break;
                case 6:
                    c6.Visibility = Visibility.Hidden;
                    break;
                default:
                    break;
            }
        }

        private void Winner(MyColor c)
        {
            if (c == MyColor.None)
                MessageBox.Show("No one won... :(", "Standoff!", MessageBoxButton.OK, MessageBoxImage.None);
            else
                MessageBox.Show("Congratulations! the " + c + " won the " + replaceColor() + "!", "Congratulations, " + c + "!", MessageBoxButton.OK, MessageBoxImage.None);
            MessageBoxResult res = MessageBox.Show("Do you want another game?", "More?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
                reset_Click(this, new RoutedEventArgs());
            else
                this.Close();
        }

        private bool doTurn(int column)
        {
            if (board.highest(column) == 4)
            {
                hideButton(column);
                //MessageBox.Show("Error! This column is full, try others.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                //return true;//continue the game
            }
            if (!board.put(column, currentColor))
            {
                MessageBox.Show("Error! Can't put the diskit into column number " + column, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            Ellipse disk = new Ellipse();
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = (currentColor == MyColor.Red ? Colors.Red : Colors.Yellow);
            disk.Fill = mySolidColorBrush;
            disk.Width = 53;
            disk.Height = 53;
            Grid.SetZIndex(disk, -1);
            MainGrid.Children.Add(disk);
            Grid.SetRow(disk, 7 - board.rowOfFree(column) + 1);//we already inserted the new one
            Grid.SetColumn(disk, column + 1);

            moves++;
            movesNr.Content = moves;
            if (board.checkForWinner(column, -1, currentColor) != MyColor.None)
                Winner(currentColor);
            replaceColor();

            if (moves == 42)//the board is full
                Winner(MyColor.None);

            return true;
        }

        private void c0_Click(object sender, RoutedEventArgs e)
        {
            if (!doTurn(0))
            {
                MessageBox.Show("Error! There was a problem somewhere!", "Error!");
                this.Close();
            }
        }
        private void c1_Click(object sender, RoutedEventArgs e)
        {
            if (!doTurn(1))
            {
                MessageBox.Show("Error! There was a problem somewhere!", "Error!");
                this.Close();
            }
        }
        private void c2_Click(object sender, RoutedEventArgs e)
        {
            if (!doTurn(2))
            {
                MessageBox.Show("Error! There was a problem somewhere!", "Error!");
                this.Close();
            }
        }
        private void c3_Click(object sender, RoutedEventArgs e)
        {
            if (!doTurn(3))
            {
                MessageBox.Show("Error! There was a problem somewhere!", "Error!");
                this.Close();
            }
        }
        private void c4_Click(object sender, RoutedEventArgs e)
        {
            if (!doTurn(4))
            {
                MessageBox.Show("Error! There was a problem somewhere!", "Error!");
                this.Close();
            }
        }
        private void c5_Click(object sender, RoutedEventArgs e)
        {
            if (!doTurn(5))
            {
                MessageBox.Show("Error! There was a problem somewhere!", "Error!");
                this.Close();
            }
        }
        private void c6_Click(object sender, RoutedEventArgs e)
        {
            if (!doTurn(6))
            {
                MessageBox.Show("Error! There was a problem somewhere!", "Error!");
                this.Close();
            }
        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = new MainWindow();
            win.Show();
            this.Close();
        }
    }
}
