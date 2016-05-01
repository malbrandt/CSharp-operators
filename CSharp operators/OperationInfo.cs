namespace CSharp_operators
{
    public class OperationInfo
    {
        public OperationInfo(string title, string description, string operatorSymbol, ExecuteFn executeFunction)
        {
            Title = title;
            Description = description;
            OperatorSymbol = operatorSymbol;
            ExecuteFunction = executeFunction;
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public string OperatorSymbol { get; private set; }
        public readonly ExecuteFn ExecuteFunction;

        public delegate int ExecuteFn(ref int firstArg, ref int secondArg);
    }
}