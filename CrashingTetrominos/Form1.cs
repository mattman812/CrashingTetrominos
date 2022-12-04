using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrashingTetrominos
{
    public partial class Form1 : Form
    {
        private int rows = 20;
        private int cols = 10;
        private int size = 32;
        private Bitmap canvasBitmap;
        private Graphics canvasGraphics;
        private int canvasWidth = 15;
        private int canvasHeight = 20;
        private int[,] canvasDotArray;
        private int dotSize = 20;
        Timer timer = new Timer();
        const string backGroundScreen = @".\..\..\images\bg.png";

        //var canvas;
        //var ctx;
        //var blockImg;
        //var bgImg;
        //var gameOverImg;
        //var curPiece;
        //var gameData;
        //var imgLoader;
        //var prevTime;
        //var curTime;
        //var isGameOver;
        //var lineSpan;
        //var curLines; //is a counter for number of lines completed
        //var touchX;
        //var touchY;
        //var touchId;

        //initalize vairables


        private static int xMax = 10;
        private static int yMax = 15;
        
        public int[,] aField = new int[xMax, yMax];
        private const int boarder = -1;
        private const int free = 0;


        /// <summary>
        /// Form1_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            ////Initialize Game Engine
            Engine gameEngine = new Engine();
            gameEngine.Initialize();
            ////Call Canvas and populate it.
            pnlGameCanvas.BackColor = Color.Black;
            Image bg = new Bitmap(backGroundScreen);
            pnlGameCanvas.BackgroundImage = bg;
            //Board Canvas = new Board();
            FillField();        
        }
        /// <summary>
        /// Fill Field
        /// </summary>
        private void FillField()
        {
            for (int y = 0; y < yMax; y++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    //aField(x, y) = free;
                }
            }
        }

        /// <summary>
        /// Form1
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            LoadCanvas();

            currentShape = GetRandomShapeWithCenterAligned();
            nextShape = GetNextShape();

            timer.Tick += TimerTick;
            timer.Interval = 500;
            timer.Start();

            this.KeyDown += Form1_KeyDown;            

        }

        /// <summary>
        /// Key Strokes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            var verticalMove = 0;
            var horizontalMove = 0;

            switch (e.KeyCode)
            {
                case Keys.Left:
                    verticalMove--;
                    break;
                case Keys.Right:
                    verticalMove++;
                    break;
                case Keys.Down:
                    horizontalMove++;
                    break;
                case Keys.Up:
                    currentShape.Turn();
                    break;
                default:
                    return;
            }

            var isMoveSuccess = MoveShapeIfPossible(horizontalMove, verticalMove);

            if (!isMoveSuccess && e.KeyCode == Keys.Up)
                currentShape.RollBack();
        }
      
        /// <summary>
        /// Timer Tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerTick(object sender, EventArgs e)
        {
            var isMoveSuccess = MoveShapeIfPossible(moveDown: 1);

            if (!isMoveSuccess)
            {
                canvasBitmap = new Bitmap(workingBitmap);
                UpdateCavasDotArrayWithCurrentShape();
                currentShape = nextShape;
                nextShape = GetNextShape();

                ClearFilledRowsAndUpdateScore();
            }
        }

        /// <summary>
        /// Load Canvas
        /// </summary>
        private void LoadCanvas()
        {
            int bgX = 0;
            int bgY = 0;
            Point bgPoint = new Point();
            bgPoint.X = bgX;
            bgPoint.Y = bgY;

            pnlGameCanvas.Width = canvasWidth * dotSize;
            pnlGameCanvas.Height = canvasHeight * dotSize;
            canvasBitmap = new Bitmap(pnlGameCanvas.Width, pnlGameCanvas.Height);
            canvasGraphics = Graphics.FromImage(canvasBitmap);
            canvasGraphics.FillRectangle(Brushes.LightGray, 0, 0, canvasBitmap.Width, canvasBitmap.Height);
            pnlGameCanvas.BackgroundImage = canvasBitmap;
            canvasDotArray = new int[canvasWidth, canvasHeight];

            Bitmap BackgroundImage = new Bitmap(backGroundScreen);
            //canvasGraphics = new Graphics().DrawImage(BackgroundImage, bgPoint);//.FromImage(BackgroundImage);
            //this.Paint += new PaintEventHandler(DrawImagePointF);
            //canvasGraphics.FillRectangle(Brushes.LightGray, 0, 0, canvasBitmap.Width, canvasBitmap.Height);

            // Load bitmap into picture box
            //pnlGameCanvas.BackgroundImage = canvasBitmap;
            //pictureBox1.Image = canvasBitmap;

            // Initialize canvas dot array. by default all elements are zero
            //canvasDotArray = new int[canvasWidth, canvasHeight];


            //e.Graphics.DrawImage(BackgroundImage, bgPoint);
        }

        /// <summary>
        /// Draw Image Point F
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawImagePointF(object sender, PaintEventArgs e)
        {
            Bitmap BackgroundImage = new Bitmap(backGroundScreen);
          
            //Image newImage = Image.FromFile("SampImag.jpg");
            PointF ulCorner = new PointF(29.0F, 44.0F);
            e.Graphics.DrawImage(BackgroundImage, ulCorner);
        }
        /// <summary>
        /// close Tool Strip Menu Item - Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// pnl Game Canvas - Paint Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlGameCanvas_Paint(object sender, PaintEventArgs e)
        {
            int bgX = 0;
            int bgY = 0;
            Point bgPoint = new Point();
            bgPoint.X = bgX;
            bgPoint.Y = bgY;

            Bitmap BackgroundImage = new Bitmap("../images/bg.png");

            e.Graphics.DrawImage(BackgroundImage, bgPoint);

        }

        int currentX;
        int currentY;
        /// <summary>
        /// Get Random Shape With Center Aligned
        /// </summary>
        /// <returns></returns>
        private Shape GetRandomShapeWithCenterAligned()
        {
            var shape = ShapesHandler.GetRandomShape();
            currentX = 7;           
            currentY = -shape.height;

            return shape;
        }

        Shape currentShape;
        Shape nextShape;

        /// <summary>
        /// Move Shape If Possible
        /// </summary>
        /// <param name="moveDown"></param>
        /// <param name="moveSide"></param>
        /// <returns></returns>
        private bool MoveShapeIfPossible(int moveDown = 0, int moveSide = 0)
        {
            var newX = currentX + moveSide;
            var newY = currentY + moveDown;
            //If out of bounds return false
            if (newX < 0 || newX + currentShape.width > canvasWidth || newY + currentShape.height > canvasHeight) return false;

            for (int i = 0; i < currentShape.width; i++)
            {
                for (int j = 0; j < currentShape.height; j++)
                {
                    if (newY + j > 0 && canvasDotArray[newX + i, newY + j] == 1 && currentShape.dots[j, i] == 1) return false;
                }
            }

            currentX = newX;
            currentY = newY;

            DrawShape();

            return true;
        }

        Bitmap workingBitmap;
        Graphics workingGraphics;
        /// <summary>
        /// Draw Shape
        /// </summary>
        private void DrawShape()
        {
            workingBitmap = new Bitmap(canvasBitmap);
            workingGraphics = Graphics.FromImage(workingBitmap);

            for (int i = 0; i < currentShape.width; i++)
            {
                for (int j = 0; j < currentShape.height; j++)
                {
                    if (currentShape.dots[j, i] == 1)
                        workingGraphics.FillRectangle(currentShape.currbrush, (currentX + i) * dotSize, (currentY + j) * dotSize, dotSize, dotSize);
                }
            }

            pnlGameCanvas.BackgroundImage = workingBitmap;
        }

        /// <summary>
        /// update Cavas Dot Array With Current Shape
        /// </summary>
        private void UpdateCavasDotArrayWithCurrentShape()
        {
            for (int i = 0; i < currentShape.width; i++)
            {
                for (int j = 0; j < currentShape.height; j++)
                {
                    if (currentShape.dots[j, i] == 1)
                    {
                        CheckIfGameOver();

                        canvasDotArray[currentX + i, currentY + j] = 1;
                    }
                }
            }
        }

        int score;
        /// <summary>
        /// Clear Filled Rows And Update Score
        /// </summary>
        public void ClearFilledRowsAndUpdateScore()
        {
            for (int i = 0; i < canvasHeight; i++)
            {
                int j;
                for (j = canvasWidth - 1; j >= 0; j--)
                {
                    if (canvasDotArray[j, i] == 0)
                        break;
                }

                if (j == -1)
                {
                    score++;
                    lblScoreUpdate.Text = score.ToString();
                    lblLevelUpdate.Text = (score / 10).ToString();
                    timer.Interval -= 10;

                    for (j = 0; j < canvasWidth; j++)
                    {
                        for (int k = i; k > 0; k--)
                        {
                            canvasDotArray[j, k] = canvasDotArray[j, k - 1];
                        }
                        canvasDotArray[j, 0] = 0;
                    }
                }
            }

            for (int i = 0; i < canvasWidth; i++)
            {
                for (int j = 0; j < canvasHeight; j++)
                {
                    canvasGraphics = Graphics.FromImage(canvasBitmap);
                    canvasGraphics.FillRectangle(canvasDotArray[i,j]==1? Brushes.Black : Brushes.LightGray, i * dotSize, j * dotSize, dotSize, dotSize);
                }
            }
            pnlGameCanvas.BackgroundImage = canvasBitmap;
        }

        Bitmap nextShapeBitmap;
        Graphics nextShapeGraphics;
        /// <summary>
        /// Get Next Shape
        /// </summary>
        /// <returns></returns>
        private Shape GetNextShape()
        {
            var shape = GetRandomShapeWithCenterAligned();
            nextShapeBitmap = new Bitmap(6 * dotSize, 6 * dotSize);
            nextShapeGraphics = Graphics.FromImage(nextShapeBitmap);

            nextShapeGraphics.FillRectangle(Brushes.LightGray, 0, 0, nextShapeBitmap.Width, nextShapeBitmap.Height);

            var startX = (6 - shape.width) / 2;
            var startY = (6 - shape.height) / 2;

            for (int i = 0; i < shape.height; i++)
            {
                for (int j = 0; j < shape.width; j++)
                {
                    nextShapeGraphics.FillRectangle(shape.dots[i, j] == 1 ? Brushes.Black : Brushes.LightGray, (startX + j) * dotSize, (startY + i) * dotSize, dotSize, dotSize);

                }
            }
            pbDisplayNext.Size = nextShapeBitmap.Size;
            pbDisplayNext.Image = nextShapeBitmap;

            return shape;
        }

        /// <summary>
        /// Check If Game Over
        /// </summary>
        private void CheckIfGameOver()
        {
            if (currentY < 0)
            {
                timer.Stop();
                MessageBox.Show("Game Over");
                Application.Restart();
            }
        }
    }
}
