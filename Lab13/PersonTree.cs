using Lab10Lib.Models;
using Lab12Lib.BinaryTree;

namespace Lab13
{
    public class PersonTree : BinaryTree<Person>
    {
        public delegate void PersonTreeEventHandler(object source, PersonTreeEventArgs args);

        public event PersonTreeEventHandler? CountChanged = null;

        public string GlobalKey { get; private set; }

        public PersonTree(string? key = null) : base()
            => GlobalKey = key ?? $"GlobalKey#{GetHashCode()}";

        public PersonTree(int capacity, string? key = null) : base(capacity)
            => GlobalKey = key ?? $"GlobalKey#{GetHashCode()}";

        public PersonTree(BinaryTree<Person> collection, string? key = null) : base(collection)
            => GlobalKey = key ?? $"GlobalKey#{GetHashCode()}";

        public virtual void OnCountChanged(object source, PersonTreeEventArgs args)
            => CountChanged?.Invoke(source, args);

        public new void Add(Person item)
        {
            var c = Count;
            base.Add(item);
            if (c != Count)
                OnCountChanged(this, new PersonTreeEventArgs(GlobalKey, "added", item));
        }

        public new void AddAll(params Person[] items)
        {
            foreach (var item in items)
                Add(item);
        }

        public new bool Remove(Person item)
        {
            var removed = base.Remove(item);
            if (removed)
                OnCountChanged(this, new PersonTreeEventArgs(GlobalKey, "removed", item));
            return removed;
        }

        public new bool[] RemoveAll(params Person[] items)
        {
            var result = new bool[items.Length];
            for (int i = 0; i < items.Length; i++)
                result[i] = Remove(items[i]);
            return result;
        }

        public bool Remove(int num)
        {
            if (num < 1 || num > Count) return false;
            var enumerator = GetEnumerator();
            var counter = 0;
            Person? person = null;
            while (counter != num && enumerator.MoveNext())
            {
                counter++;
                person = enumerator.Current;
            }
            if (person is null) throw new Exception("Something went wrong"); // не выбросится никогда
            return Remove(person);
        }
    }
}