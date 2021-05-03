using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;

namespace Syncfusion.BlazorControlTesting.Components
{
    public class CustomSfButton: Syncfusion.Blazor.Buttons.SfButton
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

        protected async Task CustomOnClickHandler(MouseEventArgs args)
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
}
