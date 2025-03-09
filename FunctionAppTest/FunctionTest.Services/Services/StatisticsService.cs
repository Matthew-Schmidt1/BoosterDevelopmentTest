using FunctionTest.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Text;

namespace FunctionTest.Services
{
    internal class StatisticsService : IStatisticsService
    {
        private readonly ICountedObjectManager<string> _WordManager;
        private readonly ICountedObjectManager<char> _CharacterManager;
        private readonly ILogger _Logger;
        public StatisticsService(ILogger<StatisticsService> logger, ICountedObjectManager<string> wordManager, ICountedObjectManager<char> characterManager)
        {
            _WordManager = wordManager;
            _CharacterManager = characterManager;
            _Logger = logger;
        }

        public async Task ConsumeString(string str)
        {
            _Logger.LogTrace("Starting {MethodName}", nameof(ConsumeString));
            Task[] tasks = new Task[2];
            tasks[0] = Task.Run(() => ConsumeWords(str));
            tasks[1] = Task.Run(() => ConsumeCharacters(str));

            await Task.WhenAll(tasks);
            _Logger.LogTrace("Finishing {MethodName}", nameof(ConsumeString));
        }

        private void ConsumeCharacters(string str)
        {
            _Logger.LogTrace("Starting {MethodName}", nameof(ConsumeCharacters));
            foreach (char character in str)
            {
                _CharacterManager.AddItem(character);
            }
            _Logger.LogTrace("Finishing {MethodName}", nameof(ConsumeCharacters));
        }

        private void ConsumeWords(string str)
        {
            _Logger.LogTrace("Starting {MethodName}", nameof(ConsumeWords));
            var Words = str.Split(' ');
            foreach (var word in Words)
            {
                _WordManager.AddItem(word);
            }
            _Logger.LogTrace("Finishing {MethodName}", nameof(ConsumeWords));
        }


        public DataTable TotalNumberOfCharactersAndWords()
        {
            _Logger.LogTrace("Starting {MethodName}", nameof(TotalNumberOfCharactersAndWords));
            DataTable dataTable = new DataTable(nameof(TotalNumberOfCharactersAndWords));
            dataTable.Columns.Add("Words", typeof(int));
            dataTable.Columns.Add("Characters", typeof(int));
            DataRow row = dataTable.NewRow();
            row["Words"] = _WordManager.GetAllItems().Count();
            row["Characters"] = _CharacterManager.GetAllItems().Count();
            dataTable.Rows.Add(row);
            _Logger.LogTrace("Finishing {MethodName}", nameof(TotalNumberOfCharactersAndWords));
            return dataTable;
        }

        public DataTable GetLargestWords(int number)
        {
            _Logger.LogTrace("Starting {MethodName}", nameof(GetLargestWords));
            var dataTable = GetOrderDataTable(number, true);
            dataTable.TableName = nameof(GetLargestWords);
            _Logger.LogTrace("Finishing {MethodName}", nameof(GetLargestWords));
            return dataTable;
        }

        private DataTable GetOrderDataTable(int number, bool largest)
        {
            _Logger.LogTrace("Starting {MethodName}", nameof(GetOrderDataTable));
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Words", typeof(string));
            dataTable.Columns.Add("Length", typeof(int));

            foreach (var item in _WordManager.GetOrderByLength(number, largest))
            {
                DataRow row = dataTable.NewRow();
                row["Words"] = item.Key;
                row["Length"] = item.Value;
                dataTable.Rows.Add(row);
            }
            _Logger.LogTrace("Finishing {MethodName}", nameof(GetOrderDataTable));
            return dataTable;
        }

        public DataTable GetSmallestWords(int number)
        {
            _Logger.LogTrace("Starting {MethodName}", nameof(GetSmallestWords));
            var dataTable = GetOrderDataTable(number, false);
            dataTable.TableName = nameof(GetSmallestWords);
            _Logger.LogTrace("Finishing {MethodName}", nameof(GetSmallestWords));
            return dataTable;
        }

        public DataTable MostFrequencyWord(int number)
        {
            _Logger.LogTrace("Starting {MethodName}", nameof(MostFrequencyWord));
            DataTable dataTable = new DataTable(nameof(GetAllCharactersFrequency));
            dataTable.Columns.Add("Words", typeof(string));
            dataTable.Columns.Add("Length", typeof(int));

            foreach (var item in _WordManager.GetOrderByCount(number, true))
            {
                DataRow row = dataTable.NewRow();
                row["Words"] = item.Key;
                row["Length"] = item.Value;
                dataTable.Rows.Add(row);
            }
            _Logger.LogTrace("Finishing {MethodName}", nameof(MostFrequencyWord));
            return dataTable;
        }

        public DataTable GetAllCharactersFrequency()
        {
            _Logger.LogTrace("Starting {MethodName}", nameof(GetAllCharactersFrequency));
            DataTable dataTable = new DataTable(nameof(GetAllCharactersFrequency));
            dataTable.Columns.Add("Character", typeof(string));
            dataTable.Columns.Add("Count", typeof(int));
            foreach (var item in _CharacterManager.GetOrderByCount(_CharacterManager.GetAllItems().Count(), true))
            {
                DataRow row = dataTable.NewRow();
                row["Character"] = item.Key;
                row["Count"] = item.Value;
                dataTable.Rows.Add(row);
            }
            _Logger.LogTrace("Finishing {MethodName}", nameof(GetAllCharactersFrequency));
            return dataTable;
        }


        public string ConvertDataTableToString(DataTable dt)
        {
            _Logger.LogTrace("Starting {MethodName}", nameof(ConvertDataTableToString));
            StringBuilder str = new StringBuilder();
            str.AppendLine(dt.TableName);
            str.AppendLine(string.Join(" &#9;&#9;| ", dt.Columns.OfType<DataColumn>().Select(x => string.Join(" , ", x.ColumnName))));
            str.AppendLine(string.Join(Environment.NewLine, dt.Rows.OfType<DataRow>().Select(x => string.Join(" &#9;&#9;| ", x.ItemArray))));
            str.AppendLine();
            _Logger.LogTrace("Finishing {MethodName}", nameof(ConvertDataTableToString));
            return str.ToString();
        }

        public void ResetService()
        {
            _Logger.LogTrace("Starting {MethodName}", nameof(ResetService));
            _WordManager.ResetCount();
            _CharacterManager.ResetCount();
            _Logger.LogTrace("Finishing {MethodName}", nameof(ResetService));
        }
    }
}
