using BusinessLogicLayer.Configuration;

namespace SmartChourey.BLL.Dtos
{
    public class ChoureyMediaCommentRequestDto
    {
        public string Comment { get; set; }
        public EnumHelpers.ECategory ECategory { get; set; }
        public long ChoureyId { get; set; }
        public long ChoureyMediaId { get; set; }
        public EnumHelpers.EUploadType EUploadType { get; set; }
        public string CreatedBy { get; set; }
        public EnumHelpers.EDeviceType EDeviceType { get; set; }
    }
}
