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
        private Random rand = new Random();
        private Point foodPosition;

        // graphics
        private Bitmap offScreenBitmap;
        private Graphics offScreenGraphics;
        private Graphics graphics;

        // square variables
        private int squareSize = 20;
        private List<Point> snake = new List<Point>();
        private string direction = "Right";

        public SnakeGame()
        {
            InitializeComponent();

            // initializing graphics
            offScreenBitmap = new Bitmap(this.Width, this.Height);
            offScreenGraphics = Graphics.FromImage(offScreenBitmap);
            graphics = this.CreateGraphics();
            this.DoubleBuffered = true;

            snake.Add(new Point(100, 100));

            timer1.Interval = 100; // 16 should be approximately 60 FPS, set to 1 for more updates per second
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateGame();
            graphics.DrawImage(offScreenBitmap, 0, 0);
        }
        private void UpdateGame()
        {
            MoveSnake();
            CheckCollisions();
            DrawGrid();
            DrawSnake();
            SpawnFood();
        }
        private void MoveSnake()
        {
            for (int i = snake.Count - 1; i > 0; i--)
            {
                snake[i] = snake[i - 1];
            }

            Point head = snake[0];
            switch (direction)
            {
                case "Up":
                    head.Y -= squareSize;
                    break;
                case "Down":
                    head.Y += squareSize;
                    break;
                case "Left":
                    head.X -= squareSize;
                    break;
                case "Right":
                    head.X += squareSize;
                    break;
            }
            snake[0] = head;

            offScreenGraphics.Clear(Color.Black);
        }
        private void DrawSnake()
        {
            foreach (Point part in snake)
            {
                offScreenGraphics.FillRectangle(Brushes.White, part.X, part.Y, squareSize, squareSize);
            }
        }
        private void SpawnFood()
        {
            if (foodPosition == Point.Empty)
            {
                // Generate random coordinates for the food
                int maxX = (this.Width / squareSize) - 1;
                int maxY = (this.Height / squareSize) - 1;
                int foodX = rand.Next(0, maxX) * squareSize;
                int foodY = rand.Next(0, maxY) * squareSize;

                // Set the food position
                foodPosition = new Point(foodX, foodY);
            }

            // Clear previous food position
            offScreenGraphics.FillRectangle(Brushes.Black, foodPosition.X, foodPosition.Y, squareSize, squareSize);

            // Draw the food
            offScreenGraphics.FillRectangle(Brushes.White, foodPosition.X, foodPosition.Y, squareSize, squareSize);
        }

        private void CheckCollisions()
        {
            if (snake[0] == foodPosition)
            {
                snake.Add(new Point(-squareSize, -squareSize));  // Add new segment
                foodPosition = Point.Empty;
                SpawnFood();
            }

            // snake runs into the walls
            if (snake[0].X < 0 || snake[0].X >= this.Width || snake[0].Y < 0 || snake[0].Y >= this.Height)
            {
                GameOver();
            }

            // snake runs into itself
            for (int i = 1; i < snake.Count; i++)
            {
                if (snake[0] == snake[i])
                {
                    GameOver();
                }
            }
        }

        private void SnakeGame_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    if (direction != "Down") direction = "Up";
                    break;
                case Keys.S:
                    if (direction != "Up") direction = "Down";
                    break;
                case Keys.A:
                    if (direction != "Right") direction = "Left";
                    break;
                case Keys.D:
                    if (direction != "Left") direction = "Right";
                    break;
                case Keys.Escape:
                    this.Close();
                    break;
            }
        }


        private void GameOver()
        {
            timer1.Stop();
            MessageBox.Show("Game Over!");
            this.Close();
        }

        private void DrawGrid()
        {
            this.BackColor = Color.Black;
            for (int x = 0; x < Width; x += 20)
            {
                //Draw vertical lines
                offScreenGraphics.DrawLine(Pens.White, x, 0, x, this.Height);
            }

            for (int y = 0; y < Height; y += 20)
            {
                //Draw horizontal lines
                offScreenGraphics.DrawLine(Pens.White, 0, y, this.Width, y);
            }
        }
    }
}
