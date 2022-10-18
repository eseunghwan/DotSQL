
using System;


namespace DotSQL.Exceptions {
    public class ConnectionFailedException: Exception {
        public ConnectionFailedException(): base() {}
        public ConnectionFailedException(String message): base(message) {}
        public ConnectionFailedException(String message, Exception innerException): base(message, innerException) {}
    }

    public class BuildResultFailedException: Exception {
        public BuildResultFailedException(): base() {}
        public BuildResultFailedException(String message): base(message) {}
        public BuildResultFailedException(String message, Exception innerException): base(message, innerException) {}
    }

    public class ResultConversionFailedException: Exception {
        public ResultConversionFailedException(): base() {}
        public ResultConversionFailedException(String message): base(message) {}
        public ResultConversionFailedException(String message, Exception innerException): base(message, innerException) {}
    }
}
