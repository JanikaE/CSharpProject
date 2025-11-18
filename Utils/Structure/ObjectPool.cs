using System;
using System.Collections.Generic;

namespace Utils.Structure
{
    public class ObjectPool<T>
    {
        private readonly List<T> _freeElements = new();
        private readonly List<T> _busyElements = new();
        private int? _maxSize = null;
        private Func<T> _onCreate;
        private Action<T> _onRelease;
        private Action<T> _onRequest;

        public int? MaxSize 
        { 
            get 
            { 
                return _maxSize; 
            } 
            set 
            { 
                _maxSize = value;
            } 
        }

        public ObjectPool(Func<T> onCreate, Action<T> onRequest = null, Action<T> onRelease = null)
        {
            _onCreate = onCreate;
            _onRelease = onRelease;
            _onRequest = onRequest;
        }

        public void Setup(Func<T> onCreate, Action<T> onRequest = null, Action<T> onRelease = null)
        {
            _onCreate = onCreate;
            _onRelease = onRelease;
            _onRequest = onRequest;
        }

        public T GetFreeElement()
        {
            Console.WriteLine("GetFreeElement BusyCnt:{0}", _busyElements.Count);
            if (_freeElements.Count > 0)
            {
                T ret = _freeElements[0];
                _freeElements.RemoveAt(0);
                _busyElements.Add(ret);
                _onRequest?.Invoke(ret);
                return ret;
            }
            else
            {
                if (_maxSize != null && _busyElements.Count >= _maxSize)
                {
                    Console.WriteLine("exceed max");
                    FreeElement(_busyElements[0]);
                    return GetFreeElement();
                }
                else
                {
                    T ret = _onCreate();
                    _freeElements.Add(ret);
                    return GetFreeElement();
                }
            }
        }

        public void FreeElement(T obj)
        {
            Console.WriteLine("FreeElement Cnt:{0}", _busyElements.Count);
            if (_busyElements.Contains(obj))
            {
                _busyElements.Remove(obj);
                _onRelease?.Invoke(obj);
                _freeElements.Add(obj);
            }
        }
    }
}