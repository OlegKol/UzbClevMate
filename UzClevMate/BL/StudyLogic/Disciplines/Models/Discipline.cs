using System.ComponentModel.DataAnnotations;

namespace UzClevMate.BL.StudyLogic.Disciplines.Models
{
    public enum Discipline
    {
        [Display(Name = "Mathematics")]
        math = 1,

        [Display(Name = "Physics")]
        physics = 2
    }
}