using Android.App;
using Android.Widget;
using Android.OS;
using SkiaSharp;
using Entry = Microcharts.Entry;
using Microcharts;
using Microcharts.Droid;
using System.Threading;
using System.Threading.Tasks;
using AgricoolMonitoringApp.Services;
using System.Linq;
using System.Collections.Generic;
using System;

namespace AgricoolMonitoringApp
{
    [Activity(Label = "AgricoolMonitoringApp", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private readonly int _cooltainerId = 1;
        private readonly int _graphMaxTimespan = 10;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var ignore = StartUpdateAsync();
        }


        public async Task StartUpdateAsync()
        {
            List<Entry> temperatureEntries = new List<Entry>();

            List<Entry> waterLevelEntries = new List<Entry>();
            Entry initialWaterLevel = ClimateMetricService.GetWaterTankLevelEntry(_cooltainerId);
            waterLevelEntries.Add(initialWaterLevel);

            var temperatureChartView = FindViewById<ChartView>(Resource.Id.temperatureChartView1);
            var humidityChartView = FindViewById<ChartView>(Resource.Id.humidityChartView1);
            var co2ChartView = FindViewById<ChartView>(Resource.Id.co2ChartView1);
            var pressureChartView = FindViewById<ChartView>(Resource.Id.pressureChartView1);
            var waterLevelChartView = FindViewById<ChartView>(Resource.Id.waterLevelChartView1);

            while (true)
            {
                // Temperature
                Entry latestTemp = ClimateMetricService.GetTemperatureEntry(_cooltainerId);
                temperatureEntries.Add(latestTemp);

                TrimGraphToTimespan(temperatureEntries);
                temperatureChartView.Chart = new LineChart() { Entries = temperatureEntries };

                // Humidity
                List<Entry> humidityEntries = ClimateMetricService.GetHumidityEntryList(_cooltainerId);
                humidityChartView.Chart = new DonutChart() { Entries = humidityEntries };

                // CO2
                List<Entry> co2Entries = ClimateMetricService.GetCo2LevelEntryList(_cooltainerId);
                co2ChartView.Chart = new DonutChart() { Entries = co2Entries };

                // pressure
                List<Entry> pressureEntries = ClimateMetricService.GetPressureEntryList(_cooltainerId);
                pressureChartView.Chart = new DonutChart() { Entries = pressureEntries };


                // Watering tank level
                var latestWaterLevel = waterLevelEntries.Last().Value - 1;
                Entry latestWaterLevelEntry = new Entry(latestWaterLevel)
                {
                    Label = DateTime.Now.ToString("h:mm:ss tt"),
                    ValueLabel = latestWaterLevel.ToString() + "liters",
                    Color = SKColor.Parse("#1E75D6")
                };

                waterLevelEntries.Add(latestWaterLevelEntry);
                TrimGraphToTimespan(waterLevelEntries);
                waterLevelChartView.Chart = new LineChart() { Entries = waterLevelEntries };

                await Task.Delay(1000);
            }
        }

        private void TrimGraphToTimespan(List<Entry> entries)
        {
            if (entries.Count > _graphMaxTimespan)
            {
                entries.RemoveAt(0);
            }
        }
    }
}

