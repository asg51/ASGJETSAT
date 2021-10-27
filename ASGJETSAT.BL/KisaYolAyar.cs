using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASGJETSAT.Entities;

namespace ASGJETSAT.BL
{
    public class KisaYolAyar
    {
        private static int buble_sort()// Kabarcık sıralama (buble sort) yoluyla sayıları küçükten büyüğe sıralayan algoritmadır.
        {
            try
            {
                int id;
                string indexler;
                for (int i = 0; i < UrunBilgisi.KisaYollar.Count - 1; i++)
                {
                    for (int j = 1; j < UrunBilgisi.KisaYollar.Count - i; j++)
                    {
                        if (UrunBilgisi.KisaYollar[j].ID < UrunBilgisi.KisaYollar[j - 1].ID)
                        {
                            id = UrunBilgisi.KisaYollar[j - 1].ID;
                            indexler = UrunBilgisi.KisaYollar[j - 1].INDEXLER;

                            UrunBilgisi.KisaYollar[j - 1] = UrunBilgisi.KisaYollar[j];

                            UrunBilgisi.KisaYollar[j].ID = id;
                            UrunBilgisi.KisaYollar[j].INDEXLER = indexler;
                        }
                    }
                }

                return 0;
            }
            catch
            {

            }
            return -1;
        }

        public static void KisaYolYenile()
        {
            
            buble_sort();
           //KisaYolKontrol();
        }

        private static void KisaYolKontrol()
        {
            foreach (ConnectionStringSettings connection in System.Configuration.ConfigurationManager.ConnectionStrings)
            {
                try
                {
                    if (hizliArama(int.Parse(ConfigurationManager.ConnectionStrings[connection.Name].ConnectionString)) == "-1")
                    {
                        var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
                        connectionStringsSection.ConnectionStrings[connection.Name].ConnectionString = "-1";
                        config.Save(ConfigurationSaveMode.Modified);
                        ConfigurationManager.RefreshSection("connectionStrings");
                    }
                }
                catch
                {

                }
            }
        }

        public static string hizliArama(int id)
        {
            int eb = UrunBilgisi.KisaYollar.Count;
            int ek = -1;

            while (eb - ek > 1)
            {
                int bakilan = (eb + ek) / 2;
                if (UrunBilgisi.KisaYollar[bakilan].ID == id)
                {
                    return UrunBilgisi.KisaYollar[bakilan].INDEXLER;
                }
                else if (UrunBilgisi.KisaYollar[bakilan].ID < id)
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
