using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OrderManagements
{
    public class OrderManager
    {
        private readonly string _filePath;
        public List<Order> Orders { get; private set; }

        public OrderManager(string filePath = "orders.txt")
        {
            _filePath = filePath;
            Orders = new List<Order>();
            LoadOrders();
        }

        public void AddOrder(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            Orders.Add(order);
            SaveOrders();
        }

        public void RemoveOrder(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            Orders.Remove(order);
            SaveOrders();
        }

        public void UpdateOrderStatus(Order order, OrderStatus newStatus)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            order.UpdateStatus(newStatus);
            SaveOrders();
        }

        private void SaveOrders()
        {
            File.WriteAllLines(_filePath, Orders.Select(o =>
                $"{o.CustomerName}|{o.Description}|{(int)o.Status}|{o.CreationDate:yyyy-MM-dd HH:mm:ss}"));
        }

        private void LoadOrders()
        {
            if (File.Exists(_filePath))
            {
                // ✅ Используем _filePath, а не "orders.txt"
                var lines = File.ReadAllLines(_filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split('|');
                    if (parts.Length == 4)
                    {
                        // ✅ Читаем статус как число
                        if (int.TryParse(parts[2], out int statusInt) &&
                            DateTime.TryParse(parts[3], out DateTime date))
                        {
                            var order = new Order(parts[0], parts[1], date);
                            order.Status = (OrderStatus)statusInt;
                            Orders.Add(order);
                        }
                    }
                }
            }
        }
    }
}