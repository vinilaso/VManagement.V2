namespace VManagement.Commons.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class UseWithCaution(string reason) : Attribute
    {
        public string Reason { get; set; } = reason;
    }
}
