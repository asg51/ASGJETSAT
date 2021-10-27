using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASGJETSAT.PL
{
    public class JETSATVIEW
    {
        public Button MenuButton;
        public FlowLayoutPanel FSepetPanel;
        public List<SatisVIEW> Satis = new List<SatisVIEW>();
        public JETSATVIEW(Button Mbutton, FlowLayoutPanel panel)
        {
            this.MenuButton = Mbutton;
            this.FSepetPanel = panel;
        }

        public class SatisVIEW
        {
            public FlowLayoutPanel SatisFPanel;
            public Button SatisButton;
            public List<SatisOzellikVIEW> SatisOzellik = new List<SatisOzellikVIEW>();
            public SatisVIEW(FlowLayoutPanel Fpanel,Button button)
            {
                this.SatisFPanel = Fpanel;
                this.SatisButton = button;
            }
        }

        public class SatisOzellikVIEW
        {
            public FlowLayoutPanel Fpanel;
            public TableLayoutPanel SepetTable;
            public Label UrunOzellikLabel;
            public Label UrunFiyatLabel;
            public TextBox UrunAdet;
            public Button UrunArti;
            public Button UrunEksi;
            public Button UrunOnayla;
            public SatisOzellikVIEW(FlowLayoutPanel panel, TableLayoutPanel SepetT, Label UrunOzellikL, Label UrunFiyatL,
                TextBox UrunAd, Button UrunA, Button UrunE, Button UrunO)
            {
                this.Fpanel = panel;
                this.SepetTable = SepetT;
                this.UrunOzellikLabel = UrunOzellikL;
                this.UrunFiyatLabel = UrunFiyatL;
                this.UrunAdet = UrunAd;
                this.UrunArti = UrunA;
                this.UrunEksi = UrunE;
                this.UrunOnayla = UrunO;
            }
        }

        public class SepetVIEW
        {
            public FlowLayoutPanel Fpanel;
            public TableLayoutPanel SepetTable;
            public Label UrunOzellikLabel;
            public Label UrunFiyatLabel;
            public TextBox UrunAdet;
            public Button UrunArti;
            public Button UrunEksi;
            public Button UrunOnayla;

            public SepetVIEW(FlowLayoutPanel panel, TableLayoutPanel SepetT, Label UrunOzellikL, Label UrunFiyatL,
                TextBox UrunAd, Button UrunA, Button UrunE, Button UrunO)
            {
                this.Fpanel = panel;
                this.SepetTable = SepetT;
                this.UrunOzellikLabel = UrunOzellikL;
                this.UrunFiyatLabel = UrunFiyatL;
                this.UrunAdet = UrunAd;
                this.UrunArti = UrunA;
                this.UrunEksi = UrunE;
                this.UrunOnayla = UrunO;
            }
        }
    }
}
