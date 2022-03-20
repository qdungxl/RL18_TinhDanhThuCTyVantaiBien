using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
using System.Data;
using System.Data.SqlClient;

namespace BLL
{
    public class DanhThuBLL
    {
        DanhThuDAL danhThuDal = new DanhThuDAL();
        /// <summary>
        /// Trả về true nếu thêm thành công
        /// Trả về false nếu thêm thất bại
        /// </summary>
        /// <param name="danhthu"></param>
        /// <returns></returns>
        public bool ThemMoiDanhThu(DanhThu dthu)
        {
            return danhThuDal.ThemMoiDuLieuDanhThu(dthu);
        }
        /// <summary>
        /// Trả về list DanhThu trong 1 ngày.
        /// </summary>
        /// <returns></returns>
        public List<DanhThu> LayDanhThuBaoCao(DateTime NgayXem)
        {
            return danhThuDal.LayDanhThuBaoCao(NgayXem);
        }
        /// <summary>
        /// Trả về true nếu sửa thành công
        /// Trả về false nếu sủa thất bại
        /// </summary>
        /// <param name="danhthu"></param>
        /// <returns></returns>
        public bool SuaDuLieuDanhThu(DanhThu dthu)
        {
            return danhThuDal.SuaDuLieuDanhThu(dthu);
        }
        /// <summary>
        /// Trả về số lượng nếu tìm thấy.
        /// Trả về 0 nếu không tìm thấy.
        /// </summary>
        /// <param name="danhthu"></param>
        /// <returns></returns>
        public int TimKiem(DanhThu dthu)
        {
            return danhThuDal.TimKiem(dthu);
        }
    }
}
