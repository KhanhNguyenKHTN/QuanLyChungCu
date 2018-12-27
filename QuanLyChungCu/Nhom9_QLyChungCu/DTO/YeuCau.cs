using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhom9_QLyChungCu.DTO
{
    public class YeuCau
    {
        string from;
        string to;
        string noiDung;
        int daXem;
        string id;
        public YeuCau(DataRow row)
        {
            this.From = row["fromAC"].ToString();
            this.To = row["toAC"].ToString();
            this.NoiDung = row["NoiDung"].ToString();
            this.Id = row["id"].ToString();
            this.DaXem = (int)row["daXem"];
        }
        public string From { get => from; set => from = value; }
        public string To { get => to; set => to = value; }
        public string NoiDung { get => noiDung; set => noiDung = value; }
        public int DaXem { get => daXem; set => daXem = value; }
        public string Id { get => id; set => id = value; }

    }
}
