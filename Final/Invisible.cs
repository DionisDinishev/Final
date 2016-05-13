using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final
{
    public class Invisible : Game
    {
        public Invisible(string type) : base(type)
        {
            snake.setColor(Color.Transparent);
        }

        public override void Draw(Graphics graphics)
        {

            snake.Draw(graphics);
            foreach (Food f in food)
            {
                f.Draw(graphics);
            }
        }

        public override void generateFood()
        {
            Part tmp = null;
            int x, y;
            x = random.Next(0, 800 / 10 - 10);
            y = random.Next(0, 600 / 10 - 10);
            tmp = new Part(x, y, snake.boja);
            if (!snake.parts.Contains(tmp))
            {
                food.Add(new NormalFood(x, y));
            }
            else
            {
                generateFood();
            }

        }

        public override void generateSpecialFood()
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
    }
}
