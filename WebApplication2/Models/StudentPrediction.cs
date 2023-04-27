using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class StudentPrediction
    {
        [Key]
        [Column(Order = 0)]
        public int StudentId { get; set; }

        [Key]
        [Column(Order = 1)]
        public int CourseId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int SubjectId { get; set; }

        public bool Success { get; set; }
    }

}
