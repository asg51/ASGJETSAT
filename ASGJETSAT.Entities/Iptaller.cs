using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASGJETSAT.Entities
{
    public class Iptaller
    {
        public int ID;
        public string URUN;
        public int ADET;
        public decimal FIYAT;
        public string ACIKLAMA;
        public string KULLANICI;
        public DateTime TARIH;

        public Iptaller(int Id,string Urun,int Adet,decimal Fiyat,string Aciklama,string Kullanici,DateTime Tarih)
        {
            this.ID = Id;
            this.URUN = Urun;
            this.ADET = Adet;
            this.FIYAT = Fiyat;
            this.ACIKLAMA = Aciklama;
            this.KULLANICI = Kullanici;
            this.TARIH = Tarih;
        }
    }
}

