using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DressDoll
{
    class Doll
    {
        public bool ParteArriba;
        public bool ParteAbajo;
        public bool Zapatos;
        public bool ParteEntera;

        public Doll(bool parteArriba, bool parteAbajo, bool zapatos, bool parteEntera) {
            ParteArriba = parteArriba;
            ParteAbajo = parteAbajo;
            Zapatos = zapatos;
            ParteEntera = parteEntera;
        }

        public bool conflictoRopa(String prendaPoner) {
            bool conflicto = false;
            switch (prendaPoner) {
                case "ParteArriba":
                    if (this.ParteEntera) conflicto = true;
                    break;
                case "ParteAbajo":
                    if (this.ParteEntera) conflicto = true;
                    break;
                case "ParteEntera":
                    if (this.ParteArriba || this.ParteAbajo) conflicto = true;
                    break;
            }
            return conflicto;
        }
   
    }
}
