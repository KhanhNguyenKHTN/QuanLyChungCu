using System;
using System.Collections.Generic;
using System.Data;
using Nhom9_QLyChungCu.DTO;

namespace Nhom9_QLyChungCu.DAO
{
    public class HoaDonDAO
    {
        public List<HoaDon> listHoadDon = new List<HoaDon>();
        //public List<HoaDonChiTiet> listHdChiTiet = new List<HoaDonChiTiet>();
        public List<DichVu> listDichVu = new List<DichVu>();


        public HoaDonDAO()
        {
            LoadHoaDonAll();
        }
        private void getHoaDon()
        {
            string query = @" SELECT * FROM dbo.HoaDon";
            DataTable dt = DataProvider.GetDataFromSQL.ExeCuteQuery(query);
            //hd = dt;
            foreach (DataRow row in dt.Rows)
            {
                HoaDon hd = new HoaDon(row);
                listHoadDon.Add(hd);
            }
        }
        private void getAllDV()
        {
            string query = @"SELECT * FROM dbo.DichVu";
            DataTable dt = DataProvider.GetDataFromSQL.ExeCuteQuery(query);
            //hd = dt;
            foreach (DataRow row in dt.Rows)
            {
                DichVu dv = new DichVu(row);
                listDichVu.Add(dv);
            }
        }
        public string getNextIDHDCT(DateTime date, CanHo canHo)
        {
            string IDHDCT = "";
            string s = date.ToString();
            string[] temp = s.Split(' ');
            temp = temp[0].Split('/');
            int thang = int.Parse(temp[0]);
            int nam = int.Parse(temp[2]);
            //foreach(HoaDon hd in listHoadDon)
            //{
            //    if(hd.Thang == thang && hd.Nam == nam)
            //    {

            //    }
            //}
            //bool Thoa = false;
            for(int i = 0; i < listHoadDon.Count; i++)
            {
                if(listHoadDon[i].Thang == thang && listHoadDon[i].Nam == nam && listHoadDon[i].IDHoaDon.Replace(" ","") == canHo.MaHD)
                {
                    //if (listHoadDon.Count < 100)
                    //{
                    //    IDHDCT = "CT" + String.Format("{0:000}", listHoadDon.Count);
                    //    return IDHDCT;
                    //}
                    //IDHDCT = "CT" + listHoadDon.Count.ToString();
                    IDHDCT = listHoadDon[i].IDHoaDonChiTiet;
                    return IDHDCT;
                }
            }
            if(listHoadDon.Count < 100)
            {
                IDHDCT = "CT"+ String.Format("{0:000}", listHoadDon.Count + 1);
                return IDHDCT;
            }
            IDHDCT = "CT" + (listHoadDon.Count + 1).ToString();
            return IDHDCT;
        }
        public string getNextMaDV()
        {
            string ma = "";
            int max = 0;
            foreach(DichVu i in listDichVu)
            {
                int temp = int.Parse(i.IDDichVU.Replace("MH", ""));
                if(temp > max)
                {
                    max = temp;
                }
            }
            ma = "MH" + String.Format("{0:000}", max +1 );
            return ma;
        }
        private void LoadHoaDonAll()
        {
            getHoaDon();
            // getDichVu();
            getAllDV();
        }
        public void ReloadHoaDon()
        {
            //listDichVu.Clear();
            listHoadDon.Clear();
            //listHdChiTiet.Clear();
            LoadHoaDonAll();
        }
        public void deleteHoaDon(string maHD, string hdct)
        {
            //delete ta ca hoa don chi tiet trước rồi mới xóa hóa đơn
            foreach(HoaDon hd in listHoadDon)
            {
                if(hd.IDHoaDon == maHD&& hd.IDHoaDonChiTiet == hdct) //timf thay hoa don can xoa
                {
                    //xoa cac hoa don chi tiet
                    //moi hoa don chi tiet se la 1 idHoaDon trong 1 thang
                    string query = @"DELETE dbo.HoaDonChiTiet WHERE IDHoaDonChiTiet = N'" + hd.IDHoaDonChiTiet + "'";
                    DataProvider.GetDataFromSQL.ExecuteNonQuery(query);
                    //xoa hoaDon trong ban HD
                    query = @"DELETE dbo.HoaDon WHERE IDHoaDon = N'"+ hd.IDHoaDon +"' AND IDHoaDonChiTiet = N'"+ hd.IDHoaDonChiTiet + "'";
                    DataProvider.GetDataFromSQL.ExecuteNonQuery(query);
                    return;
                }
            }
        }
        public DichVu getDichVuByID(string maDV)
        {
            foreach(DichVu i in listDichVu)
            {
                if(i.IDDichVU == maDV)
                {
                    return i;
                }
            }
            return null;
        }
        public DataTable GetListDichVu()
        {
            DataTable result = new DataTable();
            result.Columns.Add("Mã Dịch Vụ", typeof(string));
            result.Columns.Add("Tên Dịch Vụ", typeof(string));
            result.Columns.Add("Đơn giá", typeof(int));
            result.Columns.Add("Đơn vị", typeof(string));
            foreach(DichVu item in listDichVu)
            {
                result.Rows.Add(item.IDDichVU, item.Name, item.DonGia, item.DonVi);
            }
            return result;
        }

  #region Get Thông tin
        public DataTable getHoaDonByNgay(int ngay, int thang, int nam, string MaHoaDon = null)
        {
            DataTable result = new DataTable();
            result.Columns.Add("Số Phòng", typeof(string));
            result.Columns.Add("ID Hóa Đơn", typeof(string));
            result.Columns.Add("ID Hóa Đơn CT", typeof(string));
            result.Columns.Add("Người lập", typeof(string));
            result.Columns.Add("Ngày lập", typeof(string));
            result.Columns.Add("Tổng Tiền", typeof(int));
            string ngayThang = String.Format("{0:00}", ngay) + "/" + String.Format("{0:00}", thang) + "/" + nam.ToString();
            for (int i = 0; i < listHoadDon.Count; i++)
            {
                if (listHoadDon[i].Ngay == ngay && listHoadDon[i].Thang == thang && listHoadDon[i].Nam == nam)
                {
                    if((MaHoaDon == null || (MaHoaDon != null && listHoadDon[i].IDHoaDon.Replace(" ", "") == MaHoaDon)))
                    {
                        string s = listHoadDon[i].IDHoaDon.Replace("HD", "");
                        s = s.Replace(" ", "");
                        result.Rows.Add(s, listHoadDon[i].IDHoaDon, listHoadDon[i].IDHoaDonChiTiet.Replace(" ", ""), listHoadDon[i].NguoiLap.Replace(" ", ""), ngayThang, listHoadDon[i].TongTien);
                    }                  
                }
            }
            return result;
        }
        public DataTable getCTHDByID(string idHDCT, string idHD)
        {
            DataTable result = new DataTable();
            //result.Columns.Add("ID Hóa Đơn CT", typeof(string));
            result.Columns.Add("ID Dịch Vụ", typeof(string));
            result.Columns.Add("Tên Dịch Vụ", typeof(string));
            result.Columns.Add("Ngày Lập", typeof(string));
            result.Columns.Add("Đơn Giá", typeof(string));
            result.Columns.Add("Số Lượng", typeof(string));
            result.Columns.Add("Thành Tiền", typeof(int));
            for (int i = 0; i < listHoadDon.Count; i++)
            {
                if (listHoadDon[i].IDHoaDon == idHD && listHoadDon[i].IDHoaDonChiTiet == idHDCT)
                {
                    int tongTien = 0;
                    foreach (HoaDonChiTiet hdct in listHoadDon[i].ListHDCT)
                    {
                        result.Rows.Add(hdct.IdDichVu, getTenDichVu(hdct, hdct.IdDichVu), cutNgay(hdct.NgayLap), hdct.DonGia, hdct.SoLuong, hdct.ThanhTien);
                        tongTien += hdct.ThanhTien;
                    }
                    result.Rows.Add("Tổng cộng:", "", "", "", "", tongTien);
                    return result;
                }

            }
            return result;
        }
        string getTenDichVu(HoaDonChiTiet hd, string id)
        {
            for (int i = 0; i < hd.ListDichVu.Count; i++)
            {
                if (hd.ListDichVu[i].IDDichVU == id)
                    return hd.ListDichVu[i].Name;
            }
            return "";
        }
        string cutNgay(string date)
        {
            string[] temp = date.Split(' ');
            temp = temp[0].Split('/');
            return String.Format("{0:00}", int.Parse(temp[1])) + "/" + String.Format("{0:00}", int.Parse(temp[0])) + "/" + String.Format("{0:00}", int.Parse(temp[2]));
        }
        public DataTable getHoaDonByThang(int thang, int nam, string MaHoaDon = null)
        {
            DataTable result = new DataTable();
            result.Columns.Add("Số Phòng", typeof(string));
            result.Columns.Add("ID Hóa Đơn", typeof(string));
            result.Columns.Add("ID Hóa Đơn CT", typeof(string));
            result.Columns.Add("Người lập", typeof(string));
            result.Columns.Add("Ngày lập", typeof(string));
            result.Columns.Add("Tổng Tiền", typeof(int));
            //string ngayThang = String.Format("{0:00}", ngay) + "/" + String.Format("{0:00}", thang) + "/" + nam.ToString();
            for (int i = 0; i < listHoadDon.Count; i++)
            {
                if (listHoadDon[i].Thang == thang && listHoadDon[i].Nam == nam)
                {
                    if ((MaHoaDon == null || (MaHoaDon != null && listHoadDon[i].IDHoaDon.Replace(" ", "") == MaHoaDon)))
                    {
                        string s = listHoadDon[i].IDHoaDon.Replace("HD", "");
                        s = s.Replace(" ", "");
                        result.Rows.Add(s, listHoadDon[i].IDHoaDon, listHoadDon[i].IDHoaDonChiTiet.Replace(" ", ""), listHoadDon[i].NguoiLap.Replace(" ", ""), cutNgay(listHoadDon[i].NgayLap), listHoadDon[i].TongTien);
                    }
                    
                }
            }
            return result;
        }
        public DataTable getHDbyCanHo(string MaHoaDon)
        {
            DataTable result = new DataTable();
            result.Columns.Add("Số Phòng", typeof(string));
            result.Columns.Add("ID Hóa Đơn", typeof(string));
            result.Columns.Add("ID Hóa Đơn CT", typeof(string));
            result.Columns.Add("Người lập", typeof(string));
            result.Columns.Add("Ngày lập", typeof(string));
            result.Columns.Add("Tổng Tiền", typeof(int));
            //string ngayThang = String.Format("{0:00}", ngay) + "/" + String.Format("{0:00}", thang) + "/" + nam.ToString();
            for (int i = 0; i < listHoadDon.Count; i++)
            {
                if (listHoadDon[i].IDHoaDon.Replace(" ", "") == MaHoaDon)
                {
                    string s = listHoadDon[i].IDHoaDon.Replace("HD", "");
                    s = s.Replace(" ", "");
                    result.Rows.Add(s, listHoadDon[i].IDHoaDon, listHoadDon[i].IDHoaDonChiTiet.Replace(" ", ""), listHoadDon[i].NguoiLap.Replace(" ", ""), cutNgay(listHoadDon[i].NgayLap), listHoadDon[i].TongTien);
                }
            }
            return result;
        }
        public DataTable getHoaDonByNam(int nam, string MaHoaDon = null)
        {
            DataTable result = new DataTable();
            result.Columns.Add("Số Phòng", typeof(string));
            result.Columns.Add("ID Hóa Đơn", typeof(string));
            result.Columns.Add("ID Hóa Đơn CT", typeof(string));
            result.Columns.Add("Người lập", typeof(string));
            result.Columns.Add("Ngày lập", typeof(string));
            result.Columns.Add("Tổng Tiền", typeof(int));
            //string ngayThang = String.Format("{0:00}", ngay) + "/" + String.Format("{0:00}", thang) + "/" + nam.ToString();
            for (int i = 0; i < listHoadDon.Count; i++)
            {
                if (listHoadDon[i].Nam == nam)
                {
                    if ((MaHoaDon == null || (MaHoaDon != null && listHoadDon[i].IDHoaDon.Replace(" ", "") == MaHoaDon)))
                    {
                        string s = listHoadDon[i].IDHoaDon.Replace("HD", "");
                        s = s.Replace(" ", "");
                        result.Rows.Add(s, listHoadDon[i].IDHoaDon, listHoadDon[i].IDHoaDonChiTiet.Replace(" ", ""), listHoadDon[i].NguoiLap.Replace(" ", ""), cutNgay(listHoadDon[i].NgayLap), listHoadDon[i].TongTien);
                    }

                }
            }
            return result;
        }
#endregion

 #region Thống Kê

        public DataTable ThongKeTheoThang(DataTable dt)
        {
            DataTable result = new DataTable();
            result.Columns.Add("Tháng", typeof(string));
            result.Columns.Add("Tổng Doanh Thu", typeof(string));

            List<ThongKe> tk = new List<ThongKe>();
            for(int i =0; i < 12; i++)
            {
                ThongKe temp = new ThongKe();
                temp.thang = i + 1;
                tk.Add(temp);
            }

            foreach (DataRow row in dt.Rows)
            {
                string dateTime = row["Ngày lập"].ToString();
                string[] cutDateTime = dateTime.Split('/');
                int thang = int.Parse(cutDateTime[1]);
                int TongTien = (int)row["Tổng Tiền"];
                tk[thang - 1].thang = thang;
                tk[thang - 1].tongDoanhThu = TongTien;
            }
            for (int i = 0; i < 12; i++)
            {
                result.Rows.Add( "Tháng " + tk[i].thang, tk[i].tongDoanhThu);
            }
            return result;
        }
        public DataTable ThongKeTheoCanHo(DataTable dt) //0 ~~ Null
        {
            DataTable result = new DataTable();
            result.Columns.Add("Mã Phòng", typeof(string));
            result.Columns.Add("Tổng Doanh Thu", typeof(string));
            List<ThongKe> save = new List<ThongKe>();
            foreach (DataRow row in dt.Rows)
            {
                string Phong = row["Số Phòng"].ToString();
                int TongTien = (int)row["Tổng Tiền"];
                CongTien(Phong.Replace(" ", ""), TongTien, save);

            }

            foreach (ThongKe item in save)
            {
                result.Rows.Add(item.tenPhong, item.tongDoanhThu);
            }
            return result;
        }
        int getThang(string date)
        {
            string[] temp = date.Split(' ');
            temp = temp[0].Split('/');
            return int.Parse(temp[0]);
        }
        List<ThongKe> CongTien(string Phong, int tongTien, List<ThongKe> tk)
        {
            foreach (ThongKe item in tk)
            {
                if (item.tenPhong == Phong)
                {
                    item.tongDoanhThu += tongTien;
                    return tk;
                }
            }
            ////else tạo mới
            ThongKe temp = new ThongKe();
            temp.tenPhong = Phong;
            //temp.thang = hd.Thang;
            temp.tongDoanhThu = tongTien;
            tk.Add(temp);
            return tk;

        }
#endregion


    }
     class ThongKe
    {
        public int thang;
        public string tenPhong;
        public int tongDoanhThu;
    }
}
