//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================


using Excel.Importer.MVC.Models.Foundations.Groups;
using Excel.Importer.MVC.Services.Foundations.Groups.Exceptions;
using Moq;
using Xunit;

namespace Excel.Importer.MVC.Api.Tests.Unit.Services.Foundations.Groups
{
    public partial class GroupServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfGroupIsNullAndLogItAsync()
        {
            //given
            Group nullGroup = null;
            var nullGroupException = new NullGroupException();

            var expectedGroupValidationException =
                new GroupValidationException(nullGroupException);

            //when
            ValueTask<Group> actualGroupTask =
                this.groupService.AddGroupAsync(nullGroup);

            //then
            await Assert.ThrowsAsync<GroupValidationException>(() =>
                actualGroupTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedGroupValidationException))),
                Times.Once());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertGroupAsync(It.IsAny<Group>()),
                Times.Never());

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfGroupIsInvalidAndLogItAsync(string text)
        {
            //given
            var invalidGroup = new Group
            {
                GroupName = text
            };

            var invalidGroupException = new InvalidGroupException();

            invalidGroupException.AddData(
                key: nameof(Group.Id),
                values: "Id is required");

            invalidGroupException.AddData(
                key: nameof(Group.GroupName),
                values: "Text is required");

            var expectedGroupValidationException =
                new GroupValidationException(invalidGroupException);

            //when
            ValueTask<Group> actualGroupTask = this.groupService.AddGroupAsync(invalidGroup);

            //then
            await Assert.ThrowsAsync<GroupValidationException>(() =>
                actualGroupTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedGroupValidationException))),
                Times.Once());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertGroupAsync(It.IsAny<Group>()),
                Times.Never());

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
