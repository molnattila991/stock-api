using System;
using System.Collections.Generic;
using System.Text;

namespace stock_api_application.Services
{
    static class HUFHelper
    {
        /// <summary>
        /// Round the given price in case the end of it is 1,2,3,4,6,7,8,9
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public static int RoundPrice(int price)
        {
            var round = price % 5;
            if (round >= 3)
            {
                return price + (5 - round);
            }
            else
            {
                return price - round;
            }
        }
    }
}
