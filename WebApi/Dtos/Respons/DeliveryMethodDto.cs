﻿namespace WebApi.Dtos.Respons
{
    public class DeliveryMethodDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DeliveryTime { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
