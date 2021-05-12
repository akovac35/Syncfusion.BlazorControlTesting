using Syncfusion.BlazorControlTesting.Components;
using System;

namespace Sberbank.BlazorComponents.Common.Client.Components
{
    public class CustomNumberInputComponent<TValue> : Syncfusion.Blazor.Inputs.SfNumericTextBox<TValue>
    {
        public const string IntegerFormat = "#,##0";

        protected override void OnInitialized()
        {
            var isTypeNullable = TypeHelper.IsTypeNullable<TValue>();
            var isTypeFloatingPoint = TypeHelper.IsTypeFloatingPoint<TValue>();

            Width = "100%";
            ShowClearButton = isTypeNullable;
            Decimals = isTypeFloatingPoint ? 2 : 0;
            ValidateDecimalOnType = true;

            if (base.Decimals.HasValue && base.Decimals.Value != 0)
            {
                Format = $"{IntegerFormat}.{new String('#', base.Decimals.Value)}";
            }
            else
            {
                Format = IntegerFormat;
            }

            FloatLabelType = Syncfusion.Blazor.Inputs.FloatLabelType.Always;

            base.OnInitialized();
        }
    }
}
