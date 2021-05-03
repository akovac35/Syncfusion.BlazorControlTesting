using Syncfusion.Blazor.Grids;
using System;
using System.Collections.Generic;

namespace Syncfusion.BlazorControlTesting.Pages.Accordion
{
    public partial class GridWithDropDownListComponent
    {
        protected SfGrid<ModelItem>? GridComponentInstance { get; set; }
        protected List<DropDownListItem> Values { get; set; } = new();

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Values.Add(new DropDownListItem("1", "Value 1"));
            Values.Add(new DropDownListItem("2", "Value 2"));
            Values.Add(new DropDownListItem("3", "Value 3"));
        }

        protected void AddRow()
        {
            ModelValue.Value.Items.Add(new ModelItem());
            GridComponentInstance?.Refresh();
        }
    }

    public class GridWithDropDownListComponentModel
    {
        public List<ModelItem> Items { get; set; } = new();
    }

    public class ModelItem
    {
        public string MyGuid { get; set; } = Guid.NewGuid().ToString();
        public DropDownListItem? Value { get; set; }
    }

    public class DropDownListItem
    {
        public DropDownListItem() { }

        public DropDownListItem(string? key, string? value)
        {
            Key = key;
            Value = value;
        }

        public string? Key { get; init; }
        public string? Value { get; init; }
    }
}
