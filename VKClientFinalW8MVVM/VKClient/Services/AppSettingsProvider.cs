using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.Foundation.Collections;
using Windows.Storage;

namespace VKClient.Services
{
    public static class AppSettingsProvider
    {
        private readonly static IPropertySet Settings;

        static AppSettingsProvider()
        {
            AppSettingsProvider.Settings = ApplicationData.Current.LocalSettings.Values;
        }

        public static void Clear()
        {
            AppSettingsProvider.Settings.Clear();
        }

        public static T Get<T>(String settingName, T defaultValue)
        {
            T t;
            if (AppSettingsProvider.TryGet<T>(settingName, out t))
            {
                return t;
            }
            return defaultValue;
        }

        public static void Set(String settingName, String value)
        {
            AppSettingsProvider.Set<String>(settingName, value);
        }

        public static void Set<TValue>(String settingName, TValue value)
        {
            try
			{
				JsonSerializer jsonSerializer = new JsonSerializer();
				using (StringWriter stringWriter = new StringWriter())
				{
					jsonSerializer.Serialize(stringWriter, value);
					AppSettingsProvider.Settings[settingName] = stringWriter.GetStringBuilder().ToString();
				}
			}
			catch (Exception)
			{
			}
        }

        public static bool TryGet<TValue>(String settingName, out TValue value)
        {
            object obj;
			if (AppSettingsProvider.Settings.TryGetValue(settingName, out obj))
			{
				try
				{
					string s = (string)obj;
					using (StringReader stringReader = new StringReader(s))
					{
						using (JsonTextReader jsonTextReader = new JsonTextReader(stringReader))
						{
							JsonSerializer jsonSerializer = new JsonSerializer();
							value = jsonSerializer.Deserialize<TValue>(jsonTextReader);
							return true;
						}
					}
				}
				catch (Exception)
				{
					value = default(TValue);
				}
				return false;
			}
			value = default(TValue);
			return false;
        }

        [JsonObject]
        public class SerializedValue<T>
        {
            [JsonProperty]
            public T Value
            {
                get;
                set;
            }

            public SerializedValue()
            {
            }
        }
    }
    
}
