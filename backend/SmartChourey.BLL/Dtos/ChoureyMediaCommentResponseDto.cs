namespace SmartChourey.BLL.Dtos
{
    public class ChoureyMediaCommentResponseDto
    {
        public string Comment { get; set; }
        public long ChoureyMediaCommentId { get; set; }
        public long ChoureyMediaId { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int EUploadType { get; set; }
        public bool IsDelete { get; set; }
    }
}
