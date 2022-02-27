using System;
using System.Collections.Generic;
using System.Text;

namespace YahooStocks
{
   public class Constants
    {
       public static readonly string SymbolSQLTemplate =  "CREATE TABLE [Data](Symbol VARCHAR(50) NOT NULL,Date DATETIME NOT NULL PRIMARY KEY,Open FLOAT,High FLOAT NOT NULL, Low FLOAT NOT NULL, Close FLOAT NOT NULL, Volume INTEGER)";
       public static string NewSymbolSQLTemplate = "REPLACE INTO MASTER (SYMBOL,FIRSTDATE,LASTDATE) VALUES (@Symbol, @FirstDate, @LastDate)";
       public static string UpdateSymbolSQLTemplate = "UPDATE [TABLE] SET  OPEN=@Open, High =@High, Low=@Low,Close=@Close, Volume=@Volume WHERE DATE = @DATE";
       public static string InsertSymbolSQLTemplate = "REPLACE INTO [SYMBOL] (SYMBOL,DATE,OPEN,HIGH,LOW,CLOSE,VOLUME) VALUES(@Symbol,@Date,@Open,@High,@Low,@Close,@Volume)";
       public static string GetAllSymbolsSQLTemplate = "SELECT * FROM MASTER WHERE SYMBOL=@Symbol";
       public static string GetIssueDataSQLTemplate = "SELECT * FROM [SYMBOL] WHERE DATE >=@firstDate AND DATE <=@lastDate";
       public static readonly string ConnectionString = "Data Source="+ System.Environment.CurrentDirectory + @"\Stocks.db3;Version=3";
   }
}
namespace GenericFunctions
{
    class GenericFunctions
    {
        public static string GetMonthNumberFromAbbreviation(string mmm)
        {
            if (mmm.Length != 3) { return ("The month length =3 - GetMonthNumberFromAbbreviation"); } 
            // https://www.tutorialsrack.com/articles/185/how-to-get-the-month-number-from-month-name-in-csharp
            
            string[] monthAbbrev = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames;
            // Creates a TextInfo based on the "en-US" culture.
            System.Globalization.TextInfo myTI = new System.Globalization.CultureInfo("en-US", false).TextInfo;
            string monthname = mmm.ToLower();
            monthname = monthname[monthname.Length - 1].Equals(".") ? monthname : monthname + ".";
            int index = Array.IndexOf(monthAbbrev, monthname) + 1;
            return index.ToString("0#");
        }
    }
}