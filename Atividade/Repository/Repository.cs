namespace Atividade.Repository
{
    public abstract class Repository<T> : IDisposable
    {
        
        public abstract T? GetById(string id);
        public abstract List<T> GetAll();
        public abstract bool Update(T item);
        public abstract T? DeleteById(string id);
        public abstract T? Create(T item);

        public void Dispose()
        {
            
        }
    }
}