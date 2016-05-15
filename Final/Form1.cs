using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final
{
    public partial class Form1 : Form
    {
        Game game;
        private Random random;
        private Random tmp;
        private Snake.Direction last;
        private Keys lastKey;
        private bool delete;
        private bool pause;
        private bool gameOver;
        private bool first;
        private bool deadSnake;
        private int deadAnimation;
        private int speed;
        private int counter;
        private int specialFoodCounter;
        private int i = 0;
        private String gameType;
        private ToolStripStatusLabel timeLeft;
        private ToolStripStatusLabel food;
        private ToolStripProgressBar progressBar;
        private ToolStripProgressBar foodBar;
        private StreamWriter sw;
        private StreamReader sr;
        public Form2 f;
        public Form1()
        {
            gameOver = false;
            delete = false;
            pause = false;
            first = true;
            deadSnake = false;
            deadAnimation = 0;
            specialFoodCounter = 0;
            random = new Random();
            tmp = new Random();
            last = Snake.Direction.Right;
            lastKey = Keys.Right;
            f = new Form2();
            f.ShowDialog();
            InitializeComponent();
            if (f.DialogResult == DialogResult.OK)
            {
                speed = f.speed;
                gameType = f.gameType;
            }
            if (f.gameType == "Classic")
            {
                game = new ClassicGame(f.gameType);
            }
            else if (f.gameType == "Time attack")
            {
                game = new TimeAttack(f.gameType);
                timeLeft = new ToolStripStatusLabel();
                progressBar = new ToolStripProgressBar();
                progressBar.Minimum = 0;
                progressBar.Maximum = 120;
                statusStrip1.Items.Add(timeLeft);
                statusStrip1.Items.Add(progressBar);
                counter = 0;
            }
            else if (f.gameType == "Invisible")
            {
                game = new Invisible(f.gameType);
            }
            else
            {
                game = new Blocks(f.gameType);
            }
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (!gameOver)
            {
                game.Draw(e.Graphics);
                Pen b = new Pen(Color.Red);
                b.Width = 2;
                e.Graphics.DrawRectangle(b, 1, 1, 780, 510 + 26);
            }
            else
            {

            }
            if (first)
            {
                Font f = new Font("Arial", 26);
                Brush b = new SolidBrush(Color.Gray);
                String text = "Press any key to Start";
                e.Graphics.DrawString(text, f, b, this.Width / 2 - 26 * text.Count() / 3, this.Height / 2 - 26);
                f.Dispose();
                b.Dispose();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (first)
            {
                timer1.Start();
                timer2.Start();
                first = false;
            }
            else if (!gameOver)
            {
                if (lastKey.Equals(e.KeyData))
                {
                    return;
                }
                if (e.KeyData.Equals(Keys.Up) && last != Snake.Direction.Bottom)
                {
                    last = Snake.Direction.Top;
                    lastKey = Keys.Up;
                    game.eat(last);
                }
                else if (e.KeyData.Equals(Keys.Right) && last != Snake.Direction.Left)
                {
                    last = Snake.Direction.Right;
                    lastKey = Keys.Right;
                    game.eat(last);
                }
                else if (e.KeyData.Equals(Keys.Down) && last != Snake.Direction.Top)
                {
                    last = Snake.Direction.Bottom;
                    lastKey = Keys.Down;
                    game.eat(last);
                }
                else if (e.KeyData.Equals(Keys.Left) && last != Snake.Direction.Right)
                {
                    last = Snake.Direction.Left;
                    lastKey = Keys.Left;
                    game.eat(last);

                }
                Invalidate();
                //timer1.Start();
                if (e.KeyData.Equals(Keys.Space))
                {
                    if (pause)
                    {
                        timer1.Start();
                        timer2.Start();
                        pause = false;
                    }
                    else
                    {
                        timer1.Stop();
                        timer2.Stop();
                        pause = true;
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bool flag = game.eat(last);
            if (game.update(last))
            {
                gameOver = true;
            }
            if(gameOver)
            {
                timer4.Enabled = true;
                game.snake.setColor(Color.Transparent);
                game.food.Clear();
                timer2.Stop();
                if(deadAnimation==9)
                {
                    deadSnake = true;
                    Thread.Sleep(250);
                }
            }
            else
            {
                Invalidate();
            }
            if (gameOver&&deadSnake)
            {
                gameOver = true;
                timer1.Stop();
                timer2.Stop();
                timer1.Enabled = false;
                timer2.Enabled = false;
                DialogResult result = DialogResult.No;
                String high;
                int highscore = 0;
                int current;
                if (File.Exists("score.txt"))
                {
                    sr = new StreamReader("score.txt");
                    high = sr.ReadLine();
                    sr.Close();
                    int.TryParse(high, out highscore);
                    int.TryParse(game.points(), out current);
                    if (current > highscore)
                    {
                        sw = new StreamWriter("score.txt");
                        result = MessageBox.Show("New High Score!\n" + "High score:" + highscore + "\nYour Score:" + current + "\nTry again?", "Game Over", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        sw.Write(current.ToString());
                        sw.Flush();
                        sw.Close();

                    }
                    else
                    {
                        result = MessageBox.Show("High score:" + highscore + "\nYour Score:" + current + "\nTry again?", "Game Over", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    sr = new StreamReader("score.txt");
                    sw = new StreamWriter("score.txt");

                    result = MessageBox.Show("New High Score!\n" + "High score:" + 0 + "\nYour Score:" + game.points() + "\nTry again?", "Game Over", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    sw.Write(game.points());
                    sw.Flush();
                    sw.Close();
                    sr.Close();
                }

                if (DialogResult.Yes == result)
                {
                    if (sw != null)
                    {
                        sw.Dispose();
                        sw.Close();
                    }
                    if (sr != null)

                    {
                        sr.Dispose();
                        sr.Close();
                    }
                    this.Hide();
                    Form1 f = new Form1();
                    f.ShowDialog();
                    this.Dispose();
                    this.Close();
                }
                else
                {
                    Application.Exit();

                }

            }
            else if (!game.specialFood)
            {
                statusStrip1.Items.Remove(foodBar);
                statusStrip1.Items.Remove(food);
            }
            if (flag)
            {
                
                timer3.Enabled = true;
                timer3.Start();
            }
            toolStripStatusLabel1.Text = "Head position " + game.snakePosition();
            toolStripStatusLabel2.Text = "Food position" + game.foodPosition();
            toolStripStatusLabel3.Text = "Score:" + game.points();

           
        }

        private void dead(int pos)
        {
            Font f = new Font("Arial", 26);
            Brush b = new SolidBrush(Color.Black);
            String text1 = "GAME OVER";
            String show2 = "";
            Graphics g = this.CreateGraphics();
            PointF p = new PointF(this.Width / 2-13*(pos), this.Height / 2-26);
            //  g.DrawString(show.ToString(), f, b,p);
            g.Clear(Color.White);
            show2 = text1.Substring(0, pos);
            g.DrawString(show2, f, b, p);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

            if (gameType == "Time attack")
            {
                counter++;
                timeLeft.Text = "Time Left " + (120 - counter).ToString() + ":seconds";
                progressBar.Value = 120 - counter;
                if (counter == 120)
                {
                    gameOver = true;

                }
            }
            specialFoodCounter++;


            if (specialFoodCounter == 10 && delete)
            {
                foreach (Food f in game.food)
                {
                    if (f is SpecialFood)
                    {
                        game.food.Remove(f);
                        break;
                    }
                }
                delete = false;
                statusStrip1.Items.Remove(foodBar);
                statusStrip1.Items.Remove(food);
            }
            else if (specialFoodCounter == 20)
            {
                game.generateSpecialFood();
                specialFoodCounter = 0;
                delete = true;
                foodBar = new ToolStripProgressBar();
                food = new ToolStripStatusLabel();
                food.Text = "Special food time left:" + (10 - specialFoodCounter).ToString();
                statusStrip1.Items.Add(food);
                statusStrip1.Items.Add(foodBar);
                foodBar.Maximum = 10;
                foodBar.Minimum = 1;
                foodBar.Step = -1;
                foodBar.Value = 10;
                return;
            }
            else if (delete)
            {
                foodBar.PerformStep();
                food.Text = "Special food time left" + (10 - specialFoodCounter).ToString();
            }

        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            int a, b, c;
            a = tmp.Next(255);
            b = tmp.Next(255);
            c = tmp.Next(255);
            if (i == 30)
            {
                i = 0;
                timer3.Stop();
                timer3.Enabled = false;
                this.BackColor = Color.DarkGray;
                return;
            }
            i++;
            this.BackColor = Color.FromArgb(a, b, c);
            Invalidate();
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            dead(deadAnimation);
            if (deadAnimation <=8)
            {

                deadAnimation++;
            }
            else
            {
                timer4.Enabled = false;
            }
        }
    }
}