using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace snake_game_project
{
    public partial class SnakeGame : Form
    {
        // graphics
        private Bitmap offScreenBitmap;
        private Graphics offScreenGraphics;
        private Graphics graphics;

        // square variables
        private int squareX = 0;
        private int squareY = 0;
        private int squareSize = 20;

        // player variable
        private bool isDown = false;
        private bool isUp = false;
        private bool isLeft = false;
        private bool isRight = false;

        public SnakeGame()
        {
            InitializeComponent();

            // initializing graphics
            offScreenBitmap = new Bitmap(this.Width, this.Height);
            offScreenGraphics = Graphics.FromImage(offScreenBitmap);
            graphics = this.CreateGraphics();
            this.DoubleBuffered = true;

            timer1.Interval = 100; // 16 should be approximately 60 FPS, set to 1 for more updates per second
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Update();
            graphics.DrawImage(offScreenBitmap, 0, 0);
        }
        private void Update()
        {
            CheckDirection();
            MoveSnake();
        }
        private void MoveSnake()
        {
            offScreenGraphics.FillRectangle(Brushes.Black, 0, 0, Width, Height);
            offScreenGraphics.FillRectangle(Brushes.White, squareX, squareY, squareSize, squareSize);
        }
        private void CheckDirection()
        {
            if (isUp)
            {
                squareY -= 20;
            }
            else if (isDown)
            {
                squareY += 20;
            }
            else if (isRight)
            {
                squareX += 20;
            }
            else if (isLeft)
            {
                squareX -= 20;
            }
        }
        private void MoveInput(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    isDown = true;
                    isLeft = false;
                    isRight = false;
                    isUp = true;
                    break;
                case Keys.S:
                    isDown = true;
                    isLeft = false;
                    isRight = false;
                    isUp = false;
                    break;
                case Keys.D:
                    isDown = false;
                    isLeft = false;
                    isRight = true;
                    isUp = false;
                    break;
                case Keys.A:
                    isDown = false;
                    isLeft = true;
                    isRight = false;
                    isUp = false;
                    break;
            }
        }
        private void SnakeGame_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    timer1.Start();
                    break;
                case Keys.W:
                    MoveInput(e);
                    break;
                case Keys.S:
                    MoveInput(e);
                    break;
                case Keys.A:
                    MoveInput(e);
                    break;
                case Keys.D:
                    MoveInput(e);
                    break;
            }
        }




        //private void DrawGrid()
        //{
        //    this.BackColor = Color.Black;
        //    for (int x = 0; x < Width; x += 20)
        //    {
        //        //Draw vertical lines
        //        graphics.DrawLine(Pens.White, x, 0, x, this.Height);
        //    }

        //    for (int y = 0; y < Height; y += 20)
        //    {
        //        //Draw horizontal lines
        //        graphics.DrawLine(Pens.White, 0, y, this.Width, y);
        //    }
        //}
    }
}
