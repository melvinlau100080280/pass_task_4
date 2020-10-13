using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel.Design;

namespace SnakeGame
{

    class Program
    {

        struct Position
        {
            public int row;
            public int col;
            public Position(int row, int col)
            {
                this.row = row;
                this.col = col;
            }
        }

        static void Main(string[] args)
        {
            
            // start game
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            // display this char on the console during the game
            char ch = '*';
            bool gameLive = true;
            ConsoleKeyInfo consoleKey; // holds whatever key is pressed

            Random rand = new Random();
            char food = '@';//  display food on the console during the game
            int fx, fy;
            int countSteps = 0; // count steps each time the snake moves
            //random generate food location
            fx = rand.Next(0, 79);  
            fy = rand.Next(5, 24);

            // location info & display
            int x = 0, y = 5; // y is 5 to allow the top row for directions & space
            x = rand.Next(20, 50); y = rand.Next(16, 24); //random snake starting point around center of map
            int dx = 1, dy = 0;
            int consoleWidthLimit = 79;
            int consoleHeightLimit = 24;

            //scoreboard variables
            int score = 0;
            char rank = '-';

            // clear to color
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Clear();

            // delay to slow down the character movement so you can see it
            int delayInMillisecs = 50;

            // whether to keep trails
            bool trail = false;

            //new obj
            Random rando = new Random();
            char obj = '|';
            int obstacleNum = 3;
            int[] object_x = new int[3];
            int[] object_y = new int[3];

            for (int i = 0;i < obstacleNum; ++i)
            {
                object_x[i] = rando.Next(0, 79);
                object_y[i] = rando.Next(5, 24);
            }

            //upper horizontal wall
            char hori_wall = '_';
            int hori_wallNum = 79;
            int[] object_upper_x = new int[79];
            int[] object_upper_y = new int[79];

            for (int i = 0; i < hori_wallNum; ++i)
            {
                object_upper_x[i] = i;
                object_upper_y[i] = 5;
            }

            //lower horizontal wall
            int[] object_lower_x = new int[79];
            int[] object_lower_y = new int[79];

            for (int i = 0; i < hori_wallNum; ++i)
            {
                object_lower_x[i] = i;
                object_lower_y[i] = 24;
            }

            //left side wall
            char verti_wall = '|';
            int verti_wallNum = 25;
            int[] object_left_x = new int[25];
            int[] object_left_y = new int[25];

            for (int i = 6; i < verti_wallNum; ++i)
            {
                object_left_x[i] = 0;
                object_left_y[i] = i;
            }

            //right side wall
            int[] object_right_x = new int[25];
            int[] object_right_y = new int[25];

            for (int i = 6; i < verti_wallNum; ++i)
            {
                object_right_x[i] = 79;
                object_right_y[i] = i;
            }

            do // until escape
            {
                // print directions at top, then restore position
                // save then restore current color
                ConsoleColor cc = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Arrows move up/down/right/left. Press 'esc' quit.");
                Console.WriteLine("====================="); //scoreboard design
                Console.WriteLine("Current Score:" + score); //scoreboard display
                Console.WriteLine("Achievements : Rank " + rank); //scoreboard rank
                Console.WriteLine("====================="); //scoreboard design
                Console.SetCursorPosition(x, y);
                Console.ForegroundColor = cc;

                // see if a key has been pressed
                if (Console.KeyAvailable)
                {
                    if (score >= 10) { rank = 'D'; }
                    if (score >= 20) { rank = 'C'; }
                    if (score >= 30) { rank = 'B'; }
                    if (score >= 40) { rank = 'A'; }
                    if (score == 50) { rank = 'S'; }

                    // get key and use it to set options
                    consoleKey = Console.ReadKey(true);
                    switch (consoleKey.Key)
                    {
                        case ConsoleKey.UpArrow: //UP
                            dx = 0;
                            dy = -1;
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        case ConsoleKey.DownArrow: // DOWN
                            dx = 0;
                            dy = 1;
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            break;
                        case ConsoleKey.LeftArrow: //LEFT
                            dx = -1;
                            dy = 0;
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case ConsoleKey.RightArrow: //RIGHT
                            dx = 1;
                            dy = 0;
                            Console.ForegroundColor = ConsoleColor.Black;
                            break;
                        case ConsoleKey.Escape: //END
                            gameLive = false;
                            break;
                    }

                    //set winning conditions
                    if (score == 50)
                    {
                        string s2 = "Press any key to end the game";
                        string s3 = "Congratulation you won the game!! You rank is at Rank " + rank;
                        Console.Clear();
                        Console.SetCursorPosition((Console.WindowWidth - s3.Length) / 2, Console.CursorTop);
                        Console.WriteLine(s3);
                        Console.SetCursorPosition((Console.WindowWidth - s2.Length) / 2, Console.CursorTop);
                        Console.WriteLine(s2);
                        Console.ReadKey();
                        gameLive = false;
                        break;
                    }
                }

                // find the current position in the console grid & erase the character there if don't want to see the trail
                Console.SetCursorPosition(x, y);
                if (trail == false)
                    Console.Write(' ');

                // calculate the new position
                // note x set to 0 because we use the whole width, but y set to 1 because we use top row for instructions
                x += dx;
                if (x >= consoleWidthLimit)
                {
                    string s = "Game Over!! You hit an obstacle, you are at Rank " + rank;
                    string s2 = "Press any key to end the game";
                    x = 0;
                    Console.Clear();
                    Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
                    Console.WriteLine(s);
                    Console.SetCursorPosition((Console.WindowWidth - s2.Length) / 2, Console.CursorTop);
                    Console.WriteLine(s2);
                    Console.ReadKey();
                    gameLive = false;
                    break;
                }

                if (x <= 0)
                {
                    string s = "Game Over!! You hit an obstacle, you are at Rank " + rank;
                    string s2 = "Press any key to end the game";
                    x = consoleWidthLimit;
                    Console.Clear();
                    Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
                    Console.WriteLine(s);
                    Console.SetCursorPosition((Console.WindowWidth - s2.Length) / 2, Console.CursorTop);
                    Console.WriteLine(s2);
                    Console.ReadKey();
                    gameLive = false;
                    break;
                }


                y += dy;
                if (y >= consoleHeightLimit)
                {
                    string s = "Game Over!! You hit an obstacle, you are at Rank " + rank;
                    string s2 = "Press any key to end the game";
                    y = 5; // 2 due to top spaces used for directions, 3 more for scoreboard and achievements
                    Console.Clear();
                    Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
                    Console.WriteLine(s);
                    Console.SetCursorPosition((Console.WindowWidth - s2.Length) / 2, Console.CursorTop);
                    Console.WriteLine(s2);
                    Console.ReadKey();
                    gameLive = false;
                    break;
                }

                if (y <= 5)
                {
                    string s = "Game Over!! You hit an obstacle, you are at Rank " + rank;
                    string s2 = "Press any key to end the game";
                    y = consoleHeightLimit;
                    Console.Clear();
                    Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
                    Console.WriteLine(s);
                    Console.SetCursorPosition((Console.WindowWidth - s2.Length) / 2, Console.CursorTop);
                    Console.WriteLine(s2);
                    Console.ReadKey();
                    gameLive = false;
                    break;
                }


                ++countSteps;// Increment the steps each time the snake moves
                //change the food locaiton when snake eats it or at a specific interval
                if ((x == fx && y == fy) || countSteps > 200)
                {
                    //add score
                    score += 10;
                    /*erase the current food*/
                    Console.SetCursorPosition(fx, fy);
                    Console.Write(' ');
                    /*set a new random position for food*/
                    fx = rand.Next(1, 78);
                    fy = rand.Next(6, 23);
                    countSteps = 0; //reset countSteps
                }

                // write the character in the new position
                Console.SetCursorPosition(x, y);
                Console.Write(ch);

                // write the food in the new random position
                Console.SetCursorPosition(fx, fy);
                Console.Write(food);

                //new rand obj
                for (int i = 0; i < obstacleNum; ++i) 
                    if (x == object_x[i] && y == object_y[i])
                    {
                        string s = "Game Over!! You hit an obstacle, you are at Rank " + rank;
                        string s2 = "Press any key to end the game";
                        Console.Clear();
                        Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
                        Console.WriteLine(s);
                        Console.SetCursorPosition((Console.WindowWidth - s2.Length) / 2, Console.CursorTop);
                        Console.WriteLine(s2);
                        Console.ReadKey();
                        gameLive = false;
                        break;
                    }

                //random obj
                for (int i = 0; i < obstacleNum; ++i)
                {
                    Console.SetCursorPosition(object_x[i], object_y[i]);
                    Console.Write(obj);
                }

                //upper horizontal wall
                for (int i = 0; i < hori_wallNum; ++i)
                {
                    Console.SetCursorPosition(object_upper_x[i], object_upper_y[i]);
                    Console.Write(hori_wall);
                }

                //lower horizontal wall
                for (int i = 0; i < hori_wallNum; ++i)
                {
                    Console.SetCursorPosition(object_lower_x[i], object_lower_y[i]);
                    Console.Write(hori_wall);
                }

                //left side wall
                for (int i = 6; i < verti_wallNum; ++i)
                {
                    Console.SetCursorPosition(object_left_x[i], object_left_y[i]);
                    Console.Write(verti_wall);
                }

                //right side wall
                for (int i = 6; i < verti_wallNum; ++i)
                {
                    Console.SetCursorPosition(object_right_x[i], object_right_y[i]);
                    Console.Write(verti_wall);
                }

                // pause to allow eyeballs to keep up
                if (dy!=0)
                    System.Threading.Thread.Sleep(delayInMillisecs+20);
                else
                    System.Threading.Thread.Sleep(delayInMillisecs);

            } while (gameLive);
            
        }

    }
    
}
