using System;
using System.Threading.Tasks;
using Bunit;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;
using Xunit;
using static Bunit.ComponentParameterFactory;
using static MySharedComponents.ElementIds;

namespace MySharedComponents.Tests
{
    public class Component1UnitTests
    {
        [Fact]
        public async Task ItShouldDisplayTheParentName()
        {
            var someValue = "Here is the parent name value";

            using var ctx = new TestContext();

            // There are different ways to setup the component:
            // First with the ComponentParameterFactory static helpers.
            var renderedComponent = ctx.RenderComponent<Component1>(
                Parameter(nameof(Component1.ParentName), someValue));

            //// Second with the parameter builder.
            //var renderedComponent = ctx.RenderComponent<Component1>(
            //    builder =>
            //    {
            //        builder.Add(c => c.ParentName, someValue);
            //    });

            // Assert: first find the parent_name strong element, then verify its content.
            Assert.Equal(someValue, renderedComponent.Find(CssSelector(ParentNameId)).TextContent);
        }

        [Fact]
        public async Task ItShouldDisplayTextFromJsRunTimeOnClick()
        {
            var someValue = "initial value...";
            var newValue = "Test new value";

            using var ctx = new TestContext();

            // Mock JsRunTime that will return Text new value.
            var jsrMock = new Mock<IJSRuntime>();
            jsrMock
                .Setup(r => r.InvokeAsync<string>(ExampleJsInterop.JsShowPromptIdentifier, It.IsAny<object[]>()))
                .ReturnsAsync(() => newValue);

            // Register the jsrMock as singleton.
            ctx.Services.AddSingleton(jsrMock.Object);

            string textChangedEventTriggered = null;

            var renderedComponent = ctx.RenderComponent<Component1>(
                builder =>
                {
                    builder.Add(c => c.Text, someValue);
                    builder.Add(c => c.TextChanged, e =>
                    {
                        textChangedEventTriggered = e;
                    });
                });

            // Assert: first find the <text> element, then verify its initial content.
            Assert.Equal(someValue, renderedComponent.Find(CssSelector(TextValueId)).TextContent);

            Assert.Equal(someValue, renderedComponent.Instance.Text);

            Assert.Null(textChangedEventTriggered);

            // Find and click the <button> element to set the
            // text element value from the jsRuntime mock value.
            await renderedComponent.Find("button").ClickAsync(new MouseEventArgs());

            // Assert: find the <text> element with its ID, then verify its content.
            Assert.Equal(newValue, renderedComponent.Find(CssSelector(TextValueId)).TextContent);

            // Assert: verify the component property has been updated.
            Assert.Equal(newValue, renderedComponent.Instance.Text);

            // Assert: verify that the event has been triggered with the new text value.
            Assert.NotNull(textChangedEventTriggered);
            Assert.Equal(newValue, textChangedEventTriggered);
        }
    }
}
