namespace Lab12Lib.Exceptions
{

    public class RotateArgumentException : Exception
    {
        public RotateArgumentException() { }
        public RotateArgumentException(string message) : base(message) { }
        public RotateArgumentException(string message, Exception inner) : base(message, inner) { }
    }
}