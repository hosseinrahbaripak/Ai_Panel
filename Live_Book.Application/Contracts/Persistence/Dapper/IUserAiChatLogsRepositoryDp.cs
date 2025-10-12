using Live_Book.Application.DTOs;
using Live_Book.Application.DTOs.AiChat;

namespace Live_Book.Application.Contracts.Persistence.Dapper;
public interface IUserAiChatLogsRepositoryDp
{
	Task<List<IdTitleTimeBased>> GetAiChatsCount(object parameters);
	Task<List<WordsCountIdTitle>> GetAiChatWordsCount(object parameters);
    Task<List<AiChatLogDto>> GetAiChatlogs(string where, string orderBy, object parameters);
}

