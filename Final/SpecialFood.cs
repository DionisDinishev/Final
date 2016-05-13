using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final
{
    class SpecialFood : Food
    {
        public SpecialFood(int x, int y) : base(x, y)
        {

        }

        public override void Draw(Graphics g)
        {
            
            Size size = new Size(10, 10);
            Image img = new Bitmap(@"Resources\Apple-icon.png");
            Image show = new Bitmap(img, size);
            g.DrawImage(show, new Point(X - 5, Y - 5));
        }
    }
}

