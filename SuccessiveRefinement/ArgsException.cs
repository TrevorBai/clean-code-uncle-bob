    public class ArgsException : Exception
    {
        private char _errorArgumentId = '\0';
        private string _errorParameter = "TILT";
        private ErrorCode _errorCode = ErrorCode.OK;

        public ArgsException() { }

        public ArgsException(string message) : base(message) { }

        public ArgsException(ErrorCode errorCode)
        {
            _errorCode = errorCode;
        }

        public ArgsException(ErrorCode errorCode, string errorParameter)
        {
            _errorCode = errorCode;
            _errorParameter = errorParameter; 
        }

        public ArgsException(ErrorCode errorCode, char errorArgumentId, string errorParameter)
        {
            _errorCode = errorCode;
            _errorParameter = errorParameter; 
            _errorArgumentId = errorArgumentId;
        }

        public char GetErrorArgumentId() { return _errorArgumentId; }

        public void SetErrorArgumentId(char errorArgumentId) { _errorArgumentId = errorArgumentId; }

        public string GetErrorParameter() { return _errorParameter; }

        public void SetErrorParameter(string errorParameter) { _errorParameter = errorParameter; }

        public ErrorCode GetErrorCode() { return _errorCode; }

        public void SetErrorCode(ErrorCode errorCode) { _errorCode = errorCode; }

        public string ErrorMessage()
        {
            switch (_errorCode)
            {
                case OK:
                    throw new Exception("TILT: Should not get here.");
                case UNEXPECTED_ARGUMENT:
                    return string.Format("Argument {0} unexpected.", _errorArgumentId);
                case MISSING_STRING:
                    return string.Format("Could not find string parameter for {0}.", _errorArgumentId);
                case INVALID_INTEGER:
                    return string.Format("Argument {0} expects an integer but was {1}.", _errorArgumentId, _errorParameter);
                case MISSING_INTEGER:
                    return string.Format("Could not find integer parameter for {0}.", _errorArgumentId);
                case INVALID_DOUBLE:
                    return string.Format("Argument {0} expects a double but was {1}.", _errorArgumentId, _errorParameter);
                case MISSING_DOUBLE:
                    return string.Format("Could not find double parameter for {0}.", _errorArgumentId);
            }
            return string.Empty;
        }

        public enum ErrorCode
        {
            OK,
            INVALID_FORMAT,
            UNEXPECTED_ARGUMENT,
            INVALID_ARGUMENT_NAME,
            MISSING_STRING,
            MISSING_INTEGER,
            INVALID_INTEGER,
            MISSING_DOUBLE,
            INVALID_DOUBLE
        }
    }
