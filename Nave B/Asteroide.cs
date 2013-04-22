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
        Matriz mr;
        Matriz p1;
        Matriz p2;
        Matriz p3;
        Matriz p4;
        PointF punto1; //primer punto
        PointF punto2; //segundo punto
        PointF punto3; //tercer punto
        int contador = 1, sentido = 0, size = 50, countsize = 1, countgrade = 1, Vx = 15, countvel;
        double grado = 18;
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

        //metodo para cambiar size
        public int RandomSize()
        {
            switch (countsize)
            {
                case 1:
                    size = 75;
                    countsize++;
                    break;
                case 2:
                    size = 25;
                    countsize++;
                    break;
                case 3:
                    size = 35;
                    countsize++;
                    break;
                case 4:
                    size = 100;
                    countsize++;
                    break;
                case 5:
                    size = 10;
                    countsize++;
                    break;
                case 6:
                    size = 150;
                    countsize++;
                    break;
                case 7:
                    size = 60;
                    countsize++;
                    break;
                case 8:
                    size = 20;
                    countsize++;
                    break;
                case 9:
                    size = 45;
                    countsize = 1;
                    break;
            }

            return size;
        }

        //metodo para generar la velocidad de giro de los asteroides
        public double RandomGirar()
        {
            switch (countgrade)
            {
                case 1:
                    grado = 20;
                    countgrade++;
                    break;
                case 2:
                    grado = 65;
                    countgrade++;
                    break;
                case 3:
                    grado = 10;
                    countgrade++;
                    break;
                case 4:
                    grado = 30;
                    countgrade++;
                    break;
                case 5:
                    grado = 15;
                    countgrade++;
                    break;
                case 6:
                    grado = 55;
                    countgrade++;
                    break;
                case 7:
                    grado = 60;
                    countgrade = 1;
                    break;
            }

            return grado;
        }

        //para generar los puntos de donde inicien los asteroides 
        public int RandomAsteroide()
        {
            int y = 0;
            switch (contador)
            {
                case 1:
                    y = this.Alto / 2 + 50;
                    contador++;
                    break;
                case 2:
                    y = this.Alto / 2 - 160;
                    contador++;
                    break;
                case 3:
                    y = this.Alto / 2 + 180;
                    contador++;
                    break;
                case 4:
                    y = this.Alto / 2 - 50;
                    contador++;
                    break;
                case 5:
                    y = this.Alto / 2 + 90;
                    contador++;
                    break;
                case 6:
                    y = this.Alto / 2 - 120;
                    contador++;
                    break;
                case 7:
                    y = this.Alto / 2 + 40;
                    contador++;
                    break;
                case 8:
                    y = this.Alto / 2 - 35;
                    contador++;
                    break;
                case 9:
                    y = this.Alto / 2 + 60;
                    contador++;
                    break;
                case 10:
                    y = this.Alto / 2 - 90;
                    contador++;
                    break;
                case 11:
                    y = this.Alto / 2 + 200;
                    contador++;
                    break;
                case 12:
                    y = this.Alto / 2 - 220;
                    contador = 1;
                    break;
            }
            return y;
        }

        // metodo para desplazarlo
        public void DesplazarAsteroide()
        {
            this.X -= Vx;                          //velocidad que se mueve en x hacia la izquierda
            if (this.X <= -100)
            {                      // condicion de hasta donde llege el valox de x
                this.X = this.Ancho + 70;           //posicion inicial de x
                this.Y = RandomAsteroide();        //posicion de y "random" xD
                RandomSize();                    //para crear diferentes tamaños en cada vuelta
                RandomGirar();                   // para crear diferentes grados de giro en cada vuelta
                CargarAsteroide();     //volver a cargar desde otro punto inicial
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

        

        //  codigo del asteroide ------ marvin --------------- aun falta modificar esta parte!! 
        private void CargarAsteroide()//, int sentido, double grado)
        {
            // -------------- tamaño de la imagen a mostrar con los 4 puntos el size representa el tamaño de la imagen
            //primer punto 
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
            double radio = (grado * (Math.PI)) / 180;
            if (sentido == 0)
            {
                // lado al que girar! sentido antihorario .... falta que codificar!!! 
                mr = new Matriz(2, 2);
                mr[0, 0] = Math.Cos(radio);
                mr[0, 1] = Math.Sin(radio);
                mr[1, 0] = -Math.Sin(radio);
                mr[1, 1] = Math.Cos(radio);
                sentido = 1;
            }
            else if (sentido == 1)
            {
                //sentido horario
                mr = new Matriz(2, 2);
                mr[0, 0] = Math.Cos(radio);
                mr[0, 1] = -Math.Sin(radio);
                mr[1, 0] = Math.Sin(radio);
                mr[1, 1] = Math.Cos(radio);
                sentido = 0;
            }
        }

        // metodo para dibujarlo
        public PointF[] getPuntos()
        {
            PointF[] puntos = new PointF[3];
                punto1 = new PointF((float)p1[0, 0], (float)p1[0, 1]); //primer punto
                punto2 = new PointF((float)p2[0, 0], (float)p2[0, 1]); //segundo punto
                punto3 = new PointF((float)p3[0, 0], (float)p3[0, 1]); //tercer punto
                return new PointF[]{punto1,punto2,punto3};
        }
    }
}
