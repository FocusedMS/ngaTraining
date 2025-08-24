namespace SecureShop.Web.ViewModels
{
    using SecureShop.Web.Models;
    using System.Collections.Generic;

    public class ProductDetailsViewModel
    {
        public Product Product { get; set; } = new();
        public IEnumerable<ProductReview> Reviews { get; set; } = new List<ProductReview>();
        public ReviewInputModel Review { get; set; } = new();
    }
}
