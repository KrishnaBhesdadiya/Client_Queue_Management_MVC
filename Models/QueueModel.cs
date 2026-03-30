using Frontend_Exam.Models;

namespace ClinicQueueFrontend.Models
{
    public class QueueModel
    {
        public int id { get; set; }
        public int tokenNumber { get; set; }
        public Appointment appointment { get; set; }
        public string status { get; set; }
        public string queueDate { get; set; }
    }
}