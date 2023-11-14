using System.Threading.Channels;

namespace _06.CalculateSequence
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            Queue<int> queue = new Queue<int>();

            queue.Enqueue(n);

            while (queue.Count < 50)
            {
                queue.Enqueue(n + 1);
                queue.Enqueue(2 * n + 1);
                queue.Enqueue(n + 2);

                n+= 1;
            }

            Console.WriteLine(string.Join(", ", queue));
        }
    }
}