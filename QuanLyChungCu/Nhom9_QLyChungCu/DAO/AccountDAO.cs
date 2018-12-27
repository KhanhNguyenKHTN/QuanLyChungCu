using Nhom9_QLyChungCu.DTO;
using System.Collections.Generic;
using System.Data;


namespace Nhom9_QLyChungCu.DAO
{
    class AccountDAO //accout
    {
        private List<Account> account = new List<Account>();
        public Account AC; //chỉ public 1 accout

        public AccountDAO()//ham khoi tao la ham load data
        {
            DataTable data = DataProvider.GetDataFromSQL.ExeCuteQuery("SELECT * FROM dbo.Account");
            foreach(DataRow item in data.Rows)
            {
                Account ac = new Account(item);
                this.account.Add(ac);
            }
        }
        public bool Login(string user, string pass)
        {
            foreach(Account item in this.account)
            {
                if(user == item.UserName && pass == item.PassWord) //kiểm tra tên đăng nhập và mật khẩu
                {
                    AC = item;//lấy accout hiện tại ra
                    return true;
                }
            }
            return false;
        }

        public DataTable AccData()
        {
            DataTable result = new DataTable();

            result.Columns.Add("UserName", typeof(string));
            result.Columns.Add("PassWord", typeof(string));
            result.Columns.Add("Tên hiển thị", typeof(string));
            result.Columns.Add("Chức vụ", typeof(string));
            result.Columns.Add("Loại tài khoản", typeof(string));
            
            foreach(Account ac in account)
            {
                result.Rows.Add(ac.UserName, ac.PassWord, ac.DisPlayName, ac.ChucVu, ac.Type);
            }
            return result;
        }
        public bool CheckAcount(string userName)
        {
            //nếu userName đã tồn tại thì tạo ko đc
            foreach(Account a in account)
            {
                if(a.UserName == userName)
                {
                    return false;
                }
            }
            return true;
        }
        public Account GetAccount(string userName)
        {
            foreach (Account a in account)
            {
                if (a.UserName == userName)
                {
                    return a;
                }
            }
            return null;
        }

        //public int CountAdmin(string userName) //mục đích kiểm tra xem ngoài tài khoản này còn có tài khoản admin khác không
        //{
        //    int dem = 0;
        //    foreach(Account a in account)
        //    {
        //        if(a.Type == 1 && userName != a.UserName)
        //        {
        //            dem++;
        //        }
        //    }
        //    return dem;
        //}
        //fix: chỉnh kiểm tra còn cái admin cuối cùng là được
    }
}
