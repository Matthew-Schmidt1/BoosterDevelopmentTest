using System.Data;

namespace FunctionTest.Services.Interfaces
{
    public interface IStatisticsService
    {
        Task ConsumeString(string str);
        string ConvertDataTableToString(DataTable dt);
        DataTable GetAllCharactersFrequency();
        DataTable GetLargestWords(int number);
        DataTable GetSmallestWords(int number);
        DataTable MostFrequencyWord(int number);
        DataTable TotalNumberOfCharactersAndWords();

        void ResetService();
    }
}