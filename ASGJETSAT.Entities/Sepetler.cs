using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASGJETSAT.Entities
{
    public class Sepetler
    {
        public string INDIS;
        public int ADET;

        public Sepetler(string Indis,int Adet)
        {
            this.ADET = Adet;
            this.INDIS = Indis;
        }
    }
}
