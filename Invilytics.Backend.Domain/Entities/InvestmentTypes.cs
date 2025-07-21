using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invilytics.Backend.Domain.Entities;
public class InvestmentTypes
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int UserId { get; set; }
    public Users User { get; set; }
    public ICollection<InvestmentSectors> InvestmentSectors { get; set; } = new List<InvestmentSectors>();
}
