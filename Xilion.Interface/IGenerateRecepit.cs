using Xilion.ViewModels;

namespace Xilion.Interface
{
    public interface IGenerateRecepit
    {
        GenerateRecepitViewModel Generate(int paymentId);
    }
}