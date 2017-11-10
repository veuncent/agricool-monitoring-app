using SkiaSharp;
using System;
using System.Collections.Generic;
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
        /// Get Cooltainer humidity for Microcharts graph
        /// </summary>
        /// <param name="cooltainerId">Id of Cooltainer of interest</param>
        /// <returns>List of Microcharts Entries with current humidity of Cooltainer + rest value</returns>
        public static List<Entry> GetHumidityEntryList(int cooltainerId)
        {
            List<Entry> entries = new List<Entry>();

            var humidity = GetHumidity(cooltainerId);

            Entry humidityEntry = new Entry(humidity)
            {
                Label = "Humidity: " + humidity.ToString() + "%",
                Color = SKColor.Parse("#1E75D6")
            };

            Entry restValue = new Entry(100 - humidity)
            {
                Color = SKColor.Parse("#D6D6D6")
            };

            entries.Add(humidityEntry);
            entries.Add(restValue);

            return entries;
        }


        /// <summary>
        /// Get Cooltainer CO2 concentration at rhizosphere (root zone) for Microcharts graph
        /// </summary>
        /// <param name="cooltainerId">Id of Cooltainer of interest</param>
        /// <returns>List of Microcharts Entries with current CO2 concentration + rest value</returns>
        public static List<Entry> GetCo2LevelEntryList(int cooltainerId)
        {
            List<Entry> entries = new List<Entry>();

            var co2Level = GetCo2Level(cooltainerId);

            Entry co2LevelEntry = new Entry(co2Level)
            {
                Label = "CO2: " + co2Level.ToString() + " ppm",
                Color = SKColor.Parse("#1ED1D6")
            };

            Entry restValue = new Entry(780 - co2Level)
            {
                Color = SKColor.Parse("#D6D6D6")
            };

            entries.Add(co2LevelEntry);
            entries.Add(restValue);

            return entries;
        }


        /// <summary>
        /// Get mist head pressure for Microcharts graph
        /// </summary>
        /// <param name="cooltainerId">Id of Cooltainer of interest</param>
        /// <returns>List of Microcharts Entries with mist head pressure + rest value</returns>
        public static List<Entry> GetPressureEntryList(int cooltainerId)
        {
            List<Entry> entries = new List<Entry>();

            var pressure = GetPressure(cooltainerId);

            Entry pressureEntry = new Entry(pressure)
            {
                Label = "Pressure: " + pressure.ToString() + " kPa",
                Color = SKColor.Parse("#D61E3D")
            };

            Entry restValue = new Entry(700 - pressure)
            {
                Color = SKColor.Parse("#D6D6D6")
            };

            entries.Add(pressureEntry);
            entries.Add(restValue);

            return entries;
        }


        /// <summary>
        /// Get watering tank level for Microcharts graph
        /// </summary>
        /// <param name="cooltainerId">Id of Cooltainer of interest</param>
        /// <returns>Microcharts Entry with watering tank level in liters + rest value</returns>
        public static Entry GetWaterTankLevelEntry(int cooltainerId)
        {

            var waterLevel = GetWaterTankLevel(cooltainerId);

            Entry waterLevelEntry = new Entry(waterLevel)
            {
                Label = DateTime.Now.ToString("h:mm:ss tt"),
                ValueLabel = waterLevel.ToString() + "liters",
                Color = SKColor.Parse("#1E75D6")
            };

            return waterLevelEntry;
        }



        /// <summary>
        /// Get temperature of Cooltainer specified in parameter
        /// </summary>
        /// <param name="cooltainerId">Id of Cooltainer of interest</param>
        /// <returns>Temparature in degrees Celcius as int</returns>
        private static int GetTemperature(int cooltainerId)
        {
            Random r = new Random();
            return r.Next(25, 30);
        }

        /// <summary>
        /// Get humidity of Cooltainer specified in parameter
        /// </summary>
        /// <param name="cooltainerId">Id of Cooltainer of interest</param>
        /// <returns>Humidity in % as int</returns>
        private static int GetHumidity(int cooltainerId)
        {
            Random r = new Random();
            return r.Next(60, 80);
        }

        /// <summary>
        /// Get CO2 level at rhizosphere (root zone)
        /// </summary>
        /// <param name="cooltainerId">Id of Cooltainer of interest</param>
        /// <returns>CO2 concentrations in ppm as int</returns>
        private static int GetCo2Level(int cooltainerId)
        {
            Random r = new Random();
            return r.Next(550, 600);
        }

        /// <summary>
        /// Get mist head pressure of Cooltainer specified in parameter
        /// </summary>
        /// <param name="cooltainerId">Id of Cooltainer of interest</param>
        /// <returns>Pressure in kilopascal (kPa) as int</returns>
        private static int GetPressure(int cooltainerId)
        {
            Random r = new Random();
            return r.Next(540, 560);
        }

        /// <summary>
        /// Get watering tank level of Cooltainer specified in parameter
        /// </summary>
        /// <param name="cooltainerId">Id of Cooltainer of interest</param>
        /// <returns>Water level in liters as int</returns>
        private static int GetWaterTankLevel(int cooltainerId)
        {
            Random r = new Random();
            return r.Next(90, 99);
        }
    }
}