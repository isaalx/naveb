using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Media;

namespace Nave_B
{
    class Nave
    {
        private List<Rectangle> Naves;
        public List<Disparo> Disparos { set; get; }
        public int Y { set; get; }
        public int X { set; get; }
        public const int NORMAL = 0;
        public const int ARRIBA = 3;
        public const int ABAJO = 6;
        private bool ar = false, ab = false, iz = false, de = false;
        private int dir = 0;
        private int nave = 0;
        private SoundPlayer disparo;

        public Nave() : this (0,0){ 
        }

        public Nave(int x, int y) {
            this.X = x;
            this.Y = y;
            Naves = new List<Rectangle>();
            Naves.Add(new Rectangle(0, 42, 43, 39));
            Naves.Add(new Rectangle(46, 42, 43, 39));
            Naves.Add(new Rectangle(91, 42, 43, 39));
            Naves.Add(new Rectangle(0, 0, 43, 35));
            Naves.Add(new Rectangle(46, 0, 43, 35));
            Naves.Add(new Rectangle(91, 0, 43, 35));
            Naves.Add(new Rectangle(0, 89, 43, 31));
            Naves.Add(new Rectangle(46, 89, 43, 31));
            Naves.Add(new Rectangle(91, 89, 43, 31));
            Disparos = new List<Disparo>();
        }

        public Rectangle getNave() {
            nave = (nave < 3 ? nave : 0);
            return Naves[dir + nave++];
        }

        public void setDireccion(int dir = NORMAL | ARRIBA | ABAJO){
                this.dir = dir;
        }

        public void getPosision() {
            if (ar) {
                this.Y -= 3;
                this.setDireccion(Nave.ARRIBA);
            }
            if (ab)
            {
                this.Y += 3;
                this.setDireccion(Nave.ABAJO);
            }
            if (iz)
            {
                this.X -= 3;
                this.setDireccion(Nave.NORMAL);
            }
            if (de)
            {
                this.X += 3;
                this.setDireccion(Nave.NORMAL);
            }
        }

        public void mover(Keys tecla)
        {
            switch (tecla)
            {
                case Keys.Up:
                    ar = true;
                    break;
                case Keys.Down:
                    ab = true;
                    break;
                case Keys.Left:
                    iz = true;
                    break;
                case Keys.Right:
                    de = true;
                    break;
                case Keys.X:
                    
                    disparar();
                    disparo=new SoundPlayer();
                    disparo.Stream = Properties.Resources.disp;
                    disparo.Play();
                    break;
            }
        }

        public void detener(Keys tecla)
        {
            switch (tecla)
            {
                case Keys.Up:
                    ar = false;
                    break;
                case Keys.Down:
                    ab = false;
                    break;
                case Keys.Left:
                    iz = false;
                    break;
                case Keys.Right:
                    de = false;
                    break;
            }
        }

        public void disparar() {
            nave = (nave < 3 ? nave : 0);
            int lx = this.X + (Naves[dir + nave].Width);
            int ly = this.Y + (Naves[dir + nave].Height / 2 );
            Disparos.Add(new Disparo(lx, ly, 10, 2, Color.Yellow));
        }

    }
}
