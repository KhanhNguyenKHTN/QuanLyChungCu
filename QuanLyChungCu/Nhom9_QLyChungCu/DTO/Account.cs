using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhom9_QLyChungCu.DTO
{
    public class Account
    {        
        private string userName;
        private string disPlayName;
        private string passWord;
        private int type;
        private string chucVu;
        public string UserName { get => userName; set => userName = value; }
        public string DisPlayName { get => disPlayName; set => disPlayName = value; }
        public string PassWord { get => passWord; set => passWord = value; }
        public int Type { get => type; set => type = value; }
        public string ChucVu { get => chucVu; set => chucVu = value; }

        public Account(DataRow row)
        {         
            this.UserName = row["userName"].ToString();
            this.DisPlayName = row["displayName"].ToString();
            this.PassWord = row["pass"].ToString();
            this.Type = (int)row["type"];
            this.ChucVu = row["chucVu"].ToString();
        }
    }
}
