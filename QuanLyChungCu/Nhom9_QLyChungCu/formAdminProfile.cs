using Nhom9_QLyChungCu.DAO;
using Nhom9_QLyChungCu.DTO;
using System;
using System.Windows.Forms;

namespace Nhom9_QLyChungCu
{
    public partial class formAdminProfile : Form
    {
        AccountDAO AllAccount;
        HoaDonDAO hoaDon;
        string currentUser;
        string currentDichVu;

        public formAdminProfile()
        {
            InitializeComponent();           
            btnSetting.Enabled = false;
            btnDelete.Enabled = false;
            loadAccount();
            LoadDichVu();
        }
        private void loadAccount()
        {
            AllAccount = new AccountDAO();
            dataGridView1.DataSource = AllAccount.AccData();
        }
        private void LoadDichVu()
        {
            hoaDon = new HoaDonDAO();
            dataGridView2.DataSource = hoaDon.GetListDichVu();
        }
#region account

        private void button3_Click(object sender, EventArgs e)//chinh sua
        {
            formAddAccount f = new formAddAccount(AllAccount.GetAccount(currentUser));
            f.ShowDialog();
            loadAccount();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            formAddAccount f = new formAddAccount();
            f.ShowDialog();
            loadAccount();
        }

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;
            int index = dataGridView1.CurrentRow.Index;
            DataGridViewRow row = dataGridView1.Rows[index];
            DataGridViewCellCollection cel = row.Cells;
            currentUser = cel[0].Value.ToString();
            if (currentUser == "")
            {
                btnDelete.Enabled = false;
                btnSetting.Enabled = false;
            }
            else
            {
                btnSetting.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //int index = dgvBang.CurrentRow.Index;
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                DataGridViewCellCollection cel = row.Cells;
                currentUser = cel[0].Value.ToString();
                // currentHD = cel[1].Value.ToString();
                if (currentUser == "")
                {
                    btnDelete.Enabled = false;
                    btnSetting.Enabled = false;
                }
                else
                {
                    btnSetting.Enabled = true;
                    btnDelete.Enabled = true;
                }
            }
            catch { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có xóa tài khoản này hay không?", "Thông báo", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            if (currentUser == "admin")
            {
                MessageBox.Show("Bạn không được xóa tài khoản này! đây là tài khoản mặc định", "Thông báo!");
                return;
            }
            //
            //Xóa luôn hóa đơn đc lập bởi tk này
            //
            if (MessageBox.Show("Bạn có muốn xóa tất cả hóa đơn do tài khoản này lập không?" + Environment.NewLine
               + "Lưu ý: Mọi hóa đơn có liên quan tới tài khoản này này cũng sẽ bị xóa bỏ.", "Thông báo!", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {

                try
                {
                    HoaDonDAO hd = new HoaDonDAO();
                    for (int i = 0; i < hd.listHoadDon.Count; i++)
                    {
                        if(hd.listHoadDon[i].NguoiLap.Replace(" ","") == currentUser)
                        {
                            hd.deleteHoaDon(hd.listHoadDon[i].IDHoaDon, hd.listHoadDon[i].IDHoaDonChiTiet);                           
                        }                               
                    }
                }
                catch
                {
                    MessageBox.Show("Xóa không thành công vui lòng thử lại sau!", "Thông báo");
                 
                }
            }
            
            string query = @"DELETE dbo.Account WHERE userName = N'"+ currentUser +"'";
            try
            {
                int i = DataProvider.GetDataFromSQL.ExecuteNonQuery(query);
                if (i == 1)
                {
                    MessageBox.Show("Đã xóa tài khoản thành công!", "Thông báo!");
                    loadAccount();
                }
                else
                {
                    MessageBox.Show("Xóa không thành công!", "Thông báo!");
                }
            }
            catch
            {
                MessageBox.Show("Xóa không thành công!", "Thông báo!");
            }
          
        }
#endregion

#region Dich Vụ
        private void btnExit2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXoa2_Click(object sender, EventArgs e)
        {
            int counHDCT = 0;
            int countDV = 0;
            if (MessageBox.Show("Bạn có muốn xóa dịch vụ này không?" + Environment.NewLine
                + "Lưu ý: Mọi hóa đơn có liên quan tới dịch vụ này cũng sẽ bị xóa bỏ.", "Thông báo!", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                
                try
                {
                    HoaDonDAO hd = new HoaDonDAO();
                    for(int i = 0; i <hd.listHoadDon.Count; i++)
                    {
                        foreach (HoaDonChiTiet item in hd.listHoadDon[i].ListHDCT)
                        {
                            if(item.IdDichVu == currentDichVu)
                            {
                                object[] ob = new object[] { hd.listHoadDon[i].IDHoaDon.Replace(" ", ""), hd.listHoadDon[i].IDHoaDonChiTiet, currentDichVu, item.ThanhTien };
                                counHDCT += DataProvider.GetDataFromSQL.ExecuteNonQuery(@"EXEC dbo.USP_DeleteHDCT @hd , @idHDCT , @madv , @gia", ob);
                            }
                            
                        }
                    }
                 
                }
                catch
                {
                    MessageBox.Show("Xóa không thành công vui lòng thử lại sau!", "Thông báo");
                    return;
                }

                string query = @"DELETE dbo.DichVu WHERE IDDichVu = N'" + currentDichVu + "'";
                try
                {
                    countDV = DataProvider.GetDataFromSQL.ExecuteNonQuery(query);
                    if (countDV == 0)
                    {
                        MessageBox.Show("Xóa không thành công vui lòng thử lại sau!", "Thông báo");
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("Xóa không thành công vui lòng thử lại sau!", "Thông báo");
                    return;
                }
                LoadDichVu();
                MessageBox.Show("Xóa thành công!" + Environment.NewLine + "Số hóa đơn chi tiết đã xóa: "
                    + counHDCT.ToString() + Environment.NewLine + "Số dịch vụ đã xóa: " + countDV.ToString(), "Thông báo");

            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //int index = dgvBang.CurrentRow.Index;
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
                DataGridViewCellCollection cel = row.Cells;
                currentDichVu = cel[0].Value.ToString();
                // currentHD = cel[1].Value.ToString();
                if (currentDichVu == "")
                {
                    btnXoa2.Enabled = false;
                    btnSua2.Enabled = false;
                }
                else
                {
                    btnXoa2.Enabled = true;
                    btnSua2.Enabled = true;
                }
            }
            catch { }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0) return;
            int index = dataGridView2.CurrentRow.Index;
            DataGridViewRow row = dataGridView2.Rows[index];
            DataGridViewCellCollection cel = row.Cells;
            currentDichVu = cel[0].Value.ToString();
            if (currentDichVu == "")
            {
                btnXoa2.Enabled = false;
                btnSua2.Enabled = false;
            }
            else
            {
                btnXoa2.Enabled = true;
                btnSua2.Enabled = true;
            }
        }
        private void btnSua2_Click(object sender, EventArgs e)
        {
            formAddDichVu f = new formAddDichVu(hoaDon.getNextMaDV(), hoaDon.getDichVuByID(currentDichVu));
            f.ShowDialog();
            LoadDichVu();
        }

        private void btnThem2_Click(object sender, EventArgs e)
        {
            formAddDichVu f = new formAddDichVu(hoaDon.getNextMaDV());
            f.ShowDialog();
            LoadDichVu();
        }
        #endregion


    }
}
