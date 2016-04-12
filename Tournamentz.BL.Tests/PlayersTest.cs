namespace Tournamentz.BL.Tests
{
    using System;
    using System.Linq;
    using BL.Core.Command;
    using BL.Core.Command.Interface;
    using Commands;
    using Core;
    using DAL.Entity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PlayersTest : UnitTestBase
    {
        [TestMethod, TestCategory("Player")]
        public void CreatePlayerSuccessfully()
        {
            // Arrange
            PlayerCommands.Create createCommand = this.SampleCreateCommand();

            // Act
            ICommandResult result = this.RunCommand(createCommand);
            
            // Assert
            Assert.AreEqual(result.Status, CommandResultStatus.Success);
            Assert.IsNotNull(this.UnitOfWork.Repository<Player>().FindById((Guid) result.ReturnValue));
        }

        [TestMethod, TestCategory("Player")]
        public void CreatePlayerDuplicateUsername()
        {
            // Arrange
            PlayerCommands.Create createCommand = this.SampleCreateCommand();
            Guid playerId = this.RunAsPrerequisite<Guid>(createCommand);

            // Act
            ICommandResult result = this.RunCommand(createCommand);

            // Assert
            Assert.AreEqual(result.Status, CommandResultStatus.BrokenRules);
            Assert.AreEqual(result.BusinessRules.Count(b => b.IsBroken), 1);
        }

        protected PlayerCommands.Create SampleCreateCommand()
        {
            return new PlayerCommands.Create
            {
                Name = "Ivan",
                Surname = "Ivić",
                Nickname = "ivan.ivic"
            };
        }
    }
}
