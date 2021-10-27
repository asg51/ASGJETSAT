using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASGJETSAT.BL
{
   static public class GirisAyar //giris ayarların classı
    {
      static public  bool GirisKontrol(string Kullanici,string Parola) // giriş kontrol edilen fonksiyon.
        {
            List<Entities.Kullanıcı> AktifKullanici = UrunBilgisi.Kullanıcılar.Where(x => x.KULLANICI == Kullanici &&
             x.SIFRE == Parola).ToList();
            if (AktifKullanici.Count >0)
            {
                Entities.Kullanıcı.AktifID = AktifKullanici[0].ID;
                Entities.Kullanıcı.AktifKULLANICI = AktifKullanici[0].KULLANICI;
                Entities.Kullanıcı.AktifSIFRE = AktifKullanici[0].SIFRE;
                return true;
            }
            return false;
        }
    }
}
