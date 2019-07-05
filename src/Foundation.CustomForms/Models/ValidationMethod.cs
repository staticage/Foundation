namespace Foundation.CustomForm
{
    public class ValidationMethod
    {
        public string Method { get; set; }
        public string ErroMessage { get; set; }
    }

    public class OptionsSetting
    {
        public int Type { get; set; }
        public string ApiUrl { get; set; }
        public int? SettingId { get; set; }
    }
}