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
        public OrderManager orderManager;
        public TextBox txtCustomerName;
        public TextBox txtDescription;
        public DateTimePicker dtpCreationDate;
        public ComboBox cmbStatus;
        public Button btnAdd;
        public Button btnRemove;
        public Button btnUpdate;
        public ListBox lstOrders;

        public OrderForm()
        {
            this.Text = "Управление заказами";
            this.Width = 700;
            this.Height = 500;
            this.StartPosition = FormStartPosition.CenterScreen;

            orderManager = new OrderManager();

            txtCustomerName = new TextBox
            {
                Location = new Point(10, 20),
                Width = 200
            };

            txtDescription = new TextBox
            {
                Location = new Point(220, 20),
                Width = 250
            };

            dtpCreationDate = new DateTimePicker
            {
                Location = new Point(480, 20),
                Width = 150
            };

            btnAdd = new Button
            {
                Text = "Добавить заказ",
                Location = new Point(10, 60),
                Width = 150
            };
            btnAdd.Click += BtnAdd_Click;

            btnRemove = new Button
            {
                Text = "Удалить выбранный",
                Location = new Point(170, 60),
                Width = 150
            };
            btnRemove.Click += BtnRemove_Click;

            cmbStatus = new ComboBox
            {
                Location = new Point(330, 60),
                Width = 150,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbStatus.Items.AddRange(new string[] { "Новый", "В_обработке", "Завершён" });
            cmbStatus.SelectedIndex = 0;

            btnUpdate = new Button
            {
                Text = "Изменить статус",
                Location = new Point(490, 60),
                Width = 150
            };
            btnUpdate.Click += BtnUpdate_Click;

            lstOrders = new ListBox
            {
                Location = new Point(10, 100),
                Width = 660,
                Height = 350,
                SelectionMode = SelectionMode.One
            };

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

        public void BtnAdd_Click(object sender, EventArgs e)
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

        public void BtnRemove_Click(object sender, EventArgs e)
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

        public void BtnUpdate_Click(object sender, EventArgs e)
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

        public void UpdateOrdersList()
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