using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Logging;


using System.Threading.Tasks;
using System.Net.Http;
using System.Collections;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace hhjd.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }





        [HttpGet]

        [Route("api/getempnames")]
        public List<String> GetEmployeesList()
        {
            
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-LP8T1PB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                try
                {
                    //Select * from table
                    string sql = "SELECT  [id], [empname] ,[empdept] FROM [firstdb].[dbo].[Employee]";
                    var cmd = new SqlCommand(sql, conn);
                    SqlDataAdapter sqladap = new SqlDataAdapter(cmd);
                    conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    List<String> EmpList = new List<String>();
                    while (sdr.Read())
                    {
                        
                        String id = (String)sdr["empname"];
                        Console.WriteLine(id);
                        EmpList.Add(id);
                    }
                    String Ans = "";
                    for(int i=0;i<EmpList.Count();i++)
                    {
                        Ans += (String)EmpList[i];
                    }
                    Console.WriteLine(Ans);
                    //Close the connection
                    conn.Close();
                    return EmpList;




                }

                catch (Exception sql)
                {

                    

                    Console.WriteLine(sql.Message);
                    return null;

                }


            }






        }
        public static string dataSetToJSON(DataSet ds)
        {
            ArrayList root = new ArrayList();
            List<Dictionary<string, object>> table;
            Dictionary<string, object> data;

            foreach (DataTable dt in ds.Tables)
            {
                table = new List<Dictionary<string, object>>();
                foreach (DataRow dr in dt.Rows)
                {
                    data = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        data.Add(col.ColumnName, dr[col]);
                    }
                    table.Add(data);
                }
                root.Add(table);
            }

            return JsonConvert.SerializeObject(root);
        }
    }
}
