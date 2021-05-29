using System;
using System.Collections.Generic;
using System.Text;

namespace stock_api_domain.Entities
{
    public class StockItem : IEquatable<StockItem>
    {
        public string Type { get; set; }
        public int ValueOfType { get; set; }
        public int Amount { get; set; }

        public bool Equals(StockItem other)
        {
            if(other is null)
            {
                return false;
            }

            if(Object.ReferenceEquals(this, other))
            {
                return true;
            }

            if(this.GetType() != other.GetType())
            {
                return false;
            }

            return Type == other.Type &&
                    ValueOfType == other.ValueOfType &&
                    Amount == other.Amount;
        }
    }
}
