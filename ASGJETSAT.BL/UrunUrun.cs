using ASGJETSAT.Entities;
using System.Data.OleDb;

namespace ASGJETSAT.BL
{
    static public class UrunUrun //Urun ayarlarının yapıldığı kısım.
    {

        static public int urunlerDoldur()//Urunler struct dizi olarak ayarlıyoruz ve içeriğini dolduruyoruz.
        {
            try
            {
                for (int i = 0; i < UrunBilgisi.Urunler.Count; i++)//kategori sayısı kadar donecek dongu kuruyoruz.
                {
                    OleDbDataReader oleDbDataReader = DAL.Database.veriOku("SELECT * FROM URUN WHERE KATEGORIID = " + UrunBilgisi.Urunler[i].ID); //Kategori tablosundakı Id lerin Urun tablosundaki Kategoriid esit olanları çekiyoruz.
                    while (oleDbDataReader.Read())//çekilen verileri tek tek urunlers dizisine ekliyoruz.
                    {
                        UrunBilgisi.Urunler[i].URUNLER.Add(new Urun(int.Parse(oleDbDataReader["ID"].ToString()),
                           int.Parse(oleDbDataReader["KATEGORIID"].ToString()), oleDbDataReader["URUN"].ToString(),
                           int.Parse(oleDbDataReader["SATISSAYISI"].ToString())));
                    }

                    oleDbDataReader.Close(); //oleDbDataReader kapatıyoruz.
                    DAL.Database.databaseKapat();//database baglantısını kapatıyoruz.
                }
                return UrunUrunOzellik.urunOzelliklerDoldur();
            }
            catch//hata vermesi durumunda hic birsey yapmadan programa devam etmesini istiyoruz.
            {
                DAL.Database.databaseKapat();
                return -1;
            }
        }

        static public int UrunEkle(string Urun, int KategoriID, int kategoriIndex)//Urun tablosuna veri ekleyecek fonksıyon.
        {
            int durum = -1; //sql işlem yapılmazsa -1 değeri doncegi için durum degişkenine -1 değeri atıyoruz.
            string sql = "INSERT INTO URUN(KATEGORIID,URUN,SATISSAYISI) VALUES(" + KategoriID.ToString() + ",'" + Urun + "'," + "0" + ")";
            durum = ASGJETSAT.DAL.Database.sorguYap(sql);
            DAL.Database.databaseKapat();//database kapatıyoruz.
            if (durum != -1)
            {
                sql = "SELECT TOP 1 ID FROM URUN ORDER BY ID DESC";
                OleDbDataReader oleDbDataReader = ASGJETSAT.DAL.Database.veriOku(sql);
                int id = -1;
                while (oleDbDataReader.Read())
                {
                    if ((id = int.Parse(oleDbDataReader["ID"].ToString())) != -1)
                    {
                        UrunBilgisi.Urunler[kategoriIndex].URUNLER.Add(new Urun(id, KategoriID, Urun, 0));
                        oleDbDataReader.Close();//oleDbDataReader kapatıyoruz.
                        DAL.Database.databaseKapat();//database kapatıyoruz.
                        return 0;
                    }
                }
            }
            DAL.Database.databaseKapat();
            return -1;
        }

        static public int UrunOzellikEkle(int KategoriID, int UrunID, string Ozellik, string Barkod, decimal Fiyat, int kategoriIndex, int urunIndex)//Urune ozellik eklenen fonksıyon.
        {
            int durum = -1;
            //Urun tablosuna gelen parametreri ekleyen sql sorgu.
            string sql = "INSERT INTO URUNOZELLIK(URUNID,OZELLIK,FIYAT,STOK,SATISSAYISI,BARKOD) VALUES(" + UrunID.ToString() + ",'" + Ozellik + "','" + Fiyat + "',0,0,'" + Barkod + "')";
            durum = ASGJETSAT.DAL.Database.sorguYap(sql);
            DAL.Database.databaseKapat();
            if (durum != -1)
            {
                int id = -1;
                sql = "SELECT TOP 1 ID FROM URUNOZELLIK ORDER BY ID DESC";
                OleDbDataReader oleDbDataReader = ASGJETSAT.DAL.Database.veriOku(sql);
                while (oleDbDataReader.Read())
                {
                    if ((id = int.Parse(oleDbDataReader["ID"].ToString())) != -1)
                    {
                        UrunBilgisi.Urunler[kategoriIndex].URUNLER[urunIndex].URUNOZELLIK.Add(new UrunOzellik(id,
                            UrunID, Ozellik, Fiyat, int.Parse(Barkod), 0, 0));
                        UrunBilgisi.Barkodlar.Add(new Barkodlar(int.Parse(Barkod), kategoriIndex.ToString() + "_" + urunIndex.ToString() + "_" + id.ToString()));
                        oleDbDataReader.Close();//oleDbDataReader kapatıyoruz.
                        DAL.Database.databaseKapat();//database kapatıyoruz.
                        return 0;
                    }
                }
            }
            DAL.Database.databaseKapat();
            return -1;
        }

        static public int UrunSil(int KATEGORIID, int ID, int kategoriIndex, int urunIndex)//Urun silecek fonksıyon.
        {
            DAL.Database.sorguYap("DELETE FROM URUNOZELLIK WHERE URUNID =" + ID);
            if (DAL.Database.sorguYap("DELETE FROM URUN WHERE ID =" + ID) != -1)//Urun tablosunda silincek urunun sorgusu.
            {
                UrunBilgisi.Urunler[kategoriIndex].URUNLER.RemoveAt(urunIndex);
                DAL.Database.databaseKapat();
                return 0;
            }
            DAL.Database.databaseKapat();
            return -1;
        }

        static public int UrunUrunOzellikEkle(string Urun, int KategoriID, string Ozellik,int Barkod, decimal Fiyat, int kategoriIndex, int urunIndex) //Urunun ozelligi varsa eklenecek fonskıyon.
        {//urun tablosuna veri eklencek satır.
            string sql = "INSERT INTO URUN(KATEGORIID,URUN,SATISSAYISI,BARKOD) VALUES(" + KategoriID.ToString() + ",'" + Urun + "'," + "0,'"+Barkod + "')";
            int durum = ASGJETSAT.DAL.Database.sorguYap(sql);
            DAL.Database.databaseKapat();
            int urunID = -1;
            if (durum != -1)//veri eklene bildiyse yapılacak durum.
            {
                sql = "SELECT TOP 1 ID FROM URUN ORDER BY ID DESC";
                OleDbDataReader oleDbDataReader = ASGJETSAT.DAL.Database.veriOku(sql);
                while (oleDbDataReader.Read())
                {
                    urunID = int.Parse(oleDbDataReader["ID"].ToString());
                    if (urunID != -1)//ıd bulunduysa yapılacak işlemler.
                    {//urunozellik tablosuna veri eklenecek kısım.
                        sql = "INSERT INTO URUNOZELLIK(URUNID,OZELLIK,FIYAT,STOK,SATISSAYISI) VALUES(" + urunID.ToString() + ",'" + Ozellik + "','" + Fiyat.ToString() + "',0,0)";
                        if (ASGJETSAT.DAL.Database.sorguYap(sql) != -1)
                        {
                            int ID = -1;
                            sql = "SELECT TOP 1 ID FROM URUNOZELLIK ORDER BY ID DESC";
                            OleDbDataReader oleDb = ASGJETSAT.DAL.Database.veriOku(sql);
                            while (oleDb.Read())
                            {
                                if ((ID = int.Parse(oleDb["ID"].ToString())) != -1)
                                {
                                    UrunBilgisi.Urunler[kategoriIndex].URUNLER[urunIndex].URUNOZELLIK.Add(new UrunOzellik(
                                   ID, urunID, Ozellik, Fiyat, -1, 0, 0));
                                    DAL.Database.databaseKapat();
                                    return 0;
                                }
                            }

                        }
                        DAL.Database.databaseKapat();//database kapatlıyor.
                    }
                }
            }
            return -1;
        }

        //urune çoklu özellik ekledığımız kısım.
        static public int UrunUrunOzellikEkle(string Urun, int KategoriID, string[] Ozellik, string[] Barkod, decimal[] Fiyat, int kategoriIndex)
        {//urunu urun tablosuna eklendiği kısım.
            string sql = "INSERT INTO URUN(KATEGORIID,URUN,SATISSAYISI) VALUES(" + KategoriID.ToString() + ",'" + Urun + "'," + "0" + ")";
            int durum = ASGJETSAT.DAL.Database.sorguYap(sql);
            DAL.Database.databaseKapat();
            int urunID = -1;
            if (durum != -1)//veri eklene bildiyse yapılacak durum.
            {
                sql = "SELECT TOP 1 ID FROM URUN ORDER BY ID DESC";
                OleDbDataReader oleDbDataReader = ASGJETSAT.DAL.Database.veriOku(sql);
                while (oleDbDataReader.Read())
                {
                    urunID = int.Parse(oleDbDataReader["ID"].ToString());
                    if (urunID != -1)//ıd bulunduysa yapılacak işlemler.
                    {//urunozellik tablosuna veri eklenecek kısım.
                        BL.UrunBilgisi.Urunler[kategoriIndex].URUNLER.Add(new Entities.Urun(urunID, KategoriID, Urun, 0));
                        for (int j = 0; j < Barkod.Length; j++)
                        {
                            sql = "INSERT INTO URUNOZELLIK(URUNID,OZELLIK,FIYAT,STOK,SATISSAYISI,BARKOD) VALUES(" + urunID.ToString() + ",'" + Ozellik[j] + "','" + Fiyat[j] + "',0,0,'" + Barkod[j] + "')";
                            if (ASGJETSAT.DAL.Database.sorguYap(sql) != -1)
                            {

                                int ID = -1;
                                sql = "SELECT TOP 1 ID FROM URUNOZELLIK ORDER BY ID DESC";
                                OleDbDataReader oleDb = ASGJETSAT.DAL.Database.veriOku(sql);
                                while (oleDb.Read())
                                {
                                    if ((ID = int.Parse(oleDb["ID"].ToString())) != -1)
                                    {
                                        UrunBilgisi.Urunler[kategoriIndex].URUNLER[UrunBilgisi.Urunler[kategoriIndex].URUNLER.Count-1].URUNOZELLIK.Add(new UrunOzellik(
                                       ID, urunID, Ozellik[j], Fiyat[j], int.Parse(Barkod[j]), 0, 0));
                                    }
                                }

                            }
                        }
                        DAL.Database.databaseKapat();//database kapatlıyor.
                        return 0;
                    }
                }
            }
            return -1;
        }

        //urunu ozellikleriyle duzenleyecek fonksıyon.
        static public int UrunDuzenle(int ID, int YeniKategoriID, string YeniUrun, int OZELLIKID, decimal YeniFiyat, int kategoriIndex, int urunIndex, int yenikategoriIndex)
        {//urunu guncelenen kısım.
            int durum = DAL.Database.sorguYap("UPDATE URUN SET KATEGORIID = " + YeniKategoriID + ", URUN = '" + YeniUrun + "' WHERE ID= " + ID);
            DAL.Database.databaseKapat();
            if (durum != -1)//urun guncellendıyse yapılacak olan kısım.
            {//urun ozellik kısmının guncelleyecek kısmı.
                if (kategoriIndex != yenikategoriIndex && DAL.Database.sorguYap("UPDATE URUNOZELLIK SET URUNID = " + ID +
                    ", FIYAT = " + YeniFiyat + " WHERE ID = " + OZELLIKID) != -1)
                {
                    UrunBilgisi.Urunler[kategoriIndex].URUNLER[urunIndex].URUNOZELLIK[0].URUNID = ID;
                    UrunBilgisi.Urunler[kategoriIndex].URUNLER[urunIndex].URUNOZELLIK[0].FIYAT = YeniFiyat;
                    UrunBilgisi.Urunler[yenikategoriIndex].URUNLER.AddRange(UrunBilgisi.Urunler[kategoriIndex].URUNLER);
                    UrunBilgisi.Urunler[kategoriIndex].URUNLER.Clear();
                    return 0;
                }
            }
            DAL.Database.databaseKapat();
            return -1;
        }

        //sadece urun kısmını duzenlecek kısım.
        static public int UrunDuzenle(int ID, int YeniKategoriID, string YeniUrun, int kategoriIndex, int urunIndex, int yenikategoriIndex)
        {//urunu ıd sıne göre Yeni Kategori ID ve Yeni Urun ismine göre guncelleyen kod.
            if (DAL.Database.sorguYap("UPDATE URUN SET KATEGORIID = " + YeniKategoriID + ", URUN = '" + YeniUrun +
                "' WHERE ID = " + ID) != -1)
            {
                UrunBilgisi.Urunler[kategoriIndex].URUNLER[urunIndex].URUN = YeniUrun;
                UrunBilgisi.Urunler[yenikategoriIndex].URUNLER.Add(UrunBilgisi.Urunler[kategoriIndex].URUNLER[urunIndex]);
                UrunBilgisi.Urunler[kategoriIndex].URUNLER.RemoveAt(urunIndex);
                return 0;
            }
            DAL.Database.databaseKapat();
            return -1;
        }

        public static int EnSonEklenenUrunIDBul()
        {
            OleDbDataReader oleDbDataReader = DAL.Database.veriOku("SELECT TOP 1 ID FROM URUN ORDER BY ID DESC");
            while (oleDbDataReader.Read())
            {
                return int.Parse(oleDbDataReader["ID"].ToString());
            }
            return -1;
        }
        public static int EnSonEklenenUrunOzellikIDBul()
        {
            OleDbDataReader oleDbDataReader = DAL.Database.veriOku("SELECT TOP 1 ID FROM URUNOZELLIK ORDER BY ID DESC");
            while (oleDbDataReader.Read())
            {
                return int.Parse(oleDbDataReader["ID"].ToString());
            }
            return -1;
        }
    }
}
