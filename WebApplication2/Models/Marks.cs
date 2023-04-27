using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Marks
    {
        [Key]
        public int StudentId { get; set; }

        [Key]
        public int CourseId { get; set; }

        [Key]
        public int SubjectId { get; set; }

        public float PreboardMarks { get; set; }
        public float AssignmentMarks { get; set; }
    }
}
