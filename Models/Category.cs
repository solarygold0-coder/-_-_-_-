using System.Collections.Generic;

namespace TransactionManagementSystem.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}