namespace ProjetSimulationReseaux
{
    public interface IWeather
    {
        double SunPercentage { get; set; }
        double WindPercentage { get; set; }
        double TemperaturePercentage { get; set; }

        void UpdateWeather(int timePassed);

        void CalculateSunPercent(int timePassed);

        void CalculateWindPercent(int timePassed);

        void CalculateTemperaturePercent(int timePassed);
    }
}