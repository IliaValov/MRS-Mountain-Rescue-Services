using AutoMapper;
using MRS.Common.Mapping;
using MRS.Mobile.Data;
using MRS.Mobile.Data.Models;
using MRS.Services.Mobile.Data;
using MRS.Services.Mobile.Data.Contracts;
using MRS.Tests.Common;
using MRS.Tests.MRS.Mobile.Tests.TestEntities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Tests.MRS.Mobile.Tests
{
    public class TestMessageService
    {
       


        private IMessageService GetMessageService(MrsMobileDbContext dbContext)
        {

            return new MessageService(dbContext);
        }

        private List<MrsMobileMessage> GetDummyMessages()
        {
            var messages = new List<MrsMobileMessage>
            {
                new MrsMobileMessage {
                    Message = "TestMessage",
                    Condition = "TestCondition"


                },
                new MrsMobileMessage {
                    Message = "TestMessage2",
                    Condition = "TestCondition2"

                },
                new MrsMobileMessage {
                    Message = "TestMessage3",
                    Condition = "TestCondition3"

                }
            };


            return messages;
        }

        private async Task SeedDataAsync(MrsMobileDbContext context)
        {
            foreach (var message in GetDummyMessages())
            {
                await context.Messages.AddAsync(message);
            }
            await context.SaveChangesAsync();
        }

        [Test]
        public async Task AddMessage_WithCorredData_ShouldAddNewMessageToTheDb()
        {
            string errorMessagePrefix = "MessageService AddMessageAsync() method does not work properly.";

            //Arrange
            var dbContext = MRSMobileDbContextInMemoryFactory.InitializeMobileContext();

            var messageService = GetMessageService(dbContext);

            //Act

            var expectedResults = Mapper.Map<List<MessageTestEntity>>(GetDummyMessages());

            foreach (var message in expectedResults)
            {
                await messageService.AddMessageAsync(message);
            }

            var actualResults = dbContext.Messages.To<MessageTestEntity>().ToList();

            //Assert
            Assert.IsTrue(actualResults.Count() == expectedResults.Count, errorMessagePrefix + " " + "messages are not returned properly.");
        }

        //Problem with saving the data the inmemorydatabase don't care if the entity has required on the props and is saving the data any way'
        //[Test]
        //public async Task AddMessage_WithIncorectData_ShouldNotAddTheMessages()
        //{
        //    string errorMessagePrefix = "MessageService AddMessageAsync() method does not work properly.";

        //    //Arrange
        //    var dbContext = MRSMobileDbContextInMemoryFactory.InitializeContext();

        //    var messageService = GetMessageService(dbContext);

        //    //Act

        //    var expectedResults = Mapper.Map<List<MessageTestEntity>>(GetDummyMessages());

        //    foreach (var message in expectedResults)
        //    {
        //        message.Condition = null;
        //        await messageService.AddMessageAsync(message);
        //    }

        //    var actualResults = dbContext.Messages.To<MessageTestEntity>().ToList();

        //    //Assert
        //    Assert.IsTrue(actualResults.Count() == 0);

        //}

        [Test]
        public async Task GetAllAsync_WithCorrectData_ShouldReturnAllMessages()
        {
            string errorMessagePrefix = "MessageService GetAllAsync() method does not work properly.";

            //Arrange
            var dbContext = MRSMobileDbContextInMemoryFactory.InitializeMobileContext();
            await SeedDataAsync(dbContext);

            var messageService = GetMessageService(dbContext);

            //Act

            var actualResults = (await messageService.GetAllAsync<MessageTestEntity>()).OrderBy(x => x.CreatedOn).ToList();

            var expectedResults = Mapper.Map<List<MessageTestEntity>>(GetDummyMessages()).OrderBy(x => x.CreatedOn).ToList();

            //Assert
            Assert.IsTrue(actualResults.Count == expectedResults.Count, errorMessagePrefix + " " + "messages are not returned properly.");

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var actualResult = actualResults[0];
                var expectedResult = expectedResults[0];

                Assert.IsTrue(actualResult.Message == expectedResult.Message, errorMessagePrefix + " " + "messages are not returned properly.");

            }
        }

        [Test]
        public async Task GetAllAsync_WithZeroData_ShouldReturnAllMessages()
        {
            string errorMessagePrefix = "MessageService GetAllAsync() method does not work properly.";

            //Arrange
            var dbContext = MRSMobileDbContextInMemoryFactory.InitializeMobileContext();

            var messageService = GetMessageService(dbContext);

            //Act

            var actualResults = (await messageService.GetAllAsync<MessageTestEntity>()).OrderBy(x => x.CreatedOn).ToList();

            var expectedResults = Mapper.Map<List<MessageTestEntity>>(GetDummyMessages()).OrderBy(x => x.CreatedOn).ToList();

            //Assert
            Assert.IsTrue(actualResults.Count == 0, errorMessagePrefix + " " + "should be 0 messages but go one or more.");
        }

        [Test]
        public async Task GetAllMessagesByDateAsync_WithCorrectData_ShouldReturnAllMessagesForTheCurrentDate()
        {
            string errorMessagePrefix = "MessageService GetAllMessagesByDateAsync() method does not work properly.";

            //Arrange
            var dbContext = MRSMobileDbContextInMemoryFactory.InitializeMobileContext();
            await SeedDataAsync(dbContext);

            var messageService = GetMessageService(dbContext);

            //Act

            var actualResults = (await messageService.GetAllMessagesByDateAsync<MessageTestEntity>(DateTime.UtcNow)).OrderBy(x => x.CreatedOn).ToList();

            //Assert
            var expectedResults = Mapper.Map<List<MessageTestEntity>>(GetDummyMessages()).OrderBy(x => x.CreatedOn).ToList();

            Assert.IsTrue(actualResults.Count == expectedResults.Count, errorMessagePrefix + " " + "messages are not returned properly.");

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var actualResult = actualResults[0];
                var expectedResult = expectedResults[0];

                Assert.IsTrue(actualResult.Message == expectedResult.Message, errorMessagePrefix + " " + "messages are not returned properly.");

            }
        }

        [Test]
        public async Task GetAllMessagesByDateAsync_WithZeroData_ShouldReturnZeroMessages()
        {
            string errorMessagePrefix = "MessageService GetAllMessagesByDateAsync() method does not work properly.";

            //Arrange
            var dbContext = MRSMobileDbContextInMemoryFactory.InitializeMobileContext();

            var messageService = GetMessageService(dbContext);

            //Act

            var actualResults = (await messageService.GetAllMessagesByDateAsync<MessageTestEntity>(DateTime.UtcNow)).OrderBy(x => x.CreatedOn).ToList();

            var expectedResults = Mapper.Map<List<MessageTestEntity>>(GetDummyMessages()).OrderBy(x => x.CreatedOn).ToList();

            //Assert
            Assert.IsTrue(actualResults.Count == 0, errorMessagePrefix + " " + "should be 0 messages but go one or more.");

        }

        [Test]
        public async Task GetLastMessageAsync_WithCorrectData_ShouldReturnLastMessagesFromTheDb()
        {
            string errorMessagePrefix = "MessageService GetLastMessageAsync() method does not work properly.";

            //Arrange
            var dbContext = MRSMobileDbContextInMemoryFactory.InitializeMobileContext();
            await SeedDataAsync(dbContext);

            var messageService = GetMessageService(dbContext);

            //Act

            var actualResult = (await messageService.GetLastMessageAsync<MessageTestEntity>());

            var expectedResult = Mapper.Map<List<MessageTestEntity>>(GetDummyMessages()).OrderBy(x => x.CreatedOn).ToList().LastOrDefault();

            //Assert
            Assert.IsTrue(actualResult.Message == expectedResult.Message, errorMessagePrefix + " " + "messages are not returned properly.");


        }

        [Test]
        public async Task GetLastMessageAsync_WithZeroData_ShouldnReturnMessage()
        {
            string errorMessagePrefix = "MessageService GetLastMessageAsync() method does not work properly.";

            //Arrange
            var dbContext = MRSMobileDbContextInMemoryFactory.InitializeMobileContext();

            var messageService = GetMessageService(dbContext);

            //Act

            var actualResult = (await messageService.GetLastMessageAsync<MessageTestEntity>());

            //Assert
            Assert.IsTrue(actualResult == null, errorMessagePrefix + " " + "messages are not returned properly.");

        }
    }
}
