using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASGJETSAT.BL
{
   public static class UrunBilgisi
    {
        public static List<Entities.Kategori> Urunler = new List<Entities.Kategori>();
        public static List<Entities.Barkodlar> Barkodlar = new List<Entities.Barkodlar>();
        public static List<Entities.KisaYollar> KisaYollar = new List<Entities.KisaYollar>();
        public static List<Entities.Kullanıcı> Kullanıcılar = new List<Entities.Kullanıcı>();
        public static List<Entities.Iptaller> Iptaller = new List<Entities.Iptaller>();
        public static List<Entities.Satislar> Satislar = new List<Entities.Satislar>();
    }
}
