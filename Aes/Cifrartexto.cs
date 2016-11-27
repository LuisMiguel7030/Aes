using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aes
{
    class Cifrartexto
    {

        int[,] texto =new int[4, 4];
        List<int[,]> claves = new List<int[,]>();

        public Cifrartexto(int[,] texto, List<int[,]> claves)
        {
            this.texto = texto;
            this.claves = claves;
        }

        public int[,] cifrarTexto()
        {
            Operaciones op = new Operaciones();

            int[,] newTexto = op.addRoundkey(texto, claves[0]);

            for (int c = 1; c <= 9; c++)
            {
                newTexto = op.addRoundkey(op.mixColumns(op.shiftRows(op.textoSubbyte(newTexto))), claves[c]);
            }

            return op.addRoundkey(op.shiftRows(op.textoSubbyte(newTexto)),claves[10]);
        }


    }
}
