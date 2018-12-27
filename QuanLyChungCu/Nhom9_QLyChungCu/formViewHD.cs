using Nhom9_QLyChungCu.DAO;
using System;
using System.Windows.Forms;

namespace Nhom9_QLyChungCu
{
    public partial class formViewHD : Form
    {
        HoaDonDAO hd;
        private string currentHDCT = "";
        private string currentHD = "";
        private string mhd="";


        public formViewHD(string ch)
        {
            InitializeComponent();           
            btnSeeMore.Enabled = false;
            mhd = ch;
            loadData();
        }
        void loadData()
        {
            hd = new HoaDonDAO();
            dataGridView1.DataSource = hd.getHDbyCanHo(mhd);
        }

        private void btnSeeMore_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count == 1)
            {
                MessageBox.Show("Không có thông tin hiển thị!");
                return;

            }
            formCTHD ct = new formCTHD( currentHDCT, currentHD);
            ct.ShowDialog();
            loadData();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;           
            int index = dataGridView1.CurrentRow.Index;
            DataGridViewRow row = dataGridView1.Rows[index];
            DataGridViewCellCollection cel = row.Cells;
            currentHDCT = cel[2].Value.ToString();
            currentHD = cel[1].Value.ToString();
            if(currentHDCT=="") btnSeeMore.Enabled = false;
            else btnSeeMore.Enabled = true;
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Hiển thị theo...") return;
            if(comboBox1.Text == "Ngày")
            {
                string s = dateTimePicker1.Value.ToString();
                string[] temp = s.Split(' ');
                temp = temp[0].Split('/');
                dataGridView1.DataSource = hd.getHoaDonByNgay(int.Parse(temp[1]), int.Parse(temp[0]), int.Parse(temp[2]), mhd);
                return;
            }
            else if(comboBox1.Text == "Tháng")
            {
                string s = dateTimePicker1.Value.ToString();
                string[] temp = s.Split(' ');
                temp = temp[0].Split('/');
                dataGridView1.DataSource = hd.getHoaDonByThang(int.Parse(temp[0]), int.Parse(temp[2]), mhd);
                return;
            }
            else
            {
                string s = dateTimePicker1.Value.ToString();
                string[] temp = s.Split(' ');
                temp = temp[0].Split('/');
                dataGridView1.DataSource = hd.getHoaDonByNam(int.Parse(temp[2]), mhd);
                return;
            }
        }
    }
}
