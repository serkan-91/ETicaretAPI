namespace EticaretAPI.Application.ResponseParameters;

public class UpladImageResults
{
    public List<UpladImageResult> UpladImages { get; set; } = new List<UpladImageResult>();
}

public class UpladImageResult
{
    public string FileName { get; set; }
    public string PathOrContainerName { get; set; }
}
