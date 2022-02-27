using System;
using HtmlAgilityPack;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Data;
using System.Collections.Generic;


public class Programa_Scrapper
{
	private string webAddress;
	private HtmlWeb web = new HtmlWeb(); 
	private HtmlDocument doc;
	private HtmlNodeCollection nodes;
	private HtmlAttribute classAttribute;
	private HtmlNode table;

	public List<Estructura.Quote> FT_MultipleQuotes(DataTable data)
	{
		List<Estructura.Quote> rst= new List<Estructura.Quote>();
		Estructura.Quote rst_1 = new Estructura.Quote();

		foreach (System.Data.DataRow elem in data.Rows)
		{
			int id_ = (int)elem[0];
			string ISIN_ = (string)elem[1];
			string Ccy_ = (string)elem[2];
			rst_1 = FT_SingleQuote(ISIN_, Ccy_,id_);
			rst.Add(rst_1);
		}
		return rst;
	}
	public Estructura.Quote FT_SingleQuote(string ISIN, string Ccy = "EUR",long ID=-666)
    {
		Estructura.Quote rst = new Estructura.Quote();
		rst.ISIN = ISIN;
		rst.ID = ID;
		//rst.= DateTime.Now.ToString();
		rst.Price = 0;
		rst.PrevPrice = 0;
		rst.CurrentDate = DateTime.Now;

		NumberFormatInfo provider = new NumberFormatInfo();
		provider.NumberDecimalSeparator = ".";
		provider.NumberGroupSeparator = ",";

		webAddress = "https://markets.ft.com/data/funds/tearsheet/summary?s=" + ISIN + ":" + Ccy;
		

		try
		{
			doc = web.Load(webAddress);
			/*
			nodes = doc.DocumentNode.SelectNodes("/html/body/div[3]/div[2]/section[1]/div/div/div[1]/div[2]/ul/li[1]/span[2]");
			//double price_ = nodes!=null ? Convert.ToDouble(nodes[0].InnerHtml, provider): -666;
			nodes = doc.DocumentNode.SelectNodes("/html/body/div[3]/div[2]/section[1]/div/div/div[1]/div[2]/ul/li[2]/span[2]/span");
			string variation = nodes != null ? nodes[0].InnerText : "node =null";
			nodes = doc.DocumentNode.SelectNodes("/html/body/div[3]/div[2]/section[1]/div/div/div[1]/div[2]");
			//mod-ui-data-list__value
			*/
			nodes = doc.DocumentNode.SelectNodes("//*[contains(concat( ' ', @class, ' ' ), concat( ' ', 'mod-ui-data-list__value', ' ' ))]");
			if (nodes != null)
			{
				decimal price_ = Convert.ToDecimal(nodes[0].InnerText, provider);
				decimal PrevPrice =  price_ - Convert.ToDecimal(nodes[1].InnerText.Split('/')[0]);

				nodes = doc.DocumentNode.SelectNodes("//*[contains(concat( ' ', @class, ' ' ), concat( ' ', 'mod-disclaimer', ' ' ))]");
				String date_ = nodes[0].InnerText;
				string[] subs = date_.Split(' ');
				date_ = subs[subs.Length - 2] + "-" + subs[subs.Length - 3] + "-" + subs[subs.Length - 1].Remove(subs[subs.Length - 1].Length - 1 );
				string rr = GenericFunctions.GenericFunctions.GetMonthNumberFromAbbreviation("Feb");
				int year = int.Parse(subs[subs.Length - 1].Remove(subs[subs.Length - 1].Length - 1));
				int month = int.Parse(GenericFunctions.GenericFunctions.GetMonthNumberFromAbbreviation(subs[subs.Length - 3]));
				int day = int.Parse(subs[subs.Length - 2]);

				DateTime kkk = new DateTime(year,month,day);


				//date_ = date_.Substring(0, date_.Length - 1);
				rst.Date = new DateTime(year, month, day);
				rst.Price = (double)price_;
				rst.PrevPrice = (double)PrevPrice;
			}
			/*if (nodes != null) { 
				String date_ = nodes[0].InnerHtml;
				string[] subs = date_.Split(' ');
				date_ = subs[subs.Length - 2] + "-" + subs[subs.Length - 3] + "-" + subs[subs.Length - 1];
				date_ = date_.Substring(0, date_.Length - 1);
				rst.Date_ = date_;
			}*/
		}
        catch (NullReferenceException e)
        {
			rst.Price = -666;
        }
		return rst;
	}
/*	public Estructura.Quote Schroder_SingleQuote(string webpage_)
	{
		Estructura.Quote rst;
		rst.ISIN = "dsadsadsa";
		rst.ID = -666;
		rst.CurrentDate = DateTime.Now.ToString();

		NumberFormatInfo provider = new NumberFormatInfo();
		provider.NumberDecimalSeparator = ".";
		provider.NumberGroupSeparator = ",";

		webAddress = webpage_;
		doc = web.Load(webAddress);

		nodes = doc.DocumentNode.SelectNodes("/html/body/div[3]/div/div[2]/div/div[1]/div[2]/div/div[2]/div/div/div/amid-layout/div/div/div/div/div[1]/div/fx-profile-hero-panel/div/div[2]/div/div/div[2]/div/div/div/p/span");
		double price_ = Convert.ToDouble(nodes[0].InnerHtml, provider);
		rst.Price_ = price_;

		nodes = doc.DocumentNode.SelectNodes("/html/body/div[3]/div[2]/section[1]/div/div/div[1]/div[2]/ul/li[2]/span[2]/span");
		string variation = nodes[0].InnerText;

		nodes = doc.DocumentNode.SelectNodes("/html/body/div[3]/div[2]/section[1]/div/div/div[1]/div[2]/div");
		String date_ = nodes[0].InnerHtml;
		string[] subs = date_.Split(' ');

		date_ = subs[subs.Length - 2] + "-" + subs[subs.Length - 3] + "-" + subs[subs.Length - 1];
		date_ = date_.Substring(0, date_.Length - 1);
		rst.Date_ = date_;
		
		

		return rst;
	}
*/
	public Estructura.Quote Reuters(string Ticker) {
		Estructura.Quote rst = new Estructura.Quote();

		rst.Ticker = Ticker;
		rst.ID = -666;
		rst.CurrentDate = DateTime.Now;

		NumberFormatInfo provider = new NumberFormatInfo();
		provider.NumberDecimalSeparator = ".";
		provider.NumberGroupSeparator = ",";

		String webAddress = "https://www.reuters.com/companies/" + Ticker;

		doc = web.Load(webAddress);

		nodes = doc.DocumentNode.SelectNodes("//*[contains(concat( ' ', @class, ' ' ), concat( ' ', 'QuoteRibbon-digits-30Sds', ' ' ))]");
		Decimal price_ = Convert.ToDecimal(nodes[0].InnerHtml, provider);
		rst.Price = (Double)price_;

		nodes = doc.DocumentNode.SelectNodes("//*[contains(concat( ' ', @class, ' ' ), concat( ' ', 'QuoteRibbon-volume-22vsO', ' ' ))]");
		double Volume = Convert.ToDouble(nodes[0].InnerText.Substring(6), provider);
		rst.Volume = Volume;

		nodes = doc.DocumentNode.SelectNodes("//*[contains(concat( ' ', @class, ' ' ), concat( ' ', 'QuoteRibbon-change-1aQtL', ' ' ))]");
		var dd = nodes[0].InnerText.Substring(6).Split('(');

		Decimal PrevPrice = Convert.ToDecimal(dd[0], provider)+ price_;
		rst.PrevPrice = (Double) PrevPrice;


		/*
		 nodes = doc.DocumentNode.SelectNodes("/html/body/div[3]/div[2]/section[1]/div/div/div[1]/div[2]/div");
		String date_ = nodes[0].InnerHtml;
		string[] subs = date_.Split(' ');
		

		date_ = subs[subs.Length - 2] + "-" + subs[subs.Length - 3] + "-" + subs[subs.Length - 1];
		date_ = date_.Substring(0, date_.Length - 1);
		*/
		return rst;
	}
	public Estructura.Quote Yahoo(String ticker)
	{
		Estructura.Quote rst = new Estructura.Quote();

		NumberFormatInfo provider = new NumberFormatInfo();
		provider.NumberDecimalSeparator = ".";
		provider.NumberGroupSeparator = ",";

		String webAddress = "https://finance.yahoo.com/quote/" + ticker;
		doc = web.Load(webAddress);
		try
		{
			nodes = doc.DocumentNode.SelectNodes("//*[@id='quote-header-info']/div[3]/div[1]/div/fin-streamer[1]");	

			double price_ = nodes != null ? Convert.ToDouble(nodes[0].InnerHtml, provider) : -666;

			var pp = doc.DocumentNode.SelectNodes("//*[@id='quote-summary']/div[1]/table/tbody/tr[7]/td[2]/fin-streamer");
			double volumen = nodes != null ? Convert.ToDouble(nodes[0].InnerHtml, provider) : -666;

			
		}
		catch (NullReferenceException e)
		{
			rst.Price = -666;
		}


		rst.ISIN= "sadsadsa";
		rst.Price = 120.0;
		rst.Date = DateTime.Now;
	//	rst.CurrentDate = DateTime.Now.ToString();
		rst.ID = -666;

		return rst;


		
		}
	public Estructura.Quote primi()
	{
		Estructura.Quote rst = new Estructura.Quote();

		string webAddress;
		HtmlWeb web = new HtmlWeb(); ;
		HtmlDocument doc; 
		HtmlNodeCollection nodes;
		HtmlAttribute classAttribute;

		NumberFormatInfo provider = new NumberFormatInfo();
		provider.NumberDecimalSeparator = ".";
		provider.NumberGroupSeparator = ",";

		//webAddress = "https://markets.ft.com/data/funds/tearsheet/summary?s=LU1529955046:EUR";

		string ISIN = "LU1529955046";
		webAddress = "https://markets.ft.com/data/funds/tearsheet/summary?s=" + ISIN + ":EUR";

		doc = web.Load(webAddress);

		//nodes = doc.DocumentNode.SelectNodes("//article/h3/a|//article/div[2]/p[1]");
		nodes = doc.DocumentNode.SelectNodes("/html/body/div[3]/div[2]/section[1]/div/div/div[1]/div[2]/ul/li[1]/span[2]");
		double price_ = Convert.ToDouble(nodes[0].InnerHtml, provider);
		
		//Console.WriteLine("Short title: " + (Double) nodes[0].InnerHtml);
		//Console.WriteLine("Short title: " + price * 3 );

		nodes = doc.DocumentNode.SelectNodes("/html/body/div[3]/div[2]/section[1]/div/div/div[1]/div[2]/ul/li[2]/span[2]/span");
		string variation = nodes[0].InnerText;

		//Console.WriteLine("Short title: " + nodes[0].InnerText);

		nodes = doc.DocumentNode.SelectNodes("/html/body/div[3]/div[2]/section[1]/div/div/div[1]/div[2]/div");
		String date_ = nodes[0].InnerHtml;
		string[] subs = date_.Split(' ');

		date_ = subs[subs.Length - 2] + "-" + subs[subs.Length - 3] + "-" + subs[subs.Length - 1] ;
		date_ = date_.Substring(0, date_.Length - 1);

		Console.WriteLine("Short title: " + nodes[0].InnerHtml);

		rst.ISIN = ISIN;
		rst.Price = price_;
		//rst.Date = date_;
		//rst.CurrentDate = DateTime.Now();
		rst.ID = -666;

		return rst;


		doc = web.Load("https://www.robeco.com/en/funds/prof-glob-en-11/robeco-asia-pacific-equities-d-eur-lu0084617165.html");
		nodes = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[4]/div/div/div/div[2]/span[2]");
		
		Console.WriteLine("Short title: " + nodes[0].InnerText);

		foreach (HtmlNode node in nodes)
		{
			Console.WriteLine("Short title: " + node.InnerText);
		//	Console.WriteLine("Full title: " + node.Value);

	/*		if (node.Name == "a")
			{
				classAttribute = node.Attributes[1];

				Console.WriteLine("Short title: " + node.InnerText);
				Console.WriteLine("Full title: " + classAttribute.Value);

			}
			else
			{
				Console.WriteLine("Price: " + node.InnerText.Remove(0, 2));
				Console.WriteLine();
			}
	*/

		}
	}
}