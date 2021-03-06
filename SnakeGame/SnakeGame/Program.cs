﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

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
            //When snake collide with obstacle
            void collide()
            {
                WMPLib.WindowsMediaPlayer crash = new WMPLib.WindowsMediaPlayer();
                crash.URL = @"crash.m4a";
                crash.controls.play();
            }
            //When snake eat food
            void eat()
            {
                WMPLib.WindowsMediaPlayer eat = new WMPLib.WindowsMediaPlayer();
                eat.URL = @"eat.m4a";
                eat.controls.play();
            }
            //BackgroundMusic
            WMPLib.WindowsMediaPlayer music = new WMPLib.WindowsMediaPlayer();
            music.URL = @"music.wav";
            music.controls.play();
            int choice;
            string name;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" _______________________________________________________________________ ");
            Console.WriteLine("|                                                                       |");
            Console.WriteLine("|   #       #  ######  #        ########  #######  ##     ##  #######   |");
            Console.WriteLine("|   #       #  #       #        #         #     #  #  # #  #  #         |");
            Console.WriteLine("|   #   #   #  ######  #        #         #     #  #   #   #  #######   |");
            Console.WriteLine("|   #  # #  #  #       #        #         #     #  #       #  #         |");
            Console.WriteLine("|   ##     ##  ######  #######  ########  #######  #       #  #######   |");
            Console.WriteLine("|                                                                       |");
            Console.WriteLine("|_______________________________________________________________________|");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Enter your name: ");
            name = Console.ReadLine();
            do
            {
                Console.Clear();
                //Main Menu
                Console.WriteLine("Main Menu");
                Console.WriteLine("1. Start Game");
                Console.WriteLine("2. View Scoreboard");
                Console.WriteLine("3. Quit");
                Console.WriteLine("4. Help");
                Console.Write("Enter: ");
                choice = int.Parse(Console.ReadLine());
                if (choice == 1)
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

                    // count steps for food each time the snake moves
                    int countStepsFood = 0;
                    // count steps for bad food each time the snake moves
                    int countStepsBadFood = 0;
                    // count steps for bounty food each time the snake moves
                    int countStepsBountyFood = 0;

                    // food interval
                    int foodInterval = 100;

                    // holds whatever key is pressed
                    ConsoleKeyInfo consoleKey;

                    // clear to color
                    Console.BackgroundColor = ConsoleColor.Black;
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
                    string obj = "|";
                    int obstacleNum = 3;
                    int[] object_x = new int[3];
                    int[] object_y = new int[3];

                    int consoleWidthLimit = 79;
                    int consoleHeightLimit = 24;

                    for (int i = 0; i < obstacleNum; ++i)
                    {
                        object_x[i] = rando.Next(1, 78);
                        object_y[i] = rando.Next(5, 23);
                    }

                    // display snake on the console during the game
                    Queue<Position> snakeElements = new Queue<Position>();
                    for (int i = 0; i < 3; i++)//start with three character
                    {
                        snakeElements.Enqueue(new Position(6, i));
                    }

                    //normal food generation
                    Random rand = new Random();
                    Position food;
                    food = new Position(rand.Next(7, 23), rand.Next(1, 78));
                    Console.SetCursorPosition(food.col, food.row);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("@");

                    //bounty food generation
                    Random rand2 = new Random();
                    Position bountyfood;
                    bountyfood = new Position(rand2.Next(7, 23), rand2.Next(1, 78));
                    Console.SetCursorPosition(bountyfood.col, bountyfood.row);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("B");

                    //bad food generation 
                    Random rand3 = new Random();
                    Position badfood;
                    badfood = new Position(rand3.Next(7, 23), rand3.Next(1, 78));
                    Console.SetCursorPosition(badfood.col, badfood.row);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("X");

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
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.SetCursorPosition(0, 0);
                        Console.WriteLine("Arrows move up/down/right/left. Press 'esc' quit.");
                        Console.WriteLine("====================="); //scoreboard design
                        Console.WriteLine(String.Format("{0,-10} {1,6}", "Current Score:", score)); //updated scoreboard display
                        if (score < 20) { rank = '-'; }
                        if (score >= 20) { rank = 'D'; }
                        if (score >= 40) { rank = 'C'; }
                        if (score >= 60) { rank = 'B'; }
                        if (score >= 80) { rank = 'A'; }
                        if (score >= 100) { rank = 'S'; }
                        Console.WriteLine("Achievements : Rank " + rank); //scoreboard rank
                        Console.WriteLine("====================="); //scoreboard design
                        Console.ForegroundColor = cc;

                        // see if a key has been pressed
                        if (Console.KeyAvailable)
                        {
                            // get key and use it to set options

                            consoleKey = Console.ReadKey(true);
                            if (consoleKey.Key == ConsoleKey.LeftArrow)//Left
                            {
                                if (direction != right) direction = left;
                                
                            }
                            if (consoleKey.Key == ConsoleKey.DownArrow)//Down
                            {
                                if (direction != up) direction = down;
                                
                            }
                            if (consoleKey.Key == ConsoleKey.UpArrow)//Up
                            {
                                if (direction != down) direction = up;
                                
                            }
                            if (consoleKey.Key == ConsoleKey.RightArrow)//Right
                            {
                                if (direction != left) direction = right;
                                
                            }
                            if (consoleKey.Key == ConsoleKey.Escape)
                            {
                                gameLive = false;
                            }
                        }

                        //change color when difficulty change
                        if (score < 20) { Console.ForegroundColor = ConsoleColor.Gray; }
                        if (score >= 20) { Console.ForegroundColor = ConsoleColor.Green; }
                        if (score >= 40) { Console.ForegroundColor = ConsoleColor.Cyan; }
                        if (score >= 60) { Console.ForegroundColor = ConsoleColor.Yellow; }
                        if (score >= 80) { Console.ForegroundColor = ConsoleColor.Red; }
                        if (score >= 100) {; Console.ForegroundColor = ConsoleColor.Gray; }

                        //set winning conditions
                        if (score >= 100)
                        {
                            //Set the score to 50 so it wont exceed to 50
                            score = 50;
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

                        snakeElements.Enqueue(snakeNewHead);
                        Console.SetCursorPosition(snakeNewHead.col, snakeNewHead.row);
                        Console.Write("#");

                        //new rand obj
                        for (int i = 0; i < obstacleNum; ++i)
                        {
                            if (snakeNewHead.col == object_x[i] && snakeNewHead.row == object_y[i])
                            {
                                Console.ForegroundColor = ConsoleColor.Gray;
                                collide();
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
                        if ((snakeNewHead.col == food.col && snakeNewHead.row == food.row) || countStepsFood > foodInterval)
                        {
                            //only increase score when snake collide with food
                            if ((snakeNewHead.col == food.col) && snakeNewHead.row == food.row)
                            {
                                eat();
                                score += 5;
                            }
                            
                            /*erase the current food*/
                            Console.SetCursorPosition(food.col, food.row);
                            Console.Write(' ');
                            /*set a new random position for food*/
                            food = new Position(rand.Next(6, 23), rand.Next(1, 78));
                            countStepsFood = 0;
                        }

                        //When snake collide with bounty food
                        else if ((snakeNewHead.col == bountyfood.col && snakeNewHead.row == bountyfood.row) || countStepsBountyFood > foodInterval)
                        {
                            //only increase score when snake collide with food
                            if ((snakeNewHead.col == bountyfood.col) && snakeNewHead.row == bountyfood.row)
                            {
                                eat();
                                score += 10;
                            }
                            /*erase the current food*/
                            Console.SetCursorPosition(bountyfood.col, bountyfood.row);
                            Console.Write(' ');
                            /*set a new random position for food*/
                            bountyfood = new Position(rand.Next(6, 23), rand.Next(1, 78));
                            countStepsBountyFood = 0;
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

                        //When snake collide with bad food
                        if ((snakeNewHead.col == badfood.col && snakeNewHead.row == badfood.row) || countStepsBadFood > foodInterval) { 

                            //only increase score when snake collide with food
                            if ((snakeNewHead.col == badfood.col) && snakeNewHead.row == badfood.row)
                            {
                                eat();
                                score -= 15;
                            }
                            /*erase the current food*/
                            Console.SetCursorPosition(badfood.col, badfood.row);
                            Console.Write(' ');
                            /*set a new random position for food*/
                            badfood = new Position(rand.Next(6, 23), rand.Next(1, 78));
                            countStepsBadFood = 0;

                            if (score < 0) { score = 0; }
                        }

                        // Increment the steps for each food each time the snake moves
                        ++countStepsFood;
                        ++countStepsBountyFood;
                        ++countStepsBadFood;
                        Console.SetCursorPosition(food.col, food.row);
                        Console.Write("@");
                        Console.SetCursorPosition(bountyfood.col, bountyfood.row);
                        Console.Write("B");
                        Console.SetCursorPosition(badfood.col, badfood.row);
                        Console.Write("X");

                        if (snakeNewHead.col >= consoleWidthLimit)
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                            collide();
                            string s = "Game Over!! You hit a wall, you are at Rank " + rank;
                            string s2 = "Press any key to back to main menu";
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
                            Console.ForegroundColor = ConsoleColor.Gray;
                            collide();
                            string s = "Game Over!! You hit a wall, you are at Rank " + rank;
                            string s2 = "Press any key to back to main menu";
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
                            Console.ForegroundColor = ConsoleColor.Gray;
                            collide();
                            string s = "Game Over!! You hit a wall, you are at Rank " + rank;
                            string s2 = "Press any key to back to main menu";
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
                            Console.ForegroundColor = ConsoleColor.Gray;
                            collide();
                            string s = "Game Over!! You hit a wall, you are at Rank " + rank;
                            string s2 = "Press any key to back to main menu";
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

                        // random obstacles
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

                        //Reset to default values
                        delayInMillisecs = 50;
                        foodInterval = 100;
                        if (score != 0)
                        {
                            //Ramp up speed and decrease food interval scaling with score.
                            for (int i = 0; i < score; i += 5)
                            {
                                //Ramp up speed by score
                                delayInMillisecs -= 3;
                                //Decrease food interval 
                                foodInterval -= 2;
                            }
                        }
                     

                        // pause to allow eyeballs to keep up
                        if (snakeHead.col != 0)
                            System.Threading.Thread.Sleep(delayInMillisecs + 30);

                        else
                            System.Threading.Thread.Sleep(delayInMillisecs);

                    } while (gameLive);
                    var path = @"Scoreboard.txt";
                    StreamWriter log;
                    if (!File.Exists(path))
                    {
                        log = new StreamWriter(path);
                    }
                    else
                    {
                        log = File.AppendText(path);
                    }
                    // Write to the file:
                    log.Write("Name: ");
                    log.Write(name);
                    log.Write(" | Score: ");
                    log.WriteLine(score);
                    // Close the stream:
                    log.Close();
                }
                else if(choice == 2)
                {
                    Console.Clear();
                    var path = @"Scoreboard.txt";
                    string[] lines = File.ReadAllLines(path);
                    Console.WriteLine("==========");
                    Console.WriteLine("Scoreboard");
                    Console.WriteLine("==========");
                    foreach (string line in lines)
                        Console.WriteLine(line);

                    Console.WriteLine("--------------------------------");
                    Console.WriteLine("Press any key to quit Scoreboard.");
                    Console.ReadKey();
                }
                else if (choice == 3)
                {
                    Console.WriteLine("Thanks for playing. Have a Nice Day.");
                }
                else if (choice == 4)
                {
                    Console.Clear();
                    Console.WriteLine("=====");
                    Console.WriteLine("INTRO");
                    Console.WriteLine("=====");
                    Console.WriteLine("-> Welcome to Snake Game developed by WMS Team. This is a simple snake game developed for assignment purposes.");
                    Console.WriteLine("============");
                    Console.WriteLine("INSTRUCTIONS");
                    Console.WriteLine("============");
                    Console.WriteLine("-> Press W to move up | Press A to move left | Press S to move down | Press D to move right.");
                    Console.WriteLine("-> There will be obstacles and walls, hit anyone of them then you'll lose.");
                    Console.WriteLine("-> The snake moves faster and the food spawn intervals are quicker, scaled by the score you achieved.");
                    Console.WriteLine("==========");
                    Console.WriteLine("HOW TO WIN");
                    Console.WriteLine("==========");
                    Console.WriteLine("-> There are 3 types of food: Normal Food marked as @ | Bounty Food marked as B | Bad Food marked as X.");
                    Console.WriteLine("-> Different food grants different score: @ provides 5 points | B provides 10 points | X deducts 5 points.");
                    Console.WriteLine("-> You can keep track of your rank and score on the scoreboard placed above the game.");
                    Console.WriteLine("-> Reach 50 points, Voila!! You Win!!");
                    Console.WriteLine("-------------------------------------");
                    Console.WriteLine("Press any key to quit Help.");
                    Console.ReadKey();
                    
                }
            } while (choice != 3);

        }
    }
}
