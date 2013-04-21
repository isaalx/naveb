using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nave_B
{
    public partial class Juego : UserControl
    {
        private Nave nav;
        private Rectangle Fondo;
        private Bitmap background;
        private Bitmap naves;
        private BufferedGraphicsContext buffercontext;
        private BufferedGraphics buffer;
        //---------------------------para la rotacion de asteroides y desplazamiento
        Matriz mr = new Matriz(2, 2);
        Matriz p1 = new Matriz(1, 2);
        Matriz p2 = new Matriz(1, 2);
        Matriz p3 = new Matriz(1, 2);
        Matriz p4 = new Matriz(1, 2);
        Graphics ast;
        Bitmap asteroide;
        int posx, posy, y, contador = 1,sentido=0, size=50,countsize=1,countgrade=1, Vx=15, countvel; 
        double grado=18;
       

        public Juego(BufferedGraphicsContext bcontext)
        {
            InitializeComponent();
            this.buffercontext = bcontext;
            nav = new Nave(20, Game.Height / 2);
            Fondo = new Rectangle(0,0,Game.Width,Game.Height);
            background = Properties.Resources.back;
            naves = Properties.Resources.nave;
            back_time.Enabled = true;
            //cargar el asteroide ------------------------ marvin 
            try {
                asteroide = new Bitmap("asteroide.png");
            }catch(Exception e){
                MessageBox.Show("No se encuentra el archivo asteroide.png");
            }
            posx = Game.Width + 60;
            posy = Game.Height / 2;
            CargarAsteroide(posx, posy);
                     
        }

        private void fondo(){
            buffer.Graphics.DrawImage(background, 0, 0, Fondo, GraphicsUnit.Pixel);

            // Despues arreglo lo del fondo que se desfasa
            int division = (background.Width - Fondo.X);
            if (division < Game.Width)
            {
                Fondo.Width = division;
                buffer.Graphics.DrawImage(background, division, 0, new Rectangle(0, 0, Game.Width - division, Game.Height), GraphicsUnit.Pixel);
            }
            Fondo.X += 5; // velociadad del fondo
            if (Fondo.X > background.Width)
            {
                Fondo.X = 0;
                Fondo.Width = Game.Width;
            }
        }

        private void moverArriba()
        {
            nav.Y -= 3;
            nav.setDireccion(Nave.ARRIBA);
        }
        private void moverAbajo()
        {
            nav.Y += 3;
            nav.setDireccion(Nave.ARRIBA);
        }
        private void moverIzquierda()
        {
            nav.X -= 3;
            nav.setDireccion(Nave.ARRIBA);
        }
        private void moverDerecha()
        {
            nav.X += 3;
            nav.setDireccion(Nave.ARRIBA);
        }

        private void back_time_Tick(object sender, EventArgs e)
        {
            buffer = buffercontext.Allocate(Game.CreateGraphics(), Game.DisplayRectangle);
            fondo();
            buffer.Graphics.DrawImage(naves, nav.X, nav.Y, nav.getNave(), GraphicsUnit.Pixel);
            buffer.Render();
            buffer.Dispose();
            Rotar();
            DibujarAsteroide();
            DesplazarAsteroide();            
        }
            
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Left)
            {
                moverIzquierda();
                return true;
            }
            else if (keyData == Keys.Right)
            {
                moverDerecha();
                return true;
            }
            else if (keyData == Keys.Up)
            {
                moverArriba();
                return true;
            }
            else if (keyData == Keys.Down)
            {
                moverAbajo();
                return true;
            }
            else
                return base.ProcessCmdKey(ref msg, keyData);
        }

        //  codigo del asteroide ------ marvin --------------- aun falta modificar esta parte!! 
        private void CargarAsteroide(int x, int y)//, int sentido, double grado)
        {
            ast = Game.CreateGraphics();
            //posicion donde aparecera el asteroide ---------------- falata cambiarle valores
            ast.TranslateTransform(x, y);

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
            if (sentido == 0){
                    // lado al que girar! sentido antihorario .... falta que codificar!!! 
                    mr = new Matriz(2, 2);
                    mr[0, 0] = Math.Cos(radio);
                    mr[0, 1] = Math.Sin(radio);
                    mr[1, 0] = -Math.Sin(radio);
                    mr[1, 1] = Math.Cos(radio);
                    sentido =1;
            }else if (sentido == 1){
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
        private void DibujarAsteroide() {
            try
            {
                PointF[] puntos = new PointF[3];
                puntos[0] = new PointF(float.Parse(p1[0, 0].ToString()), float.Parse(p1[0, 1].ToString())); //primer punto
                puntos[1] = new PointF(float.Parse(p2[0, 0].ToString()), float.Parse(p2[0, 1].ToString())); //segundo punto
                puntos[2] = new PointF(float.Parse(p3[0, 0].ToString()), float.Parse(p3[0, 1].ToString())); //tercer punto
                ast.DrawImage(asteroide, puntos); //dibuja el bitmap , especificando un arreglo de 3 puntos
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al redibujar asteroide");
            }
        }
        // metodo para rotar el objeto
        private void Rotar() {
            // matriz de puntos (1*2) por la matriz de rotacion (2*2)
            p1 = p1 * mr;
            p2 = p2 * mr;
            p3 = p3 * mr;
            p4 = p4 * mr;
        }
        //metodo para cambiar size
        public int RandomSize() {
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
                    countsize=1;
                    break;
            }

            return size;
        }
        //metodo para generar la velocidad de giro de los asteroides
        public double RandomGirar() {
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
                    grado =60;
                    countgrade=1;
                    break;
            }

            return grado;
        }
        //para generar los puntos de donde inicien los asteroides 
        public int RandomAsteroide() {
             switch (contador)
             {
                case 1:
                    y = Game.Height / 2 + 50;
                    contador++;
                    break;
                case 2:
                    y = Game.Height / 2 - 160;
                    contador++;
                    break;
                case 3:
                    y = Game.Height / 2 + 180;
                    contador++;
                    break;
                case 4:
                    y = Game.Height / 2 - 50;
                    contador++;
                    break;
                case 5:
                    y = Game.Height / 2 + 90;
                    contador++;
                    break;
                case 6:
                    y = Game.Height / 2 - 120;
                    contador++;
                    break;
                case 7:
                    y = Game.Height / 2 + 40;
                    contador++;
                    break;
                case 8:
                    y = Game.Height / 2  - 35;
                    contador++;
                    break;
                case 9:
                    y = Game.Height / 2 + 60;
                    contador++;
                    break;
                case 10:
                    y = Game.Height / 2 - 90;
                    contador++;
                    break;
                case 11:
                    y = Game.Height / 2 + 200;
                    contador++;
                    break;
                case 12:
                    y = Game.Height / 2 - 220;
                    contador=1;
                    break;
                }
            return y;
        }
        // metodo para desplazarlo 
        public void DesplazarAsteroide() {
           posx -= Vx;                          //velocidad que se mueve en x hacia la izquierda
           ast = Game.CreateGraphics();         //crear el mapa de bits
           ast.TranslateTransform(posx, posy);  // crear la nueva posicion 
           if (posx<=-100){                      // condicion de hasta donde llege el valox de x
               posx = Game.Width +70;           //posicion inicial de x
               posy = RandomAsteroide();        //posicion de y "random" xD
               RandomSize();                    //para crear diferentes tamaños en cada vuelta
               RandomGirar();                   // para crear diferentes grados de giro en cada vuelta
               CargarAsteroide(posx, posy);     //volver a cargar desde otro punto inicial
               countvel++;
               if(countvel>5){                  //aumento de velocidad por cada 5 anteoides que pasen
                   Vx += 10;
                   countvel = 0;
               }
           }
        }

    }
}
