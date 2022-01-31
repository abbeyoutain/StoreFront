using StoreFront.DATA.EF; //added for access to the domain models
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; //added for validation/display metadata
using System.Linq;
using System.Web;

namespace StoreFront.UI.MVC.Models
{
    //Shopping Cart Step 1

    public class CartItemViewModel
    {
        [Range(1, int.MaxValue)]
        public int Qty { get; set; }
        public MagicItem Product { get; set; }

        //FQ CTOR
        public CartItemViewModel(int qty, MagicItem product)
        {
            Qty = qty;
            Product = product;
        }
    }
}