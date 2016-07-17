namespace ChessDotNet
{
    public class PgnException : System.Exception
    {
        public PgnException() { }
        public PgnException(string message) : base(message) { }
        public PgnException(string message, System.Exception inner) : base(message, inner) { }
        protected PgnException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}