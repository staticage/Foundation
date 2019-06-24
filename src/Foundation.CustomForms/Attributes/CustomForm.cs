using System;

namespace Foundation.CustomForm.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomFormAttribute : Attribute
    {
        public string Label { get; set; }
        public CustomFormAttribute(string label)
        {
            Label = label;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class FormFieldAttribute : Attribute
    {
        public string Label { get; set; }
        public FormFieldAttribute(string label)
        {
            Label = label;
        }
    }
}