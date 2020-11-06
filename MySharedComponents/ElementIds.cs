using System;
using System.Collections.Generic;
using System.Text;

namespace MySharedComponents
{
    public static class ElementIds
    {
        public const string ParentNameId = "parent-name";
        public const string TextValueId = "text-value";

        public static string CssSelector(string id) => $"#{id}";
    }
}
