    public class ArgsException : Exception
    {
        private char _errorArgumentId = '\0';
        private string _errorParameter = "TILT";
        private ErrorCode _errorCode = ErrorCode.OK;

        public ArgsException() { }

        public ArgsException(string message) : base(message) { }

        public enum ErrorCode
        {
            OK,
            MISSING_STRING,
            MISSING_INTEGER,
            INVALID_INTEGER,
            UNEXPECTED_ARGUMENT,
            MISSING_DOUBLE,
            INVALID_DOUBLE
        }
    }
