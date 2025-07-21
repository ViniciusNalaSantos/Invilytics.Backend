using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invilytics.Backend.Domain.Entities;
public class Users
{
    public int Id { get; set; }
    public Guid PublicId { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public ICollection<InvestmentSectors> InvestmentSectors { get; set; } = new List<InvestmentSectors>();
    public ICollection<InvestmentTypes> InvestmentTypes { get; set;} = new List<InvestmentTypes>();
    public ICollection<Stocks> Stocks { get; set; } = new List<Stocks>();
    public ICollection<StockQuotesHistory> StockQuotesHistories { get; set; } = new List<StockQuotesHistory>();
    public ICollection<Dividends> Dividends { get; set; } = new List<Dividends>();
    public ICollection<Purchases> Purchases { get; set; } = new List<Purchases>();
    public ICollection<Portfolios> Portfolios { get; set; } = new List<Portfolios>();
    public ICollection<Sales> Sales { get; set; } = new List<Sales>();
}
