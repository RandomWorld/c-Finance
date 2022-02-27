using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.OleDb;


namespace DataBase
{
    class DataBase
    {
        private string name; // field
        public string Name   // property
        {
            get { return name; }   // get method
            set { name = value; }  // set method
        }

        private string user; // field
        public string User   // property
        {
            get { return user; }   // get method
            set { user = value; }  // set method
        }

        private string pwd; // field
        public string Pwd   // property
        {
            get { return pwd; }   // get method
            set { pwd = value; }  // set method
        }

        private string ip; // field
        public string Ip   // property
        {
            get { return ip; }   // get method
            set { ip = value; }  // set method
        }

        private OleDbConnection con;

        public void lllll(int type)
        {
            String rst;
            con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\randomizer\\IDrive-Sync\\Proyectos\\BBDD\\data.accdb");
            con.Open();
        }
           
        
        
        string strSQL = "SELECT * FROM Developer";

    }
}
