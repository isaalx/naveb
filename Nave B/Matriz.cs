using System;

namespace Nave_B
{
	public class Matriz
	{
		private int filas;
		private int columnas;
		private double[,] datos;

		public Matriz ()
		{
			this.filas = 0;
			this.columnas = 0;
			this.datos = new double[filas,columnas];
		}

		public Matriz(int filas, int columnas)
			: this (filas, columnas, 0)
		{
		}
		
        public Matriz(Matriz m)
		{
            this.filas = m.Filas;
            this.columnas = m.Columnas;
			this.datos = new double[filas,columnas];
			for (int i = 0; i < filas; i++)
			{
				for (int j = 0; j < columnas; j++)
				{
					this.datos[i,j] = m[i,j];
				}
			}
		}

		public Matriz (int filas, int columnas, double valor)
		{
			this.filas = filas;
			this.columnas = columnas;
			this.datos = new double[filas,columnas];

			for (int i = 0; i < filas; i++)
			{
				for (int j = 0; j < columnas; j++)
				{
					this.datos[i,j] = valor;
				}
			}
		}

		public int Filas {
			get {
				return filas;
			}
			set {
				filas = value;
			}
		}

		public int Columnas {
			get {
				return columnas;
			}
			set {
				columnas = value;
			}
		}

		public double this[int x,int y] {
			get {
				return datos[x,y];
			}
			set {
				datos[x,y] = value;
			}
		}

		public bool esCuadrada()
		{
			return (Filas == Columnas);
		}
		
		public bool esSumable(Matriz m)
		{
			return (Filas == m.Filas && Columnas == m.Columnas);
		}
		
		public bool esMultiplicable(Matriz m)
		{
			return (m.Filas == Columnas);
		}

		public static Matriz operator *(Matriz a, double b)
		{
			Matriz resultado = new Matriz(a.Filas, a.Columnas);
			for (int i = 0; i < a.Filas; i++)
			{
				for (int j = 0; j < a.Columnas; j++)
				{
					resultado[i,j] = a[i,j] * b;
				}
			}
			return resultado;
		}

		public static Matriz operator * (double a, Matriz b)
		{
			return b * a;
		}

		public static Matriz operator * (Matriz a, Matriz b)
		{
			if (a.esMultiplicable (b))
			{
				Matriz resultado = new Matriz (a.Filas, b.Columnas);

				for (int i = 0; i < a.Filas; i++)
				{
					for (int j = 0; j < b.Columnas; j++)
					{
						double respuesta = 0;
						for (int k = 0; k < a.Columnas; k++) {
							respuesta += a[i,k] * b[k,j];
						}
						resultado[i,j] = respuesta;
					}
				}
				return resultado;
			} else {
				throw new System.InvalidOperationException("El numero de columnas de la matriz A debe coincidir con el numero de filas de la matriz B.");
			}
		}

		public static Matriz operator -(Matriz a)
		{
			return a * -1;
		}

		public static Matriz operator +(Matriz a, Matriz b)
		{
			if (a.esSumable(b))
			{
				Matriz resultado = new Matriz(a.Filas, a.Columnas);
				for (int i = 0; i < a.Filas; i++)
				{
					for (int j = 0; j < a.Columnas; j++)
					{
						resultado[i,j] = a[i,j] + b[i,j];
					}
				}
				return resultado;
			}
			else
			{
				throw new System.InvalidOperationException("Las matrices deben ser de la misma dimension.");
			}
		}

		public static Matriz operator + (Matriz a, double b)
		{
			return a + new Matriz(a.Filas, a.Columnas, b);
		}

		public static Matriz operator + (double a, Matriz b)
		{
			return b + a;
		}

		public static Matriz operator -(Matriz a, Matriz b)
		{
			return a + (-b);
		}

		public static Matriz operator -(Matriz a, double b)
		{
			return a - new Matriz(a.Filas, a.Columnas, b);
		}
		
		public static Matriz operator -(double a, Matriz b)
		{
			return -b + a;
		}

		public override int GetHashCode()        
		{            
			return base.GetHashCode();
		}         
		
		public override bool Equals (object obj)
		{
			if (obj == null) return false;             
			
			if (GetType() != obj.GetType())	return false; 

			Matriz b = (Matriz)obj;

			if (!esSumable(b)) return false;
			
			for (int i = 0; i < this.Filas; i++) {
				for (int j = 0; j < this.Columnas; j++) {
					if(this[i,j] != b[i,j]) return false;
				}
			}
			
			return true;
			
		}

		public static bool operator == (Matriz a, Matriz b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(Matriz a, Matriz b)
		{
			return !(a.Equals(b));
		}

		private static void inv_jordan (Matriz R, Matriz A, int r)
		{
			for(int i = 0; i < A.Filas; i++)
			{
				for(int j = 0; j < A.Columnas; j++)
				{
					R[i,j] = 
						( i != r && j != r ? A[i,j] - (A[i,r]*A[r,j])/A[r,r]:
						 ( i == r && j != r ? A[i,j]/A[r,r] : 
						 ( i != r && j == r ? -A[i,j]/A[r,r] : 
						 1/A[r,r] )));
				}
			}
		}

		public static Matriz operator ! (Matriz A)
		{
			if (A.esCuadrada())
			{
				Matriz T = new Matriz(A);
				Matriz R = new Matriz(A);
				for(int r = 0; r < R.Filas; r++)
				{
					if( T[r,r] == 0.0D ) throw new System.InvalidOperationException("Las matriz no se puede invertir (pivote 0).");
					inv_jordan(R, T, r);
					T = new Matriz(R);
				}
				return R;
			} else {
				throw new System.InvalidOperationException("Las matriz debe ser cuadrada.");
			}
		}

		public override String ToString()
		{
			String resultado = "";
			for (int i = 0; i < this.Filas; i++)
			{
				for (int j = 0; j < this.Columnas; j++)
				{
					resultado += "[ " + this[i,j] + "]";
				}
				resultado += "\n";
			}
			return resultado;
		}
	}
}

