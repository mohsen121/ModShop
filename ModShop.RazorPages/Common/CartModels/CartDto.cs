﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModShop.RazorPages.Common.CartModels
{
    public class CartDto
    {
        public Guid ProductId { get; set; }
        public int Count { get; set; }
    }
}
