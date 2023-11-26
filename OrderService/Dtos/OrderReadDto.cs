namespace OrderService.Dtos;

public class OrderReadDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public double Total { get; set; }
    public ICollection<OrderItemReadDto> OrderItems { get; set; }
}