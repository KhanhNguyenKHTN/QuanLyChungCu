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
    public partial class formUserProfile : Form
    {
        Account currentAccout;


        public formUserProfile(Account ac)
        {
            InitializeComponent();
            currentAccout = ac;
            LoadData();
        }

        private void LoadData()
        {
            txbUserName.Text = currentAccout.UserName;
            txbDisplayName.Text = currentAccout.DisPlayName;
            txbMatKhau.Text = currentAccout.PassWord;
            txbUserName.Enabled = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(txbMatKhau.Text == "" || txbMatKhau.Text != currentAccout.PassWord || txbMKmoi.Text != txbNhapLai.Text || txbMKmoi.Text =="")
            {
                MessageBox.Show("Cập nhật không thành công! Vui lòng kiểm tra lại!", "Thông báo!");
                return;
            }
            string query = @"UPDATE dbo.Account SET passWord = N'"+ txbMKmoi.Text + @"', displayName = N'"+ txbDisplayName.Text+@"' WHERE userName = N'"+currentAccout.UserName+@"';";
            int i = DataProvider.GetDataFromSQL.ExecuteNonQuery(query);
            if(i == 1)
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo!");
                currentAccout.PassWord = txbMKmoi.Text;
                currentAccout.DisPlayName = txbDisplayName.Text;
                if(updateAccount != null)
                {
                    updateAccount(this, new AccountEvent(currentAccout));
                }
                txbMatKhau.Text = txbMKmoi.Text;
                txbMKmoi.Text = "";
                txbNhapLai.Text = "";
            }
            else
            {
                MessageBox.Show("Cập nhật không thành công! Vui lòng kiểm tra lại!", "Thông báo!");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (txbMatKhau.UseSystemPasswordChar == false)
            {
                txbMatKhau.UseSystemPasswordChar = true;
                txbMKmoi.UseSystemPasswordChar = true;
                txbNhapLai.UseSystemPasswordChar = true;
            }
            else
            {
                txbMatKhau.UseSystemPasswordChar = false;
                txbMKmoi.UseSystemPasswordChar = false;
                txbNhapLai.UseSystemPasswordChar = false;
            }
        }
        #region truyen du lieu ve form chinh thong qua event
        private event EventHandler<AccountEvent> updateAccount;
        public event EventHandler<AccountEvent> UpdateAccount
        {
            add { updateAccount += value; }
            remove { updateAccount -= value; }
        }

        #endregion
    }
    public class AccountEvent : EventArgs
    {
        private Account acc;

        public Account Acc
        {
            get { return acc; }
            set { acc = value; }
        }

        public AccountEvent(Account acc)
        {
            this.Acc = acc;
        }
    }
}
