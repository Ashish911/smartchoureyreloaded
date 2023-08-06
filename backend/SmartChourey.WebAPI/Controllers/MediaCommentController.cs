using BusinessLogicLayer.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartChourey.BLL.Dtos;
using SmartChourey.BLL.Services.Interfaces.User;


namespace SmartChourey.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MediaCommentController : ControllerBase
    {
        private readonly IMediaCommentService _mediaCommentService;
        private readonly UserManager<IdentityUser> _userManager;

        public MediaCommentController(IMediaCommentService mediaCommentService,
            UserManager<IdentityUser> userManager)
        {
            _mediaCommentService= mediaCommentService;
            _userManager = userManager;
        }

        [HttpPost("addComment"), Authorize]
        public async Task<IActionResult> AddComment([FromBody] ChoureyMediaCommentRequestDto choureyMediaReq)
        {
            try
            {
                IdentityUser user = await _userManager.FindByEmailAsync(User.Identity.Name);
                choureyMediaReq.CreatedBy = user.Id;

                var isSuccess = await _mediaCommentService.InsertChoureyMediaComment(choureyMediaReq);
                if (isSuccess)
                {
                    return StatusCode(200, "Comment stored successfully");
                }

                return StatusCode(500, "Comment not stored");
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("deleteComment")]
        public async Task<IActionResult> DeleteComment(long commentId)
        {
            try
            {
                var isSuccess = await _mediaCommentService.DeleteChoureyMediaComment(commentId);
                if (isSuccess)
                {
                    return StatusCode(200, "Comment Deleted Successfully");
                }

                return StatusCode(404, "Comment Not Found. Delete Failed! ");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("getComments")]
        public async Task<IActionResult> GetComment(long choureyId, EnumHelpers.ECategory category, string? requestedBy)
        {
            try
            {
                var listOfMediaComments = await _mediaCommentService.GetChoureyMediaComment(choureyId, category, requestedBy);
                if (listOfMediaComments.Count > 0)
                {
                    return StatusCode(200, listOfMediaComments);
                }
                return StatusCode(204, "No Comments");
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("getCommentsByMediaId")]
        public async Task<IActionResult> GetComment(long choureyId, EnumHelpers.ECategory category, string? requestedBy, long mediaId)
        {
            try
            {
                var listOfMediaComments = await _mediaCommentService.GetChoureyMediaComment(choureyId, category, requestedBy, mediaId);
                if (listOfMediaComments.Count > 0)
                {
                    return StatusCode(200, listOfMediaComments);
                }
                return StatusCode(204, "No Comments");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
