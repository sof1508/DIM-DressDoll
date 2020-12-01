using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DressDoll
{
    class Doll
    {
        public string ParteArriba { get; set; }
        public string ParteAbajo{ get; set; }
        public string Zapatos { get; set; }
        public string ParteEntera{ get; set; }

        public Doll(String parteArriba, String parteAbajo, String zapatos, String parteEntera) {
            ParteArriba = parteArriba;
            ParteAbajo = parteAbajo;
            Zapatos = zapatos;
            ParteEntera = parteEntera;
        }
    }
}
