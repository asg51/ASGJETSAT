using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASGJETSAT.Entities
{
    public class Urun
    {
        public int ID;
        public int KATEGORIID;
        public string URUN;
        public int SATISSAYISI;
        public List<UrunOzellik> URUNOZELLIK = new List<UrunOzellik>();

        public Urun(int Id,int KategoriId, string Urun,int SatisSayisi)
        {
            this.ID = Id;
            this.KATEGORIID = KategoriId;
            this.URUN = Urun;
            this.SATISSAYISI = SatisSayisi;
        }
    }
}
