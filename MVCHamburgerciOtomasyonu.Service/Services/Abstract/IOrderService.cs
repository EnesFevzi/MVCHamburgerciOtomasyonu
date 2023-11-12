using MVCHamburgerciOtomasyonu.Service.DTOs.Orders;

namespace MVCHamburgerciOtomasyonu.Service.Services.Abstract
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetAllOrdersNonDeletedAsync();
        Task<List<OrderDto>> GetAllOrdersDeletedAsync();
        Task<List<OrderDto>> GetUserOrdersNonDeletedAsync();
        Task<List<OrderDto>> GetUserOrdersDeletedAsync();
        Task<OrderDto> GetOrderWithUserNonDeletedAsync(Guid orderID);
        Task<OrderDto> GetOrder(Guid orderID);
        Task<decimal> CalculateOrder(OrderAddDto orderAddDto);
        Task CreateOrderAsync(OrderAddDto orderAddDto);
        Task<string> UpdateOrderAsync(OrderUpdateDto OrderUpdateDto);
        Task<string> SafeDeleteOrderAsync(Guid orderID);
        Task<string> UndoDeleteOrderAsync(Guid orderID);
        Task<string> ChangeStatusCancel(Guid orderID);
        Task<string> ChangeStatusPreparing(Guid orderID);
        Task<string> ChangeStatusOntheRoad(Guid orderID);
        Task<string> ChangeStatusCompleted(Guid orderID);

        Task<List<OrderDto>> GetCancelOrdersNonDeletedAsync();
        Task<List<OrderDto>> GetOnRoadOrdersNonDeletedAsync();
        Task<List<OrderDto>> GetPreparingOrdersNonDeletedAsync();
        Task<List<OrderDto>> GetCompletedOrdersNonDeletedAsync();
    }
}
