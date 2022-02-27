using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

interface IOption
{
	void SampleMethod();
}

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
			YahooStocks.Downloader llp = new YahooStocks.Downloader();

		//	llp.UpdateSymbol("BBVA.MC",new DateTime(2012,02,05), new DateTime(2022, 02, 05));

			var watch = new System.Diagnostics.Stopwatch();
			//Selenium_Demo.Selenium_Demo SelTest = new Selenium_Demo.Selenium_Demo();
			//SelTest.start_Browser();
			//SelTest.test_search();
			
			DataBase_.DataBase_ DDBB = new DataBase_.DataBase_();
			Programa_Scrapper ScrapperTest = new Programa_Scrapper();
			//Estructura.Quote rst = ScrapperTest.FT_SingleQuote("IE00BZ6SF527", "EUR");
			Estructura.Quote rst1 = new Estructura.Quote();
			BolsaMadrid.Downloader rst3 = new BolsaMadrid.Downloader();
			rst3.BolsaMadrid();

			rst1 = ScrapperTest.FT_SingleQuote("IE00BZ6SF527", "EUR");
			DDBB.insert_1_data(rst1);
			//rst = ScrapperTest.FT_SingleQuote("LU0123357419", "USD");
			//	ScrapperTest.Reuters("BBVA.MC");
			//	ScrapperTest.Yahoo("BBVA.MC");

			DataTable Funds_ = DDBB.PullData("SELECT Universo.ID_, Universo.ISIN_,Universo.CCy_ FROM Universo WHERE(Universo.Fondo = True)");
			//var results = from p in Funds_.AsEnumerable()
			//			  select p["ID_"];


			List<Estructura.Quote> List_rst = new List<Estructura.Quote>();
			List_rst = ScrapperTest.FT_MultipleQuotes(Funds_);

		Estructura.Quote rst;
			watch.Start();
			foreach (System.Data.DataRow iter in Funds_.Rows)
            {
				int id = (int) iter[0];
				string ISIN = (string) iter[1];
				string Ccy = (string)iter[2];
				rst = ScrapperTest.FT_SingleQuote(ISIN,Ccy,id);
				Console.WriteLine("{0},\t{1},\t{2},\t{3}\t{4}", id,ISIN,rst.Price,rst.Date,rst.PrevPrice);
			}
			watch.Stop();
			Console.WriteLine($"------------- Execution Time: {watch.ElapsedMilliseconds} ms------------- ");
			
			watch.Start();

			foreach (Estructura.Quote iter in List_rst)
			{
				long id = iter.ID;
				string ISIN = iter.ISIN;
				//string Ccy = iter.;
				double Price = iter.Price;
				double PrevPrice = iter.PrevPrice;
				Console.WriteLine("{0},\t{1},\t{2},\t{3}", id, ISIN, Price, PrevPrice);
			}
			watch.Stop();

			Console.WriteLine($"------------- Execution Time: {watch.ElapsedMilliseconds} ms------------- ");


			//Estructura.SingleQuote rst = ScrapperTest.primi();
			List<String> fondos = new List<string>();

			
			
			
		//	rst = ScrapperTest.Schroder_SingleQuote("https://www.schroders.com/es/es/inversores-particulares/fondos/gfc/fund/schdr_f00000p9hs/schroder-international-selection-fund-bric-brazil-russia-india-china-a-distribution-eur-av/lu0858243842/profile/");
			DataBase_.DataBase_ rst2;
			//Boolean a=rst2.connection();

			Estructura.Quote BBVA = new Estructura.Quote();

			BBVA.Ticker="BBVA.MC";

			DataTable dividendotbl;
			

			dividendotbl = DDBB.PullData("SELECT Universo.ID_ FROM Universo GROUP BY Universo.ID_, Universo.Name_ HAVING(((Universo.Name_) = 'BBVA'))");
			BBVA.Dividendo = dividendotbl;


			Console.WriteLine("{0}", BBVA.Ticker);

			Console.WriteLine("{0}", BBVA.Dividendo.Rows.Count+1000);
			Console.WriteLine("{0}", BBVA.Dividendo.Columns.Count+22000);
			Console.WriteLine("{0}", BBVA.Dividendo.Rows[0][0]);

			Console.WriteLine("dskopkpokpokpokpokpokpokpokpokpokpoa");
			BlackSchooles.BlackScchooles test = new BlackSchooles.BlackScchooles();

			Console.WriteLine("dsadsa {0}", test.BlackScholes("c", 1, 1, 1, 0.05, 0.25));
			Console.WriteLine("dsadsa {0}", test.BlackScholes("c", 1, 1, 1, 0.05, 0.25,32));

			DataBase.DataBase lplpl = new DataBase.DataBase();
			
			lplpl.lllll(1);
			

			Stopwatch stopWatch = new Stopwatch();
			BlackSchooles.BlackScchooles lll = new BlackSchooles.BlackScchooles();
			ulong Rst_F;
			
			stopWatch.Start();
			Rst_F = lll.Factorial(60);
			stopWatch.Stop(); 
			TimeSpan ts = stopWatch.Elapsed;

			// Format and display the TimeSpan value.
			string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}, {4:000000000000000}",
				ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10,
				ts.TotalMilliseconds);

			Console.WriteLine("Factorial " + elapsedTime + "\t Rst " + Rst_F);

			stopWatch.Start();
			Rst_F = lll.Factorial2(60);
			stopWatch.Stop();
			ts = stopWatch.Elapsed;

			// Format and display the TimeSpan value.
			elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
				ts.Hours, ts.Minutes, ts.Seconds,
				ts.Milliseconds / 10);
			Console.WriteLine("Factorial 2" + elapsedTime + "\t Rst " + Rst_F);

			////////////// 77777777777

			stopWatch.Start();
			Rst_F = lll.Factorial3(60);
			stopWatch.Stop();
			ts = stopWatch.Elapsed;

			// Format and display the TimeSpan value.
			elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
				ts.Hours, ts.Minutes, ts.Seconds,
				ts.Milliseconds / 10);
			Console.WriteLine("Factorial 3" + elapsedTime + "\t Rst " + Rst_F);

			

		}
	}
}


public static class Globals
{
	public const Int32 BUFFER_SIZE = 512; // Unmodifiable
	public static String FILE_NAME = "Output.txt"; // Modifiable
	public static readonly String CODE_PREFIX = "US-"; // Unmodifiable
	public static OleDbConnection conn;

}


namespace BlackSchooles
{

    public class BlackScchooles:IOption
    {
		void IOption.SampleMethod()
		{
			Console.WriteLine("DTES");
		}

		public double BlackScholes(String CallPutFlag, double S, double X,
			double T, double r, double v)
		{
			double d1 = 0.0;
			double d2 = 0.0;
			double dBlackScholes = 0.0;

			d1 = (Math.Log(S / X) + (r + v * v / 2.0) * T) / (v * Math.Sqrt(T));
			d2 = d1 - v * Math.Sqrt(T);
			if (CallPutFlag.ToLower() == "c")
			{
				dBlackScholes = S * CND(d1) - X * Math.Exp(-r * T) * CND(d2);
			}
			else if (CallPutFlag.ToLower() == "p")
			{
				dBlackScholes = X * Math.Exp(-r * T) * CND(-d2) - S * CND(-d1);
			}
			return dBlackScholes;
		}

		public double BlackScholes(String CallPutFlag, double S, double X,
			double T, double r, double v,Int16 Num)
		{
			double d1 = 0.0;
			double d2 = 0.0;
			double dBlackScholes = 0.0;

			d1 = (Math.Log(S / X) + (r + v * v / 2.0) * T) / (v * Math.Sqrt(T));
			d2 = d1 - v * Math.Sqrt(T);
			if (CallPutFlag.ToLower() == "c")
			{
				dBlackScholes = S * CND(d1) - X * Math.Exp(-r * T) * CND(d2);
			}
			else if (CallPutFlag.ToLower() == "p")
			{
				dBlackScholes = X * Math.Exp(-r * T) * CND(-d2) - S * CND(-d1);
			}
			return dBlackScholes;
		}

		/*
		private double BinomialNodeValue(int m, int n, double p)
		{
			return BinomialCoefficient(m, n) * Math.Pow(p,
				   (double)m) * Math.Pow(1.0 - p, (double)(n - m));

		}
		private double BinomialCoefficient(int m, int n)
		{
			return Factorial(n) / (Factorial(m) * Factorial(n - m));
		}
		*/
		
		public ulong Factorial(int n)
		{
			ulong d = 1;
			for (Int64 j = 1; j <= n; j++)
			{
                d *= (ulong)j;
			}
			return d;
		}

		public ulong Factorial2(ulong n)
		{
			ulong rst;
			if (n == 0)
			{
				rst = 1;
			}
			else
			{
				rst = n * Factorial2(n - 1);
			}

			return rst;
		}

		public ulong Factorial3(ulong n)
		{
			ulong rst;
			rst = (n == 0) ? rst = 1 : rst = n * Factorial3(n - 1);
			return rst;
		}





		public double CND(double X)
		{
			double L = 0.0;
			double K = 0.0;
			double dCND = 0.0;
			const double a1 = 0.31938153;
			const double a2 = -0.356563782;
			const double a3 = 1.781477937;
			const double a4 = -1.821255978;
			const double a5 = 1.330274429;
			L = Math.Abs(X);
			K = 1.0 / (1.0 + 0.2316419 * L);
			dCND = 1.0 - 1.0 / Math.Sqrt(2 * Convert.ToDouble(Math.PI.ToString())) *
				Math.Exp(-L * L / 2.0) * (a1 * K + a2 * K * K + a3 * Math.Pow(K, 3.0) +
				a4 * Math.Pow(K, 4.0) + a5 * Math.Pow(K, 5.0));

			if (X < 0)
			{
				return 1.0 - dCND;
			}
			else
			{
				return dCND;
			}
		}

	}
}