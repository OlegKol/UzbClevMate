using System.Configuration;

namespace UzClevMate._Common.Extensions
{
    public class _Definitions
    {

        public static string DEFAULT_MAIL = ConfigurationManager.AppSettings["DefaultEmail"] ?? "o.o.kolesnikov@gmail.com";

        public static string CurrentSite = ConfigurationManager.AppSettings["DefaultSite"]; //?? "https://clevmate.com";

        public const string TaskImageFolder = "Images/Tasks";

        public const string ComplexAnswerFolder = "Images/ComplexAnswers";

        public const string MessageFolder = "Images/Messages";

        public const string CustomImagesFolder = "Images/Custom";

        public const string SummaryImagesFolder = "Images/Summaries/Items";

        public const string SummaryFolder = "Images/Summaries";

        public const string TheoryItems = "Images/TheoryItems";

        public const string OwnTasksFolder = "Images/OwnTasks";

        public const string RewardFolder = "Images/Rewards";

        public const string DefaultDateFormat = "dd.MM.yyyy";

        public const string DefaultTimeFormat = "HH:mm";

        public const string DefaultDateTimeFormat = "dd.MM.yyyy HH:mm";

        public const string ISO8601DateFormat = "yyyy-MM-dd";

        public const string ISO8601FullDateFormat = "O";

        public static string TeacherRole = "teacher";

        public static string StudentRole = "student";

        public static string DefaultCulture = ConfigurationManager.AppSettings["DefaultCulture"] ?? "uz";
    }
}