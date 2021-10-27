using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace ASGJETSAT.BL
{
    static public class KullanıcıAyar  //Kullanıcı ile ilgili yapılabilecek özelliklerin tutulduğu yer
    {
        static public int KullanıcıEkle(string Kullanici, string Sifre) //Sisteme yeni kullanıcı ekleneceği bölüm
        {
            if (ASGJETSAT.DAL.Database.sorguYap("INSERT INTO KULLANICILAR(KULLANICI,SIFRE) VALUES('" + Kullanici + "','" + Sifre + "')") != -1) //Dal Katmanına Sql cümle ile gönderip kayıtı ekliyoruz.
            {
                OleDbDataReader oleDbDataReader = DAL.Database.veriOku("SELECT TOP 1 ID FROM KULLANICILAR ORDER BY ID DESC");
                int id = -1;
                while (oleDbDataReader.Read())
                {
                    if ((id = int.Parse(oleDbDataReader["ID"].ToString())) != -1)
                    {
                        UrunBilgisi.Kullanıcılar.Add(new Entities.Kullanıcı(id, Kullanici, Sifre));
                        return 0;
                    }
                }
            }
            return -1;
        }
        static public int KullanıcıBilgileriDoldur() //Entites katmanındaki Kullanıcı class'nın Kullanıcılar struct yeni dizili nesne oluşturup dolduruyoruz. 
        {
            UrunBilgisi.Kullanıcılar.Clear();
            OleDbDataReader oleDbDataReader = DAL.Database.veriOku("SELECT * FROM KULLANICILAR"); //yeniden tüm verileri oleDbDataReader alıyoruz.
            while (oleDbDataReader.Read())//Tüm verileri yeniden okumaya başlanıyor.
            {
                UrunBilgisi.Kullanıcılar.Add(new Entities.Kullanıcı(int.Parse(oleDbDataReader["ID"].ToString()),
                   oleDbDataReader["KULLANICI"].ToString(), oleDbDataReader["SIFRE"].ToString()));
            }
           
            oleDbDataReader.Close(); // oleDbDataReader kapatıyoruz.
            DAL.Database.databaseKapat();// Database baglantısını sonlandırıyoruz.

            return 0;
        }

        static public int KullanıcıSil(int ID,int KullaniciIndex) //Kullanıcı silme fonksiyonu gelen ID göre Kullanıcılar tablosundan Kullanıcıyı siliyor.
        {
            if( ASGJETSAT.DAL.Database.sorguYap("DELETE FROM KULLANICILAR WHERE ID=" + ID.ToString()) != -1)
            {
                UrunBilgisi.Kullanıcılar.RemoveAt(KullaniciIndex);
                return 0;
            }
            return -1;
        }
        static public int KullanıcıGuncelle(int ID, string Kullanıcı, string Sifre, int KullaniciIndex) //Kullanıcı Güncelleme Fonksiyonu Gelen IDye göre bulunun kullanıcının KullanıcıAdını ve Sifresini Güncelliyor.
        {
            if( ASGJETSAT.DAL.Database.sorguYap("UPDATE KULLANICILAR SET KULLANICI ='" + Kullanıcı + "', SIFRE = '" + Sifre + "' WHERE ID=" + ID.ToString()) != -1)
            {
                UrunBilgisi.Kullanıcılar[KullaniciIndex].KULLANICI = Kullanıcı;
                UrunBilgisi.Kullanıcılar[KullaniciIndex].SIFRE = Sifre;
                return 0;
            }
            return -1;
        }
    }
}
