using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);

            // ISBN должно быть уникальным
            builder.HasIndex(b => b.ISBN).IsUnique();

            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.Genre)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(b => b.Description)
                .HasMaxLength(500);

            builder.Property(b => b.AuthorId)
                .IsRequired();

            builder.Property(b => b.ImageUrl)
         .IsRequired(false);

            builder.Property(b => b.IsBorrowed).IsRequired();
            // Отношение один ко многим с Author
            builder.HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade); // Удаление книги при удалении автора

            // Отношение с UserBook (многие ко многим через промежуточную сущность)
            builder.HasMany(b => b.UserBooks)
                .WithOne(ub => ub.Book)
                .HasForeignKey(ub => ub.BookId);
        }
    }
}