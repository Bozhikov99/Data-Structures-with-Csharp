namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class IntegerTree : Tree<int>, IIntegerTree
    {
        public IntegerTree(int key, params Tree<int>[] children)
            : base(key, children)
        {
        }

        public IEnumerable<IEnumerable<int>> GetPathsWithGivenSum(int sum)
        {
            ICollection<IEnumerable<int>> result = new List<IEnumerable<int>>();
            ICollection<Tree<int>> leaves = new HashSet<Tree<int>>();
            Dfs(this, leaves, leafCondition);

            foreach (Tree<int> l in leaves)
            {
                IEnumerable<int> currentPath = GetPath(l);

                if (currentPath.Sum() == sum)
                {
                    result.Add(currentPath);
                }
            }

            return result;
        }

        public IEnumerable<Tree<int>> GetSubtreesWithGivenSum(int sum)
        {
            ICollection<Tree<int>> subtreeCollection = new List<Tree<int>>();

            _ = DfsSubtreeSum(this, sum, subtreeCollection);

            return subtreeCollection;
        }

        private IEnumerable<int> GetPath(Tree<int> node)
        {
            ICollection<int> path = new HashSet<int>();

            Tree<int> current = node;

            while (current != null)
            {
                path.Add(current.Key);
                current = current.Parent;
            }

            return path.Reverse();
        }

        private int DfsSubtreeSum(Tree<int> node, int sum, ICollection<Tree<int>> subtrees)
        {
            int currentSum = node.Key;

            foreach (Tree<int> c in node.Children)
            {
                currentSum += DfsSubtreeSum(c, sum, subtrees);
            }

            if (currentSum == sum)
            {
                subtrees.Add(node);
            }

            return currentSum;
        }
    }
}
