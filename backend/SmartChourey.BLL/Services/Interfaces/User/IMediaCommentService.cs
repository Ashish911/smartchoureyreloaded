using BusinessLogicLayer.Configuration;
using SmartChourey.BLL.Dtos;

namespace SmartChourey.BLL.Services.Interfaces.User
{
    public interface IMediaCommentService
    {
        Task<bool> InsertChoureyMediaComment(ChoureyMediaCommentRequestDto data);
        Task<IList<ChoureyMediaCommentResponseDto>> GetChoureyMediaComment(long choureyId, EnumHelpers.ECategory category, string? requestedBy = null, long? choureyMediaId = null);
        Task<bool> DeleteChoureyMediaComment(long choureyMediaId);
    }
}
