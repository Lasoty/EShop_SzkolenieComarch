using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Services.Invoicing
{
    public interface ICalculatorService
    {
        decimal GetGrossFromNet(decimal netValue, decimal tax);
    }

    public class CalculatorService : ICalculatorService
    {
        public CalculatorService()
        {

        }

        public decimal GetGrossFromNet(decimal netValue, decimal tax)
        {
            if (tax < 0) throw new ArgumentException("Tax cannot be less than 0.");

            decimal result = netValue + netValue * tax;
            return result;
        }
    }
}
