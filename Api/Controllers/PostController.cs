using Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Api.CustomValues;
using Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.IServices;
using Api.ViewModels;
using Api.Requests.PostRequests;
using Api.Requests.NotificationRequests;
using Api.Responses.PostResponses;
using Api.Models.Post;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly INotificationService _notificationService;
        private readonly IReactionRepository _reactionRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        public PostController(IPostRepository postRepository,
            IUserRepository userRepository, INotificationService notificationService,
            IReactionRepository reactionRepository, IMapper mapper, ICommentRepository commentRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _notificationService = notificationService;
            _reactionRepository = reactionRepository;
            _mapper = mapper;
            _commentRepository = commentRepository;
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
                var notify = new NotificationRequest
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
                        Id = p.Id,
                        CreatedAt = p.CreatedAt,
                        CreatorName = $"{user.FirstName} { user.LastName}",
                        PostDescription = p.PostDescription,
                        Attachments = p.Attachments,
                        CreatedBy = p.CreatedBy,
                        
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

        [HttpPost("addReaction")]
        public async Task<ActionResult<bool>> AddReaction(AddReactionRequest request)
        {
           try
            {
                var user = await _userRepository.GetById(request.CreatedBy);
                var found = await _reactionRepository.GetSingle(x => x.CreatedById == request.CreatedBy && x.ParentId == request.ParentId);
                if (found != null)
                {
                    found.IconId = request.IconId;
                    found.UpdateModifiedByFields(user, DateTime.Now);
                    _reactionRepository.Update(found);
                }
                else
                {
                    var reaction = new Reaction
                    {
                        IconId = request.IconId,
                        ParentId = request.ParentId
                    };
                    reaction.UpdateCreatedByFields(user, DateTime.Now);
                    _reactionRepository.Add(reaction);

                }
                await _reactionRepository.Commit();

                return true;
            }
            catch
            {
                return false;
            }
            
        }


        [HttpPost("postComment")]
        public async Task<ActionResult<bool>> PostComment(AddCommentRequest request)
        {
            try
            {
                var user = await _userRepository.GetById(request.CreatedById);

                var comment = new Comment
                {
                    Content = request.Content,
                    ParentId = request.ParentId
                };
                comment.UpdateCreatedByFields(user, DateTime.Now);
                _commentRepository.Add(comment);

                await _commentRepository.Commit();

                return true;
            }
            catch
            {
                return false;
            }

        }


        [HttpGet("getAllReactions")]
        public async Task<ActionResult<List<ReactionViewModel>>> GetAllReactions(string parentId)
        {
            try
            {
                var reactions = await _reactionRepository.GetAllAsync(x => x.ParentId == parentId);
                var reactionViewModels = _mapper.Map<List<ReactionViewModel>>(reactions);
                return reactionViewModels;
            }
            catch
            {
                return NotFound("Ractions not found");
            }

        }



        [HttpGet("getAllComments")]
        public async Task<ActionResult<List<CommentViewModel>>> GetAllComments(string parentId)
        {
            try
            {
                var commnets = await _commentRepository.GetAllAsync(x => x.ParentId == parentId);
                var commnetsViewModels = _mapper.Map<List<CommentViewModel>>(commnets);
                return commnetsViewModels;
            }
            catch
            {
                return NotFound("Comments not found");
            }

        }

    }
}
