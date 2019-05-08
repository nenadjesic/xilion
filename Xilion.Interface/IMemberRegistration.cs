using System.Collections.Generic;
using System.Linq;
using Xilion.Models;
using Xilion.ViewModels;

namespace Xilion.Interface
{
    public interface IMemberRegistration
    {
        int InsertMember(MemberRegistration memberRegistration);
        long CheckNameExitsforUpdate(string memberFName, string memberLName, string memberMName);
        bool CheckNameExits(string memberFName ,string memberLName, string memberMName);
        List<MemberRegistrationGridModel> GetMemberList();
        MemberRegistrationViewModel GetMemberbyId(int memberId);
        bool DeleteMember(long memberId);
        int UpdateMember(MemberRegistration memberRegistration);
        IQueryable<MemberRegistrationGridModel> GetAll(QueryParameters queryParameters, int UsersId);
        int Count(int UsersId);
        List<MemberResponse> GetMemberNoList(string memberNo, int UsersId);
    }
}