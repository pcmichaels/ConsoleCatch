using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsoleGame2
{

    class Program
    {
        private static int _left = 0;
        private static int _top = Console.WindowHeight - 1;        

        private struct Position
        {
            public int left;
            public int top;
        }

        private static List<Position> _points = new List<Position>();

        private static DateTime nextUpdate = DateTime.MinValue;
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            DrawScreen();
            while (true)
            {
                bool autoUpdate = DateTime.Now >= nextUpdate;
                if (AcceptInput() || autoUpdate)
                {
                    DrawScreen();

                    if (autoUpdate)
                    {
                        AddStar();
                        MoveStars();

                        nextUpdate = DateTime.Now.AddMilliseconds(500);
                    }                    
                }
            }
        }

        private static void MoveStars()
        {
            for (int i = 0; i <= _points.Count() - 1; i++)
            {
                _points[i] = new Position() { left = _points[i].left, top = _points[i].top + 1 };
            }
        }

        private static void AddStar()
        {
            Random rnd = new Random();
            _points.Add(new Position() { left = rnd.Next(Console.WindowWidth), top = 0 });
        }

        private static void DrawScreen()
        {
            Console.Clear();
            Console.SetCursorPosition(_left, _top);
            Console.Write(@"\_/");

            foreach (var point in _points)
            {
                Console.SetCursorPosition(point.left, point.top);
                Console.Write('*');
            }

            Console.SetCursorPosition(0, 0);            
        }

        private static bool AcceptInput()
        {
            if (!Console.KeyAvailable)
                return false;

            ConsoleKeyInfo key = Console.ReadKey();

            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    _left--;
                    break;
                case ConsoleKey.RightArrow:
                    _left++;
                    break;

            }

            return true;
        }

    }
}
