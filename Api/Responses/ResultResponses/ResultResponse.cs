using Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Responses.ResultResponses
{
    public class ResultResponse
    {
        public List<ResultViewModel> Data { get; set; }
        public bool Response { get; set; }
    }
}
