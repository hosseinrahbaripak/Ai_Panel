using Ai_Panel.Domain.Enum;

namespace Ai_Panel.Application.Enum;
public enum UserTypeEnum
{
	Student,
	GeneralAdmin = AdminTypeIdEnum.GeneralAdmin,
	ProjectManager = AdminTypeIdEnum.ProjectManager,
	Advisor = AdminTypeIdEnum.Advisor,
}