using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using Syncfusion.Blazor.DropDowns;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

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
