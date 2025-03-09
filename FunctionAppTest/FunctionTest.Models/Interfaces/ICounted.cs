namespace FunctionTest.Models.Interfaces
{
    public interface ICounted<T>
    {
        int Length { get; }
        T Entry { get; set; }
        int Count { get; }
        void IncreaseCount();
    }
}