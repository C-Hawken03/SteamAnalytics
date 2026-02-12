using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Product {
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    public string? Description { get; set; }

    [Range(0, double.MaxValue)]
    [Column(TypeName = "decimal(10,2)")] // Important for MySQL
    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
