namespace Lab12Lib.Exceptions
{
    [Serializable]
    public class CollectionIsReadOnlyException : Exception
    {
        public CollectionIsReadOnlyException() { }
        public CollectionIsReadOnlyException(string message) : base(message) { }
        public CollectionIsReadOnlyException(string message, Exception inner) : base(message, inner) { }
    }
}