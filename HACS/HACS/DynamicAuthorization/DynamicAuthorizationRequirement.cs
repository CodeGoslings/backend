using Microsoft.AspNetCore.Authorization;

namespace HACS.DynamicAuthorization
{
    public class DynamicAuthorizationRequirement : IAuthorizationRequirement
    {
        public string EndpointName { get; }
        public DynamicAuthorizationRequirement(string endpointName)
            => EndpointName = endpointName;
    }
}
