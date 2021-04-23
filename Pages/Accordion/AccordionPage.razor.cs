using com.github.akovac35.Logging;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Syncfusion.BlazorControlTesting.Pages.Accordion
{
    public partial class AccordionPage : ComponentBase
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        [Inject]

        private ILogger<AccordionPage> Logger { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        SampleContainerComponentModel ContainerComponentValue { get; set; } = new();
        string? TextBoxValue { get; set; } = null;

        Task ContainerComponentValueChanged(SampleContainerComponentModel value)
        {
            ContainerComponentValue = value;
            Logger.Here(l => l.LogInformation("{@0}", value));
            return Task.CompletedTask;
        }

        Task TextBoxValueChanged(string? value)
        {
            TextBoxValue = value;
            Logger.Here(l => l.LogInformation("{@0}", value));
            return Task.CompletedTask;
        }

        GridWithDropDownListComponentModel GridWithDropDownListComponentValue { get; set; } = new();
        Task OnGridWithDropDownListComponentValueChangedAsync(GridWithDropDownListComponentModel value)
        {
            GridWithDropDownListComponentValue = value;
            Logger.Here(l => l.LogInformation("{@0}", value));
            return Task.CompletedTask;
        }

    }
}
