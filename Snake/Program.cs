using Snake;
using System.Drawing;
Console.OutputEncoding = System.Text.Encoding.UTF8;// this is used for the use of ●

bool gameover=false;


Random random = new Random();

coordinate gridDimensions = new coordinate(60, 25);
coordinate snakePosition = new coordinate(25, 12);


Direction movementDirection = Direction.Down;

coordinate appleposition = new coordinate(random.Next(1, gridDimensions.X - 1), random.Next(1, gridDimensions.Y - 1));

int frameDelayMilli = 100;



List<coordinate> snakeposhistory = new List<coordinate>();
int tailLength = 1;


int score = 0;





static void ShowIntro()
{
    Console.Clear();
    Console.WriteLine("===== SNAKE GAME =====");
    Console.WriteLine();
    Console.WriteLine("Press P to Play");
    Console.WriteLine("Press E to Exit");

    ConsoleKey key = Console.ReadKey(true).Key;

    if (key == ConsoleKey.E)
        Environment.Exit(0);
}

ShowIntro();

static void ShowLoading()
{
    Console.Clear();
    Console.Write("Loading");

    for (int i = 0; i < 5; i++)
    {
        Thread.Sleep(400);
        Console.Write(".");
    }

    Thread.Sleep(700);
}

ShowLoading();




while (true)
{
    Console.Clear();

    Console.WriteLine("Score" + score);

    snakePosition.ApplyMovementDirection(movementDirection);
    for (int y = 0; y < gridDimensions.Y; y++)
    {
        for (int x = 0; x < gridDimensions.X; x++)
        {

            coordinate currentCoordinate = new coordinate(x, y);


            //Print the tail of the snake
            if (snakePosition.Equals(currentCoordinate) || (snakeposhistory.Contains(currentCoordinate)))
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("■");
            }

            else if (appleposition.Equals(currentCoordinate))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("●");

            }


            else if (x == 0 || y == 0 || x == gridDimensions.X - 1 || y == gridDimensions.Y - 1)
            {


                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("■");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(" ");
            }
        }
        Console.WriteLine();

    }

  

    if (snakePosition.Equals(appleposition))
    {

        tailLength= tailLength+1;
        appleposition = new coordinate(random.Next(1, gridDimensions.X - 1), random.Next(1, gridDimensions.Y - 1));
        score++;
    }
    else if (snakePosition.X == 0 || snakePosition.Y == 0 || snakePosition.X == gridDimensions.X - 1 || snakePosition.Y == gridDimensions.Y - 1 || snakeposhistory.Contains(snakePosition))
    {
        //score = 0;
        //tailLength = 1;
        //snakePosition = new coordinate(25, 12);
        //snakeposhistory.Clear();
        //movementDirection = Direction.Down;
        //continue;

        gameover = true;

    }

    snakeposhistory.Add(new coordinate(snakePosition.X, snakePosition.Y));
    if (snakeposhistory.Count > tailLength)
    {
        snakeposhistory.RemoveAt(0);
    }



    //////////////////////////////////////////////////////////////
    if (gameover)
    {
        Console.Clear();
        Console.WriteLine("===== GAME OVER =====");
        Console.WriteLine("Final Score: " + score);
        Console.WriteLine();
        Console.WriteLine("Press R to Restart");
        Console.WriteLine("Press E to Exit");

        ConsoleKey key = Console.ReadKey(true).Key;

        if (key == ConsoleKey.E)
            Environment.Exit(0);

        // Reset game values
        score = 0;
        tailLength = 1;
        snakePosition = new coordinate(25, 12);
        snakeposhistory.Clear();
        movementDirection = Direction.Down;
        gameover = false;

        continue;
    }

    /////////////////////////////////////////////////////////////


    DateTime time = DateTime.Now;

    while ((DateTime.Now - time).TotalMilliseconds < frameDelayMilli)
    {
        if (Console.KeyAvailable)
        {
            ConsoleKey key = Console.ReadKey().Key;

            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    movementDirection = Direction.Left;
                    break;
                case ConsoleKey.RightArrow:
                    movementDirection = Direction.Right;
                    break;
                case ConsoleKey.UpArrow:
                    movementDirection = Direction.Up;
                    break;
                case ConsoleKey.DownArrow:
                    movementDirection = Direction.Down;
                    break;
            }
        }
    }
}