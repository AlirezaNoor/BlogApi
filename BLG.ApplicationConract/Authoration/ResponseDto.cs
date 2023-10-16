namespace BLG.ApplicationConract.Authoration;

public class ResponseDto
{
    public string Email { get; set; }
    public string Token { get; set; }
    public List<string> role { get; set; }
}