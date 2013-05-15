using System;
using System.Diagnostics;
using Windows.Storage;
using VKModel.Interfaces;

namespace VKDataLayer
{
    public class EntityStorage : IEntityStorage
    {
        private readonly ApplicationDataContainer _localSettings;
        private readonly string _key;

        public ApplicationDataContainer DataEntityStorage { get; set; }


        private string GetFullKey(Type info)
        {
            return info.Name + "_key_" + _key;
        }

        public EntityStorage()
        {
            _localSettings = ApplicationData.Current.LocalSettings;  
            _key = String.Empty;
        }

        public EntityStorage(string key)
        {
            _localSettings = ApplicationData.Current.LocalSettings;  
            _key = key;
        }

        public void SaveEntity<T>(T entity)
        {
            var fullKey = GetFullKey(typeof(T));
            //if (!localSettings.Containers.ContainsKey(typeof(T).Name))
            if (!_localSettings.Containers.ContainsKey("AppSettings"))
            {
                DataEntityStorage = _localSettings.CreateContainer("AppSettings", ApplicationDataCreateDisposition.Always);
                Debug.WriteLine("New Container created");
            }
            else
            {
                Debug.WriteLine("Container already existed");
            }
            if (_localSettings.Containers.ContainsKey("AppSettings"))
            {
                if (_localSettings.Containers["AppSettings"].Values.ContainsKey(fullKey))
                    _localSettings.Containers["AppSettings"].Values[fullKey] = entity;
                else
                    _localSettings.Containers["AppSettings"].Values.Add(fullKey, entity);
            }  
        }
        
        public T LoadEntity<T>()
        {
            var fullKey = GetFullKey(typeof (T));

            if (_localSettings.Containers.ContainsKey("AppSettings"))
            {
                if (_localSettings.Containers["AppSettings"].Values.ContainsKey(fullKey))
                    return (T)_localSettings.Containers["AppSettings"].Values[fullKey];
                Debug.WriteLine("Loading settings completed");
            }  
            return default(T);
        }

        public void DeleteEntity<T>()
        {
            var fullKey = GetFullKey(typeof (T));
            if (_localSettings.Containers.ContainsKey("AppSettings"))
            {
                if (_localSettings.Containers["AppSettings"].Values.ContainsKey(fullKey))
                    _localSettings.Containers["AppSettings"].Values.Remove(fullKey);
                Debug.WriteLine("Loading settings completed");
            }  

        }
        
    }
}
