using NUnit.Framework;
using stock_api_application.Interfaces;
using stock_api_infrastructure_in_memory.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_api_infrastructure_in_memory_test.Repositories
{
    public class StockRepositoryQueryTest
    {
        private IStockQueryRepository _stockRepository;

        [SetUp]
        public void Setup()
        {
            _stockRepository = new StockRepository();
        }

        [Test]
        public void ShouldReturnDefaultResultSetWithFourItem()
        {
            //Arrange
            var de = StockRepositoryMock.GetDefaultStockItemList();
            //Act
            var result = _stockRepository.GetItems().Result;

            //Assert
            Assert.IsTrue(de.Count() == result.Count());
            StockRepositoryMock.CheckList(de, result);
        }

    }
}
