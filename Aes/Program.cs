using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aes
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] texto = new int[4, 4] { { 0x32, 0x88, 0x31, 0xE0 }, { 0x43, 0x5A, 0x31, 0x37 }, { 0xF6, 0x30, 0x98, 0x07 }, { 0xA8, 0x8D, 0xA2, 0x34 } };
            int[,] clave = new int[4, 4] { { 0x2B, 0x28, 0xAB, 0x09 }, { 0x7E, 0xAE, 0xF7, 0xCF }, { 0x15, 0xD2, 0x15, 0x4F }, { 0x16, 0xA6, 0x88, 0x3C } };
    
            Subclave subclave = new Subclave(clave);
            List<int[,]> claves = subclave.subClave();

            Cifrartexto cifrartexto = new Cifrartexto(texto, claves);
            int[,] textocifrado = cifrartexto.cifrarTexto();


            // imprime la matriz de la clave

            Console.WriteLine("/////////////////// Sub claves //////////////////////////////");
            Console.WriteLine("");

            for (int i = 0; i < 11; i++)
            {
                    for (int f = 0; f < claves[i].GetLength(0); f++)
                    {
                        for (int c = 0; c < claves[i].GetLength(0); c++)
                        {
                            String hex = String.Format("{0:X2}", claves[i][f, c]);
                            Console.Write(hex + " ");
                        }
                        Console.WriteLine("");
                    }

                    Console.WriteLine("");
            }


            Console.WriteLine("/////////////////// Texto cifrado //////////////////////////////");
            Console.WriteLine("");

            for (int f = 0; f < textocifrado.GetLength(0); f++)
            {
                for (int c = 0; c < textocifrado.GetLength(0); c++)
                {
                    String hex = String.Format("{0:X2}", textocifrado[f, c]);
                    Console.Write(hex + " ");
                }
                Console.WriteLine("");
            }

            Console.WriteLine("");


            
        }
    }
}
