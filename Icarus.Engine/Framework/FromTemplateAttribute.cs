using System;

namespace Icarus.Engine.Framework
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class FromTemplateAttribute : Attribute
    {
        public FromTemplateAttribute(string name = null)
        {
            Name = name;
        }

        public string Name { get; }
    }
}