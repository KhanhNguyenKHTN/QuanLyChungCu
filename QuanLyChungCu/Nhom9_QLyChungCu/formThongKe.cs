using Nhom9_QLyChungCu.DAO;
using System;
using System.Data;
using System.Windows.Forms;
using cExcel = Microsoft.Office.Interop.Excel;

namespace Nhom9_QLyChungCu
{
    public partial class formThongKe : Form
    {
        HoaDonDAO hoaDon;
        private string currentHDCT = "";
        private string currentHD = "";
        DataTable CurrentDT = new DataTable();


        public formThongKe()
        {
            InitializeComponent();
            cbLoaiTK.Text = "Thống kê theo...";
            cbCanHo.Text = "Chọn căn hộ...";
            LoadForm();
            cbCanHo.Enabled = false;
            btnChiTiet.Enabled = false;
            btnSaveExcel.Enabled = false;
            btnDeleteHD.Enabled = false;

        }
        private void LoadForm()
        {
            hoaDon = new HoaDonDAO();
            if (cbLoaiTK.Text != "Thống kê theo..." && cbCanHo.Text == "Chọn căn hộ...")
            {
                LoadDataToDtg((string)cbLoaiTK.SelectedItem);
            }
            else if (cbLoaiTK.Text != "Thống kê theo..." && cbCanHo.Text != "Chọn căn hộ...")
            {
               LoadDataCanHo((string)cbCanHo.SelectedItem);
            }
        }
          

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > DateTime.Now)
            {
                MessageBox.Show("Vui lòng chọn ngày cho chính xác!");
                return;
            }
            string s = (string)cbLoaiTK.SelectedItem;
            LoadDataToDtg(s);

        }
        private void LoadDataToDtg(string s)
        {

            string z = dateTimePicker1.Value.ToString();
            string[] temp = z.Split(' ');
            temp = temp[0].Split('/');
            if (s == "Căn hộ")
            {
                cbCanHo.Enabled = true;
                return;
            }
            else
            {
                cbCanHo.Text = "Chọn căn hộ...";
                cbCanHo.Enabled = false;
            }
            if (s == "Ngày")
            {
                CurrentDT = hoaDon.getHoaDonByNgay(int.Parse(temp[1]), int.Parse(temp[0]), int.Parse(temp[2]));
                dgvBang.DataSource = CurrentDT;
                cbLoaiGiaTriHT.Text = "Căn Hộ";
                LoadChart(0, "Mã Phòng", "Tổng Doanh Thu");
                btnSaveExcel.Enabled = true;
                return;
            }
            if (s == "Tháng")
            {
                CurrentDT = hoaDon.getHoaDonByThang(int.Parse(temp[0]), int.Parse(temp[2]));
                dgvBang.DataSource = CurrentDT;
                cbLoaiGiaTriHT.Text = "Căn Hộ";
                LoadChart(0, "Mã Phòng", "Tổng Doanh Thu");
                btnSaveExcel.Enabled = true;
                return;
            }
            if (s == "Năm")
            {
                CurrentDT = hoaDon.getHoaDonByNam(int.Parse(temp[2]));
                dgvBang.DataSource = CurrentDT;
                cbLoaiGiaTriHT.Text = "Căn Hộ";
                LoadChart(0, "Mã Phòng", "Tổng Doanh Thu");
                btnSaveExcel.Enabled = true;
                return;
            }

        }
        
        private void cbCanHo_SelectedIndexChanged(object sender, EventArgs e)
        {

            string LayMaCanHo = (string)cbCanHo.SelectedItem;
            LoadDataCanHo(LayMaCanHo);
            //  return;
        }
        private void LoadDataCanHo(string layMaCanHo)
        {
            string[] temp1 = layMaCanHo.Split(' ');
            CurrentDT = hoaDon.getHDbyCanHo("HD" + temp1[2]);
            dgvBang.DataSource = CurrentDT;
            cbLoaiGiaTriHT.Text = "Tháng";
            LoadChart(1, "Tháng", "Tổng Doanh Thu");
            btnSaveExcel.Enabled = true;
        }
        private void dgvBang_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBang.SelectedRows.Count == 0) return;
            int index = dgvBang.CurrentRow.Index;
            DataGridViewRow row = dgvBang.Rows[index];
            DataGridViewCellCollection cel = row.Cells;
            currentHDCT = cel[2].Value.ToString();
            currentHD = cel[1].Value.ToString();
            if (currentHDCT == "")
            {
                btnChiTiet.Enabled = false;
                btnDeleteHD.Enabled = false;
            }
            else
            {
                btnChiTiet.Enabled = true;
                btnDeleteHD.Enabled = true;
            }
        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            if(dgvBang.Rows.Count == 1)
            {
                MessageBox.Show("Không có thông tin hiển thị!");
                return;
            }
            formCTHD f = new formCTHD(currentHDCT, currentHD);
            f.ShowDialog();
            LoadForm();
        }

        private void dgvBang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //int index = dgvBang.CurrentRow.Index;
                DataGridViewRow row = dgvBang.Rows[e.RowIndex];
                DataGridViewCellCollection cel = row.Cells;
                currentHDCT = cel[2].Value.ToString();
                currentHD = cel[1].Value.ToString();
                if (currentHDCT == "") btnChiTiet.Enabled = false;
                else btnChiTiet.Enabled = true;
            }
            catch { }
        }

        private void cbBieuDo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBieuDo.SelectedItem.ToString() ==  "Biểu Đồ Cột")
            {
                chartThongKe.Series["Series1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            }
            else if(cbBieuDo.SelectedItem.ToString() == "Biểu Đồ Tròn")
            {
                chartThongKe.Series["Series1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            }
            else
            {
                chartThongKe.Series["Series1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            }                
        }
        void ReloadChart()
        {
            chartThongKe.Series.Clear();
            chartThongKe.Series.Add("Series1");
        }
        void LoadChart(int type, string Loai, string value)
        {
            ReloadChart();
            DataTable temp = new DataTable();
            if(type == 0)
            {
               temp = hoaDon.ThongKeTheoCanHo(CurrentDT);
            }
            else
            {
                temp = hoaDon.ThongKeTheoThang(CurrentDT);
            }
            //hình tròn
            chartThongKe.Series["Series1"].XValueMember = Loai;
            chartThongKe.Series["Series1"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            chartThongKe.Series["Series1"].YValueMembers = value;
            chartThongKe.Series["Series1"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            chartThongKe.DataSource = temp;
            chartThongKe.DataBind();

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if(cbLoaiGiaTriHT.SelectedItem.ToString() == "Tháng")
            {
                LoadChart(1, "Tháng", "Tổng Doanh Thu");
            }
            else
            {
                LoadChart(0, "Mã Phòng", "Tổng Doanh Thu");
            }
        }

        private void btnSaveExcel_Click(object sender, EventArgs e)
        {
            //dung thu vien using cExcel = Microsoft.Office.Interop.Excel;
            SaveFileDialog f = new SaveFileDialog();
            //f.FileName = "QlyChungCuNhom9.xlsx";
            f.DefaultExt = "xlsx";
            f.Filter = "(Các file excel)|*.xlsx|(Tất cả các tệp)|*.*";
            f.AddExtension = true;
            f.RestoreDirectory = true;
            if (f.ShowDialog() != DialogResult.OK)
            {
                return;
            }


            if (f.FileName != "")
            {                                
                    this.Enabled = false;
                    export2Excel(dgvBang, f.FileName);
                    MessageBox.Show("Tạo file thành công!", "Thông báo");
                    this.Enabled = true;
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

        private void btnDeleteHD_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn thật sự muốn xóa hóa đơn này không?" + Environment.NewLine +"Nếu bạn đồng ý thì tất cả hóa đơn chi tiết của hóa đơn này cũng bị xóa theo!", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                hoaDon.deleteHoaDon(currentHD, currentHDCT);
                LoadForm();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //dung thu vien using cExcel = Microsoft.Office.Interop.Excel;
                SaveFileDialog f = new SaveFileDialog();
                //f.FileName = "QlyChungCuNhom9.xlsx";
                f.DefaultExt = "png";
                f.Filter = "(Các file png)|*.png|(Tất cả các tệp)|*.*";
                f.AddExtension = true;
                f.RestoreDirectory = true;

                if (f.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(chartThongKe.Width, chartThongKe.Height);
                chartThongKe.DrawToBitmap(bmp, chartThongKe.ClientRectangle);
                bmp.Save(f.FileName, System.Drawing.Imaging.ImageFormat.Png);
                MessageBox.Show("Lưu hình ảnh thành công!", "Thông báo!");
            }
            catch
            {
                MessageBox.Show("Lưu không thành công vui lòng quay lại sau!", "Thông báo!");
            }
        }
    }
}
