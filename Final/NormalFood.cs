using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final
{
    public class NormalFood : Food
    {
        public NormalFood(int x, int y) : base(x, y)
        {

        }

        public override void Draw(Graphics g)
        {
            Brush b = new SolidBrush(Color.Black);
            g.FillRectangle(b, X - 5, Y - 5, width, width);
            b.Dispose();
        }
        // override object.Equals
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

    }
}
