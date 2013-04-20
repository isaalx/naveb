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

        public Juego(BufferedGraphicsContext bcontext)
        {
            InitializeComponent();
            this.buffercontext = bcontext;
            nav = new Nave(20, Game.Height / 2);
            Fondo = new Rectangle(0,0,Game.Width,Game.Height);
            background = Properties.Resources.back;
            naves = Properties.Resources.nave;
            back_time.Enabled = true;
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
    }
}
