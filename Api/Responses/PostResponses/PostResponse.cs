using Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Responses.PostResponses
{
    public class PostResponse
    {
        public bool Response { get; set; }
        public List<PostViewerModel> Data { get; set; }
    }

}
