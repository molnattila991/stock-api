using System;
using System.Collections.Generic;
using System.Text;

namespace stock_api_application.Exceptions
{
    public class MoneyWithTypeIsNotInStockException : Exception
    {
        public MoneyWithTypeIsNotInStockException(string message): base(message)
        {

        }

        public MoneyWithTypeIsNotInStockException() : base()
        {

        }
    }
}
