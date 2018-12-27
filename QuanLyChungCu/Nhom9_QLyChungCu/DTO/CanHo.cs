using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhom9_QLyChungCu.DTO
{
    public class CanHo
    {
        int iD;
        string soPhong;
        string stt;
        string chuSoHuu;
        string maHD;
        int giaTien;
        string tinhTrang;
        string idKH;

        public int ID { get => iD; set => iD = value; }
        public string SoPhong { get => soPhong; set => soPhong = value; }
        public string Stt { get => stt; set => stt = value; }
        public string ChuSoHuu { get => chuSoHuu; set => chuSoHuu = value; }
        public string MaHD { get => maHD; set => maHD = value; }
        public int GiaTien { get => giaTien; set => giaTien = value; }
        public string TinhTrang { get => tinhTrang; set => tinhTrang = value; }
        public string IdKH { get => idKH; set => idKH = value; }

        public CanHo(DataRow row)
        {
            this.ID = (int)row["id"];
            this.SoPhong = row["SoPhong"].ToString();
            this.Stt = row["stt"].ToString();
            this.ChuSoHuu = row["ChuSoHuu"].ToString();           
            this.MaHD = row["MaHD"].ToString();
            this.GiaTien = (int)row["giaTien"];
            this.TinhTrang = row["tinhTrang"].ToString();
            this.IdKH = row["IDKH"].ToString();
        }
    }
}
