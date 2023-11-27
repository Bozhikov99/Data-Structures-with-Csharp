namespace RoyaleArena
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class Arena : IArena
    {
        private const int DEFAULT_CAPACITY = 4;
        private const float LOAD_FACTOR = 0.70f;
        private LinkedList<BattleCard>[] deck;
        private int capacity => deck.Length;

        public int Count { get; private set; }

        public Arena()
        {
            deck = new LinkedList<BattleCard>[DEFAULT_CAPACITY];
        }

        public Arena(int capacity)
        {
            deck = new LinkedList<BattleCard>[capacity];
        }

        private Arena(Arena oldArena)
        {
            int capacity = oldArena.capacity * 2;
            deck = new LinkedList<BattleCard>[capacity];

            foreach (BattleCard card in oldArena)
            {
                Add(card);
            }
        }

        public void Add(BattleCard card)
        {
            Grow();

            int index = GetIndex(card.Id);

            if (deck[index] is null)
            {
                deck[index] = new LinkedList<BattleCard>();
            }

            deck[index].AddLast(card);

            Count++;
        }

        private void Grow()
        {
            if ((float)(Count + 1) / capacity > LOAD_FACTOR)
            {
                Arena newArena = new Arena(this);
                deck = newArena.deck;
            }
        }

        public void ChangeCardType(int id, CardType type)
        {
            int index = GetIndex(id);

            if (deck[index] is null)
            {
                throw new InvalidOperationException();
            }

            foreach (BattleCard card in deck[index])
            {
                if (card != null && card.Id == id)
                {
                    card.Type = type;
                }
            }
        }

        public bool Contains(BattleCard card)
        {
            int index = GetIndex(card.Id);

            if (deck[index] is null)
            {
                return false;
            }

            foreach (BattleCard c in deck[index].Where(c => c != null))
            {
                if (c.CompareTo(card) == 0)
                {
                    return true;
                }
            }

            return false;
        }

        public IEnumerable<BattleCard> FindFirstLeastSwag(int n)
        {
            if (n > Count)
            {
                throw new InvalidOperationException();
            }

            ICollection<BattleCard> set = new HashSet<BattleCard>();

            foreach (LinkedList<BattleCard> list in deck.Where(l => l != null))
            {
                foreach (BattleCard card in list)
                {
                    if (card != null)
                    {
                        set.Add(card);
                    }
                }
            }

            if (set.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return set.OrderBy(c => c.Swag).ThenBy(c => c.Id).Take(n);
        }

        public IEnumerable<BattleCard> GetAllInSwagRange(double lo, double hi)
        {
            ICollection<BattleCard> set = new HashSet<BattleCard>();

            foreach (LinkedList<BattleCard> list in deck.Where(l => l != null))
            {
                foreach (BattleCard card in list.Where(c => c.Swag >= lo && c.Swag <= hi))
                {
                    if (card != null)
                    {
                        set.Add(card);
                    }
                }
            }

            return set.OrderBy(c => c.Swag);
        }

        public IEnumerable<BattleCard> GetByCardType(CardType type)
        {
            ICollection<BattleCard> set = new HashSet<BattleCard>();

            foreach (LinkedList<BattleCard> list in deck.Where(l => l != null))
            {
                foreach (BattleCard card in list.Where(c => c != null))
                {
                    if (card.Type.Equals(type))
                    {
                        set.Add(card);
                    }
                }
            }

            if (set.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return Order(set);
        }

        public IEnumerable<BattleCard> GetByCardTypeAndMaximumDamage(CardType type, double damage)
        {
            ICollection<BattleCard> set = new HashSet<BattleCard>();

            foreach (LinkedList<BattleCard> list in deck.Where(l => l != null))
            {
                foreach (BattleCard card in list.Where(c => c.Damage <= damage))
                {
                    if (card.Type.Equals(type))
                    {
                        set.Add(card);
                    }
                }
            }

            if (set.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return Order(set);
        }

        public BattleCard GetById(int id)
        {
            int index = GetIndex(id);

            if (deck[index] is null)
            {
                throw new InvalidOperationException();
            }

            foreach (BattleCard card in deck[index].Where(c => c != null))
            {
                if (card.Id == id)
                {
                    return card;
                }
            }

            throw new InvalidOperationException();
        }

        public IEnumerable<BattleCard> GetByNameAndSwagRange(string name, double lo, double hi)
        {
            ICollection<BattleCard> set = new HashSet<BattleCard>();

            foreach (LinkedList<BattleCard> list in deck.Where(l => l != null))
            {
                foreach (BattleCard card in list.Where(c => c.Swag >= lo && c.Swag < hi))
                {
                    if (card.Name.Equals(name))
                    {
                        set.Add(card);
                    }
                }
            }

            if (set.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return OrderBySwag(set);
        }

        public IEnumerable<BattleCard> GetByNameOrderedBySwagDescending(string name)
        {
            ICollection<BattleCard> set = new HashSet<BattleCard>();

            foreach (LinkedList<BattleCard> list in deck.Where(l => l != null))
            {
                foreach (BattleCard card in list)
                {
                    if (card.Name.Equals(name))
                    {
                        set.Add(card);
                    }
                }
            }

            if (set.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return OrderBySwag(set);
        }

        public IEnumerable<BattleCard> GetByTypeAndDamageRangeOrderedByDamageThenById(CardType type, int lo, int hi)
        {
            ICollection<BattleCard> set = new HashSet<BattleCard>();

            foreach (LinkedList<BattleCard> list in deck.Where(l => l != null))
            {
                foreach (BattleCard card in list.Where(c => c.Damage >= lo && c.Damage < hi))
                {
                    if (card.Type.Equals(type))
                    {
                        set.Add(card);
                    }
                }
            }

            if (set.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return Order(set);
        }

        public IEnumerator<BattleCard> GetEnumerator()
        {
            foreach (LinkedList<BattleCard> currentList in deck)
            {
                if (currentList != null)
                {
                    foreach (BattleCard card in currentList)
                    {
                        yield return card;
                    }
                }
            }
        }

        public void RemoveById(int id)
        {
            int index = GetIndex(id);

            BattleCard card = GetCardById(id);

            if (card is null)
            {
                throw new InvalidOperationException();
            }

            deck[index].Remove(card);

            Count--;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private int GetIndex(int id)
        {
            int index = Math.Abs(id % capacity);

            return index;
        }

        private BattleCard GetCardById(int id)
        {
            int index = GetIndex(id);

            if (deck[index] is null)
            {
                return null;
            }

            foreach (BattleCard card in deck[index].Where(c => c != null))
            {
                if (card.Id == id)
                {
                    return card;
                }
            }

            return null;
        }

        private IEnumerable<BattleCard> Order(IEnumerable<BattleCard> cards)
        {
            return cards.OrderByDescending(c => c.Damage)
                .ThenBy(c => c.Id);
        }
        private IEnumerable<BattleCard> OrderBySwag(IEnumerable<BattleCard> cards)
        {
            return cards.OrderByDescending(c => c.Swag)
                .ThenBy(c => c.Id);
        }
    }
}