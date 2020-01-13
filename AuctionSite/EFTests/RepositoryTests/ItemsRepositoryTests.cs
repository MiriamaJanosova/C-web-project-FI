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
    public class ItemsRepositoryTests
    {
        private readonly IUnitOfWorkProvider unitOfWorkProvider = Initializer.Container.Resolve<IUnitOfWorkProvider>();

        private readonly IRepository<Item> usersRepository = Initializer.Container.Resolve<IRepository<Item>>();

        #region RandomItems

        private Item randomItem1 = new Item
        {
            Name = "random item",
            Description = "random item description"
        };

        
        #endregion
        
        [Test]
        public async Task GetFirstItemAsync_AlreadyStoredInDBNoIncludes_ReturnsCorrectUser()
        {
            Item firstItem;
            using (unitOfWorkProvider.Create())
            {
                firstItem = await usersRepository.GetAsync(EFTestsInstaller.ItemUser1.Id);
            }

            firstItem.Should().BeEquivalentTo(EFTestsInstaller.ItemUser1);
        }
        
        [Test]
        public async Task CreateUserAsync_UserIsNotPreviouslySeeded_CreatesNewUser()
        {

            var item1 = randomItem1.DeepClone();
            
            using (var uow = unitOfWorkProvider.Create())
            {
                item1.Owner = EFTestsInstaller.User2;
                item1.OwnerID = EFTestsInstaller.User2.Id;
                usersRepository.Create(item1);
                await uow.Commit();
                
            }

            item1.Id.Should().NotBe(0);
        }
        
       [Test]
       public async Task UpdateUserAsync_UserIsPreviouslySeeded_UpdatesUser()
       {
           Item updatedItem;
           var newItem = randomItem1.DeepClone();

           using (var uow = unitOfWorkProvider.Create())
           {
               newItem.OwnerID = EFTestsInstaller.User2.Id;
               usersRepository.Create(newItem);
               await uow.Commit();
               newItem.Description = "updated description";
               usersRepository.Update(newItem);
               updatedItem = await usersRepository.GetAsync(newItem.Id);
           }

           updatedItem.Should().BeEquivalentTo(newItem);
       }
       
       [Test]
       public async Task DeleteCategoryAsync_CategoryIsPreviouslySeeded_DeletesCategory()
       {
           var itemToDelete = randomItem1.DeepClone();
           Item deletedItem;
           
           using (var uow = unitOfWorkProvider.Create())
           {
               itemToDelete.OwnerID = EFTestsInstaller.User2.Id;
               usersRepository.Create(itemToDelete);
               await uow.Commit();
               itemToDelete.Id.Should().NotBe(0);
               usersRepository.Delete(itemToDelete.Id);
               await uow.Commit();
               deletedItem = await usersRepository.GetAsync(itemToDelete.Id);
           }

           deletedItem.Should().Be(null);
       }
    }
}