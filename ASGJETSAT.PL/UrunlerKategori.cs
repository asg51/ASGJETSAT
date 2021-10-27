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
using ASGJETSAT.BL;
using ASGJETSAT.Entities;

namespace ASGJETSAT.PL
{
    public partial class UrunlerKategori : Form
    {
        public UrunlerKategori()
        {

        }

        private void btn_KategoriEkleSil_Click(object sender, EventArgs e) //txt_KategoriEkle ıcını bosaltmak için kullanılacak.
        {
            txt_KategoriEkle.Text = "";
        }

        private void btn_KategoriEkleEkle_Click(object sender, EventArgs e)//kategori tablosuna veri eklemek için kullanılan bolum.
        {
            string data = "";

            if (txt_KategoriEkle.Text != "")
            {
                data = txt_KategoriEkle.Text;

                int durum = BL.UrunKategori.KategoriEkle(data);

                if (durum != -1)
                {
                    Sayfalar.jETSAT.KategoriEkle(UrunBilgisi.Urunler.Count - 1);
                    MessageBox.Show("Kategori Eklenmiştir");
                }
                else
                    MessageBox.Show("Hata Eklenemedi Yeniden Deneyiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

                kategoriCmbxDoldur();// veri eklendikten sonra yeniden comboboxları dolduruyoruz.
            }
            else
                MessageBox.Show("Hata Eklenemedi Yeniden Deneyiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

            txt_KategoriEkle.Text = ""; //txt_KategoriEkle içini sıfırlar.
        }

        private void btn_KategoriDuzenleSil_Click(object sender, EventArgs e)//kategor duzenle sil sadece comboboxları selected index ayarlar ve txt_KategoriDuzenleYeniAd ayarlar.
        {
            try
            {
                cmbx_KategoriDuzenle.SelectedIndex = 0;
            }
            catch
            {
                cmbx_KategoriDuzenle.SelectedIndex = -1;
            }

            txt_KategoriDuzenleYeniAd.Text = cmbx_KategoriDuzenle.SelectedText;
        }

        private void btn_KategoriDuzenleEkle_Click(object sender, EventArgs e)// duzenlenmış urunu databasede gunceller.
        {
            string data = "";

            if (txt_KategoriDuzenleYeniAd.Text != "")//txt_KategoriDuzenleYeniAd bos degilse işleme devam eder.
            {
                data = txt_KategoriDuzenleYeniAd.Text;

                int durum = BL.UrunKategori.KategoriGuncelle(data, BL.UrunBilgisi.Urunler[cmbx_KategoriDuzenle.SelectedIndex].ID, cmbx_KategoriDuzenle.SelectedIndex);
                //duzenlenmis urunu databasede gunceller. 

                if (durum != -1)
                {
                    Sayfalar.jETSAT.KategoriGuncelle(cmbx_KategoriDuzenle.SelectedIndex);
                    
                    MessageBox.Show("Kategori Güncellenmiştir");
                }
                else
                    MessageBox.Show("Hata Güncellenemedi Yeniden Deneyiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

                kategoriCmbxDoldur();// veri eklendikten sonra yeniden comboboxları dolduruyoruz.
            }
            else
                MessageBox.Show("Hata Eklenemedi Yeniden Deneyiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void UrunlerKategori_Load(object sender, EventArgs e)
        {
            //comboboxları dolduruyoruz.
        }
        void kategoriCmbxDoldur()
        {
            cmbx_KategoriDuzenle.Items.Clear();/*comboboxların içlerini temizler*/
            cmbx_KategoriSil.Items.Clear();

            for (int i = 0; i < BL.UrunBilgisi.Urunler.Count; i++)/*comboboxları doldurudugumuz kısım.*/
            {
                cmbx_KategoriDuzenle.Items.Add(BL.UrunBilgisi.Urunler[i].KATEGORI.ToString());
                cmbx_KategoriSil.Items.Add(BL.UrunBilgisi.Urunler[i].KATEGORI.ToString());
            }

            try/*comboboxların selected indexlerin ayarı yapıldıgı kısım.*/
            {
                cmbx_KategoriDuzenle.SelectedIndex = 0;
            }
            catch
            {
                cmbx_KategoriDuzenle.SelectedIndex = -1;
            }
            try
            {
                cmbx_KategoriSil.SelectedIndex = 0;
            }
            catch
            {
                cmbx_KategoriSil.SelectedIndex = -1;
            }
        }

        private void btn_KategoriSilSil_Click(object sender, EventArgs e)/*eski haline getirecegimiz icin cmbx_KategoriSil seleced index ayarı yapılıyor.*/
        {
            try
            {
                cmbx_KategoriSil.SelectedIndex = 0;
            }
            catch
            {
                cmbx_KategoriSil.SelectedIndex = -1;
            }
        }

        private void btn_KategoriSilEkle_Click(object sender, EventArgs e)/*kategori silmek için kullanılan kısım.*/
        {
            int durum = UrunKategori.KategorSil(BL.UrunBilgisi.Urunler[cmbx_KategoriSil.SelectedIndex].ID, cmbx_KategoriSil.SelectedIndex);//silme fonksiyonu çalıştırıldı.

            if (durum != -1)
            {
                Sayfalar.jETSAT.KategoriSil(cmbx_KategoriSil.SelectedIndex);
                MessageBox.Show("Kategori Silinmiştir");
            }
            else
                MessageBox.Show("Hata Silinmemedi Yeniden Deneyiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

            GC.SuppressFinalize(durum);
            kategoriCmbxDoldur();
        }

        private void btnGeri_Click(object sender, EventArgs e)//anasayfaya geri donder.
        {
            Sayfalar.anaSayfa.Show();
            this.Hide();
        }

        private void btnStok_Click(object sender, EventArgs e)//stok sayfasına gider.
        {
            Sayfalar.urunlerStok.Show();
            this.Hide();
        }

        private void btnUrun_Click(object sender, EventArgs e)//urun sayfasına gıder.
        {
            Sayfalar.urunlerUrun.Show();
            this.Hide();
        }

        private void btnOzellik_Click(object sender, EventArgs e)//urunozellik sayfasına gider.
        {
            Sayfalar.urunlerOzellik.Show();
            this.Hide();
        }

        private void btn_KisaYol_Click(object sender, EventArgs e)
        {
            Sayfalar.urunlerKisaYol.Show();
            this.Hide();
        }

        private void UrunlerKategori_Shown(object sender, EventArgs e)
        {
            kategoriCmbxDoldur();
        }
    }
}
