using System;
using System.Drawing;
using Utils.Structure;
using Utils.Tool;

namespace GenerateTree
{
    public class Tree
    {
        private readonly Node<Leaf> root;
        private readonly Config config;

        public Tree(int seed, Config config)
        {
            root = new(new(config.RootRad, config.RootSize, config.RootLength), null);
            RandomTool.Random = new Random(seed);
            this.config = config;
        }

        public void Generate()
        {
            GenerateChildren(root);
        }

        private void GenerateChildren(Node<Leaf> node)
        {
            if (node.Current.length < config.MinLength || node.Current.size < config.MinSize || node.Level >= config.MaxDepth)
                return;
            int childrenNum = (int)RandomTool.Gaussian(config.ChildrenNumMu, config.ChildrenNumSigma);
            for (int i = 0; i < childrenNum; i++)
            {
                float rad = node.Current.rad + RandomTool.Single(-1, 1) * config.MaxRad;
                float size = node.Current.size * (float)RandomTool.Gaussian(config.SizeChangeMu, config.SizeChangeSigma);
                float length = node.Current.length * (float)RandomTool.Gaussian(config.LengthChangeMu, config.LengthChangeSigma);
                Node<Leaf> child = new(new(rad, size, length), node);
                node.Children.Add(child);
                GenerateChildren(child);
            }
        }

        public void Draw(Graphics g, Point startPoint)
        {
            if (root == null)
                return;
            DrawNode(g, startPoint, root);
        }

        private static void DrawNode(Graphics g, Point startPoint, Node<Leaf> node)
        {
            Point endPoint = startPoint + new Size((int)(node.Current.length * Math.Cos(node.Current.rad * Math.PI / 180)), (int)(node.Current.length * Math.Sin(node.Current.rad * Math.PI / 180)));
            g.DrawLine(new Pen(Color.Black, node.Current.size), startPoint, endPoint);
            foreach (Node<Leaf> child in node.Children)
            {
                DrawNode(g, endPoint, child);
            }
        }
    }

    public class Leaf
    {
        /// <summary>
        /// 角度
        /// </summary>
        public float rad;
        /// <summary>
        /// 大小
        /// </summary>
        public float size;
        /// <summary>
        /// 长度
        /// </summary>
        public float length;

        public Leaf(float rad, float size, float length)
        {
            this.rad = rad;
            this.size = size;
            this.length = length;
        }
    }
}
