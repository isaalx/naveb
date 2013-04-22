using System;
using System.Drawing;
using System.Collections.Generic;

namespace Nave_B
{
    class Nave
    {
        private List<Rectangle> Naves;
        public int Y { set; get; }
        public int X { set; get; }
        public const int NORMAL = 0;
        public const int ARRIBA = 3;
        public const int ABAJO = 6;
        private int index = 0;
        private int contador = 0;
        private int nave = 0;

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
        }

        public Rectangle getNave() {
            nave = (nave < 3? nave + 1: 1);
            contador--;
            if (contador == 0) setDireccion(NORMAL);
            return Naves[index + nave-1];
        }

        public void setDireccion(int dir = NORMAL | ARRIBA | ABAJO){
            if (contador == 0) {
                index = dir;
                contador = 3;
            }
        }

        public void moverArriba()
        {
            this.Y -= 3;
            this.setDireccion(Nave.ARRIBA);
        }
        public void moverAbajo()
        {
            this.Y += 3;
            this.setDireccion(Nave.ARRIBA);
        }
        public void moverIzquierda()
        {
            this.X -= 3;
            this.setDireccion(Nave.ARRIBA);
        }
        public void moverDerecha()
        {
            this.X += 3;
            this.setDireccion(Nave.ARRIBA);
        }

        public void disparar() { 
        }

    }
}
