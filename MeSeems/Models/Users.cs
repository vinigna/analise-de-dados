using System;

namespace MeSeems.Models
{
    public class Users
    {
        public Users(){}
        public Users(string appUserId, string creationDate, string region, string gender, string socialClass, string birth)
        {
            this.AppUserId = string.IsNullOrEmpty(appUserId) ? 0 : Convert.ToInt64(appUserId);
            this.CreationDate = string.IsNullOrEmpty(creationDate) ? DateTime.MinValue : Convert.ToDateTime(creationDate);
            this.Region = region;
            this.Gender = gender;
            this.SocialClass = socialClass;
            this.Birth = string.IsNullOrEmpty(birth) ? DateTime.MinValue : Convert.ToDateTime(birth); 
        }

        public long AppUserId { get; set; }
        public DateTime CreationDate { get; set; }
        public string Region { get; set; }
        public string Gender { get; set; }
        public string SocialClass { get; set; }
        public DateTime? Birth { get; set; }
    }
}