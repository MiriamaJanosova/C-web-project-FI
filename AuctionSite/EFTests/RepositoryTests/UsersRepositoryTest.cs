using System.Threading.Tasks;
using DAL.Entities;
using EFTests.Config;
using FluentAssertions;
using Force.DeepCloner;
using Infrastructure;
using Infrastructure.UnitOfWork;
using NUnit.Framework;

namespace EFTests.RepositoryTests
{
    public class UsersRepositoryTest
    {
        
        private readonly IUnitOfWorkProvider unitOfWorkProvider = Initializer.Container.Resolve<IUnitOfWorkProvider>();

        private readonly IRepository<User> usersRepository = Initializer.Container.Resolve<IRepository<User>>();

        #region Users

        private User randomUser1 = new User
        {
            UserName = "radnomUser1",
            Email = "randomUser1@random.com"
        };

        private User randomUser2 = new User
        {
            UserName = "randomUser2",
            Email = "randomUser2@randooom.com"
        };
        
        #endregion
        
        [Test]
        public async Task GetFirstUserAsync_AlreadyStoredInDBNoIncludes_ReturnsCorrectUser()
        {
            User firstUser;
            using (unitOfWorkProvider.Create())
            {
                firstUser = await usersRepository.GetAsync(EFTestsInstaller.User1.Id);
            }

            firstUser.Should().BeEquivalentTo(EFTestsInstaller.User1);
        }
        
        [Test]
        public async Task CreateUserAsync_UserIsNotPreviouslySeeded_CreatesNewUser()
        {

            var user1 = randomUser1.DeepClone();

            using (var uow = unitOfWorkProvider.Create())
            {
                usersRepository.Create(user1);
                await uow.Commit();
                
            }

            user1.Id.Should().NotBe(0);
        }
        
        [Test]
        public async Task UpdateUserAsync_UserIsPreviouslySeeded_UpdatesUser()
        {
            User updatedUser;
            var newUser = randomUser2.DeepClone();

            using (var uow = unitOfWorkProvider.Create())
            {
                usersRepository.Create(newUser);
                await uow.Commit();
                newUser.UserName = "updatedd";
                usersRepository.Update(newUser);
                updatedUser = await usersRepository.GetAsync(newUser.Id);
            }

            updatedUser.Should().BeEquivalentTo(newUser);
        }
        
        [Test]
        public async Task DeleteCategoryAsync_CategoryIsPreviouslySeeded_DeletesCategory()
        {
            var userToDelete = randomUser2.DeepClone();
            User deletedUser;
            
            using (var uow = unitOfWorkProvider.Create())
            {
                usersRepository.Create(userToDelete);
                await uow.Commit();
                userToDelete.Id.Should().NotBe(0);
                usersRepository.Delete(userToDelete.Id);
                await uow.Commit();
                deletedUser = await usersRepository.GetAsync(userToDelete.Id);
            }

            deletedUser.Should().Be(null);
        }
    }
}