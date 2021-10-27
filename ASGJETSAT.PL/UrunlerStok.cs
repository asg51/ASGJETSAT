using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASGJETSAT.PL
{
    public partial class UrunlerStok : Form
    {


        private void cmbxDoldur()/*comboboxları dolduruduk.*/
        {
            cmbxStokEkleKategori.Items.Clear();
            cmbxStokSilKategori.Items.Clear();

            for (int i = 0; i < BL.UrunBilgisi.Urunler.Count; i++)
            {
                cmbxStokEkleKategori.Items.Add(BL.UrunBilgisi.Urunler[i].KATEGORI);
                cmbxStokSilKategori.Items.Add(BL.UrunBilgisi.Urunler[i].KATEGORI);
            }

        }

        private void cmbxIndexAyar()/*comboboxların ındex ayarlarını yaptık.*/
        {
            try
            {
                cmbxStokEkleKategori.SelectedIndex = 0;
            }
            catch
            {
                cmbxStokEkleKategori.SelectedIndex = -1;
            }

            try
            {
                cmbxStokSilKategori.SelectedIndex = 0;
            }
            catch
            {
                cmbxStokSilKategori.SelectedIndex = -1;
            }
        }

        public UrunlerStok()
        {

        }

        public void UrunlerStok_Load(object sender, EventArgs e)/*strcut doldurduk ve comboboxları doldurup index ayarlarını yaptık.*/
        {
           
        }

        private void cmbxStokEkleKategori_SelectedIndexChanged(object sender, EventArgs e)
        {/*cmbxStokEkleKategori selected indexi secildigi an lblStokEkleAdet ve txtStokEkleAdet textini sıfırladık ve cmbxStokEkleUrun yeni urunlerini ekledik.*/
            if (cmbxStokEkleKategori.SelectedIndex != -1)
            {
                lblStokEkleAdet.Text = "";
                txtStokEkleAdet.Text = "";
                try
                {
                    cmbxStokEkleUrun.Items.Clear();
                    cmbxStokEkleUrun.Text = "";
                    for (int i = 0; i < BL.UrunBilgisi.Urunler[cmbxStokEkleKategori.SelectedIndex].URUNLER.Count; i++)
                    {
                        cmbxStokEkleUrun.Items.Add(BL.UrunBilgisi.Urunler[cmbxStokEkleKategori.SelectedIndex].URUNLER[i].URUN);
                    }
                    cmbxStokEkleUrun.SelectedIndex = 0;
                }
                catch
                {
                    cmbxStokEkleUrun.SelectedIndex = -1;
                }
            }
        }

        private void cmbxStokEkleUrun_SelectedIndexChanged(object sender, EventArgs e)
        {/*cmbxStokEkleUrun selected index secildigi an lblStokEkleAdet ve txtStokEkleAdet textleri sıfırladık cmbxStokEkleOzellik yeni ozelliklerini ekledik.*/
            if (cmbxStokEkleUrun.SelectedIndex != -1)
            {
                lblStokEkleAdet.Text = "";
                txtStokEkleAdet.Text = "";
                try
                {
                    cmbxStokEkleOzellik.Items.Clear();
                    cmbxStokEkleOzellik.Text = "";
                    for (int i = 0; i < BL.UrunBilgisi.Urunler[cmbxStokEkleKategori.SelectedIndex].URUNLER[cmbxStokEkleUrun.SelectedIndex].URUNOZELLIK.Count; i++)
                    {
                        cmbxStokEkleOzellik.Items.Add(BL.UrunBilgisi.Urunler[cmbxStokEkleKategori.SelectedIndex].URUNLER[cmbxStokEkleUrun.SelectedIndex].URUNOZELLIK[i].OZELLIK);
                    }
                    cmbxStokEkleOzellik.SelectedIndex = 0;
                }
                catch
                {
                    cmbxStokEkleOzellik.SelectedIndex = -1;
                }
            }
        }


        private void btn_StokEkleSil_Click(object sender, EventArgs e)
        {/*btn_StokEkleSil click eventi calıstıgı an lblStokEkleAdet ve txtStokEkleAdet text leri sıfırlanır ve cmbxStokEkleKategori index ayarı yapılır.*/
            lblStokEkleAdet.Text = "";
            txtStokEkleAdet.Text = "";
            try
            {
                cmbxStokEkleKategori.SelectedIndex = 0;
            }
            catch
            {
                cmbxStokEkleKategori.SelectedIndex = -1;
            }
        }

        private void btn_StokEkleEkle_Click(object sender, EventArgs e)
        {/*urun ozellige stok ekleme kısmı.*/
            bool durum = false;
            foreach (Control control in pnlStokEkle.Controls)
            {//pnlStokEkle combobox ların selected indexleri -1 ve textboxların textı boş olmadıgı surece stok ekleme yapılabılır.
                if (control.GetType() == typeof(ComboBox) && (control as ComboBox).SelectedIndex == -1)
                    durum = true;
                else if (control.GetType() == typeof(TextBox) && (control as TextBox).Text == "")
                    durum = true;
            }
            if (durum)
            {
                MessageBox.Show("Hata Tüm Verileri Giriniz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {/*stok eklenıp structlar yeniden doldurulur comboboxlar yeninden doldurulup index ayarı verilir.*/
                if (BL.UrunStok.StokDuzenle(BL.UrunBilgisi.Urunler[cmbxStokEkleKategori.SelectedIndex].URUNLER[cmbxStokEkleUrun.SelectedIndex].URUNOZELLIK[cmbxStokEkleOzellik.SelectedIndex].ID,
                    BL.UrunBilgisi.Urunler[cmbxStokEkleKategori.SelectedIndex].URUNLER[cmbxStokEkleUrun.SelectedIndex].URUNOZELLIK[cmbxStokEkleOzellik.SelectedIndex].STOK + int.Parse(txtStokEkleAdet.Text),
               cmbxStokEkleKategori.SelectedIndex, cmbxStokEkleUrun.SelectedIndex, cmbxStokEkleOzellik.SelectedIndex) != -1)
                {
                    MessageBox.Show("Başarılı Bir Şekilde Eklenmiştir.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbxDoldur();
                    cmbxIndexAyar();
                }
                else
                {
                    MessageBox.Show("Hata Eklenemedi!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            lblStokEkleAdet.Text = "";
            txtStokEkleAdet.Text = "";
        }

        private void cmbxStokSilKategori_SelectedIndexChanged(object sender, EventArgs e)
        {/*cmbxStokSilKategori selected indexi secildigi an txtStokSilAdet ve lblStokSilAdet textleri sıfırla ve cmbxStokSilUrun yeni urunleri ekle.*/
            if (cmbxStokSilKategori.SelectedIndex != -1)
            {
                txtStokSilAdet.Text = "";
                lblStokSilAdet.Text = "";
                try
                {
                    cmbxStokSilUrun.Items.Clear();
                    cmbxStokSilUrun.Text = "";
                    for (int i = 0; i < BL.UrunBilgisi.Urunler[cmbxStokSilKategori.SelectedIndex].URUNLER.Count; i++)
                    {
                        cmbxStokSilUrun.Items.Add(BL.UrunBilgisi.Urunler[cmbxStokSilKategori.SelectedIndex].URUNLER[i].URUN);
                    }
                    cmbxStokSilUrun.SelectedIndex = 0;
                }
                catch
                {
                    cmbxStokSilUrun.SelectedIndex = -1;
                }
            }
        }

        private void cmbxStokSilUrun_SelectedIndexChanged(object sender, EventArgs e)
        {/*cmbxStokSilUrun selected indexi secildigi an txtStokSilAdet ve lblStokSilAdet textleri sıfırlanır ve cmbxStokSilOzellik yeni ozellikleri eklenir.*/
            if (cmbxStokSilUrun.SelectedIndex != -1)
            {
                txtStokSilAdet.Text = "";
                lblStokSilAdet.Text = "";
                try
                {
                    cmbxStokSilOzellik.Items.Clear();
                    cmbxStokSilOzellik.Text = "";
                    for (int i = 0; i < BL.UrunBilgisi.Urunler[cmbxStokSilKategori.SelectedIndex].URUNLER[cmbxStokSilUrun.SelectedIndex].URUNOZELLIK.Count; i++)
                    {
                        cmbxStokSilOzellik.Items.Add(BL.UrunBilgisi.Urunler[cmbxStokSilKategori.SelectedIndex].URUNLER[cmbxStokSilUrun.SelectedIndex].URUNOZELLIK[i].OZELLIK);
                    }
                    cmbxStokSilOzellik.SelectedIndex = 0;
                }
                catch
                {
                    cmbxStokSilOzellik.SelectedIndex = -1;
                }
            }
        }

        private void cmbxStokSilOzellik_SelectedIndexChanged(object sender, EventArgs e)
        {/*cmbxStokSilOzellik selected indexi secildigi an lblStokSilAdet stok degeri girilir.*/
            if (cmbxStokSilOzellik.SelectedIndex != -1)
            {
                lblStokSilAdet.Text = "";
                try
                {
                    lblStokSilAdet.Text = BL.UrunBilgisi.Urunler[cmbxStokSilKategori.SelectedIndex].URUNLER[cmbxStokSilUrun.SelectedIndex].URUNOZELLIK[cmbxStokSilOzellik.SelectedIndex].STOK.ToString() + " - ";
                }
                catch
                {
                    lblStokSilAdet.Text = "";
                }
            }
        }

        private void btnStokSilSil_Click(object sender, EventArgs e)
        {/*btnStokSilSil click eventi calıstıgı an txtStokSilAdet ve lblStokSilAdet texleri sıfırlanır ve cmbxStokSilKategori index ayarı yapılır.*/
            txtStokSilAdet.Text = "";
            lblStokSilAdet.Text = "";
            try
            {
                cmbxStokSilKategori.SelectedIndex = 0;
            }
            catch
            {
                cmbxStokSilKategori.SelectedIndex = -1;
            }
        }

        private void cmbxStokEkleOzellik_SelectedIndexChanged(object sender, EventArgs e)
        {/*cmbxStokEkleOzellik selected indexi secildigi an lblStokEkleAdet texti sıfırlanır ve lblStokEkleAdet stok degeri girilir.*/
            if (cmbxStokEkleOzellik.SelectedIndex != -1)
            {
                lblStokEkleAdet.Text = "";
                try
                {
                    lblStokEkleAdet.Text = BL.UrunBilgisi.Urunler[cmbxStokEkleKategori.SelectedIndex].URUNLER[cmbxStokEkleUrun.SelectedIndex].URUNOZELLIK[cmbxStokEkleOzellik.SelectedIndex].STOK.ToString() + " + ";
                }
                catch
                {
                    lblStokEkleAdet.Text = "";
                }
            }
        }

        private void btnStokSilEkle_Click(object sender, EventArgs e)
        {/*btnStokSilEkle click eventine basılınca urun ozellike ait stok silme işlemi yapar.*/
            bool durum = false;
            foreach (Control control in pnlStokSil.Controls)
            {/*pnlStokSil içindeki combobox selected index degeri -1 ve textboxların textleri bos olmadıgı surece stok sil.*/
                if (control.GetType() == typeof(ComboBox) && (control as ComboBox).SelectedIndex == -1)
                    durum = true;
                else if (control.GetType() == typeof(TextBox) && (control as TextBox).Text == "")
                    durum = true;
            }
            if (durum)
            {
                MessageBox.Show("Hata Tüm Verileri Giriniz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {/*eger silinecek stok sistemdeki stoktan fazla ise stok sıfırlama sorusu sorar.*/
                DialogResult dialogResult = DialogResult.No;
                if (BL.UrunBilgisi.Urunler[cmbxStokSilKategori.SelectedIndex].URUNLER[cmbxStokSilUrun.SelectedIndex].URUNOZELLIK[cmbxStokSilOzellik.SelectedIndex].STOK - int.Parse(txtStokSilAdet.Text) < 0)
                {
                    dialogResult = MessageBox.Show("Hata Stokdan Fazla Urun Silemezsiniz! \nStok 0 Olarak Atansınmı? !", "Hata!", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    durum = true;
                }/*stok sıfırlama istegi onaylanırsa stok sıfırlanır.*/
                if (dialogResult == DialogResult.Yes)
                {
                    txtStokSilAdet.Text = BL.UrunBilgisi.Urunler[cmbxStokSilKategori.SelectedIndex].URUNLER[cmbxStokSilUrun.SelectedIndex].URUNOZELLIK[cmbxStokSilOzellik.SelectedIndex].STOK.ToString();
                    durum = false;
                }
                if (!durum)/*stok silinecek stokdan fazla veya esitse yapılacak işlemler.*/
                {
                    if (BL.UrunStok.StokDuzenle(BL.UrunBilgisi.Urunler[cmbxStokSilKategori.SelectedIndex].URUNLER[cmbxStokSilUrun.SelectedIndex].URUNOZELLIK[cmbxStokSilOzellik.SelectedIndex].ID,
                        BL.UrunBilgisi.Urunler[cmbxStokSilKategori.SelectedIndex].URUNLER[cmbxStokSilUrun.SelectedIndex].URUNOZELLIK[cmbxStokSilOzellik.SelectedIndex].STOK - int.Parse(txtStokSilAdet.Text),
                        cmbxStokSilKategori.SelectedIndex, cmbxStokSilUrun.SelectedIndex, cmbxStokSilUrun.SelectedIndex) != -1)
                    {
                        MessageBox.Show("Başarılı Bir Şekilde Silinmiştir.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmbxDoldur();
                        cmbxIndexAyar();
                    }
                    else
                    {
                        MessageBox.Show("Hata Eklenemedi!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            lblStokSilAdet.Text = "";
            txtStokSilAdet.Text = "";
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {/*AnaSayfa geçis kodu*/
            Sayfalar.anaSayfa.Show();
            this.Hide();
        }

        private void btnKategori_Click(object sender, EventArgs e)
        {/*UrunlerKategori geçis kodu*/
            Sayfalar.urunlerKategori.Show();
            this.Hide();
        }

        private void btnUrun_Click(object sender, EventArgs e)
        {/*UrunlerUrun geçis kodu*/
            Sayfalar.urunlerUrun.Show();
            this.Hide();
        }

        private void btnOzeliik_Click(object sender, EventArgs e)
        {/*UrunlerOzellik geçis kodu*/
            Sayfalar.urunlerOzellik.Show();
            this.Hide();
        }

        private void btn_KisaYol_Click(object sender, EventArgs e)
        {
            Sayfalar.urunlerKisaYol.Show();
            this.Hide();
        }

        private void UrunlerStok_Shown(object sender, EventArgs e)
        {
            cmbxDoldur();
            cmbxIndexAyar();
        }
    }
}
