using System;
using System.Collections.Generic;
using System.Text;

namespace stock_api_application.Exceptions
{
    public class NotValidCurrencyException : Exception
    {
        public NotValidCurrencyException(string message) : base(message)
        {

        }

        public NotValidCurrencyException() : base("One of the given Bill/Coin is not in valid format.")
        {

        }
    }
}