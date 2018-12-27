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
using cExcel = Microsoft.Office.Interop.Excel;

namespace Nhom9_QLyChungCu
{
    public partial class formCTHD : Form
    {
        string currentmahdct;
        string currentIDDichVu;
        string currenthd;
        int currentGia;
        HoaDonDAO hoaDon;

        public formCTHD(string hdct, string hd)
        {
            InitializeComponent();          
            currentmahdct = hdct;
            currenthd = hd;
            loadData();
            this.Text = "Thông tin chi tiết cho hóa đơn: " + hdct;
        }
        void loadData()
        {
            hoaDon = new HoaDonDAO();
            dataGridView1.DataSource = hoaDon.getCTHDByID(currentmahdct, currenthd);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "(Tất cả các tệp)|*.*|(Các file excel)|*.xlsx";
            f.ShowDialog();
            if (f.FileName != "")
            {
                try
                {
                   this.Enabled = false;
                    export2Excel(dataGridView1, f.FileName);
                    MessageBox.Show("Tạo file thành công!", "Thông báo");
                    this.Enabled = true;
                }
                catch
                {
                    MessageBox.Show("Tạo file không thành công. Vui lòng thử lại sau!", "Thông báo");
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn tập tin nào!", "Thông báo");
            }           
        }
        public void export2Excel(DataGridView g, string fileName)
        {

            cExcel.Application obj = new cExcel.Application();
            obj.Application.Workbooks.Add(Type.Missing);
            obj.Columns.ColumnWidth = 25;
            for (int i = 1; i < g.Columns.Count + 1; i++)
            {
                obj.Cells[1, i] = g.Columns[i - 1].HeaderText;
            }
            for (int i = 0; i < g.Rows.Count; i++)
            {
                for (int j = 0; j < g.Columns.Count; j++)
                {
                    if (g.Rows[i].Cells[j].Value != null)
                    {
                        obj.Cells[i + 2, j + 1] = g.Rows[i].Cells[j].Value.ToString();
                    }
                }
            }
            obj.ActiveWorkbook.SaveCopyAs(fileName);
            obj.ActiveWorkbook.Saved = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            //xóa hóa đơn chi tiết
            object[] ob = new object[] { currenthd, currentmahdct, currentIDDichVu, currentGia };
            try
            {
                int i = DataProvider.GetDataFromSQL.ExecuteNonQuery(@"EXEC dbo.USP_DeleteHDCT @hd , @idHDCT , @madv , @gia", ob);
                loadData();
                MessageBox.Show("Đã xóa thành công dịch vụ ra khỏi hóa đơn!");
            }
            catch
            {
                MessageBox.Show("Xóa không thành công vui lòng thủ lại sau!");
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //int index = dgvBang.CurrentRow.Index;
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                DataGridViewCellCollection cel = row.Cells;
                currentIDDichVu = cel[0].Value.ToString();

                int.TryParse(cel[5].Value.ToString(), out currentGia);
                //currentHD = cel[1].Value.ToString();
                if (currentIDDichVu == "") btnXoa.Enabled = false;
                else btnXoa.Enabled = true;
            }
            catch { }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0) return;
                int index = dataGridView1.CurrentRow.Index;
                DataGridViewRow row = dataGridView1.Rows[index];
                DataGridViewCellCollection cel = row.Cells;
                currentIDDichVu = cel[0].Value.ToString();
                currentGia = int.Parse(cel[5].Value.ToString());
                string currentRow = cel[1].Value.ToString();
                if (currentRow == "")
                {
                    btnXoa.Enabled = false;
                }
                else
                {
                    btnXoa.Enabled = true;
                }
            }
            catch { }
        }
    }
}
