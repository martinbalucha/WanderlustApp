using Autofac;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WanderlustInfrastructure.Query;
using WanderlustInfrastructure.Query.Predicates;
using WanderlustInfrastructure.Query.Predicates.Operators;
using WanderlustInfrastructure.UnitOfWork;
using WanderlustPersistence.Entity;
using WanderlustPersistence.Infrastructure;
using WanderlustPersistence.Infrastructure.Query;
using WanderlustPersistence.Infrastructure.UnitOfWork;
using Xunit;

namespace WanderlustPersistenceTest
{
    public class QueryTest : PersistenceTestBase
    {
        private Guid SlovakiaGuid { get; set; } = Guid.NewGuid();

        private Guid CzechRepublicGuid { get; set; } = Guid.NewGuid();

        public QueryTest() : base()
        {
            Seed();
        }

        /// <summary>
        /// Inserts initial data
        /// </summary>
        private void Seed()
        {
            var context = Container.Resolve<DbContext>();

            Country slovakia = new Country
            {
                Id = SlovakiaGuid,
                Name = "Slovakia",
                Code = "SK",
                Description = "A small but beautiful country"
            };

            Country czechRepublic = new Country
            {
                Id = CzechRepublicGuid,
                Name = "Czech Republic",
                Code = "CZ",
                Description = "A small but beautiful country"
            };


            context.Set<Country>().Add(slovakia);
            context.Set<Country>().Add(czechRepublic);
            context.SaveChanges();

            foreach (var country in context.ChangeTracker.Entries())
            {
                country.State = EntityState.Detached;
            }
        }

        [Fact]
        public async Task FilterUsingElementaryEqualPredicate_Existing()
        {
            var query = Container.Resolve<IQuery<Country>>();

            Country slovakia = new Country
            {
                Id = SlovakiaGuid,
                Name = "Slovakia",
                Code = "SK",
                Description = "A small but beautiful country"
            };

            var expectedResult = new QueryResult<Country>(new List<Country> { slovakia }, 1);
            var predicate = new ElementaryPredicate(nameof(Country.Name), ValueComparingOperator.Equal, slovakia.Name);

            IUnitOfWork unitOfWork = UnitOfWorkContext.Create();
            var actualResult = await query.Where(predicate).ExecuteAsync();
            await unitOfWork.CommitAsync();

            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task FilterUsingElementaryEqualPredicate_NotExisting()
        {
            var query = Container.Resolve<IQuery<Country>>();
            string austriaName = "Austria";

            var expectedResult = new QueryResult<Country>(new List<Country>(), 0);
            var predicate = new ElementaryPredicate(nameof(Country.Name), ValueComparingOperator.Equal, austriaName);

            IUnitOfWork unitOfWork = UnitOfWorkContext.Create();
            var actualResult = await query.Where(predicate).ExecuteAsync();
            await unitOfWork.CommitAsync();

            actualResult.Should().BeEquivalentTo(expectedResult);
        }
    }
}
