namespace OrderService.Dtos;

public class OrderCreateDto
{
    public string Email { get; set; }
    public string Address { get; set; }
    public ICollection<OrderItemCreateDto> OrderItems { get; set; }
}