using FunctionTest.Models;
using System.Data;

namespace FunctionTest.Services.Interfaces
{
    public interface IStatisticsService
    {
        Task ConsumeString(string str);
        ResultsTable GetAllCharactersFrequency();
        ResultsTable GetLargestWords(int number);
        ResultsTable GetSmallestWords(int number);
        ResultsTable MostFrequencyWord(int number);
        ResultsTable TotalNumberOfCharactersAndWords();

        void ResetService();
    }
}