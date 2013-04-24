using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Media;

namespace Nave_B
{
    class Asteroide
    {
        public int Y { set; get; }
        public int X { set; get; }
        public int Vx { set; get; }
        public int Size { set; get; }
        private int Ancho, Alto;
        private Matriz mr;
        private Matriz p1;
        private Matriz p2;
        private Matriz p3;
        private Matriz p4;
        private Random rand;
        private bool sentido_horario = true;
        private PointF[] puntos = new PointF[3];
        private GraphicsPath AstPath;
        private SoundPlayer colicion;

        public Asteroide(int ancho, int alto, int vx) {
            this.Ancho = ancho;
            this.Alto = alto;
            this.Vx = vx;
            mr = new Matriz(2, 2);
            p1 = new Matriz(1, 2);
            p2 = new Matriz(p1);
            p3 = new Matriz(p1);
            p4 = new Matriz(p1);
            rand = new Random();
            this.Size = rand.Next(15, 150);
            this.X = ancho + Size;
            this.Y = rand.Next(this.Alto / 2 - Size, this.Alto / 2 - Size);
        }

        public GraphicsPath getPath()
        {
            AstPath = new GraphicsPath();
            AstPath.AddEllipse(this.X - Size, this.Y - Size, 2 * Size, 2 * Size);
            return AstPath;
        }

        private void CargarAsteroide()
        {
            int grados = rand.Next(5, 25);
            // -- tamaño de la imagen a mostrar con los 4 puntos el size representa el tamaño de la imagen --
            // primer punto 
            p1[0, 0] = -Size; //x 
            p1[0, 1] = -Size;//y 
            //segundo punto 
            p2[0, 0] = Size; //x
            p2[0, 1] = -Size;//y 
            //tercer punto 
            p4[0, 0] = Size; //x
            p4[0, 1] = Size;//y
            //cuarto punto 
            p3[0, 0] = -Size; //x
            p3[0, 1] = Size;//y

            //rotacion
            double rad = (grados * (Math.PI)) / 180;
            if (sentido_horario)
            {
                // lado al que girar! sentido antihorario .... falta que codificar!!! 
                mr = new Matriz(2, 2);
                mr[0, 0] = Math.Cos(rad);
                mr[0, 1] = Math.Sin(rad);
                mr[1, 0] = -Math.Sin(rad);
                mr[1, 1] = Math.Cos(rad);
                sentido_horario = false;
            } else {
                //sentido horario
                mr = new Matriz(2, 2);
                mr[0, 0] = Math.Cos(rad);
                mr[0, 1] = -Math.Sin(rad);
                mr[1, 0] = Math.Sin(rad);
                mr[1, 1] = Math.Cos(rad);
                sentido_horario = true;
            }

            // metodo para rotar el objeto
            // matriz de puntos (1*2) por la matriz de rotacion (2*2)
            p1 = p1 * mr;
            p2 = p2 * mr;
            p3 = p3 * mr;
            p4 = p4 * mr;

            this.X -= Vx;
        }

        // metodo para dibujarlo
        public PointF[] getPuntos()
        {
            CargarAsteroide();
            puntos[0] = new PointF((float)p1[0, 0], (float)p1[0, 1]); //primer punto
            puntos[1] = new PointF((float)p2[0, 0], (float)p2[0, 1]); //segundo punto
            puntos[2] = new PointF((float)p3[0, 0], (float)p3[0, 1]); //tercer punto
            return puntos;
        }

        public void destruir(){
            Size -= 15;
            colicion = new SoundPlayer();
            colicion.Stream = Properties.Resources.colicion;
            colicion.Play();
        }
    }
}
