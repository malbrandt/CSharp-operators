namespace CSharp_operators
{
    public class StringAndInt
    {

        public StringAndInt(string @string, int @int)
        {
            String = @string;
            Int = @int;
        }

        public int Int { get; set; }
        public string String { get; set; }
    }
}