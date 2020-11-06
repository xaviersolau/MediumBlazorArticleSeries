using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace MySharedComponents
{
    public class ExampleJsInterop
    {
        public const string JsShowPromptIdentifier = "exampleJsFunctions.showPrompt";

        public static ValueTask<string> Prompt(IJSRuntime jsRuntime, string message, string text)
        {
            // Implemented in exampleJsInterop.js
            return jsRuntime.InvokeAsync<string>(
                JsShowPromptIdentifier,
                message,
                text);
        }
    }
}
