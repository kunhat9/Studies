namespace WebAdmin.Models
{
    public class ClassModel
    {
        public int ScheduleId { get; set; }
        public int SubjectId { get; set; }
        public int BoxId { get; set; }
        public string DateStart { get; set; }
        public string Price { get; set; }

        public string DateEnd { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public string Status { get; set; }
        public string UserId { get; set; }
        public string UserNote { get; set; }

    }
}