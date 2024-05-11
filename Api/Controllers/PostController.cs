using Api.Commons;
using Api.CustomValues;
using Api.IServices;
using Api.Models;
using Api.Models.Post;
using Api.Repositories;
using Api.Requests.NotificationRequests;
using Api.Requests.PostRequests;
using Api.Responses.PostResponses;
using Api.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        private readonly IRequestUserService _requestUserService;
        private readonly IDateTime _dateTime;
        public PostController(IPostRepository postRepository,
            IUserRepository userRepository, INotificationService notificationService,
            IReactionRepository reactionRepository, IMapper mapper, ICommentRepository commentRepository, IRequestUserService requestUserService, IDateTime dateTime)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _notificationService = notificationService;
            _reactionRepository = reactionRepository;
            _mapper = mapper;
            _commentRepository = commentRepository;
            _requestUserService = requestUserService;
            _dateTime = dateTime;
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
                var user = await _requestUserService.GetUser();
                var post = new Post
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    BelongsTo = request.BelongsTo,
                    CreatorType = request.CreatorType,
                    PostDescription = request.PostDescription,
                    Attachments = attachments
                };
                post.UpdateCreatedByFields(user, _dateTime.NowUTC);
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
            var user = await _requestUserService.GetUser();
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
                    var postModel = new PostViewerModel
                    {
                        Id = p.Id,
                        CreatedAt = p.CreatedAt,
                        CreatorName =p.CreatedBy,
                        PostDescription = p.PostDescription,
                        Attachments = p.Attachments,
                        CreatedBy = p.CreatedById,
                        
                    };
                    postResponse.Data.Insert(0, postModel);
                }
                postResponse.Response = true;
                return postResponse;
            }
            catch (Exception e)
            {
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
                return postView;
            }

        }

        [HttpPost("addReaction")]
        public async Task<ActionResult<bool>> AddReaction(AddReactionRequest request)
        {
           try
            {
                var user = await _requestUserService.GetUser();
                var found = await _reactionRepository.GetSingle(x => x.CreatedById == user.Id && x.ParentId == request.ParentId);
                if (found != null)
                {
                    found.IconId = request.IconId;
                    found.UpdateModifiedByFields(user, _dateTime.NowUTC);
                    _reactionRepository.Update(found);
                }
                else
                {
                    var reaction = new Reaction
                    {
                        IconId = request.IconId,
                        ParentId = request.ParentId
                    };
                    reaction.UpdateCreatedByFields(user, _dateTime.NowUTC);
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
            var user = await _requestUserService.GetUser();
            try
            {
                var comment = new Comment
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Content = request.Content,
                    ParentId = request.ParentId
                };
                comment.UpdateCreatedByFields(user, _dateTime.NowUTC);
                _commentRepository.Add(comment);

                var notify = new NotificationRequest
                {
                    Target = $"comment-{request.ParentId}",
                    Type = "comment",
                    DataId = comment.Id,

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
                var comments = await _commentRepository.GetAllAsync(x => x.ParentId == parentId);
                var commnetsViewModels = _mapper.Map<List<CommentViewModel>>(comments);
                return commnetsViewModels;
            }
            catch
            {
                return NotFound("Comments not found");
            }

        }

        [HttpGet("getSingleComment")]
        public async Task<ActionResult<CommentViewModel>> GetSingleComment(string commentId)
        {
            var commentView = new CommentViewModel();
            try
            {
                var comment = await _commentRepository.GetSingle(x => x.Id == commentId);
                commentView = _mapper.Map<CommentViewModel>(comment);
                return commentView;
            }
            catch (Exception e)
            {
                return commentView;
            }

        }

    }
}
