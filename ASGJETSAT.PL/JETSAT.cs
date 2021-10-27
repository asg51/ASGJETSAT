using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace ASGJETSAT.PL
{
    public partial class JETSAT : Form
    {
        public string _Kullanici = ""; //sisteme giriş yapan kullanıcıyı tutacak.

        public static List<JETSATVIEW> UrunlerFPanel = new List<JETSATVIEW>();
        public static List<JETSATVIEW.SepetVIEW> SepetFPanel = new List<JETSATVIEW.SepetVIEW>();


        bool boolHizliSat = true;
        bool boolHizliSatAdet = false;



        //acılır menu yapımında kullanılacak nesneler.
        int Tiklananpanel1 = -1; //tıklanan buttonun kategori index tutar.
        int Tiklananpanel2 = -1;//tıklanan buttonun urun index tutar.
        FlowLayoutPanel panelDropDown1; //tıklanan ilk FlowLayoutPanel index tutar refernasını tutar.
        FlowLayoutPanel panelDropDown2;//tıklanan ikinci FlowLayoutPanel index tutar refernasını tutar.
        bool timerDurum = false; //acılır menunun durumu.
        Button btn_Tick1; //tıklanan ilk button index tutar.
        Button btn_Tick2;//tıklanan ikinci button index tutar.
        bool isCollapsed; //menu acılıp veya kapanma durumuna karar verir.



        ToolTip tooltip = new ToolTip();//ekranda gostermek için tooltip oluşturuyoruz.

        public JETSAT()
        {

        }

        public int KategoriEkle(int kategoriIndex)
        {
            try
            {
                Button buttonMenu = new Button();
                buttonMenu.BackColor = Color.Black;
                buttonMenu.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 30, 30);
                buttonMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular);
                buttonMenu.ForeColor = Color.White;
                buttonMenu.Name = "btn-" + kategoriIndex.ToString();
                buttonMenu.Text = BL.UrunBilgisi.Urunler[kategoriIndex].KATEGORI.ToString(); //buttona strcutta tutugumuz kategori verisini text olarak ayarlıyoruz.
                buttonMenu.Height = 40;
                buttonMenu.Width = flowLayoutPanelMenu.Width - 10;
                buttonMenu.Dock = DockStyle.Top; //buttonu flowLayoutPanelMenu uste tam doldurması seklinde durmasını ıstıyoruz.
                buttonMenu.TextAlign = ContentAlignment.MiddleLeft;
                buttonMenu.FlatStyle = FlatStyle.Flat;
                buttonMenu.FlatAppearance.BorderSize = 0;
                buttonMenu.Click += ButtonMenu_Click; //ortak click eventi veriyoruz.

                FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
                flowLayoutPanel.Width = flowLayoutPanelUrun.Width-40;
                flowLayoutPanel.AutoScroll=true;
                UrunlerFPanel.Add(new JETSATVIEW(buttonMenu, flowLayoutPanel));

                flowLayoutPanelMenu.Controls.Add(buttonMenu);
            }
            catch
            {
                return -1;
            }
            return 0;
        }
        public int KategoriGuncelle(int kategoriIndex)
        {
            try
            {
                UrunlerFPanel[kategoriIndex].MenuButton.Text = BL.UrunBilgisi.Urunler[kategoriIndex].KATEGORI;

                flowLayoutPanelMenu.Controls.Clear();

                for (int i = 0; i < UrunlerFPanel.Count; i++)
                {
                    flowLayoutPanelMenu.Controls.Add(UrunlerFPanel[i].MenuButton);
                }
            }
            catch
            {
                return -1;
            }
            return 0;
        }
        public int KategoriSil(int kategoriIndex)
        {
            try
            {
                UrunlerFPanel.RemoveAt(kategoriIndex);

                flowLayoutPanelMenu.Controls.Clear();

                for (int i = 0; i < UrunlerFPanel.Count; i++)
                {
                    flowLayoutPanelMenu.Controls.Add(UrunlerFPanel[i].MenuButton);
                }

                for (int i = kategoriIndex; i < UrunlerFPanel.Count; i++)
                {
                    UrunlerFPanel[i].MenuButton.Name = "btn-" + i.ToString();
                    UrunlerFPanel[i].Satis.Clear();

                    for (int j = 0; j < UrunlerFPanel[i].Satis.Count; j++)
                    {
                        UrunlerFPanel[i].Satis[j].SatisButton.Name = "buttonUrun-" + i.ToString() + j.ToString();
                        UrunlerFPanel[i].Satis[j].SatisFPanel.Name = "FPanelUrunler-" + BL.UrunBilgisi.Urunler[i].URUNLER[j].ID;

                        for (int k = 0; k < UrunlerFPanel[i].Satis[j].SatisOzellik.Count; k++)
                        {
                            UrunlerFPanel[i].Satis[j].SatisOzellik[k].SepetTable.Name = "MenuTableLayoutPanel-" + i.ToString() + "_" + j.ToString() + "_" + k.ToString();
                            UrunlerFPanel[i].Satis[j].SatisOzellik[k].UrunOzellikLabel.Name = "LabelsUrunOzellik-" + i.ToString() + "_" + j.ToString() + "_" + k.ToString();
                            UrunlerFPanel[i].Satis[j].SatisOzellik[k].UrunFiyatLabel.Name = "LabelsUrunFiyat-" + i.ToString() + "_" + j.ToString() + "_" + k.ToString();
                            UrunlerFPanel[i].Satis[j].SatisOzellik[k].UrunAdet.Name = "TextBoxesAdet-" + i.ToString() + "_" + j.ToString() + "_" + k.ToString();
                            UrunlerFPanel[i].Satis[j].SatisOzellik[k].UrunArti.Name = "ButtonsArti-" + i.ToString() + "_" + j.ToString() + "_" + k.ToString();
                            UrunlerFPanel[i].Satis[j].SatisOzellik[k].UrunEksi.Name = "ButtonsEksi-" + i.ToString() + "_" + j.ToString() + "_" + k.ToString();
                            UrunlerFPanel[i].Satis[j].SatisOzellik[k].UrunOnayla.Name = "Fpanel-" + i.ToString() + "_" + j.ToString() + "_" + k.ToString();

                        }
                    }
                }
                for (int i = 0; i < SepetFPanel.Count; i++)
                {

                    string[] dizi = MenuNameBul(SepetFPanel[i].Fpanel.Name);
                    if (int.Parse(dizi[0]) > kategoriIndex)
                    {
                        SepetFPanel[i].Fpanel.Name = "Fpanel-" + (int.Parse(dizi[0]) - 1).ToString() + "_" + dizi[1] + "_" + dizi[2];
                        SepetFPanel[i].UrunOzellikLabel.Name = "L-" + (int.Parse(dizi[0]) - 1).ToString() + "_" + dizi[1] + "_" + dizi[2];
                    }
                    else if (int.Parse(dizi[0]) == kategoriIndex)
                    {
                        SepetFPanel.RemoveAt(i);
                    }
                }
            }
            catch
            {
                return -1;
            }
            return 0;
        }
        private void MenuOlustur()
        {/* menu oluşturma kısımında ekranda kullanacagımız menulerin oluşum aşaması.*/
            /*menu kategori oluşturma kodları kategori sayısı kadar donuyor.*/
            flowLayoutPanelUrun.AutoScroll = false;
            flowLayoutPanelUrun.HorizontalScroll.Visible = false;
            flowLayoutPanelUrun.HorizontalScroll.Maximum = 0;
            flowLayoutPanelUrun.VerticalScroll.Visible = false;
            flowLayoutPanelUrun.VerticalScroll.Maximum = 0;

            for (int j = 0; j < BL.UrunBilgisi.Urunler.Count; j++)
            {/* button oluşturup flowLayoutPanelMenu nesnesine ekliyoruz. button ozellikleri koda yazmaktadır.*/
                try
                {
                    KategoriEkle(j);
                    FPanelUrunOlustur(j); //bu kategori ye ait urun ve urun ozellik eklemesi için kategorinin strcutdaki index degerini gonderiyoruz. 
                }
                catch
                {

                }
            }

        }

        private int MenuIdBul(string Name)
        {
            for (int i = 0; i < Name.Length; i++)
            {
                if (Name[i] == '-')
                {
                    return int.Parse(Name.Substring(i + 1));
                }
            }
            return -1;
        }
        private string[] MenuNameBul(string Name) //ortak click kulanacagımız izin menu buttonuna name verdigimiz kısımın içinde index oludugu için o indexi ayırıp gonderiyoruz.
        {
            for (int i = 0; i < Name.Length; i++)
            {
                if (Name[i] == '-')
                {
                    return Name.Substring(i + 1).Split('_');
                }
            }
            return null;
        }

        private void ButtonMenu_Click(object sender, EventArgs e)//menu buttonlarının clicki burada urun ve urun ozelliklerimizi gostrecegımız
        {//flowLayoutPanelUrun panelini temizleyip tıkladığımız button ındexsine gore uygun FPanelKategori panelini içine alıyor.
            flowLayoutPanelUrun.Controls.Clear();
            int indis = MenuIdBul((sender as Button).Name);
            flowLayoutPanelUrun.Controls.Add(UrunlerFPanel[indis].FSepetPanel);
            flowLayoutPanelUrun.MaximumSize = UrunlerFPanel[indis].FSepetPanel.Size;
            textBoxHizliSat.Focus();
        }

        public int UrunEkle(int Kategori, int Urun)
        {
            try
            {
                FlowLayoutPanel SatisFPanel = new FlowLayoutPanel(); // her bir urun için FlowLayoutPanel oluşturuluyor burada urun ve urunozellikler tutulacak.
                SatisFPanel.Width = flowLayoutPanelUrun.Width-40;
                SatisFPanel.Name = "FPanelUrunler-" + BL.UrunBilgisi.Urunler[Kategori].URUNLER[Urun].ID;

                /*urunun adını tutacak button oluşturma kodları.*/
                Button buttonUrun = new Button();
                buttonUrun.BackColor = Color.Black;
                buttonUrun.ForeColor = Color.White;
                buttonUrun.Name = "buttonUrun-" + Kategori.ToString() + "_" + Urun.ToString();
                buttonUrun.Text = BL.UrunBilgisi.Urunler[Kategori].URUNLER[Urun].URUN;//buttona urunun adını veriliyor.
                buttonUrun.Height = 40;
                buttonUrun.Width = flowLayoutPanelUrun.Width-40;
                buttonUrun.Dock = DockStyle.Top;//uste gore sıralanıyor.
                buttonUrun.FlatStyle = FlatStyle.Flat;
                buttonUrun.FlatAppearance.BorderSize = 0;
                buttonUrun.Margin = new Padding(0, 3, 0, 0);
                buttonUrun.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                buttonUrun.FlatStyle = FlatStyle.Flat;
                buttonUrun.Click += ButtonUrun_Click; ;//ortak clıck eventi eklenıyor.

                UrunlerFPanel[Kategori].Satis.Add(new JETSATVIEW.SatisVIEW(SatisFPanel, buttonUrun));
                UrunlerFPanel[Kategori].Satis[Urun].SatisFPanel.Controls.Add(buttonUrun);//FPanelUrunler paneline button ekleniyor.

                UrunlerFPanel[Kategori].Satis[Urun].SatisFPanel.MinimumSize = new Size(flowLayoutPanelUrun.Width-40, 40);// acılır menu yapmak için panelin mınumum ayarı yapılıyor.
                /*panelin maximum ayaraı yapılıyor.*/
                UrunlerFPanel[Kategori].Satis[Urun].SatisFPanel.MaximumSize = new Size(flowLayoutPanelUrun.Width-40, 40 * BL.UrunBilgisi.Urunler[Kategori].URUNLER[Urun].URUNOZELLIK.Count + 40);
                UrunlerFPanel[Kategori].Satis[Urun].SatisFPanel.Size = UrunlerFPanel[Kategori].Satis[Urun].SatisFPanel.MinimumSize;//simdiki ayarı mınımum olarak ayarlanıyor.
                UrunlerFPanel[Kategori].FSepetPanel.Controls.Add(UrunlerFPanel[Kategori].Satis[Urun].SatisFPanel);//FPanelKategori ye FPanelUrunler ekliyoruz ve bu sayede FPanelKategori disinde o indexe ait olan butun urunler eklenmış oluyor.

            }
            catch
            {
                return -1;
            }
            return 0;
        }
        public int UrunGuncelle(int Kategori, int Urun, int YeniKategori, decimal YeniFiyat)
        {

            try
            {
                UrunlerFPanel[YeniKategori].Satis[BL.UrunBilgisi.Urunler[YeniKategori].URUNLER.Count - 1].SatisOzellik[0].UrunFiyatLabel.Text = YeniFiyat.ToString();
                return UrunGuncelle(Kategori, Urun, YeniKategori);
            }
            catch
            {
                return -1;
            }
        }
        public int UrunGuncelle(int Kategori, int Urun, int YeniKategori)
        {
            try
            {
                UrunlerFPanel[Kategori].Satis[Urun].SatisButton.Text = BL.UrunBilgisi.Urunler[YeniKategori].URUNLER[BL.UrunBilgisi.Urunler[YeniKategori].URUNLER.Count - 1].URUN;
                if (Kategori != YeniKategori)
                {
                    UrunlerFPanel[YeniKategori].Satis.Add(UrunlerFPanel[Kategori].Satis[Urun]);
                    UrunlerFPanel[YeniKategori].FSepetPanel.Controls.Add(UrunlerFPanel[Kategori].Satis[Urun].SatisFPanel);
                    UrunlerFPanel[YeniKategori].Satis[UrunlerFPanel[YeniKategori].Satis.Count - 1].SatisButton.Name = "buttonUrun-" + Kategori.ToString() + "_" + Urun.ToString();
                    for (int i = 0; i < UrunlerFPanel[YeniKategori].Satis[UrunlerFPanel[YeniKategori].Satis.Count - 1].SatisOzellik.Count; i++)
                    {
                        UrunlerFPanel[YeniKategori].Satis[UrunlerFPanel[YeniKategori].Satis[UrunlerFPanel[YeniKategori].Satis.Count - 1].SatisOzellik.Count].SatisOzellik[i].SepetTable.Name = "MenuTableLayoutPanel-" + YeniKategori.ToString() + "_" + (UrunlerFPanel[YeniKategori].Satis[UrunlerFPanel[YeniKategori].Satis.Count - 1].SatisOzellik.Count - 1).ToString() + "_" + i.ToString();
                        UrunlerFPanel[YeniKategori].Satis[UrunlerFPanel[YeniKategori].Satis[UrunlerFPanel[YeniKategori].Satis.Count - 1].SatisOzellik.Count].SatisOzellik[i].UrunOzellikLabel.Name = "LabelsUrunOzellik-" + YeniKategori.ToString() + "_" + (UrunlerFPanel[YeniKategori].Satis[UrunlerFPanel[YeniKategori].Satis.Count - 1].SatisOzellik.Count - 1).ToString() + "_" + i.ToString();
                        UrunlerFPanel[YeniKategori].Satis[UrunlerFPanel[YeniKategori].Satis[UrunlerFPanel[YeniKategori].Satis.Count - 1].SatisOzellik.Count].SatisOzellik[i].UrunFiyatLabel.Name = "LabelsUrunFiyat-" + YeniKategori.ToString() + "_" + (UrunlerFPanel[YeniKategori].Satis[UrunlerFPanel[YeniKategori].Satis.Count - 1].SatisOzellik.Count - 1).ToString() + "_" + i.ToString();
                        UrunlerFPanel[YeniKategori].Satis[UrunlerFPanel[YeniKategori].Satis[UrunlerFPanel[YeniKategori].Satis.Count - 1].SatisOzellik.Count].SatisOzellik[i].UrunAdet.Name = "TextBoxesAdet-" + YeniKategori.ToString() + "_" + (UrunlerFPanel[YeniKategori].Satis[UrunlerFPanel[YeniKategori].Satis.Count - 1].SatisOzellik.Count - 1).ToString() + "_" + i.ToString();
                        UrunlerFPanel[YeniKategori].Satis[UrunlerFPanel[YeniKategori].Satis[UrunlerFPanel[YeniKategori].Satis.Count - 1].SatisOzellik.Count].SatisOzellik[i].UrunArti.Name = "ButtonsArti-" + YeniKategori.ToString() + "_" + (UrunlerFPanel[YeniKategori].Satis[UrunlerFPanel[YeniKategori].Satis.Count - 1].SatisOzellik.Count - 1).ToString() + "_" + i.ToString();
                        UrunlerFPanel[YeniKategori].Satis[UrunlerFPanel[YeniKategori].Satis[UrunlerFPanel[YeniKategori].Satis.Count - 1].SatisOzellik.Count].SatisOzellik[i].UrunEksi.Name = "ButtonsEksi-" + YeniKategori.ToString() + "_" + (UrunlerFPanel[YeniKategori].Satis[UrunlerFPanel[YeniKategori].Satis.Count - 1].SatisOzellik.Count - 1).ToString() + "_" + i.ToString();
                        UrunlerFPanel[YeniKategori].Satis[UrunlerFPanel[YeniKategori].Satis[UrunlerFPanel[YeniKategori].Satis.Count - 1].SatisOzellik.Count].SatisOzellik[i].UrunOnayla.Name = "Fpanel-" + YeniKategori.ToString() + "_" + (UrunlerFPanel[YeniKategori].Satis[UrunlerFPanel[YeniKategori].Satis.Count - 1].SatisOzellik.Count - 1).ToString() + "_" + i.ToString();

                    }
                    UrunlerFPanel[Kategori].Satis.RemoveAt(Urun);
                    UrunlerFPanel[Kategori].FSepetPanel.Controls.RemoveAt(Urun);
                }
            }
            catch
            {
                return -1;
            }
            return 0;
        }
        public int UrunSil(int Kategori, int Urun)
        {
            try
            {
                UrunlerFPanel[Kategori].FSepetPanel.Controls.RemoveAt(Urun);

                for (int i = Urun; i < UrunlerFPanel[Kategori].FSepetPanel.Controls.Count; i++)
                {
                    UrunlerFPanel[Kategori].Satis[i].SatisButton.Name = "buttonUrun-" + Kategori.ToString() + "_" + i.ToString();

                    for (int j = 0; j < UrunlerFPanel[Kategori].Satis[i].SatisOzellik.Count; j++)
                    {
                        UrunlerFPanel[Kategori].Satis[i].SatisOzellik[j].SepetTable.Name = "MenuTableLayoutPanel-" + Kategori.ToString() + "_" + i.ToString() + "_" + j.ToString();
                        UrunlerFPanel[Kategori].Satis[i].SatisOzellik[j].UrunOzellikLabel.Name = "LabelsUrunOzellik-" + Kategori.ToString() + "_" + i.ToString() + "_" + j.ToString();
                        UrunlerFPanel[Kategori].Satis[i].SatisOzellik[j].UrunFiyatLabel.Name = "LabelsUrunFiyat-" + Kategori.ToString() + "_" + i.ToString() + "_" + j.ToString();
                        UrunlerFPanel[Kategori].Satis[i].SatisOzellik[j].UrunAdet.Name = "TextBoxesAdet-" + Kategori.ToString() + "_" + i.ToString() + "_" + j.ToString();
                        UrunlerFPanel[Kategori].Satis[i].SatisOzellik[j].UrunArti.Name = "ButtonsArti-" + Kategori.ToString() + "_" + i.ToString() + "_" + j.ToString();
                        UrunlerFPanel[Kategori].Satis[i].SatisOzellik[j].UrunEksi.Name = "ButtonsEksi-" + Kategori.ToString() + "_" + i.ToString() + "_" + j.ToString();
                        UrunlerFPanel[Kategori].Satis[i].SatisOzellik[j].UrunOnayla.Name = "Fpanel-" + Kategori.ToString() + "_" + i.ToString() + "_" + j.ToString();

                    }
                }

                for (int i = 0; i < SepetFPanel.Count; i++)
                {
                    string[] dizi = MenuNameBul(SepetFPanel[i].Fpanel.Name);
                    if (int.Parse(dizi[0]) > Kategori && int.Parse(dizi[1]) > Urun)
                    {
                        SepetFPanel[i].Fpanel.Name = "Fpanel-" + (int.Parse(dizi[0]) - 1).ToString() + "_" + (int.Parse(dizi[1]) - 1).ToString() + "_" + dizi[2];
                        SepetFPanel[i].UrunOzellikLabel.Name = "L-" + (int.Parse(dizi[0]) - 1).ToString() + "_" + (int.Parse(dizi[1]) - 1).ToString() + "_" + dizi[2];
                    }
                    else if (int.Parse(dizi[0]) == Kategori && int.Parse(dizi[1]) == Urun)
                    {
                        SepetFPanel.RemoveAt(i);
                    }
                }
            }
            catch
            {
                return -1;
            }
            return 0;
        }
        private void FPanelUrunOlustur(int kategori) //gelen kategori index parametresine göre urunlerin oluşturulduguu fonksıyon.
        {
            UrunlerFPanel[kategori].FSepetPanel.Size = new Size(flowLayoutPanelUrun.Width - 10, flowLayoutPanelUrun.Height - 10);

            for (int j = 0; j < BL.UrunBilgisi.Urunler[kategori].URUNLER.Count; j++) //urunlerin sayısı kadar dongu oluşturuluyor.
            {
                try
                {
                    UrunEkle(kategori, j);
                    FPanelUrunOzellikOlustur(kategori, j); //urune ait ozelliklerin eklendiği kısım.
                }
                catch
                {

                }
            }
        }

        private void ButtonUrun_Click(object sender, EventArgs e) //urunlerin ortak click eventi burada acılır panel çalışıyor ve o
        {//ait urun ozellikleri gosteriliyor.
            string[] indis = PanelReferansBul((sender as Button).Name); //tıklanan buttonun namede bulunun kategori index ve urun ındex ayıran fonksiyn.
            Tiklananpanel1 = int.Parse(indis[0]); //kategori index ekliyoruz.
            Tiklananpanel2 = int.Parse(indis[1]); //urun index ekliyoruz.
            if (panelDropDown1 != null) //hic bir menu acılmadıysa yapılması gerekenler.
            {// tıklanan panel panelDropDown1 esit ve panelDropDown1 acılmısa yapılacaklar.
                if (!(panelDropDown1 == UrunlerFPanel[Tiklananpanel1].Satis[Tiklananpanel2].SatisFPanel) && panelDropDown1.Size != panelDropDown1.MinimumSize)
                {
                    timerDurum = true; //eger menu acıksa diye kontrol ediliyor.
                    panelDropDown2 = panelDropDown1; //kapanma durumu geldiği zaman referansı kaybetmemek için panelDropDown2 ekliyoruz.
                    btn_Tick2 = btn_Tick1; //kapanma durumu geldiği zaman referansı kaybetmemek için btn_Tick1 ekliyoruz.
                    panelDropDown1 = UrunlerFPanel[Tiklananpanel1].Satis[Tiklananpanel2].SatisFPanel; // acılacak paneli panelDropDown1 referans veriyoruz.
                    btn_Tick1 = (sender as Button); //kullanılacak buttonu  btn_Tick1 referans veriyoruz.
                    //menu acıksa isCollapsed false ayalıyoruz.
                    if (UrunlerFPanel[Tiklananpanel1].Satis[Tiklananpanel2].SatisFPanel.Size == UrunlerFPanel[Tiklananpanel1].Satis[Tiklananpanel2].SatisFPanel.MaximumSize)
                    {
                        isCollapsed = false;
                    }
                    else//menu kapalıysa isCollapsed true ayalıyoruz.
                    {
                        isCollapsed = true;
                    }// isCollapsed durumu ayarlandıktan sonra timer_CollapseMenu2 calıştıryoruz buda menumuzu yavas yavas acılmasını saglıyor.
                    timer_CollapseMenu2.Start();
                }
            }
            if (!timerDurum) //eger onceden menu acık ise yapılacak işlemler.
            {//eger onceki acık ise kapmak için yapılacak işlemler
                if (UrunlerFPanel[Tiklananpanel1].Satis[Tiklananpanel2].SatisFPanel.Size == UrunlerFPanel[Tiklananpanel1].Satis[Tiklananpanel2].SatisFPanel.MaximumSize)
                {
                    panelDropDown1 = UrunlerFPanel[Tiklananpanel1].Satis[Tiklananpanel2].SatisFPanel;
                    btn_Tick1 = (sender as Button);
                    isCollapsed = false;
                    timer_CollapseMenu.Start();
                }
                else
                { //eger onceki kapalı ise açmak için yapılacak işlemler
                    panelDropDown1 = UrunlerFPanel[Tiklananpanel1].Satis[Tiklananpanel2].SatisFPanel;
                    btn_Tick1 = (sender as Button);
                    isCollapsed = true;
                    timer_CollapseMenu.Start();
                }
            }
            textBoxHizliSat.Focus();
        }

        private string[] PanelReferansBul(string name) //acılak menunun name içindeki kategori ve urun indexlerini bulan fonksıyon.
        {
            for (int i = 0; i < name.Length; i++) //gelen name uzunlugu kadar donen dongu.
            {
                if (name[i] == '-') //name i indexi - eşit ise yapılacak işlemler.
                {
                    return name.Substring(i + 1).Split('_'); //indexten 1 fazlasın name alıp _ göre parçalayıp dizi olarak gonderiyor.
                }
            }
            return null;
        }
        public int Urun_Ozellik_Ekle(int Kategori, int Urun, int UrunOzellik)
        {
            try
            {
                FlowLayoutPanel Fpanel = new FlowLayoutPanel(); //her urun için ayrı ayrı flowLayoutPanelUrunler oluşturuluyor.
                Fpanel.BackColor = Color.FromArgb(20, 20, 20);
                Fpanel.Height = 40;
                Fpanel.Width = flowLayoutPanelUrun.Width-40;
                Fpanel.Margin = tableLayoutPanel2.Margin;
                Fpanel.Dock = DockStyle.Top;


                TableLayoutPanel SepetTable = new TableLayoutPanel(); //her nesnemiz duzenli olsun diye hepsine TableLayoutPanel oluştuuryoruz.
                SepetTable.Location = new System.Drawing.Point(0, 0);
                SepetTable.Name = "MenuTableLayoutPanel-" + Kategori.ToString() + "_" + Urun.ToString() + "_" + UrunOzellik.ToString();
                SepetTable.ColumnCount = 6;
                SepetTable.RowCount = 1;
                SepetTable.Size = new System.Drawing.Size(Fpanel.Width, Fpanel.Height);
                SepetTable.Margin = tableLayoutPanel2.Margin;

                /*TableLayoutPanel özellikleri kac satır ve o % lik olarak ayarlanması, kac sutun % olarak ayarlanması gerektigi kodlar.*/
                SepetTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

                SepetTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));
                SepetTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
                SepetTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
                SepetTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
                SepetTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
                SepetTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));

                Label UrunOzellikLabel = new Label();/*urunun ozellik ayarları*/
                UrunOzellikLabel.Text = BL.UrunBilgisi.Urunler[Kategori].URUNLER[Urun].URUNOZELLIK[UrunOzellik].OZELLIK;
                UrunOzellikLabel.Name = "LabelsUrunOzellik-" + Kategori.ToString() + "_" + Urun.ToString() + "_" + UrunOzellik.ToString();
                UrunOzellikLabel.ForeColor = Color.White;
                UrunOzellikLabel.Anchor = AnchorStyles.None;
                UrunOzellikLabel.Margin = tableLayoutPanel2.Margin;
                UrunOzellikLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                SepetTable.Controls.Add(UrunOzellikLabel, 0, 0); //menuye 0 satırın 0 sutuna eklenmesini istiyoruz.

                Label UrunFiyatLabel = new Label();/*urun ozelligin fiyatini tutan label.*/
                UrunFiyatLabel.Text = BL.UrunBilgisi.Urunler[Kategori].URUNLER[Urun].URUNOZELLIK[UrunOzellik].FIYAT.ToString() + "TL";
                UrunFiyatLabel.Name = "LabelsUrunFiyat-" + Kategori.ToString() + "_" + Urun.ToString() + "_" + UrunOzellik.ToString();
                UrunFiyatLabel.ForeColor = Color.White;
                UrunFiyatLabel.Anchor = AnchorStyles.None;
                UrunFiyatLabel.Margin = tableLayoutPanel2.Margin;
                UrunFiyatLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                SepetTable.Controls.Add(UrunFiyatLabel, 1, 0);//menuye 0 satırın 1 sutuna eklenmesini istiyoruz.


                TextBox UrunAdet = new TextBox(); /*urun ozelligin kac adet olacagını satıs yapılacagını tutulan yer otomatık olarak 0 atanıyor.*/
                UrunAdet.Text = "0";
                UrunAdet.Name = "TextBoxesAdet-" + Kategori.ToString() + "_" + Urun.ToString() + "_" + UrunOzellik.ToString();
                UrunAdet.ForeColor = Color.Black;
                UrunAdet.Anchor = AnchorStyles.None;
                UrunAdet.Margin = new Padding(3, 0, 3, 0);
                SepetTable.Controls.Add(UrunAdet, 2, 0);//menuye 0 satırın 2 sutuna eklenmesini istiyoruz.


                Button UrunArti = new Button();/*urun ozelliklerin adet birer birer artırmak için kullanılacak button.*/
                UrunArti.BackColor = Color.FromArgb(40, 40, 40);
                UrunArti.ForeColor = Color.White;
                UrunArti.Name = "ButtonsArti-" + Kategori.ToString() + "_" + Urun.ToString() + "_" + UrunOzellik.ToString();
                UrunArti.Text = "+";
                UrunArti.FlatAppearance.BorderSize = 0;
                UrunArti.Margin = tableLayoutPanel2.Margin;
                UrunArti.Dock = DockStyle.Fill;
                UrunArti.FlatStyle = FlatStyle.Flat;
                UrunArti.Click += ButtonsArti_Click;/*ortak click event kullanımı veriyoruz*/
                SepetTable.Controls.Add(UrunArti, 3, 0);//menuye 0 satırın 3 sutuna eklenmesini istiyoruz.


                Button UrunEksi = new Button();/*urunun adet azaltma buttonu.*/
                UrunEksi.BackColor = Color.FromArgb(20, 20, 20);
                UrunEksi.ForeColor = Color.White;
                UrunEksi.Name = "ButtonsEksi-" + Kategori.ToString() + "_" + Urun.ToString() + "_" + UrunOzellik.ToString();
                UrunEksi.Text = "-";
                UrunEksi.FlatAppearance.BorderSize = 0;
                UrunEksi.Margin = tableLayoutPanel2.Margin;
                UrunEksi.Dock = DockStyle.Fill;
                UrunEksi.FlatStyle = FlatStyle.Flat;
                UrunEksi.Click += ButtonsEksi_Click; //ortak click eventi veriyoruz.
                SepetTable.Controls.Add(UrunEksi, 4, 0);//menuye 0 satırın 4 sutuna eklenmesini istiyoruz.


                Button UrunOnayla = new Button(); // adeti girilen ürünün bu button tıklanması durumunda sepete gonderilmesini saglıyan button.
                UrunOnayla.BackColor = Color.FromArgb(40, 40, 40);
                UrunOnayla.ForeColor = Color.White;
                UrunOnayla.Name = "Fpanel-" + Kategori.ToString() + "_" + Urun.ToString() + "_" + UrunOzellik.ToString();
                UrunOnayla.Text = "Onayla";
                UrunOnayla.FlatAppearance.BorderSize = 0;
                UrunOnayla.Margin = tableLayoutPanel2.Margin;
                UrunOnayla.Dock = DockStyle.Fill;
                UrunOnayla.FlatStyle = FlatStyle.Flat;
                UrunOnayla.Click += ButtonsOnayla_Click;//ortak click eventi kullanıldı.
                SepetTable.Controls.Add(UrunOnayla, 5, 0);//menuye 0 satırın 5 sutuna eklenmesini istiyoruz.

                Fpanel.Controls.Add(SepetTable);
                UrunlerFPanel[Kategori].Satis[Urun].SatisOzellik.Add(new JETSATVIEW.SatisOzellikVIEW(Fpanel, SepetTable, UrunOzellikLabel, UrunFiyatLabel, UrunAdet, UrunArti, UrunEksi, UrunOnayla));

                UrunlerFPanel[Kategori].FSepetPanel.Controls.Add(Fpanel); //MenuTableLayoutPanel flowLayoutPanelUrunler nesnesine eklenerek urun içinde urun ozellikten 1 adet eklemiş olundu.
                UrunlerFPanel[Kategori].Satis[Urun].SatisFPanel.Controls.Add(Fpanel);//FPanelUrunler flowLayoutPanelUrunler ekledik buda urun ozellik verilerin daha duzgun durmasını sagladı.
                return 0;
            }
            catch
            {
                return -1;
            }
        }
        public int UrunOzellikEkle(int Kategori, int Urun, int UrunOzellik)
        {
            try
            {
                Urun_Ozellik_Ekle(Kategori, Urun, UrunOzellik);
                UrunlerFPanel[Kategori].Satis[Urun].SatisFPanel.MaximumSize = new Size(UrunlerFPanel[Kategori].Satis[Urun].SatisFPanel.MaximumSize.Width,
                    UrunlerFPanel[Kategori].Satis[Urun].SatisFPanel.MaximumSize.Height + 40);
                return 0;
            }
            catch
            {
                return -1;
            }
        }
        public int UrunOzellikGuncelle(int Kategori, int Urun, int UrunOzellik, int YeniKategori, int YeniUrun)
        {
            try
            {
                UrunlerFPanel[Kategori].Satis[Urun].SatisOzellik[UrunOzellik].SepetTable.Name = "MenuTableLayoutPanel-" + YeniKategori.ToString() + "_" +
                    YeniUrun.ToString() + "_" + (BL.UrunBilgisi.Urunler[YeniKategori].URUNLER[YeniUrun].URUNOZELLIK.Count - 1).ToString();
                UrunlerFPanel[Kategori].Satis[Urun].SatisOzellik[UrunOzellik].UrunOzellikLabel.Name = "LabelsUrunOzellik-" + YeniKategori.ToString() + "_" +
                    YeniUrun.ToString() + "_" + (BL.UrunBilgisi.Urunler[YeniKategori].URUNLER[YeniUrun].URUNOZELLIK.Count - 1).ToString();
                UrunlerFPanel[Kategori].Satis[Urun].SatisOzellik[UrunOzellik].UrunFiyatLabel.Name = "LabelsUrunFiyat-" + YeniKategori.ToString() + "_" +
                    YeniUrun.ToString() + "_" + (BL.UrunBilgisi.Urunler[YeniKategori].URUNLER[YeniUrun].URUNOZELLIK.Count - 1).ToString();
                UrunlerFPanel[Kategori].Satis[Urun].SatisOzellik[UrunOzellik].UrunAdet.Name = "TextBoxesAdet-" + YeniKategori.ToString() + "_" +
                    YeniUrun.ToString() + "_" + (BL.UrunBilgisi.Urunler[YeniKategori].URUNLER[YeniUrun].URUNOZELLIK.Count - 1).ToString();
                UrunlerFPanel[Kategori].Satis[Urun].SatisOzellik[UrunOzellik].UrunArti.Name = "ButtonsArti-" + YeniKategori.ToString() + "_" +
                    YeniUrun.ToString() + "_" + (BL.UrunBilgisi.Urunler[YeniKategori].URUNLER[YeniUrun].URUNOZELLIK.Count - 1).ToString();
                UrunlerFPanel[Kategori].Satis[Urun].SatisOzellik[UrunOzellik].UrunEksi.Name = "ButtonsEksi-" + YeniKategori.ToString() + "_" +
                    YeniUrun.ToString() + "_" + (BL.UrunBilgisi.Urunler[YeniKategori].URUNLER[YeniUrun].URUNOZELLIK.Count - 1).ToString();
                UrunlerFPanel[Kategori].Satis[Urun].SatisOzellik[UrunOzellik].UrunOnayla.Name = "Fpanel-" + YeniKategori.ToString() + "_" +
                    YeniUrun.ToString() + "_" + (BL.UrunBilgisi.Urunler[YeniKategori].URUNLER[YeniUrun].URUNOZELLIK.Count - 1).ToString();

                UrunlerFPanel[Kategori].Satis[Urun].SatisOzellik[UrunOzellik].UrunOzellikLabel.Text =
                    BL.UrunBilgisi.Urunler[YeniKategori].URUNLER[YeniUrun].URUNOZELLIK[(BL.UrunBilgisi.Urunler[YeniKategori].URUNLER[YeniUrun].URUNOZELLIK.Count - 1)].OZELLIK;
                UrunlerFPanel[Kategori].Satis[Urun].SatisOzellik[UrunOzellik].UrunFiyatLabel.Text =
                    BL.UrunBilgisi.Urunler[YeniKategori].URUNLER[YeniUrun].URUNOZELLIK[(BL.UrunBilgisi.Urunler[YeniKategori].URUNLER[YeniUrun].URUNOZELLIK.Count - 1)].FIYAT.ToString();

                UrunlerFPanel[YeniKategori].Satis[YeniUrun].SatisOzellik.Add(UrunlerFPanel[Kategori].Satis[Urun].SatisOzellik[UrunOzellik]);
                UrunlerFPanel[YeniKategori].Satis[YeniUrun].SatisFPanel.Controls.Add(UrunlerFPanel[YeniKategori].Satis[YeniUrun].SatisOzellik[UrunlerFPanel[YeniKategori].Satis[YeniUrun].SatisOzellik.Count - 1].Fpanel);

                for (int i = UrunOzellik; i < UrunlerFPanel[Kategori].Satis[Urun].SatisOzellik.Count; i++)
                {
                    UrunlerFPanel[Kategori].Satis[Urun].SatisOzellik[i].SepetTable.Name = "MenuTableLayoutPanel-" + Kategori.ToString() + "_" +
                        Urun.ToString() + "_" + i.ToString();
                    UrunlerFPanel[Kategori].Satis[Urun].SatisOzellik[i].UrunOzellikLabel.Name = "LabelsUrunOzellik-" + Kategori.ToString() + "_" +
                        Urun.ToString() + "_" + i.ToString();
                    UrunlerFPanel[Kategori].Satis[Urun].SatisOzellik[i].UrunFiyatLabel.Name = "LabelsUrunFiyat-" + Kategori.ToString() + "_" +
                        Urun.ToString() + "_" + i.ToString();
                    UrunlerFPanel[Kategori].Satis[Urun].SatisOzellik[i].UrunAdet.Name = "TextBoxesAdet-" + Kategori.ToString() + "_" +
                        Urun.ToString() + "_" + i.ToString();
                    UrunlerFPanel[Kategori].Satis[Urun].SatisOzellik[i].UrunArti.Name = "ButtonsArti-" + Kategori.ToString() + "_" +
                        Urun.ToString() + "_" + i.ToString();
                    UrunlerFPanel[Kategori].Satis[Urun].SatisOzellik[i].UrunEksi.Name = "ButtonsEksi-" + Kategori.ToString() + "_" +
                        Urun.ToString() + "_" + i.ToString();
                    UrunlerFPanel[Kategori].Satis[Urun].SatisOzellik[i].UrunOnayla.Name = "Fpanel-" + Kategori.ToString() + "_" +
                        Urun.ToString() + "_" + i.ToString();
                }
                UrunlerFPanel[Kategori].Satis[Urun].SatisFPanel.MaximumSize = new Size(UrunlerFPanel[Kategori].Satis[Urun].SatisFPanel.MaximumSize.Width, UrunlerFPanel[Kategori].Satis[Urun].SatisFPanel.MaximumSize.Height - 40);
                UrunlerFPanel[YeniKategori].Satis[YeniUrun].SatisFPanel.MaximumSize = new Size(UrunlerFPanel[YeniKategori].Satis[YeniUrun].SatisFPanel.MaximumSize.Width, UrunlerFPanel[YeniKategori].Satis[YeniUrun].SatisFPanel.MaximumSize.Height + 40);

                return 0;
            }
            catch
            {
                return -1;
            }
        }
        public int UrunOzellikSil(int Kategori, int Urun, int UrunOzellik)
        {
            try
            {
                UrunlerFPanel[Kategori].Satis[Urun].SatisOzellik.RemoveAt(UrunOzellik);
                UrunlerFPanel[Kategori].Satis[Urun].SatisFPanel.Controls.RemoveAt(UrunOzellik + 1);
                for (int i = UrunOzellik; i < UrunlerFPanel[Kategori].Satis[Urun].SatisOzellik.Count; i++)
                {
                    UrunlerFPanel[Kategori].Satis[Urun].SatisOzellik[i].SepetTable.Name = "MenuTableLayoutPanel-" + Kategori.ToString() + "_" + Urun.ToString() + "_" + i.ToString();
                    UrunlerFPanel[Kategori].Satis[Urun].SatisOzellik[i].UrunOzellikLabel.Name = "LabelsUrunOzellik-" + Kategori.ToString() + "_" + Urun.ToString() + "_" + i.ToString();
                    UrunlerFPanel[Kategori].Satis[Urun].SatisOzellik[i].UrunFiyatLabel.Name = "LabelsUrunFiyat-" + Kategori.ToString() + "_" + Urun.ToString() + "_" + i.ToString();
                    UrunlerFPanel[Kategori].Satis[Urun].SatisOzellik[i].UrunAdet.Name = "TextBoxesAdet-" + Kategori.ToString() + "_" + Urun.ToString() + "_" + i.ToString();
                    UrunlerFPanel[Kategori].Satis[Urun].SatisOzellik[i].UrunArti.Name = "ButtonsArti-" + Kategori.ToString() + "_" + Urun.ToString() + "_" + i.ToString();
                    UrunlerFPanel[Kategori].Satis[Urun].SatisOzellik[i].UrunEksi.Name = "ButtonsEksi-" + Kategori.ToString() + "_" + Urun.ToString() + "_" + i.ToString();
                    UrunlerFPanel[Kategori].Satis[Urun].SatisOzellik[i].UrunOnayla.Name = "Fpanel-" + Kategori.ToString() + "_" + Urun.ToString() + "_" + i.ToString();
                }
                UrunlerFPanel[Kategori].Satis[Urun].SatisFPanel.MaximumSize = new Size(UrunlerFPanel[Kategori].Satis[Urun].SatisFPanel.MaximumSize.Width,
                    UrunlerFPanel[Kategori].Satis[Urun].SatisFPanel.MaximumSize.Height - 40);
                return 0;
            }
            catch
            {
                return -1;
            }
        }
        private void FPanelUrunOzellikOlustur(int kategori, int urun) //urunlerin ozelliklerini oluşturudugumuz kısım.
        {
            for (int z = 0; z < BL.UrunBilgisi.Urunler[kategori].URUNLER[urun].URUNOZELLIK.Count; z++) //her urunun urun ozellik uzunlugu kadar donen dongu.
            {
                Urun_Ozellik_Ekle(kategori, urun, z);
            }
        }

        private void ButtonsOnayla_Click(object sender, EventArgs e) //satıs sepetine ekleme buttonumuz.
        {
            string[] indis = MenuNameBul((sender as Button).Name); //buttonun name içine ekledıgımız ındexi buluyoruz.
            bool durum = true;
            try//TextBoxesAdet indexideki text int donup donmedigini kontrol ediyoruz.
            {
                int.Parse(UrunlerFPanel[int.Parse(indis[0])].Satis[int.Parse(indis[1])].SatisOzellik[int.Parse(indis[2])].UrunAdet.Text);
            }
            catch //text int donmez ise hata mesajı verip durumu false yapıyoruz bu sayede işlem yapmayı engellıyoruz.
            {
                MessageBox.Show("Sayı Giriniz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                durum = false;
            }
            if (durum && int.Parse(UrunlerFPanel[int.Parse(indis[0])].Satis[int.Parse(indis[1])].SatisOzellik[int.Parse(indis[2])].UrunAdet.Text) > 0) //TextBoxesAdet indexideki text int donmus ve içindeki sayı 0 dan buyukse yapılacak işlemler.
            {
                SepeteUrunEkle((sender as Button).Name, UrunlerFPanel[int.Parse(indis[0])].Satis[int.Parse(indis[1])].SatisOzellik[int.Parse(indis[2])].UrunAdet.Text);
            }//butun satıs yapılacak olan TextBoxesAdet 0 olarak atıyoruz yeni urun eklerken en son girilen değer kalmasın diye.
            for (int i = 0; i < BL.UrunBilgisi.Urunler.Count; i++)
            {
                for (int j = 0; j < BL.UrunBilgisi.Urunler[i].URUNLER.Count; j++)
                {
                    for (int k = 0; k < BL.UrunBilgisi.Urunler[i].URUNLER[j].URUNOZELLIK.Count; k++)
                    {
                        UrunlerFPanel[i].Satis[j].SatisOzellik[k].UrunAdet.Text = "0";
                    }
                }
            }
            UrunlerFPanel[int.Parse(indis[0])].Satis[int.Parse(indis[1])].SatisOzellik[int.Parse(indis[2])].UrunAdet.Text = "0";
            textBoxHizliSat.Focus();
        }
        void SepeteUrunEkle(string buttonname, string adet)
        {

            bool durum = true;
            for (int i = 0; i < flowLayoutPanelSatislar.Controls.Count; i++)
            {
                if (SepetFPanel[i].Fpanel.Name == buttonname)
                {
                    durum = false;
                    string[] indis = MenuNameBul(buttonname);
                    lblFiyat.Text = (decimal.Parse(FiyatBul(lblFiyat.Text)) - decimal.Parse(FiyatBul(SepetFPanel[i].UrunFiyatLabel.Text))).ToString() + " TL";
                    SepetFPanel[i].UrunAdet.Text = (int.Parse(SepetFPanel[i].UrunAdet.Text) + int.Parse(adet)).ToString();
                    SepetFPanel[i].UrunFiyatLabel.Text = (decimal.Parse(
                        FiyatBul(UrunlerFPanel[int.Parse(indis[0])].Satis[int.Parse(indis[1])].SatisOzellik[int.Parse(indis[2])].UrunFiyatLabel.Text))
                        * int.Parse(SepetFPanel[i].UrunAdet.Text)).ToString() + " TL";

                    lblFiyat.Text = (decimal.Parse(FiyatBul(lblFiyat.Text)) + decimal.Parse(FiyatBul(SepetFPanel[i].UrunFiyatLabel.Text))).ToString() + " TL";

                    break;
                }
            }
            if (durum)
            {
                string[] dizi = MenuNameBul(buttonname);
                if (flowLayoutPanelSatislar.Controls.Count < SepetFPanel.Count)
                {
                    SepetFPanel[flowLayoutPanelSatislar.Controls.Count].Fpanel.Name = UrunlerFPanel[int.Parse(dizi[0])].Satis[int.Parse(dizi[1])].SatisOzellik[int.Parse(dizi[2])].UrunOnayla.Name;
                    SepetFPanel[flowLayoutPanelSatislar.Controls.Count].UrunOzellikLabel.Text =
                        BL.UrunBilgisi.Urunler[int.Parse(dizi[0])].URUNLER[int.Parse(dizi[1])].URUNOZELLIK[int.Parse(dizi[2])].OZELLIK;
                    SepetFPanel[flowLayoutPanelSatislar.Controls.Count].UrunAdet.Text = adet;
                    SepetFPanel[flowLayoutPanelSatislar.Controls.Count].UrunOzellikLabel.Name = "L-" + dizi[0] + "_" + dizi[1] + "_" + dizi[2];
                    SepetFPanel[flowLayoutPanelSatislar.Controls.Count].UrunOzellikLabel.MouseMove += UrunOzellikLabel_MouseMove;
                    SepetFPanel[flowLayoutPanelSatislar.Controls.Count].UrunFiyatLabel.Text = (decimal.Parse(FiyatBul(
                        UrunlerFPanel[int.Parse(dizi[0])].Satis[int.Parse(dizi[1])].SatisOzellik[int.Parse(dizi[2])].UrunFiyatLabel.Text)) *
                        decimal.Parse(SepetFPanel[flowLayoutPanelSatislar.Controls.Count].UrunAdet.Text)).ToString() + " TL";
                    SepetFPanel[flowLayoutPanelSatislar.Controls.Count].UrunAdet.Name = "T-" + flowLayoutPanelSatislar.Controls.Count.ToString();
                    SepetFPanel[flowLayoutPanelSatislar.Controls.Count].UrunArti.Name = "A-" + flowLayoutPanelSatislar.Controls.Count.ToString();
                    SepetFPanel[flowLayoutPanelSatislar.Controls.Count].UrunEksi.Name = "E-" + flowLayoutPanelSatislar.Controls.Count.ToString();
                    SepetFPanel[flowLayoutPanelSatislar.Controls.Count].UrunFiyatLabel.Name = "F-" + flowLayoutPanelSatislar.Controls.Count.ToString();
                    SepetFPanel[flowLayoutPanelSatislar.Controls.Count].UrunOnayla.Name = "O-" + flowLayoutPanelSatislar.Controls.Count.ToString();

                    flowLayoutPanelSatislar.Controls.Add(SepetFPanel[flowLayoutPanelSatislar.Controls.Count].Fpanel);
                    lblFiyat.Text = (decimal.Parse(FiyatBul(lblFiyat.Text)) + decimal.Parse(FiyatBul(SepetFPanel[flowLayoutPanelSatislar.Controls.Count - 1].UrunFiyatLabel.Text))).ToString() + " TL";
                }
                else
                {
                    FlowLayoutPanel Fpanel = new FlowLayoutPanel();
                    Fpanel.Height = 40;
                    Fpanel.Name = UrunlerFPanel[int.Parse(dizi[0])].Satis[int.Parse(dizi[1])].SatisOzellik[int.Parse(dizi[2])].UrunOnayla.Name;
                    Fpanel.Width = flowLayoutPanelSatislar.Width;
                    Fpanel.Margin = tableLayoutPanel2.Margin;
                    Fpanel.Dock = DockStyle.Top;

                    TableLayoutPanel SepetTable = new TableLayoutPanel();
                    SepetTable.Location = new System.Drawing.Point(0, 0);
                    SepetTable.BackColor = Color.FromArgb(20, 20, 20);
                    SepetTable.ColumnCount = 8;
                    SepetTable.RowCount = 1;
                    SepetTable.Size = new System.Drawing.Size(
                        UrunlerFPanel[int.Parse(dizi[0])].Satis[int.Parse(dizi[1])].SatisOzellik[int.Parse(dizi[2])].Fpanel.Width,
                        UrunlerFPanel[int.Parse(dizi[0])].Satis[int.Parse(dizi[1])].SatisOzellik[int.Parse(dizi[2])].SepetTable.Height);
                    SepetTable.Margin =
                        UrunlerFPanel[int.Parse(dizi[0])].Satis[int.Parse(dizi[1])].SatisOzellik[int.Parse(dizi[2])].SepetTable.Margin;

                    SepetTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

                    SepetTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
                    SepetTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5));
                    SepetTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
                    SepetTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
                    SepetTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15));
                    SepetTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
                    SepetTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
                    SepetTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));

                    Label LabelsUrunOzellik = new Label();
                    LabelsUrunOzellik.Text =
                        UrunlerFPanel[int.Parse(dizi[0])].Satis[int.Parse(dizi[1])].SatisOzellik[int.Parse(dizi[2])].UrunOzellikLabel.Text;
                    LabelsUrunOzellik.Name = "L-" + dizi[0] + "_" + dizi[1] + "_" + dizi[2];
                    LabelsUrunOzellik.MouseMove += UrunOzellikLabel_MouseMove;
                    LabelsUrunOzellik.ForeColor = Color.White;
                    LabelsUrunOzellik.Anchor = AnchorStyles.None;
                    LabelsUrunOzellik.Margin = flowLayoutPanelSatislar.Margin;
                    LabelsUrunOzellik.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                    SepetTable.Controls.Add(LabelsUrunOzellik, 0, 0);

                    SepetTable.Controls.Add(Xlabel(), 1, 0);

                    TextBox UrunAdet = new TextBox();
                    UrunAdet.Text = adet;
                    UrunAdet.ForeColor = Color.Black;
                    UrunAdet.Anchor = AnchorStyles.None;
                    UrunAdet.Margin = new Padding(3, 0, 3, 0);
                    SepetTable.Controls.Add(
                        UrunAdet, 2, 0);

                    Label UrunFiyatLabel = new Label();
                    UrunFiyatLabel.Text = (decimal.Parse(FiyatBul(
                        UrunlerFPanel[int.Parse(dizi[0])].Satis[int.Parse(dizi[1])].SatisOzellik[int.Parse(dizi[2])].UrunFiyatLabel.Text)) *
                        int.Parse(adet)).ToString() + " TL";
                    UrunFiyatLabel.ForeColor = Color.White;
                    UrunFiyatLabel.Anchor = AnchorStyles.None;
                    UrunFiyatLabel.Margin = flowLayoutPanelSatislar.Margin;
                    UrunFiyatLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));

                    SepetTable.Controls.Add(
                        UrunFiyatLabel, 4, 0);

                    Button UrunArti = new Button();
                    UrunArti.BackColor = Color.FromArgb(40, 40, 40);
                    UrunArti.ForeColor = Color.White;
                    UrunArti.Name = "btnArtiSatis-" + (flowLayoutPanelSatislar.Controls.Count).ToString();
                    UrunArti.Text = "+";
                    UrunArti.FlatAppearance.BorderSize = 0;
                    UrunArti.Click += ButtonsArtiSatis_Click;
                    UrunArti.Margin = tableLayoutPanel2.Margin;
                    UrunArti.Dock = DockStyle.Fill;
                    UrunArti.FlatStyle = FlatStyle.Flat;
                    SepetTable.Controls.Add(
                        UrunArti, 5, 0);

                    Button UrunEksi = new Button();
                    UrunEksi.BackColor = Color.FromArgb(20, 20, 20);
                    UrunEksi.ForeColor = Color.White;
                    UrunEksi.Name = "btnEksiSatis-" + (flowLayoutPanelSatislar.Controls.Count).ToString();
                    UrunEksi.Text = "-";
                    UrunEksi.FlatAppearance.BorderSize = 0;
                    UrunEksi.Click += ButtonsEksiSatis_Click;
                    UrunEksi.Margin = tableLayoutPanel2.Margin;
                    UrunEksi.Dock = DockStyle.Fill;
                    UrunEksi.FlatStyle = FlatStyle.Flat;
                    SepetTable.Controls.Add(
                        UrunEksi, 6, 0);

                    Button UrunOnayla = new Button();
                    UrunOnayla.BackColor = Color.FromArgb(40, 40, 40);
                    UrunOnayla.ForeColor = Color.White;
                    UrunOnayla.Name = "btnSilSatis-" + (flowLayoutPanelSatislar.Controls.Count).ToString();
                    UrunOnayla.Text = "X";
                    UrunOnayla.FlatAppearance.BorderSize = 0;
                    UrunOnayla.Click += ButtonsSilSatis_Click;
                    UrunOnayla.Margin = tableLayoutPanel2.Margin;
                    UrunOnayla.Dock = DockStyle.Fill;
                    UrunOnayla.FlatStyle = FlatStyle.Flat;
                    SepetTable.Controls.Add(
                        UrunOnayla, 7, 0);

                    Fpanel.Controls.Add(SepetTable);

                    SepetFPanel.Add(new JETSATVIEW.SepetVIEW(Fpanel, SepetTable, LabelsUrunOzellik, UrunFiyatLabel, UrunAdet, UrunArti, UrunEksi, UrunOnayla));
                    flowLayoutPanelSatislar.Controls.Add(Fpanel);

                    lblFiyat.Text = (decimal.Parse(FiyatBul(lblFiyat.Text)) + decimal.Parse(FiyatBul(SepetFPanel[flowLayoutPanelSatislar.Controls.Count - 1].UrunFiyatLabel.Text))).ToString() + " TL";
                }
            }
            if (flowLayoutPanelSatislar.Controls.Count > 0)
            {
                flowLayoutPanelSatislar.BackgroundImage = null;
            }

            textBoxHizliSat.Focus();
        }

        private void UrunOzellikLabel_MouseMove(object sender, MouseEventArgs e)
        {
            string[] indisler = PanelReferansBul((sender as Label).Name);
            tooltip.RemoveAll();//ekranda gosterecek nesneyi temizliyoruz.
            tooltip.Show(BL.UrunBilgisi.Urunler[int.Parse(indisler[0])].URUNLER[int.Parse(indisler[1])].URUN + " "
                + BL.UrunBilgisi.Urunler[int.Parse(indisler[0])].URUNLER[int.Parse(indisler[1])].URUNOZELLIK[int.Parse(indisler[2])].OZELLIK, sender as Label, e.X, e.Y - 15, 500);

        }

        Label Xlabel()
        {
            return new Label
            {
                Text = "x",
                ForeColor = Color.White,
                Anchor = AnchorStyles.None,
                Margin = flowLayoutPanelSatislar.Margin,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)))
            };
        }

        private void ButtonsSilSatis_Click(object sender, EventArgs e)//sepeteki urunu silen button click eventi.
        {
            int indis = MenuIdBul((sender as Button).Name);
            lblFiyat.Text = (decimal.Parse(FiyatBul(lblFiyat.Text)) - decimal.Parse(FiyatBul(SepetFPanel[indis].UrunFiyatLabel.Text))).ToString() + " TL";
            SepetFPanel[indis].UrunFiyatLabel.Text = "";
            SepetFPanel[indis].UrunOzellikLabel.Text = "";
            SepetFPanel[indis].UrunAdet.Text = "1";

            flowLayoutPanelSatislar.Controls.RemoveAt(indis);

            SepetFPanel.Add(SepetFPanel[indis]);
            SepetFPanel.RemoveAt(indis);

            for (int i = 0; i < flowLayoutPanelSatislar.Controls.Count; i++)
            {
                SepetFPanel[i].UrunOnayla.Name = "O-" + i.ToString();
            }

            if (flowLayoutPanelSatislar.Controls.Count == 0)
            {
                flowLayoutPanelSatislar.BackgroundImage = global::ASGJETSAT.PL.Properties.Resources.Sepett;
            }
            textBoxHizliSat.Focus();
        }

        private void ButtonsEksiSatis_Click(object sender, EventArgs e)// sepeteki urunun adetini azaltan button click eventi.
        {
            int indis = MenuIdBul((sender as Button).Name); //button nameden indisi buluyor.
            if (!(int.Parse(SepetFPanel[indis].UrunAdet.Text) <= 1))//1 den kucuk olmadıgı surece işlem yapıyorç
            {//ilk o urunlerin tüm fiyatını çıkartıyor daha sonra kalan adet sayısı ile çarpılmıs fiyatla yeniden ekliyor.
                string[] dizi = MenuNameBul(SepetFPanel[indis].Fpanel.Name);
                lblFiyat.Text = (decimal.Parse(FiyatBul(lblFiyat.Text)) - decimal.Parse(FiyatBul(SepetFPanel[indis].UrunFiyatLabel.Text))).ToString() + " TL";
                SepetFPanel[indis].UrunAdet.Text = (int.Parse(SepetFPanel[indis].UrunAdet.Text) - 1).ToString();
                SepetFPanel[indis].UrunFiyatLabel.Text = (decimal.Parse(
                        FiyatBul(UrunlerFPanel[int.Parse(dizi[0])].Satis[int.Parse(dizi[1])].SatisOzellik[int.Parse(dizi[2])].UrunFiyatLabel.Text))
                        * int.Parse(SepetFPanel[indis].UrunAdet.Text)).ToString() + " TL";
                lblFiyat.Text = (decimal.Parse(FiyatBul(lblFiyat.Text)) + decimal.Parse(FiyatBul(SepetFPanel[indis].UrunFiyatLabel.Text))).ToString() + " TL";
            }
            textBoxHizliSat.Focus();
        }

        private void ButtonsArtiSatis_Click(object sender, EventArgs e)// sepeteki urunun adet sayısını artırma.
        {//ilk o urunlerin tüm fiyatını çıkartıyor daha sonra kalan adet sayısı ile çarpılmıs fiyatla yeniden ekliyor.
            int indis = MenuIdBul((sender as Button).Name);
            string[] dizi = MenuNameBul(SepetFPanel[indis].Fpanel.Name);
            lblFiyat.Text = (decimal.Parse(FiyatBul(lblFiyat.Text)) - decimal.Parse(FiyatBul(SepetFPanel[indis].UrunFiyatLabel.Text))).ToString() + " TL";
            SepetFPanel[indis].UrunAdet.Text = (int.Parse(SepetFPanel[indis].UrunAdet.Text) + 1).ToString();
            SepetFPanel[indis].UrunFiyatLabel.Text = (decimal.Parse(
                        FiyatBul(UrunlerFPanel[int.Parse(dizi[0])].Satis[int.Parse(dizi[1])].SatisOzellik[int.Parse(dizi[2])].UrunFiyatLabel.Text))
                        * int.Parse(SepetFPanel[indis].UrunAdet.Text)).ToString() + " TL";
            lblFiyat.Text = (decimal.Parse(FiyatBul(lblFiyat.Text)) + decimal.Parse(FiyatBul(SepetFPanel[indis].UrunFiyatLabel.Text))).ToString() + " TL";
            textBoxHizliSat.Focus();
        }

        private void ButtonsEksi_Click(object sender, EventArgs e)// urun satıs bolumundeki adet azaltma olayı.
        {
            try
            {
                string[] indis = MenuNameBul((sender as Button).Name); //indisi buluyoruz.
                if (!(int.Parse(UrunlerFPanel[int.Parse(indis[0])].Satis[int.Parse(indis[1])].SatisOzellik[int.Parse(indis[2])].UrunAdet.Text) <= 0)) //0 dan kucuk veya esit olmadıgı surece adeti 1 adet azaltıyor.
                    UrunlerFPanel[int.Parse(indis[0])].Satis[int.Parse(indis[1])].SatisOzellik[int.Parse(indis[2])].UrunAdet.Text =
                        (int.Parse(UrunlerFPanel[int.Parse(indis[0])].Satis[int.Parse(indis[1])].SatisOzellik[int.Parse(indis[2])].UrunAdet.Text) - 1).ToString();
            }
            catch
            {
                string[] indis = MenuNameBul((sender as Button).Name); //indisi buluyoruz.
                UrunlerFPanel[int.Parse(indis[0])].Satis[int.Parse(indis[1])].SatisOzellik[int.Parse(indis[2])].UrunAdet.Text = "0";
            }
            textBoxHizliSat.Focus();
        }

        private void ButtonsArti_Click(object sender, EventArgs e)// urun satıs bolumundeki adet artırma olayı.
        {
            try
            {
                string[] indis = MenuNameBul((sender as Button).Name);
                UrunlerFPanel[int.Parse(indis[0])].Satis[int.Parse(indis[1])].SatisOzellik[int.Parse(indis[2])].UrunAdet.Text =
                    (int.Parse(UrunlerFPanel[int.Parse(indis[0])].Satis[int.Parse(indis[1])].SatisOzellik[int.Parse(indis[2])].UrunAdet.Text) + 1).ToString();//adeti 1 adet artiriyor.
            }
            catch
            {
                string[] indis = MenuNameBul((sender as Button).Name);
                UrunlerFPanel[int.Parse(indis[0])].Satis[int.Parse(indis[1])].SatisOzellik[int.Parse(indis[2])].UrunAdet.Text = "0";//adeti 1 adet artiriyor.
            }
            textBoxHizliSat.Focus();
        }

        public void JETSAT_Load(object sender, EventArgs e)//form ilk acılısta yapması gerekenler
        {
            this.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        }
        public void MenuDuzenle()
        {
            _Kullanici = Entities.Kullanıcı.AktifKULLANICI;//giriş yapan kullanıcıyı _Kullanici deger olarak atıyoruz.

            flowLayoutPanelMenu.Controls.Clear();
            flowLayoutPanelUrun.Controls.Clear();
            flowLayoutPanelSatislar.Controls.Clear();
            try
            {
                MenuOlustur();
            }
            catch
            {

            }
            textBoxHizliSat.Focus();
        }

        private string FiyatBul(string Data)// ekranda gosterirken fiyatin sonuna tl yazılıyordu tl cıkarıp string tipinde gonderiyor.
        {
            string fiyat = "";
            for (int i = 0; i < Data.Length; i++)
            {
                if (Data[i] == 'T' && Data[i + 1] == 'L')
                    break;
                fiyat += Data[i];
            }
            return fiyat;
        }

        private void timer_CollapseMenu_Tick(object sender, EventArgs e)//acılır menu için timer kullanımı.
        {
            if (isCollapsed) //eger menu acılacaksa yapılacak işlemler
            {
                panelDropDown1.Height += 40;
                if (panelDropDown1.Size.Height >= panelDropDown1.MaximumSize.Height)
                {
                    panelDropDown1.Height = panelDropDown1.MaximumSize.Height;
                    timer_CollapseMenu.Stop();
                }
            }
            else //eger menu kapanacaksa yapılacak işlemler.
            {
                panelDropDown1.Height -= 40;
                if (panelDropDown1.Size.Height <= panelDropDown1.MinimumSize.Height)
                {
                    panelDropDown1.Height = panelDropDown1.MinimumSize.Height;
                    timerDurum = false;
                    timer_CollapseMenu.Stop();
                }
            }
        }

        private void timer_CollapseMenu2_Tick(object sender, EventArgs e) //acılır menuda 2 timer kullanımı burada eger onceden acılmıs menu varsa onu kapatamk için kullanıyoruz.
        {
            panelDropDown2.Height -= 40;
            if (panelDropDown2.Size.Height <= panelDropDown2.MinimumSize.Height)
            {
                panelDropDown2.Height = panelDropDown2.MinimumSize.Height;
                timerDurum = false;
                Thread.Sleep(100);
                timer_CollapseMenu.Start();
                timer_CollapseMenu2.Stop();
            }
        }

        string[] UrunOzellikReferansBul(string name)//urun ozellik kısmında kategori, urun ve urunozellik ındex bulmak için kullanılan fonksiyon.
        {
            for (int i = 0; i < name.Length; i++)
            {
                if (name[i] == '-')//indeximiz - den sonra basladıgı için - den sonrasını alıyoruz.
                {
                    name = name.Substring(i + 1);
                    break;
                }
            }
            return name.Split('_');//aldgımız yeride _ ile ındexleri ayırmıstık onuları dizi şekildende ayrırıyoruz.
        }

        private void btnSat_Click(object sender, EventArgs e)//sepeteki urunleri satan button click eventi.
        {
            int durum = -1;//sqlden gelen bilgi için tutuluyor.
            for (int i = 0; i < flowLayoutPanelSatislar.Controls.Count; i++)//toplam urun sayısı kadar donen dongu kuruyoruz buradakı amac
            {//en fazla sepette urun ozellik kadar olacagından tek tek kontrol etmek.
                if (SepetFPanel[i].UrunFiyatLabel.Text != "" && SepetFPanel[i].UrunAdet.Text != "0")
                {//eger panel bos degil ve kontrol amaclı LabelsUrunFiyatSatis TextBoxesAdetSatis degerler atamıstık onları kontrol edıyoruz. 

                    string[] refernas = UrunOzellikReferansBul(SepetFPanel[i].Fpanel.Name);//bakılan urunun indexleri bulunuyor.
                    DialogResult dialogResult = DialogResult.None;
                    if (BL.UrunBilgisi.Urunler[int.Parse(refernas[0])].URUNLER[int.Parse(refernas[1])].URUNOZELLIK[int.Parse(refernas[2])].STOK <= 0)//stok durumu kontrol ediliyor.
                    {
                        MessageBox.Show("Stokta Urun Bulunmamaktadır.\n" + BL.UrunBilgisi.Urunler[int.Parse(refernas[0])].URUNLER[int.Parse(refernas[1])].URUN
                            + " " + BL.UrunBilgisi.Urunler[int.Parse(refernas[0])].URUNLER[int.Parse(refernas[1])].URUNOZELLIK[int.Parse(refernas[2])].OZELLIK, "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (BL.UrunBilgisi.Urunler[int.Parse(refernas[0])].URUNLER[int.Parse(refernas[1])].URUNOZELLIK[int.Parse(refernas[2])].STOK//istenen stok durumu varmı bakılıyor.
                      - int.Parse(SepetFPanel[i].UrunAdet.Text) < 0)
                        {
                            dialogResult = MessageBox.Show("Stokta " + SepetFPanel[i].UrunAdet.Text + " adet bulunmamaktadır.\n" +
                                 BL.UrunBilgisi.Urunler[int.Parse(refernas[0])].URUNLER[int.Parse(refernas[1])].URUNOZELLIK[int.Parse(refernas[2])].STOK.ToString() + " Satılsınmı ?", "Hata", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                            if (dialogResult == DialogResult.Yes)// istenen stok durumu olmayıp en fazla verilebilecek stok durumunu istenirse adet durumunu stok durumunu eşit ediyor.
                            {
                                SepetFPanel[i].UrunAdet.Text = BL.UrunBilgisi.Urunler[int.Parse(refernas[0])].URUNLER[int.Parse(refernas[1])].URUNOZELLIK[int.Parse(refernas[2])].STOK.ToString();
                            }
                        }

                        if (dialogResult != DialogResult.No)//stok durumu mevcutsa yapılacak işlemler.
                        {

                            durum = BL.UrunSatis.SatisYap(BL.UrunBilgisi.Urunler[int.Parse(refernas[0])].URUNLER[int.Parse(refernas[1])].URUNOZELLIK[int.Parse(refernas[2])].ID
                                , BL.UrunBilgisi.Urunler[int.Parse(refernas[0])].URUNLER[int.Parse(refernas[1])].URUNOZELLIK[int.Parse(refernas[2])].STOK -
                                int.Parse(SepetFPanel[i].UrunAdet.Text), SepetFPanel[i].UrunOzellikLabel.Text, int.Parse(SepetFPanel[i].UrunAdet.Text),
                                decimal.Parse(FiyatBul(SepetFPanel[i].UrunFiyatLabel.Text)), DateTime.Now, _Kullanici, refernas);//satıs yapılıyor.
                            if (durum != -1)
                                //urun satıldıysa stok durumu azaltılıyor.
                                BL.UrunBilgisi.Urunler[int.Parse(refernas[0])].URUNLER[int.Parse(refernas[1])].URUNOZELLIK[int.Parse(refernas[2])].STOK -= int.Parse(SepetFPanel[i].UrunAdet.Text);
                            // eklenen urun sepeteki urunler hafızadan sılınmeden flowLayoutPanelSatislar silinip anlıyacagımız sekilde TextBoxesAdetSatis ve LabelsUrunFiyatSatis degerleri ataniyor.
                            SepetFPanel[i].UrunAdet.Text = "0";
                            SepetFPanel[i].UrunFiyatLabel.Text = "";

                        }
                    }
                }
            }
            if (durum != -1)
            {
                flowLayoutPanelSatislar.Controls.Clear();
                MessageBox.Show("Başarılı Bir Şekilde Satılmıştır.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Hata Satılamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (flowLayoutPanelSatislar.Controls.Count == 0)
            {
                flowLayoutPanelSatislar.BackgroundImage = global::ASGJETSAT.PL.Properties.Resources.Sepett;
            }
            textBoxHizliSat.Focus();
        }

        private void btnTemizle_Click(object sender, EventArgs e)//sepetteki tüm ürünler temizleniyor.
        {
            for (int i = 0; i < flowLayoutPanelSatislar.Controls.Count; i++)//toplam urun sayısı kadar donen dongu kuuryoruz.
            {
                if (SepetFPanel[i].UrunFiyatLabel.Text != "" && SepetFPanel[i].UrunAdet.Text != "0")//flow panelleri boş olmayan hepsini flowLayoutPanelSatislar atıyoruz.
                {
                    SepetFPanel[i].UrunFiyatLabel.Text = "";
                    SepetFPanel[i].UrunOzellikLabel.Text = "";
                    SepetFPanel[i].UrunAdet.Text = "1";
                }
                flowLayoutPanelSatislar.Controls.Clear();
            }
            lblFiyat.Text = "0TL";//ana fiyatı 0 TL olarak ayarlıyoruz.

            if (flowLayoutPanelSatislar.Controls.Count == 0)
            {
                flowLayoutPanelSatislar.BackgroundImage = global::ASGJETSAT.PL.Properties.Resources.Sepett;
            }
            textBoxHizliSat.Focus();
        }

        private void btnIptal_Click(object sender, EventArgs e)// sepete secılen urunleri ıptalını alındıgı bolum.
        {
            bool aciklamadurum = true;//urun neden iptal edildigini acıklandıgı bolum.
            string aciklama = "";
            int durum = -1; //sqlden gelen sonuc.
            for (int i = 0; i < flowLayoutPanelSatislar.Controls.Count; i++) //toplam urun sayısı kadar donen dongu kuruldu.
            {

                if (SepetFPanel[i].UrunFiyatLabel.Text != "" && SepetFPanel[i].UrunAdet.Text != "0") //eger urun sepetteyse
                {
                    if (aciklamadurum)//bir defa acıklama alıyoruz hepsine bu acıklama yazılacak.
                    {
                        aciklama = Microsoft.VisualBasic.Interaction.InputBox("Açıklama Giriniz", "Açıklama");//vb özelliği olan ınput girişi ozelligini kullanarak açıklama alıyoruz.
                        aciklamadurum = false;//bi daha ıptaller için acıklama almamak için false yapıyoruz.
                    }

                    string[] refernas = UrunOzellikReferansBul(SepetFPanel[i].Fpanel.Name);//ındexleri buluyoruz.

                    //iptal edilen urunleri Iptaller tablosuna kayıtını ekliyoruz.
                    durum = BL.UrunIptal.UrunIptalEt(BL.UrunBilgisi.Urunler[int.Parse(refernas[0])].URUNLER[int.Parse(refernas[1])].URUNOZELLIK[int.Parse(refernas[2])].ID
                                , BL.UrunBilgisi.Urunler[int.Parse(refernas[0])].URUNLER[int.Parse(refernas[1])].URUNOZELLIK[int.Parse(refernas[2])].STOK +
                                int.Parse(SepetFPanel[i].UrunAdet.Text), SepetFPanel[i].UrunOzellikLabel.Text, int.Parse(SepetFPanel[i].UrunAdet.Text),
                        decimal.Parse(FiyatBul(SepetFPanel[i].UrunFiyatLabel.Text)), DateTime.Now, _Kullanici, aciklama, int.Parse(refernas[0]), int.Parse(refernas[1]), int.Parse(refernas[2]));
                    if (durum != -1)
                        //stok durumunu iptalden sonra yeniden guncelliyoruz.
                        BL.UrunBilgisi.Urunler[int.Parse(refernas[0])].URUNLER[int.Parse(refernas[1])].URUNOZELLIK[int.Parse(refernas[2])].STOK += int.Parse(SepetFPanel[i].UrunAdet.Text);

                    //sepeteki urunu siliyoruz.
                    SepetFPanel[i].UrunAdet.Text = "0";
                    SepetFPanel[i].UrunFiyatLabel.Text = "";


                }
                flowLayoutPanelSatislar.Controls.Clear();

            }
            if (durum != -1)
            {
                MessageBox.Show("Başarılı Bir Şekilde Iptal Edilmiştir.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblFiyat.Text = "0TL";
            }
            else
                MessageBox.Show("Hata Iptal Edilmedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (flowLayoutPanelSatislar.Controls.Count == 0)
            {
                flowLayoutPanelSatislar.BackgroundImage = global::ASGJETSAT.PL.Properties.Resources.Sepett;
            }
            textBoxHizliSat.Focus();
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {//geri tusu ile anasayfaya geri donuyoruz.
            textBoxHizliSat.Focus();
            btnTemizle_Click(sender, e);
            Sayfalar.anaSayfa.Show();
            this.Hide();
        }

        void sepetiHafizayaAl()
        {
            List<Entities.Sepetler> sepets = new List<Entities.Sepetler>();
            for (int i = 0; i < flowLayoutPanelSatislar.Controls.Count; i++)
            {
                if (SepetFPanel[i].UrunFiyatLabel.Text != "" && SepetFPanel[i].UrunAdet.Text != "0") //eger urun sepetteyse
                {


                    sepets.Add(new Entities.Sepetler(SepetFPanel[i].Fpanel.Name, int.Parse(SepetFPanel[i].UrunAdet.Text)));

                    SepetFPanel[i].UrunAdet.Text = "0";
                    SepetFPanel[i].UrunFiyatLabel.Text = "";


                }
            }

            flowLayoutPanelSatislar.Controls.Clear();
            BL.SepetAyar.ListeSepet.Add(sepets);
            BL.SepetAyar.ListeFiyat.Add(decimal.Parse(FiyatBul(lblFiyat.Text)));
            lblFiyat.Text = "0TL";//ana fiyatı 0 TL olarak ayarlıyoruz.
            flowLayoutPanelSatislar.BackgroundImage = global::ASGJETSAT.PL.Properties.Resources.Sepett;
        }


        private void btn_YeniSepet_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanelSatislar.Controls.Count != 0)
            {
                sepetiHafizayaAl();
                flowLayoutPanelSatislar.BackgroundImage = global::ASGJETSAT.PL.Properties.Resources.Sepett;
            }
            textBoxHizliSat.Focus();
        }

        public void SepetGetir(int index)
        {
            for (int i = 0; i < BL.SepetAyar.ListeSepet[index].Count; i++)
            {
                SepeteUrunEkle(BL.SepetAyar.ListeSepet[index][i].INDIS.ToString(), BL.SepetAyar.ListeSepet[index][i].ADET.ToString());
            }

            BL.SepetAyar.ListeSepet.RemoveAt(index);
            BL.SepetAyar.ListeFiyat.RemoveAt(index);

            textBoxHizliSat.Focus();
        }

        private void btn_OncekiSepet_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanelSatislar.Controls.Count != 0)
            {
                if (MessageBox.Show("Sepeteki Ürünler Bekletilsin mi?", "Bekleme.", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    sepetiHafizayaAl();
                }
            }


            Sayfalar.sepettekiler.MdiParent = this.MdiParent;
            Sayfalar.sepettekiler.sepettekileriEkle();
            Sayfalar.sepettekiler.ShowDialog();
            textBoxHizliSat.Focus();
        }

        private void btn_BarkodArti_Click(object sender, EventArgs e)
        {
            try
            {
                txtBarkodAdet.Text = (int.Parse(txtBarkodAdet.Text) + 1).ToString();
            }
            catch
            {
                MessageBox.Show("Hata Adete Sayı Giriniz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBarkodAdet.Text = "1";
            }
            textBoxHizliSat.Focus();
        }

        private void btn_BarkodEksi_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(txtBarkodAdet.Text) > 1)
                    txtBarkodAdet.Text = (int.Parse(txtBarkodAdet.Text) - 1).ToString();
            }
            catch
            {
                MessageBox.Show("Hata Adete Sayı Giriniz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBarkodAdet.Text = "1";
            }
            textBoxHizliSat.Focus();
        }

        private void btn_BarkodEkle_Click(object sender, EventArgs e)
        {
            try
            {
                SepeteUrunEkle("Fpanel-" + BL.BarkodAyar.hizliArama(int.Parse(txtBarkod.Text)).ToString(), int.Parse(txtBarkodAdet.Text).ToString());
            }
            catch
            {
                MessageBox.Show("Hata Barkod Veya Adet Yanlış!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            txtBarkod.Text = "";
            txtBarkodAdet.Text = "1";
            textBoxHizliSat.Focus();
        }

        private void JETSAT_KeyDown(object sender, KeyEventArgs e)
        {
            textBoxHizliSat.Focus();
            textBoxHizliSat.Text = "";
            if (e.KeyValue == Keys.Escape.GetHashCode())
            {
                e.SuppressKeyPress = true;
                btnGeri_Click(sender, e);
            }
            else if (e.KeyValue == Keys.Enter.GetHashCode())
            {
                e.SuppressKeyPress = true;
                btnSat_Click(sender, e);
            }
            else if (e.KeyValue == Keys.Delete.GetHashCode())
            {
                e.SuppressKeyPress = true;
                btnTemizle_Click(sender, e);
            }
            else if (e.KeyValue == Keys.CapsLock.GetHashCode())
            {
                e.SuppressKeyPress = true;
                if (boolHizliSat)
                    boolHizliSat = false;
                else
                    boolHizliSat = true;
            }
            else if (e.KeyValue == Keys.Back.GetHashCode())
            {
                e.SuppressKeyPress = true;
                if (boolHizliSat)
                    txtBarkod.Text = "";
                else
                {
                    boolHizliSatAdet = true;
                    txtBarkodAdet.Text = "1";
                }
            }
            else if (e.KeyValue == Keys.F1.GetHashCode())
            {
                e.SuppressKeyPress = true;
                btn_OncekiSepet_Click(sender, e);
            }
            else if (e.KeyValue == Keys.F2.GetHashCode())
            {
                e.SuppressKeyPress = true;
                btn_YeniSepet_Click(sender, e);
            }
            else if (e.KeyValue == Keys.Space.GetHashCode())
            {
                e.SuppressKeyPress = true;
                btn_BarkodEkle_Click(sender, e);
                txtBarkod.Text = "";
                txtBarkodAdet.Text = "1";
            }
            else if (e.KeyValue == Keys.Subtract.GetHashCode())
            {
                e.SuppressKeyPress = true;
                try
                {
                    if (int.Parse(txtBarkodAdet.Text) > 1)
                        txtBarkodAdet.Text = (int.Parse(txtBarkodAdet.Text) - 1).ToString();
                }
                catch
                {
                    txtBarkodAdet.Text = "1";
                }
            }
            else if (e.KeyValue == Keys.Add.GetHashCode())
            {
                e.SuppressKeyPress = true;
                try
                {
                    txtBarkodAdet.Text = (int.Parse(txtBarkodAdet.Text) + 1).ToString();
                }
                catch
                {
                    txtBarkodAdet.Text = "1";
                }
            }
            else
            {
                try
                {
                    SepeteUrunEkle("Fpanel-" + BL.KisaYolAyar.hizliArama(int.Parse(ConfigurationManager.ConnectionStrings[e.KeyData.ToString().ToUpper()].ConnectionString)).ToString(), "1");
                }
                catch
                {
                    try
                    {
                        if (e.KeyValue == Keys.NumPad0.GetHashCode() || e.KeyValue == Keys.D0.GetHashCode())
                        {
                            e.SuppressKeyPress = true;
                            if (boolHizliSat)
                                txtBarkod.Text += "0";
                            else
                            {
                                if (boolHizliSatAdet)
                                {
                                    txtBarkodAdet.Text = "";
                                    boolHizliSatAdet = false;
                                }
                                txtBarkodAdet.Text += "0";
                            }
                        }
                        else if (e.KeyValue == Keys.NumPad1.GetHashCode() || e.KeyValue == Keys.D1.GetHashCode())
                        {
                            e.SuppressKeyPress = true;
                            if (boolHizliSat)
                                txtBarkod.Text += "1";
                            else
                            {
                                if (boolHizliSatAdet)
                                {
                                    txtBarkodAdet.Text = "";
                                    boolHizliSatAdet = false;
                                }
                                txtBarkodAdet.Text += "1";
                            }
                        }
                        else if (e.KeyValue == Keys.NumPad2.GetHashCode() || e.KeyValue == Keys.D2.GetHashCode())
                        {
                            e.SuppressKeyPress = true;
                            if (boolHizliSat)
                                txtBarkod.Text += "2";
                            else
                            {
                                if (boolHizliSatAdet)
                                {
                                    txtBarkodAdet.Text = "";
                                    boolHizliSatAdet = false;
                                }
                                txtBarkodAdet.Text += "2";
                            }
                        }
                        else if (e.KeyValue == Keys.NumPad3.GetHashCode() || e.KeyValue == Keys.D3.GetHashCode())
                        {
                            e.SuppressKeyPress = true;
                            if (boolHizliSat)
                                txtBarkod.Text += "3";
                            else
                            {
                                if (boolHizliSatAdet)
                                {
                                    txtBarkodAdet.Text = "";
                                    boolHizliSatAdet = false;
                                }
                                txtBarkodAdet.Text += "3";
                            }
                        }
                        else if (e.KeyValue == Keys.NumPad4.GetHashCode() || e.KeyValue == Keys.D4.GetHashCode())
                        {
                            e.SuppressKeyPress = true;
                            if (boolHizliSat)
                                txtBarkod.Text += "4";
                            else
                            {
                                if (boolHizliSatAdet)
                                {
                                    txtBarkodAdet.Text = "";
                                    boolHizliSatAdet = false;
                                }
                                txtBarkodAdet.Text += "4";
                            }
                        }
                        else if (e.KeyValue == Keys.NumPad5.GetHashCode() || e.KeyValue == Keys.D5.GetHashCode())
                        {
                            e.SuppressKeyPress = true;
                            if (boolHizliSat)
                                txtBarkod.Text += "5";
                            else
                            {
                                if (boolHizliSatAdet)
                                {
                                    txtBarkodAdet.Text = "";
                                    boolHizliSatAdet = false;
                                }
                                txtBarkodAdet.Text += "5";
                            }
                        }
                        else if (e.KeyValue == Keys.NumPad6.GetHashCode() || e.KeyValue == Keys.D6.GetHashCode())
                        {
                            e.SuppressKeyPress = true;
                            if (boolHizliSat)
                                txtBarkod.Text += "6";
                            else
                            {
                                if (boolHizliSatAdet)
                                {
                                    txtBarkodAdet.Text = "";
                                    boolHizliSatAdet = false;
                                }
                                txtBarkodAdet.Text += "6";
                            }
                        }
                        else if (e.KeyValue == Keys.NumPad7.GetHashCode() || e.KeyValue == Keys.D7.GetHashCode())
                        {
                            e.SuppressKeyPress = true;
                            if (boolHizliSat)
                                txtBarkod.Text += "7";
                            else
                            {
                                if (boolHizliSatAdet)
                                {
                                    txtBarkodAdet.Text = "";
                                    boolHizliSatAdet = false;
                                }
                                txtBarkodAdet.Text += "7";
                            }
                        }
                        else if (e.KeyValue == Keys.NumPad8.GetHashCode() || e.KeyValue == Keys.D8.GetHashCode())
                        {
                            e.SuppressKeyPress = true;
                            if (boolHizliSat)
                                txtBarkod.Text += "8";
                            else
                            {
                                if (boolHizliSatAdet)
                                {
                                    txtBarkodAdet.Text = "";
                                    boolHizliSatAdet = false;
                                }
                                txtBarkodAdet.Text += "8";
                            }
                        }
                        else if (e.KeyValue == Keys.NumPad9.GetHashCode() || e.KeyValue == Keys.D9.GetHashCode())
                        {
                            e.SuppressKeyPress = true;
                            if (boolHizliSat)
                                txtBarkod.Text += "9";
                            else
                            {
                                if (boolHizliSatAdet)
                                {
                                    txtBarkodAdet.Text = "";
                                    boolHizliSatAdet = false;
                                }
                                txtBarkodAdet.Text += "9";
                            }
                        }
                    }
                    catch
                    {

                    }
                }
            }
        }

        private void JETSAT_Shown(object sender, EventArgs e)
        {
            textBoxHizliSat.Focus();
        }

        private void txtBarkod_MouseClick(object sender, MouseEventArgs e)
        {
            boolHizliSat = true;
            textBoxHizliSat.Focus();
        }

        private void txtBarkodAdet_MouseClick(object sender, MouseEventArgs e)
        {
            boolHizliSat = false;
            textBoxHizliSat.Focus();
        }
    }
}
