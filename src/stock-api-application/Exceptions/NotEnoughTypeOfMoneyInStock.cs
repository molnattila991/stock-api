using System;
using System.Collections.Generic;
using System.Text;

namespace stock_api_application.Exceptions
{
    public class NotEnoughTypeOfMoneyInStock : Exception
    {
        public NotEnoughTypeOfMoneyInStock(string message) : base(message)
        {

        }

        public NotEnoughTypeOfMoneyInStock() : base()
        {

        }
    }
}
