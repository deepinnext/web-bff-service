using Deepin.WebBff.API.ApiClients;

namespace Deepin.WebBff.API.Models.Presences;

public class UserPresence
{
    public PresenceStatus Status { get; set; }
    public DateTime LastSeen { get; set; }
    public string CustomStatus { get; set; }
    public DateTime? CustomStatusExpiresAt { get; set; }
    public UserPresence(UserPresenceDto dto)
    {
        Status = dto.Status;
        LastSeen = dto.LastOnlineAt;
        CustomStatus = dto.CustomStatus;
        CustomStatusExpiresAt = dto.CustomStatusExpiresAt;
    }
}
