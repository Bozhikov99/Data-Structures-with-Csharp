namespace Tree
{
    using System;
    using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetLeafKeys()
        {
            throw new NotImplementedException();
        }

        public T GetDeepestKey()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetLongestPath()
        {
            throw new NotImplementedException();
        }
    }
}
