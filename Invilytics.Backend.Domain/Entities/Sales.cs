using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invilytics.Backend.Domain.Entities;
public class Sales
{
    public int Id { get; set; }
    public int StockId { get; set; }
    public Stocks Stock { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public DateTime SaleTimestamp { get; set; }
    public int UserId { get; set; }
    public Users User { get; set; }
}
