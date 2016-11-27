using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aes
{
    class Subclave
    {
        int c = 0;
        int s = 1;

        List<int[,]> subclaves = new List<int[,]>();     
    

        public Subclave(int[,] clave)
        {
            this.subclaves.Add(clave);
        }

        public List<int[,]> subClave()
        {
            Operaciones op = new Operaciones();
            Tablas tabla = new Tablas();
            

            for(int i = 0; i < 10; i++)
            {
                int[,] claves = new int[4, 4];
                for (int x = 0; x < 4; x++)
                {
                
                    if (x == 0)
                    {
                       
                        int[] aXor = op.xOr(this.subclaves[this.c], op.subByte(op.rotWord(this.subclaves[this.c])), tabla.rCon(this.c));
                        claves[0, 0] = aXor[0];
                        claves[1, 0] = aXor[1];
                        claves[2, 0] = aXor[2];
                        claves[3, 0] = aXor[3];
                        this.subclaves.Add(claves);

                    }

                    else
                    {
                        for (int c = 0; c < 3; c++)
                        {
                            for (int f = 0; f < 4; f++)
                            {
                                this.subclaves[this.s][f, c + 1] = this.subclaves[this.c][f, c + 1] ^ this.subclaves[this.s][f, c];
                            }
                        }
                     }
                 }
                this.s++;
                this.c++;
            }

            return this.subclaves;
        }          
        


    }

}
