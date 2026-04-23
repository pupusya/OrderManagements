using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Collections.Generic;
using OrderManagements;

namespace OrderManagementTests
{
    [TestClass]
    public class OrderManagerTests
    {
        private string _testFilePath = "test_orders_temp.txt";

        // Настраиваем чистое окружение перед каждым тестом
        [TestInitialize]
        public void Setup()
        {
            if (File.Exists(_testFilePath))
            {
                File.Delete(_testFilePath);
            }
        }

        // Убираем за собой после каждого теста
        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(_testFilePath))
            {
                File.Delete(_testFilePath);
            }
        }

        #region Тесты конструктора

        [TestMethod]
        public void OrderManager_Constructor_WithNewFile_InitializesEmptyList()
        {
            // Arrange
            // _testFilePath гарантированно удален в Setup

            // Act
            OrderManager manager = new OrderManager(_testFilePath);

            // Assert
            Assert.IsNotNull(manager.Orders);
            Assert.AreEqual(0, manager.Orders.Count);
        }

        [TestMethod]
        public void OrderManager_Constructor_LoadsOrdersFromFile_WhenFileExists()
        {
            // Arrange
            // Создаем файл с валидными данными в формате: Name|Desc|StatusInt|Date
            string fileContent = "Иван|Тестовый заказ|1|2023-10-10 10:00:00";
            File.WriteAllText(_testFilePath, fileContent);

            // Act
            OrderManager manager = new OrderManager(_testFilePath);

            // Assert
            Assert.AreEqual(1, manager.Orders.Count);
            Assert.AreEqual("Иван", manager.Orders[0].CustomerName);
            Assert.AreEqual(OrderStatus.В_обработке, manager.Orders[0].Status);
        }

        [TestMethod]
        public void OrderManager_Constructor_DoesNotCrash_WhenFileIsMissing()
        {
            // Arrange & Act
            // Файла не существует, конструктор должен просто создать пустой список
            OrderManager manager = new OrderManager(_testFilePath);

            // Assert
            Assert.AreEqual(0, manager.Orders.Count);
        }

        #endregion

        #region Тесты метода AddOrder

        [TestMethod]
        public void AddOrder_WhenOrderIsValid_AddsToCollection()
        {
            // Arrange
            OrderManager manager = new OrderManager(_testFilePath);
            Order newOrder = new Order("Иван", "Описание", DateTime.Now);
            int initialCount = manager.Orders.Count;

            // Act
            manager.AddOrder(newOrder);

            // Assert
            Assert.AreEqual(initialCount + 1, manager.Orders.Count);
            Assert.IsTrue(manager.Orders.Contains(newOrder));
        }

        [TestMethod]
        public void AddOrder_WhenOrderIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            OrderManager manager = new OrderManager(_testFilePath);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => manager.AddOrder(null));
        }

        #endregion

        #region Тесты метода RemoveOrder

        [TestMethod]
        public void RemoveOrder_WhenOrderExists_RemovesFromCollection()
        {
            // Arrange
            OrderManager manager = new OrderManager(_testFilePath);
            Order order = new Order("Иван", "Описание", DateTime.Now);
            manager.AddOrder(order);
            int initialCount = manager.Orders.Count;

            // Act
            manager.RemoveOrder(order);

            // Assert
            Assert.AreEqual(initialCount - 1, manager.Orders.Count);
            Assert.IsFalse(manager.Orders.Contains(order));
        }

        [TestMethod]
        public void RemoveOrder_WhenOrderDoesNotExist_DoesNotChangeCollection()
        {
            // Arrange
            OrderManager manager = new OrderManager(_testFilePath);
            Order existingOrder = new Order("Иван", "Описание", DateTime.Now);
            manager.AddOrder(existingOrder);

            Order nonExistingOrder = new Order("Другой", "Описание", DateTime.Now);
            int initialCount = manager.Orders.Count;

            // Act
            manager.RemoveOrder(nonExistingOrder);

            // Assert
            Assert.AreEqual(initialCount, manager.Orders.Count);
        }

        [TestMethod]
        public void RemoveOrder_WhenOrderIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            OrderManager manager = new OrderManager(_testFilePath);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => manager.RemoveOrder(null));
        }

        #endregion

        #region Тесты метода UpdateOrderStatus

        [TestMethod]
        public void UpdateOrderStatus_WhenValid_ChangesOrderStatus()
        {
            // Arrange
            OrderManager manager = new OrderManager(_testFilePath);
            Order order = new Order("Иван", "Описание", DateTime.Now);
            manager.AddOrder(order);

            // По умолчанию статус "Новый"
            Assert.AreEqual(OrderStatus.Новый, order.Status);

            // Act
            manager.UpdateOrderStatus(order, OrderStatus.Завершён);

            // Assert
            Assert.AreEqual(OrderStatus.Завершён, order.Status);
        }

        [TestMethod]
        public void UpdateOrderStatus_WhenOrderIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            OrderManager manager = new OrderManager(_testFilePath);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => manager.UpdateOrderStatus(null, OrderStatus.В_обработке));
        }

        #endregion
    }
}