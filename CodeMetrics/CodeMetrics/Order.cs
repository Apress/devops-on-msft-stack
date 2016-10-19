using System.Collections.Generic;

namespace CodeMetrics
{
    public class Order
    {
        internal IEnumerable<OrderLine> OrderLines { get; set; }
    }
}