using Moq;
using NUnit.Framework;
using stock_api_application.Interfaces;
using stock_api_application.Services;
using stock_api_application_test.Mock;
using stock_api_domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_api_application_test.Services
{
    public class ValidateIncomingItemsTest
    {
        private IValidateIncomingItems validateIncomingItems;
        Mock<IValidItemRepository> validItemRepository;

        [SetUp]
        public void Setup()
        {
            validItemRepository = new Mock<IValidItemRepository>();
            validItemRepository.Setup(item => item.GetItems()).ReturnsAsync(ValidItemsMock.Get());
            validateIncomingItems = new ValidateIncomingItems(validItemRepository.Object);
        }

        [Test]
        public void ShouldBeValid()
        {
            //Arrange
            var validItems = ValidItems();

            //Act
            var result = validateIncomingItems.ValidatedItems(validItems);

            //Assert
            validItemRepository.Verify(item => item.GetItems(), Times.Once);
            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldBeInvalid()
        {
            //Arrange
            var invalidItems = InValidItems();

            //Act
            var result = validateIncomingItems.ValidatedItems(invalidItems);

            //Assert
            validItemRepository.Verify(item => item.GetItems(), Times.Once);
            Assert.IsFalse(result);
        }

        private IEnumerable<StockItem> ValidItems() => new List<StockItem>()
        {
            new StockItem() { ValueOfType = 5, Type = "5", Amount = 10 },
            new StockItem() { ValueOfType = 10, Type = "10", Amount = 10 },
            new StockItem() { ValueOfType = 20, Type = "20", Amount = 10 },
            new StockItem() { ValueOfType = 50, Type = "50", Amount = 10 }
        };

        private IEnumerable<StockItem> InValidItems() => new List<StockItem>()
        {
            new StockItem() { ValueOfType = 5, Type = "55", Amount = 10 },
            new StockItem() { ValueOfType = 10, Type = "10", Amount = 10 },
            new StockItem() { ValueOfType = 20, Type = "20", Amount = 10 },
            new StockItem() { ValueOfType = 50, Type = "50", Amount = 10 }
        };
    }
}
