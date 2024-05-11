using Api.ViewModels;
using System.Collections.Generic;

namespace Api.Responses.PostResponses
{
    public class PostResponse
    {
        public bool Response { get; set; }
        public List<PostViewerModel> Data { get; set; }
    }

}
