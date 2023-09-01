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
                new Student(){ FirstMidName="Kristjan Georg",LastName="Kessel",EnrollmentDate=DateTime.Now},
                new Student(){ FirstMidName="Kenneth-Marcus",LastName="Aljas",EnrollmentDate=DateTime.Now},
                new Student(){ FirstMidName="Karl-Umberto",LastName="Kats",EnrollmentDate=DateTime.Now},
                new Student(){ FirstMidName="Lebron",LastName="James",EnrollmentDate=DateTime.Now},
                new Student(){ FirstMidName="Tyrion",LastName="Lannister",EnrollmentDate=DateTime.Now},
                new Student(){ FirstMidName="Jeffrey",LastName="Epstein",EnrollmentDate=DateTime.Now}
            };

            context.Students.AddRange(students);

            foreach(Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();
        }
    }
}
