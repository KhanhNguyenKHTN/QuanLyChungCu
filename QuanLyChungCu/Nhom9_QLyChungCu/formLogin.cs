using Nhom9_QLyChungCu.DAO;
using System;
using System.IO;
using System.Windows.Forms;

namespace Nhom9_QLyChungCu
{
    public partial class formLogin : Form
    {
        AccountDAO login;
        public formLogin()
        {
            InitializeComponent();
            LoadData();
        }


        private void LoadData()
        {
            try
            {
                using (StreamReader sr = new StreamReader(Application.StartupPath + @"\config.inf"))
                {
                    DataProvider.GetDataFromSQL.ConnectionSTR = sr.ReadLine();
                }
                if (DataProvider.GetDataFromSQL.ConnectionSTR == "")
                {
                    MessageBox.Show("Cơ sở dữ liệu chưa được kết nối! Vui lòng kết nối", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                login = new AccountDAO();
            }
            catch
            {
                MessageBox.Show("Tải dữ liệu không thành công hoặc kết nối SQL không thành công!"
                                + Environment.NewLine + "Vui lòng kiểm tra kết nối hoặc tạo mới kết nối!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void formLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("Bạn có muốn thoát chương trình hay không?","Thông báo", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (login.Login(txbUserName.Text, txbPassWord.Text) == false)
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng! Vui lòng thử lại", "Thông báo");
                    return;
                }
                this.Hide();
                formMain t = new formMain(login.AC);
                t.ShowDialog();
                this.Show();
                login = new AccountDAO();
            }
            catch
            {
                 MessageBox.Show("Lỗi đăng nhập! Cơ sở dữ liệu chưa được kết nối! Vui lòng kết nối", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (txbPassWord.UseSystemPasswordChar == false) txbPassWord.UseSystemPasswordChar = true;
            else txbPassWord.UseSystemPasswordChar = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            formSendMessage t = new formSendMessage(0);
            t.Show();
            this.Show();
        }

        private void btnData_Click(object sender, EventArgs e)
        {
            formKetNoiSQL f = new formKetNoiSQL();
            f.ShowDialog();
            LoadData();
            MessageBox.Show("Đã reload Data! Sẵn sàng kết nối!");

        }
    }
}
