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
    public partial class AnaSayfa : Form
    {
        public AnaSayfa()
        {

        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            Application.Exit();//uygulamayı kapatır.
        }

        private void btnKilit_Click(object sender, EventArgs e)
        {
            Sayfalar.giris.Show(); // giris nesnesini açar
            this.Hide(); //suanki form kapatır.
            GC.Collect(); // ram temizleyici cagiririz.
        }

        private void btnMuhasebe_Click(object sender, EventArgs e)
        {
            Sayfalar.muhasebe.Muhasebe_Load(sender, e);
            Sayfalar.muhasebe.Show(); // Muhasebe nesnesini açar
            this.Hide(); //suanki form kapatır.
            GC.Collect(); // ram temizleyici cagiririz.
        }

        private void btnUrunler_Click(object sender, EventArgs e)
        {
            Sayfalar.urunlerStok.Show(); // urunlerStok nesnesini açar
            this.Hide();//suanki form kapatır.
            GC.Collect();// ram temizleyici cagiririz.
        }

        private void btnJETSAT_Click(object sender, EventArgs e)
        {
            Sayfalar.jETSAT.Show();// JETSAT nesnesini açar
            this.Hide();//suanki form kapatır.
            GC.Collect();// ram temizleyici cagiririz.
        }

        private void btnAyarlar_Click(object sender, EventArgs e)
        {
            Sayfalar.ayarlar.Show();// Ayarlar nesnesini açar
            this.Hide();//suanki form kapatır.
            GC.Collect();// ram temizleyici cagiririz.
        }
    }
}
