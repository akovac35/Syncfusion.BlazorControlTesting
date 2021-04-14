using com.github.akovac35.Logging;
using com.github.akovac35.Logging.Correlation;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Syncfusion.Blazor.Grids;
using Syncfusion.BlazorControlTesting.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Syncfusion.BlazorControlTesting.Pages
{
    public partial class GridTesting: ComponentBase
    {
        private bool disposedValue;

        [Inject]
        WeatherForecastContextFactory ContextFactory { get; set; }
        [Inject]
        ILogger<GridTesting> Logger { get; set; }
        [Inject]
        CorrelationProviderAccessor CorrelationAccessor { get; set; }

        WeatherForecastContext Context { get; set; }
        IQueryable<WeatherForecast> Forecasts { get; set; }
        List<WeatherForecast> ForecastsList { get; set; }

        protected override void OnInitialized()
        {
            Logger.Here(l => l.Entering());

            base.OnInitialized();

            Context = ContextFactory.Create();
            Forecasts = Context.Forecasts.AsQueryable();
            ForecastsList = Forecasts.ToList();

            Logger.Here(l => l.Exiting());
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

        protected virtual Task OnGridActionFailureAsync(Syncfusion.Blazor.Grids.FailureEventArgs failureEvent)
        {
            Logger.Here(l => l.LogError(failureEvent.Error, failureEvent.Error.Message));

            return Task.CompletedTask;
        }

        protected virtual async Task OnGridActionCompleteAsync(ActionEventArgs<WeatherForecast> args)
        {
            Logger.Here(l => l.Entering(args.Action, args.RowIndex, args.Type, args.RequestType));

            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Delete)
            {
                Logger.Here(l => l.LogInformation("Delete invoked."));
            }

            Logger.Here(l => l.Exiting());
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public class AttachmentDeleteCommandButtonOptions : CommandButtonOptions
        {
            public AttachmentDeleteCommandButtonOptions()
            {
                Content = "Delete";
                CssClass = "e-danger";
            }
        }
    }
}
