namespace FunctionTest.Models
{
    public class ResultsTable
    {
        public string TableName { get; set; }
        public string[] ColumnNames { get; set; } = new string[2];
        public Dictionary<string, int> TableData { get; set; } = new Dictionary<string, int>();
        public ResultsTable(string name)
        {
            TableName = name;
        }
    }
}
