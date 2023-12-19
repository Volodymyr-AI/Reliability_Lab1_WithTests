using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input.Manipulations;

namespace Reliability_Lab1_WithTests
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            double alfa1 = double.Parse(Alfa1TextBox.Text);
            double alfa2 = double.Parse(Alfa2TextBox.Text);
            double alfa3 = double.Parse(Alfa3TextBox.Text);
            double alfa4 = double.Parse(Alfa4TextBox.Text);
            double m1 = double.Parse(M1TextBox.Text);
            double m2 = double.Parse(M2TextBox.Text);
            double m3 = double.Parse(M3TextBox.Text);
            double m4 = double.Parse(M4TextBox.Text);
            double t = double.Parse(TTextBox.Text);
            double initialValue = double.Parse(InitialValueTextBox.Text);
            double step = double.Parse(StepTextBox.Text);

            // Calculate P amd Sums
            Calculate(alfa1, alfa2, alfa3, alfa4, m1, m2, m3, m4, t, initialValue, step);
        }

        private void Calculate(double alfa1, double alfa2, double alfa3, double alfa4, double m1, double m2, double m3, double m4, double t, double initialValue, double step)
        {
            MyClass solver = new MyClass();
            double[] y = [initialValue, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
            double h = step;

            List<DataItem> data = new List<DataItem>();

            for (double i = initialValue; i <= t; i += step)
            {
                y = solver.RungeKutta(i, y, h, alfa1, alfa2, alfa3, alfa4, m1, m2, m3, m4);

                DataItem item = new DataItem
                {
                    T = i,
                    P0 = y[0],
                    P1 = y[1],
                    P2 = y[2],
                    P3 = y[3],
                    P4 = y[4],
                    P5 = y[5],
                    P6 = y[6],
                    P7 = y[7],
                    P8 = y[8],
                    P9 = y[9],
                    P10 = y[10],
                    P11 = y[11],
                    P12 = y[12],
                    P13 = y[13],
                    P14 = y[14],
                    P15 = y[15],
                    P16 = y[16],
                    P17 = y[17],
                    P18 = y[18],
                    P19 = y[19],
                    P20 = y[20],
                    P21 = y[21],
                    P22 = y[22],
                    P23 = y[23],
                    P24 = y[24],
                    P25 = y[25],
                };
                data.Add(item);
            }

            ShowTable(data);
        }

        private void ShowTable(List<DataItem> data)
        {
            Window newWindow = new Window
            {
                Title = "Table",
                Width = 800,
                Height = 600
            };

            DataGrid dataGrid = new DataGrid
            {
                AutoGenerateColumns = false,
                ItemsSource = data
            };

            PropertyInfo[] properties = typeof(DataItem).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                dataGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = property.Name,
                    Binding = new Binding(property.Name)
                });
            }

            foreach (var column in dataGrid.Columns)
            {
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }

            newWindow.Content = dataGrid;

            newWindow.Show();
        }
        
    }

    public class DataItem
    {
        public double T { get; set; }
        public double P0 { get; set; }
        public double P1 { get; set; }
        public double P2 { get; set; }
        public double P3 { get; set; }
        public double P4 { get; set; }
        public double P5 { get; set; }
        public double P6 { get; set; }
        public double P7 { get; set; }
        public double P8 { get; set; }
        public double P9 { get; set; }
        public double P10 { get; set; }
        public double P11 { get; set; }
        public double P12 { get; set; }
        public double P13 { get; set; }
        public double P14 { get; set; }
        public double P15 { get; set; }
        public double P16 { get; set; }
        public double P17 { get; set; }
        public double P18 { get; set; }
        public double P19 { get; set; }
        public double P20 { get; set; }
        public double P21 { get; set; }
        public double P22 { get; set; }
        public double P23 { get; set; }
        public double P24 { get; set; }
        public double P25 { get; set; }
    
        public double SUM {
            get
            {
                return (P0 + P1 + P2 + P3 + P4 + P5 + P6 + P7 + P8 + P9 + P10 + P11 + P12 + P13 + P14 + P15 + P16 + P17 + P18 + P19 + P20 + P21 + P22 + P23 + P24 + P25);
            }
        }
        public double Pt
        {
            get
            {
                return (P0 + P1 + P3 + P13 + P14 + P16);
            }
        }
        public double Qt
        {
            get
            {
                return 1 - Pt;
            }
        }
    }

    public class MyClass
    {
        public double[] RungeKutta(double t, double[] y, double h, double a1, double a2, double a3, double a4, double m1, double m2, double m3, double m4)
        {
            double[] k1 = System(t, y, a1, a2, a3, a4, m1, m2, m3, m4);
            double[] k2 = System(t + h / 2, AddArrays(y, ScaleArray(k1, h / 2)), a1, a2, a3, a4, m1, m2, m3, m4);
            double[] k3 = System(t + h / 2, AddArrays(y, ScaleArray(k2, h / 2)), a1, a2, a3, a4, m1, m2, m3, m4);
            double[] k4 = System(t + h, AddArrays(y, ScaleArray(k3, h)), a1, a2, a3, a4, m1, m2, m3, m4);

            double[] dy = ScaleArray(AddArrays(AddArrays(k1, ScaleArray(k2, 2)), AddArrays(ScaleArray(k3, 2), k4)), h / 6);

            return AddArrays(y, dy);
        }

        public double[] System(double t, double[] y, double a1, double a2, double a3, double a4, double m1, double m2, double m3, double m4)
        {
            double P0 = y[0];
            double P1 = y[1];
            double P2 = y[2];
            double P3 = y[3];
            double P4 = y[4];
            double P5 = y[5];
            double P6 = y[6];
            double P7 = y[7];
            double P8 = y[8];
            double P9 = y[9];
            double P10 = y[10];
            double P11 = y[11];
            double P12 = y[12];
            double P13 = y[13];
            double P14 = y[14];
            double P15 = y[15];
            double P16 = y[16];
            double P17 = y[17];
            double P18 = y[18];
            double P19 = y[19];
            double P20 = y[20];
            double P21 = y[21];
            double P22 = y[22];
            double P23 = y[23];
            double P24 = y[24];
            double P25 = y[25];

            //(a2 + a3 + a4) * P3

            double dP0dt = -(2*a1 + 2*a2 + a3 + a4) * P0 + m1*P1 + m2*P3 + m3*P5 + m4 * P6;
            double dP1dt = a1 * P0 - (2*a2 + a3 + a4 + m1) * P1 + m2*P7 + m3*P9 +m4*P10;
            double dP2dt = a1 * P0 - m1*P13;
            double dP3dt = a2*P0 - (2*a1 + a3 + a4 + m2) * P3 + m1*P7 + m3*P11 + m4*P12;
            double dP4dt = a2 * P0 - m2 * P13;
            double dP5dt = a3 * P0 - m3 * P5;
            double dP6dt = a4*P0 - m4 * P6;
            double dP7dt = a2*P1 + a1*P3 - (m1 + m2) * P7;
            double dP8dt = a2 * P1 + a1 * P3 - (m1 + m2) * P8;
            double dP9dt = a3 * P1 - m3 * P9;
            double dP10dt = a4*P1 - m3*P10;
            double dP11dt = a3 * P3 - m3 * P11;
            double dP12dt = a4*P3 - m4 * P12;
            double dP13dt = m1*P2 +m2*P4 - (2*a1 + 2 * a2 + a3 + a4) * P13 + m1*P14 + m2*P16 + m3*P18 + m4*P19;
            double dP14dt = m2 * P8 + a1 * P13 - (m1 + 2 * a2 + a3 + a4) * P14 + m2 * P20 + m3 * P22 + m4 * P23;
            double dP15dt = a1 * P13;
            double dP16dt = m1 * P8 + a2 * P13 - (m2 + 2 * a1 + a3 + a4) * P16 + m1 * P20 + m3 * P24 + m4 * P25;
            double dP17dt = a2 * P13;
            double dP18dt = a3 * P13 - m3 * P18;
            double dP19dt = a4 * P13 - m4 * P19;
            double dP20dt = a2 * P14 + a1 * P16 - m2 * P20;
            double dP21dt = a2 * P14 + a1 * P16;
            double dP22dt = a3 * P14 - m3 * P22;
            double dP23dt = a4 * P14 - m4 * P23;
            double dP24dt = a3 * P16 - m3 * P24;
            double dP25dt = a4 * P16 - m4 * P25;

            // woooooooooooo I did it 

            return new double[] { dP0dt, dP1dt, dP2dt, dP3dt, dP4dt, dP5dt, dP6dt, dP7dt, dP8dt, dP9dt, dP10dt, dP11dt, dP12dt, dP13dt, dP14dt, dP15dt, dP16dt, dP17dt, dP18dt, dP19dt, dP20dt, dP21dt, dP22dt, dP23dt, dP24dt, dP25dt };
        }

        private double[] AddArrays(double[] a, double[] b)
        {
            double[] result = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                result[i] = a[i] + b[i];
            }
            return result;
        }

        private double[] ScaleArray(double[] a, double scale)
        {
            double[] result = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                result[i] = a[i] * scale;
            }
            return result;
        }
    }
}