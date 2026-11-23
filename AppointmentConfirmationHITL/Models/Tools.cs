using static Google.Protobuf.WireFormat;

namespace AppointmentConfirmationHITL.Models
{
    public static class Tools
    {
        public static DateTime CurrentDateAndTime(TimeType type)
        {
            return type switch
            {
                TimeType.Local => DateTime.Now,
                TimeType.Utc => DateTime.UtcNow,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };         
    }
        public static string CurrentTimezone()
        {
            return TimeZoneInfo.Local.DisplayName;
        }

        public enum TimeType
        {
            Local,
            Utc
        }

    }
}
