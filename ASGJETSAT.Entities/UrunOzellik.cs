using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASGJETSAT.Entities
{
    public class UrunOzellik
    {
        public int ID;
        public int URUNID;
        public string OZELLIK;
        public decimal FIYAT;
        public int BARKOD;
        public int STOK;
        public int SATISSAYISI;

        public UrunOzellik(int Id, int UrunID,string Ozellik, decimal Fiyat,int Barkod,int Stok,int SatisSayisi)
        {
            this.ID = Id;
            this.URUNID = UrunID;
            this.OZELLIK = Ozellik;
            this.FIYAT = Fiyat;
            this.BARKOD = Barkod;
            this.STOK = Stok;
            this.SATISSAYISI = SatisSayisi;
        }
    }
}
