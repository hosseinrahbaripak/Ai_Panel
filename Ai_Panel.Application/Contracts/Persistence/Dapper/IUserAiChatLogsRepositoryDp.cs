using Ai_Panel.Application.DTOs;
using Ai_Panel.Application.DTOs.AiChat;

namespace Ai_Panel.Application.Contracts.Persistence.Dapper;
public interface IUserAiChatLogsRepositoryDp
{
	Task<List<IdTitleTimeBased>> GetAiChatsCount(object parameters);
	Task<List<WordsCountIdTitle>> GetAiChatWordsCount(object parameters);
    Task<List<AiChatLogDto>> GetAiChatlogs(string where, string orderBy, object parameters);
}

