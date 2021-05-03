using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;

namespace Syncfusion.BlazorControlTesting.Components
{
    public class CustomSfButton : Syncfusion.Blazor.Buttons.SfButton
    {
        [Parameter]
        public EventCallback<MouseEventArgs> OnCustomClick { get; set; }

        protected int OnClickHash { get; set; }

        protected override void OnInitialized()
        {
            OnClick = EventCallback.Factory.Create(this, CustomOnClickHandler);
            OnClickHash = HashCode.Combine(OnClick);

            base.OnInitialized();
        }

        protected override void OnParametersSet()
        {
            if (OnClickHash != HashCode.Combine(OnClick))
                throw new ArgumentException($"Please use property {nameof(OnCustomClick)} instead of {nameof(OnClick)}.", nameof(OnClick));

            base.OnParametersSet();
        }

        protected virtual async Task CustomOnClickHandler(MouseEventArgs args)
        {
            if (Disabled || !OnCustomClick.HasDelegate)
                return;

            Disabled = true;

            try
            {
                await OnCustomClick.InvokeAsync(args);
            }
            finally
            {
                Disabled = false;
            }
        }
    }

    public class CustomSfButton2 : CustomSfButton
    {
        protected override async Task CustomOnClickHandler(MouseEventArgs args)
        {
            if (Disabled || !OnCustomClick.HasDelegate)
                return;

            Disabled = true;

            try
            {
                // Trigger Blazor lifecycle methods ...
                // While this statement is by itself not harmful, it may cause the Blazor screen updates to be not as the
                // developer expects, see this example:
                // https://github.com/akovac35/TestingBlazor/blob/59d43141cd9909b93ddf6507d7acc882dbca1423/Pages/LoadingIndicator.razor.cs#L95
                await Task.Delay(50);
                
                await OnCustomClick.InvokeAsync(args);
            }
            finally
            {
                Disabled = false;
            }
        }
    }
}
