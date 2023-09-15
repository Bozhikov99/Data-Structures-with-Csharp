namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly T value;
        private Tree<T> parent;
        private IList<Tree<T>> children;

        public Tree(T value)
        {
            this.value = value;
            children = new List<Tree<T>>();
        }

        public Tree(T value, params Tree<T>[] children)
            : this(value)
        {
            this.value = value;

            foreach (Tree<T> c in children)
            {
                this.children.Add(c);
                c.parent = this;
            }
        }

        public void AddChild(T parentKey, Tree<T> child)
        {
            Tree<T> parent = FindBfs(parentKey);

            child.parent = parent;
            parent.children.Add(child);
        }

        public IEnumerable<T> OrderBfs()
        {
            List<T> result = new List<T>();
            Queue<Tree<T>> queue = new Queue<Tree<T>>();

            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                Tree<T> current = queue.Dequeue();
                result.Add(current.value);

                foreach (Tree<T> c in current.children)
                {
                    queue.Enqueue(c);
                }
            }

            return result;
        }

        public IEnumerable<T> OrderDfs()
        {
            Stack<T> result = new Stack<T>();
            Stack<Tree<T>> stack = new Stack<Tree<T>>();
            stack.Push(this);

            while (stack.Count > 0)
            {
                Tree<T> current = stack.Pop();
                result.Push(current.value);

                foreach (Tree<T> c in current.children)
                {
                    stack.Push(c);
                }
            }

            return result;
        }

        public void RemoveNode(T nodeKey)
        {
            Tree<T> subtree = FindBfs(nodeKey);

            if (subtree.parent is null)
            {
                throw new ArgumentException();
            }

            Tree<T> parent = subtree.parent;

            parent.children
                .Remove(subtree);

            subtree.parent = null;
        }

        public void Swap(T firstKey, T secondKey)
        {
            Tree<T> firstTree = FindBfs(firstKey);
            Tree<T> secondTree = FindBfs(secondKey);

            EnsureNotRoot(firstTree, secondTree);

            Tree<T> firstParent = firstTree.parent;
            Tree<T> secondParent = secondTree.parent;

            int firstIndex = firstParent.children
                .IndexOf(firstTree);

            int secondIndex = secondParent.children
                .IndexOf(secondTree);

            firstParent.children[firstIndex] = secondTree;
            secondTree.parent = firstParent;

            secondParent.children[secondIndex] = firstTree;
            firstTree.parent = secondParent;
        }

        private Tree<T> FindBfs(T key)
        {
            Queue<Tree<T>> queue = new Queue<Tree<T>>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                Tree<T> current = queue.Dequeue();

                if (current.value.Equals(key))
                {
                    return current;
                }

                foreach (Tree<T> c in current.children)
                {
                    queue.Enqueue(c);
                }
            }

            throw new ArgumentNullException();
        }

        private Tree<T> FindDfs(T key)
        {
            Tree<T> match = Dfs(this, key);

            return match;
        }

        private void EnsureNotRoot(params Tree<T>[] trees)
        {
            foreach (Tree<T> t in trees)
            {
                if (t.parent is null)
                {
                    throw new ArgumentException();
                }
            }
        }

        private IEnumerable<T> OrderDfsRecursive()
        {
            ICollection<T> list = new List<T>();
            Dfs(this, list);

            return list;
        }

        private void Dfs(Tree<T> tree, ICollection<T> list)
        {
            foreach (Tree<T> c in tree.children)
            {
                Dfs(c, list);
            }

            list.Add(tree.value);
        }

        private Tree<T> Dfs(Tree<T> tree, T key)
        {
            if (tree.value.Equals(key))
            {
                return tree;
            }

            foreach (Tree<T> c in tree.children)
            {
                Dfs(c, key);
            }

            throw new ArgumentException();
        }
    }
}
