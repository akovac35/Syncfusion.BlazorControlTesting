﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using Syncfusion.Blazor.DropDowns;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Syncfusion.BlazorControlTesting.Components
{
    public partial class CustomizedSfDropDownList<TValue, TItem> : SfDropDownList<TValue, TItem>
        where TItem : class, new()
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        [Parameter]
        public string NameOfTextProperty { get; set; }

        [Parameter]
        public string NameOfValueProperty { get; set; }

        [Parameter]
        public EventCallback<TItem?> ItemChanged { get; set; }

        [Parameter]
        public Expression<Func<TItem?>>? ItemProviderForNotifyFieldChanged { get; set; }

        [CascadingParameter]
        protected EditContext? EditContextInstance { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        protected int _valueChangedHash = 0;
        protected int _defaultValueChangedHash = 0;

        protected override void OnInitialized()
        {
            ChildContent = (RenderTreeBuilder builder) =>
            {
                builder.OpenComponent<DropDownListFieldSettings>(0);
                builder.AddAttribute(1, "Text", NameOfTextProperty);
                builder.AddAttribute(2, "Value", NameOfValueProperty);
                builder.CloseComponent();
            };

            ValueChanged = EventCallback.Factory.Create<TValue>(this, item => OnValueChangedInternalAsync(item));
            _valueChangedHash = HashCode.Combine(ValueChanged);
            _defaultValueChangedHash = _valueChangedHash;

            FloatLabelType = Syncfusion.Blazor.Inputs.FloatLabelType.Always;
            Width = "100%";
            ShowClearButton = true;
            AllowFiltering = true;

            base.OnInitialized();
        }

        protected virtual async Task OnValueChangedInternalAsync(TValue value)
        {
            var tmp = value != null ? this.GetDataByValue(value) : null;

            if (ItemChanged.HasDelegate)
            {
                await ItemChanged.InvokeAsync(tmp);
            }

            if (ItemProviderForNotifyFieldChanged != null)
            {
                var fIdent = FieldIdentifier.Create(ItemProviderForNotifyFieldChanged);
                EditContextInstance?.NotifyFieldChanged(fIdent);
            }
        }

        protected override void OnParametersSet()
        {
            var tmpValueChangedHash = HashCode.Combine(ValueChanged);
            if (_valueChangedHash != tmpValueChangedHash)
            {
                _valueChangedHash = tmpValueChangedHash;
            }

            if (_defaultValueChangedHash != _valueChangedHash && ValueChanged.HasDelegate && ItemChanged.HasDelegate)
            {
                throw new ArgumentException($"Please use either {nameof(ItemChanged)} or {nameof(ValueChanged)}, but not both.", nameof(ItemChanged));
            }

            base.OnParametersSet();
        }
    }
}
