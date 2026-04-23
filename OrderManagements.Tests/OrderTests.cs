using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OrderManagements;

namespace OrderManagementTests
{
    [TestClass]
    public class OrderTests
    {
        #region Тесты для конструктора Order

        [TestMethod]
        public void Order_Constructor_WithValidData_SetsPropertiesCorrectly()
        {
            // Arrange
            string customerName = "Иван Иванов";
            string description = "Тестовый заказ";
            DateTime creationDate = new DateTime(2025, 4, 23);

            // Act
            var order = new Order(customerName, description, creationDate);

            // Assert
            Assert.AreEqual(customerName, order.CustomerName);
            Assert.AreEqual(description, order.Description);
            Assert.AreEqual(creationDate, order.CreationDate);
            Assert.AreEqual(OrderStatus.Новый, order.Status);
        }

        [TestMethod]
        public void Order_Constructor_WithEmptyCustomerName_SetsProperty()
        {
            // Arrange
            string customerName = "";
            string description = "Тестовый заказ";
            DateTime creationDate = DateTime.Now;

            // Act
            var order = new Order(customerName, description, creationDate);

            // Assert
            Assert.AreEqual(customerName, order.CustomerName);
        }

        [TestMethod]
        public void Order_Constructor_WithEmptyDescription_SetsProperty()
        {
            // Arrange
            string customerName = "Иван Иванов";
            string description = "";
            DateTime creationDate = DateTime.Now;

            // Act
            var order = new Order(customerName, description, creationDate);

            // Assert
            Assert.AreEqual(customerName, order.CustomerName);
            Assert.AreEqual(description, order.Description);
        }

        [TestMethod]
        public void Order_Constructor_SetsDefaultStatusToNew()
        {
            // Arrange
            string customerName = "Иван Иванов";
            string description = "Тестовый заказ";
            DateTime creationDate = DateTime.Now;

            // Act
            var order = new Order(customerName, description, creationDate);

            // Assert
            Assert.AreEqual(OrderStatus.Новый, order.Status);
        }

        [TestMethod]
        public void Order_Constructor_WithNullCustomerName_SetsProperty()
        {
            // Arrange
            string customerName = null;
            string description = "Тестовый заказ";
            DateTime creationDate = DateTime.Now;

            // Act
            var order = new Order(customerName, description, creationDate);

            // Assert
            Assert.IsNull(order.CustomerName);
        }

        [TestMethod]
        public void Order_Constructor_WithNullDescription_SetsProperty()
        {
            // Arrange
            string customerName = "Иван Иванов";
            string description = null;
            DateTime creationDate = DateTime.Now;

            // Act
            var order = new Order(customerName, description, creationDate);

            // Assert
            Assert.IsNull(order.Description);
        }

        #endregion

        #region Тесты для метода UpdateStatus

        [TestMethod]
        public void UpdateStatus_WhenCalled_ChangesOrderStatus()
        {
            // Arrange
            var order = new Order("Иван Иванов", "Тестовый заказ", DateTime.Now);
            OrderStatus newStatus = OrderStatus.В_обработке;

            // Act
            order.UpdateStatus(newStatus);

            // Assert
            Assert.AreEqual(OrderStatus.В_обработке, order.Status);
        }

        [TestMethod]
        public void UpdateStatus_FromNewToCompleted_UpdatesCorrectly()
        {
            // Arrange
            var order = new Order("Иван Иванов", "Тестовый заказ", DateTime.Now);

            // Act
            order.UpdateStatus(OrderStatus.Завершён);

            // Assert
            Assert.AreEqual(OrderStatus.Завершён, order.Status);
        }

        [TestMethod]
        public void UpdateStatus_FromProcessingToCompleted_UpdatesCorrectly()
        {
            // Arrange
            var order = new Order("Иван Иванов", "Тестовый заказ", DateTime.Now);
            order.UpdateStatus(OrderStatus.В_обработке);

            // Act
            order.UpdateStatus(OrderStatus.Завершён);

            // Assert
            Assert.AreEqual(OrderStatus.Завершён, order.Status);
        }

        [TestMethod]
        public void UpdateStatus_MultipleTimes_UpdatesToLastStatus()
        {
            // Arrange
            var order = new Order("Иван Иванов", "Тестовый заказ", DateTime.Now);

            // Act
            order.UpdateStatus(OrderStatus.В_обработке);
            order.UpdateStatus(OrderStatus.Завершён);
            order.UpdateStatus(OrderStatus.В_обработке);

            // Assert
            Assert.AreEqual(OrderStatus.В_обработке, order.Status);
        }

        [TestMethod]
        public void UpdateStatus_ToSameStatus_StatusRemainsUnchanged()
        {
            // Arrange
            var order = new Order("Иван Иванов", "Тестовый заказ", DateTime.Now);
            order.UpdateStatus(OrderStatus.В_обработке);

            // Act
            order.UpdateStatus(OrderStatus.В_обработке);

            // Assert
            Assert.AreEqual(OrderStatus.В_обработке, order.Status);
        }

        #endregion

        #region Тесты для свойств Order

        [TestMethod]
        public void CustomerName_Get_ReturnsSetValue()
        {
            // Arrange
            var order = new Order("Иван Иванов", "Тестовый заказ", DateTime.Now);

            // Act
            string result = order.CustomerName;

            // Assert
            Assert.AreEqual("Иван Иванов", result);
        }

        [TestMethod]
        public void CustomerName_Set_UpdatesValue()
        {
            // Arrange
            var order = new Order("Иван Иванов", "Тестовый заказ", DateTime.Now);

            // Act
            order.CustomerName = "Петр Петров";

            // Assert
            Assert.AreEqual("Петр Петров", order.CustomerName);
        }

        [TestMethod]
        public void Description_Get_ReturnsSetValue()
        {
            // Arrange
            var order = new Order("Иван Иванов", "Тестовый заказ", DateTime.Now);

            // Act
            string result = order.Description;

            // Assert
            Assert.AreEqual("Тестовый заказ", result);
        }

        [TestMethod]
        public void Description_Set_UpdatesValue()
        {
            // Arrange
            var order = new Order("Иван Иванов", "Тестовый заказ", DateTime.Now);

            // Act
            order.Description = "Новое описание";

            // Assert
            Assert.AreEqual("Новое описание", order.Description);
        }

        [TestMethod]
        public void CreationDate_Get_ReturnsSetValue()
        {
            // Arrange
            DateTime testDate = new DateTime(2025, 4, 23, 10, 30, 0);
            var order = new Order("Иван Иванов", "Тестовый заказ", testDate);

            // Act
            DateTime result = order.CreationDate;

            // Assert
            Assert.AreEqual(testDate, result);
        }

        [TestMethod]
        public void Status_Get_ReturnsSetValue()
        {
            // Arrange
            var order = new Order("Иван Иванов", "Тестовый заказ", DateTime.Now);

            // Act
            OrderStatus result = order.Status;

            // Assert
            Assert.AreEqual(OrderStatus.Новый, result);
        }

        #endregion

        #region Тесты для enum OrderStatus

        [TestMethod]
        public void OrderStatus_Enum_HasCorrectValues()
        {
            // Arrange & Act
            var новый = OrderStatus.Новый;
            var вОбработке = OrderStatus.В_обработке;
            var завершён = OrderStatus.Завершён;

            // Assert
            Assert.AreEqual(0, (int)новый);
            Assert.AreEqual(1, (int)вОбработке);
            Assert.AreEqual(2, (int)завершён);
        }

        [TestMethod]
        public void OrderStatus_Enum_CanBeConvertedToString()
        {
            // Arrange
            OrderStatus status = OrderStatus.В_обработке;

            // Act
            string statusString = status.ToString();

            // Assert
            Assert.AreEqual("В_обработке", statusString);
        }

        [TestMethod]
        public void OrderStatus_Enum_Parse_WorksCorrectly()
        {
            // Arrange
            string statusString = "Завершён";

            // Act
            OrderStatus status = (OrderStatus)Enum.Parse(typeof(OrderStatus), statusString);

            // Assert
            Assert.AreEqual(OrderStatus.Завершён, status);
        }

        #endregion
    }
}