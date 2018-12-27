using Nhom9_QLyChungCu.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhom9_QLyChungCu.DAO
{
    class KhachHangDAO
    {
        public List<KhachHang> listKhachHang = new List<KhachHang>();
        public int count = 0;
        public KhachHangDAO() //hàm tạo = hàm load
        {
            DataTable data = DataProvider.GetDataFromSQL.ExeCuteQuery("SELECT * FROM dbo.ChuCanHo");
            foreach (DataRow item in data.Rows)
            {
                KhachHang ac = new KhachHang(item);
                this.listKhachHang.Add(ac);
            }
            count = data.Rows.Count;
        }
        public KhachHang getKHByID(string idKH) //chua dung
        {
            foreach(KhachHang item in listKhachHang)
            {
                if(idKH == item.ID)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
