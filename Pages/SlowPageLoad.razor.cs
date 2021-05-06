using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Syncfusion.BlazorControlTesting.Pages
{
    public partial class SlowPageLoad
    {
        private bool IsInitialized { get; set; }

        protected override void OnInitialized()
        {
            if(!IsInitialized)
            {
                Task.Delay(3000).Wait();
                IsInitialized = true;
            }

            base.OnInitialized();
        }
    }
}
