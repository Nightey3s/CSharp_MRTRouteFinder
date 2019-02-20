using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;

namespace WpfCA1
{
    class DB
    {
        /*Create list for lines */
        List<Line> EW = new List<Line>();
        List<Line> NS = new List<Line>();
        List<Line> NE = new List<Line>();
        List<Line> CC = new List<Line>();
        List<Line> DT = new List<Line>();
        List<Line> CG = new List<Line>();
        List<Line> BP = new List<Line>();

        List<Line> transStations = new List<Line>();//used for complex routes

        /*Create list for fare*/
        List<string> fares = new List<string>();

        public void initialiseDB()
        {
            initialiseFares();
            createLineTable();
            insertLineTable();
        }

        private void initialiseFares()
        {
            /*Create list for fares */
            List<string> fares = File.ReadAllLines(@"../../Fares/fares.txt").ToList();
            CreateFareTable();
            for (int count = 0; count <= fares.Count() - 1; count++)
            {
                /*Assigns parts of the text file into respective names */
                string start = fares[count].Split(' ')[0];
                string end = fares[count].Split(' ')[1];
                string money1 = fares[++count];
                string money2 = fares[++count];
                int time = int.Parse(fares[++count]);

                InsertFareTable(start, end, money1, money2, time);
            }
        }
        private void CreateFareTable()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    conn.ConnectionString =
                        "Data Source = DESKTOP-O1L6A2E;database=APPDProj;integrated security = true";
                    conn.Open();
                    comm.Connection = conn;

                    /*Creates fare table*/
                    comm.CommandText =
                        "Create TABLE Fares(StartStn VARCHAR(20) NOT NULL," +
                        " EndStn VARCHAR(20) NOT NULL, CardFare DECIMAL(5,2) NOT NULL," +
                        " TicketFare DECIMAL(5,2) NOT NULL, Time INT NOT NULL)";
                    comm.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        private void InsertFareTable(string start, string end, string money1, string money2, int time)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    conn.ConnectionString =
                        "Data Source = DESKTOP-O1L6A2E;database=APPDProj;integrated security = true";
                    conn.Open();
                    comm.Connection = conn;

                    /*Inserts values*/
                    comm.CommandText =
                        "INSERT INTO Fares VALUES(" + "'" + start + "'" + ", '" + end + "', " + money1 + ", " + money2 + ", " + time + ")";

                    comm.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
        public string getFares(string StartCode, string EndCode)
        {/*need help with this part, sql command cannot select the proper start or end stn code, it'll just choose the first match.
            for example if choose NS2 as start and EW1 as end, it'll display startstn as NS23 and endStn as EW17
            you see if you can change so that it strictly follows NS2 and EW1. After you fix this, hopefully,
            you can either do extra features or just submit it*/
            string fares = "";
            using (SqlConnection conn = new SqlConnection())
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    conn.ConnectionString =
                        "Data Source = DESKTOP-O1L6A2E;database=APPDProj;integrated security = true";
                    conn.Open();
                    comm.Connection = conn;

                    /*Inserts values*/
                    comm.CommandText =
                        "SELECT * FROM Fares WHERE "  +"("+ " StartStn LIKE " + '\u0027' + "%"+ StartCode + '\u0027'+" OR StartStn LIKE " + '\u0027' + StartCode +"%" + '\u0027' +")" + " AND " +"(" + " EndStn LIKE " + '\u0027'+"%"+ EndCode + '\u0027' + " OR EndStn LIKE " + '\u0027' + EndCode + "%" + '\u0027' +")";
                    SqlDataReader reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        fares = "\n\nFare: $" + reader["CardFare"].ToString() +"\nSingle Trip Fare: $" + reader["TicketFare"].ToString() +'\n'+"Journey Time: "+ reader["Time"].ToString() +"minutes"
                            +"Test:" + reader["StartStn"].ToString() + '\n' + reader["EndStn"].ToString();

                    }
                    reader.Close();
                }
                conn.Close();
            }
            return fares;
        }

        public void createLineTable()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    conn.ConnectionString =
                        "Data Source = DESKTOP-O1L6A2E;database=APPDProj;integrated security = true";
                    conn.Open();
                    comm.Connection = conn;
                    /* Creates DB for EWL */
                    comm.CommandText =
                        "CREATE TABLE EW(LineCode VARCHAR(40) NOT NULL, StationName VARCHAR(50) NOT NULL)";
                    comm.ExecuteNonQuery();

                    /* Creates DB for NSL */
                    comm.CommandText =
                        "CREATE TABLE NS(LineCode VARCHAR(40) NOT NULL, StationName VARCHAR(50) NOT NULL)";
                    comm.ExecuteNonQuery();

                    /* Creates DB for NEL */
                    comm.CommandText =
                        "CREATE TABLE NE(LineCode VARCHAR(40) NOT NULL, StationName VARCHAR(50) NOT NULL)";
                    comm.ExecuteNonQuery();

                    /* Creates DB for CCL */
                    comm.CommandText =
                        "CREATE TABLE CC(LineCode VARCHAR(40) NOT NULL, StationName VARCHAR(50) NOT NULL)";
                    comm.ExecuteNonQuery();

                    /* Creates DB for DTL */
                    comm.CommandText =
                        "CREATE TABLE DT(LineCode VARCHAR(40) NOT NULL, StationName VARCHAR(50) NOT NULL)";
                    comm.ExecuteNonQuery();

                    /* Creates DB for CGL */
                    comm.CommandText =
                        "CREATE TABLE CG(LineCode VARCHAR(40) NOT NULL, StationName VARCHAR(50) NOT NULL)";
                    comm.ExecuteNonQuery();

                    /* Creates DB for BPL */
                    comm.CommandText =
                        "CREATE TABLE BP(LineCode VARCHAR(40) NOT NULL, StationName VARCHAR(50) NOT NULL)";
                    comm.ExecuteNonQuery();

                    /* Creates DB for transfer stations */
                    //comm.CommandText =
                    //   "CREATE TABLE transStations(LineCode VARCHAR(40) NOT NULL, StationName VARCHAR(50) NOT NULL)";
                    //comm.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void insertLineTable()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    conn.ConnectionString =
                        "Data Source = DESKTOP-O1L6A2E;database=APPDProj;integrated security = true";
                    conn.Open();
                    comm.Connection = conn;

                    /* Insert values into EWLine */
                    string[] EWLine = File.ReadAllLines("../../MRT_Lines/EastWestLine.txt");
                    for (int num = 0, name = 1; name < EWLine.Length; num += 2, name += 2)
                    {
                        string stnNum = EWLine[num];
                        string stnName = EWLine[name];
                        comm.CommandText =
                            "INSERT INTO EW VALUES(" + "'" + stnNum + "', '" + stnName + "')";
                        comm.ExecuteNonQuery();
                    }

                    /* Insert values into NSLine */
                    string[] NSLine = File.ReadAllLines("../../MRT_Lines/NorthSouthLine.txt");
                    for (int num = 0, name = 1; name < NSLine.Length; num += 2, name += 2)
                    {
                        string stnNum = NSLine[num];
                        string stnName = NSLine[name];
                        comm.CommandText =
                            "INSERT INTO NS VALUES(" + "'" + stnNum + "', '" + stnName + "')";
                        comm.ExecuteNonQuery();
                    }

                    /* Insert values into NELine */
                    string[] NELine = File.ReadAllLines("../../MRT_Lines/NorthEastLine.txt");
                    for (int num = 0, name = 1; name < NELine.Length; num += 2, name += 2)
                    {
                        string stnNum = NELine[num];
                        string stnName = NELine[name];
                        comm.CommandText =
                            "INSERT INTO NE VALUES(" + "'" + stnNum + "', '" + stnName + "')";
                        comm.ExecuteNonQuery();
                    }

                    /* Insert values into CCLine */
                    string[] CCLine = File.ReadAllLines("../../MRT_Lines/CircleLine.txt");
                    for (int num = 0, name = 1; name < CCLine.Length; num += 2, name += 2)
                    {
                        string stnNum = CCLine[num];
                        string stnName = CCLine[name];
                        comm.CommandText =
                            "INSERT INTO CC VALUES(" + "'" + stnNum + "', '" + stnName + "')";
                        comm.ExecuteNonQuery();
                    }

                    /* Insert values into DTLine */
                    string[] DTLine = File.ReadAllLines("../../MRT_Lines/DowntownLine.txt");
                    for (int num = 0, name = 1; name < DTLine.Length; num += 2, name += 2)
                    {
                        string stnNum = DTLine[num];
                        string stnName = DTLine[name];
                        comm.CommandText =
                            "INSERT INTO DT VALUES(" + "'" + stnNum + "', '" + stnName + "')";
                        comm.ExecuteNonQuery();
                    }

                    /* Insert values into CGLine */
                    string[] CGLine = File.ReadAllLines("../../MRT_Lines/ChangiLine.txt");
                    for (int num = 0, name = 1; name < CGLine.Length; num += 2, name += 2)
                    {
                        string stnNum = CGLine[num];
                        string stnName = CGLine[name];
                        comm.CommandText =
                            "INSERT INTO CG VALUES(" + "'" + stnNum + "', '" + stnName + "')";
                        comm.ExecuteNonQuery();
                    }

                    /* Insert values into BPLine */
                    string[] BPLine = File.ReadAllLines("../../MRT_Lines/BukitPanjangLine.txt");
                    for (int num = 0, name = 1; name < BPLine.Length; num += 2, name += 2)
                    {
                        string stnNum = BPLine[num];
                        string stnName = BPLine[name];
                        comm.CommandText =
                            "INSERT INTO BP VALUES(" + "'" + stnNum + "', '" + stnName + "')";
                        comm.ExecuteNonQuery();
                    }

                    /* Insert values into transfer stations */
                    //string[] transStations = File.ReadAllLines("../../MRT_Lines/transit.txt");
                    //for (int num = 0, name = 1; name < transStations.Length; num += 2, name += 2)
                    //{
                    //    string stnNum = transStations[num];
                    //    string stnName = transStations[name];
                    //    comm.CommandText =
                    //        "INSERT INTO transStations VALUES(" + "'" + stnNum + "', '" + stnName + "')";
                    //    comm.ExecuteNonQuery();
                    //}
                }
                conn.Close();
            }
        }
        public void deleteonExit()//deletes the tables on exit
        {
            using (SqlConnection conn = new SqlConnection())
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    conn.ConnectionString =
                        "Data Source = DESKTOP-O1L6A2E;database=APPDProj;integrated security = true";
                    conn.Open();
                    comm.Connection = conn;

                    /*Inserts values*/
                    comm.CommandText =
                        "DROP TABLE BP" +'\n' + "DROP TABLE CC"+ '\n' + "DROP TABLE CG" + '\n' + "DROP TABLE DT" +'\n' + "DROP TABLE EW" + '\n' + 
                        "DROP TABLE NE" + '\n' + "DROP TABLE NS" + '\n' + "DROP TABLE Fares" + '\n';

                    comm.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public List<Line> queryDB(string lineName)
        {
            List<Line> returnList = new List<Line>();
            using (SqlConnection conn = new SqlConnection())
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    conn.ConnectionString =
                        "Data Source = DESKTOP-O1L6A2E;database=APPDProj;integrated security = true";
                    conn.Open();
                    comm.Connection = conn;

                    comm.CommandText =
                        "SELECT * from " + changeName(lineName);
                    SqlDataReader reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        returnList.Add(new Station(lineName, reader["stationName"].ToString(), reader["LineCode"].ToString()));

                    }
                    reader.Close();
                } 
                conn.Close();
            }
            return returnList; 
        }

        private string changeName(string lineName)
        {
            switch (lineName)
            {
                case "East West Line":
                    return "EW";
                    break;
                case "North South Line":
                    return "NS";
                    break;
                case "North East Line":
                    return "NE";
                    break;
                case "Circle Line":
                    return "CC";
                    break;
                case "Downtown Line":
                    return "DT";
                    break;
                case "Changi Line":
                    return "CG";
                    break;
                case "Bukit Panjang Line":
                    return "BP";
                    break;
                //case "Transit Stations":
                //    return "transStation";
                //    break;

                default: return "";
            }
        }
    }
}
