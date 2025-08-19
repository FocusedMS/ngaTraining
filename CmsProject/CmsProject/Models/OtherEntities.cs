namespace CmsProject.Models
{
    public class Menu
    {
        public int MenuId { get; set; }
        public string? ItemName { get; set; }
        public string? ItemType { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public string? Rating { get; set; }
    }

    public class Order
    {
        public int OrderId { get; set; }
        public int? CustId { get; set; }
        public int? MenuId { get; set; }
        public int? VendorId { get; set; }
        public int? QtyOrd { get; set; }
        public decimal? BillAmount { get; set; }
        public string? OrderStatus { get; set; }
        public string? OrderComments { get; set; }
    }

    public class Vendor
    {
        public int VendorId { get; set; }
        public string? VendorName { get; set; }
        public string? VendorUserName { get; set; }
        public string? VendorPassword { get; set; }
        public string? VendorEmail { get; set; }
        public string? VendorMobile { get; set; }
    }

    public class Wallet
    {
        public int WalletId { get; set; }
        public int? CustId { get; set; }
        public string? WalletType { get; set; }
        public decimal? WalletAmount { get; set; }
    }
}
