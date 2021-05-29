using stock_api_domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_api_application_test.Mock
{
    public static class ValidItemsMock
    {
        public static IReadOnlyList<ValidItem> Get() => new List<ValidItem>()
            {
                new ValidItem()
                {
                    Type = "5",
                    ValueOfType = 5
                },
                new ValidItem()
                {
                    Type = "10",
                    ValueOfType = 10
                },
                new ValidItem()
                {
                    Type = "20",
                    ValueOfType = 20
                },
                new ValidItem()
                {
                    Type = "50",
                    ValueOfType = 50
                },
                new ValidItem()
                {
                    Type = "100",
                    ValueOfType = 100
                },
                new ValidItem()
                {
                    Type = "200",
                    ValueOfType = 200
                },
                new ValidItem()
                {
                    Type = "500",
                    ValueOfType = 500
                },
                new ValidItem()
                {
                    Type = "1000",
                    ValueOfType = 1000
                },
                new ValidItem()
                {
                    Type = "2000",
                    ValueOfType = 2000
                },
                new ValidItem()
                {
                    Type = "5000",
                    ValueOfType = 5000
                },
                new ValidItem()
                {
                    Type = "10000",
                    ValueOfType = 10000
                },
                new ValidItem()
                {
                    Type = "20000",
                    ValueOfType = 20000
                }
            };
    }
}
