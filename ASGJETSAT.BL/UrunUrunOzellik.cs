using ASGJETSAT.Entities;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ASGJETSAT.Entities.UrunOzellik;

namespace ASGJETSAT.BL
{
    static public class UrunUrunOzellik //urunozellik ayarlarının yapılacagı kısım.
    {
        static public int urunOzelliklerDoldur() //urunlerozellik struct dizisinin verileri doldurulan fonksiyon.
        {
            try
            {
                for (int i = 0; i < UrunBilgisi.Urunler.Count; i++) //kategori sayısı kadar dongu kuruldu.
                {
                    for (int j = 0; j < UrunBilgisi.Urunler[i].URUNLER.Count; j++) //urunsayıları kadar dongu kuruldu.
                    {
                        int z = 0;
                        //urunler ıd'ne eşit olan urunozellik katmanında urunıd sunundaki verileri çekecek kod.
                        OleDbDataReader oleDbDataReader = DAL.Database.veriOku("SELECT * FROM URUNOZELLIK WHERE URUNID = " + UrunBilgisi.Urunler[i].URUNLER[j].ID);
                        while (oleDbDataReader.Read()) //tek tek verileri okuyor.
                        {
                            UrunBilgisi.Urunler[i].URUNLER[j].URUNOZELLIK.Add(new UrunOzellik(int.Parse(oleDbDataReader["ID"].ToString()),
                                int.Parse(oleDbDataReader["URUNID"].ToString()), oleDbDataReader["OZELLIK"].ToString(),
                                decimal.Parse(oleDbDataReader["FIYAT"].ToString()), int.Parse(oleDbDataReader["BARKOD"].ToString()),
                                 int.Parse(oleDbDataReader["STOK"].ToString()), int.Parse(oleDbDataReader["SATISSAYISI"].ToString())));

                            UrunBilgisi.Barkodlar.Add(new Barkodlar(int.Parse(oleDbDataReader["BARKOD"].ToString()),
                                i.ToString() + "_" + j.ToString() + "_" + z.ToString()));

                            UrunBilgisi.KisaYollar.Add(new KisaYollar(int.Parse(oleDbDataReader["ID"].ToString()),
                               i.ToString() + "_" + j.ToString() + "_" + z.ToString()));

                            z++;
                        }
                        oleDbDataReader.Close(); //oleDbDataReader kapat.
                        DAL.Database.databaseKapat();//Database kapat.
                    }
                }
                DAL.Database.databaseKapat();
                return 0;
            }
            catch
            {

            }
            return -1;
        }
       

        static public int OzellikEkle(int UrunID, string Ozellik, int Barkod, decimal Fiyat,int kategoriIndex,int urunIndex) //urune ozellik ekleyecek fonksiyon.
        {
            int durum = -1;
            durum = DAL.Database.sorguYap("INSERT INTO URUNOZELLIK(URUNID,OZELLIK,FIYAT,STOK,SATISSAYISI,BARKOD) VALUES(" + UrunID + ",'" + Ozellik + "','" + Fiyat + "',0,0,'" + Barkod + "')");
            DAL.Database.databaseKapat();
            if(durum != -1)
            {
                int ID = -1;
                OleDbDataReader oleDbDataReader = ASGJETSAT.DAL.Database.veriOku("SELECT TOP 1 ID FROM URUNOZELLIK ORDER BY ID DESC");
                while (oleDbDataReader.Read())
                {
                    if ((ID = int.Parse(oleDbDataReader["ID"].ToString())) != -1)
                    {
                        UrunBilgisi.Urunler[kategoriIndex].URUNLER[urunIndex].URUNOZELLIK.Add(new UrunOzellik(ID, UrunID, Ozellik,
                            Fiyat, Barkod, 0, 0));
                        DAL.Database.databaseKapat();
                        return 0;
                    }
                }
            }
            return -1;
        }

        static public int OzellikDuzenle(int ID, int UrunID, string Ozellik, int Barkod, decimal Fiyat,int kategoriIndex,int urunIndex,int urunozellikIndex,int yeniKategoriIndex,int yeniUrunIndex) //ozellik duzenleme sadece urun ozellik kısmını duzenleyen fonksiyon.
        {
            int durum = -1;
            durum = DAL.Database.sorguYap("UPDATE URUNOZELLIK SET URUNID = " + UrunID + ", OZELLIK = '" + Ozellik + "', FIYAT ='" + Fiyat + "', BARKOD = '" + Barkod + "' WHERE ID= " + ID);
            DAL.Database.databaseKapat();
            if(durum != -1)
            {
                UrunBilgisi.Urunler[kategoriIndex].URUNLER[urunIndex].URUNOZELLIK[urunozellikIndex].OZELLIK = Ozellik;
                UrunBilgisi.Urunler[kategoriIndex].URUNLER[urunIndex].URUNOZELLIK[urunozellikIndex].FIYAT = Fiyat;
                UrunBilgisi.Urunler[kategoriIndex].URUNLER[urunIndex].URUNOZELLIK[urunozellikIndex].BARKOD = Barkod;
                if (UrunBilgisi.Urunler[kategoriIndex].URUNLER[urunIndex].URUNOZELLIK[urunozellikIndex].URUNID != UrunID)
                {
                    UrunBilgisi.Urunler[kategoriIndex].URUNLER[urunIndex].URUNOZELLIK[urunozellikIndex].URUNID = UrunID;
                    UrunBilgisi.Urunler[yeniKategoriIndex].URUNLER[yeniUrunIndex].URUNOZELLIK.Add(UrunBilgisi.Urunler[kategoriIndex].URUNLER[urunIndex].URUNOZELLIK[urunozellikIndex]);
                    UrunBilgisi.Urunler[kategoriIndex].URUNLER[urunIndex].URUNOZELLIK.RemoveAt(urunozellikIndex);
                }
                
                return 0;
            }
            return -1;
        }
        //static public int OzellikDuzenle(int ID, int KategoriID,int kategoriIndex,int urunIndex,int yenikategoriIndex) //ozellik duzenleme sadece urun kısmını duzenleyen fonksiyon.
        //{
        //    int durum = -1;
        //    durum = DAL.Database.sorguYap("UPDATE URUN SET KATEGORIID = " + KategoriID + " WHERE ID= " + ID);
        //    DAL.Database.databaseKapat();
        //    if(durum != -1)
        //    {
        //        UrunBilgisi.Urunler[yenikategoriIndex].URUNLER.Add(UrunBilgisi.Urunler[kategoriIndex].URUNLER[urunIndex]);
        //        UrunBilgisi.Urunler[kategoriIndex].URUNLER.Clear();
        //        return 0;
        //    }
        //    return -1;
        //}

        static public int OzellikSil(int ID,int kategoriIndex,int urunIndex,int urunozellikIndex)// urun ozellik kısmında veri silen fonskıyon.
        {
            int durum = -1;
            durum = DAL.Database.sorguYap("DELETE FROM URUNOZELLIK WHERE ID = " + ID);
            DAL.Database.databaseKapat();
            if (durum != -1)
            {
                UrunBilgisi.Urunler[kategoriIndex].URUNLER[urunIndex].URUNOZELLIK.RemoveAt(urunozellikIndex);
                return 0;
            }
            return -1;
        }
    }
}
