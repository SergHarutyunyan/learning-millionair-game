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
        static string engConnectionString = ConfigurationManager.ConnectionStrings["EngWordManager"].ConnectionString;


        public void GetQuestions(Question q)
        {
            using(SqlConnection connection = new SqlConnection(engConnectionString))
            {
                SqlCommand command = new SqlCommand("GetQuestion", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;


                SqlParameter firstParameter = new SqlParameter();
                firstParameter.ParameterName = "@category";
                firstParameter.Value = q.category;
                command.Parameters.Add(firstParameter);

                connection.Open();
                SqlDataReader Rdr = command.ExecuteReader();

                while(Rdr.Read())
                {

                }
            }
        }
    }
}
