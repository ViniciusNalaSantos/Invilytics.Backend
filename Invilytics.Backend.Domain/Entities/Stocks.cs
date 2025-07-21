using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invilytics.Backend.Domain.Entities;
public class Stocks
{
    public int Id { get; set; }
    public string Symbol { get; set; }
    public string Description { get; set; }
    public int InvestmentSectorId { get; set; }
    public InvestmentSectors InvestmentSector { get; set; }
    public int UserId { get; set; }
    public Users User { get; set; }
    public ICollection<StockQuotesHistory> StockQuotesHistory { get; set; } = new List<StockQuotesHistory>();
    public ICollection<Dividends> Dividends { get; set; } = new List<Dividends>();
    public ICollection<Purchases> Purchases { get; set; } = new List<Purchases>();
    public ICollection<Sales> Sales { get; set; } = new List<Sales>();
    public ICollection<Portfolios> Portfolios { get; set; } = new List<Portfolios>();
}
