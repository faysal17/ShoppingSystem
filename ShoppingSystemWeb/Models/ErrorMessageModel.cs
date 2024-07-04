namespace ShoppingSystemWeb.Models
{
    [Serializable]
    public class ErrorMessageModel
    {
        public string? Message { get; set; }
        public string? StackTrace { get; set; }
    }
}
