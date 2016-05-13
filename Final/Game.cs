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
        public Random random;
        public String type;
        private int score = 0;
        public enum GameType
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
            specialFood = false;
            food.Add(new SpecialFood(20, 0));
            //generateFood();
        }
        public abstract void Draw(Graphics graphics);
        public abstract void generateFood();
        public abstract void generateSpecialFood();
        
        
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
