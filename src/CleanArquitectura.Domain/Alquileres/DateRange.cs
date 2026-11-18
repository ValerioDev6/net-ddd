using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArquitectura.Domain.Alquileres
{
    public class DateRange
    {
        private DateRange() { }

        public DateOnly Inicio { get; init; }
        public DateOnly Fin { get; init; }

        public int CantidadDias => Fin.DayNumber - Inicio.DayNumber;

        public static DateRange Create(DateOnly inicio, DateOnly fin)
        {
            if (inicio > fin)
            {
                throw new ApplicationException("La fecha es anterior ala fecha de inicio");
            }

            return new DateRange
            {
                Inicio = inicio,
                Fin = fin
            };
        }
    }
}
