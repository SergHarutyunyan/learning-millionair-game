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
        List<Categories> categoryList = new List<Categories>();



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
                else
                {
                    categoryList.Clear();
                }

            }
        }


        public List<Categories> GetCategoryList()
        {
            using (SqlConnection connection = new SqlConnection(ConnString))
            {
                SqlCommand command = new SqlCommand("GetCategoryList", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader Rdr = command.ExecuteReader();

                while (Rdr.Read())
                {
                    Categories category = new Categories();
                    category.Id = Rdr.GetInt32(0);
                    category.Price = Rdr.GetInt32(1);
                    category.IsPrimary = Rdr.GetBoolean(2);

                    categoryList.Add(category);
                }

                return categoryList;

            }
        }


        public int CheckAnswer(int id, int answer)
        {
            using (SqlConnection connection = new SqlConnection(ConnString))
            {
                SqlCommand command = new SqlCommand("CheckAnswer", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;


                SqlParameter firstParameter = new SqlParameter();
                firstParameter.ParameterName = "@answer";
                firstParameter.Value = answer;
                command.Parameters.Add(firstParameter);

                SqlParameter secondParameter = new SqlParameter();
                secondParameter.ParameterName = "@id";
                secondParameter.Value = id;
                command.Parameters.Add(secondParameter);

                connection.Open();
                int result = (int)command.ExecuteScalar();


                return result;
            }
        }


        public int GetAnswer(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnString))
            {
                SqlCommand command = new SqlCommand("GetAnswer", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;


                SqlParameter secondParameter = new SqlParameter();
                secondParameter.ParameterName = "@id";
                secondParameter.Value = id;
                command.Parameters.Add(secondParameter);

                connection.Open();
                int result = (int)command.ExecuteScalar();


                return result;
            }
        }

    }
}
