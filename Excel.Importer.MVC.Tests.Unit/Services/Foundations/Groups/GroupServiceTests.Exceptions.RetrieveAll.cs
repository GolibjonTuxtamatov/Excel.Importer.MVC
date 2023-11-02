//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Excel.Importer.MVC.Services.Foundations.Groups.Exceptions;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;
using Xunit;

namespace Excel.Importer.MVC.Api.Tests.Unit.Services.Foundations.Groups
{
    public partial class GroupServiceTests
    {
        [Fact]
        public void ShouldThrowCriticalDependencyExceptionOnRetrieveAllIfSqlErrorOccursAndLogIt()
        {
            //given
            SqlException sqlException = GetSqlError();

            var faildStorageGroupException = new FaildStorageGroupException(sqlException);

            var expectedGroupDependecyException =
                new GroupDependencyException(faildStorageGroupException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllGroup())
                .Throws(sqlException);

            //when
            Action retrieveAllGroupsAction = () => this.groupService.RetrieveAllGroups();

            GroupDependencyException actualGroupdependencyException =
                Assert.Throws<GroupDependencyException>(retrieveAllGroupsAction);

            //then
            actualGroupdependencyException.Should().BeEquivalentTo(expectedGroupDependecyException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllGroup(), Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(expectedGroupDependecyException))),
                Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowServiceExceptionOnRetrieveAllIfServiceErrorOccursAndLogIt()
        {
            //given
            var exception = new Exception();
            var failedServiceException = new FailedServiceException(exception);

            var expectedGroupServiceException =
                new GroupServiceException(failedServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllGroup()).Throws(exception);

            //when
            Action groupRetrieveAllAction = () => this.groupService.RetrieveAllGroups();

            GroupServiceException actualGroupException =
                Assert.Throws<GroupServiceException>(groupRetrieveAllAction);

            //then
            actualGroupException.Should().BeEquivalentTo(expectedGroupServiceException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllGroup(), Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedGroupServiceException))),
                Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
