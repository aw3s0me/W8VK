namespace VKClient.Models
{
    namespace Interfaces
    {
        public interface IEntityStorage
        {
            void SaveEntity<T>(T entity);

            T LoadEntity<T>();

            void DeleteEntity<T>();
        }
    }

    
}
