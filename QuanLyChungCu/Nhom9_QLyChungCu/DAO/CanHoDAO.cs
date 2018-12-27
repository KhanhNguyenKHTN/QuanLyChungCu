using Nhom9_QLyChungCu.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhom9_QLyChungCu.DAO
{
    class CanHoDAO
    {
        public List<CanHo> listCanHo = new List<CanHo>();


        public CanHoDAO() //hàm tạo = hàm load
        {
            loadData();
        }
        private void loadData()
        {
            DataTable data = DataProvider.GetDataFromSQL.ExeCuteQuery("SELECT * FROM dbo.CanHo");
            foreach (DataRow item in data.Rows)
            {
                CanHo ac = new CanHo(item);
                this.listCanHo.Add(ac);
            }
        }
        public void reloadData()
        {
            listCanHo.Clear();
            loadData();
        }
    }
}
