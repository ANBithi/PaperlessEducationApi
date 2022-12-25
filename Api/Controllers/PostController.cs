using Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Api.CustomValues;
using Api.Models;
using Api.Repositories;
using Api.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{

    public class AttachmentVM
    {
        public string FileFormat { get; set; }
        public int FileSize { get; set; }
        public string Url { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }

    }
    public class PostViewerModel
    {
        public string CreatedBy { get; set; }
        public string CreatorName { get; set; }
        public string PostDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Attachment> Attachments { get; set; }
    }

    public class PostResponse
    {
        public bool Response { get; set; }
        public List<PostViewerModel> Data { get; set; }
    }

    public class AddPostRequest
    {
        public string BelongsTo { get; set; }
        public string CreatedBy { get; set; }
        public int CreatorType { get; set; }
        public string PostDescription { get; set; }
        public List<AttachmentVM> Attachments { get; set; }
    }
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly INotificationService _notificationService;
        public PostController(IPostRepository postRepository,
            IUserRepository userRepository, INotificationService notificationService)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _notificationService = notificationService;
        }
        [HttpPost("add")]
        public async Task<ActionResult<bool>> Add(AddPostRequest request)
        {
            var attachments = new List<Attachment>();
            if(request.Attachments != null)
            {
                if (request.Attachments.Count > 0)
                {
                    foreach (AttachmentVM attach in request.Attachments)
                    {
                        var attachment = new Attachment();
                        switch ((AttachmentType)attach.Type)
                        {
                            case AttachmentType.Link:
                                attachment.Type = AttachmentType.Link;
                                attachment.Metadata = new LinkMetadata
                                {
                                    Url = attach.Url,
                                    Name = attach.Name
                                };
                                attachments.Add(attachment);
                                break;
                            case AttachmentType.Image:
                                attachment.Type = AttachmentType.Image;
                                attachment.Metadata = new ImageMetadata
                                {
                                    FileFormat = attach.FileFormat,
                                    FileSize = attach.FileSize,
                                    Name = attach.Name,
                                    Url = attach.Url,
                                };
                                attachments.Add(attachment);
                                break;
                            case AttachmentType.Pdf:
                                attachment.Type = AttachmentType.Pdf;
                                attachment.Metadata = new PdfMetadata
                                {
                                    FileSize = attach.FileSize,
                                    Name = attach.Name,
                                    Url = attach.Url,
                                };
                                attachments.Add(attachment);
                                break;
                            case AttachmentType.Video:
                                attachment.Type = AttachmentType.Video;
                                attachment.Metadata = new VideoMetadata
                                {
                                    FileFormat = attach.FileFormat,
                                    FileSize = attach.FileSize,
                                    Name = attach.Name,
                                    Url = attach.Url,
                                };
                                attachments.Add(attachment);
                                break;
                            default:
                                attachment.Type = AttachmentType.Link;
                                attachment.Metadata = new Metadata
                                {
                                    Url = attach.Url,
                                    Name = attach.Name
                                };
                                attachments.Add(attachment);
                                break;
                        }

                    }
                }
            }
            
            try
            {
                var post = new Post
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    BelongsTo = request.BelongsTo,
                    CreatedBy = request.CreatedBy,
                    CreatorType = request.CreatorType,
                    PostDescription = request.PostDescription,
                    Attachments = attachments
                };
                post.CreatedAt = DateTime.Now;
                _postRepository.Add(post);
                var notify = new NotifyRequest
                {
                    Target = $"post-{request.BelongsTo}",
                    Type = "post",
                    DataId = post.Id,
                   
                };
                _notificationService.CreateNotification(notify);
                await _postRepository.Commit();
                var notifcationHelper = new NotificationHelper();
                notifcationHelper.Notify(notify);
                return true;
            }
            catch
            {
                return false;
            }
        }


        [HttpGet("getAll")]
        public async Task<ActionResult<PostResponse>> GetAll(string belongsTo)
        {
            var postResponse = new PostResponse
            {
                Response = false,
                Data = new List<PostViewerModel>()
            };
            try
            {
                var posts = await _postRepository.GetAllAsync(x => x.BelongsTo == belongsTo);
                foreach(Post p in posts)
                {
                    var user = await _userRepository.GetById(p.CreatedBy);
                    var postModel = new PostViewerModel
                    {
                        CreatedAt = p.CreatedAt,
                        CreatorName = $"{user.FirstName} { user.LastName}",
                        PostDescription = p.PostDescription,
                        Attachments = p.Attachments
                    };
                    postResponse.Data.Insert(0, postModel);
                }
                postResponse.Response = true;
                return postResponse;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return postResponse;
            }

        }


        [HttpGet("getSingle")]
        public async Task<ActionResult<PostViewerModel>> GetSingle(string postId)
        {
            var postView = new PostViewerModel();
            try
            {
                var post = await _postRepository.GetSingle(x => x.Id == postId);
                var user = await _userRepository.GetById(post.CreatedBy);
                postView.CreatedAt = post.CreatedAt;
                postView.CreatedBy = post.CreatedBy;
                postView.CreatorName = $"{user.FirstName} { user.LastName}";
                postView.PostDescription = post.PostDescription;
                postView.Attachments = post.Attachments;

                return postView;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return postView;
            }

        }
    }
}
