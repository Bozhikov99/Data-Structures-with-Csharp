﻿namespace Tree
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
            ICollection<Tree<T>> internals = new List<Tree<T>>();
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
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetLongestPath()
        {
            throw new NotImplementedException();
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
