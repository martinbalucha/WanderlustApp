using Autofac;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WanderlustInfrastructure.Query;
using WanderlustInfrastructure.Query.Predicates;
using WanderlustInfrastructure.Query.Predicates.Operators;
using WanderlustInfrastructure.UnitOfWork;
using WanderlustPersistence.Entity;
using WanderlustPersistence.Enums;
using Xunit;

namespace WanderlustPersistenceTest
{
    public class QueryUserTests : PersistenceTestBase
    {
        private Guid AdminGuid { get; set; } = Guid.NewGuid();

        private Guid RegularUserGuid { get; set; } = Guid.NewGuid();

        public QueryUserTests() : base()
        {
            SeedUsers();
        }

        [Fact]
        public async void FilterUsingElementaryLessThanPredicate_Existing()
        {
            var query = Container.Resolve<IQuery<User>>();
            DateTime upperBound = DateTime.Parse("1990-01-01");

            var youngerUsers = InitializeUsers().Where(c => c.DateOfBirth < upperBound).ToList();

            var expectedResult = new QueryResult<User>(youngerUsers, youngerUsers.Count);
            var predicate = new ElementaryPredicate(nameof(User.DateOfBirth), ValueComparingOperator.LessThan, upperBound);

            IUnitOfWork unitOfWork = UnitOfWorkContext.Create();
            var actualResult = await query.Where(predicate).ExecuteAsync();
            await unitOfWork.CommitAsync();

            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async void FilterUsingElementaryGreaterThanPredicate_Existing()
        {
            var query = Container.Resolve<IQuery<User>>();
            DateTime upperBound = DateTime.Parse("1990-01-01");

            var youngerUsers = InitializeUsers().Where(c => c.DateOfBirth > upperBound).ToList();

            var expectedResult = new QueryResult<User>(youngerUsers, youngerUsers.Count);
            var predicate = new ElementaryPredicate(nameof(User.DateOfBirth), ValueComparingOperator.GreaterThan, upperBound);

            IUnitOfWork unitOfWork = UnitOfWorkContext.Create();
            var actualResult = await query.Where(predicate).ExecuteAsync();
            await unitOfWork.CommitAsync();

            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async void FilterUsingElementaryLessThanOrEqualPredicate_Existing()
        {
            var query = Container.Resolve<IQuery<User>>();
            DateTime upperBound = DateTime.Parse("1996-10-01");

            var youngerUsers = InitializeUsers().Where(c => c.DateOfBirth <= upperBound).ToList();

            var expectedResult = new QueryResult<User>(youngerUsers, youngerUsers.Count);
            var predicate = new ElementaryPredicate(nameof(User.DateOfBirth), ValueComparingOperator.LessThanOrEqual, upperBound);

            IUnitOfWork unitOfWork = UnitOfWorkContext.Create();
            var actualResult = await query.Where(predicate).ExecuteAsync();
            await unitOfWork.CommitAsync();

            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async void FilterUsingElementaryGreaterThanOrEqualPredicate_Existing()
        {
            var query = Container.Resolve<IQuery<User>>();
            DateTime upperBound = DateTime.Parse("1996-10-01");

            var youngerUsers = InitializeUsers().Where(c => c.DateOfBirth >= upperBound).ToList();

            var expectedResult = new QueryResult<User>(youngerUsers, youngerUsers.Count);
            var predicate = new ElementaryPredicate(nameof(User.DateOfBirth), ValueComparingOperator.GreaterThanOrEqual, upperBound);

            IUnitOfWork unitOfWork = UnitOfWorkContext.Create();
            var actualResult = await query.Where(predicate).ExecuteAsync();
            await unitOfWork.CommitAsync();

            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async void FilterUsingElementaryGreaterNotEqualPredicate_Existing()
        {
            var query = Container.Resolve<IQuery<User>>();
            DateTime upperBound = DateTime.Parse("1996-10-01");

            var youngerUsers = InitializeUsers().Where(c => c.DateOfBirth != upperBound).ToList();

            var expectedResult = new QueryResult<User>(youngerUsers, youngerUsers.Count);
            var predicate = new ElementaryPredicate(nameof(User.DateOfBirth), ValueComparingOperator.NotEqual, upperBound);

            IUnitOfWork unitOfWork = UnitOfWorkContext.Create();
            var actualResult = await query.Where(predicate).ExecuteAsync();
            await unitOfWork.CommitAsync();

            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        private void SeedUsers()
        {
            var context = Container.Resolve<DbContext>();
            IList<User> users = InitializeUsers();
            context.Set<User>().AddRange(users);

            context.SaveChanges();

            foreach (var user in context.ChangeTracker.Entries())
            {
                user.State = EntityState.Detached;
            }
        }

        private IList<User> InitializeUsers()
        {
            var admin = new User
            {
                Id = AdminGuid,
                Username = "Mato",
                DateOfBirth = DateTime.Parse("1996-10-01"),
                Email = "testing.email@outlook.com",
                Salt = "L3CdLhdf2ya9aBzqQOc6sg==",
                Password = "452BCkh9nkdsU708cP0JBXTsTJw=",
                Role = Role.Admin,
            };

            var regularUser = new User
            {
                Id = RegularUserGuid,
                Username = "Nieber",
                DateOfBirth = DateTime.Parse("1987-05-10"),
                Email = "testing.user@outlook.com",
                Salt = "L3CdLhdf2ya9aBzqQOc6sg==",
                Password = "452BCkh9nkdsU708cP0JBXTsTJw=",
                Role = Role.Regular,
            };

            return new List<User> { admin, regularUser };
        }
    }
}
