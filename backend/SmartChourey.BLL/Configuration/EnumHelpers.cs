namespace BusinessLogicLayer.Configuration
{
    public class EnumHelpers
    {
        public enum EStatus
        {
            Active = 1,
            Inactive
        }

        public enum EFileType
        {
            PDF = 1,
            Doc
        }

        public enum EViewMode
        {
            Normal = 1,
            Demo
        }

        public enum ECategory
        {
            ChoureyOne = 1,
            ChoureyTwo,
            Disaster,
            SafetyDeclaration
        }

        public enum EUploadType
        {
            Image = 1,
            Video,
            Pdf
        }

        public enum EDeviceType
        {
            Android = 1,
            iOS,
            Web
        }

        public enum EChangeCategory
        {
            Info = 1,
            Image,
            Video,
            File
        }

        public enum EChangeType
        {
            Added = 1,
            Modified,
            Deleted
        }
    }
}