using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASGJETSAT.DAL
{
    public static class Database
    {
        private static OleDbConnection _oleDbConnection; //baglanti nesnesi.

        private static void databaseBaglan() //database baglanmak için gerekli kodlar.
        {
            try
            {
                _oleDbConnection = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=../../../Database.accdb");
                _oleDbConnection.Open();
            }
            catch
            {
                _oleDbConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=../../../Database.mdb");
                _oleDbConnection.Open();
            }
        }
        private static OleDbCommand komutOlustur(string sorgu) //komut oluşturmak için gerekli kodlar.
        {
            if (_oleDbConnection == null) //baglantı yoksa baglantı oluştur.
                databaseBaglan();
            if (_oleDbConnection.State == System.Data.ConnectionState.Closed) //baglantı kapalıysa baglantı ac.
                _oleDbConnection.Open();

            return new OleDbCommand(sorgu,_oleDbConnection); //yeni komut oluştur ve geri gonder.
        }
        public static OleDbDataReader veriOku(string sorgu)  //gelen sorguya göre databaseden veri çekip nesnesi oluşturur.
        {
            OleDbCommand oleDbCommand = komutOlustur(sorgu); //komut oluştur.
            OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader(); //veriyi oku oleDbDataReader çevir.
            oleDbCommand.Dispose();// komutu kapat.
            return oleDbDataReader;
        }
        public static int sorguYap(string sorgu) //gelen sorguyu çalıştırıp çalışıp çalışmadığını ögrenmek için geriye int deger donderir.
        {
            OleDbCommand oleDbCommand = komutOlustur(sorgu); //komut oluştur.
            int durum = oleDbCommand.ExecuteNonQuery();//komutu çalıştır.
            oleDbCommand.Dispose();//komutu kapat.
            return durum;
        }
        public static void databaseKapat()//database baglantısını kapat.
        {
            _oleDbConnection.Close();
        }
    }
}
