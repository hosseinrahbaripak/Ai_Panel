using Live_Book.Domain;
using Live_Book.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Live_Book.Persistence.Configurations.Entities;

public class AiModelConfiguration : IEntityTypeConfiguration<AiModel>
{
    public void Configure(EntityTypeBuilder<AiModel> builder)
    {
        builder.HasData(
            new AiModel()
            {
                Id = 1,
                Title = "Open Ai",
                ParentId = null,
                IsDelete = false
            },
            new AiModel()
            {
                Id = 2,
                Title = "gpt-4o",
                ParentId = 1,
                IsDelete = false
            },
            new AiModel()
            {
                Id = 3,
                Title = "gpt-4o-mini",
                ParentId = 1,
                IsDelete = false
            },
            new AiModel()
            {
                Id = 4,
                Title = "o1",
                ParentId = 1,
                IsDelete = false
            },
            new AiModel()
            {
                Id = 5,
                Title = "o1-mini",
                ParentId = 1,
                IsDelete = false
            },
            new AiModel()
            {
                Id = 6,
                Title = "o3-mini",
                ParentId = 1,
                IsDelete = false
            },
            new AiModel()
            {
                Id = 7,
                Title = "gpt-3.5-turbo",
                ParentId = 1,
                IsDelete = false
            },
            new AiModel()
            {
                Id = 8,
                Title = "groq",
                ParentId = null,
                IsDelete = false
            },
            new AiModel()
            {
                Id = 9,
                Title = "deepseek-r1-distill-llama-70b",
                ParentId = 8,
                IsDelete = false
            }
        );
    }
}

// https://platform.openai.com/docs/models