namespace CmsProject.Models
{
    public class CustomerCreateDto
    {
        public int CustId { get; set; }                    // your PK is NOT identity
        public string? CustName { get; set; }
        public string? CustUserName { get; set; }
        public string? Password { get; set; }              // plain from client
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Email { get; set; }
        public string? MobileNo { get; set; }
    }

    public class CustomerAuthDto
    {
        public string? CustUserName { get; set; }
        public string? Password { get; set; }
    }
}
