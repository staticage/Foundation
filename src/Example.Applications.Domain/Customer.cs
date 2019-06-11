using Foundation.CustomForm.Attributes;

namespace Example.Applications.Domain
{
    [CustomForm("客户")]
    public class Customer 
    {
        [FormField("姓名")]
        public string Name { get; set; }
    }
}