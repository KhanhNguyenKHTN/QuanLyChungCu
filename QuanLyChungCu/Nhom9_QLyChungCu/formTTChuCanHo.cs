using Nhom9_QLyChungCu.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nhom9_QLyChungCu
{
    public partial class formTTChuCanHo : Form
    {
        public formTTChuCanHo(string id, string name)
        {
            InitializeComponent();
            loadData(id, name);
        }

        private void loadData(string id, string name)
        {
            string ngay = "";
            string query = @"SELECT * FROM dbo.ChuCanHo WHERE ID = N'"+ id + @"' AND name = N'"+ name +@"'";
            DataTable dt = DataProvider.GetDataFromSQL.ExeCuteQuery(query);
            foreach(DataRow item in dt.Rows)
            {
                txbName.Text = item["name"].ToString();
                txbGT.Text = item["GioiTinh"].ToString();
                txbCH.Text = item["IDCanHo"].ToString();
                txbCMND.Text = item["CMND"].ToString();
                txbTT.Text = item["TrangThai"].ToString();
                ngay = item["NgayMuaNha"].ToString();
            }
            string[] temp = ngay.Split(' ');
            string[] temp1 = temp[0].Split('/');
            txbDate.Text = String.Format("{0:00}", temp1[1]) + "/" + String.Format("{0:00}", temp1[0]) + "/" +  temp1[2];
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
