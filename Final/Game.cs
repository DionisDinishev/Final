using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final
{
    public abstract class Game
    {
        public Snake snake;
        public List<Food> food;
        public List<Block> block;
        public bool specialFood;
        private Random random;
        private String type;
        private int score = 0;
        private enum GameType
        {
            Classic,
            TimeAttack,
            Block,
            Invisible
        }
        public Game(String type)
        {
            snake = new Snake();
            food = new List<Food>();
            random = new Random();
            this.type = type;
            generateFood();
            if (type == "Blocks")
            {
                block = new List<Block>();
                generateBlock();
            }
            specialFood = false;
            food.Add(new SpecialFood(20, 0));
        }
        public abstract void Draw(Graphics graphics);
        private void generateFood()
        {
            Part tmp = null;
            Block pom = null;
            int x, y;
            x = random.Next(0, 800 / 10 - 10);
            y = random.Next(0, 600 / 10 - 10);
            tmp = new Part(x, y, snake.boja);
            pom = new Block(x, y);
            if (type == GameType.Block.ToString())
            {
                if (!snake.parts.Contains(tmp) && !block.Contains(pom))
                {
                    food.Add(new NormalFood(x, y));
                }
                else
                {
                    generateFood();
                }
            }
            else
            {
                if (!snake.parts.Contains(tmp))
                {
                    food.Add(new NormalFood(x, y));
                }
                else
                {
                    generateFood();
                }
            }
        }
        public void generateSpecialFood()
        {
            Part tmp;
            int x, y;
            x = random.Next(0, 800 / 10 - 10);
            y = random.Next(0, 600 / 10 - 10);
            tmp = new Part(x, y, snake.boja);

            if (!snake.parts.Contains(tmp))
            {
                food.Add(new SpecialFood(x, y));
            }
            specialFood = true;
        }
        private void generateBlock()
        {
            for (int i = 0; i < 36; i++)
            {
                block.Add(new Block(20 + i, 13));
                block.Add(new Block(20 + i, 37));
            }
            for (int i = 0; i < 6; i++)
            {
                block.Add(new Block(20, 13 + i));
                block.Add(new Block(20, 37 - i));
                block.Add(new Block(56, 37 - i));
                block.Add(new Block(56, 13 + i));

            }
        }
        public bool eat(Snake.Direction last)
        {
            Part p = null;
            bool flag = false;
            bool add = false;
            double dis = 50;
            if (food.Count != 0)
                dis = getDistance(new Point(snake.firstPart.getX(), snake.firstPart.getY()), new Point(food.First().getX(), food.First().getY()));
            if (type != GameType.Invisible.ToString())

            {
                if (dis < 10)
                {
                    snake.setColor(Color.Green);
                }
                else if (dis >= 10 && dis < 20)
                {
                    snake.setColor(Color.GreenYellow);
                }
                else if (dis >= 20 && dis < 30)
                {
                    snake.setColor(Color.Yellow);
                }
                else if (dis >= 30 && dis < 40)
                {
                    snake.setColor(Color.YellowGreen);
                }
                else
                {
                    snake.setColor(Color.Red);
                }
            }
            Food f = new NormalFood(snake.firstPart.getX(), snake.firstPart.getY());
            Food s = new SpecialFood(snake.firstPart.getX(), snake.firstPart.getY());
            if (food.Contains(f))
            {
                score++;
                food.Remove(f);
                p = new Part(snake.lastPart.getX(), snake.lastPart.getY(), snake.boja);
                generateFood();
                add = true;
            }
            else if (food.Contains(s))
            {
                specialFood = false;
                food.Remove(s);
                score += 10;
                flag = true;
            }
            snake.move(last);
            if (add)
            {
                snake.AddPart(p);
                add = false;
            }
            return flag;
        }
        private double getDistance(Point point1, Point point2)
        {
            double a = (double)(point2.X - point1.X);
            double b = (double)(point2.Y - point1.Y);
            return Math.Sqrt(a * a + b * b);
        }
        public bool update(Snake.Direction last)
        {
            if (snake.isGameOver() || snake.eatItself() || Hit(snake.firstPart))
            {
                return true;
            }
            else return false;

        }
        public bool Hit(Part obj)
        {
            if (type == "Blocks")
            {
                Block b = new Block(obj.getX(), obj.getY());
                if (block.Contains(b))
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }
        public String snakePosition()
        {
            return "X:" + snake.firstPart.getX() + " Y:" + snake.firstPart.getY();
        }
        public String foodPosition()
        {
            if (food.Count != 0)
            {
                return "X:" + food.First().getX() + " Y:" + food.First().getY();
            }
            else return "";
        }
        public String points()
        {
            return score.ToString();
        }
    }
}
