using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
        private Asteroide ast;
        private Rectangle Fondo;
        private Bitmap background;
        private Bitmap naves;
        private Bitmap asteroide;
        private BufferedGraphicsContext buffercontext;
        private BufferedGraphics buffer;
        private SoundPlayer navesound;

            
            
        public Juego(BufferedGraphicsContext bcontext)
        {
            InitializeComponent();
            this.buffercontext = bcontext;
            nav = new Nave(20, Game.Height / 2);
            Fondo = new Rectangle(0,0,Game.Width,Game.Height);
            background = Properties.Resources.back;
            naves = Properties.Resources.nave;
            asteroide = Properties.Resources.asteroide;
            back_time.Enabled = true;
            ast = new Asteroide(Game.Width, Game.Height);
            //sonido de asteroide
            navesound = new SoundPlayer();
            navesound.Stream = Properties.Resources.naveintro;
            navesound.Play();
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

        private void back_time_Tick(object sender, EventArgs e)
        {
            buffer = buffercontext.Allocate(Game.CreateGraphics(), Game.DisplayRectangle);
            fondo();
            buffer.Graphics.DrawImage(naves, nav.X, nav.Y, nav.getNave(), GraphicsUnit.Pixel);
            buffer.Graphics.TranslateTransform(ast.X, ast.Y);
            ast.Rotar();
            buffer.Graphics.DrawImage(asteroide, ast.getPuntos()); //dibuja el bitmap , especificando un arreglo de 3 puntos
            ast.DesplazarAsteroide();
            buffer.Graphics.TranslateTransform(-ast.X, -ast.Y);
            // No colocar codigo para dibujar despues de aqui.
            buffer.Render();
            buffer.Dispose();
        }
            
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Left)
            {
                nav.moverIzquierda();
                return true;
            }
            else if (keyData == Keys.Right)
            {
                nav.moverDerecha();
                return true;
            }
            else if (keyData == Keys.Up)
            {
                nav.moverArriba();
                return true;
            }
            else if (keyData == Keys.Down)
            {
                nav.moverAbajo();
                return true;
            }
            else if (keyData == Keys.X)
            {
                nav.disparar();
                return true;
            }
            else
                return base.ProcessCmdKey(ref msg, keyData);
        }

        
        
        

    }
}
