using stock_api_application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stock_api.Validator
{
    public static class StockDictionaryValidator
    {
        public static void Validate(Dictionary<string, int> items)
        {
            foreach (var item in items)
            {
                try
                {
                    Convert.ToInt32(item.Key);
                }
                catch(FormatException e)
                {
                    throw new NotValidCurrencyException($"Format is not valid for currency '{item.Key}'.");
                }
            }
        }
    }
}
