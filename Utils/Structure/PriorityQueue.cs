using System;

namespace Utils.Structure
{
    /// <summary>
    /// 优先队列
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PriorityQueue<T> where T : IComparable
    {
        private readonly T[] heap;
        private int count;
        private readonly HeapType type;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="size">队列大小</param>
        /// <param name="type">大根堆/小根堆</param>
        public PriorityQueue(int size, HeapType type)
        {
            count = 0;
            heap = new T[size + 1];
            this.type = type;
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        public bool IsEmpty => count == 0;

        /// <summary>
        /// 获取堆顶值
        /// </summary>
        public T Top => heap[1];

        /// <summary>
        /// 将元素放入堆底
        /// </summary>
        /// <param name="value"></param>
        public void Push(T value)
        {
            heap[++count] = value;
            Swim();
        }

        /// <summary>
        /// 获取并弹出堆顶上的值
        /// </summary>
        public T Pop()
        {
            T ret = heap[1];
            count--;
            Sink();
            return ret;
        }

        private void Swap(int i, int j)
        {
            (heap[j], heap[i]) = (heap[i], heap[j]);
        }

        /// <summary>
        /// 堆底元素上移
        /// </summary>
        private void Swim()
        {
            int k = count;
            switch (type)
            {

                case HeapType.MinHeap:
                    while (k > 1 && heap[k - 1].CompareTo(heap[k]) > 0)
                    {
                        Swap(k - 1, k);
                        k--;
                    }
                    break;
                case HeapType.MaxHeap:
                    while (k > 1 && heap[k - 1].CompareTo(heap[k]) < 0)
                    {
                        Swap(k - 1, k);
                        k--;
                    }
                    break;
            }
        }

        /// <summary>
        /// 堆顶元素下移
        /// </summary>
        private void Sink()
        {
            int k = 1;
            while (k <= count)
            {
                Swap(k, k + 1);
                k++;
            }
        }        
    }

    public enum HeapType
    {
        MaxHeap,
        MinHeap
    }
}
