namespace Xilion.Models.Messages.Domain
{
    public interface IAttachmentProvider
    {
        /// <summary>
        /// Get url for attachment regarding provider implementation.
        /// </summary>
        /// <param name="attachment"></param>
        /// <returns></returns>
        string GetUrl(Attachment attachment);
    }
}