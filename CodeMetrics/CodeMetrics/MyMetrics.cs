using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeMetrics
{
    public class MyMetrics
    {
        private object DefaultPostalArea;
        private CustomerRepository _customerRepository;

        public int LinesOfCode()
        {
            return 42;
        }

        public decimal Calculate(int id, Order order)
        {
            Customer customer = _customerRepository.GetCustomerById(id);

            decimal shippingCosts = CalculateShippingCosts(customer);
            decimal totalOrderCosts = CalculateOrderCosts(order);

            decimal totalCost = CalculateTotalCost(shippingCosts, totalOrderCosts);

            return totalCost;
        }

        private static decimal CalculateTotalCost(decimal shippingCosts, decimal totalOrderCosts)
        {
            return totalOrderCosts + shippingCosts;
        }

        private static decimal CalculateOrderCosts(Order order)
        {
            decimal totalOrderCosts = 0;
            foreach (OrderLine o in order.OrderLines)
            {

                totalOrderCosts += o.Cost;
            }

            return totalOrderCosts;
        }

        private decimal CalculateShippingCosts(Customer customer)
        {
            decimal shippingCosts = 0;
            if (customer.PostalArea != DefaultPostalArea)
            {
                shippingCosts = 10;
            }

            return shippingCosts;
        }

        public int HighComplexity(int x, int y)
        {
            if (x < 0 && y < 0)

                if (x < y)
                    return y;
                else if (x > y)
                    return x + y;
                else
                    return x;
            else
                if (x * y > 100)
                if (x < y)
                    return x;
            return y;
        }
    }
}
