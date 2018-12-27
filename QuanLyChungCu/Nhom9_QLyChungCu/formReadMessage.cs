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
    public partial class formReadMessage : Form
    {
        Account currentAC;
        MessageDAO mes;
        string id;
        string noiDung;
        string nguoiGui;

        public formReadMessage(Account ac)
        {
            InitializeComponent();
            currentAC = ac;
            mes = new MessageDAO();
            dataGridView1.DataSource = mes.GetListMessage(ac);
            btnDoc.Enabled = false;
            btnDel.Enabled = false;
            btnRep.Enabled = false;
        }

        private void btnDoc_Click(object sender, EventArgs e)
        {
            MessageBox.Show(noiDung, "Nội dung tin nhắn");
            if(mes.DaDocTn(id))
            {
                mes.ReLoad();
                //dataGridView1.Rows.Clear();
                dataGridView1.DataSource = mes.GetListMessage(currentAC);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;
            btnDoc.Enabled = true;
            btnDel.Enabled = true;
            btnRep.Enabled = true;
            int index = dataGridView1.CurrentRow.Index;
            DataGridViewRow row = dataGridView1.Rows[index];
            DataGridViewCellCollection cel = row.Cells;
            id = cel[0].Value.ToString();
            noiDung = cel[2].Value.ToString();
            nguoiGui = cel[1].Value.ToString();
        }

        private void btnRep_Click(object sender, EventArgs e)
        {
            formSendMessage f = new formSendMessage(1, nguoiGui);
            f.ShowDialog();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (mes.XoaTn(id))
            {
                mes.ReLoad();
                //dataGridView1.Rows.Clear();
                dataGridView1.DataSource = mes.GetListMessage(currentAC);
            }
            MessageBox.Show("Đã xóa thành công!", "Thông báo");
        }
    }
}
