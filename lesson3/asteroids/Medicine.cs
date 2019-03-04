using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace asteroids
{
    class Medicine : BaseObject
    {
        public Medicine(Point pos,Point dir,Size size):base(pos,dir,size)
        {

        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(new Pen(Color.Red, 1), Pos.X - Size.Width / 2, Pos.Y, Size.Width, Size.Height);
            Game.Buffer.Graphics.DrawLine(new Pen(Color.Red, 4), Pos.X, Pos.Y+2, Pos.X, Pos.Y + Size.Height-2);
            Game.Buffer.Graphics.DrawLine(new Pen(Color.Red, 4), Pos.X-Size.Width/2+2, Pos.Y+Size.Height/2, 
                Pos.X + Size.Width/2-2, Pos.Y + Size.Height / 2);
        }

        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
        }
        public bool isScreenOut()
        {
            return (Pos.X < 0);
        }
    }
}
