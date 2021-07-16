using System;
using System.Collections.Generic;

namespace MeSeems.Models
{
    public class Invitations
    {
        public Invitations(string senderAppUserId, string recipientAppUserId, string status)
        {
            this.SenderAppUserId = string.IsNullOrEmpty(senderAppUserId) ? 0 : Convert.ToInt64(senderAppUserId);
            this.RecipientAppUserId = string.IsNullOrEmpty(recipientAppUserId) ? 0 : Convert.ToInt64(recipientAppUserId);
            this.Status = string.IsNullOrEmpty(status) ? false : Convert.ToBoolean(Convert.ToInt32(status));
        }
        public long SenderAppUserId { get; set; }
        public long RecipientAppUserId { get; set; }
        public bool Status { get; set; }
        public Users RecipientData { get; set; }
    }
}