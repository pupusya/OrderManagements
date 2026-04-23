using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using OrderManagements;

namespace OrderManagementTests
{
    [TestClass]
    public class OrderFormTests
    {
        private OrderForm _orderForm;

        [TestInitialize]
        public void Setup()
        {
            _orderForm = new OrderForm();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _orderForm.Dispose();
        }

        #region Тесты для кнопки "Добавить"

        [TestMethod]
        public void AddButton_Click_WhenValidData_AddsOrder()
        {
            // Arrange
            _orderForm.txtCustomerName.Text = "Иван Иванов";
            _orderForm.txtDescription.Text = "Тестовый заказ";
            _orderForm.dtpCreationDate.Value = System.DateTime.Now;
            int initialCount = _orderForm.orderManager.Orders.Count;

            // Act
            _orderForm.BtnAdd_Click(null, System.EventArgs.Empty);

            // Assert
            Assert.AreEqual(initialCount + 1, _orderForm.orderManager.Orders.Count);
            Assert.AreEqual("", _orderForm.txtCustomerName.Text);
            Assert.AreEqual("", _orderForm.txtDescription.Text);
        }

        [TestMethod]
        public void AddButton_Click_WhenEmptyCustomerName_ShowsError()
        {
            // Arrange
            _orderForm.txtCustomerName.Text = "";
            _orderForm.txtDescription.Text = "Тестовый заказ";
            int initialCount = _orderForm.orderManager.Orders.Count;

            // Act
            _orderForm.BtnAdd_Click(null, System.EventArgs.Empty);

            // Assert
            Assert.AreEqual(initialCount, _orderForm.orderManager.Orders.Count);
        }

        [TestMethod]
        public void AddButton_Click_WhenEmptyDescription_ShowsError()
        {
            // Arrange
            _orderForm.txtCustomerName.Text = "Иван Иванов";
            _orderForm.txtDescription.Text = "";
            int initialCount = _orderForm.orderManager.Orders.Count;

            // Act
            _orderForm.BtnAdd_Click(null, System.EventArgs.Empty);

            // Assert
            Assert.AreEqual(initialCount, _orderForm.orderManager.Orders.Count);
        }

        [TestMethod]
        public void AddButton_Click_WhenOnlySpacesInCustomerName_ShowsError()
        {
            // Arrange
            _orderForm.txtCustomerName.Text = "   ";
            _orderForm.txtDescription.Text = "Тестовый заказ";
            int initialCount = _orderForm.orderManager.Orders.Count;

            // Act
            _orderForm.BtnAdd_Click(null, System.EventArgs.Empty);

            // Assert
            Assert.AreEqual(initialCount, _orderForm.orderManager.Orders.Count);
        }

        [TestMethod]
        public void AddButton_Click_WhenOnlySpacesInDescription_ShowsError()
        {
            // Arrange
            _orderForm.txtCustomerName.Text = "Иван Иванов";
            _orderForm.txtDescription.Text = "   ";
            int initialCount = _orderForm.orderManager.Orders.Count;

            // Act
            _orderForm.BtnAdd_Click(null, System.EventArgs.Empty);

            // Assert
            Assert.AreEqual(initialCount, _orderForm.orderManager.Orders.Count);
        }

        #endregion

        #region Тесты для кнопки "Удалить"

        [TestMethod]
        public void RemoveButton_Click_WhenItemSelected_RemovesOrder()
        {
            // Arrange
            var order = new Order("Иван Иванов", "Тестовый заказ", System.DateTime.Now);
            _orderForm.orderManager.AddOrder(order);
            _orderForm.UpdateOrdersList();
            _orderForm.lstOrders.SelectedIndex = 0;
            int initialCount = _orderForm.orderManager.Orders.Count;

            // Act
            _orderForm.BtnRemove_Click(null, System.EventArgs.Empty);

            // Assert
            Assert.AreEqual(initialCount - 1, _orderForm.orderManager.Orders.Count);
        }

        [TestMethod]
        public void RemoveButton_Click_WhenNoItemSelected_ShowsWarning()
        {
            // Arrange
            var order = new Order("Иван Иванов", "Тестовый заказ", System.DateTime.Now);
            _orderForm.orderManager.AddOrder(order);
            _orderForm.UpdateOrdersList();
            _orderForm.lstOrders.ClearSelected();
            int initialCount = _orderForm.orderManager.Orders.Count;

            // Act
            _orderForm.BtnRemove_Click(null, System.EventArgs.Empty);

            // Assert
            Assert.AreEqual(initialCount, _orderForm.orderManager.Orders.Count);
        }

        [TestMethod]
        public void RemoveButton_Click_WhenOrdersEmpty_ShowsWarning()
        {
            // Arrange
            int initialCount = _orderForm.orderManager.Orders.Count;

            // Act
            _orderForm.BtnRemove_Click(null, System.EventArgs.Empty);

            // Assert
            Assert.AreEqual(initialCount, _orderForm.orderManager.Orders.Count);
        }

        #endregion

        #region Тесты для кнопки "Обновить статус"

        [TestMethod]
        public void UpdateButton_Click_WhenItemSelected_UpdatesOrderStatus()
        {
            // Arrange
            var order = new Order("Иван Иванов", "Тестовый заказ", System.DateTime.Now);
            _orderForm.orderManager.AddOrder(order);
            _orderForm.UpdateOrdersList();
            _orderForm.lstOrders.SelectedIndex = 0;
            _orderForm.cmbStatus.SelectedIndex = 1; // "В_обработке"
            var expectedStatus = OrderStatus.В_обработке;

            // Act
            _orderForm.BtnUpdate_Click(null, System.EventArgs.Empty);

            // Assert
            var updatedOrder = _orderForm.orderManager.Orders[0];
            Assert.AreEqual(expectedStatus, updatedOrder.Status);
        }

        [TestMethod]
        public void UpdateButton_Click_WhenItemSelected_ChangesToCompletedStatus()
        {
            // Arrange
            var order = new Order("Иван Иванов", "Тестовый заказ", System.DateTime.Now);
            _orderForm.orderManager.AddOrder(order);
            _orderForm.UpdateOrdersList();
            _orderForm.lstOrders.SelectedIndex = 0;
            _orderForm.cmbStatus.SelectedIndex = 2; // "Завершён"
            var expectedStatus = OrderStatus.Завершён;

            // Act
            _orderForm.BtnUpdate_Click(null, System.EventArgs.Empty);

            // Assert
            var updatedOrder = _orderForm.orderManager.Orders[0];
            Assert.AreEqual(expectedStatus, updatedOrder.Status);
        }

        [TestMethod]
        public void UpdateButton_Click_WhenNoItemSelected_ShowsWarning()
        {
            // Arrange
            var order = new Order("Иван Иванов", "Тестовый заказ", System.DateTime.Now);
            _orderForm.orderManager.AddOrder(order);
            _orderForm.UpdateOrdersList();
            _orderForm.lstOrders.ClearSelected();

            // Act
            _orderForm.BtnUpdate_Click(null, System.EventArgs.Empty);

            // Assert
            // Статус не должен измениться
            Assert.AreEqual(OrderStatus.Новый, _orderForm.orderManager.Orders[0].Status);
        }

        #endregion

        #region Тесты для UpdateOrdersList

        [TestMethod]
        public void UpdateOrdersList_DisplayStringContainsCorrectFormat()
        {
            // Arrange
            var order = new Order("Иван Иванов", "Тестовый заказ", new System.DateTime(2025, 4, 23));
            _orderForm.orderManager.AddOrder(order);

            // Act
            _orderForm.UpdateOrdersList();

            // Assert
            string displayString = _orderForm.lstOrders.Items[0].ToString();
            Assert.IsTrue(displayString.Contains("[Новый]"));
            Assert.IsTrue(displayString.Contains("Иван Иванов"));
            Assert.IsTrue(displayString.Contains("Тестовый заказ"));
        }
        #endregion
    }
}