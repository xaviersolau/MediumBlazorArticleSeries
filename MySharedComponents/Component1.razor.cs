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
        public const string Body = nameof(Body);
        public const string ClickMe = nameof(ClickMe);
        public const string EnterSomeText = nameof(EnterSomeText);

        [Parameter]
        public string ParentName { get; set; }

        [Parameter]
        public string Text { get; set; }

        [Parameter]
        public EventCallback<string> TextChanged { get; set; }

        [Inject]
        private IJSRuntime JsRuntime { get; set; }

        [Inject]
        private IStringLocalizer<Component1> L { get; set; }

        private async Task GetTextAsync()
        {
            var localizedText = L[EnterSomeText];

            var text = await ExampleJsInterop.Prompt(JsRuntime, localizedText, Text);
            
            await TextChanged.InvokeAsync(text);

            Text = text;
        }
    }
}
