using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Snake
{
    class Snake
    {
        const int Size = 10;
        int[,] Arr = new int[Size, Size];
        int[] SnakePos = new int[2] {Size / 2, Size / 2}; // i, j
        string Direction = "up";
        int Score = 0;
        int[] FoodDirection = new int[2];
        int Interval = 500;

        public void Main()
        {
            Filler();
            Walls();
            GenerateFood();
            do
            {
                Console.Clear();
                Step(Direction);
                ScoreCheck();
                Thread KeyReader = new Thread(InputKey);
                KeyReader.Start();
                
                Output();
                Thread.Sleep(Interval);
            } while (true);
        }

        private void GenerateFood()
        {
            Random Rand = new Random();
            int x = Rand.Next(0, Size);
            int y = Rand.Next(0, Size);
            if (Arr[x, y] == 0)
                Arr[x, y] = 1;
            else GenerateFood();
        }

        private void ScoreCheck()
        {
            int MinInterval = 50;
            if (Arr[SnakePos[0], SnakePos[1]] == 1)
            {
                Score++;
                Arr[SnakePos[0], SnakePos[1]] = 0;
                if (Interval > MinInterval)
                {
                    Interval -= MinInterval;
                }
                GenerateFood();
            }
            if (Arr[SnakePos[0], SnakePos[1]] == 2)
            {
                Console.WriteLine("DEAD INSIDE");
                Interval = 500;
                Score = 0;
                Console.Read();
                Main();
            }
        }

        private void Step(string Key)
        {
            switch (Key)
            {
                case "up":
                    if (SnakePos[0] > 0)
                    {
                        SnakePos[0]--;
                        Direction = "up";
                    }
                    else if (SnakePos[0] == 0)
                        SnakePos[0] = Size - 1;
                    break;
                case "down":
                    if (SnakePos[0] < Size - 1)
                    {
                        SnakePos[0]++;
                        Direction = "down";
                    }
                    else if (SnakePos[0] == Size - 1)
                        SnakePos[0] = 0;
                    break;
                case "left":
                    if (SnakePos[1] > 0)
                    {
                        SnakePos[1]--;
                        Direction = "left";
                    }
                    else if (SnakePos[1] == 0)
                        SnakePos[1] = Size - 1;
                    break;
                case "right":
                    if (SnakePos[1] < Size - 1)
                    {

                        SnakePos[1]++;
                        Direction = "right";
                    }
                    else if (SnakePos[1] == Size - 1)
                        SnakePos[1] = 0;
                    break;
            }
        }

        private void InputKey()
        {
            ConsoleKeyInfo InputKey;
                InputKey = Console.ReadKey();

                switch (InputKey.Key)
                {
                    case ConsoleKey.UpArrow:
                        Direction = "up";
                        break;
                    case ConsoleKey.DownArrow:
                        Direction = "down";
                        break;
                    case ConsoleKey.LeftArrow:
                        Direction = "left";
                        break;
                    case ConsoleKey.RightArrow:
                        Direction = "right";
                        break;
                }
        }

        private void Filler()
        {
            for (int i = 0; i < Size; i++)
                for (int j = 0; j < Size; j++)
                    Arr[i, j] = 0;
        }

        private void Walls()
        {
            Random Rand = new Random();
            int x, y;
            for (int i = 0; i < Rand.Next(3, Size); i++)
            {
                x = Rand.Next(0, Size);
                y = Rand.Next(0, Size);
                if (SnakePos[0] != x && SnakePos[1] != y)
                    Arr[x, y] = 2;
            }
        }

        private void Output()
        {
            int[] Info = new int[3] {Size, Score, Interval};
            string[] Inf = new string[3] { "Size: {0}x{0}", "Score: {0}", "Refresh interval: {0}ms" };
            Console.BackgroundColor = ConsoleColor.White;

            for (int i = 0; i <= Size; i++) // Верхняя рамка
            {
                Console.Write("==");
            }
            Console.WriteLine();

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if ((j == 0)) // Левая рамка
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write("|");
                    }

                    switch (Arr[i, j])
                    {
                        case 0: // Пустота
                            Console.BackgroundColor = ConsoleColor.Black;
                            break;
                        case 1: // Еда
                            Console.BackgroundColor = ConsoleColor.Red;
                            break;
                        case 2: // Стена - нема
                            Console.BackgroundColor = ConsoleColor.White;
                            break;
                    }


                    if (i == SnakePos[0] && j == SnakePos[1]) // Змея
                        Console.BackgroundColor = ConsoleColor.Green;

                    Console.Write("  ");
                }
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write("|"); // Правая рамка
                Console.BackgroundColor = ConsoleColor.Black;
                if (i < Info.Length)
                Console.Write("\t" + Inf[i], Info[i]);
                Console.WriteLine(" ");
            }

            Console.BackgroundColor = ConsoleColor.White;
            for (int i = 0; i <= Size; i++) // Нижняя рамка
            {
                Console.Write("==");
            }
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
