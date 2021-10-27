using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASGJETSAT.Entities
{
    public class KisaYollar
    {
        public int ID;
        public string INDEXLER;

        public KisaYollar(int Id,string Indexler)
        {
            this.ID = Id;
            this.INDEXLER = Indexler;
        }
    }
}
