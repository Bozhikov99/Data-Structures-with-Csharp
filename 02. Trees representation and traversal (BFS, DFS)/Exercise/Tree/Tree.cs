namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;

    public class Tree<T> : IAbstractTree<T>
    {
        private List<Tree<T>> children;

        public Tree(T key, params Tree<T>[] children)
        {
            Key = key;

            this.children = new List<Tree<T>>();

            foreach (Tree<T> c in children)
            {
                this.children.Add(c);
            }
        }

        public T Key { get; private set; }

        public Tree<T> Parent { get; private set; }

        public IReadOnlyCollection<Tree<T>> Children => children.AsReadOnly();

        public void AddChild(Tree<T> child)
        {
            children.Add(child);
        }

        public void AddParent(Tree<T> parent)
        {
            Parent = parent;
        }

        public string AsString()
        {
            StringBuilder builder = new StringBuilder();

            DfsString(this, 0, builder);
            string output = builder.ToString()
                            .Trim();

            return output;
        }

        private void DfsString(Tree<T> tree, int identationLevel, StringBuilder builder)
        {
            builder.Append(' ', identationLevel++ * 2);
            builder.AppendLine($"{tree.Key}");

            foreach (Tree<T> c in tree.children)
            {
                DfsString(c, identationLevel, builder);
            };
        }

        public IEnumerable<T> GetInternalKeys()
        {
            ICollection<Tree<T>> internals = new HashSet<Tree<T>>();
            Func<Tree<T>, bool> condition = t => t.children.Count != 0 && t.Parent != null;
            Dfs(this, internals, condition);

            IEnumerable<T> result = internals.Select(t => t.Key);

            return result;
        }

        public IEnumerable<T> GetLeafKeys()
        {
            ICollection<Tree<T>> leaves = new HashSet<Tree<T>>();
            Func<Tree<T>, bool> condition = t => t.children.Count == 0;
            Dfs(this, leaves, condition);

            IEnumerable<T> result = leaves.Select(t => t.Key);

            return result;
        }

        public T GetDeepestKey()
        {
            Func<Tree<T>, bool> leafCondition = t => t.children.Count == 0;
            ICollection<Tree<T>> leaves = new HashSet<Tree<T>>();
            Dfs(this, leaves, leafCondition);

            T deepestKey = default;
            int maxDepth = 0;

            foreach (Tree<T> node in leaves)
            {
                int currentDepth = GetDepth(node);

                if (currentDepth > maxDepth)
                {
                    maxDepth = currentDepth;
                    deepestKey = node.Key;
                }
            }

            return deepestKey;
        }

        public IEnumerable<T> GetLongestPath()
        {
            IEnumerable<T> longestPath = new HashSet<T>();
            ICollection<Tree<T>> leaves = new HashSet<Tree<T>>();
            Func<Tree<T>, bool> condition = t => t.children.Count == 0;
            Dfs(this, leaves, condition);

            foreach (Tree<T> l in leaves)
            {
                IEnumerable<T> currentPath = GetPath(l);

                if (currentPath.Count() > longestPath.Count())
                {
                    longestPath = currentPath;
                }
            }

            return longestPath.Reverse();
        }

        private IEnumerable<T> GetPath(Tree<T> tree)
        {
            HashSet<T> path = new HashSet<T>();
            Tree<T> node = tree;

            while (node != null)
            {
                T key = node.Key;
                path.Add(key);
                node = node.Parent;
            }

            return path;
        }

        private int GetDepth(Tree<T> node)
        {
            int depth = 0;

            while (node != null)
            {
                depth++;
                node = node.Parent;
            }

            return depth;
        }

        private void Dfs(Tree<T> node, ICollection<Tree<T>> collection, Func<Tree<T>, bool> condition)
        {
            if (condition(node))
            {
                collection.Add(node);
            }

            foreach (Tree<T> c in node.children)
            {
                Dfs(c, collection, condition);
            }
        }
    }
}
