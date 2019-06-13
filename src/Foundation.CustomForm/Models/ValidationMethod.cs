namespace Foundation.CustomForm
{
    public class CustomField
    {
        public string PropertyName { get; set; }
        public string Label { get; set; }
        public string Component { get; set; }
        public ValidationMethod ValidationMethods { get; set; }
    }

    public class ValidationMethod
    {
        public string Method { get; set; }
        public string ErroMessage { get; set; }
    }
}