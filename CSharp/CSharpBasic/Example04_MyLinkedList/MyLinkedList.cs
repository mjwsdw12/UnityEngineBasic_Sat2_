using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example04_MyLinkedList
{
    // sealed 제한자 : 해당 클래스는 상속받을 수 없다.
    public sealed class Node<K>
    {
        public K value;
        public Node<K> prev;
        public Node<K> next;

        public Node(K value)
        {
            // this : 객체 자기 자신 참조 하는 키워드
            this.value = value;
        }
    }
    internal class MyLinkedList<T>
    {
        private Node<T> _first, _last, _tmp1, _tmp2;

        // 삽입 알고리즘
        // O(1)
        public void AddFirst(T value)
        {
            _tmp1 = new Node<T>(value);
            if (_first != null)
            {
                _tmp1.next = _first;
                _first.prev = _tmp1;
            }
            if (_last == null)
                _last = _tmp1;
            _first = _tmp1;
        }

        public void AddLast(T value)
        {
            _tmp1 = new Node<T>(value);
            if (_last != null)
            {
                _tmp1.prev = _last;
                _last.next = _tmp1;
            }
            if (_first == null)
                _first = _tmp1;
            _last = _tmp1;
        }

        public void AddBefore(Node<T> node, T value)
        {
            if (node == null)
                return;

            _tmp1 = new Node<T>(value);

            if (node.prev != null)
            {
                node.prev.next = _tmp1;
                _tmp1.prev = node.prev;
            }

            node.prev = _tmp1;
            _tmp1.next = node;

            if (node == _first)
                _first = _tmp1;
        }

        public void AddAfier(Node<T> node, T value)
        {
            if (node == null)
                return;

            _tmp1 = new Node<T>(value);

            if (node.next != null)
            {
                node.next.next = _tmp1;
                _tmp1.next = node.next;
            }

            node.next = _tmp1;
            _tmp1.prev = node;

            if (node == _last)
                _last = _tmp1;
        }

        // 탐색 알고리즘
        // O(N)
        public Node<T> Find(int value)
        {
            _tmp1 = _first;
            while (_tmp1 != null)
            {
                if (Comparer<T>.Default.Compare(_tmp1.value, value) == 0)
                    return _tmp1;
                _tmp1 = _tmp1.next;
            }
            return null;
        }

        public Node<T> FindLast(int value)
        {
            _tmp1 = _last;
            while (_tmp1 != null)
            {
                if (Comparer<T>.Default.Compare(_tmp1.value, value) == 0)
                    return _tmp1;
                _tmp1 = _tmp1.next;
            }
            return null;
        }

        // 삭제 알고리즘
        // O(N)
        public bool Remove(T value)
        {
            _tmp1 = Find(value);
            if (_tmp1 != null)
            {
                if (_tmp1.prev != null)
                    _tmp1.prev.next = _tmp1.next;
                if(_tmp1.next != null)
                    _tmp1.next.prev = _tmp1.prev;
                _tmp1.next = null;
                _tmp1.prev = null;
                _tmp1 = null;
                return true;
            }
            return false;
        }

        public bool RemoveLast(T value)
        {
            _tmp1 = FindLast(value);
            if (_tmp1 != null)
            {
                if (_tmp1.prev != null)
                    _tmp1.prev.next = _tmp1.next;
                if (_tmp1.next != null)
                    _tmp1.next.prev = _tmp1.prev;
                _tmp1.next = null;
                _tmp1.prev = null;
                _tmp1 = null;
                return true;
            }
            return false;
        }
    }
}
