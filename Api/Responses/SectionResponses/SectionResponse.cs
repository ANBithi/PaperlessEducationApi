using Api.ViewModels;
using System.Collections.Generic;

namespace Api.Responses.SectionResponses
{
    public class SectionResponse
    {
        public bool Response { get; set; }
        public List<SectionViewModel> Data { get; set; }
    }
}
