using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;

namespace MySharedComponents
{
    public partial class Component1
    {
        [Parameter]
        public string ParentName { get; set; }

        [Parameter]
        public string Text { get; set; }

        [Parameter]
        public EventCallback<string> TextChanged { get; set; }

        [Inject]
        private IJSRuntime JsRuntime { get; set; }

        //[Inject]
        //private IStringLocalizer<Resources.MySharedComponents> L { get; set; }

        private async Task GetTextAsync()
        {
            //var text = await ExampleJsInterop.Prompt(JsRuntime, L[Resources.MySharedComponents.EnterSomeText], Text);
            var text = await ExampleJsInterop.Prompt(JsRuntime, Resources.MySharedComponents.EnterSomeText, Text);

            await TextChanged.InvokeAsync(text);

            Text = text;
        }
    }
}
