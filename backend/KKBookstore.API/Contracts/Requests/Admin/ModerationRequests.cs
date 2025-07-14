using KKBookstore.Common.Configuration;

namespace KKBookstore.Contracts.Requests.Admin;

public record UpdateModerationLevelRequest(ModerationLevel Level);

public record ModerationSettingsRequest();

public record TestCommentModerationRequest(string Comment, int? ProductId = null, string Language = "vi");