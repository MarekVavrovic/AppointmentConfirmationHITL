using System.ComponentModel;

namespace AppointmentConfirmationHITL.Models
{
    public class BookingService
    {
        //Available dates (NEXT 30 days)       
        private static List<DateTime> _available = Enumerable.Range(1, 30)
            .Select(i => DateTime.Now.AddDays(i).Date)
            .ToList();

        // The Booked appointments      
        private static List<Appointment> _booked = new List<Appointment>(); //

        //The Description attribute provides a human-readable description of what each method does
        //This helps the AI agent understand the purpose of each method when it is invoked.
        //This is particularly useful in scenarios where the AI agent needs to choose which method to call based on user requests.
        //The Description can also be added to the parameters of each method to further clarify their purpose .

        [Description("Determines the next available date for a booking")]
        public static DateTime GetNextAvailableDate(DateTime startDate)
        {
            return _available.FirstOrDefault(d => d > startDate);
        }

        [Description("Allows the user to make a booking.")]
        public static string BookAppointment(DateTime date, string patientName)
        {            
            date = date.Date;//no time componen

            // Check if the date is available for booking
            if (_available.Contains(date))
            {
                _available.Remove(date);
                _booked.Add(new Appointment { Date = date, PatientName = patientName });                // new appointment to the booked list
                return "Appointment booked for " + date.ToShortDateString() + " for " + patientName;    
            }
            else
            {
                return "The selected date is not available";
            }
        }

        [Description("Cancels a booking.")]
        public static string CancelAppointment(DateTime date, string patientName)
        {            
            var appointment = _booked.FirstOrDefault(a => a.Date == date && a.PatientName == patientName);         

            if (appointment != null)
            {
                _booked.Remove(appointment);            
                _available.Add(date);                   

                return "Appointment on " + date.ToShortDateString() + " for " + patientName + " cancelled";
            }
            else
            {
                return "The selected appointment is not booked";
            }
        }

        [Description("Lists all booked appointments.")]
        public static string ListBookedAppointments()
        {
            if (_booked.Count == 0)
                return "No appointments booked.";
            
            //appointment with its date and patient name.
            return string.Join("<br/>", _booked.Select(a => "Date: " + a.Date.ToShortDateString() + ", Name: " + a.PatientName));
        }

        [Description("Lists all available dates for booking.")]
        public static List<DateTime> ListAvailableDates()
        {
            return _available;
        }


        [Description("Finds all appointments for a given patient name.")]
        public static List<Appointment> FindAppointmentsByName(string patientName)
        {
            return _booked.Where(a => a.PatientName.Equals(patientName, StringComparison.OrdinalIgnoreCase)).ToList();
            
        }

    }
}
