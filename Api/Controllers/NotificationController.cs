using Api.Models;
using Api.Repositories;
using Api.Responses.Notification;
using Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController  : ControllerBase
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IPostRepository _postRepostiry;
        private readonly IUserRepository _userRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IUserInteractionRepository _userInteractionRepository;

        public NotificationController(INotificationRepository notificationRepository, 
            IPostRepository postRepostiry, IUserRepository userRepository, 
            ICourseRepository courseRepository, ISectionRepository sectionRepository,
            IUserInteractionRepository userInteractionRepository )
        {
            _notificationRepository = notificationRepository;
            _postRepostiry = postRepostiry;
            _userRepository = userRepository;
            _courseRepository = courseRepository;
            _sectionRepository = sectionRepository;
            _userInteractionRepository = userInteractionRepository;
        }

        [HttpGet("getNotifications")]
        public async Task<ActionResult<AllNotificationResponse>> GetNotifications(string currentUser, string type)
        {
            var response = new AllNotificationResponse
            {
                OldNotifications = new List<NotificationViewModel>(),
                NewNotifications = new List<NotificationViewModel>(),
            };
            var notifications = await _notificationRepository.GetAllAsync(x=> x.Type == type);
            var userInteraction = await _userInteractionRepository.GetSingle(x => x.CreatedById == currentUser && x.Type == InteractionType.NotificationRead);
            if (type == "post")
            {
                foreach(Notification n in notifications)
                {
                    var post = await _postRepostiry.GetById(n.DataId);
                    var user = await _userRepository.GetById(post.CreatedById);
                    var section = await _sectionRepository.GetById(post.BelongsTo);
                    var course = await _courseRepository.GetById(section.CourseId);
                    var view = new NotificationViewModel
                    {
                        Id = n.Id,
                        CreatorName = $"{user.FirstName} { user.LastName}",
                        CourseName = course.Name,
                        PostId = post.Id,
                        Section = section.SectionNumber,
                        SectionId = section.Id
                    };
                    if (userInteraction != null)
                    {
                        if (n.CreatedAt > userInteraction.ModifiedAt)
                        {
                            response.NewNotifications.Add(view);
                        }
                        else
                        {
                            response.OldNotifications.Add(view);
                        }
                    }
                    else
                    {
                        response.NewNotifications.Add(view);
                    }
                   
                }
                response.UnreadCount = response.NewNotifications.Count;
            }
           
            return response;
        }
    }
}
