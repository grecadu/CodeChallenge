using BLueCodeChanllenge.Context;
using BLueCodeChanllenge.Interfaces;
using BLueCodeChanllenge.Models;
using BLueCodeChanllenge.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using System.Collections.Generic;

namespace Testing
{
    public class Tests
    {
        [TestFixture]
        public class DbServiceTests
        {
            private Mock<BlueContext> _contextMock;
            private DbService<ShortUrls> _dbService;

            [SetUp]
            public void Setup()
            {
                _contextMock = new Mock<BlueContext>();
                _dbService = new DbService<ShortUrls>(_contextMock.Object);

                _contextMock.Setup(c => c.Entry(It.IsAny<ShortUrls>())).Returns(Mock.Of<EntityEntry<ShortUrls>>());

            }



            [Test]
            public void Add_EntityIsAdded()
            {
                // Arrange
                var entity = new ShortUrls();

                // Act
                _dbService.Add(entity);

                // Assert
                _contextMock.Verify(c => c.Add(entity), Times.Once);
                _contextMock.Verify(c => c.SaveChanges(), Times.Once);
            }

           

            [Test]
            public void GetAll_ReturnsAllEntities()
            {
                // Arrange
                var entities = GetSampleData(); // Sample data for testing
                _contextMock.Setup(c => c.Set<ShortUrls>()).Returns(MockDbSet(entities));

                // Act
                var result = _dbService.GetAll();

                // Assert
                Assert.AreEqual(entities.Count, result.Count());
            }

            [Test]
            public void Update_EntityIsUpdated()
            {
                // Arrange
                var entity = new ShortUrls
                {
                    Id = 1,
                    ShortUrl = "B",
                    LongUrl = "https://www.google.com/",
                    Counter = 4
                };

                // Act
                _dbService.Update(entity);

                // Assert
                _contextMock.VerifySet(c => c.Entry(entity).State = EntityState.Modified, Times.Once);
                _contextMock.Verify(c => c.SaveChanges(), Times.Once);
            }

            // Helper method to generate sample data
            private List<ShortUrls> GetSampleData()
            {
                var list = new List<ShortUrls>();
                list.Add(new ShortUrls 
                { 
                    Id = 1,
                    ShortUrl = "B",
                    LongUrl = "https://www.google.com/",
                   Counter = 4
                });

                list.Add(new ShortUrls
                {
                    Id = 2,
                    ShortUrl = "C",
                    LongUrl = "https://www.linkedin.com/",
                    Counter = 2
                });

                return list;
            }

            // Helper method to create a mock DbSet
            private DbSet<T> MockDbSet<T>(List<T> data) where T : class
            {
                var queryableData = data.AsQueryable();
                var dbSetMock = new Mock<DbSet<T>>();

                dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
                dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
                dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
                dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryableData.GetEnumerator());

                return dbSetMock.Object;
            }
        }
    }
}