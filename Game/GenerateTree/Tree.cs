using System;
using System.Collections.Generic;
using System.Drawing;
using Utils.Tool;

namespace GenerateTree
{
    public class Tree
    {
        private class Node
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

            /// <summary>
            /// 子节点
            /// </summary>
            public List<Node> children;

            public Node(float rad, float size, float length)
            {
                this.rad = rad;
                this.size = size;
                this.length = length;
                children = new List<Node>();
            }
        }

        private Node root;
        private readonly Random random;
        private readonly Config config;

        public Tree(int seed, Config config)
        {
            root = null;
            random = new Random(seed);
            MathTool.Random = random;
            this.config = config;
        }

        public void Generate()
        {
            root = new Node(config.RootRad, config.RootSize, config.RootLength);
            GenerateChildren(root, 1);
        }

        private void GenerateChildren(Node node, int depth)
        {
            if (node.length < config.MinLength || node.size < config.MinSize || depth > config.MaxDepth)
                return;
            int childrenNum = (int)MathTool.Gaussian(config.ChildrenNumMu, config.ChildrenNumSigma);
            for (int i = 0; i < childrenNum; i++)
            {
                float rad = node.rad + random.NextSingle() * config.MaxRad * 2 - config.MaxRad;
                float size = node.size * (float)MathTool.Gaussian(config.SizeChangeMu, config.SizeChangeSigma);
                float length = node.length * (float)MathTool.Gaussian(config.LengthChangeMu, config.LengthChangeSigma);
                Node child = new(rad, size, length);
                node.children.Add(child);
                GenerateChildren(child, depth + 1);
            }
        }

        public void Draw(Graphics g, Point startPoint)
        {
            if (root == null)
                return;
            DrawNode(g, startPoint, root);
        }

        private static void DrawNode(Graphics g, Point startPoint, Node node)
        {
            Point endPoint = startPoint + new Size((int)(node.length * Math.Cos(node.rad * Math.PI / 180)), (int)(node.length * Math.Sin(node.rad * Math.PI / 180)));
            g.DrawLine(new Pen(Color.Black, node.size), startPoint, endPoint);
            foreach (Node child in node.children)
            {
                DrawNode(g, endPoint, child);
            }
        }
    }
}
