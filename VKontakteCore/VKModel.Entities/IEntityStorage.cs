using System.Threading.Tasks;

namespace VKModel
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
