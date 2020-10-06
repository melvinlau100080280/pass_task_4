using System;

namespace SnakeGame
{
    class Program
    {
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
            fy = rand.Next(2, 24);

            // location info & display
            int x = 0, y = 2; // y is 2 to allow the top row for directions & space
            int dx = 1, dy = 0;
            int consoleWidthLimit = 79;
            int consoleHeightLimit = 24;

            //scoreboard variables
            int score = 0;
            string rank = " ";

            // clear to color
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Clear();

            // delay to slow down the character movement so you can see it
            int delayInMillisecs = 50;

            // whether to keep trails
            bool trail = false;

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
                if (score >= 20) { rank = "D"; }
                if (score >= 40) { rank = "C"; }
                if (score >= 60) { rank = "B"; }
                if (score >= 80) { rank = "A"; }
                if (score >= 99) { rank = "S"; }
                Console.WriteLine("Achievements : Rank " + rank); //scoreboard rank
                Console.WriteLine("====================="); //scoreboard design
                Console.SetCursorPosition(x, y);
                Console.ForegroundColor = cc;

                // see if a key has been pressed
                if (Console.KeyAvailable)
                {
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
                    if (score == 100)
                    {
                        Console.Clear();
                        Console.WriteLine("Congratulation you won the game!! You rank is at Rank " + rank); //win score
                        Console.WriteLine("Press any key to end the game"); //win score
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
                if (x > consoleWidthLimit)
                    x = 0;
                if (x < 0)
                    x = consoleWidthLimit;

                y += dy;
                if (y > consoleHeightLimit)
                    y = 5; // 2 due to top spaces used for directions, 3 more for scoreboard and achievements
                if (y < 5)
                    y = consoleHeightLimit;

                ++countSteps;// Increment the steps each time the snake moves
                //change the food locaiton when snake eats it or at a specific interval
                if ((x== fx && y == fy)||countSteps > 200)
                {
                    /*erase the current food*/
                    Console.SetCursorPosition(fx, fy);
                    Console.Write(' ');
                    /*set a new random position for food*/
                    fx = rand.Next(0, 79);
                    fy = rand.Next(2, 24);
                    countSteps = 0; //reset countSteps
                }

                // write the character in the new position
                Console.SetCursorPosition(x, y);
                Console.Write(ch);

                // write the food in the new random position
                Console.SetCursorPosition(fx, fy);
                Console.Write(food);

                // pause to allow eyeballs to keep up
                System.Threading.Thread.Sleep(delayInMillisecs);

            } while (gameLive);
        }
    }
    
}
