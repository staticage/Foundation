using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Foundation.CustomForm
{
    public class CustomForm
    {
        public int Id { get; set; }
        public string EntityType { get; set; }
        public string Label { get; set; }
        public List<CustomFieldGroup> FieldGroups { get; set; } = new List<CustomFieldGroup>();

        public static CustomForm Create(CustomFormMetadata metadata)
        {
            return new CustomForm
            {
                EntityType = metadata.TypeMetadata.Type,
                FieldGroups = new List<CustomFieldGroup>
                {
                    new CustomFieldGroup
                    {
                        Name = "基本信息",
                        Fields = metadata.Properties.Select(x=> new CustomField
                        { 
                            PropertyName = x.Name,
                            Input = new FieldInput
                            {
                                Type = FieldType.Text,
                            },
                            Label = x.Label
                        }).ToList()
                    }
                }
            };
        }
    }
}