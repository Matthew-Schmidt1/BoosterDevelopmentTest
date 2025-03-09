namespace FunctionTest.Services.Interfaces
{
    public interface IWordStreamService
    {
        Task<string> GetStreamDataAsync(int BufferSize = 1024);
    }
}
