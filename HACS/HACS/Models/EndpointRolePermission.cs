namespace HACS.Models
{
    public class EndpointRolePermission
    {
        public int Id { get; set; }
        public string Endpoint { get; set; } = default!;
        public string Role { get; set; } = default!;
    }
}
