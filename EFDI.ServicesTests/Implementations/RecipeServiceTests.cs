using Microsoft.VisualStudio.TestTools.UnitTesting;
using EFDI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using EFDI.CommonLibrary.Designer;
using System.Data.Entity;
using EFDI.Services.Implementations;

namespace EFDI.Services.Tests
{
    [TestClass()]
    public class RecipeServiceTests
    {
        [TestMethod()]
        public void AddRecipe_SaveViaContext_ReturnAddedRecipe()
        {
            // Arrange
            var mockSet = new Mock<DbSet<Recipe>>();
            var mockContext = new Mock<MRSEntities>();

            // Act
            mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);

            //var service = new RecipeService(mockContext.Object);
            //service.AddRecipe(new Recipe { Name = "Pasta", CategoryId = 2 });

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Recipe>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}