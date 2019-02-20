using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCA1
{
    class Line
    {
        private String LineName, StationName;


        public Line()//default constructor
        {

        }

        public Line(String line, String name)//overloaded constructor
        {
            this.LineName = line;
            this.StationName = name;
        }

        public String LN//LineName property
        {
            get { return this.LineName; }
            set { this.LineName = value; }
        }

        public String SN//StationName property
        {
            get { return this.StationName; }
            set { this.StationName = value; }
        }

        public String getInfo()//prints station and Line
        {
            return "\nLine: " + LN + "\nStation: " + this.SN;
        }

        public virtual String printFullInfo()//override method
        {
            return "";
        }

        public String getName()//prints station name
        {
            return SN;
        }

        /*Searches through the lists of stations*/
        public String searchList(List<Line> i, List<Line> j)
        {
            String transStation = "";
            foreach (Station x in i)
            {
                String stationini = x.getName();
                foreach (Station y in j)
                {
                    String stationinj = y.getName();
                    if (stationini.Equals(stationinj))
                    {
                        transStation += x.getName() + "\n";
                    }
                }
            }
            return transStation;
        }//end of searchList

        /*Counts number of stations*/
        public String countStationsfromtransfer(String startStation, String endStation, List<String> translist, List<Line> i, List<Line> j)
        {
            int startstation = 0, transstation1 = 0, transstation2 = 0, endstation = 0, distance = 0, distance1 = 1000, xdistance = 0, xdistance1 = 1000, ydistance = 0, ydistance1 = 100, count = 0;
            String transferStn = "", transfer = "", test = "", choice = "";

            String startstationcode = startStation.Substring(1, 2), endstationcode = endStation.Substring(1, 2);//gets the Line Code
            //for(int a=0; a < translist.Count; a++)
            List<int> optionchoice = new List<int>();
            foreach (String z in translist)
            {
                ++count;
                transfer = z;
                foreach (Station x in i)
                {
                    if (startStation.Contains(x.getName()))
                        startstation = i.IndexOf(x);

                    if (transfer.Contains(x.getName()))
                        transstation1 = i.IndexOf(x);

                    xdistance = Math.Abs(transstation1 - startstation);
                    if (xdistance < xdistance1)
                        xdistance1 = xdistance;

                }
                foreach (Station y in j)//count from transfer station to end station
                {
                    if (transfer.Contains(y.getName()))
                        transstation2 = j.IndexOf(y);

                    if (endStation.Contains(y.getName()))
                        endstation = j.IndexOf(y);
                    ydistance = Math.Abs(endstation - transstation2);

                    if (ydistance < ydistance1)
                        ydistance1 = ydistance;
                }
                distance = Math.Abs(xdistance) + Math.Abs(ydistance);

                if (distance < distance1)
                {
                    distance1 = distance;
                }
                else
                    transferStn = transfer;
                test += "Option " + count + " \nNumber of stops: " + distance + "\nTake from: " + startStation + " \nTransfer at: " +
                    " [" + startstationcode + (transstation1 + 1) + "][" + endstationcode + (transstation2 + 1) + "] " + transfer + "\nTake to: " + endStation + "\n\n";

                optionchoice.Add(distance);
            }
            choice = "You should pick Option " + (optionchoice.IndexOf(optionchoice.Min()) + 1) + " as it is the shortest route";
            test += choice;
            return test;
        }
    }
}
