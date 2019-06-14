using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime;

namespace Foundation.CustomForm
{
    public class CustomField
    {
        public string PropertyName { get; set; }
        public string Label { get; set; }
        public FieldInput Input { get; set; }
        
    }

    public class CustomFieldGroup
    {
        public string Label { get; set; }
        public List<CustomField> Fields { get; set; } = new List<CustomField>();
    }

    public class FieldInput
    {
        public FieldType Type { get; set; }
        public List<ValidationMethod> ValidationMethods { get; set; } = new List<ValidationMethod>();
    }
    
    public enum FieldType
    {
        [Description("文本")]
        Text,
        
        [Description("数字")]
        Number,
        
        [Description("多行文本")]
        Textarea,
        
        [Description("日期")]
        Date,
        
        [Description("图片")]
        Image,
        
        [Description("下拉选框")]
        Dropdown,
        
        [Description("多选框")]
        Checkbox,
        
        [Description("单选框")]
        Radio,
        
        [Description("开关")]
        Switch,
        
        [Description("富文本")]
        RichText
    }
}