using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCA1
{
    class Station : Line
    {
        private String stationnum;
        public Station() //default constructor
        {

        }

        public Station(String LineName, String StationName, String stnnum) : base(LineName, StationName)//station object constructor
        {
            this.stationnum = stnnum;
        }

        public String Num//StationNum property
        {
            get { return this.stationnum; }
            set { this.stationnum = value; }
        }

        public override String printFullInfo()//override method
        {
            return "[" + Num + "] " + base.getName();
        }
    }
}
