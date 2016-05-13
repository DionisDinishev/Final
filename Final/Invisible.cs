﻿using System;
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
    }
}