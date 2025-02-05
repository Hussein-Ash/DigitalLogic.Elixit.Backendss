using AutoMapper;
using AutoMapper.QueryableExtensions;
using Elixir.DATA;
using Elixir.DATA.DTOs;
using Elixir.DATA.DTOs.Order;
using Elixir.Entities;
using Elixir.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Elixir.Services;

public interface IOrderService
{
    Task<(List<StoreOrdersDto>? dtos, int? totalCount, string? error)> GetAllStoreOrders(OrderFilter filter, Guid storeId);
    Task<(List<UserOrdersDto>? dtos, int? totalCount, string? error)> GetAllUserOrders(OrderFilter filter, Guid userId);
    Task<(List<StoreOrdersDto>? dtos, int? totalCount, string? error)> GetAll(OrderFilter filter);

    Task<(OrderDto? dto, string? error)> Add(OrderForm form, Guid userId);

    Task<(OrderDto? dto, string? error)> Update(Guid id, OrderUpdate update);
    Task<(OrderDto? dto, string? error)> AdminUpdate(Guid id, OrderUpdate update,Guid userId);


    Task<(OrderDto? dto, string? error)> UserUpdate(Guid id, OrderUpdate update);

    Task<(string? Dto, string? error)> DeleteProductInOrder(Guid id, Guid userId);

    Task<(OrderDto? Dto, string? error)> Delete(Guid id, Guid userId);
    Task<(OrderDto? Dto, string? error)> GetById(Guid id);




}

public class OrderService : IOrderService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IUserClaimsService _claim;

    public OrderService(IMapper mapper, DataContext context, IUserClaimsService claim)
    {
        _mapper = mapper;
        _context = context;
        _claim = claim;
    }

    public async Task<(OrderDto? dto, string? error)> Add(OrderForm form, Guid userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId && !x.Deleted);
        if (user == null) return (null, "user not found");
        if (form.ProductInOrders == null) return (null, "no items");
        var newProducts = new List<ProductInOrder>();
        decimal sum = 0;
        foreach (var item in form.ProductInOrders)
        {
        var products = _mapper.Map<ProductInOrder>(item);
        newProducts.Add(products);
        var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x=>x.Id == item.ProductId);
        if(product == null)return(null,"error with product id");
        sum = sum + product.Price;

        }
        var newOrder = new Order
        {
            UserId = userId,
            UserAddressId = form.UserAddressId,
            Products = newProducts,
            DeliveryType = form.DeliveryType,
            PaymentMethod = form.PaymentMethod,
            Status = OrderState.pending
        };
        newOrder.Rating = form.Rating;
        decimal deliveryFee = 5;
        if (form.DeliveryType == DeliveryType.fast)
        {
            deliveryFee = 10;
        }

        newOrder.TotalPrice = sum + deliveryFee;
        await _context.Orders.AddAsync(newOrder);
        await _context.SaveChangesAsync();
        return (_mapper.Map<OrderDto>(newOrder), null);
    }

    public async Task<(OrderDto? Dto, string? error)> Delete(Guid id, Guid userId)
    {
        var order = await _context.Orders.Include(x=>x.Products).FirstOrDefaultAsync(x => x.Id == id && !x.Deleted);
        if (order == null)
            return (null, "order not found");

        order.Deleted = true;
        order.Products.ForEach(x=>x.Deleted = true);
        var result = _context.Orders.Update(order);
        if (result == null)
            return (null, "Failed to delete Order");
        await _context.SaveChangesAsync();
        return (_mapper.Map<OrderDto>(order), null);
    }

    public async Task<(List<StoreOrdersDto>? dtos, int? totalCount, string? error)> GetAllStoreOrders(OrderFilter filter, Guid storeId)
    {
        var store = await _context.Stores.FirstOrDefaultAsync(x=>x.Id == storeId);
        if(store == null) return(null,null,"Store not found");
        var query = _context.ProductInOrders
            .Include(x=>x.Order)
            .ThenInclude(y=>y.User)
            .Where(x => !x.Deleted  && x.Product.StoreId == storeId)
            .Where(y=>y.Order.Deleted != true)
            .AsQueryable();

        var totalCount = await query.CountAsync();

        var orders = await query.Paginate(filter)
            .Where(x=>x.Order.Deleted != true)
            .ProjectTo<StoreOrdersDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return (orders, totalCount, null);
    }
    public async Task<(List<StoreOrdersDto>? dtos, int? totalCount, string? error)> GetAll(OrderFilter filter)
    {
        var query = _context.ProductInOrders
            .Include(x=>x.Order)
            .ThenInclude(y=>y.User)
            .Where(x => !x.Deleted)
            .AsQueryable();

        var totalCount = await query.CountAsync();

        var orders = await query.Paginate(filter)
            .ProjectTo<StoreOrdersDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
            

        return (orders, totalCount, null);
    }

    public async Task<(List<UserOrdersDto>? dtos, int? totalCount, string? error)> GetAllUserOrders(OrderFilter filter, Guid userId)
    {
        var query = _context.Orders.Where(x => !x.Deleted && x.UserId == userId);
        var totalCount = await query.CountAsync();
        var userOrders = await query.Paginate(filter)
        .ProjectTo<UserOrdersDto>(_mapper.ConfigurationProvider)
        .ToListAsync();

        return (userOrders, totalCount, null);
    }

    public async Task<(OrderDto? Dto, string? error)> GetById(Guid id)
    {
        var content = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
        if (content == null) return (null, "not found");
        var contentDto = _mapper.Map<OrderDto>(content);
        return (contentDto, null);
    }

    public async Task<(OrderDto? dto, string? error)> Update(Guid id, OrderUpdate update)
    {
        var userId = _claim.GetUserId();
        var user = await _context.UserStores.FirstOrDefaultAsync(x => x.UserId == userId && !x.Deleted && x.StoreId == id);

        if (user == null)
            return (null, "User not found");

        var Order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id && !x.Deleted);

        if (Order == null)
            return (null, "Order not found");
        _mapper.Map(update, Order);
        var result = _context.Orders.Update(Order).Entity;
        if (result == null)
            return (null, "Failed to update Order");
        await _context.SaveChangesAsync();
        return (_mapper.Map<OrderDto>(result), null);
    }

    public async Task<(OrderDto? dto, string? error)> UserUpdate(Guid id, OrderUpdate update)
    {
        if (update.Status != OrderState.canceled) return (null, "you only allowed to cancel");

        var userId = _claim.GetUserId();
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId && !x.Deleted);

        if (user == null)
            return (null, "User not found");

        var Order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id && !x.Deleted && x.UserId == userId);

        if (Order == null)
            return (null, "Order not found");
        if ((int)Order.Status < 3) return (null, "you can not cancel the order");
        _mapper.Map(update, Order);
        var result = _context.Orders.Update(Order).Entity;
        if (result == null)
            return (null, "Failed to update Order");
        await _context.SaveChangesAsync();
        return (_mapper.Map<OrderDto>(result), null);

    }

    public async Task<(OrderDto? dto, string? error)> AdminUpdate(Guid id, OrderUpdate update,Guid userId)
    {

        // var userId = _claim.GetUserId();
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId && !x.Deleted);

        if (user == null)
            return (null, "User not found");

        var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id && !x.Deleted );

        if (order == null)
            return (null, "Order not found");
        order.Status = update.Status;
        var result = _context.Orders.Update(order).Entity;
        if (result == null)
            return (null, "Failed to update Order");
        await _context.SaveChangesAsync();
        return (_mapper.Map<OrderDto>(result), null);
    }

    public async Task<(string? Dto, string? error)> DeleteProductInOrder(Guid id, Guid userId)
    {
        var productInOrder = await _context.ProductInOrders.FirstOrDefaultAsync(x => x.Id == id && !x.Deleted);
        if (productInOrder == null)
            return (null, "productInOrder not found");

        productInOrder.Deleted = true;
        var result = _context.ProductInOrders.Update(productInOrder);
        if (result == null)
            return (null, "Failed to delete Order");
        await _context.SaveChangesAsync();
        return ("done", null);
    }
}
