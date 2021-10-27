using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASGJETSAT.Entities
{
    public class Satislar
    {
        public int ID;
        public string URUN;
        public int ADET;
        public decimal FIYAT;
        public DateTime TARIH;
        public string KULLANICI;

        public Satislar(int Id,string Urun,int Adet,decimal Fiyat,DateTime Tarih,string Kullanici)
        {
            this.ID = Id;
            this.URUN = Urun;
            this.ADET = Adet;
            this.FIYAT = Fiyat;
            this.TARIH = Tarih;
            this.KULLANICI = Kullanici;
        }
    }
}
