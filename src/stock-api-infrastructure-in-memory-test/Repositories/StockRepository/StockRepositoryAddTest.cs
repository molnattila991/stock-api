using NUnit.Framework;
using stock_api_application.Interfaces;
using stock_api_domain.Entities;
using stock_api_infrastructure_in_memory.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_api_infrastructure_in_memory_test.Repositories
{
    public class StockRepositoryAddTest
    {
        private IStockQueryRepository stockQueryRepository;
        private IStockAddRepository stockAddRepository;


        [SetUp]
        public void Setup()
        {
            IStockRepository repo = new StockRepository();
            stockAddRepository = repo;
            stockQueryRepository = repo;
        }

        
        [Test]
        public async Task ShouldAddOneNewTypeOfItem()
        {
            //Arrange
            var newLength = 5;
            var newItem = new StockItem()
            {
                Type = "2000",
                ValueOfType = 20,
                Amount = 11
            };
            var defaultList = StockRepositoryMock.GetDefaultStockItemList();

            //Act
            await stockAddRepository.AddItem(newItem);
            var result = stockQueryRepository.GetItems().Result;

            //Assert
            Assert.IsTrue(result.Count() == 5);
            Assert.AreEqual(newItem, result.ElementAt(newLength - 1));
        }

        [Test]
        public async Task ShouldEditExistingItem()
        {
            //Arrange
            var length = 4;
            var newItem = new StockItem()
            {
                Type = "20",
                ValueOfType = 20,
                Amount = 11
            };

            var resultItem = new StockItem()
            {
                Type = "20",
                ValueOfType = 20,
                Amount = 21
            };

            //Act
            await stockAddRepository.AddItem(newItem);
            var result = await stockQueryRepository.GetItems();

            //Assert
            Assert.IsTrue(length == result.Count());
            Assert.AreEqual(resultItem, result.ElementAt(2));
        }
    }
}
