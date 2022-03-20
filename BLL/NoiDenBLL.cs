using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class NoiDenBLL
    {
        NoiDenDAL noiDenDal = new NoiDenDAL();
        public Dictionary<string,NoiDen> LayNoiDen()
        {
            return noiDenDal.LayNoiDen();
        }
    }
}
