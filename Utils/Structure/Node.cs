using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils.Structure
{
    /// <summary>
    /// 树、节点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Node<T>
    {
        /// <summary>
        /// 当前节点数据
        /// </summary>
        public T Current { get; }

        /// <summary>
        /// 根节点
        /// </summary>
        public Node<T> Root => Parent == null ? this : Parent.Root;

        /// <summary>
        /// 父节点
        /// </summary>
        public Node<T> Parent { get; }

        /// <summary>
        /// 子节点
        /// </summary>
        public List<Node<T>> Children { get; set; }

        /// <summary>
        /// 兄弟节点（不包括当前节点）
        /// </summary>
        public List<Node<T>> Siblings
        {
            get
            {
                List<Node<T>> list = new();
                if (Parent == null)
                    return list;
                foreach (var node in Parent.Children)
                {
                    list.Add(node);
                }
                list.Remove(this);
                return list;
            }
        }

        /// <summary>
        /// 节点的层
        /// </summary>
        public int Level => Parent?.Level + 1 ?? 1;

        /// <summary>
        /// 以当前节点为根节点的子树的高度
        /// </summary>
        public int Height => IsLeaf ? 1 : Children.Select(node => node.Height).Max() + 1;

        /// <summary>
        /// 节点的度
        /// </summary>
        public int Degree => Children.Count;

        /// <summary>
        /// 是否为根节点
        /// </summary>
        public bool IsRoot => Parent == null;

        /// <summary>
        /// 是否为叶子节点
        /// </summary>
        public bool IsLeaf => Degree == 0;

        /// <summary>
        /// 是否有子节点
        /// </summary>
        public bool HasChild => !IsLeaf;

        public Node(T item, Node<T> parent)
        {
            Current = item;
            Parent = parent;
            Children = new();
        }

        /// <summary>
        /// 增加子节点
        /// </summary>
        /// <param name="item">子节点的数据</param>
        public void AddChildren(T item)
        {
            Node<T> node = new(item, this);
            Children.Add(node);
        }

        public static void Print(Node<T> root)
        {
            for (int i = 0; i < root.Level; i++)
            {
                Console.Write("->");
            }
            Console.WriteLine(root.Current);
            foreach (var item in root.Children)
            {
                Print(item);
            }
        }
    }
}
