using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhom9_QLyChungCu.DTO
{
    class KhachHang
    {
        string iD;
        string ngayMuaNha;
        string iDCanHo;
        string cmnd;
        string name;
        string gioiTinh;
        string trangThai;


        public string ID { get => iD; set => iD = value; }
        public string NgayMuaNha { get => ngayMuaNha; set => ngayMuaNha = value; }
        public string IDCanHo { get => iDCanHo; set => iDCanHo = value; }
        public string Cmnd { get => cmnd; set => cmnd = value; }
        public string Name { get => name; set => name = value; }
        public string GioiTinh { get => gioiTinh; set => gioiTinh = value; }
        public string TrangThai { get => trangThai; set => trangThai = value; }

        public KhachHang(DataRow row)
        {
            this.ID = row["ID"].ToString();
            this.NgayMuaNha = row["NgayMuaNha"].ToString();
            this.IDCanHo = row["IDCanHo"].ToString();
            this.Cmnd = row["CMND"].ToString();
            this.Name = row["name"].ToString();
            this.GioiTinh = row["GioiTinh"].ToString();
            this.TrangThai = row["TrangThai"].ToString();
        }
            
    }
}
