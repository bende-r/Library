using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configurations
{
    public class UserBookConfiguration : IEntityTypeConfiguration<UserBook>
    {
        public void Configure(EntityTypeBuilder<UserBook> builder)
        {
            builder.HasKey(ub => ub.Id);

            // Указываем, что Id будет автоинкрементным
            builder.Property(ub => ub.Id)
                .ValueGeneratedOnAdd();

            builder.Property(ub => ub.BorrowedDate)
                .IsRequired(false);

            builder.Property(ub => ub.ReturnDate)
                .IsRequired(false);

            // Отношение с пользователем (многие ко многим через промежуточную сущность)
            builder.HasOne(ub => ub.User)
                .WithMany(u => u.UserBooks)
                .HasForeignKey(ub => ub.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Отношение с книгами (многие ко многим через промежуточную сущность)
            builder.HasOne(ub => ub.Book)
                .WithMany(b => b.UserBooks)
                .HasForeignKey(ub => ub.BookId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
