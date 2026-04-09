using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderManagements
{
    
        public partial class OrderForm : Form
        {
            private OrderManager orderManager;
            private TextBox txtCustomerName;
            private TextBox txtDescription;
            private DateTimePicker dtpCreationDate;
            private ComboBox cmbStatus;
            private Button btnAdd;
            private Button btnRemove;
            private Button btnUpdate;
            private ListBox lstOrders;

            public OrderForm()
            {
                this.Text = "Управление заказами";
                this.Width = 700;
                this.Height = 500;
                this.StartPosition = FormStartPosition.CenterScreen;

                orderManager = new OrderManager();

                // Имя клиента
                txtCustomerName = new TextBox();
                txtCustomerName.Location = new Point(10, 20);
                txtCustomerName.Width = 200;


                // Описание
                txtDescription = new TextBox();
                txtDescription.Location = new Point(220, 20);
                txtDescription.Width = 250;


                // Дата
                dtpCreationDate = new DateTimePicker();
                dtpCreationDate.Location = new Point(480, 20);
                dtpCreationDate.Width = 150;

                // Кнопка Добавить
                btnAdd = new Button();
                btnAdd.Text = "Добавить заказ";
                btnAdd.Location = new Point(10, 60);
                btnAdd.Width = 150;
                btnAdd.Click += BtnAdd_Click;

                // Кнопка Удалить
                btnRemove = new Button();
                btnRemove.Text = "Удалить выбранный";
                btnRemove.Location = new Point(170, 60);
                btnRemove.Width = 150;
                btnRemove.Click += BtnRemove_Click;

                // ComboBox Статус
                cmbStatus = new ComboBox();
                cmbStatus.Location = new Point(330, 60);
                cmbStatus.Width = 150;
                cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbStatus.Items.AddRange(new string[] { "Новый", "В_обработке", "Завершён" });
                cmbStatus.SelectedIndex = 0;

                // Кнопка Обновить статус
                btnUpdate = new Button();
                btnUpdate.Text = "Изменить статус";
                btnUpdate.Location = new Point(490, 60);
                btnUpdate.Width = 150;
                btnUpdate.Click += BtnUpdate_Click;

                // Список
                lstOrders = new ListBox();
                lstOrders.Location = new Point(10, 100);
                lstOrders.Width = 660;
                lstOrders.Height = 350;
                lstOrders.SelectionMode = SelectionMode.One;

                // Добавляем на форму
                this.Controls.Add(txtCustomerName);
                this.Controls.Add(txtDescription);
                this.Controls.Add(dtpCreationDate);
                this.Controls.Add(btnAdd);
                this.Controls.Add(btnRemove);
                this.Controls.Add(cmbStatus);
                this.Controls.Add(btnUpdate);
                this.Controls.Add(lstOrders);

                UpdateOrdersList();
            }

            private void BtnAdd_Click(object sender, EventArgs e)
            {
                if (string.IsNullOrWhiteSpace(txtCustomerName.Text) ||
                    string.IsNullOrWhiteSpace(txtDescription.Text))
                {
                    MessageBox.Show("Заполните все поля!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Order newOrder = new Order(
                    txtCustomerName.Text,
                    txtDescription.Text,
                    dtpCreationDate.Value
                );

                orderManager.AddOrder(newOrder);
                txtCustomerName.Clear();
                txtDescription.Clear();
                UpdateOrdersList();
            }

            private void BtnRemove_Click(object sender, EventArgs e)
            {
                if (lstOrders.SelectedItem == null)
                {
                    MessageBox.Show("Выберите заказ для удаления!", "Внимание",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string selectedText = lstOrders.SelectedItem.ToString();
                var orderToRemove = orderManager.Orders.Find(o => selectedText.Contains(o.Description));

                if (orderToRemove != null)
                {
                    orderManager.RemoveOrder(orderToRemove);
                    UpdateOrdersList();
                }
            }

            private void BtnUpdate_Click(object sender, EventArgs e)
            {
                if (lstOrders.SelectedItem == null)
                {
                    MessageBox.Show("Выберите заказ!", "Внимание",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string selectedText = lstOrders.SelectedItem.ToString();
                var orderToUpdate = orderManager.Orders.Find(o => selectedText.Contains(o.Description));

                if (orderToUpdate != null)
                {
                    string selectedStatusText = cmbStatus.SelectedItem.ToString();
                    OrderStatus newStatus = (OrderStatus)Enum.Parse(typeof(OrderStatus), selectedStatusText);
                    orderManager.UpdateOrderStatus(orderToUpdate, newStatus);
                    UpdateOrdersList();
                }
            }

            private void UpdateOrdersList()
            {
                lstOrders.Items.Clear();
                foreach (var order in orderManager.Orders)
                {
                    string displayString = $"[{order.Status}] {order.CustomerName} - {order.Description} ({order.CreationDate.ToShortDateString()})";
                    lstOrders.Items.Add(displayString);
                }
            }
        }
    }
