namespace Jobeer.Models.Base
{
    public class KeyEntity<T>: BaseEntity
    {
        public T Key {  get; set; }
    }
}
