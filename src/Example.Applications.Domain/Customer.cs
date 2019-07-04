using Foundation.CustomForm.Attributes;

namespace Example.Applications.Domain
{
    [CustomForm("客户")]
    public class Customer 
    {
        public int Id { get; set; }
        [FormField("姓名")]
        public string Name { get; set; }
        
        [FormField("年龄")]
        public int Age { get; set; }
        
        [FormField("身份证")]
        public string IdCard { get; set; }
        
        [FormField("手机号")]
        public string Mobile { get; set; }
        
        [FormField("照片")]
        public string Photo { get; set; }
        
        [FormField("来源")]
        public string Source { get; set; }
    }
}