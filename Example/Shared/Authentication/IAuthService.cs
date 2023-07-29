using ProtoBuf.Grpc;
using System.ServiceModel;

namespace Example.Shared.Authentication
{
    [ServiceContract]
    public interface IAuthService
    {
        Task SignIn(string username, CallContext context = default);

        Task SignOut(CallContext context = default);

        Task<AuthIdentity> GetIdentity(CallContext context = default);
    }
}
