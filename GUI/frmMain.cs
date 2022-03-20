using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using BLL;

namespace GUI
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (KiemTraThongTinNhap() == false)
            {
                return;
            }
            DanhThu dthu = new DanhThu();
            dthu.Ngay = DateTime.Now.Date;
            NoiDen nd = new NoiDen();
            nd.Ten = cboNoiDen.Text.Trim();
            dthu.NoiDen = nd;
            dthu.SoLuong = int.Parse(txtKhoiLuong.Text);
            DanhThuBLL dthuBll = new DanhThuBLL();
            int TimKiem = dthuBll.TimKiem(dthu);
            if (TimKiem == 0) // dữ liệu mới chưa tồn tại trước đó nên thêm mới.
            {
                if (dthuBll.ThemMoiDanhThu(dthu))
                    MessageBox.Show("Thêm mới danh thu thành công.");
                else
                    MessageBox.Show("Thêm mới không thành công.");
            }
            if(TimKiem > 0) //dữ liệu mới đã có dữ liệu, nêu sửa lại số lượng.
            {
                if (TimKiem + dthu.SoLuong > 50)
                {
                    MessageBox.Show("Đã tồn tại đơn hàng và số lượng mới + số lượng cũ > 50");
                }
                else
                {
                    dthu.SoLuong = dthu.SoLuong + TimKiem;
                    if (dthuBll.SuaDuLieuDanhThu(dthu))
                        MessageBox.Show("Sửa dữ liệu thành công");
                    else
                        MessageBox.Show("Sửa dữ liệu thất bại");
                }
            }
              
            cboNoiDen.SelectedIndex = -1;
            txtKhoiLuong.Text = "";
        }
        /// <summary>
        /// Trả về true khi thông tin nhập OK
        /// Nếu thông tin sai thì hiển thị Error provoder lên GUI
        /// </summary>
        /// <returns></returns>
        private bool KiemTraThongTinNhap()
        {
            int kq = 0;
            errorProvider1.Clear();
            if (cboNoiDen.SelectedIndex == -1)
            {
                errorProvider1.SetError(cboNoiDen, "Hãy chọn nơi đến.");
                kq++;
            }
            
            if (txtKhoiLuong.Text=="")
            {              
                errorProvider1.SetError(txtKhoiLuong, "Hãy nhập khối lượng.");
                kq++;
            }
            else
            {
                int khoiLuong = int.Parse(txtKhoiLuong.Text);
                if (khoiLuong > 50)
                {
                    errorProvider1.SetError(txtKhoiLuong, "Hãy nhập khối lượng nhỏ hơn 50");
                    kq++;
                }
                   
            }
            if (kq == 0)
                return true;
            else
                return false;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadDuLieuLenComboBox();
        }
        /// <summary>
        /// Lấy dữ liệu từ bảng NoiDen và đưa lên ComboBox.
        /// </summary>
        private void LoadDuLieuLenComboBox()
        {
            NoiDenBLL noiDenBll = new NoiDenBLL();
            cboNoiDen.Items.Clear();
            foreach(NoiDen item in noiDenBll.LayNoiDen().Values.ToList())
            {
                cboNoiDen.Items.Add(item);
            }
        }

        private void btnQuayVe_Click(object sender, EventArgs e)
        {
            DialogResult ret = MessageBox.Show("Bạn muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(ret == DialogResult.Yes)
            {
                Close();
            }
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmBaoCao frm = new frmBaoCao();
            frm.ShowDialog();
            frm = null;
            this.Show();
        }
    }
}
