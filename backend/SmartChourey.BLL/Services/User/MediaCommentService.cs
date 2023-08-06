using BusinessLogicLayer.Configuration;
using Dapper;
using SmartChourey.BLL.Configuration;
using SmartChourey.BLL.Dtos;
using SmartChourey.BLL.Services.Interfaces.User;
using SmartChourey.DAL;
using SmartChourey.DAL.Repositories.Interfaces;
using System.Data;

namespace SmartChourey.BLL.Services.User
{
    public class MediaCommentService : IMediaCommentService
    {
        private readonly IRepository<ChoureyMediaComment> _mediaCommentRepository;
        private readonly IConnectionFactory _connectionFactory;

        public MediaCommentService(IRepository<ChoureyMediaComment> mediaCommentRepository, IConnectionFactory connectionFactory)
        {
            _mediaCommentRepository = mediaCommentRepository;
            _connectionFactory = connectionFactory;
        }

        public async Task<bool> DeleteChoureyMediaComment(long choureyMediaId)
        {
            try
            {
                var choureyMediaComment = _mediaCommentRepository.Table.Where(x => x.ChoureyMediaCommentId == choureyMediaId).FirstOrDefault();
                if (choureyMediaComment != null)
                {
                    await _mediaCommentRepository.Remove(choureyMediaComment);
                    return true;
                }
                return false;
            } catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IList<ChoureyMediaCommentResponseDto>> GetChoureyMediaComment(long choureyId, EnumHelpers.ECategory category, string? requestedBy = null, long? choureyMediaId = null)
        {
            try
            {
                using var connection = _connectionFactory.GetConnection();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@choureyId", choureyId);
                parameters.Add("@categoryId", (int)category);
                if(choureyMediaId != null)
                {
                    parameters.Add("@choureyMediaId", choureyMediaId);
                }

                if(requestedBy != null)
                {
                    parameters.Add("@requestedBy", requestedBy);
                }
                IEnumerable<ChoureyMediaCommentResponseDto> choureyMediaResponse = await connection.QueryAsync<ChoureyMediaCommentResponseDto>("sp_GetChoureyMediaComment", parameters, commandType: CommandType.StoredProcedure);

                return choureyMediaResponse.ToList();
            } catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> InsertChoureyMediaComment(ChoureyMediaCommentRequestDto data)
        {
            try
            {
                var choureyMediaComment = new ChoureyMediaComment();
                choureyMediaComment.ChoureyId = data.ChoureyId;
                choureyMediaComment.ChoureyMediaId = data.ChoureyMediaId;
                choureyMediaComment.EuploadType = (int)data.EUploadType;
                choureyMediaComment.EdeviceType = (int)data.EDeviceType;
                choureyMediaComment.Ecategory = (int)data.ECategory;
                choureyMediaComment.EfileType = 1;
                choureyMediaComment.CreatedOn = new Utilities().GetTokyoDate();
                choureyMediaComment.CreatedBy = data.CreatedBy;
                choureyMediaComment.Comment = data.Comment;
                await _mediaCommentRepository.Add(choureyMediaComment);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
