using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example05_MyHashtable
{
    internal class MyHashtable<T, K>
    {
        private const int DEFAULT_SIZE = 1000;
        private LinkedList<K>[] _bucket = new LinkedList<K>[DEFAULT_SIZE];
        private int _tmpHash;

        // 삽입 알고리즘
        // Hash 함수 시간을 무시한 이상적인 경우에 O(1) 
        public void Add(T key, K value)
        {
            _tmpHash = Hash(key);
            if (_bucket[_tmpHash] == null)
                _bucket[_tmpHash] = new LinkedList<K>();
            _bucket[_tmpHash].AddLast(value);
        }

        // 탐색 알고리즘
        // Hash 함수 시간을 무시한 이상적인 경우에 O(1) 
        public bool ContainsKey(T target)
        {
            _tmpHash = Hash(target);
            if (_bucket[_tmpHash] != null &&
                _bucket[_tmpHash].Count > 0)
                return true;
            return false;
        }

        public bool TryGetValue(T key, out K value)
        {
            bool isOK = true;
            value = default(K);
            _tmpHash = Hash(key);

            try
            {
                value = _bucket[_tmpHash].First();
            }
            catch
            {
                isOK = false;
            }

            return isOK;
        }

        // 삭제 알고리즘
        // Hash 함수 시간을 무시한 이상적인 경우에 O(1) 
        public bool Remove(T key)
        {
            _tmpHash = Hash(key);
            if (_bucket[_tmpHash] != null)
            {
                _bucket[_tmpHash].Clear();
                _bucket[_tmpHash] = null;
                return true;
            }
            return false;
        }

        public void Clear()
        {
            for (int i = 0; i < _bucket.Length; i++)
            {
                if (_bucket[i] != null)
                {
                    _bucket[i].Clear();
                    _bucket[i] = null;
                }
            }
        }

        private int Hash(T key)
        {
            _tmpHash = 0;
            string tmpString = key.ToString();
            for (int i = 0; i < tmpString.Length; i++)
            {
                _tmpHash += tmpString[i];
            }
            _tmpHash %= DEFAULT_SIZE;
            return _tmpHash;
        }
    }
}
