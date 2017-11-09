using SkiaSharp;
using System;
using Entry = Microcharts.Entry;

namespace AgricoolMonitoringApp.Services
{
    /// <summary>
    /// Provides a way of retreiving metrics regarding Cooltainers from our cloud services
    /// </summary>
    public static class ClimateMetricService
    {
        /// <summary>
        /// Get Cooltainer temperature for Microcharts graph
        /// </summary>
        /// <param name="cooltainerId">Id of Cooltainer of interest</param>
        /// <returns>Microcharts Entry with current temperature of Cooltainer</returns>
        public static Entry GetTemperatureEntry(int cooltainerId)
        {
            var temp = GetTemperature(cooltainerId);
            return new Entry(temp)
            {
                Label = DateTime.Now.ToString("h:mm:ss tt"),
                ValueLabel = temp.ToString() + "°",
                Color = SKColor.Parse("#90D585")
            };
        }

        /// <summary>
        /// Get temperature of Cooltainer specified in parameter
        /// </summary>
        /// <param name="cooltainerId"></param>
        /// <returns>Temparature in degrees Celcius as int</returns>
        private static int GetTemperature(int cooltainerId)
        {
            Random r = new Random();
            return r.Next(25, 30);
        }

        /// <summary>
        /// Get humidity of Cooltainer specified in parameter
        /// </summary>
        /// <param name="cooltainerId"></param>
        /// <returns>Humidity in % as int</returns>
        public static int GetHumidity(int cooltainerId)
        {
            return 72;
        }

        /// <summary>
        /// Get air pressure of Cooltainer specified in parameter
        /// </summary>
        /// <param name="cooltainerId"></param>
        /// <returns>Air pressure in hectopascal (hPa) as int</returns>
        public static int GetPressure(int cooltainerId)
        {
            return 1016;
        }
    }
}