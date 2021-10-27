using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ASGJETSAT.Entities.Satislar;

namespace ASGJETSAT.BL
{
    static public class UrunSatis
    {
        static private int satislerdoldur() //entites katmanındaki Satislar classının içindeki Satis struct nesnesini dizi olarak doldurulup gönderilmesini sağlayan fonksiyon.
        {
            try
            {
                OleDbDataReader oleDbDataReader = DAL.Database.veriOku("select * from SATISLAR");//SATISLAR tablosundan tüm verileri çekiyor.

                while (oleDbDataReader.Read())//okunan tüm verileri satislar nesnesine ekliyoruz.
                {
                    UrunBilgisi.Satislar.Add(new Entities.Satislar(int.Parse(oleDbDataReader["ID"].ToString()),
                        oleDbDataReader["URUN"].ToString(), int.Parse(oleDbDataReader["ADET"].ToString()),
                        decimal.Parse(oleDbDataReader["FIYAT"].ToString()), DateTime.Parse(oleDbDataReader["TARIH"].ToString()),
                        oleDbDataReader["KULLANICI"].ToString()));
                }
                oleDbDataReader.Close();//oleDbDataReader kapatıyoruz.
                DAL.Database.databaseKapat();//Database baglantı kapatıyoruz.
                return 0;
            }
            catch
            {

            }
            return -1;
        }
        //satıs yapılan fonksıyon.
        static public int SatisYap(int UrunOzellikID, int UrunOzellikStok, string Urun, int Adet, decimal Fiyat, DateTime time, string Kullanıcı, string[] indexler)
        {
            int durum = -1; //sql gelecek durumu tutacak.
            //satış yapıldıktan sonra stok durumunu düzeltecek satır.
            durum = DAL.Database.sorguYap("UPDATE URUNOZELLIK SET STOK = " + UrunOzellikStok + " WHERE ID = " + UrunOzellikID);
            DAL.Database.databaseKapat();//Database baglantı kapatıyoruz.
            if (durum != -1)//stok durumu duzeldiyse satışlar tablosuna veri eklenecek satır.
            {
                UrunBilgisi.Urunler[int.Parse(indexler[0])].URUNLER[int.Parse(indexler[1])].URUNOZELLIK[int.Parse(indexler[2])].STOK = UrunOzellikStok;
                durum = DAL.Database.sorguYap("INSERT INTO SATISLAR(URUN,ADET,FIYAT,TARIH,KULLANICI) VALUES(" + "'" + Urun + "',"
                      + Adet + ",'" + Fiyat + "','" + time.ToString() + "','" + Kullanıcı + "'" + ")");
                DAL.Database.databaseKapat();//Database baglantı kapatıyoruz.
                if (durum != -1)
                {
                    int id = -1;
                    OleDbDataReader oleDbDataReader = DAL.Database.veriOku("SELECT TOP 1 ID FROM SATISLAR ORDER BY ID DESC");
                    while (oleDbDataReader.Read())
                    {
                        if ((id = int.Parse(oleDbDataReader["ID"].ToString())) != -1)
                        {
                            UrunBilgisi.Satislar.Add(new Entities.Satislar(id, Urun, Adet, Fiyat, time, Kullanıcı));
                            UrunBilgisi.Urunler[int.Parse(indexler[0])].SATISSAYISI += Adet;
                            UrunBilgisi.Urunler[int.Parse(indexler[0])].URUNLER[int.Parse(indexler[1])].SATISSAYISI += Adet;
                            UrunBilgisi.Urunler[int.Parse(indexler[0])].URUNLER[int.Parse(indexler[1])].URUNOZELLIK[int.Parse(indexler[2])].SATISSAYISI += Adet;
                        }
                    }
                   
                    durum = DAL.Database.sorguYap("UPDATE KATEGORI SET SATISSAYISI = " + UrunBilgisi.Urunler[int.Parse(indexler[0])].SATISSAYISI + " WHERE ID = " + UrunBilgisi.Urunler[int.Parse(indexler[0])].ID);
                    DAL.Database.databaseKapat();//Database baglantı kapatıyoruz.

                    durum = DAL.Database.sorguYap("UPDATE URUN SET SATISSAYISI = " + UrunBilgisi.Urunler[int.Parse(indexler[0])].URUNLER[int.Parse(indexler[1])].SATISSAYISI + " WHERE ID = " + UrunBilgisi.Urunler[int.Parse(indexler[0])].URUNLER[int.Parse(indexler[1])].ID);
                    DAL.Database.databaseKapat();//Database baglantı kapatıyoruz.

                    durum = DAL.Database.sorguYap("UPDATE URUNOZELLIK SET SATISSAYISI = " + UrunBilgisi.Urunler[int.Parse(indexler[0])].URUNLER[int.Parse(indexler[1])].URUNOZELLIK[int.Parse(indexler[2])].SATISSAYISI + " WHERE ID = " + UrunBilgisi.Urunler[int.Parse(indexler[0])].URUNLER[int.Parse(indexler[1])].URUNOZELLIK[int.Parse(indexler[2])].ID);
                    DAL.Database.databaseKapat();//Database baglantı kapatıyoruz.

                    return 0;
                }
            }
            DAL.Database.databaseKapat();//Database baglantı kapatıyoruz.
            return -1; ;
        }
    }
}
