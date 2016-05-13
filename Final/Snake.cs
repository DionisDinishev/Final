using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final
{
    public class Snake
    {
        public List<Part> parts;
        public List<int> moves;
        public Part firstPart;
        public Part lastPart;
        private int lastmove;
        private int direction;
  
        private int brojac;
        public Color boja { get; set; }
        public enum Direction
        {
            Top = 1,
            Right = 2,
            Bottom = 3,
            Left = 4
        }
        public Snake()
        {

            parts = new List<Part>();
            moves = new List<int>();
            parts.Add(new Part(6, 0, boja));
            parts.Add(new Part(5, 0, boja));
            parts.Add(new Part(4, 0, boja));
            parts.Add(new Part(3, 0, boja));
            parts.Add(new Part(2, 0, boja));
            parts.Add(new Part(1, 0, boja));
            parts.Add(new Part(0, 0, boja));
            for (int i = 0; i < 7; i++)
            {
                moves.Add(2);
            }
            firstPart = parts[0];
            lastPart = parts.Last();
            brojac = 0;
        }
        public void AddPart(Part p)
        {
            parts.Add(p);
            firstPart = parts.First();
            lastPart = parts.Last();
            moves.Add(moves[moves.Count - 1]);
        }
        /*
       TOP = 0 -1
       RIGHT = 1 0
       BOTTOM = 0 1
       LEFT = -1 0
      */
        public void move(Direction d)
        {
            lastmove = (int)d;
            direction = (int)d;
            moves.Insert(0, direction);
            moves.Remove(moves.Count);
            int i = 0;
            foreach(Part p in parts)
            {
                p.move(moves[i]);
                i++;
            }
        }
        public bool eatItself()
        {
            Part test = new Part(firstPart.getX(), firstPart.getY(), boja);
            for (int i = 1; i < parts.Count; i++)
            {
                if (test.Equals(parts[i]))
                {
                    return true;
                }
            }
            return false;
        }
        public bool isGameOver()
        {
            if (firstPart.getX() < 0 || firstPart.getY() < 0 || firstPart.getX() > 780 / 10 || firstPart.getY() > 510 / 10)
            {
                return true;
            }
            else return false;
        }

        public void Draw(Graphics g)
        {
            brojac++;
            int[] niza = new int[] { 0, 1, 2, 3 };
            int j = 0;
            int k = 0;
            if (brojac == 4)
            {
                brojac = 0;
            }
            for (int i = 0; i < parts.Count; i++)
            {
               
                j = niza[(i + brojac) % 4];

                if (moves[i] % 2 == 0)
                {
                    if (j == 0)
                    {
                        parts[i].v = -1;
                        parts[i].Draw(g);
                        parts[i].v = 0;

                    }
                    else if (j == 1)
                    {
                        parts[i].v = 0;
                        parts[i].Draw(g);
                        parts[i].v = 0;
                    }
                    else if (j == 2)
                    {
                        parts[i].v = 1;
                        parts[i].Draw(g);
                        parts[i].v = 0;
                    }
                    else if (j == 3)
                    {
                        parts[i].v = 0;
                        parts[i].Draw(g);
                        parts[i].v = 0;

                    }

                }

                else if (moves[i] % 2 == 1)
                {
                    if (j == 0)
                    {

                        parts[i].h = -1;
                        parts[i].Draw(g);
                        parts[i].h = 0;
                    }
                    else if (j == 1)
                    {
                        parts[i].h = 0;
                        parts[i].Draw(g);
                        parts[i].h = 0;
                    }
                    else if (j == 2)
                    {
                        parts[i].h = 1;
                        parts[i].Draw(g);
                        parts[i].h = 0;

                    }
                    else if (j == 3)
                    {
                        parts[i].h = 0;
                        parts[i].Draw(g);
                        parts[i].h = 0;
                    }
                 
                }
            }
        }
        public void setColor(Color c)
        {
            boja = c;
            for (int i = 0; i < parts.Count; i++)
            {
                parts[i].color = c;
            }
        }
    }
}
