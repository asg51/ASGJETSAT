using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASGJETSAT.BL
{
   static public class UrunStok //urun stok ayarlarının yapıldığı bölüm.
    {
       static public int StokDuzenle(int ID ,int Sayı, int KategoriIndex,int UrunIndex,int UrunOzellikIndex)//urunozellik tablosundaki ID göre parametreden gelen ID eşit olanları güncelleyen satır.
        {
            if( DAL.Database.sorguYap("UPDATE URUNOZELLIK SET STOK =" + Sayı + " WHERE ID = " + ID) != -1)
            {
                UrunBilgisi.Urunler[KategoriIndex].URUNLER[UrunIndex].URUNOZELLIK[UrunOzellikIndex].STOK = Sayı;
                return 0;
            }
            return -1;
        }
    }
}
