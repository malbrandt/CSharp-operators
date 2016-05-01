namespace CSharp_operators
{
    public struct StringAndInt
    {
        public StringAndInt(string String, int Int)
        {
            this.String = String;
            this.Int = Int;
        }
        public string String { get; set; }
        public int Int { get; set; }
    }
}