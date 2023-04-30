using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInitializer
{
    internal class RawDataDto
    {
        internal DateTime Date { get; set; }
        internal Dictionary<string, double> ExchangeRates { get; set; }
    }
}
