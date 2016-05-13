using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final
{
    public class Blocks : Game
    {

        public Blocks(string type) : base(type)
        {
            snake.setColor(Color.Black);
            block = new List<Block>();
           // base.block = new List<Block>();
            generateBlock();
            generateFood();
        }

        public override void Draw(Graphics graphics)
        {
            snake.Draw(graphics);
            foreach (Food f in food)
            {
                f.Draw(graphics);
            }
            foreach (Block b in block)
            {
                b.Draw(graphics);
            }
        }

        public override void generateFood()
        {
            Part tmp = null;
            Block pom = null;
            int x, y;
            x = random.Next(0, 800 / 10 - 10);
            y = random.Next(0, 600 / 10 - 10);
            tmp = new Part(x, y, snake.boja);
            pom = new Block(x, y);
            if (!snake.parts.Contains(tmp) && !block.Contains(pom))
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
            Block pom;
            int x, y;
            x = random.Next(0, 800 / 10 - 10);
            y = random.Next(0, 600 / 10 - 10);
            tmp = new Part(x, y, snake.boja);
            pom = new Block(x, y);
            if (!snake.parts.Contains(tmp)&&!block.Contains(pom))
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
    }
}
