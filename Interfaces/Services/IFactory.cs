namespace Jobeer.Services.Interfaces
{
    public interface IFactory<T,V>
    {
        public T Get(V source);
    }
}
