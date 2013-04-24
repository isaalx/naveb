using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Nave_B
{
    public partial class Juego : UserControl
    {
        private Nave nav;
        private List<Asteroide> Asteroides;
        private Rectangle Fondo;
        private Bitmap background;
        private BufferedGraphicsContext buffercontext;
        private BufferedGraphics buffer;
        private int ticks = 0, puntos = 0, prenivel = 0, nivel = 0;
        private GraphicsPath BordesNave;
        private SoundPlayer soundast;
        private Main Principal;
        private int asti = 0;
        private bool asts = true;

        public Juego(BufferedGraphicsContext bcontext, Main principal)
        {
            InitializeComponent();
            this.Principal = principal;
            this.buffercontext = bcontext; // BufferContext desde el Frame
            Fondo = new Rectangle(0,0,Game.Width,Game.Height); 
            background = Properties.Resources.back; // imagen de Fondo
            back_time.Enabled = true; // timer (es como un while infinito que se encarga de realizar los cambios)
            Asteroides = new List<Asteroide>();
            control.Focus();
            cargarAsteroides();
            BordesNave = new GraphicsPath();
            BordesNave.AddRectangle(new Rectangle(20, 0, Game.Width * 3 / 4, 2)); // Arriba
            BordesNave.AddRectangle(new Rectangle(20, 0, 2, Game.Height - 20)); // Izquierda
            BordesNave.AddRectangle(new Rectangle(20, Game.Height - 20, Game.Width * 3 / 4, 2)); // Abajo
            BordesNave.AddRectangle(new Rectangle(20 + Game.Width * 3 / 4, 0, 2, Game.Height - 20)); // Derecha 
            nav = new Nave(20, Game.Height / 2); // Posicion inicial de la nave
            soundast = new SoundPlayer();
            soundast.Stream = Properties.Resources.naveintro;
            soundast.Play();
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

        private void cargarAsteroides() {
            asti = (asti < 3 ? asti : 0);
            asts = !asts;
            Asteroides.Add(new Asteroide(Game.Width, Game.Height, 15, asti++, asts));
            soundast = new SoundPlayer();
            soundast.Stream = Properties.Resources.aste;
            soundast.Play();
        }

        private void back_time_Tick(object sender, EventArgs e)
        {
            /* Dimesiones del buffer a utilizar. Ancho y alto del PictureBox */ 
            buffer = buffercontext.Allocate(Game.CreateGraphics(), Game.DisplayRectangle);
            /* Dibuja el fondo */ 
            fondo();
            /* Cargar la nave */
            nav.getPosision();
            /* Dibuja cada disparo */
            for (int i = 0; i < nav.Disparos.Count; i++) {
                buffer.Graphics.FillPath(nav.Disparos[i].ColorD, nav.Disparos[i].getPath());
                if (nav.Disparos[i].X > Game.Width) nav.Disparos.RemoveAt(i);
            }
            /*Dibuja el asteroides */
            for (int j = 0; j < Asteroides.Count; j++)
            {
                int tmpx = Asteroides[j].X;
                int tmpy = Asteroides[j].Y;
                buffer.Graphics.TranslateTransform(tmpx, tmpy);
                buffer.Graphics.DrawImage(Asteroides[j].getImg(), Asteroides[j].getPuntos()); //dibuja el bitmap , especificando un arreglo de 3 puntos
                buffer.Graphics.TranslateTransform(-tmpx, -tmpy);
                
                if (Asteroides[j].X < -Asteroides[j].Size)
                {
                    Asteroides.RemoveAt(j--);
                }
                else
                {
                    for (int k = 0; k < nav.Disparos.Count; k++)
                    {
                        Region r = new Region(Asteroides[j].getPath());
                        r.Intersect(nav.Disparos[k].getPath());
                        if (!r.IsEmpty(buffer.Graphics))
                        {
                            Asteroides[j].destruir();
                            nav.Disparos.RemoveAt(k--);
                            puntos += 10;
                            if (Asteroides[j].Size < 15)
                            {
                                Asteroides.RemoveAt(j--);
                                break;
                            }
                        }
                    }
                }
            }
            /* Cargar la nave */
            buffer.Graphics.DrawImage(nav.getImg(), nav.X, nav.Y, nav.getNave(), GraphicsUnit.Pixel);
            for (int j = 0; j < Asteroides.Count; j++)
            {
                Region r1 = new Region(Asteroides[j].getPath());
                r1.Intersect(nav.getPath());
                if (!r1.IsEmpty(buffer.Graphics))
                {
                    nav.Destruir();
                }
            }
            /* No colocar codigo para dibujar despues de aqui. */
            Puntos();
            buffer.Render();
            buffer.Dispose();
        }
            
        private void Puntos(){
            ticks++;
            if (ticks > 50)
            {
                ticks = 0;
                puntos+=10;
                cargarAsteroides();
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

        private void button1_Click(object sender, EventArgs e)
        {
            Principal.Volver();
        }

    }
}
