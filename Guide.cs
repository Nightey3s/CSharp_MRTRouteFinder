using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;

namespace WpfCA1
{
    class Guide
    {
        /*Create lists for each line*/
        List<Line> EW = new List<Line>();
        List<Line> NS = new List<Line>();
        List<Line> NE = new List<Line>();
        List<Line> CC = new List<Line>();
        List<Line> DT = new List<Line>();
        List<Line> CG = new List<Line>();
        List<Line> BP = new List<Line>();
        
        List<Line> transStations = new List<Line>();//used for complex routes

        /*Read the mrt files*/
       public void setList()
        {
            DB init = new DB();
            EW = init.queryDB("East West Line");
            NS = init.queryDB("North South Line");
            NE = init.queryDB("North East Line");
            CC = init.queryDB("Circle Line");
            DT = init.queryDB("Downtown Line");
            CG = init.queryDB("Changi Line");
            BP = init.queryDB("Bukit Panjang Line");

        }
        public void initialiseList()
        {
            DB init = new DB();
            init.initialiseDB();

            //string[] EWLine = File.ReadAllLines("../../MRT_Lines/EastWestLine.txt");
            //for (int num = 0, name = 1; name < EWLine.Length; num += 2, name += 2)
            //{
            //    EW.Add(new Station("East West Line", EWLine[name], EWLine[num]));
            //}
            EW = init.queryDB("East West Line");

            //string[] NSLine = File.ReadAllLines("../../MRT_Lines/NorthSouthLine.txt");
            //for (int num = 0, name = 1; name < NSLine.Length; num += 2, name += 2)
            //{
            //    NS.Add(new Station("North South Line", NSLine[name], NSLine[num]));
            //}
            NS = init.queryDB("North South Line");

            //string[] NELine = File.ReadAllLines("../../MRT_Lines/NorthEastLine.txt");
            //for (int num = 0, name = 1; name < NELine.Length; num += 2, name += 2)
            //{
            //    NE.Add(new Station("North East Line", NELine[name], NELine[num]));
            //}
            NE = init.queryDB("North East Line");

            //string[] CCLine = File.ReadAllLines("../../MRT_Lines/CircleLine.txt");
            //for (int num = 0, name = 1; name < CCLine.Length; num += 2, name += 2)
            //{
            //    CC.Add(new Station("Circle Line", CCLine[name], CCLine[num]));
            //}
            CC = init.queryDB("Circle Line");

            //string[] DTLine = File.ReadAllLines("../../MRT_Lines/DowntownLine.txt");
            //for (int num = 0, name = 1; name < DTLine.Length; num += 2, name += 2)
            //{
            //    DT.Add(new Station("Downtown Line", DTLine[name], DTLine[num]));
            //}
            DT = init.queryDB("Downtown Line");

            //string[] CGLine = File.ReadAllLines("../../MRT_Lines/ChangiLine.txt");
            //for (int num = 0, name = 1; name < CGLine.Length; num += 2, name += 2)
            //{
            //    CG.Add(new Station("Changi Line", CGLine[name], CGLine[num]));
            //}
            CG = init.queryDB("Changi Line");

            //string[] BPLine = File.ReadAllLines("../../MRT_Lines/BukitPanjangLine.txt");
            //for (int num = 0, name = 1; name < BPLine.Length; num += 2, name += 2)
            //{
            //    BP.Add(new Station("Bukit Panjang Line", BPLine[name], BPLine[num]));
            //}
            BP = init.queryDB("Bukit Panjang Line");

            //string[] transLine = File.ReadAllLines("../../MRT_Lines/transit.txt");//do not need to appear in options
            //for (int num = 0, name = 1; name < transLine.Length; num += 2, name += 2)
            //{
            //    transStations.Add(new Station("Transit Stations", transLine[name], transLine[num]));
            //}
            //CG = init.queryDB("Transit Stations");

        }

        public List<Line> ew
        {
            get { return this.EW; }
        }

        public List<Line> ns
        {
            get { return this.NS; }
        }

        public List<Line> ne
        {
            get { return this.NE; }
        }

        public List<Line> cc
        {
            get { return this.CC; }
        }

        public List<Line> dt
        {
            get { return this.DT; }
        }
        public List<Line> cg
        {
            get { return this.CG; }
        }
        public List<Line> bp
        {
            get { return this.BP; }
        }

        /*add stations into combo box*/
        public String cmbbox()
        {
            String cmb = "";
            foreach (Station x in EW)
            {
                cmb += x.printFullInfo() + "\n";
            }
            foreach (Station x in NS)
            {
                cmb += "[" + x.Num + "] " + x.getName() + "\n";
            }
            foreach (Station x in NE)
            {
                cmb += "[" + x.Num + "] " + x.getName() + "\n";

            }
            foreach (Station x in CC)
            {
                cmb += "[" + x.Num + "] " + x.getName() + "\n";
            }
            foreach (Station x in DT)
            {
                cmb += "[" + x.Num + "] " + x.getName() + "\n";
            }
            foreach (Station x in CG)
            {
                cmb += "[" + x.Num + "] " + x.getName() + "\n";
            }
            foreach (Station x in BP)
            {
                cmb += "[" + x.Num + "] " + x.getName() + "\n";
            }
            return cmb;
        }

        /*counts for arrows*/
        public int counting(String station)
        {
            String startstationcode = station.Substring(1, 2);//gets the Line Code
            int trimstartnum = station.IndexOf("]") + 2;
            String startstation = station.Substring(trimstartnum);//trim the line code
            int index = 0;
            switch (startstationcode)
            {
                case "EW":
                    foreach (Station x in EW)
                    {
                        if (startstation.Contains(x.getName()))
                            index = EW.IndexOf(x);
                    }
                    break;

                case "NS":
                    foreach (Station x in NS)
                    {
                        if (startstation.Contains(x.getName()))
                            index = NS.IndexOf(x);
                    }
                    break;

                case "NE":
                    foreach (Station x in NE)
                    {
                        if (startstation.Contains(x.getName()))
                            index = NE.IndexOf(x);
                    }
                    break;

                case "CC":
                    foreach (Station x in CC)
                    {
                        if (startstation.Contains(x.getName()))
                            index = CC.IndexOf(x);
                    }
                    break;

                case "DT":
                    foreach (Station x in DT)
                    {
                        if (startstation.Contains(x.getName()))
                            index = DT.IndexOf(x);
                    }
                    break;
                case "BP":
                    foreach (Station x in BP)
                    {
                        if (startstation.Contains(x.getName()))
                            index = BP.IndexOf(x);
                    }
                    break;
            }
            return index;
        }

        /*Used when no transfers are needed(ie when start line and end line are the same)*/
        public String computeEZRoute(String startStn, String endStn)
        {
            setList();

            String startstationcode = startStn.Substring(1, 2), endstationcode = endStn.Substring(1, 2);//gets the Line Code
            int trimstartnum = startStn.IndexOf("]") + 2, trimendnum = endStn.IndexOf("]") + 2;
            String startstation = startStn.Substring(trimstartnum), endstation = endStn.Substring(trimendnum);//trim the line code
            int numofstations = 0, startstationindex = 0, endstationindex = 0;

            /*count number of stations*/
            switch (startstationcode)
            {
                case "EW":
                    foreach (Station i in EW)
                    {
                        if (startstation.Contains(i.getName()))
                            startstationindex = EW.IndexOf(i);

                        if (endstation.Contains(i.getName()))
                            endstationindex = EW.IndexOf(i);
                    }
                    numofstations = endstationindex - startstationindex;
                    break;

                case "NS":
                    foreach (Station i in NS)
                    {
                        if (startstation.Equals(i.getName()))
                            startstationindex = NS.IndexOf(i);

                        if (endstation.Equals(i.getName()))
                            endstationindex = NS.IndexOf(i);
                    }
                    numofstations = endstationindex - startstationindex;
                    break;

                case "NE":
                    foreach (Station i in NE)
                    {
                        if (startstation.Equals(i.getName()))
                            startstationindex = NE.IndexOf(i);

                        if (endstation.Equals(i.getName()))
                            endstationindex = NE.IndexOf(i);
                    }
                    numofstations = endstationindex - startstationindex;
                    break;

                case "CC":
                    foreach (Station i in CC)
                    {
                        if (startstation.Equals(i.getName()))
                            startstationindex = CC.IndexOf(i);

                        if (endstation.Equals(i.getName()))
                            endstationindex = CC.IndexOf(i);
                    }
                    numofstations = endstationindex - startstationindex;
                    break;

                case "DT":
                    foreach (Station i in DT)
                    {
                        if (startstation.Equals(i.getName()))
                            startstationindex = DT.IndexOf(i);

                        if (endstation.Equals(i.getName()))
                            endstationindex = DT.IndexOf(i);
                    }
                    numofstations = endstationindex - startstationindex;
                    break;

                case "CG":
                    foreach (Station i in CG)
                    {
                        if (startstation.Equals(i.getName()))
                            startstationindex = CG.IndexOf(i);

                        if (endstation.Equals(i.getName()))
                            endstationindex = CG.IndexOf(i);
                    }
                    numofstations = endstationindex - startstationindex;
                    break;

                case "BP":
                    foreach (Station i in BP)
                    {
                        if (startstation.Equals(i.getName()))
                            startstationindex = BP.IndexOf(i);

                        if (endstation.Equals(i.getName()))
                            endstationindex = BP.IndexOf(i);
                    }
                    numofstations = endstationindex - startstationindex;
                    break;
            }

            return "\nTake from: " + startStn + " \nAlight at: " + endStn + " \nTotal Number of Stations: " + Math.Abs(numofstations) +'\n';

        }

        /*Used when one transfer is needed*/
        public String computeOneTransferRoute(String startStn, String endStn)
        {
            setList();
            String startstationcode = startStn.Substring(1, 2), endstationcode = endStn.Substring(1, 2);//gets the Line Code
            int trimstartnum = startStn.IndexOf("]") + 2, trimendnum = endStn.IndexOf("]") + 2;
            String startstation = startStn.Substring(trimstartnum), endstation = endStn.Substring(trimendnum);//trim the line code
            //int numofstations = 0, startstationindex = 0, endstationindex = 0;
            String transferStation = "", possibleTransfers = "", numoftransferandstops = "";
            List<String> transferList = new List<String>();
            Line pR = new Line();
            
            switch (startstationcode)
            {
                case "EW":
                    switch (endstationcode)
                    {
                        case "NS":
                            possibleTransfers = pR.searchList(NS, EW).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, EW, NS);
                            
                            break;

                        case "NE":
                            possibleTransfers = pR.searchList(NE, EW).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, EW, NE);
                            break;

                        case "CC":
                            possibleTransfers = pR.searchList(CC, EW).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, EW, CC);
                            break;

                        case "DT":
                            possibleTransfers = pR.searchList(DT, EW).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, EW, DT);
                            break;

                        case "CG":
                            possibleTransfers = pR.searchList(CG, EW).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, EW, CG);
                            break;

                        case "BP":
                            possibleTransfers = pR.searchList(BP, EW).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, EW, BP);
                            break;
                    }
                    break;

                case "NS":
                    switch (endstationcode)
                    {
                        case "EW":
                            possibleTransfers = pR.searchList(EW, NS).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, NS, EW);
                            break;

                        case "NE":
                            possibleTransfers = pR.searchList(NE, NS).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, NS, NE);
                            break;

                        case "CC":
                            possibleTransfers = pR.searchList(CC, NS).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, NS, CC);
                            break;

                        case "DT":
                            possibleTransfers = pR.searchList(DT, NS).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, NS, DT);
                            break;

                        case "CG":
                            possibleTransfers = pR.searchList(CG, NS).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, NS, CG);
                            break;

                        case "BP":
                            possibleTransfers = pR.searchList(BP, NS).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, NS, BP);
                            break;
                    }
                    break;

                case "NE":
                    switch (endstationcode)
                    {
                        case "EW":
                            possibleTransfers = pR.searchList(EW, NE).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, NE, EW);
                            break;

                        case "NS":
                            possibleTransfers = pR.searchList(NS, NE).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, NE, NS);
                            break;

                        case "CC":
                            possibleTransfers = pR.searchList(CC, NE).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, NE, CC);
                            break;

                        case "DT":
                            possibleTransfers = pR.searchList(DT, NE).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, NE, DT);
                            break;

                        case "CG":
                            possibleTransfers = pR.searchList(CG, NE).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, NE, CG);
                            break;

                        case "BP":
                            possibleTransfers = pR.searchList(BP, NE).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, NE, BP);
                            break;
                    }
                    break;

                case "CC":
                    switch (endstationcode)
                    {
                        case "EW":
                            possibleTransfers = pR.searchList(EW, CC).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, CC, EW);
                            break;

                        case "NS":
                            possibleTransfers = pR.searchList(NS, CC).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, CC, NS);
                            break;

                        case "NE":
                            possibleTransfers = pR.searchList(NE, CC).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, CC, NE);
                            break;

                        case "DT":
                            possibleTransfers = pR.searchList(DT, CC).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, CC, DT);
                            break;

                        case "CG":
                            possibleTransfers = pR.searchList(CG, CC).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, CC, CG);
                            break;

                        case "BP":
                            possibleTransfers = pR.searchList(BP, CC).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, CC, BP);
                            break;
                    }
                    break;

                case "DT":
                    switch (endstationcode)
                    {
                        case "EW":
                            possibleTransfers = pR.searchList(EW, DT).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, DT, EW);
                            break;

                        case "NS":
                            possibleTransfers = pR.searchList(NS, DT).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, DT, NS);
                            break;

                        case "NE":
                            possibleTransfers = pR.searchList(NE, DT).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, DT, NE);
                            break;

                        case "CC":
                            possibleTransfers = pR.searchList(CC, DT).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, DT, CC);
                            break;

                        case "CG":
                            possibleTransfers = pR.searchList(CG, DT).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, DT, CG);
                            break;

                        case "BP":
                            possibleTransfers = pR.searchList(BP, DT).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, DT, BP);
                            break;
                    }
                    break;

                case "CG":
                    switch (endstationcode)
                    {
                        case "EW":
                            possibleTransfers = pR.searchList(EW, CG).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, BP, EW);
                            break;

                        case "NS":
                            possibleTransfers = pR.searchList(NS, CG).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, BP, NS);
                            break;

                        case "NE":
                            possibleTransfers = pR.searchList(NE, CG).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, BP, NE);
                            break;

                        case "CC":
                            possibleTransfers = pR.searchList(CC, CG).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, BP, CC);
                            break;

                        case "DT":
                            possibleTransfers = pR.searchList(DT, CG).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, BP, DT);
                            break;

                        case "BP":
                            possibleTransfers = pR.searchList(BP, CG).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, BP, DT);
                            break;

                    }
                    break;

                case "BP":
                    switch (endstationcode)
                    {
                        case "EW":
                            possibleTransfers = pR.searchList(EW, BP).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, BP, EW);
                            break;

                        case "NS":
                            possibleTransfers = pR.searchList(NS, BP).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, BP, NS);
                            break;

                        case "NE":
                            possibleTransfers = pR.searchList(NE, BP).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, BP, NE);
                            break;

                        case "CC":
                            possibleTransfers = pR.searchList(CC, BP).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, BP, CC);
                            break;

                        case "DT":
                            possibleTransfers = pR.searchList(DT, BP).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, BP, DT);
                            break;

                        case "CG":
                            possibleTransfers = pR.searchList(BP, BP).TrimEnd('\n');
                            transferList = possibleTransfers.Split('\n').ToList();
                            numoftransferandstops = pR.countStationsfromtransfer(startStn, endStn, transferList, BP, CG);
                            break;
                    }
                    break;
            }
            if (String.IsNullOrWhiteSpace(possibleTransfers))
            {
                return "\n\nCannot find transfer";
            }
            else
                return transferStation + "\n\n" + numoftransferandstops;
        }
    }
}
