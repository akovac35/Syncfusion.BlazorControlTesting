﻿using com.github.akovac35.Logging;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Syncfusion.BlazorControlTesting.Pages
{
    public partial class ButtonTesting : ComponentBase
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        [Inject]
        ILogger<ButtonTesting> Logger { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        async Task OnClickAsync()
        {
            Logger.Here(l => l.Entering(LogLevel.Information));

            await Task.Delay(200);

            // Here the screen should NOT be updated as per Blazor lifecycle rules, we need to use StateHasChanged

            await Task.Delay(200);

            Logger.Here(l => l.Exiting(LogLevel.Information));
        }

        Task OnClickWithTaskContinuationAsync()
        {
            Logger.Here(l => l.Entering(LogLevel.Information));

            return Task.Delay(400).ContinueWith(antecedent => Logger.Here(l => l.Exiting(LogLevel.Information)));
        }

        async Task OnClickWithFakedTaskAsync()
        {
            Logger.Here(l => l.Entering(LogLevel.Information));

            // Trigger Blazor lifecycle methods ...
            await Task.Delay(50);

            Task.Delay(350).Wait();

            Logger.Here(l => l.Exiting(LogLevel.Information));
        }

        void OnClick()
        {
            Logger.Here(l => l.Entering(LogLevel.Information));

            Task.Delay(400).Wait();

            Logger.Here(l => l.Exiting(LogLevel.Information));
        }


        bool IsDisabled { get; set; } = false;
        void OnClickWithDisabling()
        {            
            Logger.Here(l => l.Entering(LogLevel.Information));

            if (IsDisabled)
                goto RETURN;

            IsDisabled = true;

            try
            {
                Task.Delay(400).Wait();
            }
            finally
            {
                IsDisabled = false;
            }

            RETURN:
            Logger.Here(l => l.Exiting(LogLevel.Information));
        }
    }
}
