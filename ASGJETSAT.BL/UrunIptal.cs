using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASGJETSAT.Entities;

namespace ASGJETSAT.BL
{
    static public class UrunIptal //Urunlerin iptal özelliklerinin edildiği class
    {
        static public int UrunIptalEt(int UrunOzellikID, int UrunOzellikStok, string Urun, int Adet, decimal Fiyat, DateTime Tarih, string Kullanıcı, string Acıklama, int KategoriIndex, int UrunIndex, int UrunOzellikIndex)
        {
            int durum = -1; //durumu -1 olarak tanımlıyoruz hiçbir sart tutmazsa durum geri gönderilecek.

            //stok güncellediğimiz kısım.
            durum = DAL.Database.sorguYap("UPDATE URUNOZELLIK SET STOK = " + UrunOzellikStok + " WHERE ID = " + UrunOzellikID);
            if (durum != -1)//stok güncellendi ise yapılacak olan kısım
            {   //IPTALLER tablosuna verileri eklediğimiz kısım.
                UrunBilgisi.Urunler[KategoriIndex].URUNLER[UrunIndex].URUNOZELLIK[UrunOzellikIndex].STOK += UrunOzellikStok;
                int Id = -1;
                if (ASGJETSAT.DAL.Database.sorguYap("INSERT INTO IPTALLER(URUN,ADET,FIYAT,TARIH,KULLANICI,ACIKLAMA) VALUES('" +
                Urun + "'," + Adet + ",'" + Fiyat + "','" + Tarih.ToString() + "','" + Kullanıcı + "','" + Acıklama + "')") != -1)
                {
                    OleDbDataReader oleDbDataReader = DAL.Database.veriOku("SELECT TOP 1 ID FROM IPTALLER ORDER BY ID DESC");
                    while (oleDbDataReader.Read())
                    {
                        Id = int.Parse(oleDbDataReader["ID"].ToString());
                        UrunBilgisi.Iptaller.Add(new Iptaller(Id, Urun, Adet, Fiyat, Acıklama, Kullanıcı, Tarih));
                        return 0;
                    }

                }
            }
            return -1;
        }
        //entites katamanındaki Iptaller Classının Iptal struct nesnesini dizi Şeklinde Iptaller Tablosundaki verilerin tutuluduğu yer.
        static public int iptaldoldur()
        {
            BL.UrunBilgisi.Iptaller.Clear();
            try
            {
                OleDbDataReader oleDbDataReader = DAL.Database.veriOku("select * from IPTALLER");//SATISLAR tablosunun verilerini oleDbDataReader nesnesine çekiyoruz.
                while (oleDbDataReader.Read()) //Tüm verileri tek tek okuyoruz ve iptals dizisine tek tek ekliyoruz.
                {
                    UrunBilgisi.Iptaller.Add(new Iptaller(int.Parse(oleDbDataReader["ID"].ToString()),
                        oleDbDataReader["URUN"].ToString(), int.Parse(oleDbDataReader["ADET"].ToString()),
                        decimal.Parse(oleDbDataReader["FIYAT"].ToString()), oleDbDataReader["ACIKLAMA"].ToString(),
                        oleDbDataReader["KULLANICI"].ToString(), DateTime.Parse(oleDbDataReader["TARIH"].ToString())));
                }
                oleDbDataReader.Close(); //oleDbDataReader nesnesini kapatiyoruz.
                DAL.Database.databaseKapat(); // Datebase kapatıyoruz.
                return 0; //iptals nesnesini geri dönderiyoruz.
            }
            catch
            {

            }
            return -1;
        }
    }
}
