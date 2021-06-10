using System;

namespace Erp.Server.Helper
{
    public class MyException : Exception
    {
        public MyException(string message) : base(message)
        {
        }
    }
}
