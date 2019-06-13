using System.Collections.Generic;

namespace Foundation.CustomForm
{
    public class CustomFormSetting
    {
        public int Id { get; set; }
        public string EntityType { get; set; }
        public string Label { get; set; }
        public List<CustomField> Fields { get; set; } = new List<CustomField>();
    }
}