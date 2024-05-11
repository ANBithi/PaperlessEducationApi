namespace Api.CustomValues
{
    public enum AttachmentType
    {
        Link,
        Image,
        Video,
        Pdf
    }
    public class Metadata
    {
        public string Url { get; set; }
        public string Name { get; set; }

    }

    public class LinkMetadata  : Metadata
    {
    
    }


    public class ImageMetadata : Metadata
    {
        public string FileFormat { get; set; }
        public int FileSize { get; set; }
    }

    public class VideoMetadata : Metadata
    {
        public string FileFormat { get; set; }
        public int FileSize { get; set; }

    }

    public class PdfMetadata : Metadata
    {
        public int FileSize { get; set; }

    }

    public class Attachment
    {
        public AttachmentType Type { get; set; }
        public Metadata Metadata { get; set; }
    }
}
