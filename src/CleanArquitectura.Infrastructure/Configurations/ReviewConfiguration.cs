using CleanArquitectura.Domain.Alquileres;
using CleanArquitectura.Domain.Reviews;
using CleanArquitectura.Domain.Users;
using CleanArquitectura.Domain.Vehiculos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArquitectura.Infrastructure.Configurations;


internal sealed class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("reviews");

        builder.HasKey(review => review.Id);

        builder.Property(review => review.Id)
            .HasConversion(id => id.Value, id => new ReviewId(id));
        
        builder.Property(review => review.Rating)
            .HasConversion(rating => rating.Value, value => Raiting.Create(value).Value);

        builder.Property(review => review.Comentario)
            .HasMaxLength(200)
            .HasConversion(comentario => comentario.Value, value => new Comentario(value));

        builder.HasOne<Vehiculo>()
            .WithMany()
            .HasForeignKey(review => review.VehiculoId);

        builder.HasOne<Alquiler>()
            .WithMany()
            .HasForeignKey(review => review.AlquilerId);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(review => review.UserId);




    }
}