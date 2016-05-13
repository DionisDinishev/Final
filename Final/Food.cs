using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final
{
    public abstract class Food
    {

        public int X;
        public int Y;
        public const int width = 10;
        public Food(int x, int y)
        {
            this.X = 7 + x * 10;
            this.Y = 7 + y * 10;
        }
        public int getX()
        {
            return (X-7)/10;
        }
        public int getY()
        {
            return (Y-7)/10;
        }   
        public override bool Equals(object obj)
        {
            Food f;
            if (obj == null)
            {
                return false;
            }

            f = obj as Food;
            if (f.GetType() == GetType())
            {
                if (f.X == X && f.Y == Y)
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }
        public abstract void Draw(Graphics g);
        
    }
}
