using System;

namespace LoginAndRegisterFinal.Models
{
    public abstract class BaseEntity
    {
        public DateTime created_at { get; set;}
        public DateTime updated_at { get; set;}
        public BaseEntity()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }

        public string TimePast()
        {
            TimeSpan timeSinceMes = DateTime.Now - created_at;
            double minutesSince = Math.Floor(timeSinceMes.TotalMinutes);
            if (minutesSince < 1)
            {
                return "now";
            }
            else if (minutesSince < 2)
            {
                return minutesSince + " minute ago";
            }
            else if (minutesSince < 60)
            {
                return minutesSince + " minutes ago";
            }
            else if (minutesSince/60 < 2)
            {
                return Math.Floor(minutesSince/60) + " hour ago";
            }
            else if (minutesSince/60 < 24)
            {
                return Math.Floor(minutesSince/60) + " hours ago";
            }
            else if(minutesSince/1440 < 24)
            {
                return Math.Floor(minutesSince/1440) + " day ago";
            }
            else if(minutesSince/1440 < 48)
            {
                return Math.Floor(minutesSince/1440) + " days ago";
            }
            else 
            {
                return "on " + created_at.ToString("MMMM dd, yyyy");
            }
        }
    }
}