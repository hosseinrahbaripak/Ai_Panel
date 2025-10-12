using AutoMapper;
using Live_Book.Application.Constants;
using Live_Book.Application.DTOs;
using Live_Book.Application.DTOs.AiChat;
using Live_Book.Application.DTOs.AiConfig;
using Live_Book.Application.DTOs.AiContent;

using Live_Book.Application.Tools;
using Live_Book.Domain;

namespace Live_Book.Application.Profiles;

public class MappingProfile : Profile
{
	public MappingProfile()
	{


		CreateMap<UpsertUserAiChatLogDto, UserAiChatLog>().ReverseMap();
		CreateMap<AiChatApiDto, UserAiChatLog>().ReverseMap();

		CreateMap<UpsertAiConfigDto, AiConfig>().ForMember(dest => dest.Stop, opt => opt.MapFrom(src => src.Stop.Split('،', StringSplitOptions.None) ?? Enumerable.Empty<string>()))
			.ReverseMap().ForMember(dest => dest.Stop, opt => opt.MapFrom(src => src.Stop.Any() ? string.Join("،", src.Stop) : null));

		CreateMap<AiContent, AiContentUpsertDto>().ReverseMap();
		CreateMap<TestAiConfig, TestAiConfigDto>().ReverseMap();


	}
}