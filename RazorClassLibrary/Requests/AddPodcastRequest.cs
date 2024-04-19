

namespace RazorClassLibrary.Requests;

public class AddPodcastRequest
{
    public string URL { get; set; } = "";
    public bool Commentable { get; set; }
    public DateTime PublishDate { get; set; } = DateTime.Today.ToUniversalTime();
    public string PodcastName { get; set; } = "Default Name";
}
