namespace WebAdmin.Models
{
    public class ClassModel
    {
        public int ScheduleId { get; set; }
        public int SubjectId { get; set; }
        public int BoxId { get; set; }
        public string DateStart { get; set; }
        public string Price { get; set; }
        public int BoxSubjectId { get; set; }
        public string DateEnd { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public string Status { get; set; }
        public string UserId { get; set; }
        public string UserNote { get; set; }
        public string ScheduleFileId { get; set; }
        public string DayOfWeek { get; set; }
        public string RoomId { get; set; }
        // dot 2 
        public string DayOfWeek2 { get; set; }
        public string TimeStart2 { get; set; }
        public string TimeEnd2 { get; set; }

    }
}