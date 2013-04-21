using System;
using System.Collections.Generic;
using System.Text;


    class Matriz
    {
        //Campos...
        
        private int nc, nf;
        private Matriz INV;
        private double[,] arr;
        private double det;



        //Constructores...

        public Matriz(int numfilas, int numcolumnas) {

            redimensionar(numfilas, numcolumnas);

        
        }


        public Matriz(Matriz m) {
            redimensionar(m.nfilas, m.ncolumnas);

            copiar(m);

        }


        //Destructor...

    ~ Matriz() {
        nf = 0;
        nc = 0;
        arr = null;
        INV = null;
    }


        //Properties...

        public int nfilas {
            get { return nf; }
            set { nf = value; }
        }

        public int ncolumnas {
            get { return nc; }
            set { nc = value; }
        
        }


        public double this[int fila, int columna]{

            get { return arr[fila, columna]; }
            set { arr[fila, columna] = value; }
                         
    
        }


        public double determinante {
            get { intercambio_Jordan();
                  return det;
            }
        }

        


        //Metodos 


        public void redimensionar(int numfilas, int numcolumnas)
        {

            nf = numfilas;
            nc = numcolumnas;
            arr = new double[nf, nc];

        }

        
        public void copiar(Matriz m, int fi, int ci) {
            int f,c, maxf, maxc;

            maxf = (this.nfilas >= m.nfilas + fi) ? m.nfilas + fi : this.nfilas ;

            maxc = (this.ncolumnas >= m.ncolumnas + ci) ? m.ncolumnas + ci : this.ncolumnas ;

            for (f = fi; f <= maxf; f++) {

                for (c = ci; c <= maxc; c++) {
                    arr[f, c] = m[f - fi, c - ci];
            }
            }
            
        }

        public void copiar(Matriz m) {
            copiar(m, 0, 0);

        }


        public Matriz pivotear(Matriz mat, int r, int s)
        {


            Matriz A = new Matriz(mat.nfilas , mat.ncolumnas );

            for (int i = 0; i < mat.nfilas ; i++)
            {
                for (int j = 0; j < mat.ncolumnas ; j++)
                {

                    if ((r != i) && (j != s))
                        A[i, j] = mat[i, j] - mat[i, s] * mat[r, j] / mat[r, s];

                    if ((r == i) && (j != s))
                        A[i, j] = mat[i, j] / mat[r, s];

                    if ((r != i) && (j == s))
                        A[i, j] = -(mat[i, j] / mat[r, s]);

                    if ((r == i) && (j == s))
                        A[i, j] = (1 / mat[r, s]);
                }

            }

            return A;

        }


        private bool intercambio_Jordan()
        {

            int orden;
            double d=1;
            bool pendiente = false;
            bool pivoteo = false;

            orden = this.nfilas;


            bool[] pivs = new bool[orden];
            INV = new Matriz(orden, orden);


            INV = this.Copia();


            for (int x = 0; x < orden; x++)
            {

                if (INV[x, x] == 0)
                    pivs[x] = false;
                else
                {
                    pivs[x] = true;
                    d *= INV[x,x];
                    INV = pivotear(INV, x, x);
                }
            }

            do
            {
                pivoteo = false;
                pendiente = false;

                for (int y = 0; y < orden; y++)
                {
                    if (pivs[y] == false)
                    {

                        if (INV[y, y] != 0)
                        {
                            pivs[y] = true;
                            d *= INV[y, y];
                            INV = pivotear(INV, y, y);
                            pivoteo = true;
                        }
                        else
                        {
                            pendiente = true;
                        }
                    }

                }

            } while (pivoteo);

            if (pendiente == true)
            {
                det = 0;
                return false;
            }
            else
            {
                det = d;
                return true;
            }


        }


        public bool invertirme()
        {

            if (this.nfilas != this.ncolumnas)
                return false;
            else {

                bool b;

                b= intercambio_Jordan();

                if (b){
                for (int f = 0; f < INV.nfilas; f++)
                {
                    for (int c = 0; c < INV.ncolumnas; c++)
                    {
                        this[f, c] = INV[f, c];
                    }
                }
                   
                }

                return b;

            }
            
            
        }


        public Matriz Copia()
        {

            Matriz C = new Matriz(this.nfilas, this.ncolumnas);

            for (int f = 0; f < this.nfilas; f++)
            {

                for (int c = 0; c < this.ncolumnas; c++)
                {

                    C[f, c] = this[f, c];

                }

            }

            return C;

        }

        
        //Sobrecarga de Operadores


        //Unarios 

        public static Matriz operator -(Matriz A) {

            return -1 * A;
       }

        public static Matriz operator +(Matriz A) {
            return A;
        }


        //Binarios - Aritmeticos 


        public static Matriz operator +(Matriz A, Matriz B) {

            Matriz res = new Matriz(A.nfilas, A.ncolumnas);

            if ((A.nfilas == B.nfilas) && (A.ncolumnas == B.ncolumnas))
            {

                for (int i = 0; i < A.nfilas; i++)
                {
                    for (int j = 0; j < A.ncolumnas; j++)
                    {

                        res[i, j] = A[i, j] + B[i, j];


                    }

                }

                return res;


            }
            else
                return null;
            
        
        }

        public static Matriz operator -(Matriz A, Matriz B) {

            Matriz res = new Matriz(A.nfilas, A.ncolumnas);

            if ((A.nfilas == B.nfilas) && (A.ncolumnas == B.ncolumnas))
            {

                for (int i = 0; i < A.nfilas; i++)
                {
                    for (int j = 0; j < A.ncolumnas; j++)
                    {

                        res[i, j] = A[i, j] - B[i, j];


                    }

                }

                return res;


            }
            else
                return null;
        }

        public static Matriz operator *(Matriz A, Matriz B)
        {
            if (A.ncolumnas != B.nfilas)
                return null;

            Matriz N = new Matriz(A.nfilas, B.ncolumnas);

            for (int f = 0; f < A.nfilas; f++)
            {
                for (int c = 0; c < B.ncolumnas; c++)
                {

                    for (int x = 0; x < A.ncolumnas; x++)
                    {
                        N[f, c] += A[f, x] * B[x, c];
                    }

                }
            }

            return N;

        }

        public static Matriz operator *(double d, Matriz A) {

            Matriz res = new Matriz(A.nfilas, A.ncolumnas);

            for (int i=0; i < res.nfilas; i++)
            {
                for (int j=0; j < res.ncolumnas; j++) { 
                
                

                res[i, j] = A[i, j] * d; 

                    }
            }
            return res;

        }

        public static Matriz operator *(Matriz A, double d) {

            return d * A;

        }

        public  static Matriz  operator / (Matriz A, double d){

            return (1 / d) * A;
            
        }


        //Binarios - Logicos 


        public static bool operator ==(Matriz A, Matriz B) {
            bool diferente=false;

            if ((A.nfilas == B.nfilas ) && ( A.ncolumnas == B.ncolumnas )){
            
                for (int i= 0; i< A.nfilas ; i ++){
                    for (int j = 0; j < A.ncolumnas; j++)
                    {
                        if (A[i, j] != B[i, j])
                            diferente = true;
                    }
                }
            }
            else
            return false;



            if (diferente)
                return false;
            else
                return true;
            
         }


        public static bool operator !=(Matriz A, Matriz B) {

            return !(A == B);
        }








    }

