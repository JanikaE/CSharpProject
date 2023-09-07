using System.Collections.Generic;

namespace PENet
{
    /// <summary>
    /// IOCP会话连接Token缓存池
    /// </summary>
    public class IOCPTokenPool<T, K>
        where T : IOCPToken<K>, new()
        where K : IOCPMsg, new()
    {
        Stack<T> stk;
        public int size => stk.Count;

        public IOCPTokenPool(int capacity)
        {
            stk = new Stack<T>(capacity);
        }

        public T Pop()
        {
            lock (stk)
            {
                return stk.Pop();
            }
        }

        public void Push(T token)
        {
            if (token == null)
            {
                IOCPTool.Error("Push Token To Pool Cannot Be Null");
                return;
            }
            lock (stk)
            {
                stk.Push(token);
            }
        }
    }
}
