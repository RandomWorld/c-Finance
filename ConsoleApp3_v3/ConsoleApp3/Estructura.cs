using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Estructura
{
    public class SingleQuote2
    {
        public double Price_;
        public string Ticker_;
        public string Isin_;
        public string Date_;
        public string Current_Date_;
        public DataTable Dividendo_;
    }
    public struct SingleQuote
    {
        public double Price_;
     //   public string Ticker_;
        public string Isin_;
        public string Date_;
        public string Current_Date_;
        public long Id_;
     //   public DataTable Dividendo_;
        public SingleQuote(double Price_, string Isin_,string Date_, string Current_Date_,long Id_=-666)
        {
            this.Price_ = Price_;
            this.Isin_ = Isin_;
            this.Date_ = Date_;
            this.Current_Date_ = Current_Date_;
            this.Id_ = Id_;
        }
    }

    public class Quote
    {
        private long pID = -666;
        private String pISIN ;
        private String pCUSIP;
        private Double pPrice= -666;
        private Double pPrevPrice = -666;
        private DateTime pCurrentDate = DateTime.Now; //DateTime(1006, 6, 6);
        private DateTime pDate;
        private DataTable pDividendo;
        private Double pVolume;
        private String pTicker ;
        private String pCcy; 

        public long ID
        {
            get => pID;
            set
            {
                pID = value;
            }
        }
        public String Ticker
        {
            get => pTicker;
            set {
                pTicker = value;
            }
        }
        public DataTable Dividendo
        {
            get => pDividendo;
            set { pDividendo = (DataTable)value; }
        }
        public String CUSIP
        {
            get => pCUSIP;
            set { pCUSIP = (String)value; }
        }
        public String ISIN {
            get => pISIN;
            set { pISIN = (String) value;            }
        }
        public Double Price
        {
            get => pPrice;
            set { pPrice = value; }
        }
        public Double PrevPrice
        {
            get => pPrevPrice;
            set { pPrevPrice = value; }
        }
        public Double Volume
        {
            get => pVolume;
            set { pVolume = value; }
        }
        public DateTime Date
        {
            get => pDate;
            set { pDate = value;  }
        }
        public DateTime CurrentDate
        {
            get => pCurrentDate;
            set { pCurrentDate = value; }
        }
        public String Ccy
        {
            get => pCcy;
            set
            {
                pCcy = value;
            }
        }
        public override string ToString() => $"({Date}, {ISIN})";
    }

    public class Future_Quote : Quote
    {
        private Double pFuture = -666;
        private DateTime pFuture_Maturity ;
        public Double Future
        {
            get => pFuture;
            set { pFuture = value; }
        }
        public DateTime Future_Maturity
        {
            get => pFuture_Maturity;
            set { pFuture_Maturity = value; }
        }
        public double ImpliedRepo()
        {
            double rst;
            //if (this.Date ==null) { this.Date = (String) DateTime.Now; }
            var diffofDates =  (this.Future_Maturity - this.Date);
            rst = Math.Log(this.Future / this.Price) / (diffofDates.Days / 365.25);

            return rst;
        }
    }
}
