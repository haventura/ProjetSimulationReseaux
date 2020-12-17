using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSimulationReseaux
{
    public interface IMarket
    {
        Random Random { get; set; }
        double RandomValue { get; set; }
        double PriceFactor { get; set; }
        void RandomizePriceFactor();
        void UpdatePrice();
    }
}
