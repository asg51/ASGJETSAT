using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASGJETSAT.BL
{
    static public class UrunMuhasebe
    {

        static public decimal[] aylikUcretGetir(int yil)// İstenen yıla göre aylık ücretleri hesaplayıp gönderen fonksiyon.
        {
            decimal[] ucretler = new decimal[12];//12 ay olduğu için ucretler isminde float bir 12 lik dizi oluşturuduk.
            for (int i = 0; i < 12; i++)//ucretler dizisinin tüm elemanlarını 0 degeri atiyoruz.
                ucretler[i] = 0;

            for (int i = 0; i < UrunBilgisi.Satislar.Count; i++) //satis struct uzunluğu kadar donuyor.
            {
                for (int j = 1; j < 13; j++) //ay sayısı kadar donderiyoruz
                {
                    //eger satıs dizisinin içindeki ay ile j değişkeni içindeki değer aynı ve satiş dizisindeki yıl ile fonk içindeki parametre aynı ise 
                    //ucretler dizisine satis dizisindeki fiyatida ekliyoruz.
                    if (UrunBilgisi.Satislar[i].TARIH.Month == j && yil == UrunBilgisi.Satislar[i].TARIH.Year)
                    {
                        ucretler[j - 1] += UrunBilgisi.Satislar[i].FIYAT;
                    }
                }
                //istenilen yıldaki tüm ayların fiyatşları bulunuyor.
            }
            for (int i = 0; i < UrunBilgisi.Iptaller.Count; i++) //satis struct uzunluğu kadar donuyor.
            {
                for (int j = 1; j < 13; j++) //ay sayısı kadar donderiyoruz
                {
                    //eger satıs dizisinin içindeki ay ile j değişkeni içindeki değer aynı ve satiş dizisindeki yıl ile fonk içindeki parametre aynı ise 
                    //ucretler dizisine satis dizisindeki fiyatida ekliyoruz.
                    if (UrunBilgisi.Iptaller[i].TARIH.Month == j && yil == UrunBilgisi.Iptaller[i].TARIH.Year)
                    {
                        ucretler[j - 1] -= UrunBilgisi.Iptaller[i].FIYAT;
                    }
                }
                //istenilen yıldaki tüm ayların fiyatşları bulunuyor.
            }
            return ucretler;
        }
        static public decimal[] aylikUcretGetir(int ay1, int yil1, int ay2, int yil2)// 2. aylikUcretGetir fonksiyonuda aynı mantıkta çalışmakta lakin bu fonksiyon istenen 
                                                                                   //tarihler arasi ucretleri getiriyor.
        {
            DateTime dateTime1 = new DateTime(yil1, ay1, 1);//girilen ay ve yil degerine göre ilk tarih oluşturuluyor.
            DateTime dateTime2 = new DateTime(yil2, ay2, 1);//girilen ay ve yil degerine göre ikinci tarih oluşturuluyor.

            int sayac = 0;// tarihler arası ayları tutmak için tanımlandı
            for (DateTime dateTime = dateTime1; dateTime <= dateTime2.AddMonths(1);)//ilk tarih ile ikinci tarih arasında dongu kuruyoruz ve her dongude bir ay artıyoruz ay sayısı bulunuyor.
            {
                sayac++;
                dateTime = dateTime.AddMonths(1);
            }
            decimal[] ucretler = new decimal[sayac]; //sayac kadar ucretler dizi float dizi oluşturuyoruz
            for (int i = 0; i < sayac; i++)//diziyi 0'lıyoruz.
            {
                ucretler[i] = 0;
            }


            sayac = 0;//sayacı index olarak kullanmak için 0 atıyoruz.
            for (DateTime dateTime = dateTime1; dateTime <= dateTime2;)//yine iki ay arasında döngü kuruyoruz ay ay artıyoruz.
            {
                for (int i = 0; i < UrunBilgisi.Satislar.Count; i++) //satıs nesnesi kadar dönecek döngü kuruyoruz.
                {
                    if (UrunBilgisi.Satislar[i].TARIH.Month == dateTime.Month && dateTime.Year == UrunBilgisi.Satislar[i].TARIH.Year) //aylarile yıllar bizim istediğim tarihler arasındaysa ucretler üzerine fiyati eklıyoruz.
                        ucretler[sayac] += UrunBilgisi.Satislar[i].FIYAT;
                    
                }
                sayac++;
                dateTime = dateTime.AddMonths(1);
            }

            sayac = 0;//sayacı index olarak kullanmak için 0 atıyoruz.
            for (DateTime dateTime = dateTime1; dateTime <= dateTime2;)//yine iki ay arasında döngü kuruyoruz ay ay artıyoruz.
            {
                for (int i = 0; i < UrunBilgisi.Iptaller.Count; i++) //satıs nesnesi kadar dönecek döngü kuruyoruz.
                {
                    if (UrunBilgisi.Iptaller[i].TARIH.Month == dateTime.Month && dateTime.Year == UrunBilgisi.Iptaller[i].TARIH.Year) //aylarile yıllar bizim istediğim tarihler arasındaysa ucretler üzerine fiyati eklıyoruz.
                        ucretler[sayac] -= UrunBilgisi.Iptaller[i].FIYAT;

                }
                sayac++;
                dateTime = dateTime.AddMonths(1);
            }

            return ucretler;
        }

        static public decimal[] yillikUcretGetir(int yil1, int yil2)//yıllar arası ücretleri bulan fonksiyon
        {
            DateTime dateTime1 = new DateTime(yil1, 1, 1);//girilen ay ve yil degerine göre ilk tarih oluşturuluyor.
            DateTime dateTime2 = new DateTime(yil2, 1, 1);//girilen ay ve yil degerine göre ikinci tarih oluşturuluyor.


            int sayac = 0; //toplam yıl sayısını tutmak için tanımlandı.
            for (DateTime dateTime = dateTime1; dateTime <= dateTime2.AddMonths(1);)//iki tarih arasında döngü her döndüğünde sayac 1 artırılıp tarihler 1 sene ileri alınıyor.
            {
                sayac++;
                dateTime = dateTime.AddYears(1);
            }

            decimal[] ucretler = new decimal[sayac]; //toplam yıl sayısı kadar ucretler diye dizi oluşturuldu.
            for (int i = 0; i < sayac; i++)//dizi 0'lar atandı.
            {
                ucretler[i] = 0;
            }

            sayac = 0; //sayacı index olarak kullanmak için 0 atandı.
            for (DateTime dateTime = dateTime1; dateTime <= dateTime2;) //iki tarih arası döngü kuruldu.
            {
                for (int i = 0; i < UrunBilgisi.Satislar.Count; i++)//satıs sayısı kadar döngü kuruldu.
                {
                    if (dateTime.Year == UrunBilgisi.Satislar[i].TARIH.Year) //satıs nesnesindeki yıl ile datetime nesnesinde yıl aynı ıse ucretlere fıyat eklendi.
                        ucretler[sayac] += UrunBilgisi.Satislar[i].FIYAT;
                }
                sayac++;
                dateTime = dateTime.AddYears(1);// dateime 1 yıl ileri alındı.
            }

            sayac = 0; //sayacı index olarak kullanmak için 0 atandı.
            for (DateTime dateTime = dateTime1; dateTime <= dateTime2;) //iki tarih arası döngü kuruldu.
            {
                for (int i = 0; i < UrunBilgisi.Iptaller.Count; i++)//satıs sayısı kadar döngü kuruldu.
                {
                    if (dateTime.Year == UrunBilgisi.Iptaller[i].TARIH.Year) //satıs nesnesindeki yıl ile datetime nesnesinde yıl aynı ıse ucretlere fıyat eklendi.
                        ucretler[sayac] -= UrunBilgisi.Iptaller[i].FIYAT;
                }
                sayac++;
                dateTime = dateTime.AddYears(1);// dateime 1 yıl ileri alındı.
            }

            return ucretler;
        }

        static public decimal[] gunlukUcretGetir(int ay, int yil)//girilen ay ve yıl değerine göre o yıl ve aydaki günlük ücretleri gösteriyor.
        {
            DateTime dateTime1 = new DateTime(yil, ay, 1);//İlk tarih oluşturuluyor.
            DateTime dateTime2 = dateTime1.AddMonths(1);//İkinci tarih birinci tarihin 1 ay fazlası olarak eklendi.
            dateTime2 = dateTime2.AddDays(-1);// ikinci ayı 1 gün azaltlık.

            int sayac = 0; //günlük ücret istenen ayın gününü tutulması için tanımlandı.
            for (DateTime dateTime = dateTime1; dateTime <= dateTime2;) //iki tarih arası döngü oluşturuldu ve gün sayısı bulundu.
            {
                sayac++;
                dateTime = dateTime.AddDays(1);
            }

            decimal[] ucretler = new decimal[sayac]; //gün sayısı kadar ucretler dizisi oluşturuldu.
            for (int i = 0; i < sayac; i++)//ucretler dizi 0'landı.
            {
                ucretler[i] = 0;
            }

            sayac = 0; //index olarak kullanılmak üzere 0 değeri atandı.
            for (DateTime dateTime = dateTime1; dateTime <= dateTime2;)//iki tarih arası döngü kuruldu.
            {
                for (int i = 0; i < UrunBilgisi.Satislar.Count; i++)//satıs sayısı kadar dongu kuruldu.
                {//satıs nesnesi ile dateTime nesnesi arasında gün, ay ve yıl aynı ise ucretler dizisine fiyat eklendi.
                    if (dateTime.Year == UrunBilgisi.Satislar[i].TARIH.Year && dateTime.Month == UrunBilgisi.Satislar[i].TARIH.Month && dateTime.Day == UrunBilgisi.Satislar[i].TARIH.Day)
                        ucretler[sayac] += UrunBilgisi.Satislar[i].FIYAT;
                }
                sayac++;
                dateTime = dateTime.AddDays(1);
            }


            sayac = 0; //index olarak kullanılmak üzere 0 değeri atandı.
            for (DateTime dateTime = dateTime1; dateTime <= dateTime2;)//iki tarih arası döngü kuruldu.
            {
                for (int i = 0; i < UrunBilgisi.Iptaller.Count; i++)//satıs sayısı kadar dongu kuruldu.
                {//satıs nesnesi ile dateTime nesnesi arasında gün, ay ve yıl aynı ise ucretler dizisine fiyat eklendi.
                    if (dateTime.Year == UrunBilgisi.Iptaller[i].TARIH.Year && dateTime.Month == UrunBilgisi.Iptaller[i].TARIH.Month && dateTime.Day == UrunBilgisi.Iptaller[i].TARIH.Day)
                        ucretler[sayac] -= UrunBilgisi.Iptaller[i].FIYAT;
                }
                sayac++;
                dateTime = dateTime.AddDays(1);
            }
           

            return ucretler;
        }

        static public decimal[] gunlukUcretGetir(int gun1, int ay1, int yil1, int gun2, int ay2, int yil2)//istenilen iki tarih arasi günlük ücret bulan fonksiyon.
        {
            DateTime dateTime1 = new DateTime(yil1, ay1, gun1);//Birinci tarih oluşturuldu.
            DateTime dateTime2 = new DateTime(yil2, ay2, gun2);//İkinci tarih oluşturuldu.

            int sayac = 0;//tarihler arasi toplam gün sayısı tutulması için tanımlandı.
            for (DateTime dateTime = dateTime1; dateTime <= dateTime2;) //İki tarih arasinda döngü kuruldu toplam gün sayısı bulundu.
            {
                sayac++;
                dateTime = dateTime.AddDays(1);
            }

            decimal[] ucretler = new decimal[sayac];//toplam gün sayısı kadar ucretler dizi oluşturuldu.
            for (int i = 0; i < sayac; i++)//ucretler dizisi 0'landı.
            {
                ucretler[i] = 0;
            }

            sayac = 0;//index olarak kullanılmak üzere 0 atandı.
            for (DateTime dateTime = dateTime1; dateTime <= dateTime2;)//iki tarih arasinda döngü kuruldu.
            {
                for (int i = 0; i < UrunBilgisi.Satislar.Count; i++)//satıs sayısı kadar döngü kuruldu.
                {//satıs nesnesi ve dateTime nesnesi tarihleri içinde gün, ay ve yıl esit ise ucretler fiyat eklendi.
                    if (dateTime.Year == UrunBilgisi.Satislar[i].TARIH.Year && dateTime.Month == UrunBilgisi.Satislar[i].TARIH.Month && dateTime.Day == UrunBilgisi.Satislar[i].TARIH.Day)
                        ucretler[sayac] += UrunBilgisi.Satislar[i].FIYAT;
                }
                sayac++;
                dateTime = dateTime.AddDays(1);
            }

            sayac = 0;//index olarak kullanılmak üzere 0 atandı.
            for (DateTime dateTime = dateTime1; dateTime <= dateTime2;)//iki tarih arasinda döngü kuruldu.
            {
                for (int i = 0; i < UrunBilgisi.Iptaller.Count; i++)//satıs sayısı kadar döngü kuruldu.
                {//satıs nesnesi ve dateTime nesnesi tarihleri içinde gün, ay ve yıl esit ise ucretler fiyat eklendi.
                    if (dateTime.Year == UrunBilgisi.Iptaller[i].TARIH.Year && dateTime.Month == UrunBilgisi.Iptaller[i].TARIH.Month && dateTime.Day == UrunBilgisi.Iptaller[i].TARIH.Day)
                        ucretler[sayac] -= UrunBilgisi.Iptaller[i].FIYAT;
                }
                sayac++;
                dateTime = dateTime.AddDays(1);
            }

            return ucretler;
        }
    }
}
