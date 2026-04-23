using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using OrderManagements;

namespace OrderManagementTests
{
    [TestClass]
    public class UIElementsTests
    {
        private OrderForm _form;

        [TestInitialize]
        public void Setup()
        {
            _form = new OrderForm();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _form.Dispose();
        }

        #region Тесты видимости элементов

        [TestMethod]
        public void TextBox_CustomerName_IsVisibleAndEnabled()
        {
            // Arrange & Act
            var textBox = _form.txtCustomerName;

            // Assert
            Assert.IsNotNull(textBox);
            Assert.IsTrue(textBox.Enabled);
        }

        [TestMethod]
        public void TextBox_Description_IsVisibleAndEnabled()
        {
            // Arrange & Act
            var textBox = _form.txtDescription;

            // Assert
            Assert.IsNotNull(textBox);
            Assert.IsTrue(textBox.Enabled);
        }

        [TestMethod]
        public void DateTimePicker_CreationDate_IsVisibleAndEnabled()
        {
            // Arrange & Act
            var dateTimePicker = _form.dtpCreationDate;

            // Assert
            Assert.IsNotNull(dateTimePicker);
            Assert.IsTrue(dateTimePicker.Enabled);
        }

        [TestMethod]
        public void ComboBox_Status_IsVisibleAndEnabled()
        {
            // Arrange & Act
            var comboBox = _form.cmbStatus;

            // Assert
            Assert.IsNotNull(comboBox);
            Assert.IsTrue(comboBox.Enabled);
        }

        [TestMethod]
        public void Button_Add_IsVisibleAndEnabled()
        {
            // Arrange & Act
            var button = _form.btnAdd;

            // Assert
            Assert.IsNotNull(button);
            Assert.IsTrue(button.Enabled);
        }

        [TestMethod]
        public void Button_Remove_IsVisibleAndEnabled()
        {
            // Arrange & Act
            var button = _form.btnRemove;

            // Assert
            Assert.IsNotNull(button);
            Assert.IsTrue(button.Enabled);
        }

        [TestMethod]
        public void Button_Update_IsVisibleAndEnabled()
        {
            // Arrange & Act
            var button = _form.btnUpdate;

            // Assert
            Assert.IsNotNull(button);
            Assert.IsTrue(button.Enabled);
        }

        [TestMethod]
        public void ListBox_Orders_IsVisibleAndEnabled()
        {
            // Arrange & Act
            var listBox = _form.lstOrders;

            // Assert
            Assert.IsNotNull(listBox);
            Assert.IsTrue(listBox.Enabled);
        }

        #endregion

        #region Тесты ComboBox Status

        [TestMethod]
        public void ComboBox_Status_HasCorrectItems()
        {
            // Arrange & Act
            var comboBox = _form.cmbStatus;

            // Assert
            Assert.IsNotNull(comboBox.Items);
            Assert.AreEqual(3, comboBox.Items.Count);
            Assert.AreEqual("Новый", comboBox.Items[0].ToString().Trim());
            Assert.AreEqual("В_обработке", comboBox.Items[1].ToString().Trim());
            Assert.AreEqual("Завершён", comboBox.Items[2].ToString().Trim());
        }

        [TestMethod]
        public void ComboBox_Status_HasDefaultSelection()
        {
            // Arrange & Act
            var comboBox = _form.cmbStatus;

            // Assert
            Assert.AreEqual(0, comboBox.SelectedIndex);
            Assert.AreEqual("Новый", comboBox.SelectedItem.ToString().Trim());
        }

        #endregion

        #region Тесты наличия элементов на форме

        [TestMethod]
        public void Form_Contains_AllRequiredControls()
        {
            // Arrange & Act
            var controls = _form.Controls;

            // Assert
            Assert.IsTrue(controls.Contains(_form.txtCustomerName));
            Assert.IsTrue(controls.Contains(_form.txtDescription));
            Assert.IsTrue(controls.Contains(_form.dtpCreationDate));
            Assert.IsTrue(controls.Contains(_form.btnAdd));
            Assert.IsTrue(controls.Contains(_form.btnRemove));
            Assert.IsTrue(controls.Contains(_form.cmbStatus));
            Assert.IsTrue(controls.Contains(_form.btnUpdate));
            Assert.IsTrue(controls.Contains(_form.lstOrders));
        }

        [TestMethod]
        public void Form_HasCorrectTitle()
        {
            // Arrange & Act
            var title = _form.Text;

            // Assert
            Assert.AreEqual("Управление заказами", title.Trim());
        }

        [TestMethod]
        public void Form_HasCorrectSize()
        {
            // Arrange & Act
            var form = _form;

            // Assert
            Assert.AreEqual(700, form.Width);
            Assert.AreEqual(500, form.Height);
        }

        #endregion

        #region Тесты TextBox properties

        [TestMethod]
        public void TextBox_CustomerName_IsEmptyInitially()
        {
            // Arrange & Act
            var textBox = _form.txtCustomerName;

            // Assert
            Assert.AreEqual("", textBox.Text);
        }

        [TestMethod]
        public void TextBox_Description_IsEmptyInitially()
        {
            // Arrange & Act
            var textBox = _form.txtDescription;

            // Assert
            Assert.AreEqual("", textBox.Text);
        }

        #endregion
    }
}