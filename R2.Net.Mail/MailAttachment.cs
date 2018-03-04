namespace R2.Net.Mail
{
    public class MailAttachment
    {
        private const string _TYPE_PDF = "application/pdf";
        private const string _DISPOSITION_ATTACHMENT = "attachment";

        public MailAttachment(
            string attachmentName,
            string attachmentContent,
            string attachmentType = _TYPE_PDF,
            string attachmentDisposition = _DISPOSITION_ATTACHMENT)
        {
            AttachmentName = attachmentName;
            AttachmentContent = attachmentContent;
            AttachmentType = attachmentType;
            AttachmentDisposition = attachmentDisposition;
        }

        public string AttachmentName { get; }

        /// <summary>
        /// AttachmentContent must be Base64-encoded.
        /// </summary>
        public string AttachmentContent { get; }

        public string AttachmentType { get; }

        public string AttachmentDisposition { get; }
    }
}