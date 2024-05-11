using Api.ViewModels;
using System.Collections.Generic;

namespace Api.Responses.ResultResponses
{
    public class ResultResponse
    {
        public List<ResultViewModel> Data { get; set; }
        public bool Response { get; set; }
    }
}
