using Live_Book.Application.DTOs;
using Live_Book.Domain.Enum;
using MediatR;

namespace Live_Book.Application.Features.Roles.Request.Queries;
public class GetRoleByProjectRequest : IRequest<List<IdTitle>>
{
    public List<int> ProjectIds { get; set; }
    public AdminTypeIdEnum? AdminType { get; set; }
}