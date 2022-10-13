using System.Collections.Generic;
using System.Linq;

namespace ארבע_בשורה
{
    public enum MyColor
    {
        Red = 1,
        Yellow = 2,
        None = 3
    }

    public enum Directions
    {
        right = 1,
        up_right = 2,
        up = 3,
        up_left = 4,
        left = 5,
        down_left = 6,
        down = 7,
        down_right = 8
    }

    public class Board
    {
        // 05 15 25 35 45 55 65
        // 04 14 24 34 44 54 64
        // 03 13 23 33 43 53 63
        // 02 12 22 32 42 52 62
        // 01 11 21 31 41 51 61
        // 00 10 20 30 40 50 60

        public MyColor[,] board;

        public Board()
        {
            board = new MyColor[7, 6];
            for (int x = 0; x < 7; x++)
                for (int y = 0; y < 6; y++)
                    board[x, y] = MyColor.None;
        }

        public bool isFull(int column)
        {
            if (board[column, 5] != MyColor.None)//the highest is full - the column is full
                return true;
            return false;
        }

        public int rowOfFree(int column)//6 -> full
        {
            for (int i = 0; i < 6; i++)
            {
                if (board[column, i] == MyColor.None)
                    return i;
            }
            return 6;
        }

        public bool put(int column, MyColor c)
        {
            if (column < 0 || column > 6 || c == MyColor.None || isFull(column))
                return false;
            for (int i = 0; i < 6; i++)
            {
                if (board[column, i] == MyColor.None)
                {
                    board[column, i] = c;
                    return true;
                }
            }
            return false;
        }

        public MyColor checkForWinner()
        {
            for (int y = 0; y < 6; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    MyColor c = checkForWinner(x, y);
                    if (c != MyColor.None)
                        return c;
                }
            }
            return MyColor.None;
        }

        public int highest(int x)
        {
            for (int y = 5; y >= 0; y--)
            {
                if (board[x, y] != MyColor.None)
                    return y;
            }
            return -1;
        }

        /// <summary>
        /// checks if this disk makes someone winner
        /// </summary>
        /// <param name="x">x value</param>
        /// <param name="y">y value (-1 is the highest)</param>
        /// <param name="c">color to check</param>
        /// <returns>which color won</returns>
        public MyColor checkForWinner(int x, int y = -1, MyColor c = MyColor.None)
        {
            if (y == -1)
                y = highest(x);
            if (c == MyColor.None)
                c = board[x, y];

            int c_fit_xy = (c == board[x, y] ? 1 : 0);

            List<int> counts = new List<int>();
            counts.Add(count(x, y, c, (int)Directions.up) + count(x, y, c, (int)Directions.down) - c_fit_xy);//up_down
            counts.Add(count(x, y, c, (int)Directions.right) + count(x, y, c, (int)Directions.left) - c_fit_xy);//right_left
            counts.Add(count(x, y, c, (int)Directions.up_right) + count(x, y, c, (int)Directions.down_left) - c_fit_xy);//positive_slant
            counts.Add(count(x, y, c, (int)Directions.up_left) + count(x, y, c, (int)Directions.down_right) - c_fit_xy);//negative_slant

            if (counts.Max() >= 4)
                return c;
            else
                return MyColor.None;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">x value</param>
        /// <param name="y">y value</param>
        /// <param name="c">which MyColor</param>
        /// <param name="direction">1=right, 2=up-right, 3=up, 4=up-left, 5=left, 6=down-left, 7=down, 8=down-right</param>
        /// <returns>how many disks with the same MyColor in a row</returns>
        private int count(int x, int y, MyColor c, int direction)
        {
            if (x < 0 || x > 6 || y < 0 || y > 5 || c == MyColor.None || board[x, y] != c)
                return 0;

            switch (direction)
            {
                case 1://right
                    return 1 + count(x + 1, y + 0, c, direction);
                case 2://up-right
                    return 1 + count(x + 1, y + 1, c, direction);
                case 3://up
                    return 1 + count(x + 0, y + 1, c, direction);
                case 4://up-left
                    return 1 + count(x - 1, y + 1, c, direction);
                case 5://left
                    return 1 + count(x - 1, y + 0, c, direction);
                case 6://down-left
                    return 1 + count(x - 1, y - 1, c, direction);
                case 7://down
                    return 1 + count(x + 0, y - 1, c, direction);
                case 8://down-right
                    return 1 + count(x + 1, y - 1, c, direction);
                default:
                    return 0;
            }
        }
    }
}
