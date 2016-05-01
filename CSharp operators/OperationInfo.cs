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
            LastResult = default(int);
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public string OperatorSymbol { get; private set; }
        public readonly ExecuteFn ExecuteFunction;
        public int LastResult { get; private set; }

        public delegate int ExecuteFn(int firstArg, int secondArg);
    }
}