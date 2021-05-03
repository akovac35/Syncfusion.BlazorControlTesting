using Syncfusion.Blazor.DropDowns;

namespace Syncfusion.BlazorControlTesting.Components
{
    public partial class CustomizedSfDropDownList2<TValue, TItem> : SfDropDownList<TValue, TItem>
    {
        protected override void OnInitialized()
        {
            Width = "100%";
            AllowFiltering = true;
            FloatLabelType = Syncfusion.Blazor.Inputs.FloatLabelType.Always;

            base.OnInitialized();

        }
    }
}
