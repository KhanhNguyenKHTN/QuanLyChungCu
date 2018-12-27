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

namespace Nhom9_QLyChungCu
{
    public partial class formSendMessage : Form
    {
        public formSendMessage(int type, string name = null)
        {
            InitializeComponent();
            if (type == 1 && name != null)
            {
                txbUser.Text = "admin";
                txbUser.Enabled = false;
                txbTo.Text = name;
                txbTo.Enabled = false;
            }
            else if (type == 0 && name != null)
            {
                txbUser.Text = name;
                txbUser.Enabled = false;
                txbTo.Text = "admin";
                txbTo.Enabled = false;
            }
            else
            {
                txbTo.Text = "admin";
                txbTo.Enabled = false;
            }

 
        }

        private void btnGui_Click(object sender, EventArgs e)
        {
            if(txbContent.Text.Length > 1000 || txbContent.Text == "" || txbUser.Text == "" || txbTo.Text == "")
            {
                MessageBox.Show("Nội dung tin nhắn không hợp lệ!", "Thông báo");
                return;
            }

           // @guiTu  @guiDen   @mes
            int temp = DataProvider.GetDataFromSQL.ExecuteNonQuery(@"EXEC dbo.USP_SendMessage @guiTu , @guiDen , @mes", new object[] { txbUser.Text, txbTo.Text, txbContent.Text }); //1 là tài khoản admin
            if(temp == 1)
            {
                MessageBox.Show("Tin nhắn của bạn đã được gửi đi. Vui lòng chờ hồi đáp từ admin!", "Thông báo");
                txbContent.Text = "";
                txbUser.Text = "";
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
