using Nhom9_QLyChungCu.DAO;
using Nhom9_QLyChungCu.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Nhom9_QLyChungCu
{
    public partial class formMain : Form
    {
        Account currentAC;
        private List<Label> lb = new List<Label>();
        private CanHoDAO canHo;
        private CanHo currentCH;
        private MessageDAO tinNhan;
        string IDKhSelected;
        string TenKHSelected;
        int tncd; //tin nhan chua doc

        public formMain(Account ac)
        {
            InitializeComponent();          
            currentAC = ac;
            canHo = new CanHoDAO();//gọi hàm dựng, vừa load data   
            tinNhan = new MessageDAO();
            foreach (Control ctl in panel1.Controls)
            {
                lb.Add(ctl as Label);
            }
            loadData();
        }

        private void loadData()
        {
            //load tn va loai tai khoan
            LoadTN();

            //vẽ sơ đồ chung cư
            loadSoDoCanHo();
            
            //load accout
            loadAccout(currentAC);

            pictureBox1.Image = Properties.Resources.support;

        }

        private void LoadTN()
        {
            tncd = tinNhan.CountYeuCau(currentAC);
            lbTN.Text = "Bạn có " + tncd.ToString() +  " tin nhắn chưa đọc";
        }

        private void loadSoDoCanHo()
        {           
            try
            {
                int index = 0;
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        Button btn = new Button() { Width = 57, Height = 61, Location = new Point(15 + j * 79, 32 + i * 138) };
                        btn.BackgroundImageLayout = ImageLayout.Stretch;
                        btn.Tag = index;
                        if (canHo.listCanHo[index].Stt == "Trống")
                        {
                            btn.BackgroundImage = Properties.Resources.trong;
                        }
                        else if (canHo.listCanHo[index].Stt == "Đã được bán")
                        {
                            btn.BackgroundImage = Properties.Resources.daconguoi;
                        }
                        else
                        {
                            btn.BackgroundImage = Properties.Resources.danggiaodich;
                        }
                        btn.Click += Btn_Click;

                        //btn.Text = canHo.listCanHo[index].SoPhong;
                        panel1.Controls.Add(btn);
                        index++;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Dữ liệu bị hỏng. Vui lòng trở lại sau!");
            }
        }

        private void loadAccout(Account AC)
        {
            txbChucVu.Text = AC.ChucVu;
            switch (AC.Type)
            {
                case 1:
                    txbLoaiTK.Text = "Admin";
                    break;
                case 2:
                    txbLoaiTK.Text = "Nhân viên";
                    break;
                default:
                    txbLoaiTK.Text = "Tài khoản khách";
                    break;
            }
           // txbMatKhau.Text = AC.PassWord;
            txbTenHienThi.Text = AC.DisPlayName;
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            int index = (int)bt.Tag;
            CanHo ch = canHo.listCanHo[index];
            ShowCanHo(ch);
            currentCH = ch;
            switch(ch.Stt)
            {
                case "Trống":
                    ptbSTT.BackgroundImage = Properties.Resources.trong;
                    lbstt.Text = "Còn trống";
                    break;
                case "Đã được bán":
                    ptbSTT.BackgroundImage = Properties.Resources.daconguoi;
                    lbstt.Text = "Đã được bán";
                    break;
                default:
                    ptbSTT.BackgroundImage = Properties.Resources.danggiaodich;
                    lbstt.Text = "Đang giao dịch";
                    break;
            }
            TenKHSelected = ch.ChuSoHuu;
            IDKhSelected = ch.IdKH;
            if (TenKHSelected != "" && IDKhSelected != "") btnMore.Enabled = true;
            else btnMore.Enabled = false;
        }

        private void ShowCanHo(CanHo ch)
        {
            txbstt.Text = ch.Stt;
            txbSoPhong.Text = ch.SoPhong;
            txbGiaBan.Text = ch.GiaTien.ToString();
            txbchuSoHuu.Text = ch.ChuSoHuu;
            txbtinhTrang.Text = ch.TinhTrang;
            txbMaHD.Text = ch.MaHD;
        }


        private void formMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát chương trình hay không?", "Thông báo", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void button25_Click(object sender, EventArgs e)//button more
        {
            if (currentAC == null || currentAC.Type != 1)
            {
                if (MessageBox.Show("Bạn không được phép truy cập. Vui lòng liên hệ admin!" + Environment.NewLine + "Bạn có muốn gửi yêu cầu truy cập lên admin ko?", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    formSendMessage z = new formSendMessage(currentAC.Type);
                    z.ShowDialog();
                    return;
                }
                return;
            }
            //this.Hide();
            formTTChuCanHo t = new formTTChuCanHo(IDKhSelected, TenKHSelected);
            t.ShowDialog();
            //this.Show();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            formSendMessage t = new formSendMessage(0, currentAC.UserName);
            t.ShowDialog();
        }

        private void button32_Click(object sender, EventArgs e)
        {
            formUserProfile t = new formUserProfile(currentAC);
            t.UpdateAccount += T_UpdateAccount;
            t.ShowDialog();               
        }

        private void T_UpdateAccount(object sender, AccountEvent e)
        {
            currentAC = e.Acc;
            loadAccout(e.Acc);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (currentAC == null || currentAC.Type != 1)
            {
                if (MessageBox.Show("Bạn không được phép truy cập. Vui lòng liên hệ admin!" + Environment.NewLine + "Bạn có muốn gửi yêu cầu truy cập lên admin ko?", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    formSendMessage z = new formSendMessage(currentAC.Type);
                    z.ShowDialog();
                    return;
                }
                return;
            }
            if (currentCH == null)
            {
                MessageBox.Show("Vui lòng chọn một căn hộ!", "Không thể thực hiện giao dịch");
                return;
            }
            if (currentCH.ChuSoHuu != null && currentCH.ChuSoHuu != "")
            {
                MessageBox.Show("Căn hộ đã có tên chủ sở hữu. vui lòng chọn update nên muốn chỉnh sửa!", "Không thể thực hiện giao dịch");
                return;
            }
            else
            {
                formSellCanHo t = new formSellCanHo(currentCH, 0);
                t.CanHochange += T_CanHochange;
                t.ShowDialog();
            }

        }

        private void T_CanHochange(object sender, CanHoChangeEvent e)
        {
            ReloadSoDo();
        }
        private void ReloadSoDo()
        {
            panel1.Controls.Clear();
            canHo.reloadData();
            foreach (Label item in lb)
            {
                panel1.Controls.Add(item);
            }
            loadSoDoCanHo();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (currentAC == null || currentAC.Type != 1)
            {
                if (MessageBox.Show("Bạn không được phép truy cập. Vui lòng liên hệ admin!" + Environment.NewLine + "Bạn có muốn gửi yêu cầu truy cập lên admin ko?", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    formSendMessage z = new formSendMessage(currentAC.Type);
                    z.ShowDialog();
                    return;
                }
                return;
            }
            if (currentCH == null)
            {
                MessageBox.Show("Vui lòng chọn một căn hộ!", "Thông báo");
                return;
            }
            if (currentCH.ChuSoHuu == null || currentCH.ChuSoHuu == "")
            {
                this.Enabled = false;
                formSellCanHo editCanHo = new formSellCanHo(currentCH, -1);
                editCanHo.CanHochange += EditCanHo_CanHochange;
                editCanHo.ShowDialog();
                this.Enabled = true;
                return;
            }
            this.Enabled = false;
            formSellCanHo editBoth = new formSellCanHo(currentCH, 1);
            editBoth.CanHochange += EditBoth_CanHochange;
            editBoth.ShowDialog();
            this.Enabled = true;
        }

        private void EditBoth_CanHochange(object sender, CanHoChangeEvent e)
        {
            ReloadSoDo();
        }

        private void EditCanHo_CanHochange(object sender, CanHoChangeEvent e) //event
        {
            ReloadSoDo();
        }

        private void btnXemHoaDon_Click(object sender, EventArgs e)
        {
            if (currentAC == null || currentAC.Type != 1)
            {
                if (MessageBox.Show("Bạn không được phép truy cập. Vui lòng liên hệ admin!" + Environment.NewLine + "Bạn có muốn gửi yêu cầu truy cập lên admin ko?", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    formSendMessage z = new formSendMessage(currentAC.Type);
                    z.ShowDialog();
                    return;
                }
                return;
            }
            if (currentCH == null)
            {
                MessageBox.Show("Vui lòng chọn một căn hộ!", "Thông báo");
                return;
            }
            if (currentCH.ChuSoHuu == null || currentCH.ChuSoHuu == "")
            {
                MessageBox.Show("Căn hộ chưa được sử dụng. vui lòng quay lại sau!", "Không thể thực hiện giao dịch");
                return;
            }
            formViewHD t = new formViewHD(currentCH.MaHD);
            t.ShowDialog();
        }
        private void btnThemHD_Click(object sender, EventArgs e)
        {
            if (currentAC == null || currentAC.Type > 2)
            {
                if (MessageBox.Show("Bạn không được phép truy cập. Vui lòng liên hệ admin!" + Environment.NewLine + "Bạn có muốn gửi yêu cầu truy cập lên admin ko?", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    formSendMessage z = new formSendMessage(currentAC.Type);
                    z.ShowDialog();
                    return;
                }
                return;
            }
            if (currentCH == null)
            {
                MessageBox.Show("Vui lòng chọn một căn hộ!", "Không thể thực hiện giao dịch");
                return;
            }
            if (currentCH.ChuSoHuu == null || currentCH.ChuSoHuu == "")
            {
                MessageBox.Show("Căn hộ chưa được sử dụng. vui lòng quay lại sau!", "Không thể thực hiện giao dịch");
                return;
            }
            formAddHD th = new formAddHD(currentCH, currentAC);
            th.ShowDialog();
        }

        private void đăngXuấtToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lbTN_Click(object sender, EventArgs e)
        {
            if (tncd == 0) return;
            formReadMessage r = new formReadMessage(currentAC);
            r.ShowDialog();
        }

        private void tinNhắnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formReadMessage r = new formReadMessage(currentAC);
            r.ShowDialog();
        }

        private void chỉnhSửaThôngTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formUserProfile z = new formUserProfile(currentAC);
            z.UpdateAccount += Z_UpdateAccount;
            z.ShowDialog();
        }

        private void Z_UpdateAccount(object sender, AccountEvent e)
        {
            currentAC = e.Acc;
            loadAccout(e.Acc);
        }

        private void thốngKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(currentAC == null || currentAC.Type != 1)
            {
                if (MessageBox.Show("Bạn không được phép truy cập. Vui lòng liên hệ admin!" + Environment.NewLine + "Bạn có muốn gửi yêu cầu truy cập lên admin ko?", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    formSendMessage z = new formSendMessage(currentAC.Type);
                    z.ShowDialog();
                    return;
                }
                return;
            }
            formThongKe f = new formThongKe();
            f.ShowDialog();
        }

        private void gửiYêuCầuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentAC == null || currentAC.Type == 1)
            {
                MessageBox.Show("Bạn đang dùng tài khoản admin nên không cần dùng chức năng này!", "Thông báo");
                return;
            }
            formSendMessage f = new formSendMessage(currentAC.Type);
            f.ShowDialog();
        }

        private void côngCụToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentAC == null || currentAC.Type != 1)
            {
                if (MessageBox.Show("Bạn không được phép truy cập. Vui lòng liên hệ admin!" + Environment.NewLine + "Bạn có muốn gửi yêu cầu truy cập lên admin ko?", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    formSendMessage z = new formSendMessage(currentAC.Type);
                    z.ShowDialog();
                    return;
                }
                return;
            }
            //xu ly o day
            formAdminProfile ad = new formAdminProfile();
            ad.ShowDialog();

        }

        private void kiểmTraCậpNhâtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bạn đang dùng phần mềm quản lý chung cư của nhóm 7 verson 1.0", "Thông báo");
        }

        private void aboutMyTeamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Nhóm 7 (lớp 1):" + Environment.NewLine
                            + "Nguyễn Văn Khánh (1512246): Nhóm trưởng." + Environment.NewLine 
                            + "Đặng Phương Nam (1512330)" + Environment.NewLine
                            + "Nguyễn Xuân Kiệt (1512271)" + Environment.NewLine
                            + "Nguyễn Bá Kỳ (1512274)" + Environment.NewLine
                            + "Nguyễn Trương Lê Văn (1512661)" + Environment.NewLine, "Thông báo");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
