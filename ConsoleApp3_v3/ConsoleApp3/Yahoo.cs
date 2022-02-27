
using System;
using System.Data;
using System.Net;
using System.Data.SQLite;
using HtmlAgilityPack;


namespace YahooStocks
{
    public class Downloader
    {
        private string urlTemplate =
            @"https://query1.finance.yahoo.com/v7/finance/download/[symbol]" +
            "?period1=[start]&period2=[end]&interval=1d&events=history&includeAdjustedClose=true";

        public DataTable UpdateSymbol(string symbol, DateTime startDate, DateTime endDate)
        {
            long reference_date = new DateTime(1970, 1, 1).Ticks;
            long startDate_long = startDate.Ticks;
            long endDate_long = endDate.Ticks;

            long Start_Long = (startDate_long - reference_date)/ 10000000;
            long End_Long = (endDate_long - reference_date)/ 10000000;

            urlTemplate = urlTemplate.Replace("[symbol]", symbol);
            urlTemplate = urlTemplate.Replace("[start]", Start_Long.ToString());
            urlTemplate = urlTemplate.Replace("[end]", End_Long.ToString());
            
            string history = String.Empty;
            Console.WriteLine(urlTemplate);
            WebClient wc = new WebClient();
            try
            {
                history = wc.DownloadString(urlTemplate);
            }
            catch (WebException wex)
            {
                Console.WriteLine(wex.Message);
                //  throw wex;
            }
            finally
            {
                wc.Dispose();
            }
            DataTable dt = new DataTable();
            // trim off unused characters from end of line
            history = history.Replace("\r", "");
            // split to array on end of line
            string[] rows = history.Split('\n');
            // split to colums
            string[] colNames = rows[0].Split(',');
            // add the columns to the DataTable
            foreach (string colName in colNames)
                dt.Columns.Add(colName);
            DataRow row = null;
            string[] rowValues;
            object[] rowItems;
            // split the rows
            for (int i = rows.Length - 1; i > 0; i--)
            {
                rowValues = rows[i].Split(',');
                row = dt.NewRow();
                rowItems = ConvertStringArrayToObjectArray(rowValues);
                if (rowItems[0] != null && (string)rowItems[0] != "")
                {
                    row.ItemArray = rowItems;
                    dt.Rows.Add(row);
                }
            }
            return dt;
        }
        public void InsertOrUpdateIssue(DataTable issueTable, string symbol)
        {
            if (issueTable.Rows.Count == 0) return;
            symbol = symbol.Replace("^", "");
            string InsertMasterSQL = Constants.NewSymbolSQLTemplate;
            //"REPLACE INTO MASTER (SYMBOL,FIRSTDATE,LASTDATE) VALUES (@Symbol, @FirstDate, @LastDate)";

            DateTime FirstDate = Convert.ToDateTime(issueTable.Rows[0]["Date"]);
            DateTime LastDate = Convert.ToDateTime(issueTable.Rows[issueTable.Rows.Count - 1]["Date"]);
            object[] parms = { symbol, FirstDate, LastDate };
            SQLiteHelper.ExecuteNonQuery(Constants.ConnectionString, InsertMasterSQL, parms);
            string createIssueTableSql = Constants.SymbolSQLTemplate;
            // "CREATE TABLE [Data](Symbol VARCHAR(50) NOT NULL,Date DATETIME NOT NULL,
            // Open FLOAT,High FLOAT NOT NULL, Low FLOAT NOT NULL, Close FLOAT NOT NULL, Volume INTEGER)";
            createIssueTableSql = createIssueTableSql.Replace("[Data]", symbol);
            try
            {
                SQLiteHelper.ExecuteNonQuery(Constants.ConnectionString, createIssueTableSql, null);
            }
            catch (Exception ex)
            {
                // TABLE ALREADY EXISTS (Sorry to use exception for biz logic, but hey...)
            }
            SQLiteConnection cn = new SQLiteConnection(Constants.ConnectionString);
            cn.Open();
            // always do multiple operations in SQLite in a transaction!
            SQLiteTransaction trans = cn.BeginTransaction();
            //"INSERT INTO [SYMBOL] (SYMBOL,DATE,OPEN,HIGH,LOW,CLOSE,VOLUME) 
            // VALUES(@Symbol,@Date,@Open,@High,@Low,@Close,@Volume)"
            string sql = Constants.InsertSymbolSQLTemplate;
            foreach (DataRow row in issueTable.Rows)
            {
                string sym = symbol;
                DateTime date = Convert.ToDateTime(row["Date"]);
                float open = (float)Convert.ToDouble(row["Open"]);
                float high = (float)Convert.ToDouble(row["High"]);
                float low = (float)Convert.ToDouble(row["Low"]);
                float close = (float)Convert.ToDouble(row["close"]);
                double volume = Convert.ToDouble(row["Volume"]);
                object[] parms2 = { sym, date, open, high, low, close, volume };
                sql = sql.Replace("[SYMBOL]", symbol);
                SQLiteHelper.ExecuteNonQuery(trans, sql, parms2);
            }
            trans.Commit();  //using a transaction for SQLite speeds up multi-inserts bigtime!
            cn.Close();
        }

        private object[] ConvertStringArrayToObjectArray(string[] input)
        {
            int elements = input.Length;
            object[] objArray = new object[elements];
            input.CopyTo(objArray, 0);
            return objArray;
        }
    }
}


namespace PIMCO
{    public class Downloader
    {
        private string urlTemplate =
            @"https://www.pimco.es/es-ES/handlers/GetFundData.ashx?args=%7B%20cusip%20:%20%27[cusip]%27," +
            "%20start%20:%20%27[startYear]/[startMonth]/[startDay]" +
            "%20end%27[endYear]/[endMonth]/[endDay]" +
            "%27,%20id:%27pricing%27,%20df:%27%7B30F31DDC-9D46-4A55-AAD6-741EB002F80A%7D@%27,itemid:%27%7B02EEA849-92FB-4F73-9EB4-B8F479FE8449%7D%27%7D";


        public DataTable UpdateSymbol(string symbol, DateTime? startDate, DateTime? endDate)
        {
            DataTable rst = new DataTable();
            if (!endDate.HasValue) endDate = DateTime.Now;
            if (!startDate.HasValue) startDate = DateTime.Now.AddYears(-5);
            if (symbol == null || symbol.Length < 1)
                throw new ArgumentException("Symbol invalid: " + symbol);
            // NOTE: Yahoo's scheme uses a month number 1 less than actual e.g. Jan. ="0"
            int strtMo = startDate.Value.Month - 1;
            string startMonth = strtMo.ToString();
            string startDay = startDate.Value.Day.ToString();
            string startYear = startDate.Value.Year.ToString();

            int endMo = endDate.Value.Month - 1;
            string endMonth = endMo.ToString();
            string endDay = endDate.Value.Day.ToString();
            string endYear = endDate.Value.Year.ToString();

            urlTemplate = urlTemplate.Replace("[symbol]", symbol);

            urlTemplate = urlTemplate.Replace("[startMonth]", startMonth);
            urlTemplate = urlTemplate.Replace("[startDay]", startDay);
            urlTemplate = urlTemplate.Replace("[startYear]", startYear);

            urlTemplate = urlTemplate.Replace("[endMonth]", endMonth);
            urlTemplate = urlTemplate.Replace("[endDay]", endDay);
            urlTemplate = urlTemplate.Replace("[endYear]", endYear);


            return rst;

        }
    }
}

namespace BolsaMadrid
{
    public class Downloader{
        private HtmlWeb web = new HtmlWeb();
        private HtmlDocument doc;
        private HtmlNodeCollection nodes;
        private HtmlAttribute classAttribute;
        private HtmlNode table;
        
        public DataTable BolsaMadrid()
        {
            DataTable rst = new DataTable();
            
            String webAddress = "https://www.bolsamadrid.es/esp/aspx/Indices/Resumen.aspx";
            doc = web.Load(webAddress);


            table = doc.DocumentNode.SelectSingleNode("//*[@id='ctl00_Contenido_tblÍndices']");

            try
            {
                table = doc.DocumentNode.SelectSingleNode("//*[@id='ctl00_Contenido_tblÍndices']");
                
                // -------------- HEADERS -----------

                foreach (var cell in table.SelectNodes(".//th")) 
                {
                    string someVariable= cell.InnerText;
                    rst.Columns.Add(someVariable);
                    Console.WriteLine(someVariable);
                }
                Console.WriteLine(rst.Columns.Count);
        //        Console.WriteLine(table.SelectNodes(".//th").CopyTo);

                //table = doc.DocumentNode.SelectSingleNode(".//td");
                //https://stackoverflow.com/questions/12642049/how-to-add-new-datarow-into-datatable

                int N_Cols = (table.SelectNodes(".//th").Count);
                int N_Rows = (table.SelectNodes(".//td").Count)/ N_Cols;

                foreach (var cell2 in table.SelectNodes(".//td")) // **notice the .**
                {
                    //Console.WriteLine(cell2.Attributes);
                    Console.WriteLine(cell2.Depth);
                    string someVariable = cell2.InnerText.Replace("&#174;", "");
                    //DataRow row = new DataRow;

                    //rst.Rows.Add();
                    Console.WriteLine(someVariable);
                    
                }
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.ToString());
            }

            rst.Columns.Add("aaa");
            

            return rst;
        }
    }

}