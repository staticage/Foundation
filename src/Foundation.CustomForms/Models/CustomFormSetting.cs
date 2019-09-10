using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Foundation.CustomForm
{
    public class CustomForm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public List<CustomField> Fields { get; set; } = new List<CustomField>();

        public static CustomForm Create(CustomFormMetadata metadata)
        {
            return new CustomForm
            {
                Name = metadata.TypeMetadata.Type,

                Fields = metadata.Fields.Select(x => new CustomField
                {
                    Name = x.Name,
                    Input = new FieldInput
                    {
                        Type = "text",
                    },
                    Label = x.Label
                }).ToList()
            };
        }
    }

    public class Setting
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Item> Items { get; set; }


        public class Item
        {
            public bool Enabled { get; set; }
            public string Value { get; set; }
            public string Label { get; set; }
            public int SortNumber { get; set; }
        }
    }
}