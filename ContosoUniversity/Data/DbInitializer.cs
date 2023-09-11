using ContosoUniversity.Models;

namespace ContosoUniversity.Data
{
    public class DbInitializer
    {
        public static void Initialize(SchoolContext context)
        {
            context.Database.EnsureCreated();

            if(context.Students.Any())
            {
                return;
            }

            var students = new Student[]
            {
                new Student(){Id=1, FirstMidName="Kenneth-Marcus",LastName="Aljas",EnrollmentDate=DateTime.Now},
                new Student(){Id=2, FirstMidName="Karl-Umberto",LastName="Kats",EnrollmentDate=DateTime.Now},
                new Student(){Id=3, FirstMidName="Kristjan Georg",LastName="Kessel",EnrollmentDate=DateTime.Now},
                new Student(){Id=4, FirstMidName="Lebron",LastName="James",EnrollmentDate=DateTime.Now},
                new Student(){Id=5, FirstMidName="Tyrion",LastName="Lannister",EnrollmentDate=DateTime.Now},
                new Student(){Id=6, FirstMidName="Jeffrey",LastName="Epstein",EnrollmentDate=DateTime.Now},
                new Student(){Id=7, FirstMidName="Jong",LastName="Zena",EnrollmentDate=DateTime.Now}
            };

            context.Students.AddRange(students);

            foreach(Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            var instructors = new Instructor[]
            {
                new Instructor {FirstMidName = "Magnus", LastName = "Carlsen" }
            };

            var courses = new Course[]
            {
                new Course() {CourseID=1050,Title="Gotham",Credits=160},
                new Course() {CourseID=1060,Title="CCP",Credits=160},
                new Course() {CourseID=6900,Title="GetTheBag",Credits=160},
                new Course() {CourseID=1420,Title="Lunch",Credits=160},
                new Course() {CourseID=6666,Title="Jail",Credits=160},
                new Course() {CourseID=6323,Title="6feet+",Credits=150},
                new Course() {CourseID=1234,Title="Island",Credits=160}
            };

            foreach(Course c in courses)
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
                new Enrollment{StudentID=1,CourseID=1050,Grade=Grade.Eeeehmazing},
                new Enrollment{StudentID=1,CourseID=1234,Grade=Grade.Eeeehmazing},
                new Enrollment{StudentID=1,CourseID=6323,Grade=Grade.Eeeehmazing},
                new Enrollment{StudentID=3,CourseID=1420,Grade=Grade.Eeeehmazing},
                new Enrollment{StudentID=3,CourseID=1060,Grade=Grade.Fucked},
                new Enrollment{StudentID=2,CourseID=1420,Grade=Grade.Eeeehmazing},
                new Enrollment{StudentID=4,CourseID=6666,Grade=Grade.Eeeehmazing},
                new Enrollment{StudentID=5,CourseID=6323,Grade=Grade.Fucked},
                new Enrollment{StudentID=6,CourseID=1234,Grade=Grade.Eeeehmazing},
                new Enrollment{StudentID=7,CourseID=1060,Grade=Grade.Cool},
                new Enrollment{StudentID=7,CourseID=6900,Grade=Grade.Boom},
            };
            foreach (Enrollment e in enrollments)
            {
                context.Enrollments.Add(e);
            }
            context.SaveChanges();
        }
    }
}
