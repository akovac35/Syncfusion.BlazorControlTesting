using com.github.akovac35.Logging;
using com.github.akovac35.Logging.Correlation;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Syncfusion.Blazor.Grids;
using Syncfusion.BlazorControlTesting.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Syncfusion.BlazorControlTesting.Pages
{
    public partial class GridValidations : ComponentBase
    {
        private TestingDtoValidator TestingDtoValidatorInstance = new TestingDtoValidator();

        private List<TestingDto> Data = new List<TestingDto>();

        private List<Summary> Summaries = new List<Summary>() {
            new Summary(){ Id= 1, SummaryText= "A" },
            new Summary(){ Id= 2, SummaryText= "B" },
            new Summary(){ Id= 3, SummaryText= "C" },
            new Summary(){ Id= 4, SummaryText= "D" }
        };

        public class AttachmentDeleteCommandButtonOptions : CommandButtonOptions
        {
            public AttachmentDeleteCommandButtonOptions()
            {
                Content = "Delete";
                CssClass = "e-danger";
            }
        }

        public class TestingDtoValidator : AbstractValidator<TestingDto>
        {
            public TestingDtoValidator()
            {
                RuleFor(item => item.Summary)
                .Custom((item, context) =>
                {
                    if (item?.Id == null)
                        context.AddFailure("Invalid");
                });
            }
        }

        public class Summary
        {
            public int Id { get; set; }
            public string? SummaryText { get; set; }
        }

        public class TestingDto
        {
            public string Id { get; set; } = Guid.NewGuid().ToString();
            public Summary? Summary { get; set; }
        }
    }
}
