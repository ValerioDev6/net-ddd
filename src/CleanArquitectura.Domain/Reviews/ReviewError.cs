using CleanArquitectura.Domain.Abstraccions;

namespace CleanArquitectura.Domain.Reviews;

public static class ReviewError
{
    public static readonly Error NotEligible = new(
        "Review.NotEligible",
        "Est rview y calificacion apra el auto no es legible porque aun nose completa");
}