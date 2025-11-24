using Microsoft.AspNetCore.Http;

namespace psychoshare_api.DTOs.Post
{
    public class CreatePostRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Authorship { get; set; }
        public string? Resume { get; set; }
        public IFormFile? Image { get; set; }
        public IFormFile? Pdf { get; set; }
    }
}
