using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ASGJETSAT.PL
{
    public partial class Muhasebe : Form
    {

        public Muhasebe()
        {

        }

        public void Muhasebe_Load(object sender, EventArgs e)
        {
            txtGunlukAy.Text = DateTime.Now.Month.ToString();//o anki ayi txtGunlukAy gosteriyor.
            txtGunlukYil.Text = DateTime.Now.Year.ToString();//o anki yılı txtGunlukYil gosteriyor.
            txtAylikYil.Text = DateTime.Now.Year.ToString();//o anki yılı txtAylikYil gosteriyor.

            //aylık ücreti gosteriyor.
            chartAylikEkle(BL.UrunMuhasebe.aylikUcretGetir(DateTime.Now.Year), new DateTime(DateTime.Now.Year, 1, 1));

            SatanlarıBul();
        }

        void SatanlarıBul()
        {
            Thread thread1 = new Thread(new ThreadStart(kategoriEncokSatan));
            Thread thread2 = new Thread(new ThreadStart(urunEncokSatan));
            Thread thread3 = new Thread(new ThreadStart(urunOzellikEncokSatan));

            thread1.Start();
            thread2.Start();
            thread3.Start();

            thread1.Join();
            thread2.Join();
            thread3.Join();
        }

        void kategoriEncokSatan()
        {
            int enfazla = 0;
            string kategori = "";

            for (int i = 0; i < BL.UrunBilgisi.Urunler.Count; i++)
            {
                if (enfazla < BL.UrunBilgisi.Urunler[i].SATISSAYISI)
                {
                    enfazla = BL.UrunBilgisi.Urunler[i].SATISSAYISI;
                    kategori = BL.UrunBilgisi.Urunler[i].KATEGORI;
                }
            }

            lbl_KategoriAdet.Text = enfazla.ToString() + " Adet İle";
            lbl_KategoriSatan.Text = kategori;
        }

        void urunEncokSatan()
        {
            int enfazla = 0;
            string kategori = "";

            for (int i = 0; i < BL.UrunBilgisi.Urunler.Count; i++)
            {
                for (int j = 0; j < BL.UrunBilgisi.Urunler[i].URUNLER.Count; j++)
                {
                    if (enfazla < BL.UrunBilgisi.Urunler[i].URUNLER[j].SATISSAYISI)
                    {
                        enfazla = BL.UrunBilgisi.Urunler[i].URUNLER[j].SATISSAYISI;
                        kategori = BL.UrunBilgisi.Urunler[i].URUNLER[j].URUN;
                    }
                }
            }

            lbl_UrunAdet.Text = enfazla.ToString() + " Adet İle";
            lbl_UrunSatan.Text = kategori;
        }

        void urunOzellikEncokSatan()
        {
            int enfazla = 0;
            string kategori = "";

            for (int i = 0; i < BL.UrunBilgisi.Urunler.Count; i++)
            {
                for (int j = 0; j < BL.UrunBilgisi.Urunler[i].URUNLER.Count; j++)
                {
                    for (int k = 0; k < BL.UrunBilgisi.Urunler[i].URUNLER[j].URUNOZELLIK.Count; k++)
                    {
                        if (enfazla < BL.UrunBilgisi.Urunler[i].URUNLER[j].URUNOZELLIK[k].SATISSAYISI)
                        {
                            if (BL.UrunBilgisi.Urunler[i].URUNLER[j].URUNOZELLIK[k].OZELLIK == "")
                                kategori = BL.UrunBilgisi.Urunler[i].URUNLER[j].URUN;
                            else
                                kategori = BL.UrunBilgisi.Urunler[i].URUNLER[j].URUNOZELLIK[k].OZELLIK;

                            enfazla = BL.UrunBilgisi.Urunler[i].URUNLER[j].URUNOZELLIK[k].SATISSAYISI;
                        }
                    }
                }
            }

            lbl_UrunOzellikAdet.Text = enfazla.ToString() + " Adet İle";
            lbl_UrunOzellikSatan.Text = kategori;
        }

        void chartAylikEkle(decimal[] aylikUcretler, DateTime dateTime)//aylık ücret gosteren fonksiyon.
        {
            chart1.Series.Clear();//chart nesnesini temizliyoruz.
            var series1 = new System.Windows.Forms.DataVisualization.Charting.Series//chart nesnesine yeni series oluşturuyoruz.
            {
                Name = "Series2",
                Color = System.Drawing.Color.Green,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Column
            };
            series1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.DiagonalRight;
            series1.BackSecondaryColor = System.Drawing.Color.Cyan;
            series1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));

            this.chart1.Series.Add(series1);//seriesi charta ekliyoruz.

            for (int i = 0; i < aylikUcretler.Length; i++)//aylikucretler dizisin uzulugu kadar donen bi dongu kuruyoruz.
            {
                series1.Points.AddXY(dateTime.Month.ToString() + "." + dateTime.Year.ToString(), aylikUcretler[i]);//charta x kordınatina tarihi y kordınatına ucreti eklıyoruz.
                dateTime = dateTime.AddMonths(1);//1 ay uzatıyoruz.
            }
            chart1.Invalidate();//chart yeniliyoruz.
        }

        void chartYillikEkle(decimal[] yillikUcretler, DateTime dateTime)//yıllık ücret gosteren fonksiyon.
        {
            chart1.Series.Clear();//chart nesnesini temizliyoruz.
            var series1 = new System.Windows.Forms.DataVisualization.Charting.Series//chart nesnesine yeni series oluşturuyoruz.
            {
                Name = "Series2",
                Color = System.Drawing.Color.Green,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Column
            };
            series1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.DiagonalRight;
            series1.BackSecondaryColor = System.Drawing.Color.Cyan;
            series1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));

            this.chart1.Series.Add(series1);//seriesi charta ekliyoruz.

            for (int i = 0; i < yillikUcretler.Length; i++)//yıllıkucretler dizisin uzulugu kadar donen bi dongu kuruyoruz.
            {
                series1.Points.AddXY(dateTime.Month.ToString() + "." + dateTime.Year.ToString(), yillikUcretler[i]);//charta x kordınatina tarihi y kordınatına ucreti eklıyoruz.
                dateTime = dateTime.AddYears(1);//1 yıl uzatıyoruz.
            }
            chart1.Invalidate();//chart yeniliyoruz.
        }

        void chartGunlukEkle(decimal[] gunlukUcretler, DateTime dateTime)//gunluk ücret gosteren fonksiyon.
        {
            chart1.Series.Clear();//chart nesnesini temizliyoruz.
            var series1 = new System.Windows.Forms.DataVisualization.Charting.Series//chart nesnesine yeni series oluşturuyoruz.
            {
                Name = "Series2",
                Color = System.Drawing.Color.Green,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Column
            };
            series1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.DiagonalRight;
            series1.BackSecondaryColor = System.Drawing.Color.Cyan;
            series1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));

            this.chart1.Series.Add(series1);//seriesi charta ekliyoruz.

            for (int i = 0; i < gunlukUcretler.Length; i++)//gunlukucretler dizisin uzulugu kadar donen bi dongu kuruyoruz.
            {
                series1.Points.AddXY(dateTime.Day.ToString() + "." + dateTime.Month.ToString() + "." + dateTime.Year.ToString(), gunlukUcretler[i]);//charta x kordınatina tarihi y kordınatına ucreti eklıyoruz.
                dateTime = dateTime.AddDays(1);//1 gun uzatıyoruz.
            }
            chart1.Invalidate();//chart yeniliyoruz.
        }

        Point? prevPosition = null;//point null degeri almadıgı için ? ile aldırıyoruz.
        ToolTip tooltip = new ToolTip();//ekranda gostermek için tooltip oluşturuyoruz.

        private void chart1_MouseMove(object sender, MouseEventArgs e)//chart uzerıne fare gelince yapılacak işlemler.
        {
            var pos = e.Location; //farenın lokasyonunu alıyoruz.
            if (prevPosition.HasValue && pos == prevPosition.Value)//chart grafiklerinin içindeki değer uzerinde olup olmadıgını kontrol ediyoruz.
                return;
            tooltip.RemoveAll();//ekranda gosterecek nesneyi temizliyoruz.
            prevPosition = pos; //prevPosition farenin lokasyonunu tutan pos u degerini verdik.
            var results = chart1.HitTest(pos.X, pos.Y, false, ChartElementType.DataPoint); // set ChartElementType.PlottingArea for full area, not only DataPoints

            foreach (var result in results)//result ları tek tek kontrol ettık.
            {
                if (result.ChartElementType == ChartElementType.DataPoint) // set ChartElementType.PlottingArea for full area, not only DataPoints
                {
                    double enbuyuk = 0;
                    bool durum = true;
                    for (int i = pos.Y; i >= 0 && durum; i--)
                    {
                        if (result.ChartArea.AxisY.PixelPositionToValue(i) > enbuyuk)
                            enbuyuk = result.ChartArea.AxisY.PixelPositionToValue(i);

                        var results1 = chart1.HitTest(pos.X, i, false, ChartElementType.DataPoint); // set ChartElementType.PlottingArea for full area, not only DataPoints

                        foreach (var result1 in results1)
                        {
                            if (result1.ChartElementType != ChartElementType.DataPoint) // set ChartElementType.PlottingArea for full area, not only DataPoints
                            {
                                durum = false;
                                break;//farenin ustunde durdugu x eksenindeki degerin y de en fazla kaç oludgunu bulunca donguyu kır.
                            }
                        }

                    }
                    tooltip.Show((enbuyuk).ToString(), chart1, pos.X, pos.Y - 15);//ekranda enbuyuk degeri goster.
                }
            }
        }

        private void btnAylikGoster_Click(object sender, EventArgs e)//girilen degerlere göre aylık gosteren button click eventi
        {
            try
            {
                chartAylikEkle(BL.UrunMuhasebe.aylikUcretGetir(int.Parse(txtAylikYil.Text)), new DateTime(int.Parse(txtAylikYil.Text), 1, 1));
            }
            catch
            {
                MessageBox.Show("Hata Yıl Hatalı Girdiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAylikGosterAralıklı_Click(object sender, EventArgs e)//girilen degerlere göre aralıklı aylık gosteren button click eventi
        {
            try
            {
                chartAylikEkle(BL.UrunMuhasebe.aylikUcretGetir(int.Parse(txtAylikAy1.Text), int.Parse(txtAylikYil1.Text),
                    int.Parse(txtAylikAy2.Text), int.Parse(txtAylikYil2.Text)), new DateTime(int.Parse(txtAylikYil1.Text), int.Parse(txtAylikAy1.Text), 1));
            }
            catch
            {
                MessageBox.Show("Hata Eksik Bilgi Girdiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnYıllıkGoster_Click(object sender, EventArgs e)//girilen degerlere göre yıllık gosteren button click eventi
        {
            try
            {
                chartYillikEkle(BL.UrunMuhasebe.yillikUcretGetir(int.Parse(txtYıllıkYıl1.Text), int.Parse(txtYıllıkYıl2.Text))
                    , new DateTime(int.Parse(txtYıllıkYıl1.Text), 1, 1));
            }
            catch
            {
                MessageBox.Show("Hata Eksik Bilgi Girdiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGunlukGoster_Click(object sender, EventArgs e)//girilen degerlere göre gunluk gosteren button click eventi
        {
            try
            {
                chartGunlukEkle(BL.UrunMuhasebe.gunlukUcretGetir(int.Parse(txtGunlukAy.Text), int.Parse(txtGunlukYil.Text))
                    , new DateTime(int.Parse(txtGunlukYil.Text), int.Parse(txtGunlukAy.Text), 1));
            }
            catch
            {
                MessageBox.Show("Hata Eksik Bilgi Girdiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGunlukAralıklıGoster_Click(object sender, EventArgs e)//girilen degerlere göre aralıklı gunluk gosteren button click eventi
        {
            try
            {
                chartGunlukEkle(BL.UrunMuhasebe.gunlukUcretGetir(dateTimePicker1.Value.Day, dateTimePicker1.Value.Month, dateTimePicker1.Value.Year,
                    dateTimePicker2.Value.Day, dateTimePicker2.Value.Month, dateTimePicker2.Value.Year)
                    , new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day));
            }
            catch
            {
                MessageBox.Show("Hata Eksik Bilgi Girdiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGeri_Click(object sender, EventArgs e)//anasayfaya geri donme tuşu.
        {
            Sayfalar.anaSayfa.Show();
            GC.Collect();
            this.Hide();
        }
    }


}
