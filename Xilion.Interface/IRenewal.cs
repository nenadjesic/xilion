using System;
using Xilion.ViewModels;

namespace Xilion.Interface
{
    public interface IRenewal
    {
        RenewalViewModel GetMemberNo(string memberNo, int Usersid);
        bool CheckRenewalPaymentExists(DateTime newdate, long memberId);
    }
}