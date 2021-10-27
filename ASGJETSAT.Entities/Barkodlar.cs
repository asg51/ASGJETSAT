using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASGJETSAT.Entities
{
    public class Barkodlar
    {
        public int BARKOD;
        public string INDEXLER;

        public Barkodlar(int Barkod,string Indexler)
        {
            this.BARKOD = Barkod;
            this.INDEXLER = Indexler;
        }
    }
}
