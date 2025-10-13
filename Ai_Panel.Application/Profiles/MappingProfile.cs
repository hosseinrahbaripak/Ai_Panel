using AutoMapper;
using Ai_Panel.Application.Constants;
using Ai_Panel.Application.DTOs;
using Ai_Panel.Application.DTOs.AiChat;
using Ai_Panel.Application.DTOs.AiConfig;
using Ai_Panel.Application.DTOs.AiContent;

using Ai_Panel.Application.Tools;
using Ai_Panel.Domain;

namespace Ai_Panel.Application.Profiles;

public class MappingProfile : Profile
{
	public MappingProfile()
	{


		CreateMap<UpsertUserAiChatLogDto, UserAiChatLog>().ReverseMap();
		CreateMap<AiChatApiDto, UserAiChatLog>().ReverseMap();

		CreateMap<UpsertAiConfigDto, AiConfig>().ForMember(dest => dest.Stop, opt => opt.MapFrom(src => src.Stop.Split('،', StringSplitOptions.None) ?? Enumerable.Empty<string>()))
			.ReverseMap().ForMember(dest => dest.Stop, opt => opt.MapFrom(src => src.Stop.Any() ? string.Join("،", src.Stop) : null));



	}
}