using System.Runtime.Serialization;

namespace Example.Shared.Authentication
{
    [DataContract]
    public class AuthIdentity
    {
        [DataMember(Order = 1)]
        public required bool IsAuthenticated { get; set; }

        [DataMember(Order = 2)]
        public string? Username { get; set; }
    }
}
