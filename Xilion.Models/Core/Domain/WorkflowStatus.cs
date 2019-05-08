using Xilion.Framework;

namespace Xilion.Models.Core.Domain
{
    public class WorkflowStatus : Enumeration
    {
        public static readonly WorkflowStatus Draft = new WorkflowStatus(0, "Draft");
        public static readonly WorkflowStatus Pending = new WorkflowStatus(1, "Pending");
        public static readonly WorkflowStatus Live = new WorkflowStatus(2, "Live");
        public static readonly WorkflowStatus Archived = new WorkflowStatus(3, "Archived");
        public static readonly WorkflowStatus Deleted = new WorkflowStatus(4, "Deleted");
        public static readonly WorkflowStatus Aktivan = new WorkflowStatus(5, "Aktivan");
        public static readonly WorkflowStatus NeAktivan = new WorkflowStatus(6, "NeAktivan");
        public WorkflowStatus(int value, string displayName) : base(value, displayName)
        {
        }
    }
}