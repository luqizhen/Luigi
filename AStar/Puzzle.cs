using System;
using System.Collections.Generic;
using System.Drawing;

namespace luigi.algorithms
{
    public class Puzzle
    {
        public int Width { get; }
        public int Height { get; }
        public Point Blank { get; private set; }

        private int[,] _container;

        public static Puzzle Default(int width, int height)
        {
            Puzzle puzzle = new Puzzle(width, height);
            int value = 1;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    puzzle[x, y] = value++;
                }
            }
            puzzle.Blank = new Point(width - 1, height - 1);
            return puzzle;
        }

        public static Puzzle Default(int size)
        {
            return Puzzle.Default(size, size);
        }

        public static Puzzle Random(int seed, int width, int height, int complexity = 100)
        {
            seed = seed * seed * DateTime.Now.Minute + seed * DateTime.Now.Second + DateTime.Now.Millisecond;
            Random random = new Random(seed);
            Puzzle puzzle = Puzzle.Default(width, height);
            int times = random.Next(complexity);
            for (int i = 0; i < times; i++)
            {
                var rs = true;
                switch (random.Next(4))
                {
                    case 0:
                        rs = puzzle.MoveLeft();
                        break;
                    case 1:
                        rs = puzzle.MoveRight();
                        break;
                    case 2:
                        rs = puzzle.MoveUp();
                        break;
                    case 3:
                        rs = puzzle.MoveDown();
                        break;
                }
                if (!rs)
                {
                    i--;
                }
            }
            return puzzle;
        }

        public static Puzzle Random(int seed, int size, int complexity = 100)
        {
            return Puzzle.Random(seed, size, size, complexity);
        }

        public static Dictionary<Puzzle, double> NextStepsRule(Puzzle start)
        {
            int width = start.Width;
            int height = start.Height;
            Point blank = start.Blank;

            Dictionary<Puzzle, double> nextSteps = new Dictionary<Puzzle, double>();
            Puzzle next;

            next = start.Copy();
            if (next.MoveLeft())
            {
                nextSteps.Add(next, 1);
            }

            next = start.Copy();
            if (next.MoveRight())
            {
                nextSteps.Add(next, 1);
            }

            next = start.Copy();
            if (next.MoveUp())
            {
                nextSteps.Add(next, 1);
            }

            next = start.Copy();
            if (next.MoveDown())
            {
                nextSteps.Add(next, 1);
            }

            return nextSteps;
        }

        public Puzzle(int width, int height)
        {
            Width = width;
            Height = height;
            _container = new int[Width, Height];
        }

        public int this[int x, int y]
        {
            get
            {
                return _container[x, y];
            }
            private set
            {
                _container[x, y] = value;
            }
        }

        public bool MoveLeft()
        {
            int x, y, temp;
            x = Blank.X-1;
            y = Blank.Y;
            if (x < 0)
            {
                return false;
            }
            temp = this[x, y];
            this[x, y] = this[Blank.X, Blank.Y];
            this[Blank.X, Blank.Y] = temp;
            Blank = new Point(x, y);
            return true;
        }

        public bool MoveRight()
        {
            int x, y, temp;
            x = Blank.X + 1;
            y = Blank.Y;
            if (x > Width - 1)
            {
                return false;
            }
            temp = this[x, y];
            this[x, y] = this[Blank.X, Blank.Y];
            this[Blank.X, Blank.Y] = temp;
            Blank = new Point(x, y);
            return true;
        }

        public bool MoveUp()
        {
            int x, y, temp;
            x = Blank.X;
            y = Blank.Y - 1;
            if (y < 0)
            {
                return false;
            }
            temp = this[x, y];
            this[x, y] = this[Blank.X, Blank.Y];
            this[Blank.X, Blank.Y] = temp;
            Blank = new Point(x, y);
            return true;
        }

        public bool MoveDown()
        {
            int x, y, temp;
            x = Blank.X;
            y = Blank.Y + 1;
            if (y > Height - 1)
            {
                return false;
            }
            temp = this[x, y];
            this[x, y] = this[Blank.X, Blank.Y];
            this[Blank.X, Blank.Y] = temp;
            Blank = new Point(x, y);
            return true;
        }

        public Puzzle Copy()
        {
            Puzzle copy = new Puzzle(Width, Height);
            copy.Blank = new Point(Blank.X, Blank.Y);
            copy._container = _container.Clone() as int[,];
            return copy;
        }

        public override bool Equals(object obj)
        {
            Puzzle other = obj as Puzzle;
            if (other == null)
            {
                return false;
            }

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (this[x, y] != other[x, y])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override string ToString()
        {
            string str = string.Empty;

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if(x== Blank.X && y == Blank.Y)
                    {
                        str += " + ";
                    }
                    else
                    {
                        if (this[x, y] < 10)
                        {
                            str += " ";
                        }
                        str += this[x, y] + " ";
                    }
                }
                str += "\n";
            }
            return str;
        }
    }
}
