using System.Collections.ObjectModel;
using System.Windows;

namespace CSharp_operators
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public ObservableCollection<OperationInfo> OperationInfos { get; private set; }
        public ObservableCollection<StringAndInt> PieChartData { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            OperationInfos = new ObservableCollection<OperationInfo>
            {
                new OperationInfo("Logical AND", "Logical or bitwise AND. Use with integer types and enum types is generally allowed.", @"&", (f, s) => f & s),
                new OperationInfo("Logical XOR", "Logical or bitwise XOR. You can generally use this with integer types and enum types.", @"^", (f, s) => f ^ s),
                new OperationInfo("Logical OR", "Logical or bitwise OR. Use with integer types and enum types is generally allowed.", @"|", (f, s) => f | s),
                new OperationInfo("Left shift", "Shift bits left and fill with zero on the right", @"<<", (f,s) => f << s),
                new OperationInfo("Right shift", "Shift bits right.If the left operand is int or long, then left bits are filled with the sign bit.If the left operand is uint or ulong, then left bits are filled with zero", @">>", (f,s) => f >> s)
            };

            PieChartData = new ObservableCollection<StringAndInt>
            {
                new StringAndInt("x", Properties.Settings.Default.DefaultFirstValue),
                new StringAndInt("y", Properties.Settings.Default.DefaultSecondValue),
                new StringAndInt("operation result", default(int))
            };
        }

        #region Dependency properties

        public string BitToInteger
        {
            get { return (string)GetValue(BitToIntegerProperty); }
            set { SetValue(BitToIntegerProperty, value); }
        }
        public static readonly DependencyProperty BitToIntegerProperty =
            DependencyProperty.Register("BitToInteger", typeof(string), typeof(MainWindow), new PropertyMetadata(string.Empty));

        public int FirstValue
        {
            get { return (int)GetValue(FirstValueProperty); }
            set { SetValue(FirstValueProperty, value); }
        }
        public static readonly DependencyProperty FirstValueProperty =
            DependencyProperty.Register("FirstValue", typeof(int), typeof(MainWindow), new PropertyMetadata(0));

        public int SecondValue
        {
            get { return (int)GetValue(SecondValueProperty); }
            set { SetValue(SecondValueProperty, value); }
        }
        public static readonly DependencyProperty SecondValueProperty =
            DependencyProperty.Register("SecondValue", typeof(int), typeof(MainWindow), new PropertyMetadata(0));

        #endregion Dependency properties
    }
}
