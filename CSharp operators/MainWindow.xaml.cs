using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using CSharp_operators.Annotations;
using CSharp_operators.Properties;

namespace CSharp_operators
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        private int _lastResult;

        public ObservableCollection<OperationInfo> OperationInfos { get; private set; }

        /// <summary>
        /// Variables values list before computation
        /// </summary>
        public ObservableCollection<StringAndInt> PieChartData1 { get; private set; }
        /// <summary>
        /// Variables values list after computation
        /// </summary>
        public ObservableCollection<StringAndInt> PieChartData2 { get; private set; }

        public string FirstValueBinary => FirstValue.ToBinaryString();
        public string SecondValueBinary => SecondValue.ToBinaryString();
        public string LastResultBinary => _lastResult.ToBinaryString();
        public string ExpressionString => $"{FirstValue} {CurrentlySelectedOperationInfo.OperatorSymbol} {SecondValue} = {_lastResult}";


        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            OperationInfos = new ObservableCollection<OperationInfo>
            {
                new OperationInfo("Logical AND", "Logical or bitwise AND. Use with integer types and enum types is generally allowed.", @"&", (ref int f, ref int s) => f & s),
                new OperationInfo("Logical XOR", "Logical or bitwise XOR. You can generally use this with integer types and enum types.", @"^", (ref int f, ref int s) => f ^ s),
                new OperationInfo("Logical OR", "Logical or bitwise OR. Use with integer types and enum types is generally allowed.", @"|", (ref int f, ref int s) => f | s),
                new OperationInfo("Left shift", "Shift bits left and fill with zero on the right", @"<<", (ref int f, ref int s) => f << s),
                new OperationInfo("Right shift", "Shift bits right.If the left operand is int or long, then left bits are filled with the sign bit.If the left operand is uint or ulong, then left bits are filled with zero", @">>", (ref int f, ref int s) => f >> s),
                new OperationInfo("Addition", string.Empty, @"+", (ref int arg, ref int secondArg) => arg + secondArg),
                new OperationInfo("Substraction", string.Empty, @"-", (ref int arg, ref int secondArg) => arg - secondArg),
                new OperationInfo("Multiplication", string.Empty, "*", (ref int arg, ref int secondArg) => arg * secondArg),
                new OperationInfo("Division", "If the operands are integers, the result is an integer truncated toward zero (for example, -7 / 2 is -3)", @"/", (ref int arg, ref int secondArg) => (arg * secondArg != 0) ? arg / secondArg : 0), // DIV by 0
                new OperationInfo("Modulus", "If the operands are integers, this returns the remainder of dividing x by y.If q = x / y and r = x % y, then x = q * y + r.", @"%", (ref int arg, ref int secondArg) => arg % secondArg)
            };

            PieChartData1 = new ObservableCollection<StringAndInt>();
            PieChartData2 = new ObservableCollection<StringAndInt>();
            UpdatePieChartData();
        }


        private void UpDownBase_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            UpdatePieChartData();
        }

        private void OperatorsListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Update pie chart data when changed operation
            UpdatePieChartData();
        }
        private void UpdatePieChartData()
        {
            if (CurrentlySelectedOperationInfo == null
                || PieChartData1 == null
                || PieChartData2 == null) return;

            PieChartData1.Clear();
            PieChartData1.Add(new StringAndInt(Settings.Default.FirstParamName, FirstValue));
            PieChartData1.Add(new StringAndInt(Settings.Default.SecondParamName, SecondValue));

            var p1 = FirstValue;
            var p2 = SecondValue;
            var result = CurrentlySelectedOperationInfo.ExecuteFunction(ref p1, ref p2);
            _lastResult = result;
            PieChartData2.Clear();
            PieChartData2.Add(new StringAndInt(Settings.Default.OperationResultLiteral, result));
            PieChartData2.Add(new StringAndInt(Settings.Default.FirstParamName, p1));
            PieChartData2.Add(new StringAndInt(Settings.Default.SecondParamName, p2));

            OnPropertyChanged(nameof(FirstValueBinary));
            OnPropertyChanged(nameof(SecondValueBinary));
            OnPropertyChanged(nameof(LastResultBinary));
            OnPropertyChanged(nameof(ExpressionString));
        }
        #region Dependency properties

        public int FirstValue
        {
            get { return (int)GetValue(FirstValueProperty); }
            set { SetValue(FirstValueProperty, value); }
        }
        public static readonly DependencyProperty FirstValueProperty =
            DependencyProperty.Register("FirstValue", typeof(int), typeof(MainWindow), new PropertyMetadata(Settings.Default.DefaultFirstValue));

        public int SecondValue
        {
            get { return (int)GetValue(SecondValueProperty); }
            set { SetValue(SecondValueProperty, value); }
        }
        public static readonly DependencyProperty SecondValueProperty =
            DependencyProperty.Register("SecondValue", typeof(int), typeof(MainWindow), new PropertyMetadata(Settings.Default.DefaultSecondValue));

        public OperationInfo CurrentlySelectedOperationInfo
        {
            get { return (OperationInfo)GetValue(CurrentlySelectedOperationInfoProperty); }
            set { SetValue(CurrentlySelectedOperationInfoProperty, value); }
        }
        public static readonly DependencyProperty CurrentlySelectedOperationInfoProperty =
            DependencyProperty.Register("CurrentlySelectedOperationInfo", typeof(OperationInfo), typeof(MainWindow), new PropertyMetadata(null));

        #endregion Dependency properties

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Swap button
            if (FirstValue == SecondValue) return;
            var temp = FirstValue;
            FirstValue = SecondValue;
            SecondValue = temp;
            UpdatePieChartData();
        }
    }
}
