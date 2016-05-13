using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final
{
    public class ClassicGame : Game
    {
        public ClassicGame(string type) : base(type)
        {
            snake.setColor(Color.Black);
        }

        public override void Draw(Graphics graphics)
        {
            snake.Draw(graphics);
            foreach (Food f in food)
            {
                f.Draw(graphics);
            }
        }
    }
}
