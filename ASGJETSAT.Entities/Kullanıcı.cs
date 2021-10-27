using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASGJETSAT.Entities
{
    public class Kullanıcı
    {
        public int ID;
        public string KULLANICI;
        public string SIFRE;

        public static int AktifID;
        public static string AktifKULLANICI;
        public static string AktifSIFRE;
        public Kullanıcı(int Id,string Kullanici,string Sifre)
        {
            this.ID = Id;
            this.KULLANICI = Kullanici;
            this.SIFRE = Sifre;
        }
    }
}
