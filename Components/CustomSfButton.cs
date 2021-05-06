using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Reflection;
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

    public class CustomSfButton3 : Syncfusion.Blazor.Buttons.SfButton
    {
        [Inject]
        protected NavigationManager NavigationManagerInstance { get; set; } = null!;

        [Parameter]
        public EventCallback<MouseEventArgs> OnCustomClick { get; set; }

        [Parameter]
        public int ClickFilterMilliSeconds { get; set; } = 500;

        private bool IsInitialized { get; set; }
        private bool IsLocationChanged { get; set; }
        private int OnClickHash { get; set; }
        private DateTime? PreviousClick { get; set; }

        protected override void OnInitialized()
        {
            // Blazor server side controls are initialized two times ...
            if (!IsInitialized)
            {
                NavigationManagerInstance.LocationChanged += OnLocationChanged;
                IsInitialized = true;
            }

            OnClick = EventCallback.Factory.Create(this, OnCustomClickHanlder);
            OnClickHash = HashCode.Combine(OnClick);

            base.OnInitialized();
        }

        protected override void OnParametersSet()
        {
            if (OnClickHash != HashCode.Combine(OnClick))
                throw new ArgumentException($"Please use property {nameof(OnCustomClick)} instead of {nameof(OnClick)}.", nameof(OnClick));

            base.OnParametersSet();
        }

        private async Task OnCustomClickHanlder(MouseEventArgs args)
        {
            if (Disabled || !OnCustomClick.HasDelegate)
                return;

            // Handle unintentional double clicks
            if (PreviousClick != null && DateTime.Now.Subtract(PreviousClick.Value).TotalMilliseconds <= ClickFilterMilliSeconds)
                return;

            Disabled = true;

            try
            {
                if (IsCallbackType(OnCustomClick, typeof(Func<Task>)))
                {
                    await OnCustomClick.InvokeAsync();
                }
                else if (IsCallbackType(OnCustomClick, typeof(Action)))
                {
                    // Trigger blazor page update lifecycle ...
                    await Task.Delay(50);

                    await OnCustomClick.InvokeAsync();
                }
                else if (IsCallbackType(OnCustomClick, typeof(Func<MouseEventArgs, Task>)))
                {
                    await OnCustomClick.InvokeAsync(args);
                }
                else if (IsCallbackType(OnCustomClick, typeof(Action<MouseEventArgs>)))
                {
                    // Trigger blazor page update lifecycle ...
                    await Task.Delay(50);

                    await OnCustomClick.InvokeAsync(args);
                }
                else throw new NotSupportedException();
            }
            finally
            {
                if (!(IsDisposed || IsLocationChanged)) Disabled = false;
                PreviousClick = DateTime.Now;
            }
        }
        
        private void OnLocationChanged(object? source, LocationChangedEventArgs args)
        {
            Disabled = true;
            IsLocationChanged = true;
        }

        private bool IsCallbackType(object callback, Type t)
        {
            Type typ;
            if (callback is EventCallback)
            {
                typ = typeof(EventCallback);
            }
            else if (callback is EventCallback<MouseEventArgs>)
            {
                typ = typeof(EventCallback<MouseEventArgs>);
            }
            else
                throw new NotSupportedException();

            FieldInfo? delField = typ.GetField("Delegate", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            if (delField == null) throw new NotSupportedException();
            return delField.GetValue(callback)?.GetType() == t;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                NavigationManagerInstance.LocationChanged -= OnLocationChanged;
                Disabled = true;
            }
        }
    }
}
