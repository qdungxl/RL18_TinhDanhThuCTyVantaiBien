using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DanhThuDAL
    {
        string sqlconn = @"Data Source=QUOCDUNGSURFACE\SQLEXPRESS01;Initial Catalog=CSDL_BTRL18_VanTaiBien;Integrated Security=True";
        SqlConnection conn = null;
        private void openconn()
        {
            if (conn == null)
                conn = new SqlConnection(sqlconn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
        }
        private void closeconn()
        {
            if (conn != null || conn.State == ConnectionState.Open)
                conn.Close();
        }
        /// <summary>
        /// Trả về list DanhThu trong 1 ngày.
        /// </summary>
        /// <returns></returns>
        public List<DanhThu> LayDanhThuBaoCao(DateTime NgayXem)
        {
            List<DanhThu> lsDanhThu = new List<DanhThu>();
            try
            {
                openconn();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select *from DanhThu where Ngay = @ngay";
                command.Parameters.Add("@ngay", SqlDbType.DateTime).Value = NgayXem;
                command.Connection = conn;
                SqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    DanhThu dthu = new DanhThu();
                    dthu.Ngay = reader.GetDateTime(0);
                    NoiDen nd = new NoiDen();
                    nd.Ten = reader.GetString(1);
                    dthu.NoiDen = nd;
                    dthu.SoLuong = reader.GetInt32(2);
                    lsDanhThu.Add(dthu);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
            }
            return lsDanhThu;
            
        }

        /// <summary>
        /// Trả về số lượng nếu tìm thấy.
        /// Trả về 0 nếu không tìm thấy.
        /// </summary>
        /// <param name="danhthu"></param>
        /// <returns></returns>
        public int TimKiem(DanhThu danhthu)
        {
            int kq = 0;
            try
            {
                openconn();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select *from DanhThu where Ngay=@ngay and NoiDen=@noiden";
                DateTime ngay = danhthu.Ngay.Date;
                command.Parameters.Add("@ngay", SqlDbType.DateTime).Value = ngay;
                string noiden = danhthu.NoiDen.Ten;
                command.Parameters.Add("@noiden", SqlDbType.NVarChar).Value = noiden;
                command.Connection = conn;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    kq = reader.GetInt32(2);
                }
                reader.Close();
            }
            catch(Exception ex)
            {
            }
            return kq;
        }
        /// <summary>
        /// Trả về true nếu thêm thành công
        /// Trả về false nếu thêm thất bại
        /// </summary>
        /// <param name="danhthu"></param>
        /// <returns></returns>
        public bool ThemMoiDuLieuDanhThu(DanhThu danhthu)
        {
            openconn();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "insert into DanhThu(Ngay,NoiDen,SoLuong) values (@ngay,@noiden,@soluong)";
            command.Parameters.Add("@ngay", SqlDbType.DateTime).Value = danhthu.Ngay.Date;
            command.Parameters.Add("@noiden", SqlDbType.NVarChar).Value = danhthu.NoiDen.Ten;
            command.Parameters.Add("@soluong", SqlDbType.Int).Value = danhthu.SoLuong;
            command.Connection = conn;
            int ret = command.ExecuteNonQuery();
            if (ret > 0)
                return true;
            return false;
        }
        /// <summary>
        /// Trả về true nếu sửa thành công
        /// Trả về false nếu sủa thất bại
        /// </summary>
        /// <param name="danhthu"></param>
        /// <returns></returns>
        public bool SuaDuLieuDanhThu(DanhThu danhthu)
        {
            bool kq = false;
            try
            {
                openconn();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "update DanhThu set SoLuong=@soluong where Ngay = @ngay and NoiDen=@noiden";
                command.Parameters.Add("@ngay", SqlDbType.DateTime).Value = danhthu.Ngay.Date;
                command.Parameters.Add("@noiden", SqlDbType.NVarChar).Value = danhthu.NoiDen.Ten;
                command.Parameters.Add("@soluong", SqlDbType.Int).Value = danhthu.SoLuong;
                command.Connection = conn;
                command.ExecuteNonQuery();
                kq =  true;
            }
            catch(Exception ex)
            {
            }
            return kq;
        }
       
    }
}
