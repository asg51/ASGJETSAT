using System;
using System.Windows.Forms;

namespace ASGJETSAT.PL
{
    public partial class Ayarlar : Form
    {

        public Ayarlar()
        {

        }

        private void btn_EkleCikis_Click(object sender, EventArgs e) //kullanıcı ekle panleindeki kullanıcı adı ve sifre txtlerini bos olarak ayarlar.
        {
            txt_EkleKullanıcıAdı.Text = "";
            txt_EkleKullanıcıSifre.Text = "";
        }

        private void btn_EkleOnayla_Click(object sender, EventArgs e)  //kullanıcı ekleme yeri.
        {
            if (BL.KullanıcıAyar.KullanıcıEkle(txt_EkleKullanıcıAdı.Text, txt_EkleKullanıcıSifre.Text) != -1)
            {  //kullanıcı bilgilerini kullanıcı ayar nesnesindeki kullanıcıekle fonksiyonuna gonderiyoruz.
                MessageBox.Show("Başarılı Bir Şekilde Eklenmiştir.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CmbxDoldur(); //comboboxların içini yeniden olduruyoruz.
            }
            else
            {
                MessageBox.Show("Hata Eklenemedi!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            txt_EkleKullanıcıAdı.Text = ""; //txtleri yeniden içini sıfırlıyoruz.
            txt_EkleKullanıcıSifre.Text = "";

            CmbxIndexAyar(); //comboboxların select index ayarlarlarını yapıyoruz.
        }

        private void btn_SilCikis_Click(object sender, EventArgs e) //kullanıcı sil panelindeki cıkıs butonu basılırsa o panele ait combobox select index ayarı.
        {
            try
            {
                cmbx_DuzenleKullanıcıAdı.SelectedIndex = 0;
            }
            catch
            {
                cmbx_DuzenleKullanıcıAdı.SelectedIndex = -1;
            }
        }

        private void btn_SilOnayla_Click(object sender, EventArgs e)//kullanıcı sil onayla buttonu.
        {
            try
            {
                if (BL.UrunBilgisi.Kullanıcılar[cmbx_SilKullanıcıAdı.SelectedIndex].ID != -1)  //kullanıcı bilgilerini kullanıcı ayar nesnesindeki kullanıcısil fonksiyonuna gonderiyoruz.
                {
                    MessageBox.Show("Başarılı Bir Şekilde Silinmiştir", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CmbxDoldur(); //comboboxların içini yeniden olduruyoruz.
                }
                else
                {
                    MessageBox.Show("Hata Silinemedi!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch
            {
                MessageBox.Show("Hata Silinemedi!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            CmbxIndexAyar();//comboboxların select index ayarlarlarını yapıyoruz.
        }

        private void Ayarlar_Load(object sender, EventArgs e)
        {
            CmbxDoldur();//comboboxların içini yeniden olduruyoruz.
            CmbxIndexAyar();//comboboxların select index ayarlarlarını yapıyoruz.
        }

        void CmbxDoldur()
        {
            cmbx_DuzenleKullanıcıAdı.Items.Clear();// comboboxları içini temizliyor.
            cmbx_SilKullanıcıAdı.Items.Clear();

            for (int i = 0; i < BL.UrunBilgisi.Kullanıcılar.Count; i++) //kullanıcılars struct uzunluğu kadar donen dongu kuruyoruz.
            {
                cmbx_DuzenleKullanıcıAdı.Items.Add(BL.UrunBilgisi.Kullanıcılar[i].KULLANICI); //comboboxları tek tek kullanıcılars strcut KULLANICI ile dolduruyoruz.
                cmbx_SilKullanıcıAdı.Items.Add(BL.UrunBilgisi.Kullanıcılar[i].KULLANICI);
            }

            CmbxIndexAyar(); //comboboxlara index ayarı yapıyoruz.
        }

        void CmbxIndexAyar()
        {
            try //cmbx_SilKullanıcıAdı indexi 0 ayarlıyoruz ilk kullanıcıyı gostermesi için
            {
                cmbx_SilKullanıcıAdı.SelectedIndex = 0;
            }
            catch// eger cmbx_SilKullanıcıAdı veri yok ise selected index 0 oluyacak hata verecek -1 olarak ayarlıyoruz.
            {
                cmbx_SilKullanıcıAdı.SelectedIndex = -1;
            }

            try//cmbx_DuzenleKullanıcıAdı indexi 0 ayarlıyoruz ilk kullanıcıyı gostermesi için
            {
                cmbx_DuzenleKullanıcıAdı.SelectedIndex = 0;
            }
            catch// eger cmbx_DuzenleKullanıcıAdı veri yok ise selected index 0 oluyacak hata verecek -1 olarak ayarlıyoruz.
            {
                cmbx_DuzenleKullanıcıAdı.SelectedIndex = -1;
            }
        }

        private void btn_DuzenleCikis_Click(object sender, EventArgs e) // kullanıcı duzenle panelindeki cıkıs buttonu.
        {
            try //cmbx_DuzenleKullanıcıAdı indexi 0 ayarlıyoruz ilk kullanıcıyı gostermesi için
            {
                cmbx_DuzenleKullanıcıAdı.SelectedIndex = 0;
            }
            catch// eger cmbx_DuzenleKullanıcıAdı veri yok ise selected index 0 oluyacak hata verecek -1 olarak ayarlıyoruz.
            {
                cmbx_DuzenleKullanıcıAdı.SelectedIndex = -1;
            }

            txt_DuzenleKullanıcıAdı.Text = ""; //kullanıcı duzenle panelindeki txtleri içini sıfırlıyoruz.
            txt_DuzenleKullanıcıSifre.Text = "";
        }

        private void btn_DuzenleOnayla_Click(object sender, EventArgs e) // kullanıcı duzenle panelindeki onayla buttonu.
        {
            try
            {//kullanıcı düzenleme bilgilerini KullanıcıGuncelle fonksiyonuna gönderiyoruz.
                if (BL.KullanıcıAyar.KullanıcıGuncelle(BL.UrunBilgisi.Kullanıcılar[cmbx_SilKullanıcıAdı.SelectedIndex].ID, txt_DuzenleKullanıcıAdı.Text, txt_DuzenleKullanıcıSifre.Text,cmbx_DuzenleKullanıcıAdı.SelectedIndex) != -1)
                {
                    MessageBox.Show("Başarılı Bir Şekilde Güncellenmiştir", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CmbxDoldur(); //comboboxların içini yeniden olduruyoruz.
                }
                else
                {
                    MessageBox.Show("Hata Güncellenmedi!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch
            {
                MessageBox.Show("Hata Güncellenmedi!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            CmbxIndexAyar();//comboboxların select index ayarlarlarını yapıyoruz.
            txt_DuzenleKullanıcıAdı.Text = ""; // kullanıcı duzenle panelindeki txt lerin içini boşaltır.
            txt_DuzenleKullanıcıSifre.Text = "";
        }

        private void btnGeri_Click(object sender, EventArgs e) //geri tuşu ana sayfaya geri donmemizi sağlar.
        {
            Sayfalar.anaSayfa.Show();//anasayfa nesneni acar.
            this.Hide(); //bu formu kapatır.
        }
    }
}
