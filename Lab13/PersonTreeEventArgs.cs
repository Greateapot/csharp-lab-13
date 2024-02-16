namespace Lab13
{
    public class PersonTreeEventArgs(string treeGlobalKey, string eventType, object? obj) : EventArgs
    {
        public string TreeGlobalKey { get; private set; } = treeGlobalKey;

        public string EventType { get; private set; } = eventType;

        public object? Obj { get; private set; } = obj;

        public override string ToString()
            => $"PersonTreeEventArgs#{GetHashCode()}(tree global key: {TreeGlobalKey}, event type: {EventType}, obj: {Obj})";
    }
}