using Nhom9_QLyChungCu.DAO;
using Nhom9_QLyChungCu.DTO;
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
    public partial class formAddHD : Form
    {
        CanHo currentCH;
        Account CurrentAC;
        HoaDonDAO hd;
        private int donGia = 0;
        private string idDV = "";

        public formAddHD(CanHo ch, Account ac)
        {
            InitializeComponent();
            currentCH = ch;
            CurrentAC = ac;
            hd = new HoaDonDAO();
            loadData();
        }

        private void loadData()
        {
            txbMHD.Text = currentCH.MaHD;
            txbNguoiLap.Text = CurrentAC.UserName;
            txbHDCT.Text = hd.getNextIDHDCT(DateTime.Now, currentCH);
            foreach(DichVu dv in hd.listDichVu)
            {
                if(dv.IDDichVU.Replace(" ","") != "MH001")  cbLoaiDichVu.Items.Add(dv.Name);
            }           
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(dateTimePicker1.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày lập hóa đơn không thể lớn hơn ngày hiện tại!");
                return;
            }
            if(cbLoaiDichVu.Text == "Chọn loại dịch vụ...")
            {
                MessageBox.Show("Không được bỏ trống loại dịch vụ!");
                return;
            }

            string query = " EXEC dbo.USP_InsertHoaDon @idHoaDon ,  @idHDCT ,  @idDV ,  @sl , @donGia ,  @NgayLap , @NguoiLap";
            int i = DataProvider.GetDataFromSQL.ExecuteNonQuery(query, new object[] { txbMHD.Text, txbHDCT.Text, idDV, (int)nmbSL.Value, int.Parse(txbDonGia.Text),  ngayLap() , txbNguoiLap.Text});
            if (i == 0)
            {
                MessageBox.Show("Thêm không thành công! Vui lòng thử lại sau!");
                return;
            }
            if (MessageBox.Show("Thêm hóa đơn thành công. Bạn muốn tiếp tục thêm không?", "Thông báo", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                this.Close();
            }
            return;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            txbHDCT.Text = hd.getNextIDHDCT(dateTimePicker1.Value, currentCH);
        }
        string ngayLap()
        {
            string  s = dateTimePicker1.Value.ToString();
            string[] temp = s.Split(' ');
            temp = temp[0].Split('/');
            s = temp[2] + "-" + String.Format("{0:00}", temp[0]) + "-" + String.Format("{0:00}", temp[1]); 
            return s;
        }
        private void cbLoaiDichVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string name =  cbLoaiDichVu.SelectedItem.ToString();
            foreach(DichVu dv in hd.listDichVu)
            {
                if(dv.Name == name)
                {
                    txbDonGia.Text = dv.DonGia.ToString();
                    txbTT.Text = (dv.DonGia * (int)nmbSL.Value).ToString();
                    donGia = dv.DonGia;
                    txbDonVi.Text = dv.DonVi;
                    idDV = dv.IDDichVU;
                }
            }
        }

        private void nmbSL_ValueChanged(object sender, EventArgs e)
        {
            txbTT.Text = (donGia * (int)nmbSL.Value).ToString();
        }
    }
}
