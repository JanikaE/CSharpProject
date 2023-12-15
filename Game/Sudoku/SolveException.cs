using System.Runtime.Serialization;

namespace Sudoku
{
    public class SolveException : Exception
    {
        public SolveException()
        {
        }

        public SolveException(string? message) : base(message)
        {
        }

        public SolveException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected SolveException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
