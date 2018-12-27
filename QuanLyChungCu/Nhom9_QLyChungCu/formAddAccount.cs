using Nhom9_QLyChungCu.DAO;
using Nhom9_QLyChungCu.DTO;
using System;
using System.Windows.Forms;

namespace Nhom9_QLyChungCu
{
    public partial class formAddAccount : Form
    {
        AccountDAO ac;
        Account currentAC;
        int type;
        int oldType;
        bool chinhSua = false;
        public formAddAccount(Account acc = null)
        {
            InitializeComponent();
            ac = new AccountDAO();
            if (acc == null)
            {
                this.Text = "Thêm Tài Khoản";
                chinhSua = false;
            }
            else
            {
                this.Text = "Chỉnh Sửa Tài Khoản";
                currentAC = acc;
                chinhSua = true;
                LoadData();
            }

        }

        private void LoadData()
        {
            txbChucVu.Text = currentAC.ChucVu;
            txbMatKhau.Text = currentAC.PassWord;
            txbDisplayName.Text = currentAC.DisPlayName;
            txbNhapLai.Text = currentAC.PassWord;
            txbUserName.Text = currentAC.UserName;
            btnADD.Text = "Chỉnh Sửa";
            lbMain.Text = "Chỉnh sửa tài khoản";
            lbUSer.Text += " (Mặc địch)";
            oldType = currentAC.Type;
            txbUserName.ReadOnly = true;
        }

        #region Sự kiện
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(txbMatKhau.UseSystemPasswordChar == true)
            {
                txbMatKhau.UseSystemPasswordChar = false;
                txbNhapLai.UseSystemPasswordChar = false;
            }
            else
            {
                txbMatKhau.UseSystemPasswordChar = true;
                txbNhapLai.UseSystemPasswordChar = true;
            }
        }
        private void btnADD_Click(object sender, EventArgs e)
        {
            if(txbUserName.Text.Contains(" ") == true)
            {
                MessageBox.Show("User name không được có khoảng trắng!", "Thông báo");
                return;
            }
            if(txbChucVu.Text == "" || txbDisplayName.Text == "" || txbMatKhau.Text == ""
                || txbNhapLai.Text == "" || (string)comboBox1.SelectedItem =="")
            {
                MessageBox.Show("Vui lòng kiểm tra lại dữ liệu nhập vào!", "Thông báo");
                return;
            }
            if(txbMatKhau.Text != txbNhapLai.Text )
            {
                MessageBox.Show("Mật khẩu không khớp với nhau. Kiểm tra lại sau!", "Thông báo");
                return;
            }
           
            if(comboBox1.Text == "Chọn loại tài khoản...")
            {
                MessageBox.Show("Vui lòng chọn loại tài khoản!", "Thông báo");
                return;
            }
            //Kiểm tra phải chỉnh sửa hay ko. //nếu không thì tạo tài khoản
            if(chinhSua ==  false)
            {
                //kiểm tra userName
                if (ac.CheckAcount(txbUserName.Text) == false || txbUserName.Text == "")
                {
                    MessageBox.Show("User name đã có người sử dụng hoặc không đúng!", "Thông báo");
                    return;
                }
                try
                {
                    object[] ob = new object[] { txbUserName.Text, txbDisplayName.Text, txbMatKhau.Text, type, txbChucVu.Text };
                    int i = DataProvider.GetDataFromSQL.ExecuteNonQuery(@"EXEC dbo.USP_InsertAccount @userName , @dpName , @pass , @loai ,  @chuc", ob);
                    if (i == 1)
                    {
                        MessageBox.Show("Đã tạo tài khoản thành công!", "Thông báo");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Tạo tài khoản không thành công!", "Thông báo");
                    }
                }
                catch
                {
                    MessageBox.Show("Không tạo được tài khoản. Vui lòng thử lại sau!", "Thông báo");
                }
            }
            else
            {
                try
                {
                    object[] ob = new object[] { txbUserName.Text, txbDisplayName.Text, txbMatKhau.Text, type, txbChucVu.Text };
                    if(txbUserName.Text == "admin" && type != 1)
                    {
                        MessageBox.Show("Tài khoản bạn là tài khoản admin mặc định của hệ thống! Vui lòng giữ nguyên để có thể truy cập với quyền cao nhất!", "Lỗi chỉnh sửa tài khoản");
                        return;
                    }
                    int i = DataProvider.GetDataFromSQL.ExecuteNonQuery(@"EXEC dbo.USP_UpdateAccount @userName , @dpName , @pass , @loai ,  @chuc", ob);
                    if (i == 1)
                    {
                        MessageBox.Show("Đã chỉnh sửa tài khoản thành công!", "Thông báo");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Chỉnh sửa tài khoản không thành công!", "Thông báo");
                    }
                }
                catch
                {
                    MessageBox.Show("Không chỉnh sửa được tài khoản. Vui lòng thử lại sau!", "Thông báo");
                }
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s = (string)comboBox1.SelectedItem;
            if( s== "Admin")
            {
                type = 1;
                return;
            }
            if (s == "Nhân viên")
            {
                type = 2;
                return;
            }
            if (s == "Khách")
            {
                type = 3;
                return;
            }
        }
        #endregion

        private void btnExit_Click(object sender, EventArgs e)
        {

        }
    }
}
