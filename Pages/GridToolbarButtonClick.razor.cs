﻿using com.github.akovac35.Logging;
using com.github.akovac35.Logging.Correlation;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Syncfusion.BlazorControlTesting.Data;
using System;
using System.Linq;

namespace Syncfusion.BlazorControlTesting.Pages
{
    public partial class GridToolbarButtonClick : ComponentBase, IDisposable
    {
        private bool disposedValue;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        [Inject]
        WeatherForecastContextFactory ContextFactory { get; set; }
        [Inject]
        ILogger<Index> Logger { get; set; }
        [Inject]
        CorrelationProviderAccessor CorrelationAccessor { get; set; }

        WeatherForecastContext Context { get; set; }
        IQueryable<WeatherForecast> Forecasts { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        protected override void OnInitialized()
        {
            Logger.Here(l => l.Entering());

            base.OnInitialized();

            Context = ContextFactory.Create();
            Forecasts = Context.Forecasts.AsQueryable();

            Logger.Here(l => l.Exiting());
        }

        void DoNothing()
        {
            Logger.Here(l => l.LogInformation("Clicked"));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (Context != null)
                    {
                        Context.Dispose();
                    }
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
