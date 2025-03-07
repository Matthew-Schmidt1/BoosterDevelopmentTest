// See https://aka.ms/new-console-template for more information
using BoosterTest.Services;

Console.WriteLine("Hello, World!");
var stream = new Booster.CodingTest.Library.WordStream();
var statics = new StatisticsService();
for (int i = 0; i < 1000; i++)
{

    byte[] data = new byte[1024];
    await stream.ReadAsync(data);
    var str = System.Text.Encoding.Default.GetString(data);
    await statics.ConsumeString(str);
  

}
Console.WriteLine(statics.ConvertDataTableToString(statics.TotalNumberOfCharactersAndWords()));
Console.WriteLine(statics.ConvertDataTableToString(statics.GetLargestWords(5)));
Console.WriteLine(statics.ConvertDataTableToString(statics.GetSmallestWords(5)));
Console.WriteLine(statics.ConvertDataTableToString(statics.MostFrequencyWord(10)));
Console.WriteLine(statics.ConvertDataTableToString(statics.GetAllCharactersFrequency()));