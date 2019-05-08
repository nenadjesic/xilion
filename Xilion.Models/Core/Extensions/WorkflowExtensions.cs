using System;
using Xilion.Models.Core.Domain;

namespace Xilion.Models.Core.Extensions
{
    public static class WorkflowExtensions
    {
        public static bool IsExpired(this IHaveWorkflow workflow)
        {
            return workflow.ExpiresOn != null && workflow.ExpiresOn < DateTime.Now;
        }

        public static bool IsLive(this IHaveWorkflow workflow)
        {
            return workflow.Status == WorkflowStatus.Live && workflow.PublishedOn < DateTime.Now;
        }

        public static void Publish(this IHaveWorkflow workflow)
        {
            workflow.PublishedOn = DateTime.Now;
            workflow.Status = WorkflowStatus.Live;
        }
    }
}