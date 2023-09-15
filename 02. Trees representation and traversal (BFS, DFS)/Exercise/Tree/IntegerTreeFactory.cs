namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class IntegerTreeFactory
    {
        private Dictionary<int, IntegerTree> nodesByKey;

        public IntegerTreeFactory()
        {
            this.nodesByKey = new Dictionary<int, IntegerTree>();
        }

        public IntegerTree CreateTreeFromStrings(string[] input)
        {
            foreach (string s in input)
            {
                int[] keys = s.Split(' ')
                    .Select(int.Parse)
                    .ToArray();

                AddEdge(keys[0], keys[1]);
            }

            return GetRoot();
        }

        public IntegerTree CreateNodeByKey(int key)
        {
            if (!nodesByKey.ContainsKey(key))
            {
                IntegerTree node = new IntegerTree(key);
                nodesByKey.Add(key, node);

                return node;
            }
            else
            {
                IntegerTree node = nodesByKey[key];

                return node;
            }

        }

        public void AddEdge(int parent, int child)
        {
            IntegerTree parentNode = CreateNodeByKey(parent);
            IntegerTree childNode = CreateNodeByKey(child);

            parentNode.AddChild(childNode);
            childNode.AddParent(parentNode);
        }

        public IntegerTree GetRoot()
        {
            foreach (IntegerTree n in nodesByKey.Values)
            {
                if (n.Parent is null)
                {
                    return n;
                }
            }

            return null;
        }
    }
}
