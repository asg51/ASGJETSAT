using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASGJETSAT.DAL;
using ASGJETSAT.Entities;
using static ASGJETSAT.Entities.Kategori;

namespace ASGJETSAT.BL
{
    static public class UrunKategori
    {
        static public int kategorilerdoldur() //Kategoriler structnun dizi yapısından dönüş yapacak kategorilerdoldur fonksiyonu oluşturuldu.
        {
            BL.UrunBilgisi.Urunler.Clear();
            try
            {
                OleDbDataReader oleDbDataReader = DAL.Database.veriOku("select * from KATEGORI"); //KATEGORI tablosunun tüm verisi çekildi.

                while (oleDbDataReader.Read()) //Veriler tek tek okunuyor ve kategorilers nesnesine ekleniyor.
                {
                    UrunBilgisi.Urunler.Add(new Kategori(int.Parse(oleDbDataReader["ID"].ToString()),
                        oleDbDataReader["KATEGORI"].ToString(), int.Parse(oleDbDataReader["SATISSAYISI"].ToString())));
                }

                oleDbDataReader.Close();//oleDbDataReader kapatıyoruz.
                DAL.Database.databaseKapat();//database kapatıyoruz.

                return UrunUrun.urunlerDoldur();
            }
            catch
            {
                DAL.Database.databaseKapat();//database kapatıyoruz.
                return -1;
            }
        }

        static public int KategoriEkle(string data) // KATEGORI tablosuna kategori ismini kayıt ediyoruz.
        {
            string sql = "INSERT INTO KATEGORI(KATEGORI,SATISSAYISI) VALUES(" + "'" + data + "'," + "0" + ")";
            if (ASGJETSAT.DAL.Database.sorguYap(sql) != -1)
            {
                sql = "SELECT TOP 1 ID FROM KATEGORI ORDER BY ID DESC";
                OleDbDataReader oleDbDataReader = ASGJETSAT.DAL.Database.veriOku(sql);
                int id = -1;

                while (oleDbDataReader.Read())
                {
                    if ((id = int.Parse(oleDbDataReader["ID"].ToString())) != -1)
                    {
                        UrunBilgisi.Urunler.Add(new Kategori(id, data, 0));
                        oleDbDataReader.Close();//oleDbDataReader kapatıyoruz.
                        DAL.Database.databaseKapat();//database kapatıyoruz.
                        return 0;
                    }
                }
            }
            DAL.Database.databaseKapat();//database kapatıyoruz.
            return -1;
        }
        static public int KategoriGuncelle(string data, int ID, int kategoriIndex) //KATEGORI tablosuna ID ye göre kategori ismini güncelliyoruz.
        {
            string sql = "UPDATE KATEGORI SET KATEGORI='" + data + "' WHERE ID=" + ID.ToString();
            if (ASGJETSAT.DAL.Database.sorguYap(sql) != -1)
            {
                UrunBilgisi.Urunler[kategoriIndex].KATEGORI = data;
                DAL.Database.databaseKapat();//database kapatıyoruz.
                return 0;
            }
            DAL.Database.databaseKapat();//database kapatıyoruz.
            return -1;
        }
        static public int KategorSil(int ID, int kategoriIndex) //KATEGORI ID göre silen fonksiyon
        {
            int durum = -1; // sorgudan gelecek değeri tutma için tanımlandı -1 ise sorgunun çalışmazsa gönderecegi deger olarak atandı.

            //Urun tablosundaki kategorııd'sı silecegimiz kategori ıd ile aynı olanlarıçekiyoruz.
            string sql = "SELECT ID AS ID FROM URUN WHERE KATEGORIID =" + ID;
            OleDbDataReader oleDbDataReader = ASGJETSAT.DAL.Database.veriOku(sql);
            while (oleDbDataReader.Read()) //Bulduğumuz ID leri Urunozellik tablosundan siliyoruz bu sayede Kategoriye ait olan Urun ozellikled siliniyor.
            {
                sql = "DELETE FROM URUNOZELLIK WHERE URUNID=" + oleDbDataReader["ID"].ToString();
                durum = ASGJETSAT.DAL.Database.sorguYap(sql);
            }
            oleDbDataReader.Close(); //oleDbDataReader kapatıyoruz.
            DAL.Database.databaseKapat();//Database kapatıyoruz.

            //Urun tablosundaki Kategorıd'ler silecegimiz ID esit ise siliyoruz bu sayede Urun tablosundaki kategorimize bagli olanlarda siliniyor.
            sql = "DELETE FROM URUN WHERE KATEGORIID=" + ID.ToString();
            durum = ASGJETSAT.DAL.Database.sorguYap(sql);
            DAL.Database.databaseKapat();//Database kapatıyoruz.

            //Kategorı tablosunda Id uygun olan kategorimizi siliyoruz.
            sql = "DELETE FROM KATEGORI WHERE ID=" + ID.ToString();
            durum = ASGJETSAT.DAL.Database.sorguYap(sql);
            DAL.Database.databaseKapat();//Database kapatıyoruz.

            //Kategoriye ait tüm veriler siliniyor ise yaramayan veriler hafizada kalmıyor.
            if (durum != -1)
            {
                UrunBilgisi.Urunler.RemoveAt(kategoriIndex);
                oleDbDataReader.Close();//oleDbDataReader kapatıyoruz.
                DAL.Database.databaseKapat();//database kapatıyoruz.
                return 0;
            }
            oleDbDataReader.Close();//oleDbDataReader kapatıyoruz.
            DAL.Database.databaseKapat();//database kapatıyoruz.
            return -1;
        }
    }
}
