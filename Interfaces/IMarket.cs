namespace ProjetSimulationReseaux
{
    public interface IMarket
    {
        double PriceFactor { get; set; }

        void UpdatePrice(int timePassed);
    }
}