using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;
using OrderManagements;

namespace OrderManagementTests
{
    [TestClass]
    public class BoundaryValueTests
    {
        private string _testFilePath = "boundary_test_orders.txt";

        [TestInitialize]
        public void Setup()
        {
            if (File.Exists(_testFilePath))
            {
                File.Delete(_testFilePath);
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(_testFilePath))
            {
                File.Delete(_testFilePath);
            }
        }

        #region Тесты граничных значений для строк (CustomerName и Description)

        [TestMethod]
        public void Order_CustomerName_WithEmptyString_CreatedSuccessfully()
        {
            // Arrange & Act
            var order = new Order("", "Описание", DateTime.Now);

            // Assert
            Assert.AreEqual("", order.CustomerName);
        }

        [TestMethod]
        public void Order_CustomerName_WithSingleCharacter_CreatedSuccessfully()
        {
            // Arrange & Act
            var order = new Order("А", "Описание", DateTime.Now);

            // Assert
            Assert.AreEqual("А", order.CustomerName);
        }

        [TestMethod]
        public void Order_CustomerName_WithVeryLongString_CreatedSuccessfully()
        {
            // Arrange
            string longName = new string('A', 1000);

            // Act
            var order = new Order(longName, "Описание", DateTime.Now);

            // Assert
            Assert.AreEqual(longName, order.CustomerName);
            Assert.AreEqual(1000, order.CustomerName.Length);
        }

        [TestMethod]
        public void Order_CustomerName_WithOnlySpaces_CreatedSuccessfully()
        {
            // Arrange & Act
            var order = new Order("   ", "Описание", DateTime.Now);

            // Assert
            Assert.AreEqual("   ", order.CustomerName);
        }

        [TestMethod]
        public void Order_CustomerName_WithSpecialCharacters_CreatedSuccessfully()
        {
            // Arrange
            string specialName = "!@#$%^&*()_+-=[]{}|;':\",./<>?";

            // Act
            var order = new Order(specialName, "Описание", DateTime.Now);

            // Assert
            Assert.AreEqual(specialName, order.CustomerName);
        }

        [TestMethod]
        public void Order_CustomerName_AndDescription_WithMaxLengthStrings_CreatedSuccessfully()
        {
            // Arrange
            string maxName = new string('N', 500);
            string maxDesc = new string('D', 5000);

            // Act
            var order = new Order(maxName, maxDesc, DateTime.Now);

            // Assert
            Assert.AreEqual(500, order.CustomerName.Length);
            Assert.AreEqual(5000, order.Description.Length);
        }

        #endregion

        #region Тесты граничных значений для DateTime

        [TestMethod]
        public void Order_CreationDate_WithDateTimeMinValue_CreatedSuccessfully()
        {
            // Arrange
            DateTime minDate = DateTime.MinValue;

            // Act
            var order = new Order("Иван", "Описание", minDate);

            // Assert
            Assert.AreEqual(minDate, order.CreationDate);
        }

        [TestMethod]
        public void Order_CreationDate_WithDateTimeMaxValue_CreatedSuccessfully()
        {
            // Arrange
            DateTime maxDate = DateTime.MaxValue;

            // Act
            var order = new Order("Иван", "Описание", maxDate);

            // Assert
            Assert.AreEqual(maxDate, order.CreationDate);
        }

        [TestMethod]
        public void Order_CreationDate_WithUnixEpoch_CreatedSuccessfully()
        {
            // Arrange
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0);

            // Act
            var order = new Order("Иван", "Описание", unixEpoch);

            // Assert
            Assert.AreEqual(unixEpoch, order.CreationDate);
        }

        [TestMethod]
        public void Order_CreationDate_WithLeapYearDate_CreatedSuccessfully()
        {
            // Arrange
            DateTime leapDate = new DateTime(2024, 2, 29);

            // Act
            var order = new Order("Иван", "Описание", leapDate);

            // Assert
            Assert.AreEqual(leapDate, order.CreationDate);
        }

        [TestMethod]
        public void Order_CreationDate_WithMidnight_CreatedSuccessfully()
        {
            // Arrange
            DateTime midnight = new DateTime(2025, 4, 23, 0, 0, 0);

            // Act
            var order = new Order("Иван", "Описание", midnight);

            // Assert
            Assert.AreEqual(midnight, order.CreationDate);
        }

        [TestMethod]
        public void Order_CreationDate_WithEndOfDay_CreatedSuccessfully()
        {
            // Arrange
            DateTime endOfDay = new DateTime(2025, 4, 23, 23, 59, 59);

            // Act
            var order = new Order("Иван", "Описание", endOfDay);

            // Assert
            Assert.AreEqual(endOfDay, order.CreationDate);
        }

        [TestMethod]
        public void Order_CreationDate_WithYearBoundary_NewYear_CreatedSuccessfully()
        {
            // Arrange
            DateTime newYear = new DateTime(2025, 1, 1, 0, 0, 0);

            // Act
            var order = new Order("Иван", "Описание", newYear);

            // Assert
            Assert.AreEqual(newYear, order.CreationDate);
        }

        #endregion

        #region Тест граничных значений для путей к файлам

        [TestMethod]
        public void OrderManager_FilePath_WithValidRelativePath_WorksCorrectly()
        {
            // Arrange
            string relativePath = ".\\test_orders.txt";

            // Act
            var manager = new OrderManager(relativePath);

            // Assert
            Assert.IsNotNull(manager);
            Assert.AreEqual(0, manager.Orders.Count);

            // Cleanup
            if (File.Exists(relativePath))
                File.Delete(relativePath);
        }

        #endregion

        #region Тесты граничных значений для OrderStatus

        [TestMethod]
        public void OrderStatus_UpdateToInvalidValue_CanBeSet()
        {
            // Arrange
            var order = new Order("Иван", "Описание", DateTime.Now);

            // Act
            order.UpdateStatus((OrderStatus)999);

            // Assert
            Assert.AreEqual((OrderStatus)999, order.Status);
        }

        [TestMethod]
        public void OrderStatus_UpdateToNegativeValue_CanBeSet()
        {
            // Arrange
            var order = new Order("Иван", "Описание", DateTime.Now);

            // Act
            order.UpdateStatus((OrderStatus)(-1));

            // Assert
            Assert.AreEqual((OrderStatus)(-1), order.Status);
        }

        [TestMethod]
        public void OrderStatus_UpdateToMaxIntValue_CanBeSet()
        {
            // Arrange
            var order = new Order("Иван", "Описание", DateTime.Now);

            // Act
            order.UpdateStatus((OrderStatus)int.MaxValue);

            // Assert
            Assert.AreEqual((OrderStatus)int.MaxValue, order.Status);
        }

        [TestMethod]
        public void OrderStatus_AllValidValues_CanBeSet()
        {
            // Arrange
            var order = new Order("Иван", "Описание", DateTime.Now);

            // Act & Assert
            order.UpdateStatus(OrderStatus.Новый);
            Assert.AreEqual(OrderStatus.Новый, order.Status);

            order.UpdateStatus(OrderStatus.В_обработке);
            Assert.AreEqual(OrderStatus.В_обработке, order.Status);

            order.UpdateStatus(OrderStatus.Завершён);
            Assert.AreEqual(OrderStatus.Завершён, order.Status);
        }

        #endregion

        #region Тест граничных значений для сохранения/загрузки

        [TestMethod]
        public void OrderManager_SaveOrders_WithEmptyFile_CreatedSuccessfully()
        {
            // Arrange
            File.WriteAllText(_testFilePath, "");

            // Act
            var manager = new OrderManager(_testFilePath);

            // Assert
            Assert.AreEqual(0, manager.Orders.Count);
        }

        #endregion

        #region Тесты граничных значений для UpdateStatus

        [TestMethod]
        public void Order_UpdateStatus_MultipleTimesInSequence_LastStatusApplied()
        {
            // Arrange
            var order = new Order("Иван", "Описание", DateTime.Now);

            // Act
            order.UpdateStatus(OrderStatus.Новый);
            order.UpdateStatus(OrderStatus.В_обработке);
            order.UpdateStatus(OrderStatus.Завершён);
            order.UpdateStatus(OrderStatus.В_обработке);

            // Assert
            Assert.AreEqual(OrderStatus.В_обработке, order.Status);
        }

        [TestMethod]
        public void Order_UpdateStatus_ToSameStatus_MultipleTimes_StatusUnchanged()
        {
            // Arrange
            var order = new Order("Иван", "Описание", DateTime.Now);
            order.UpdateStatus(OrderStatus.В_обработке);

            // Act
            for (int i = 0; i < 100; i++)
            {
                order.UpdateStatus(OrderStatus.В_обработке);
            }

            // Assert
            Assert.AreEqual(OrderStatus.В_обработке, order.Status);
        }

        #endregion
    }
}