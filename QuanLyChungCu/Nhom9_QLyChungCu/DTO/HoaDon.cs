using Nhom9_QLyChungCu.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhom9_QLyChungCu.DTO
{
    public class DichVu
    {
        string iDDichVU;
        string donVi;
        string name;
        int donGia;

        public DichVu(DataRow row)
        {
            this.IDDichVU = row["IDDichVu"].ToString();
            this.Name = row["Name"].ToString();
            this.DonVi = row["DonVi"].ToString();
            this.DonGia = (int)row["DonGia"];
        }

        public string DonVi { get => donVi; set => donVi = value; }
        public string Name { get => name; set => name = value; }
        public int DonGia { get => donGia; set => donGia = value; }
        public string IDDichVU { get => iDDichVU; set => iDDichVU = value; }
    }
    public class HoaDon
    {
        string iDHoaDon;
        string ngayLap; //convert date to string
        int ngay;
        int thang;
        int nam;
        string iDHoaDonChiTiet; //chi tiet tung mat hang
        string nguoiLap; //ID account
        int tongTien;
        List<HoaDonChiTiet> listHDCT = new List<HoaDonChiTiet>();
        public HoaDon(DataRow row)
        {
            this.IDHoaDon = row["IDHoaDon"].ToString();
            this.IDHoaDonChiTiet = row["IDHoaDonChiTiet"].ToString();
            this.NgayLap = row["NgayLap"].ToString();
            string[] temp = this.NgayLap.Split(' ');
            string[] temp1 = temp[0].Split('/');
            this.Ngay = int.Parse(temp1[1]);
            this.Thang = int.Parse(temp1[0]);
            this.Nam = int.Parse(temp1[2]);
            this.NguoiLap = row["NguoiLap"].ToString();
            this.TongTien = (int)row["TongTien"];
            getListHDCT(row["IDHoaDon"].ToString(), row["IDHoaDonChiTiet"].ToString());
        }
        private bool getListHDCT(string id, string idhdct)
        {
            //hdct.IDHoaDonChiTiet, hdct.NgayLap, hdct.IDDichVu, hdct.DonGia, hdct.SoLuong, hdct.ThanhTien
            string query = @"  SELECT * FROM  dbo.HoaDonChiTiet hdct JOIN dbo.HoaDon hd ON  hdct.IDHoaDonChiTiet  = hd.IDHoaDonChiTiet WHERE hd.IDHoaDon = N'" + id + "' AND hd.IDHoaDonChiTiet = N'"+ idhdct +"'";
            try
            {
                DataTable dt = DataProvider.GetDataFromSQL.ExeCuteQuery(query);
                foreach (DataRow row in dt.Rows)
                {
                    HoaDonChiTiet dv = new HoaDonChiTiet(row);
                    ListHDCT.Add(dv);

                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public string IDHoaDon { get => iDHoaDon; set => iDHoaDon = value; }
        public string NgayLap { get => ngayLap; set => ngayLap = value; }
        public string IDHoaDonChiTiet { get => iDHoaDonChiTiet; set => iDHoaDonChiTiet = value; }
        public string NguoiLap { get => nguoiLap; set => nguoiLap = value; }
        public int TongTien { get => tongTien; set => tongTien = value; }
        public List<HoaDonChiTiet> ListHDCT { get => listHDCT; set => listHDCT = value; }
        public int Ngay { get => ngay; set => ngay = value; }
        public int Thang { get => thang; set => thang = value; }
        public int Nam { get => nam; set => nam = value; }
    }
    public class HoaDonChiTiet
    {
        string iDHoaDonChiTiet;//nvarchar(10)
        string ngayLap;
        int soLuong;
        int donGia;
        int thanhTien;
        string idDichVu;
        int ngay;
        int thang;
        int nam;
        List<DichVu> listDichVu = new List<DichVu>();
        public HoaDonChiTiet(DataRow row)
        {
            this.IdDichVu = row["IDDichVu"].ToString();
            this.IDHoaDonChiTiet = row["IDHoaDonChiTiet"].ToString();
            this.NgayLap = row["NgayLap"].ToString();
            string[] temp = this.NgayLap.Split(' ');
            string[] temp1 = temp[0].Split('/');
            this.Ngay = int.Parse(temp1[1]);
            this.Thang = int.Parse(temp1[0]);
            this.Nam = int.Parse(temp1[2]);
            this.DonGia = (int)row["DonGia"];
            this.SoLuong = (int)row["SoLuong"];
            this.ThanhTien = (int)row["ThanhTien"];
            getListDVForHDCT(row["IDHoaDonChiTiet"].ToString());
        }
        private bool getListDVForHDCT(string id)
        {
            //dv.IDDichVu, dv.Name, dv.DonGia, dv.DonVi
            string query = @"SELECT * FROM dbo.DichVu dv JOIN  dbo.HoaDonChiTiet hdct ON dv.IDDichVu = hdct.IDDichVu WHERE hdct.IDHoaDonChiTiet = N'" + id +"'";
            try
            {
                DataTable dt = DataProvider.GetDataFromSQL.ExeCuteQuery(query);
                foreach (DataRow row in dt.Rows)
                {
                    DichVu dv = new DichVu(row);
                    ListDichVu.Add(dv);

                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string IDHoaDonChiTiet { get => iDHoaDonChiTiet; set => iDHoaDonChiTiet = value; }
        public string NgayLap { get => ngayLap; set => ngayLap = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
        public int DonGia { get => donGia; set => donGia = value; }
        public int ThanhTien { get => thanhTien; set => thanhTien = value; }
        public string IdDichVu { get => idDichVu; set => idDichVu = value; }
        public List<DichVu> ListDichVu { get => listDichVu; set => listDichVu = value; }
        public int Nam { get => nam; set => nam = value; }
        public int Thang { get => thang; set => thang = value; }
        public int Ngay { get => ngay; set => ngay = value; }
    }
}
