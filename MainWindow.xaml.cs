using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfCA1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /* Creates List */
        List<Line> EW = new List<Line>();
        List<Line> NS = new List<Line>();
        List<Line> NE = new List<Line>();
        List<Line> CC = new List<Line>();
        List<Line> DT = new List<Line>();
        List<Line> CG = new List<Line>();
        List<Line> BP = new List<Line>();

        public MainWindow()
        {
            InitializeComponent();

            /*Add Lines into combo box*/
            startLn.Items.Add("East West Line");
            startLn.Items.Add("North South Line");
            startLn.Items.Add("North East Line");
            startLn.Items.Add("Circle Line");
            startLn.Items.Add("Downtown Line");
            startLn.Items.Add("Changi Line");
            startLn.Items.Add("Bukit Panjang Line");
            startLn.SelectedIndex = 0;

            endLn.Items.Add("East West Line");
            endLn.Items.Add("North South Line");
            endLn.Items.Add("North East Line");
            endLn.Items.Add("Circle Line");
            endLn.Items.Add("Downtown Line");
            endLn.Items.Add("Changi Line");
            endLn.Items.Add("Bukit Panjang Line");
            endLn.SelectedIndex = 0;

            /* Add Lines into combo box for station finder */
            findLn.Items.Add("East West Line");
            findLn.Items.Add("North South Line");
            findLn.Items.Add("North East Line");
            findLn.Items.Add("Circle Line");
            findLn.Items.Add("Downtown Line");
            findLn.Items.Add("Changi Line");
            findLn.Items.Add("Bukit Panjang Line");
            findLn.SelectedIndex = 0;

            Guide init = new Guide();
            init.initialiseList();

            this.EW = init.ew;
            this.NS = init.ns;
            this.NE = init.ne;
            this.CC = init.cc;
            this.DT = init.dt;
            this.CG = init.cg;
            this.BP = init.bp;

        }

        /*Change combo box options based on start line*/
        private void startLn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            startStn.Items.Clear();
            Guide start = new Guide();
            String te = start.cmbbox();
            String[] test = te.Split('\n');


            switch (startLn.SelectedIndex)
            {
                case 0:
                    EW.ForEach(x =>
                    {
                        startStn.Items.Add(x.printFullInfo());
                    });
                    break;

                case 1:
                    NS.ForEach(x =>
                    {
                        startStn.Items.Add(x.printFullInfo());
                    });
                    break;

                case 2:
                    NE.ForEach(x =>
                    {
                        startStn.Items.Add(x.printFullInfo());
                    });
                    break;

                case 3:
                    CC.ForEach(x =>
                    {
                        startStn.Items.Add(x.printFullInfo());
                    });
                    break;

                case 4:
                    DT.ForEach(x =>
                    {
                        startStn.Items.Add(x.printFullInfo());
                    });
                    break;

                case 5:
                    CG.ForEach(x =>
                    {
                        startStn.Items.Add(x.printFullInfo());
                    });
                    break;

                case 6:
                    BP.ForEach(x =>
                    {
                        startStn.Items.Add(x.printFullInfo());
                    });
                    break;

            }
            
        }
        

        private void endLn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Guide end = new Guide();
            String te = end.cmbbox();
            String[] test = te.Split('\n');

            endStn.Items.Clear();
            int name = endLn.SelectedIndex;

            switch (name)
            {
                case 0:
                    EW.ForEach(x =>
                    {
                        endStn.Items.Add(x.printFullInfo());
                    });
                    break;

                case 1:
                    NS.ForEach(x =>
                    {
                        endStn.Items.Add(x.printFullInfo());
                    });
                    break;

                case 2:
                    NE.ForEach(x =>
                    {
                        endStn.Items.Add(x.printFullInfo());
                    });
                    break;

                case 3:
                    CC.ForEach(x =>
                    {
                        endStn.Items.Add(x.printFullInfo());
                    });
                    break;

                case 4:
                    DT.ForEach(x =>
                    {
                        endStn.Items.Add(x.printFullInfo());
                    });
                    break;

                case 5:
                    CG.ForEach(x =>
                    {
                        endStn.Items.Add(x.printFullInfo());
                    });
                    break;

                case 6:
                    BP.ForEach(x =>
                    {
                        endStn.Items.Add(x.printFullInfo());
                    });
                    break;
            }
        }

        /*Starts the computation for routes*/
        private void btnCompute_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                rtb_Display.Clear();
                DB run = new DB();
                Guide main = new Guide();
                //main.initialiseList();
                rtb_op.Text = "";
                rtb_Display.Text = "";
                rtb_Display.Text += "Start Station: " + startStn.SelectedItem + "\r\nEnd Station: " + endStn.SelectedItem + "\r\n";
                String stncode = startStn.SelectedItem.ToString().Substring(1, startStn.SelectedItem.ToString().IndexOf("]") - 1);
                String stncode2 = endStn.SelectedItem.ToString().Substring(1, endStn.SelectedItem.ToString().IndexOf("]") - 1);
                if (startLn.SelectedItem.ToString().Equals(endLn.SelectedItem.ToString()))//uses this method if start line is the same as destination line
                {
                    rtb_Display.Text += main.computeEZRoute(startStn.SelectedItem.ToString(), endStn.SelectedItem.ToString());
                    if (!String.IsNullOrWhiteSpace(run.getFares(stncode, stncode2)))
                        rtb_Display.Text += run.getFares(stncode, stncode2);
                    else
                        rtb_Display.Text += "The fares for the route combination is not available";
                }
                else
                {
                    rtb_Display.Text += main.computeOneTransferRoute(startStn.SelectedItem.ToString(), endStn.SelectedItem.ToString());
                    rtb_op.Text = main.computeOneTransferRoute(startStn.SelectedItem.ToString(), endStn.SelectedItem.ToString()).Split('\n').Last();
                    if (!String.IsNullOrWhiteSpace(run.getFares(stncode, stncode2)))
                        rtb_op.Text += run.getFares(stncode, stncode2);
                    else
                        rtb_op.Text += "The fares for the route combination is not available";
                }
            }

            catch (Exception ex)//catches all exceptions
            {
                MessageBox.Show("Error! your mom gay");
            }
        }

        private void btnFindStation_Click(object sender, RoutedEventArgs e)
        {
            Guide findRoute = new Guide();
            //findRoute.initialiseList();
            String line = findLn.Text;
            int count = 0;
            
            try
            {
                imgEW.Visibility = Visibility.Hidden;
                imgNS.Visibility = Visibility.Hidden;
                imgNE.Visibility = Visibility.Hidden;
                imgBP.Visibility = Visibility.Hidden;
                imgDT.Visibility = Visibility.Hidden;
                imgCC.Visibility = Visibility.Hidden;


                switch (line)
                {
                    case "East West Line":
                        imgEW.Visibility = Visibility.Visible;
                        count = findRoute.counting(startStn.SelectedItem.ToString());
                        break;

                    case "North South Line":
                        imgNS.Visibility = Visibility.Visible;
                        count = findRoute.counting(startStn.SelectedItem.ToString());
                        break;

                    case "North East Line":
                        imgNE.Visibility = Visibility.Visible;
                        count = findRoute.counting(startStn.SelectedItem.ToString());
                        break;

                    case "Circle Line":
                        imgCC.Visibility = Visibility.Visible;
                        count = findRoute.counting(startStn.SelectedItem.ToString());
                        break;

                    case "Downtown Line":
                        imgDT.Visibility = Visibility.Visible;
                        count = findRoute.counting(startStn.SelectedItem.ToString());
                        break;

                    case "Changi Line":
                        imgEW.Visibility = Visibility.Visible;
                        count = findRoute.counting(startStn.SelectedItem.ToString());
                        break;
                    case "Bukit Panjang Line":
                        imgBP.Visibility = Visibility.Visible;
                        count = findRoute.counting(startStn.SelectedItem.ToString());
                        break;
                }
            }
            catch (Exception ex) { }
        }

        private void findLn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Guide start = new Guide();
            //start.initialiseList();
            String te = start.cmbbox();
            String[] test = te.Split('\n');
            findStn.Items.Clear();
            int name = findLn.SelectedIndex;

            switch (name)
            {
                case 0:
                    EW.ForEach(x =>
                    {
                        findStn.Items.Add(x.getName());
                    });
                    break;

                case 1:
                    NS.ForEach(x =>
                    {
                        findStn.Items.Add(x.getName());
                    });
                    break;

                case 2:
                    NE.ForEach(x =>
                    {
                        findStn.Items.Add(x.getName());
                    });
                    break;

                case 3:
                    CC.ForEach(x =>
                    {
                        findStn.Items.Add(x.getName());
                    });
                    break;

                case 4:
                    DT.ForEach(x =>
                    {
                        findStn.Items.Add(x.getName());
                    });
                    break;

                case 5:
                    CG.ForEach(x =>
                    {
                        findStn.Items.Add(x.getName());
                    });
                    break;

                case 6:
                    BP.ForEach(x =>
                    {
                        findStn.Items.Add(x.getName());
                    });
                    break;

            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DB end = new DB();
            end.deleteonExit();
        }
    }
}
