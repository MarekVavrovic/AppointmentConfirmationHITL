namespace AppointmentConfirmationHITL.Models
{
    
    // Used in BookingService to manage booked appointments
    public class Appointment
    {
        public DateTime Date { get; set; }
        public string PatientName { get; set; } = string.Empty;
    }
}
