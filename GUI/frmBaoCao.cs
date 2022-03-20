using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;

namespace GUI
{
    public partial class frmBaoCao : Form
    {
        public frmBaoCao()
        {
            InitializeComponent();
        }

        private void btnQuayVe_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void frmBaoCao_Load(object sender, EventArgs e)
        {
            DanhThuBLL dtBLL = new DanhThuBLL();
            NoiDenBLL ndBLL = new NoiDenBLL();
            List<DanhThu> lsDT = dtBLL.LayDanhThuBaoCao(DateTime.Now.Date);
            Dictionary<string, NoiDen> dicND = ndBLL.LayNoiDen();
            int TongTien = 0;
            lvBaoCao.Items.Clear();
            foreach(DanhThu item in lsDT)
            {
                ListViewItem lvi = new ListViewItem(item.NoiDen.Ten);
                int gia = dicND[item.NoiDen.Ten].Gia;
                lvi.SubItems.Add(item.SoLuong * gia + "");
                lvBaoCao.Items.Add(lvi);
                TongTien = TongTien + item.SoLuong * gia;
            }
            ListViewItem lviCuoi = new ListViewItem("TỔNG CỘNG");
            lviCuoi.SubItems.Add(TongTien+"");
            lvBaoCao.Items.Add(lviCuoi);

        }
    }
}
