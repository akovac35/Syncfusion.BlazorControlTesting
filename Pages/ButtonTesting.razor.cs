using com.github.akovac35.Logging;
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

            await Task.Delay(700);

            // Here the screen should NOT be updated as per Blazor lifecycle rules, we need to use StateHasChanged

            await Task.Delay(500);

            Logger.Here(l => l.Exiting(LogLevel.Information));
        }

        async Task OnClickWithTaskContinuationAsync()
        {
            Logger.Here(l => l.Entering(LogLevel.Information));

            await Task.Delay(700).ContinueWith(async antecedent => await Task.Delay(500));

            Logger.Here(l => l.Exiting(LogLevel.Information));
        }

        void OnClick()
        {
            Logger.Here(l => l.Entering(LogLevel.Information));

            Task.Delay(1200).Wait();

            Logger.Here(l => l.Exiting(LogLevel.Information));
        }
    }
}
