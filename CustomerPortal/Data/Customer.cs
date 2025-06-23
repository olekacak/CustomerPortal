namespace CustomerPortal.Data
{
    public class Customer
    {
        public int? customerId { get; set; }
        public string? name { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public string? phone { get; set; }
        public string? address { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public string? country { get; set; }
        public DateTime? dob { get; set; }
        public string? gender { get; set; }
        public string? occupation { get; set; }
        public int? role { get; set; }
        public int? is_active { get; set; }
        public int? is_delete { get; set; }

    }
}
