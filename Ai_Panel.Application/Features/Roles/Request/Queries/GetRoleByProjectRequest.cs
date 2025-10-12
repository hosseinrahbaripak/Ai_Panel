using Ai_Panel.Application.DTOs;
using Ai_Panel.Domain.Enum;
using MediatR;

namespace Ai_Panel.Application.Features.Roles.Request.Queries;
public class GetRoleByProjectRequest : IRequest<List<IdTitle>>
{
    public List<int> ProjectIds { get; set; }
    public AdminTypeIdEnum? AdminType { get; set; }
}