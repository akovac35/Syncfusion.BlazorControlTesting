using Microsoft.AspNetCore.Components;

namespace Syncfusion.BlazorControlTesting.Pages.Accordion
{
    public abstract partial class ContainerComponentBase<TValue>
        where TValue : class
    {
        [Parameter]
        public string SetEditableButtonColumnCssClass { get; set; } = "col-3";
        [Parameter]
        public string SetEditableButtonLabel { get; set; } = "Edit";
        [Parameter]
        public string SetEditableButtonCssClass { get; set; }

        [Parameter]
        public string CancelEditingButtonColumnCssClass { get; set; } = "col-3";
        [Parameter]
        public string CancelEditingButtonLabel { get; set; } = "Cancel";
        [Parameter]
        public string CancelEditingButtonCssClass { get; set; }

        [Parameter]
        public string CompleteEditingButtonColumnCssClass { get; set; } = "col-3";
        [Parameter]
        public string CompleteEditingButtonLabel { get; set; } = "Complete";
        [Parameter]
        public string CompleteEditingButtonCssClass { get; set; } = "e-danger";
    }
}
