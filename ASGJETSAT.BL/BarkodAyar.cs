using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASGJETSAT.Entities;

namespace ASGJETSAT.BL
{
    public class BarkodAyar
    {
        private static void buble_sort()// Kabarcık sıralama (buble sort) yoluyla sayıları küçükten büyüğe sıralayan algoritmadır.
        {
            int barkod;
            string indexler;
            for (int i = 0; i < UrunBilgisi.Barkodlar.Count - 1; i++)
            {
                for (int j = 1; j < UrunBilgisi.Barkodlar.Count - i; j++)
                {
                    if (UrunBilgisi.Barkodlar[j].BARKOD < UrunBilgisi.Barkodlar[j - 1].BARKOD)
                    {
                        barkod = UrunBilgisi.Barkodlar[j - 1].BARKOD;
                        indexler = UrunBilgisi.Barkodlar[j - 1].INDEXLER;

                        UrunBilgisi.Barkodlar[j - 1] = UrunBilgisi.Barkodlar[j];

                        UrunBilgisi.Barkodlar[j].BARKOD = barkod;
                        UrunBilgisi.Barkodlar[j].INDEXLER = indexler;
                    }
                }
            }

        }

        public static int BarkodSirala()
        {
            try
            {
                buble_sort();
                return 0;
            }
            catch
            {

            }
            return -1;
        }


        public static int indexBul(string data)
        {
            string[] dizi = data.Split('_');
            int sayac = 0;

            for (int i = 0; i < UrunBilgisi.Urunler.Count; i++)
            {
                for (int j = 0; j < UrunBilgisi.Urunler[i].URUNLER.Count; j++)
                {
                    if (i == int.Parse(dizi[0]) && j == int.Parse(dizi[1]))
                        break;
                    sayac += UrunBilgisi.Urunler[i].URUNLER[j].URUNOZELLIK.Count;

                }
                if (i == int.Parse(dizi[0]))
                    break;
            }
            sayac += int.Parse(dizi[2]);
            return sayac;
        }

        public static string hizliArama(int barkod)
        {
            int eb = UrunBilgisi.Barkodlar.Count;
            int ek = -1;

            while (eb - ek > 1)
            {
                int bakilan = (eb + ek) / 2;
                if (UrunBilgisi.Barkodlar[bakilan].BARKOD == barkod)
                {
                    return UrunBilgisi.Barkodlar[bakilan].INDEXLER;
                }
                else if (UrunBilgisi.Barkodlar[bakilan].BARKOD < barkod)
                {
                    ek = bakilan;
                }
                else
                {
                    eb = bakilan;
                }
            }
            return "-1";
        }
    }
}
