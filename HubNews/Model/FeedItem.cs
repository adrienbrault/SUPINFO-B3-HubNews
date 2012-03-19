using System;
using System.Security.Cryptography;
using System.Text;

namespace HubNews.Model
{
    public class FeedItem
    {
        // Create SHA1 hash from Url
        public string Id
        {
            get
            {
                var sHA1Managed = new SHA1Managed();
                Byte[] result = sHA1Managed.ComputeHash(new UTF8Encoding().GetBytes(Url ?? ""));
                var hashedString = new StringBuilder();
                foreach (Byte outputByte in result)
                    hashedString.Append(outputByte.ToString("x2").ToUpper());

                return hashedString.ToString(); 
            }
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}
