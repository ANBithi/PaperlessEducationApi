using Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.ViewModels;

namespace Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController  : ControllerBase
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IPostRepository _postRepostiry;
        private readonly IUserRepository _userRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly ICourseRepository _courseRepository;

        public NotificationController(INotificationRepository notificationRepository, IPostRepository postRepostiry, IUserRepository userRepository, ICourseRepository courseRepository, ISectionRepository sectionRepository)
        {
            _notificationRepository = notificationRepository;
            _postRepostiry = postRepostiry;
            _userRepository = userRepository;
            _courseRepository = courseRepository;
            _sectionRepository = sectionRepository;
        }

        [HttpGet("getNotifications")]
        public async Task<ActionResult<List<NotificationViewModel>>> GetNotifications(string currentUser, string type)
        {
            var notificationViews = new List<NotificationViewModel>();
            var notifications = await _notificationRepository.GetAllAsync(x=>x.Id != currentUser && x.Type == type);
            if(type == "post")
            {
                foreach(Notification n in notifications)
                {
                    var post = await _postRepostiry.GetById(n.DataId);
                    var user = await _userRepository.GetById(post.CreatedBy);
                    var section = await _sectionRepository.GetById(post.BelongsTo);
                    var course = await _courseRepository.GetById(section.BelongsTo);
                    var view = new NotificationViewModel
                    {
                        Id = n.Id,
                        CreatorName = $"{user.FirstName} { user.LastName}",
                        CourseName = course.Name,
                        PostId = post.Id,
                        Section = section.SectionNumber,
                        SectionId = section.Id
                };
                    notificationViews.Add(view);  
                }
            }
           
            return notificationViews;
        }
    }
}
