using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;

namespace Snake
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
    class Program
    {
        static void Main(string[] args)
        {
            // start game
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            int right = 0;
            int left = 1;
            int down = 2;
            int up = 3;
            // whether to keep trails
            bool trail = false;
            bool gameLive = true;
            //scoreboard variables
            int score = 0;
            char rank = '-';

            // delay to slow down the character movement so you can see it
            int delayInMillisecs = 50;

            //initialise direction
            int direction = right;

            // count steps each time the snake moves
            int countSteps = 0;

            // holds whatever key is pressed
            ConsoleKeyInfo consoleKey;

            // clear to color
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Clear();

            //Snake direction
            Position[] directions = new Position[]
            {
                new Position(0, 1), // right
                new Position(0, -1), // left
                new Position(1, 0), // down
                new Position(-1, 0), // up
            };

            Random rando = new Random();
            string obj = "||";
            int obstacleNum = 3;
            int[] object_x = new int[3];
            int[] object_y = new int[3];

            int consoleWidthLimit = 79;
            int consoleHeightLimit = 24;

            for (int i = 0; i < obstacleNum; ++i)
            {
                object_x[i] = rando.Next(0, 79);
                object_y[i] = rando.Next(5, 24);
            }

            // display snake on the console during the game
            Queue<Position> snakeElements = new Queue<Position>();
            for (int i = 0; i < 3; i++)//start with three character
            {
                snakeElements.Enqueue(new Position(6, i));
            }

            Random rand = new Random();
            Position food;
            food = new Position(rand.Next(5, 24), rand.Next(0, 79));
            Console.SetCursorPosition(food.col, food.row);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("@");

            foreach (Position position in snakeElements)
            {
                Console.SetCursorPosition(position.col, position.row);
                Console.Write("*");
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
                if (score >= 10) { rank = 'D'; }
                if (score >= 20) { rank = 'C'; }
                if (score >= 30) { rank = 'B'; }
                if (score >= 40) { rank = 'A'; }
                if (score == 50) { rank = 'S'; }
                Console.WriteLine("Achievements : Rank " + rank); //scoreboard rank
                Console.WriteLine("====================="); //scoreboard design
                Console.ForegroundColor = cc;
                // see if a key has been pressed
                if (Console.KeyAvailable)
                {
                    // get key and use it to set options

                    consoleKey = Console.ReadKey(true);
<<<<<<< HEAD
                    if (consoleKey.Key == ConsoleKey.LeftArrow)//Left
                    {
                        if (direction != right) direction = left;
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    if (consoleKey.Key == ConsoleKey.DownArrow)//Down
                    {
                        if (direction != up) direction = down;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    if (consoleKey.Key == ConsoleKey.UpArrow)//Up
                    {
                        if (direction != down) direction = up;
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    if (consoleKey.Key == ConsoleKey.RightArrow)//Right
                    {
                        if (direction != left) direction = right;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    if (consoleKey.Key == ConsoleKey.Escape)
                    {
                        gameLive = false;
                    }
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
                //Tail of snake
                Position snakeHead = snakeElements.Last();

                //New direction of snake
                Position nextDirection = directions[direction];
                Position snakeNewHead = new Position(snakeHead.row + nextDirection.row, snakeHead.col + nextDirection.col);

                Console.SetCursorPosition(snakeHead.col, snakeHead.row);
                Console.Write("*");
=======

                    // prevent users for moving against their direction
                    if (!(consoleKey.Key == ConsoleKey.UpArrow && dy == 1) &&
                       !(consoleKey.Key == ConsoleKey.DownArrow && dy == -1) &&
                      !(consoleKey.Key == ConsoleKey.LeftArrow && dx == 1) &&
                       !(consoleKey.Key == ConsoleKey.RightArrow && dx == -1))
                    {
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
                    }
>>>>>>> d117953b972861b004f75e6a18eb5e5865ef187e

                snakeElements.Enqueue(snakeNewHead);
                Console.SetCursorPosition(snakeNewHead.col, snakeNewHead.row);
                Console.Write("*");

                //new rand obj
                for (int i = 0; i < obstacleNum; ++i)
                {
                    if (snakeNewHead.col == object_x[i] && snakeNewHead.row == object_y[i])
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
                }
                //When snake collide with food
                if ((snakeNewHead.col == food.col && snakeNewHead.row == food.row) || countSteps > 100)
                {
                    //only increase score when snake collide with food
                    if ((snakeNewHead.col == food.col) && snakeNewHead.row == food.row)
                    {
                        score += 5;
                    }
                    /*erase the current food*/
                    Console.SetCursorPosition(food.col, food.row);
                    Console.Write(' ');
                    /*set a new random position for food*/
                    food = new Position(rand.Next(5, 24), rand.Next(0, 79));
                    countSteps = 0;
                }
                else
                {
                    // moving...
                    Position last = snakeElements.Dequeue();
                    // find the current position in the console grid & erase the character there if don't want to see the trail
                    Console.SetCursorPosition(last.col, last.row);
                    if (trail == false)
                        Console.Write(' ');
                }
                ++countSteps;// Increment the steps each time the snake moves
                Console.SetCursorPosition(food.col, food.row);
                Console.Write("@");


                if (snakeNewHead.col >= consoleWidthLimit)
                {
                    string s = "Game Over!! You hit an obstacle, you are at Rank " + rank;
                    string s2 = "Press any key to end the game";
                    snakeNewHead.col = 0;
                    Console.Clear();
                    Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
                    Console.WriteLine(s);
                    Console.SetCursorPosition((Console.WindowWidth - s2.Length) / 2, Console.CursorTop);
                    Console.WriteLine(s2);
                    Console.ReadKey();
                    gameLive = false;
                    break;
                }

                if (snakeNewHead.col <= 0)
                {
                    string s = "Game Over!! You hit an obstacle, you are at Rank " + rank;
                    string s2 = "Press any key to end the game";
                    snakeNewHead.col = consoleWidthLimit;
                    Console.Clear();
                    Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
                    Console.WriteLine(s);
                    Console.SetCursorPosition((Console.WindowWidth - s2.Length) / 2, Console.CursorTop);
                    Console.WriteLine(s2);
                    Console.ReadKey();
                    gameLive = false;
                    break;
                }

                if (snakeNewHead.row >= consoleHeightLimit)
                {
                    string s = "Game Over!! You hit an obstacle, you are at Rank " + rank;
                    string s2 = "Press any key to end the game";
                    snakeNewHead.row = 5; // 2 due to top spaces used for directions, 3 more for scoreboard and achievements
                    Console.Clear();
                    Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
                    Console.WriteLine(s);
                    Console.SetCursorPosition((Console.WindowWidth - s2.Length) / 2, Console.CursorTop);
                    Console.WriteLine(s2);
                    Console.ReadKey();
                    gameLive = false;
                    break;
                }

                if (snakeNewHead.row <= 5)
                {
                    string s = "Game Over!! You hit an obstacle, you are at Rank " + rank;
                    string s2 = "Press any key to end the game";
                    snakeNewHead.row = consoleHeightLimit;
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
<<<<<<< HEAD

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
<<<<<<< HEAD
                if (snakeHead.col != 0)
                    System.Threading.Thread.Sleep(delayInMillisecs + 30);
=======
                if (dy!=0)
                    System.Threading.Thread.Sleep(delayInMillisecs+20);
=======

                //pause a little bit longer if snake is moving vertically
                if(dy!=0)
                    System.Threading.Thread.Sleep(delayInMillisecs+30);
>>>>>>> prevent-snake-move-backwards
>>>>>>> d117953b972861b004f75e6a18eb5e5865ef187e
                else
                    System.Threading.Thread.Sleep(delayInMillisecs);
            } while (gameLive);
        }
    }
}
