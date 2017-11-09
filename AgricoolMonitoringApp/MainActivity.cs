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
            List<Entry> entries = new List<Entry>();
            var chartView = FindViewById<ChartView>(Resource.Id.chartView1);

            while (true)
            {
                var latestTemp = ClimateMetricService.GetTemperatureEntry(_cooltainerId);
                entries.Add(latestTemp);

                TrimGraphToTimespan(entries);

                chartView.Chart = new LineChart() { Entries = entries };

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

