using Force.DeepCloner;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Syncfusion.BlazorControlTesting.Pages.Accordion
{
    public abstract partial class NestedContainerComponentBase<TValue> : ComponentBase
        where TValue : class
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        [Parameter]
        public TValue Value { get; set; }
        /// <summary>
        /// This delegate should throw an exception in order to stop the component from committing user input.
        /// </summary>
        [Parameter]
        public EventCallback<TValue> ValueChanged { get; set; }

        protected bool _allowEdit;
        [Parameter]
        public bool AllowEdit
        {
            get => _allowEdit;
            set
            {
                _allowEdit = value;
                if (!_allowEdit)
                {
                    Readonly = true;
                }
            }
        }

        /// <summary>
        /// Contains custom components.
        /// </summary>
        protected RenderFragment<NestedContainerComponentBaseModel<TValue>> ChildContent { get; set; }

        /// <summary>
        /// Only available while the component is NOT in <see cref="Readonly"/> mode.
        /// </summary>
        protected TValue ClonedValue { get; set; }
        protected int _previousValueHashCode;
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        /// <summary>
        /// When true, the component should display readonly child content, or editable child content otherwise.
        /// </summary>
        public bool Readonly { get; protected set; } = true;

        protected NestedContainerComponentBaseModel<TValue> ModelValue { get; set; } = new();

        /// <summary>
        /// Override in a derived class if needed.
        /// </summary>
        public virtual bool IsAlwaysEditable
        {
            get => false;
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (Value == null) throw new ArgumentNullException(nameof(Value));

            RefreshInternalContext();
        }

        public virtual Task OnSetEditableAsync()
        {
            Readonly = false;
            RefreshInternalContext();

            return Task.CompletedTask;
        }

        public virtual async Task OnCompleteEditingAsync()
        {
            if (ValueChanged.HasDelegate)
            {
                // Do not catch the exception here in case additional lines are added below
                // and we don't want to reach them if the callback throws
                await ValueChanged.InvokeAsync(ClonedValue);
            }

            Reset();
        }

        public virtual Task OnCancelEditingAsync()
        {
            Reset();

            return Task.CompletedTask;
        }

        protected virtual void Reset()
        {
            Readonly = true;
            RefreshInternalContext();
        }

        #region Helper functions
        protected virtual void RefreshInternalContext(bool forceClone = false)
        {
            if (Readonly)
            {
                Cleanup();
            }
            else
            {
                CloneValueAsNeeded(forceClone);
            }
        }

        protected virtual void CloneValueAsNeeded(bool forceClone = false)
        {
            if (forceClone || Value.GetHashCode() != _previousValueHashCode || ClonedValue == null)
            {
                ClonedValue = Value.DeepClone();
                _previousValueHashCode = Value.GetHashCode();
            }
        }

        protected virtual void Cleanup()
        {
            _previousValueHashCode = 0;
            ClonedValue = null!;
            ModelValue.Value = null!;
        }
        #endregion
    }

    public class NestedContainerComponentBaseModel<TValue>
        where TValue : class
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        // Value when container is read only, cloned value otherwise.
        public TValue Value { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    }


}
