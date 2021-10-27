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
    public partial class Giris : Form
    {
        int _ciftTiklamaDurumu = 0; // cıkış buttonuna ıkı kere tıklanması durumunda uygulamayı kapatmak için tıklanma durumunu tutan degişken.
        public Giris()
        {

        }

        private void Giris_Load(object sender, EventArgs e)
        {

        }

        private void btn_Cikis_Click(object sender, EventArgs e) //cıkıs buttonu.
        {
            if (txt_Kullanici.Text == "" && txt_Parola.Text == "") //txtler boş ise _ciftTiklamaDurumu bir artır.
                _ciftTiklamaDurumu++;
            else //degilse _ciftTiklamaDurumu 0 ata ve txtleri sıfırla.
            {
                _ciftTiklamaDurumu = 0;

                txt_Kullanici.Text = "";
                txt_Parola.Text = "";
            }

            if (_ciftTiklamaDurumu == 2) //eger _ciftTiklamaDurumu 2 olduysa programdan çık.
                Application.Exit();
        }

        private void btn_Giris_Click(object sender, EventArgs e) //giriş butonu.
        {
            if (ASGJETSAT.BL.GirisAyar.GirisKontrol(txt_Kullanici.Text, txt_Parola.Text)) // kullanıcı adı ve parola txt gelen verileri giriş ayar clasının giriş kontrole gonderip bool değer alıyoruz.
            {
                this.Hide(); //bu sayfayı kapat.
                Sayfalar.anaSayfa.Show(); //ana sayfayı ac.
            }
            else
            {
                MessageBox.Show("Kullanıcı Adı Veya Şifre Yanlış!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_Kullanici.Text = "";
                txt_Parola.Text = "";
            }
        }



        private void Giris_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == Keys.Escape.GetHashCode())
            {
                e.SuppressKeyPress = true;
                btn_Cikis_Click(sender, e);
            }
            else if (e.KeyValue == Keys.Enter.GetHashCode())
            {
                e.SuppressKeyPress = true;
                btn_Giris_Click(sender, e);
            }
        }
    }
}
