using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArquitectura.Domain.Abstraccions;

namespace CleanArquitectura.Domain.UnitTests.Insfrastructure
{
    public class BaseTest
    {
        public static T AssertDomainEventWasPublished<T>(IEntity entity) where T : IDomainEvents
        {
            var domainEvent = entity.GetDomainEvents().OfType<T>().SingleOrDefault();
            if (domainEvent is null)
            {
                throw new Exception($"{typeof(T).Name} no fue publicado");
            }

            return domainEvent;
        }

    }
}
