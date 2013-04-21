using System;
using System.Reflection;
using VKModel.Interfaces;
using Windows.Storage;

namespace VKDataLayer
{
    public class EntityStorage : IEntityStorage
    {
        private ApplicationDataContainer localSettings;
        private ApplicationDataContainer dataEntityStorage;
        private readonly string key;


        private string GetFullKey(MemberInfo info)
        {
            return info.Name + "_key" + key;
        }

        public EntityStorage()
        {
            localSettings = ApplicationData.Current.LocalSettings;  
            key = String.Empty;
        }

        public EntityStorage(string key)
        {
            localSettings = ApplicationData.Current.LocalSettings;  
            this.key = key;
        }

        public void SaveEntity<T>(T item)
        {
            var fullKey = GetFullKey(typeof(T).);

        }

        public T LoadEntity<T>()
        {
            return default(T);
        }

        public void DeleteEntity<T>()
        {
            
        }

    }
}
