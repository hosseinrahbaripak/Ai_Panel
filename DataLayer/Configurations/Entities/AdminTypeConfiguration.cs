using Ai_Panel.Domain;
using Ai_Panel.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ai_Panel.Persistence.Configurations.Entities;
public class AdminTypeConfiguration : IEntityTypeConfiguration<AdminType>
{
	public void Configure(EntityTypeBuilder<AdminType> builder)
	{
		builder.HasData(
			new AdminType() { 
				Id = (int)AdminTypeIdEnum.GeneralAdmin,
				Title = "ادمین کل",
			},
			new AdminType()
			{
				Id = (int)AdminTypeIdEnum.ProjectManager,
				Title = "مدیر پروژه",
			},
			new AdminType()
			{
				Id = (int)AdminTypeIdEnum.Advisor,
				Title = "مشاور",
			},
            new AdminType()
            {
                Id = (int)AdminTypeIdEnum.ParentAdvisor,
                Title = "سر مشاور",
            },
            new AdminType()
            {
                Id = (int)AdminTypeIdEnum.Supervisor,
                Title = "سر پرست",
            }
        );
	}
}

