namespace BLG.ApplicationConract.uploadimages;

public class UploadimageDto
{
    public Guid id { get; set; }
    public string tiltle { get; set; }
    public string  fileExtension { get; set; }
    public string filename { get; set; }
    public string  urlhandle { get; set; }
    public DateTime time { get; set; }
}