using Nhom9_QLyChungCu.DAO;
using System;
using System.Data;
using System.Data.Sql;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Nhom9_QLyChungCu
{
    public partial class formKetNoiSQL : Form
    {
        public formKetNoiSQL()
        {
            InitializeComponent();
            string line = "";        

            using (StreamReader sr = new StreamReader(Application.StartupPath + "\\config.inf"))
            {
                line = sr.ReadLine();
            }
            //tách từng thành phần ra
            string[] part = line.Split(';');
            string[] temp = part[0].Split('\\');
            txbServer.Text = temp[temp.Length - 1];
            string[] temp1 = part[1].Split('=');
            txbData.Text = temp1[temp1.Length - 1];
            lbSTT.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //public string connectionSTR = @"Data Source=.\sqlexpress;Initial Catalog=QlyChungCu;Integrated Security=True";
            lbSTT.Text = "Vui lòng đợi...";
            lbSTT.BringToFront();
            lbSTT.Show();
            panel1.Enabled = false;

            DataProvider.GetDataFromSQL.ConnectionSTR = @"Data Source=.\" + txbServer.Text + ";Initial Catalog=" + txbData.Text + ";Integrated Security=True";
            //thử ping đến dataBase
            string query = @"USE QuanLyChungCu";
            try
            {
                DataProvider.GetDataFromSQL.ExeCuteQuery(query);
            }
            catch
            {
                panel1.Enabled = true;
                lbSTT.Hide();
                MessageBox.Show("Kết nối này không khả dụng. Vui lòng chọn kết nối khác", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);             
                return;
            }

            panel1.Enabled = true;
            lbSTT.Hide();
            MessageBox.Show("Kết nối đến server thành công!", "Thông báo!");
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            lbSTT.Text = "Vui lòng đợi...";
            lbSTT.BringToFront();
            lbSTT.Show();
            panel1.Enabled = false;

            var instances = SqlDataSourceEnumerator.Instance.GetDataSources();
            foreach (DataRow instance in instances.AsEnumerable())
            {
                ListViewItem lvi = new ListViewItem(instance[1].ToString());
                lvi.SubItems.Add(instance[0].ToString() + @"\" + instance[1].ToString());
                listView1.Items.Add(lvi);
            }
            panel1.Enabled = true;
            lbSTT.Hide();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count > 0)
            {
                ListViewItem lv = listView1.SelectedItems[0];
                txbServer.Text = lv.Text;
            }
        }

        private void btnManualEdit_Click(object sender, EventArgs e)
        {
            txbServer.ReadOnly = false;
        }

        private void formKetNoiSQL_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                using (StreamWriter sr = new StreamWriter(Application.StartupPath + "\\config.inf"))
                {
                    sr.WriteLine(DataProvider.GetDataFromSQL.ConnectionSTR);
                }
            }
            catch
            {
               
            }

        }
    }
}
