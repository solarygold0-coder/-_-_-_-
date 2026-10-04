namespace TransactionManagementSystem.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Color { get; set; } = "#2196F3";
        public TransactionType TransactionType { get; set; }
    }
}
