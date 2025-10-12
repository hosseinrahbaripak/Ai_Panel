using Live_Book.Domain.Enum;

namespace Live_Book.Application.Enum;
public enum UserTypeEnum
{
	Student,
	GeneralAdmin = AdminTypeIdEnum.GeneralAdmin,
	ProjectManager = AdminTypeIdEnum.ProjectManager,
	Advisor = AdminTypeIdEnum.Advisor,
}