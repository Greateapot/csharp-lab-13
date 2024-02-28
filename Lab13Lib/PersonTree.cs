using Lab10Lib.Entities;
using Lab10Lib.Utils;
using Lab12Lib.BinaryTree;
using Lab12Lib.Exceptions;

namespace Lab13Lib
{
    public class PersonTree : BinaryTree<Person>
    {
        public delegate void PersonTreeEventHandler(object source, PersonTreeEventArgs args);

        public event PersonTreeEventHandler? CountChanged = null;

        public event PersonTreeEventHandler? TreeDisposed = null;

        public string GlobalKey { get; private set; }

        public PersonTree(string key) : base(new PersonComparerOut())
            => GlobalKey = key;

        public PersonTree(int capacity, string key) : base(new PersonComparerIn(), capacity)
            => GlobalKey = key;

        public PersonTree(BinaryTree<Person> collection, string key) : base(collection)
            => GlobalKey = key;

        public virtual void OnCountChanged(object source, PersonTreeEventArgs args)
            => CountChanged?.Invoke(source, args);

        public virtual void OnTreeDisposed(object source, PersonTreeEventArgs args)
            => TreeDisposed?.Invoke(source, args);

        protected override void Dispose(bool disposing)
        {
            if (IsDisposed) return;
            if (disposing)
            {
                OnTreeDisposed(this, new PersonTreeEventArgs(GlobalKey, "disposed", null));
                base.Dispose(disposing);
            }
        }

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

        public Person? FindByLastName(string lastName)
        {
            foreach (var item in this)
                if (item.LastName == lastName)
                    return item;
            return null;
        }

        public new void Clear()
        {
            if (IsReadOnly)
                throw new CollectionIsReadOnlyException();
            while (Root != null)
                Remove(Root.Value);
        }

        public new bool[] RemoveAll(params Person[] items)
        {
            var result = new bool[items.Length];
            for (int i = 0; i < items.Length; i++)
                result[i] = Remove(items[i]);
            return result;
        }
    }
}