using System;
using System.Drawing;
using System.Media;

namespace Nave_B
{
    class Asteroide
    {
        public int Y { set; get; }
        public int X { set; get; }
        private int Ancho, Alto;
        private Matriz mr;
        private Matriz p1;
        private Matriz p2;
        private Matriz p3;
        private Matriz p4;
        private Random rand;
        int Vx = 15, countvel;
        private bool sentido_horario = true;
        private PointF[] puntos = new PointF[3];
        private SoundPlayer soundast;

        public Asteroide(int ancho, int alto) {
            this.X = ancho + 60;
            this.Y = alto / 2 ;
            this.Ancho = ancho;
            this.Alto = alto;
            mr = new Matriz(2,2);
            p1 = new Matriz(1, 2);
            p2 = new Matriz(p1);
            p3 = new Matriz(p1);
            p4 = new Matriz(p1);
            rand = new Random();
        }

        // metodo para rotar el objeto
        public void Rotar()
        {
            // matriz de puntos (1*2) por la matriz de rotacion (2*2)
            p1 = p1 * mr;
            p2 = p2 * mr;
            p3 = p3 * mr;
            p4 = p4 * mr;
        }

        // metodo para desplazarlo
        public void DesplazarAsteroide()
        {
            this.X -= Vx; //velocidad que se mueve en x hacia la izquierda

            if (this.X <= -100) // condicion de hasta donde llege el valox de x
            {
                int size = rand.Next(10, 150);
                this.X = this.Ancho + size;  //posicion inicial de x
                this.Y = rand.Next(this.Alto / 2 - size, this.Alto / 2 - size); // posicion de Y random" xD

                /* Carga el asteroide con un tamaño aleatoreo y la velocidad de giro en grados */
                CargarAsteroide(size, rand.Next(10, 65));
                soundast = new SoundPlayer();
                soundast.Stream = Properties.Resources.aste;
                soundast.Play();
                countvel++;
                if (countvel > 5)
                {                  //aumento de velocidad por cada 5 anteoides que pasen
                    Vx += 10;
                    countvel = 0;
                }
            }
        }

        private void CargarAsteroide(int size, int grados)
        {
            // -- tamaño de la imagen a mostrar con los 4 puntos el size representa el tamaño de la imagen --
            // primer punto 
            p1[0, 0] = -size; //x 
            p1[0, 1] = -size;//y 
            //segundo punto 
            p2[0, 0] = size; //x
            p2[0, 1] = -size;//y 
            //tercer punto 
            p4[0, 0] = size; //x
            p4[0, 1] = size;//y
            //cuarto punto 
            p3[0, 0] = -size; //x
            p3[0, 1] = size;//y

            //rotacion
            double radio = (grados * (Math.PI)) / 180;
            if (sentido_horario)
            {
                // lado al que girar! sentido antihorario .... falta que codificar!!! 
                mr = new Matriz(2, 2);
                mr[0, 0] = Math.Cos(radio);
                mr[0, 1] = Math.Sin(radio);
                mr[1, 0] = -Math.Sin(radio);
                mr[1, 1] = Math.Cos(radio);
                sentido_horario = false;
            } else {
                //sentido horario
                mr = new Matriz(2, 2);
                mr[0, 0] = Math.Cos(radio);
                mr[0, 1] = -Math.Sin(radio);
                mr[1, 0] = Math.Sin(radio);
                mr[1, 1] = Math.Cos(radio);
                sentido_horario = true;
            }
        }

        // metodo para dibujarlo
        public PointF[] getPuntos()
        {
            puntos[0] = new PointF((float)p1[0, 0], (float)p1[0, 1]); //primer punto
            puntos[1] = new PointF((float)p2[0, 0], (float)p2[0, 1]); //segundo punto
            puntos[2] = new PointF((float)p3[0, 0], (float)p3[0, 1]); //tercer punto
            return puntos;
        }
    }
}
