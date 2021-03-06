﻿using System.Threading.Tasks;
using Demo.Monolith.API.Contracts.v1;
using Demo.Monolith.API.Repositories.Interfaces;

namespace Demo.Monolith.API.Managers
{
    public class OrderManager
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShipmentRepository _shipmentRepository;

        public OrderManager(IOrderRepository orderRepository, IShipmentRepository shipmentRepository)
        {
            _orderRepository = orderRepository;
            _shipmentRepository = shipmentRepository;
        }

        public async Task<OrderConfirmation> CreateAsync(Order createdOrder)
        {
            var confirmationId = await _orderRepository.CreateAsync(createdOrder);
            var shipmentInformation = await _shipmentRepository.CreateAsync(createdOrder);

            var orderConfirmation = new OrderConfirmation
            {
                ConfirmationId = confirmationId,
                ShipmentInformation = shipmentInformation,
                Customer = createdOrder.Customer
            };

            return orderConfirmation;
        }
    }
}
