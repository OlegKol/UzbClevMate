using Microsoft.AspNet.Identity;
using System;
using System.Web.Mvc;
using UzClevMate._Common.Extensions;
using UzClevMate.BL.Emails.EmailSending.Managers;
using UzClevMate.BL.UzClevMateUsers.Students.Managers;
using UzClevMate.BL.UzClevMateUsers.Students.Models;
using UzClevMate.BL.UzClevMateUsers.Teachers.Managers;
using UzClevMate.BL.UzClevMateUsers.Teachers.Models;
using UzClevMate.MvcLogic._Common.Controllers;

namespace UzClevMate.MvcLogic.Components.TaskComplaintLogic.Controllers
{
    public class TaskComplaintController : _BaseController
    {
        //[HttpPost]
        //public ActionResult SendTaskComplaint(int taskId, string text, string firstParam, string secondParam)
        //{
        //
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var userId = User.Identity.GetUserId();
        //        var teacher = TeacherGetManager.GetWithSettingsByUserId(userId);
        //        var student = StudentGetManager.GetByUserId(userId);
        //
        //        StartSupportThread(taskId, text, firstParam, secondParam, userId, teacher, student);
        //        SendEmailToSupportService(taskId, text, firstParam, secondParam, teacher, student);
        //    }
        //    else
        //    {
        //        SendEmailFromAnonymousUser(taskId, text, firstParam, secondParam);
        //        StartSupportThread(taskId, text, firstParam, secondParam);
        //    }
        //
        //    return Content("ok");
        //}
        //
        //private void SendEmailFromAnonymousUser(int taskId, string text, string firstParam, string secondParam)
        //{
        //    var task = TaskGetManager.GetTask(taskId);
        //
        //    var result = new System.Text.StringBuilder();
        //    result.AppendLine($"Идентификатор задачи: {taskId}.");
        //    
        //    if (task.IsConstructibleTask && firstParam.HasValue())
        //    {
        //        result.AppendLine($"Значение первого параметра: {firstParam}.");
        //    }
        //    if (task.IsConstructibleTask && task.HasTwoParameters && secondParam.HasValue())
        //    {
        //        result.AppendLine($"Значение второго параметра: {secondParam}.");
        //    }
        //    if (task.IsChecked)
        //    {
        //        result.AppendLine($"Задача проверялась ранее.");
        //    }
        //    result.AppendLine($"Текст жалобы: {text}.");
        //
        //    EmailManager.SendCommonCustomerServiceMail("Найдена ошибка в задаче (анонимный пользователь)", result.ToString());
        //}
        //
        //private static void SendEmailToSupportService(int taskId, string text, string firstParam, string secondParam, Teacher teacher, Student student)
        //{
        //    var isTeacher = teacher != null;
        //    var name = isTeacher ? teacher.Name : student.Name;
        //    var email = isTeacher ? teacher.Email : student.Email;
        //    var task = TaskGetManager.GetTask(taskId);
        //
        //    //send mail
        //    var result = new System.Text.StringBuilder();
        //
        //    result.AppendLine($"Идентификатор задачи: {taskId}.");
        //    if (task.IsConstructibleTask && firstParam.HasValue())
        //    {
        //        result.AppendLine($"Значение первого параметра: {firstParam}.");
        //    }
        //    if (task.IsConstructibleTask && task.HasTwoParameters && secondParam.HasValue())
        //    {
        //        result.AppendLine($"Значение второго параметра: {secondParam}.");
        //    }
        //    result.AppendLine($"{(isTeacher ? "От учителя." : "От ученика.")}");
        //
        //    if (task.IsChecked)
        //    {
        //        result.AppendLine($"Задача проверялась ранее.");
        //    }
        //    result.AppendLine($"Имя: {name}. Email: {email}. Текст жалобы: {text}");
        //
        //    EmailManager.SendCommonCustomerServiceMail("Найдена ошибка в задаче", result.ToString());
        //}
        //
        //private static void StartSupportThread(int taskId, string text, string firstParam, string secondParam, string userId = null, Teacher teacher = null, Student student = null)
        //{
        //    var paramString = string.Empty;
        //    if (firstParam.HasElements())
        //    {
        //        paramString = $", первый параметр: {firstParam}";
        //    }
        //    if (secondParam.HasElements())
        //    {
        //        paramString = paramString + $", второй параметр: {secondParam}";
        //    }
        //    var thread = SupportMessageEditManager.StartSupportThread($"Идентификатор задачи:{taskId}{paramString}", userId, teacher?.Id, student?.Id, $"Ошибка в задаче {taskId}, {DateTime.Now.ToString(_Definitions.DefaultDateTimeFormat)}");
        //    SupportMessageEditManager.AddQuestion(text, thread.Id);
        //}
    }
}