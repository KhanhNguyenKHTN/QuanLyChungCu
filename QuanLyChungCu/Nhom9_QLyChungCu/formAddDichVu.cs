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
    public partial class formAddDichVu : Form
    {
        DichVu currentDV;
        int type;

        public formAddDichVu(string NextDV, DichVu dv = null)
        {
            InitializeComponent();        
            if (dv == null)
            {
                this.Text = "Thêm dịch vụ";
                lbMain.Text = "Thêm dịch vụ";
                txbMDV.Text = NextDV;
                btnThem.Text = "Thêm";
                type = 0;
            }
            else
            {
                this.Text = "Cập nhật dịch vụ";
                lbMain.Text = "Cập nhật dịch vụ";
                currentDV = dv;
                txbMDV.Text = dv.IDDichVU;
                txbGia.Text = dv.DonGia.ToString();
                txbDV.Text = dv.DonVi;
                txbTen.Text = dv.Name;
                btnThem.Text = "Sửa";
                type = 1;
            }

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if(txbDV.Text == "" || txbTen.Text == "" || txbGia.Text == "")
            {
                MessageBox.Show("Dữ liệu nhập vào không đúng! Vui lòng kiểm tra lại.", "Thông báo");
                return;
            }
            int n = 3;
            if(int.TryParse(txbGia.Text,out n) == false)
            {
                if (n < 1000)
                {
                    MessageBox.Show("Giá phải là các con số!", "Thông báo");
                    return;
                }
                MessageBox.Show("Giá phải là các con số!", "Thông báo");
                return;
           }
            //thêm
            object[] ob = new object[] { txbMDV.Text, txbTen.Text, int.Parse(txbGia.Text), txbDV.Text };
            if (type == 0)
            {
                try
                {
                   int t =  DataProvider.GetDataFromSQL.ExecuteNonQuery(@"EXEC dbo.USP_InsertDV @id , @name , @gia , @dv", ob);
                    if(t==0)
                    {
                        MessageBox.Show("Thêm không thành công! Vui lòng thử lại sau.", "Thông báo");
                        return;
                    }
                    MessageBox.Show("Thêm thành công!", "Thông báo");
                    this.Close();
                    return;
                }
                catch
                {
                    MessageBox.Show("Thêm không thành công! Vui lòng thử lại sau.", "Thông báo");
                    return;
                }
            }
            else //sửa
            {
                try
                {
                    int t = DataProvider.GetDataFromSQL.ExecuteNonQuery(@"EXEC dbo.USP_UpdateDV @id , @name , @gia , @dv", ob);
                    if (t == 0)
                    {
                        MessageBox.Show("Sửa không thành công! Vui lòng thử lại sau.", "Thông báo");
                        return;
                    }
                    MessageBox.Show("Sửa thành công!", "Thông báo");
                    this.Close();
                    return;
                }
                catch
                {
                    MessageBox.Show("Sửa không thành công! Vui lòng thử lại sau.", "Thông báo");
                    return;
                }
            }
        }
    }
}
