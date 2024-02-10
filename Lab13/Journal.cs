using System.Collections;

namespace Lab13
{
    // Это точная копия PersonTreeEventArgs
    public struct JournalEntry(string collectionGlobalKey, string eventType, object data)
    {
        public string CollectionGlobalKey { get; private set; } = collectionGlobalKey;
        public string EventType { get; private set; } = eventType;
        public object Data { get; private set; } = data;

        public override readonly string ToString()
            => $"JournalEntry(collectionGlobalKey: {CollectionGlobalKey}, eventType: {EventType}, data: {Data})";
    }

    public class Journal : IEnumerable<JournalEntry>
    {
        private readonly List<JournalEntry> entries = [];

        public Journal() { }

        public void CountChanged(object source, PersonTreeEventArgs args)
        {
            entries.Add(new(args.TreeGlobalKey, args.EventType, args.Obj));
        }

        public IEnumerator<JournalEntry> GetEnumerator() => entries.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }
}