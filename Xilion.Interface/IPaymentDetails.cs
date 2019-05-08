using System.Linq;
using Xilion.Models;
using Xilion.ViewModels;

namespace Xilion.Interface
{
    public interface IPaymentDetails
    {
        IQueryable<PaymentDetailsViewModel> GetAll(QueryParameters queryParameters, int UsersId);
        int Count(int UsersId);
        bool RenewalPayment(RenewalViewModel renewalViewModel);
    }
}