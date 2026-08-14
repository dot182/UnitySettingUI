using System;
using UnityEngine;
using System.Runtime.CompilerServices;

namespace Uni.Settings
{
    [Serializable]
    public class PlayerPrefsSaveMethod : ISaveMethod
    {
        public T Get<T>(string key, T defaultValue = default)
        {
            if (!PlayerPrefs.HasKey(key)) return defaultValue;

            Type type = typeof(T);

            // Reference types (string) do not suffer from boxing overhead anyway
            if (type == typeof(string))
            {
                string val = PlayerPrefs.GetString(key);
                return Unsafe.As<string, T>(ref val);
            }
            if (type == typeof(int))
            {
                int val = PlayerPrefs.GetInt(key);
                return Unsafe.As<int, T>(ref val);
            }
            if (type == typeof(float))
            {
                float val = PlayerPrefs.GetFloat(key);
                return Unsafe.As<float, T>(ref val);
            }
            return defaultValue;
        }
        public void SaveToDisk()
        {
            PlayerPrefs.Save();
        }

        public void Set<T>(string key, T value)
        {
            if (value is float f)
            {
                PlayerPrefs.SetFloat(key, f);
            }
            else if (value is string s)
            {
                PlayerPrefs.SetString(key, s);
            }
            else if (value is int i)
            {
                PlayerPrefs.SetInt(key, i);
            }
            else if (value is bool b)
            {
                PlayerPrefs.SetInt(key, b ? 1 : 0);
            }
        }
    }
}