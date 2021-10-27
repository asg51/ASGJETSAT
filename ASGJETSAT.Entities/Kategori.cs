using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASGJETSAT.DAL;
using System.Data.OleDb;

namespace ASGJETSAT.Entities
{
    public class Kategori
    {
        public int ID;
        public string KATEGORI;
        public int SATISSAYISI;
        public List<Urun> URUNLER = new List<Urun>();

        public Kategori(int Id, string Kategori,int SatisSayisi)
        {
            this.ID = Id;
            this.KATEGORI = Kategori;
            this.SATISSAYISI = SatisSayisi;
        }
    }
}
