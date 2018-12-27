using Nhom9_QLyChungCu.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nhom9_QLyChungCu.DAO
{
    public class MessageDAO
    {
        List<YeuCau> listYC = new List<YeuCau>();

        public MessageDAO()
        {
            string query = @"SELECT * FROM dbo.yeucau";
            DataTable dt = DataProvider.GetDataFromSQL.ExeCuteQuery(query);
            //hd = dt;
            foreach (DataRow row in dt.Rows)
            {
                YeuCau yc = new YeuCau(row);
                listYC.Add(yc);
            }
        }
        public int CountYeuCau(Account ac)
        {
            int count = 0;
            foreach(YeuCau yc in listYC)
            {
                if(yc.To == ac.UserName && yc.DaXem == 1)
                {
                    count++;
                }
            }
            return count;
        }
        public DataTable GetListMessage(Account ac)
        {
            DataTable result = new DataTable();
            result.Columns.Add("ID", typeof(string));
            result.Columns.Add("Gửi từ", typeof(string));
            result.Columns.Add("Nội dung", typeof(string));
            result.Columns.Add("Tình trạng", typeof(string));
            foreach (YeuCau yc in listYC)
            {
                if (yc.To == ac.UserName)
                {
                    string s = "";
                    if (yc.DaXem == 1) s = "Chưa đọc";
                    else s = "Đã đọc";
                    result.Rows.Add(yc.Id, yc.From, yc.NoiDung, s);
                }
            }
            return result;
        }

        public bool DaDocTn(string id)
        {
            string query = "UPDATE dbo.yeucau SET daXem = 0 WHERE id =" + id;
            int i = DataProvider.GetDataFromSQL.ExecuteNonQuery(query);
            if (i == 0) return false;
            return true;
        }
        public bool XoaTn(string id)
        {
            string query = "DELETE FROM dbo.yeucau WHERE ID =" + id;
            int i = DataProvider.GetDataFromSQL.ExecuteNonQuery(query);
            if (i == 0) return false;
            return true;
        }
        public void ReLoad()
        {
            listYC.Clear();
            string query = @"SELECT * FROM dbo.yeucau";
            DataTable dt = DataProvider.GetDataFromSQL.ExeCuteQuery(query);
            //hd = dt;
            foreach (DataRow row in dt.Rows)
            {
                YeuCau yc = new YeuCau(row);
                listYC.Add(yc);
            }
        }

    }
}
