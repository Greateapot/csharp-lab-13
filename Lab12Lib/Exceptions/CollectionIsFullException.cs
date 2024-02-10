namespace Lab12Lib.Exceptions
{
    [Serializable]
    public class CollectionIsFullException : Exception
    {
        public CollectionIsFullException() { }
        public CollectionIsFullException(string message) : base(message) { }
        public CollectionIsFullException(string message, Exception inner) : base(message, inner) { }
    }
}