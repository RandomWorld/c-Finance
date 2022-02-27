using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericDDBB
{
    
    public class GenericDDBB : Interface_DataBase
    {
         private DbConnectionStringBuilder builder = new DbConnectionStringBuilder();
        public GenericDDBB() {
            //https://docs.microsoft.com/en-us/dotnet/api/system.data.common.dbconnectionstringbuilder?view=net-6.0
            //DbConnectionStringBuilder builder = new DbConnectionStringBuilder();
            
        }
        void Init_Variables()
        {
            throw new NotImplementedException();
        }
        public void Connection(String Origen)
        {
            switch (Origen.ToLower())
            {
                case "access":
                    builder.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + Origen + "; Persist Security Info = False; ";
                    OleDbConnection DBConnect = new OleDbConnection(builder.ConnectionString);
                    Console.WriteLine("Value is 1");
                    break;
                case "sqlite":
                    builder.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + Origen + "; Persist Security Info = False; ";
                    SqlConnection SQLConnect = new SqlConnection(builder.ConnectionString);
                    Console.WriteLine("Value is 2");
                    break;
                default:
                    Console.WriteLine("value is different");
                    break;
            }

            
            builder.ConnectionString = @"Data Source=c:\MyData\MyDb.mdb";
            builder.Add("Provider", "Microsoft.Jet.Oledb.4.0");
            //builder.Add("Jet OLEDB:Database Password", "*
        }

        void Interface_DataBase.Init_Variables()
        {
            throw new NotImplementedException();
        }
    }
}
