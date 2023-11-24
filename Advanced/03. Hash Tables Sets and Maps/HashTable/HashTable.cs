namespace HashTable
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
    {
        private const int DEFAULT_CAPACITY = 4;
        private const float LOAD_FACTOR = 0.75f;

        private LinkedList<KeyValue<TKey, TValue>>[] table;

        public int Count { get; private set; }

        public int Capacity => table.Length;

        public HashTable()
        {
            table = new LinkedList<KeyValue<TKey, TValue>>[DEFAULT_CAPACITY];
        }

        public HashTable(int capacity)
        {
            table = new LinkedList<KeyValue<TKey, TValue>>[capacity];
        }

        private HashTable(HashTable<TKey, TValue> table)
        {
            int capacity = table.Capacity * 2;
            this.table = new LinkedList<KeyValue<TKey, TValue>>[capacity];

            foreach (KeyValue<TKey, TValue> kvp in table)
            {
                Add(kvp.Key, kvp.Value);
            }
        }

        public void Add(TKey key, TValue value)
        {
            GrowIfNeeded();

            int index = GetIndex(key);

            LinkedList<KeyValue<TKey, TValue>> currentList = table[index];
            KeyValue<TKey, TValue> newPair = new KeyValue<TKey, TValue>(key, value);

            if (currentList == null)
            {
                currentList = new LinkedList<KeyValue<TKey, TValue>>();
                table[index] = currentList;
            }
            else if (GetPairByKey(key) != null)
            {
                throw new ArgumentException();
            }

            currentList.AddLast(newPair);
            Count++;
        }

        public bool AddOrReplace(TKey key, TValue value)
        {
            int index = GetIndex(key);

            if (table[index] == null)
            {
                table[index] = new LinkedList<KeyValue<TKey, TValue>>();
            }

            KeyValue<TKey, TValue> duplicate = GetPairByKey(key);

            if (duplicate is null)
            {
                try
                {
                    Add(key, value);
                }
                catch (Exception)
                {
                    return false;
                }

                return true;
            }

            duplicate.Value = value;

            return true;
        }

        public TValue Get(TKey key)
        {
            KeyValue<TKey, TValue> pair = GetPairByKey(key);

            if (pair == null)
            {
                throw new KeyNotFoundException();
            }

            return pair.Value;
        }

        public TValue this[TKey key]
        {
            get
            {
                int index = GetIndex(key);

                if (table[index] != null)
                {
                    foreach (KeyValue<TKey, TValue> kvp in table[index])
                    {
                        if (kvp != null)
                        {
                            TKey currentKey = kvp.Key;

                            if (currentKey.Equals(key))
                            {
                                return kvp.Value;
                            }
                        }
                    }
                }

                throw new KeyNotFoundException();
            }
            set
            {
                AddOrReplace(key, value);
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var element = Find(key);

            if (element != null)
            {
                value = element.Value;
                return true;
            }

            value = default;
            return false;
        }

        public KeyValue<TKey, TValue> Find(TKey key)
        {
            KeyValue<TKey, TValue> pair = GetPairByKey(key);

            return pair;
        }

        public bool ContainsKey(TKey key)
        {
            KeyValue<TKey, TValue> pair = GetPairByKey(key);

            return pair != null;
        }

        public bool Remove(TKey key)
        {
            int index = GetIndex(key);

            KeyValue<TKey, TValue> pair = GetPairByKey(key);

            if (pair == null)
            {
                return false;
            }

            table[index].Remove(pair);
            Count--;

            return true;
        }

        public void Clear()
        {
            table = new LinkedList<KeyValue<TKey, TValue>>[DEFAULT_CAPACITY];
            Count = 0;
        }

        public IEnumerable<TKey> Keys => this.Select(l => l.Key);

        public IEnumerable<TValue> Values => this.Select(l => l.Value);

        public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
        {
            foreach (LinkedList<KeyValue<TKey, TValue>> l in table)
            {
                if (l != null)
                {
                    foreach (KeyValue<TKey, TValue> kvp in l)
                    {
                        yield return kvp;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private int GetIndex(TKey key)
        {
            int index = Math.Abs(key.GetHashCode() % Capacity);

            return index;
        }

        public void GrowIfNeeded()
        {
            if ((float)(Count + 1) / Capacity > LOAD_FACTOR)
            {
                Grow();
            }
        }

        private void Grow()
        {
            HashTable<TKey, TValue> newTable = new HashTable<TKey, TValue>(this);
            table = newTable.table;
        }

        private KeyValue<TKey, TValue> GetPairByKey(TKey key)
        {
            int index = GetIndex(key);

            if (table[index] != null)
            {
                foreach (KeyValue<TKey, TValue> kvp in table[index])
                {
                    if (kvp != null)
                    {
                        TKey currentKey = kvp.Key;

                        if (currentKey.Equals(key))
                        {
                            return kvp;
                        }
                    }
                }
            }
            return null;
        }
    }
}
