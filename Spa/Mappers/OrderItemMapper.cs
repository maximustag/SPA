﻿using Spa.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Spa.Mappers
{
    public class OrderItemMapper: EntityTypeConfiguration<OrderItem>
    {
        public OrderItemMapper()
        {
            this.ToTable("OrderItems");

            this.HasKey(oi => oi.OrderItemId);
            this.Property(oi => oi.OrderItemId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(oi => oi.OrderItemId).IsRequired();

            this.Property(oi => oi.Amount).IsRequired();

            this.HasRequired(oi => oi.Order).WithMany().Map(o => o.MapKey("OrderId"));
            this.HasRequired(oi => oi.Product).WithMany().Map(o => o.MapKey("ProductId"));
        }
    }
}