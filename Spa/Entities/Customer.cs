﻿using Spa.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Spa.Data.Entities
{
    public class Customer: AppUser
    {
        //public Customer()
        //{
        //    CustomerGroup = new CustomerGroup();
        //    ApplicationUser = new ApplicationUser();
        //}
        //public int CustomerId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool SubscribedNews { get; set; }
        public CustomerGroup CustomerGroup { get; set; }
        //public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Ratio> Ratios { get; set; }
    }
}