using System;
using System.Collections.Generic;
using System.Text;

namespace stock_api_application.Exceptions
{
    public class ChangeException : Exception
    {
        public ChangeException(string message): base(message)
        {

        }

        public ChangeException() : base()
        {

        }
    }
}
