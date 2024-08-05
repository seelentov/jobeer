namespace Jobeer.Services.Interfaces
{
    public interface IFactory<T>
    {
        public T Get();
    }
}
