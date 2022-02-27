using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.IO;


namespace DataBase_
{
    public class DataBase_ : Interface_DataBase
    {
        public string Name { get; set; }
        //public object MyConn;
        private String pOrigen;
        public String Origen
        {
            get => pOrigen;
            set { pOrigen = value; }
        }

        public OleDbConnection MyConn;

       /* public Object MyConn{
            get => pMyConn;
            set { pMyConn = connection(); }
        }*/

        private Object pMyConn;
        private Object connection()
        {
            OleDbConnection pMyConn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + this.Origen + ";Persist Security Info=False;");
            this.MyConn = pMyConn;
            MyConn.Open();
            return 0;
        }
        public DataBase_()
        {
            try {
                Init_Variables();
                Console.WriteLine("Init Variable ---- Done.");
                connection();
            }
            catch (Exception e){
                Console.WriteLine("Error al conectare . Error: " + e.ToString());
            }
        }
        public void Init_Variables() { 
            if (this.Origen == null)            { 
             this.Origen = Directory.Exists("C:\\Users\\randomizer\\IDrive-Sync") ?
                "C:\\Users\\randomizer\\IDrive-Sync\\Proyectos\\BBDD\\data.accdb" :
                "C:\\Users\\casa_\\IDrive-Sync\\Proyectos\\BBDD\\data.accdb";           }
        }
        public DataTable PullData(string query)
        {
            if (MyConn != null && MyConn.State == ConnectionState.Closed)
            {
                this.MyConn.Open();
            }
            
            DataTable dataTable = new DataTable();
            OleDbCommand cmd = new OleDbCommand(query, MyConn);

            Console.WriteLine("{0}", query);

            // create data adapter
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            // this will query your database and return the result to your datatable
            da.Fill(dataTable);
            da.Dispose();
            return dataTable;
        }
        public int insert_1_data(Estructura.Quote input, string tbl = "HistoricalData_Funds") {

            int rst = 0;
            String query_ = "INSERT INTO " + tbl + "(ID_Fund_, Date_, Price_, UpdatedDate_) " +
                                                   " VALUES ( " + input.ID + ",#" + input.Date + "#," + input.Price + ",#" + input.CurrentDate + "#);";
            try
            {
                OleDbCommand command = new OleDbCommand(query_, MyConn);
                command.ExecuteNonQuery();
            }
            catch (Exception e) { 
                Console.WriteLine(e.ToString()); 
                Console.WriteLine(query_); 
            }
            return rst;
        }

    }
}
