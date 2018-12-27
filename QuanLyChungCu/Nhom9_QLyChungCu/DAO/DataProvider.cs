using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhom9_QLyChungCu.DAO
{
    public class DataProvider
    {

        private static DataProvider getDataFromSQL;


        public static DataProvider GetDataFromSQL
        {
            get { if (getDataFromSQL == null) getDataFromSQL = new DataProvider(); return getDataFromSQL; }
            private set { getDataFromSQL = value; }
        }

        public string ConnectionSTR { get => connectionSTR; set => connectionSTR = value; }

        private DataProvider(){ }

        private string connectionSTR;//= @"Data Source=.\sqlexpress;Initial Catalog=QuanLyChungCu;Integrated Security=True";
        public DataTable ExeCuteQuery(string query)
        {
            DataTable data = new DataTable();

            using (SqlConnection connection = new SqlConnection(ConnectionSTR))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection); //truyen cau query
                SqlDataAdapter adapter = new SqlDataAdapter(command); //lay data tu tu ket qua tra ve
                adapter.Fill(data); //them data vao datatable
                connection.Close();
            }
            return data;
        }
        //dùng để insert dữ liệu
        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionSTR))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);

                    if (parameter != null)
                    {
                        string[] listPara = query.Split(' ');
                        int i = 0;
                        foreach (string item in listPara)
                        {
                            if (item.Contains('@'))
                            {
                                command.Parameters.AddWithValue(item, parameter[i]);
                                i++;
                            }
                        }
                    }

                    data = command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch { }


            return data;
        }
        public string QueryWithParameter(string query, object[] para = null) //chỉ dành cho kiểu char
        {
            int j = 0;
            string result = "";
            string[] cut = query.Split(' ');
            for(int i = 0; i < cut.Length; i++)
            {
                if(cut[i].Contains('@'))
                {
                    result +=" " + cut[i] + "= N'"+ para[j].ToString() +"'";
                    j++;
                }
                else
                {
                    result +=" " + cut[i];
                }

            }
            return result;
        }
        public object ExecuteScalar(string query, object[] parameter = null)
        {
            object data = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionSTR))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);

                    if (parameter != null)
                    {
                        string[] listPara = query.Split(' ');
                        int i = 0;
                        foreach (string item in listPara)
                        {
                            if (item.Contains('@'))
                            {
                                command.Parameters.AddWithValue(item, parameter[i]);
                                i++;
                            }
                        }
                    }

                    data = command.ExecuteScalar();

                    connection.Close();
                }

            }
            catch { }

            return data;
        }

    }
}
