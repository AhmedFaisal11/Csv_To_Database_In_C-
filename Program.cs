using System;
using System.Data.SqlClient;
using System.IO;


namespace sqlConnection
{
    class Program
    {
        static void Main(string[] args)
        {
            var lineNumber = 0;
            // Connection string for SQL Server database 
            //
            using (SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-2K2N69V; Integrated Security = True"))
            {
                //DESKTOP-2K2N69V
                //CSVImport.dbo.Products

                conn.Open();

                using (StreamReader reader = new StreamReader(@"D:\CSV-Import-to-Database-Csharp-master\stock_list.csv"))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();

                        if(lineNumber != 0)
                        {
                            var values = line.Split(',');

                            var sql = "INSERT INTO CSVImport.dbo.Products VALUES  ('" + values[0] + "','" + values[1] + "'," + values[2] + ")";

                            var cmd = new SqlCommand();

                            cmd.CommandText = sql;

                            cmd.CommandType = System.Data.CommandType.Text;

                            cmd.Connection = conn;

                            cmd.ExecuteNonQueryAsync();

                        }

                        lineNumber++;
                        
                    }
                }

                conn.Close();

            }

            Console.WriteLine("Products Imported........");
            Console.ReadLine();
        }
    }
}
