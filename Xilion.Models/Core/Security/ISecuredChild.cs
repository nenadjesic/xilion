namespace Xilion.Models.Core.Security
{
    public interface ISecuredChild : ISecured
    {
        ISecured Parent { get; }
    }
}