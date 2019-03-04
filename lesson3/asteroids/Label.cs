using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asteroids
{/// <summary>
/// Класс текстового поля. 
/// </summary>
    class Label:BaseObject
    {
        public string Text { get; set; }
        protected Color brushColor;
        protected uint col=0xFFFF0000;

        public Label(string Text,Point pos,Size size):base(pos,new Point(0,0),size)
        {
            brushColor = Color.FromArgb((int)col);
            this.Text = Text;
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawString(Text, new Font("Arial", Size.Height), new SolidBrush(brushColor), Pos);
        }
        public override void Update()
        {
            col++;
            brushColor=Color.FromArgb((int)col);
        }
    }
}
