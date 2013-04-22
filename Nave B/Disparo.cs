using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Nave_B
{
    class Disparo
    {
        public SolidBrush ColorD { set; get; }
        public int Y { set; get; }
        public int X { set; get; }
        public int Ancho { set; get; }
        public int Alto { set; get; }

        public Disparo(int x, int y, int ancho, int alto, Color color) {
            this.X = x;
            this.Y = y;
            this.Ancho = ancho;
            this.Alto = alto;
            ColorD = new SolidBrush(color);
        }

        public GraphicsPath getPath() {
            GraphicsPath disparo = new GraphicsPath();
            disparo.AddRectangle(new Rectangle(this.X, this.Y, this.Ancho, this.Alto));
            this.X += 10;
            return disparo;
        }
    }
}
