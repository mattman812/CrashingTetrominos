using System.Drawing;
namespace CrashingTetrominos
{
    internal class Shape
    {
        public int width;
        public int height;
        public int[,] dots;

        private int[,] backupDots;
        public int RandomShape { get; set; }    
        public int CurrentPosition { get; set; } 
        public int MovLeft { get; set; }
        public int MovRight { get; set; }
        public int MovUp { get; set; }
        public int MovDown { get; set; }
        public int MovRotate { get; set; }
        public Brush currbrush { get; set; }

        public Shape(int randomShape, int currentPosition, int movLeft, int movRight, int movUp, int movDown, int movRotate, Brush brush)
        { 
            RandomShape = randomShape;
            CurrentPosition = currentPosition;
            MovLeft = movLeft;
            MovRight = movRight;
            MovUp = movUp;
            MovDown = movDown;
            MovRotate = movRotate;
            currbrush = brush;
        }
        public Shape()
        {
            RandomShape = 0;
            CurrentPosition = 0;
            MovLeft = 0;
            MovRight = 0;
            MovUp = 0;
            MovDown = 0;
            MovRotate = 0;
            currbrush = Brushes.Blue;
        }

        public void Turn()
        {
            backupDots = dots;
            dots = new int[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    dots[i, j] = backupDots[height - 1 - j, i];
                }
            }
            var temp = width;
            width = height;
            height = temp;           
        }

        public void RollBack()
        {
            dots = backupDots;

            var temp = width;
            width = height;
            height = temp;
        }
    }    
}
