using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime;

namespace Foundation.CustomForm
{
    public class CustomField
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public FieldInput Input { get; set; }
        public bool IsShowInTable { get; set; }
        public QueryOptions QueryOptions { get; set; }
        
    }



    public class FieldInput
    {
        public string Type { get; set; }
        public List<ValidationMethod> ValidationMethods { get; set; } = new List<ValidationMethod>();
        public OptionsSetting Options { get; set; } = new OptionsSetting();
    }

    public class QueryOptions
    {
        public QueryType Type { get; set; }
    }
    
//    public enum FieldType
//    {
//        [Description("文本")]
//        Text,
//        
//        [Description("数字")]
//        Number,
//        
//        [Description("多行文本")]
//        Textarea,
//        
//        [Description("日期")]
//        Date,
//        
//        [Description("图片")]
//        Image,
//        
//        [Description("下拉选框")]
//        Dropdown,
//        
//        [Description("多选框")]
//        Checkbox,
//        
//        [Description("单选框")]
//        Radio,
//        
//        [Description("开关")]
//        Switch,
//        
//        [Description("富文本")]
//        RichText
//    }
}