using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invilytics.Backend.Domain.Entities;
public class InvestmentSectors
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int InvestmentTypeId { get; set; }
    public InvestmentTypes InvestmentTypes { get; set; }
    public int UserId { get; set; }
    public Users User { get; set; }
    public ICollection<Stocks> Stocks { get; set; } = new List<Stocks>();
}
