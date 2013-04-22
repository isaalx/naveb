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
        private Asteroide ast;
        private Rectangle Fondo;
        private Bitmap background;
        private Bitmap naves;
        private Bitmap asteroide;
        private BufferedGraphicsContext buffercontext;
        private BufferedGraphics buffer;
        private int ticks = 0, puntos = 0, prenivel = 0, nivel = 0;

        public Juego(BufferedGraphicsContext bcontext)
        {
            InitializeComponent();
            this.buffercontext = bcontext; // BufferContext desde el Frame
            nav = new Nave(20, Game.Height / 2); // Posicion inicial de la nave
            Fondo = new Rectangle(0,0,Game.Width,Game.Height); 
            background = Properties.Resources.back; // imagen de Fondo
            naves = Properties.Resources.nave; // sprites de la nave
            asteroide = Properties.Resources.asteroide; // imagen de asteroide
            back_time.Enabled = true; // timer (es como un while infinito que se encarga de realizar los cambios)
            ast = new Asteroide(Game.Width, Game.Height); // Espacio que el asteroide tiene para moverse
            control.Focus();
        }

        private void fondo(){
            /* Dibuja el Fondo */
            buffer.Graphics.DrawImage(background, 0, 0, Fondo, GraphicsUnit.Pixel);

            //TODO: Despues arreglo lo del fondo que se desfasa
            /* Verificamos si el fondo ha llegado a su fin y redibujamos la otra parte desde el principio */
            int division = (background.Width - Fondo.X);
            if (division < Game.Width)
            {
                Fondo.Width = division;
                buffer.Graphics.DrawImage(background, division, 0, new Rectangle(0, 0, Game.Width - division, Game.Height), GraphicsUnit.Pixel);
            }

            /* Velocidad de desplazamiento del fondo
             * Aumenta con el tiempo de juego
             */
            Fondo.X += (2 + prenivel);
            if (Fondo.X > background.Width)
            {
                Fondo.X = 0;
                Fondo.Width = Game.Width;
            }
        }

        private void back_time_Tick(object sender, EventArgs e)
        {
            /* Dimesiones del buffer a utilizar. Ancho y alto del PictureBox */ 
            buffer = buffercontext.Allocate(Game.CreateGraphics(), Game.DisplayRectangle);
            /* Dibuja el fondo */ 
            fondo();
            /* Dibuja la nave */
            nav.getPosision();
            buffer.Graphics.DrawImage(naves, nav.X, nav.Y, nav.getNave(), GraphicsUnit.Pixel);
            for (int i = 0; i < nav.Disparos.Count; i++) {
                buffer.Graphics.FillPath(nav.Disparos[i].ColorD, nav.Disparos[i].getPath());
            }
            /*Dibuja el asteroide */
            buffer.Graphics.TranslateTransform(ast.X, ast.Y);
            ast.Rotar();
            buffer.Graphics.DrawImage(asteroide, ast.getPuntos()); //dibuja el bitmap , especificando un arreglo de 3 puntos
            ast.DesplazarAsteroide();
            buffer.Graphics.TranslateTransform(-ast.X, -ast.Y);

            /* No colocar codigo para dibujar despues de aqui. */
            Puntos();
            buffer.Render();
            buffer.Dispose();
        }
            
        private void Puntos(){
            ticks++;
            if (ticks > 100)
            {
                ticks = 0;
                puntos+=10;
            }
            if (puntos != 0 && puntos%1000 == 0)
            {
                prenivel++;
            }
            if (prenivel > 100)
            {
                prenivel = 0;
                nivel++;
            }
            lpuntos.Text = "Puntos: " + puntos;
        }

        private void control_KeyDown(object sender, KeyEventArgs e)
        {
            nav.mover(e.KeyCode);
            control.Text = "";
        }

        private void control_KeyUp(object sender, KeyEventArgs e)
        {
            nav.detener(e.KeyCode);
            control.Text = "";
        }

    }
}
