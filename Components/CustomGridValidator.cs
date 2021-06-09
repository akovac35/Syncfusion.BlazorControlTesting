using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Syncfusion.Blazor.Grids;
using System;

namespace Syncfusion.BlazorControlTesting.Components
{
    public class CustomGridValidator<TRow> : ComponentBase, IDisposable
    {
        [Parameter]
        public ValidatorTemplateContext Context { get; set; } = null!;

        [CascadingParameter]
        public EditContext CurrentEditContext { get; set; } = null!;

        [Parameter]
        public AbstractValidator<TRow> Validator { get; set; } = null!;

        private ValidationMessageStore messageStore { get; set; } = null!;
        private bool disposedValue;
        private bool isInitialized;

        protected override void OnInitialized()
        {
            if (CurrentEditContext == null) throw new ArgumentNullException(nameof(CurrentEditContext));
            if (Context == null) throw new ArgumentNullException(nameof(Context));
            if (Validator == null) throw new ArgumentNullException(nameof(Validator));

            messageStore = new ValidationMessageStore(CurrentEditContext);

            if (!isInitialized)
            {
                CurrentEditContext.OnValidationRequested += ValidateRequested;
                CurrentEditContext.OnFieldChanged += ClearFieldMessage;
                isInitialized = true;
            }

            base.OnInitialized();
        }

        private void ClearFieldMessage(object? editContext, FieldChangedEventArgs fieldChangedEventArgs)
        {
            messageStore.Clear(fieldChangedEventArgs.FieldIdentifier);
            CurrentEditContext.NotifyValidationStateChanged();
        }

        private void ValidateRequested(object? editContext, ValidationRequestedEventArgs validationEventArgs)
        {
            messageStore.Clear();

            var tmp = (TRow)Context.Data;

            var validationResult = Validator.Validate(tmp);

            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.Errors)
                {
                    messageStore.Add(new FieldIdentifier(tmp, item.PropertyName), item.ErrorMessage);
                }
            }

            CurrentEditContext.NotifyValidationStateChanged();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (CurrentEditContext != null)
                    {
                        CurrentEditContext.OnValidationRequested -= ValidateRequested;
                        CurrentEditContext.OnFieldChanged -= ClearFieldMessage;
                    }
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

}