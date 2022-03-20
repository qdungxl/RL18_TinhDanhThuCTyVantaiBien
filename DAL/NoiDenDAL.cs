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
    public class NoiDenDAL
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
        /// Trả về dictionary với key = tên, value = nơi đến.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string,NoiDen> LayNoiDen()
        {
            Dictionary<string, NoiDen> dic = new Dictionary<string, NoiDen>();
            openconn();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select *from NoiDen";
            command.Connection = conn;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string Ma = reader.GetString(0);
                string Ten = reader.GetString(1);
                int Gia = reader.GetInt32(2);
                NoiDen nd = new NoiDen() { Ma = Ma, Ten = Ten, Gia = Gia };
                dic.Add(Ten, nd);
            }
            reader.Close();
            return dic;
        }
    }
}
