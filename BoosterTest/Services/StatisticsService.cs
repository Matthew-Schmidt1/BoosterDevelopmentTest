using System.Data;
using System.Text;

namespace BoosterTest.Services
{
    class StatisticsService
    {
        private readonly WordManagerService WordManager= new WordManagerService();
        private readonly CharacterManagerService CharacterManager = new CharacterManagerService();
        
        public async Task ConsumeString(string str)
        {
            Task[] tasks = new Task[2];
            tasks[0] = Task.Run(() => ConsumeWords(str));
            tasks[1] = Task.Run(() => ConsumeCharacters(str));

            await Task.WhenAll(tasks);

        }

        private void ConsumeCharacters(string str)
        {
            foreach (char character in str)
            {
                CharacterManager.AddItem(character);
            }
        }

        private void ConsumeWords(string str)
        {
            var Words = str.Split(' ');
            foreach (var word in Words)
            {
                WordManager.AddItem(word);
            }
        }


        public DataTable TotalNumberOfCharactersAndWords()
        {
            DataTable dataTable = new DataTable(nameof(TotalNumberOfCharactersAndWords));
            dataTable.Columns.Add("Words",typeof(int));
            dataTable.Columns.Add("Characters", typeof(int));
            DataRow row = dataTable.NewRow();
            row["Words"] = WordManager.GetAllItems().Count();
            row["Characters"] = CharacterManager.GetAllItems().Count();
            dataTable.Rows.Add(row);
            return dataTable;
        }

        public DataTable GetLargestWords(int number)
        {
            var dataTable = GetOrderDataTable(number,true);
            dataTable.TableName = nameof(GetLargestWords);
            return dataTable;
        }

        private DataTable GetOrderDataTable(int number,bool largest)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Words", typeof(string));
            dataTable.Columns.Add("Length", typeof(int));
            
            foreach (var item in WordManager.getOrderByLength(number, largest))
            {
                DataRow row = dataTable.NewRow();
                row["Words"] = item.Key;
                row["Length"] = item.Value;
                dataTable.Rows.Add(row);
            }
            
            return dataTable;
        }

        public DataTable GetSmallestWords(int number)
        {
            var dataTable =GetOrderDataTable(number, false);
            dataTable.TableName = nameof(GetSmallestWords);
            return dataTable;
        }

        public DataTable MostFrequencyWord(int number)
        {
            DataTable dataTable = new DataTable(nameof(GetAllCharactersFrequency));
            dataTable.Columns.Add("Words", typeof(string));
            dataTable.Columns.Add("Length", typeof(int));
            
            foreach (var item in WordManager.getOrderByCount(number, true))
            {
                DataRow row = dataTable.NewRow();
                row["Words"] = item.Key;
                row["Length"] = item.Value;
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }

        public DataTable GetAllCharactersFrequency()
        {
            DataTable dataTable = new DataTable(nameof(GetAllCharactersFrequency));
            dataTable.Columns.Add("Character", typeof(string));
            dataTable.Columns.Add("Count", typeof(int));
            foreach (var item in CharacterManager.getOrderByCount(CharacterManager.GetAllItems().Count(), true))
            {
                DataRow row = dataTable.NewRow();
                row["Character"] = item.Key;
                row["Count"] = item.Value;
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }


        public string ConvertDataTableToString(DataTable dt)
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine(dt.TableName);
            str.AppendLine(string.Join(" \t\t| ",dt.Columns.OfType<DataColumn>().Select(x => string.Join(" , ", x.ColumnName))));
            str.AppendLine(string.Join(Environment.NewLine, dt.Rows.OfType<DataRow>().Select(x => string.Join(" \t\t| ", x.ItemArray))));
            str.AppendLine();
            return str.ToString();
        }
    }
}
