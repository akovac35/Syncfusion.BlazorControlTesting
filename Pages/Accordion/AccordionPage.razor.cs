using com.github.akovac35.Logging;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

#nullable enable

namespace Syncfusion.BlazorControlTesting.Pages.Accordion
{
    public partial class AccordionPage: ComponentBase
    {
        [Inject]
        private ILogger<AccordionPage> Logger { get; set; }

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
    }
}
