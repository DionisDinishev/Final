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
        private bool exist;
        private int speed;
        private int counter;
        private int specialFoodCounter;
        private int i = 0;
        private String gameType;
        private ToolStripStatusLabel timeLeft;
        private ToolStripProgressBar progressBar;
        private ToolStripProgressBar foodBar;
        private FileStream fs;
        private StreamWriter sw;
        private StreamReader sr;
        public Form1()
        {
            gameOver = false;
            delete = false;
            pause = false;
            first = true;
            last = Snake.Direction.Right;
            lastKey = Keys.Right;
            random = new Random();
            specialFoodCounter = 0;
            Form2 f = new Form2();
            f.ShowDialog();
            InitializeComponent();
           
            //fs.Close();
            if (f.DialogResult == DialogResult.OK)
            {
                speed = f.speed;
                gameType = f.gameType;
            }
            else
            {
                speed = 50;
                gameType = "Classic";
            }
            if (f.gameType == "Classic")
            {
                game = new ClassicGame(f.gameType);
            }
            else if (f.gameType == "Time attack")
            {
                game = new TimeAttack(f.gameType);
                timer2.Enabled = true;
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
            timer1.Interval = speed;
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
                    //timer1.Stop();
                    last = Snake.Direction.Top;
                    lastKey = Keys.Up;
                    game.eat(last);
                    //game.update(last);
                }
                else if (e.KeyData.Equals(Keys.Right) && last != Snake.Direction.Left)
                {

                    // timer1.Stop();
                    last = Snake.Direction.Right;
                    lastKey = Keys.Right;
                    game.eat(last);
                    //game.update(last);
                }
                else if (e.KeyData.Equals(Keys.Down) && last != Snake.Direction.Top)
                {
                    //   timer1.Stop();
                    last = Snake.Direction.Bottom;
                    lastKey = Keys.Down;
                    game.eat(last);
                    //  game.update(last);
                }
                else if (e.KeyData.Equals(Keys.Left) && last != Snake.Direction.Right)
                {
                    // timer1.Stop();
                    last = Snake.Direction.Left;
                    lastKey = Keys.Left;
                    game.eat(last);

                }
                //     game.update(last);
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
            if(game.update(last))
            {
                gameOver = true;
            }
            if (gameOver)
            {
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
                    //sw.Close();
                    //sr.Close();
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

                //MessageBox.Show("Game Over");
                //sw.Close();
                //sr.Close();
                //fs.Close();
                gameOver = true;
                if (DialogResult.Yes == result)
                {
                    this.Hide();
                    Form1 f = new Form1();
                    f.ShowDialog();
                    this.Close();
                    this.Dispose();
                    //fs.Flush();
                    //fs.Dispose();
                    sw.Dispose();
                    sr.Dispose();
                    //fs.Close();
                    sw.Close();
                    sr.Close();
                    //GC.Collect();
                    //Thread.Yield();


                }
                else
                {
                    Application.Exit();
                 
                }
                
            }
            else if (!game.specialFood)
            {
                statusStrip1.Items.Remove(foodBar);
            }
            if (flag)
            {
                tmp = new Random();
                timer3.Enabled = true;
                timer3.Start();
            }
            toolStripStatusLabel1.Text = "Head position " + game.snakePosition();
            toolStripStatusLabel2.Text = "Food position" + game.foodPosition();
            toolStripStatusLabel3.Text = "Score:" + game.points();

            Invalidate();
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
                    //timer1.Stop();
                    //timer2.Stop();
                    
                }
            }
            specialFoodCounter++;
            if (specialFoodCounter == 5 && delete)
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
            }
            else if (specialFoodCounter == 10)
            {
                game.generateSpecialFood();
                specialFoodCounter = 0;
                delete = true;
                foodBar = new ToolStripProgressBar();
                statusStrip1.Items.Add(foodBar);
                foodBar.Maximum = 5;
                foodBar.Minimum = 1;
                foodBar.Step = -1;
                foodBar.Value = 5;
                return;
            }
            else if (delete)
            {
                foodBar.PerformStep();
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


    }
}