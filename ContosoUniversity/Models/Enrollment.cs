namespace ContosoUniversity.Models
{
    public enum Grade
    {
        Eeeehmazing, Boom, Cool, Dam, Fucked
    }
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Grade? Grade { get; set; }
        public Student Student { get; set; }
    }
}
