using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invilytics.Backend.Domain.Entities;
public class Portfolios
{
    public int UserId { get; set; }
    public Users User { get; set; }
    public int StockId { get; set; }
    public Stocks Stock { get; set; }
    public decimal ValueInvested { get; set; }
    public int Quantity { get; set; }
}
