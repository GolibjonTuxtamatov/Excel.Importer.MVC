//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using EFxceptions.Models.Exceptions;
using Excel.Importer.MVC.Models.Foundations.Groups;
using Excel.Importer.MVC.Services.Foundations.Groups.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;
using Xunit;

namespace Excel.Importer.MVC.Api.Tests.Unit.Services.Foundations.Groups
{
    public partial class GroupServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccuresAndLogitAsync()
        {
            //given
            Group someGroup = CreateRandomGroup();
            SqlException sqlException = GetSqlError();
            var faildStorageGroupException = new FaildStorageGroupException(sqlException);

            var expectedGroupDependencyException =
                new GroupDependencyException(faildStorageGroupException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertGroupAsync(someGroup))
                .ThrowsAsync(sqlException);

            //when
            ValueTask<Group> actualGroupTask =
                this.groupService.AddGroupAsync(someGroup);

            //then
            await Assert.ThrowsAsync<GroupDependencyException>(() =>
                actualGroupTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertGroupAsync(someGroup),
                Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(expectedGroupDependencyException))),
                Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShoulThrowDependencyValidationExceptionOnAddIfDuplicateErrorOccuresAndLogitAsync()
        {
            //given
            Group someGroup = CreateRandomGroup();
            var duplicateKeyException = new DuplicateKeyException(GetRandomString());
            var alreadyExistGroupException = new AlreadyExistGroupException(duplicateKeyException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertGroupAsync(someGroup))
                .ThrowsAsync(duplicateKeyException);

            var expectedGroupDependencyValidationException =
                new GroupDependencyValidationException(alreadyExistGroupException);

            //when
            ValueTask<Group> actualGroupTask =
                this.groupService.AddGroupAsync(someGroup);

            //then
            await Assert.ThrowsAsync<GroupDependencyValidationException>(() =>
                actualGroupTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertGroupAsync(someGroup), Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedGroupDependencyValidationException))),
                Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccuresAndLogItAsync()
        {
            //given
            Group someGroup = CreateRandomGroup();
            var exception = new Exception();
            var faildServiceException = new FailedServiceException(exception);
            var expectedGroupServiceException = new GroupServiceException(faildServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertGroupAsync(someGroup))
                .ThrowsAsync(exception);

            //when
            ValueTask<Group> actualGroupTask =
                this.groupService.AddGroupAsync(someGroup);

            //then
            await Assert.ThrowsAsync<GroupServiceException>(() => actualGroupTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertGroupAsync(someGroup), Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedGroupServiceException))),
                Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
