using System;
using System.Collections.Generic;

namespace Utilities
{
    public class EnumDictionary<TEnum, TValue> where TEnum : Enum
    {
        private readonly Dictionary<TEnum, TValue> _dictionary;

        public EnumDictionary()
        {
            _dictionary = new Dictionary<TEnum, TValue>();
            foreach (TEnum enumValue in Enum.GetValues(typeof(TEnum)))
            {
                _dictionary.Add(enumValue, default(TValue));
            }
        }

        public TValue this[TEnum key]
        {
            get => _dictionary[key];
            set => _dictionary[key] = value;
        }

        public bool ContainsKey(TEnum key)
        {
            return _dictionary.ContainsKey(key);
        }

        public bool TryGetValue(TEnum key, out TValue value)
        {
            return _dictionary.TryGetValue(key, out value);
        }

        public void Add(TEnum key, TValue value)
        {
            _dictionary.Add(key, value);
        }

        public void Clear()
        {
            _dictionary.Clear();
        }

        public Dictionary<TEnum, TValue>.Enumerator GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        public int Count => _dictionary.Count;

        public ICollection<TEnum> Keys => _dictionary.Keys;

        public ICollection<TValue> Values => _dictionary.Values;
    }
}