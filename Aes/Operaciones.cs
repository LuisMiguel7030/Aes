using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aes
{
    class Operaciones
    {
        
        public int[] rotWord(int[,] clave)
        {
            int[] rotword = new int[4];

            rotword[0] = clave[1,3];
            rotword[1] = clave[2,3];
            rotword[2] = clave[3,3];
            rotword[3] = clave[0,3];

            return rotword;
        }


        public int[] subByte(int[] rotword)
        {
            Tablas tabla = new Tablas();
            int[] subbyte = new int[4];
            char[] busqueda = new char[16] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

            for(int i = 0; i < rotword.GetLength(0); i++)
            {
                int x = rotword[i];
                String hex = x.ToString("X2");
            
                int posx = Array.IndexOf(busqueda, hex[0]);
                int posy = Array.IndexOf(busqueda, hex[1]);
                subbyte[i] = tabla.sBox(posx, posy);
            }

            return subbyte;
         
        }

        public int[] xOr(int[,] subclave, int[] subbyte, int[] rcon)
        {
            int[] colum = new int[4];
            colum[0] = subclave[0, 0];
            colum[1] = subclave[1, 0];
            colum[2] = subclave[2, 0];
            colum[3] = subclave[3, 0];

            for(int i = 0; i < colum.GetLength(0); i++)
            {
                colum[i] = colum[i] ^ subbyte[i] ^ rcon[i];
            }

            return colum;
        }


        public int[,] shiftRows(int[,] texto)
        {
            Operaciones op = new Operaciones();
            int[,] textoshift = texto;
            int a = 1, b = 2, c = 3, d = 0;

            for (int i = 1; i < 4; i++)
            {
                int[] fila = op.matrizFila(texto, i);
                
                textoshift[i, 0] = fila[a];
                textoshift[i, 1] = fila[b];
                textoshift[i, 2] = fila[c];
                textoshift[i, 3] = fila[d];

                a = a + 1; b = b + 1; c = c +1; d = d + 1;
                a = op.validez(a);
                b = op.validez(b);
                c = op.validez(c);
                d = op.validez(d);
                
            }

            return texto;
            
        }

        public int[,] mixColumns(int[,] texto)
        {
            Operaciones op = new Operaciones();
            int[,] matriz = new int[4, 4];
            for(int i = 0; i < 4; i++)
            {
                matriz[0, i] = op.mul_02(texto[0, i]) ^ op.mul_03(texto[1, i]) ^ texto[2, i] ^ texto[3, i];
                matriz[1, i] = texto[0, i] ^ op.mul_02(texto[1, i]) ^ op.mul_03(texto[2, i]) ^ texto[3, i];
                matriz[2, i] = texto[0, i] ^ texto[1, i] ^ op.mul_02(texto[2, i]) ^ op.mul_03(texto[3, i]);
                matriz[3, i] = op.mul_03(texto[0, i]) ^ texto[1, i] ^ texto[2, i] ^ op.mul_02(texto[3, i]);
            }

            return matriz;
        }

        public int[,] textoSubbyte(int[,] texto)
        {
            Operaciones op = new Operaciones();
           

            for (int c = 0; c < 4; c++)
            {
                int[] columna = op.matrizColumna(texto, c);
                int[] subbyte = op.subByte(columna);

                texto[0, c] = subbyte[0];
                texto[1, c] = subbyte[1];
                texto[2, c] = subbyte[2];
                texto[3, c] = subbyte[3];
            }

            return texto;
        } 

    

        public int[,] addRoundkey(int[,] texto, int[,] calve)
        {
            for (int i = 0; i < 4; i++)
            {
                texto[0, i] = texto[0, i] ^ calve[0, i];
                texto[1, i] = texto[1, i] ^ calve[1, i];
                texto[2, i] = texto[2, i] ^ calve[2, i];
                texto[3, i] = texto[3, i] ^ calve[3, i];
            }

            return texto;
        }


        public int mul_02(int hexa)
        {
            
            hexa = hexa * 2;

            if (hexa > 0xFF)
            {
                String tol = hexa.ToString("X2");
                String uno = Char.ToString(tol[1]);
                String dos = Char.ToString(tol[2]);
                int r = Convert.ToInt32(String.Concat(uno, dos), 16);
                r = r ^ 0x1B;
                return r;
            }

            return hexa;
        }

        public int mul_03(int hexa)
        {
            
            int hex = hexa;

            hexa = hexa * 2;
            if (hexa > 0xFF)
            {
                String tol = hexa.ToString("X2");
                String uno = Char.ToString(tol[1]);
                String dos = Char.ToString(tol[2]);
                int r = Convert.ToInt32(String.Concat(uno, dos), 16);
                r = r ^ 0x1B ^ hex;
                return r;
            }

            else
            {
                hexa = hexa ^ hex;
                return hexa;
            }

        }

        public int [] matrizColumna(int[,] matriz, int c)
        {
            int[] columna = new int[4];
            for (int i = 0; i < columna.GetLength(0); i++)
            {
                columna[i] = matriz[i, c];
            }

            return columna;
        }

        public int[] matrizFila(int[,] matriz, int f)
        {
            int[] fila = new int[4];
            for(int i = 0; i < fila.GetLength(0); i++)
            {
                fila[i] = matriz[f, i];
            }

            return fila;
        }


        public int validez(int letra)
        {
            if(letra == 4)
            {
                return letra = 0;
            }

            else
            {
                return letra;
            }
        }


    }
}
