using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DressDoll
{
    class Doll
    {
        public string[] RopaPuesta = new string[4];

        public Doll(String[] ropaActual) {
            RopaPuesta = ropaActual;
            //0 parte Arriba
            //1 parte Abajo
            //2 Zapatos
            //3 Prenda Entera

        }

        public bool conflictoRopa(String[] ropaActual, String prendaPoner) {
            bool conflicto = false;
            switch (prendaPoner) {
                case "ParteArriba":
                    if (ropaActual[3] != "" ) conflicto = true;
                    break;
                case "ParteAbajo":
                    if (ropaActual[3] != "") conflicto = true;
                    break;
                case "ParteEntera":
                    if (ropaActual[0] != "" || ropaActual[1] != "") conflicto = true;
                    break;
            }
            return conflicto;
        }
   
    }
}
