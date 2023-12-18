using System.Reflection;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            double t = double.Parse(TTextBox.Text);
            double initialValue = double.Parse(InitialValueTextBox.Text);
            double step = double.Parse(StepTextBox.Text);

            // Calculate P amd Sums
            Calculate(alfa1, alfa2, alfa3, alfa4, t, initialValue, step);
        }

        private void Calculate(double alfa1, double alfa2, double alfa3, double alfa4, double t, double initialValue, double step)
        {
            MyClass solver = new MyClass();
            double[] y = [initialValue, 0, 0, 0, 0, 0, 0, 0, 0, 0];
            double h = step;

            List<DataItem> data = new List<DataItem>();

            for (double i = initialValue; i <= t; i += step)
            {
                y = solver.RungeKutta(i, y, h, alfa1, alfa2, alfa3, alfa4);

                DataItem item = new DataItem
                {
                    T = i,
                    P1 = y[0],
                    P2 = y[1],
                    P3 = y[2],
                    P4 = y[3],
                    P5 = y[4],
                    P6 = y[5],
                    P7 = y[6],
                    P8 = y[7],
                    P9 = y[8],
                    P10 = y[9]
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
        public double SUM {
            get
            {
                return (P1 + P2 + P3 + P4 + P5 + P6 + P7 + P8 + P9 + P10);
            }
        }
        public double Pt
        {
            get
            {
                return (P1 + P2 + P3);
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
        public double[] RungeKutta(double t, double[] y, double h, double a1, double a2, double a3, double a4)
        {
            double[] k1 = System(t, y, a1, a2, a3, a4);
            double[] k2 = System(t + h / 2, AddArrays(y, ScaleArray(k1, h / 2)), a1, a2, a3, a4);
            double[] k3 = System(t + h / 2, AddArrays(y, ScaleArray(k2, h / 2)), a1, a2, a3, a4);
            double[] k4 = System(t + h, AddArrays(y, ScaleArray(k3, h)), a1, a2, a3, a4);

            double[] dy = ScaleArray(AddArrays(AddArrays(k1, ScaleArray(k2, 2)), AddArrays(ScaleArray(k3, 2), k4)), h / 6);

            return AddArrays(y, dy);
        }

        public double[] System(double t, double[] y, double a1, double a2, double a3, double a4)
        {
            double P1 = y[0];
            double P2 = y[1];
            double P3 = y[2];
            double P4 = y[3];
            double P5 = y[4];
            double P6 = y[5];
            double P7 = y[6];
            double P8 = y[7];
            double P9 = y[8];
            double P10 = y[9];

            double dP1dt = -(a1 + a2 + a3 + a4) * P1;
            double dP2dt = a1 * P1 - (a2 + a3 + a4) * P2;
            double dP3dt = a2 * P1 - (a2 + a3 + a4) * P3;
            double dP4dt = a3 * P1;
            double dP5dt = a4 * P1;
            double dP6dt = a2 * P2 + a2 * P3;
            double dP7dt = a3 * P2;
            double dP8dt = a4 * P2;
            double dP9dt = a3 * P3;
            double dP10dt = a4 * P3;

            return new double[] { dP1dt, dP2dt, dP3dt, dP4dt, dP5dt, dP6dt, dP7dt, dP8dt, dP9dt, dP10dt };
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