using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using BusinessObjectModels;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;

namespace DataAccess
{
    public class DataManager : DbContext
    {
        static string ConnString = ConfigurationManager.ConnectionStrings["DataManager"].ConnectionString;
        static List<int> categoryList = new List<int>();

        public void GetNextQuestion(int category)
        {
            using (SqlConnection connection = new SqlConnection(ConnString))
            {
                SqlCommand command = new SqlCommand("GetQuestion", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;


                SqlParameter firstParameter = new SqlParameter();
                firstParameter.ParameterName = "@category";
                firstParameter.Value = category;
                command.Parameters.Add(firstParameter);

                connection.Open();
                SqlDataReader Rdr = command.ExecuteReader();

                if (Rdr.Read())
                {
                    Questions.Id = Rdr.GetInt32(0);
                    Questions.Question = Rdr.GetString(1);
                    Questions.VariantA = Rdr.GetString(2);
                    Questions.VariantB = Rdr.GetString(3);
                    Questions.VariantC = Rdr.GetString(4);
                    Questions.VariantD = Rdr.GetString(5);
                    Questions.Answer = Rdr.GetByte(6);
                }

            }
        }


        public List<int> getCategoryList()
        {
            using (SqlConnection connection = new SqlConnection(ConnString))
            {
                SqlCommand command = new SqlCommand("GetCategoryList", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader Rdr = command.ExecuteReader();

                while (Rdr.Read())
                {
                    categoryList.Add(Rdr.GetInt32(0));
                }

                return categoryList;

            }
        }




    }
}
