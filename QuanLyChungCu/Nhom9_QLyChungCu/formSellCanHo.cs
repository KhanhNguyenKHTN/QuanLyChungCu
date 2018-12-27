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
    public partial class formSellCanHo : Form
    {
        private CanHo currentCH;
        KhachHangDAO listKH;
        string idKH;
        int type;

        public formSellCanHo(CanHo CH, int para)
        {
            InitializeComponent();
            type = para;
            listKH = new KhachHangDAO();
            currentCH = CH;

            if (para == -1)
            {
                this.Text = "Chỉnh sửa căn hộ";
                panel2.Enabled = false;
                loadCanHo();
            }
            else if(para == 0)
            {
                this.Text = "Bán căn hộ";
                idKH = GetNextIDKhachHang();
                loadData();
            }
            else if(para == 1)
            {
                this.Text = "Chỉnh sửa căn hộ";
                loadCanHo();
                loadKhachHang();
            }                                
        }

        private void loadCanHo()
        {
            //enable
            txbchuSoHuu.Text = currentCH.ChuSoHuu;
            txbGiaBan.Text = currentCH.GiaTien.ToString();
            cbTinhTrang.Text = currentCH.TinhTrang;
            //disable
            txbIDKH.Text = currentCH.IdKH;
            txbMaHD.Text = currentCH.MaHD;
            txbSoPhong.Text = currentCH.SoPhong;
            if (type == -1)
            {
                cbSTT.Items.Add("Trống");
                cbSTT.Text = "Trống";
                cbSTT.Enabled = false;
                txbchuSoHuu.Enabled = false;              
            }
        }
        private void loadKhachHang()
        {
            KhachHang temp = listKH.getKHByID(currentCH.IdKH);
            txbKHcmnd.Text = temp.Cmnd;
            txbKHid.Text = temp.ID;
            txbKHsoPhong.Text = temp.IDCanHo;
            txbKHTen.Text = temp.Name;
            cbGT.Text = temp.GioiTinh;
            cbTTHonNhan.Text = temp.TrangThai;
            string[] cut1 = temp.NgayMuaNha.Split(' ');
            string[] cut2 = cut1[0].Split('/');         
            DateTime dt = new DateTime(int.Parse(cut2[2]), int.Parse(cut2[0]), int.Parse(cut2[1]));
            dateTimePicker1.Value = dt;
        }

        private void loadData()
        {
            txbKHid.Text = GetNextIDKhachHang();
            txbIDKH.Text = txbKHid.Text;
            txbSoPhong.Text = currentCH.SoPhong;
            txbMaHD.Text = currentCH.MaHD;
            txbKHsoPhong.Text = currentCH.SoPhong;
            txbGiaBan.Text = currentCH.GiaTien.ToString();
        }

        private string GetNextIDKhachHang()//id tu tang
        {
            string id = "KH";
            if (listKH.count + 1 < 10)
            {
                id += "00" + (listKH.count + 1).ToString();
                return id;
            }
            else if (listKH.count + 1 < 100)
            {
                id += "0" + (listKH.count + 1).ToString();
                return id;
            }
            else
            {
                id += (listKH.count + 1).ToString();
                return id;
            }
        }
        private void btnADD_Click(object sender, EventArgs e)
        {
            //xử lý trạng thái
            if(cbSTT.Text == "Trống" && cbTinhTrang.Text == "Đang được sử dụng")
            {
                MessageBox.Show("Căn hộ không được sử dụng trong trạng thái trống", "Thông báo");
                return;
            }
            if ((txbKHcmnd.Text == "" && panel2.Enabled == true) || txbGiaBan.Text == "" || (txbKHcmnd.Text.Length > 10 && panel2.Enabled == true))
            {
                MessageBox.Show("Thông tin bị lỗi! Vui lòng kiểm tra lại thông tin", "Thông báo");
                return;
            }
            if (type == -1)
            {
                if(UpdateCanHo()== false)
                {
                    MessageBox.Show("Update thất bại! Vui lòng quay lại sau!", "Thông báo");;
                }
                this.Close();
                return;
            }
            //xứ lý tên không trùng
            if (txbchuSoHuu.Text != txbKHTen.Text || txbKHTen.Text == "")
            {
                MessageBox.Show("Tên khách hàng không trùng khớp. Vui lòng kiểm tra lại", "Thông báo");
                return;
            }
            //xử lý trạng thái
            if ((cbSTT.Text == "Đang giao dịch" && cbTinhTrang.Text == "Đang được sử dụng") || (cbSTT.Text == "Trống" && cbTinhTrang.Text == "Đang được sử dụng"))
            {
                MessageBox.Show("Căn hộ không thể được sử dụng trong khi đang giao dịch", "Thông báo");
                return;
            }
          
            //xử lý ngày tháng            
            if (dateTimePicker1.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày giao dịch không được lớn hơn thời điểm hiện tại!", "Thông báo");
                return;
            }
            
            if(type == 0)
            {
                ThucHienBan(); 
            }
            else
            {
                upThongTin();
            }
            this.Close();          
        }


#region type = 1
        private void upThongTin()
        {
            if (editInfo() == false)
            {
                MessageBox.Show("Chỉnh sửa không thành công! Vui lòng quay lại sau!", "Thông báo");
                this.Close();
                return;
            }
            if (UpdateCanHo() == false)
            {
                MessageBox.Show("Chỉnh sửa không thành công! Vui lòng quay lại sau!", "Thông báo");
                this.Close();
                return;
            }
            MessageBox.Show("Chỉnh sửa thông tin thành công! Vui lòng quay lại sau!", "Thông báo");           
            return;

        }

        private bool editInfo()
        {
            try
            {
                string NgayMN = dateTimePicker1.Value.Year.ToString() + "-" + dateTimePicker1.Value.Month.ToString() + "-" + dateTimePicker1.Value.Day.ToString();
                string query = DataProvider.GetDataFromSQL.QueryWithParameter(@"EXEC dbo.USP_updateChuCanHo @id , @NgayMuaNha , @CMND , @gioiTinh , @ten , @tt", new object[] { txbKHid.Text, NgayMN, txbKHcmnd.Text, cbGT.Text, txbKHTen.Text, cbTTHonNhan.Text });
                int i = DataProvider.GetDataFromSQL.ExecuteNonQuery(query);
                if (i == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
#endregion
#region type = 0
        private void ThucHienBan()
        {
            if (sellThisFlat() == false)
            {
                MessageBox.Show("Giao dịch không thành công! Vui lòng quay lại sau!", "Thông báo");
                this.Close();
                return;
            }
            MessageBox.Show("Giao dịch thành công!", "Thông báo");
            return;
        }

        private bool sellThisFlat()
        {
            if (UpdateCanHo() == false) return false;
            if (InsertKH() == false) return false;
            return true;
        }

        private bool InsertKH()
        {
            try
            {
                string NgayMN = dateTimePicker1.Value.Year.ToString() + "-" + dateTimePicker1.Value.Month.ToString() + "-" + dateTimePicker1.Value.Day.ToString();
                string query = DataProvider.GetDataFromSQL.QueryWithParameter(@"EXEC dbo.USP_InsertKH @ID , @NgayMuaNha , @IDCanHo , @CMND , @name , @GioiTinh , @TrangThai", new object[] { idKH, NgayMN, txbSoPhong.Text, txbKHcmnd.Text, txbKHTen.Text, cbGT.Text, cbTTHonNhan.Text });
                int i = DataProvider.GetDataFromSQL.ExecuteNonQuery(query);
                if (i == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
#endregion
#region type = -1 && type = 0
        private bool UpdateCanHo()
        {
            try
            {
                string chuSoHuu = txbchuSoHuu.Text;
                string IDKH = txbKHid.Text;
                string GiaTien = txbGiaBan.Text;
                string TinhTrang = cbTinhTrang.Text;
                string stt = cbSTT.Text;
                string IDPhong = currentCH.SoPhong;
                string query = @"UPDATE dbo.CanHo SET ChuSoHuu = N'" + chuSoHuu + "', IDKH = N'" + IDKH + "', giaTien = " + GiaTien + ", tinhTrang = N'" + TinhTrang + "', stt = N'" + stt + "' WHERE SoPhong = N'" + IDPhong + "'";
                int i = DataProvider.GetDataFromSQL.ExecuteNonQuery(query);
                if (i == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

        }
#endregion

        #region event
        private event EventHandler<CanHoChangeEvent> canHochange;        
        public event EventHandler<CanHoChangeEvent> CanHochange
        {
            add { canHochange += value; }
            remove { canHochange -= value; }
        }

        #endregion

        private void formSellCanHo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (canHochange != null)
            {
                canHochange(this, new CanHoChangeEvent(currentCH));
            }
        }
    }
    public class CanHoChangeEvent : EventArgs
    {
        private CanHo ch;

        public CanHo Ch { get => ch; set => ch = value; }

        public CanHoChangeEvent(CanHo canHo)
        {
            this.Ch = canHo;
        }
    }
}
