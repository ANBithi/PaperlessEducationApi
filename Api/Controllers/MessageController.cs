using Api.CustomValues;
using Api.IServices;
using Api.Models;
using Api.Repositories;
using Api.Requests.MessageRequests;
using Api.Requests.NotificationRequests;
using Api.Responses.MessageResponses;
using Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {

        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        private readonly INotificationService _notificationService;
        public MessageController(IMessageRepository messageRepository,
            IUserRepository userRepository, INotificationService notificationService)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
            _notificationService = notificationService;
        }

        [HttpPost("add")]
        public async Task<ActionResult<bool>> Add(AddMessageRequest request)
        {
            var attachments = new List<Attachment>();
            if (request.Attachments != null)
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
                var message = new Message
                {
                    Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                    BelongsTo = request.BelongsTo,
                    CreatedBy = request.CreatedBy,
                    CreatorType = request.CreatorType,
                    MessageBody = request.MessageBody,
                    Attachments = attachments,
                    CreatedAt = DateTime.Now
                };

                _messageRepository.Add(message);
                var notify = new NotificationRequest
                {
                    Target = "chat",
                    Type = "chat",
                    DataId = message.Id,
                };
                _notificationService.CreateNotification(notify);
                await _messageRepository.Commit();
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
        public async Task<ActionResult<MessageResponse>> GetAll(string belongsTo)
        {
            var messageResponse = new MessageResponse
            {
                Response = false,
                Data = new List<MessageViewModel>()
            };
            try
            {
                var messages = await _messageRepository.GetAllAsync(x => x.BelongsTo == belongsTo);
                foreach (Message m in messages)
                {
                    var user = await _userRepository.GetById(m.CreatedBy);
                    var messageModel = new MessageViewModel
                    {
                        CreatedAt = m.CreatedAt,
                        CreatedBy = user.Id,
                        CreatorName = $"{user.FirstName} { user.LastName}",
                        MessageBody = m.MessageBody,
                        Attachments = m.Attachments
                    };
                    messageResponse.Data.Add(messageModel);
                }
                messageResponse.Response = true;
                return messageResponse;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return messageResponse;
            }

        }

        [HttpGet("getSingle")]
        public async Task<ActionResult<MessageViewModel>> GetSingle(string chatId)
        {
            var msgView = new MessageViewModel();
            try
            {
                var chat = await _messageRepository.GetSingle(x => x.Id == chatId);
                    var user = await _userRepository.GetById(chat.CreatedBy);

                msgView.CreatedAt = chat.CreatedAt;
                msgView.CreatedBy = user.Id;
                msgView.CreatorName = $"{user.FirstName} { user.LastName}";
                msgView.MessageBody = chat.MessageBody;
                msgView.Attachments = chat.Attachments;

                return msgView;
            }
            catch (Exception e)
            {
                return msgView;
            }

        }
    }

}
