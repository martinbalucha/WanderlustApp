using Autofac;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WanderlustInfrastructure.Query;
using WanderlustInfrastructure.Query.Predicates;
using WanderlustInfrastructure.Query.Predicates.Operators;
using WanderlustInfrastructure.UnitOfWork;
using WanderlustPersistence.Entity;
using Xunit;

namespace WanderlustPersistenceTest
{
    public class QueryCountryTest : PersistenceTestBase
    {
        public QueryCountryTest() : base()
        {
            SeedCountries();
        }

        [Fact]
        public async Task FilterUsingElementaryEqualPredicate_Existing()
        {
            var query = Container.Resolve<IQuery<Country>>();

            Country slovakia = InitializeCountries().Where(c => c.Id.Equals(SlovakiaGuid)).FirstOrDefault();

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

        [Fact]
        public async Task FilterUsingElementaryStringContainsPredicate_Existing()
        {           
            var query = Container.Resolve<IQuery<Country>>();
            string partOfName = "Republic";

            Country czechRepublic = InitializeCountries().Where(c => c.Id.Equals(CzechRepublicGuid)).FirstOrDefault();

            var expectedResult = new QueryResult<Country>(new List<Country> { czechRepublic }, 1);
            var predicate = new ElementaryPredicate(nameof(Country.Name), ValueComparingOperator.StringContains, partOfName);

            IUnitOfWork unitOfWork = UnitOfWorkContext.Create();
            var actualResult = await query.Where(predicate).ExecuteAsync();
            await unitOfWork.CommitAsync();

            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task FilterUsingCompositeEqualPredicate_Existing()
        {
            var query = Container.Resolve<IQuery<Country>>();

            var czechRepublic = InitializeCountries().Where(c => c.Id.Equals(CzechRepublicGuid)).FirstOrDefault();
            var expectedResult = new QueryResult<Country>(new List<Country> { czechRepublic }, 1);

            string substring1 = "Czech";
            string substring2 = "Republic";

            var predicate1 = new ElementaryPredicate(nameof(Country.Name), ValueComparingOperator.StringContains, substring1);
            var predicate2 = new ElementaryPredicate(nameof(Country.Name), ValueComparingOperator.StringContains, substring2);
            var compositePredicate = new CompositePredicate(new List<IPredicate> { predicate1, predicate2 });

            IUnitOfWork unitOfWork = UnitOfWorkContext.Create();
            var actualResult = await query.Where(compositePredicate).ExecuteAsync();
            await unitOfWork.CommitAsync();

            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task FilterUsingCompositeEqualPredicate_NotExisting()
        {
            var query = Container.Resolve<IQuery<Country>>();

            var expectedResult = new QueryResult<Country>(new List<Country>(), 0);

            string substring1 = "Czech";
            string substring2 = "Slovak";

            var predicate1 = new ElementaryPredicate(nameof(Country.Name), ValueComparingOperator.StringContains, substring1);
            var predicate2 = new ElementaryPredicate(nameof(Country.Name), ValueComparingOperator.StringContains, substring2);
            var compositePredicate = new CompositePredicate(new List<IPredicate> { predicate1, predicate2 });

            IUnitOfWork unitOfWork = UnitOfWorkContext.Create();
            var actualResult = await query.Where(compositePredicate).ExecuteAsync();
            await unitOfWork.CommitAsync();

            actualResult.Should().BeEquivalentTo(expectedResult);
        }
    }
}
