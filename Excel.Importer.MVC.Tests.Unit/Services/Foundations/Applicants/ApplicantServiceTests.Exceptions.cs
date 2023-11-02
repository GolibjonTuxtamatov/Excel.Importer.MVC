//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using EFxceptions.Models.Exceptions;
using Excel.Importer.MVC.Models.Foundations.Applicants;
using Excel.Importer.MVC.Models.Foundations.Applicants.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;
using Xunit;

namespace Excel.Importer.MVC.Api.Tests.Unit.Services.Foundations.Applicants
{
    public partial class ApplicantServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptiononAddIfSqlErrorOccursAndLogItAsync()
        {
            //given
            Applicant someApplicant = CreateRandomApplicant();
            SqlException sqlException = GetSqlError();
            var failedApplicantStorageException = new FailedApplicantStorageException(sqlException);

            var expectedApplicantDependencyExcpetion =
                new ApplicantDependencyExcpetion(failedApplicantStorageException);

            this.storageBrokerMock.Setup(broker =>
            broker.InsertApplicantAsync(someApplicant))
                .ThrowsAsync(sqlException);

            //when
            ValueTask<Applicant> addApplicantTask =
                this.applicantService.AddApplicantAsync(someApplicant);

            //then
            await Assert.ThrowsAsync<ApplicantDependencyExcpetion>(() =>
                addApplicantTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertApplicantAsync(someApplicant),
                    Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedApplicantDependencyExcpetion))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationOnAddIfDuplicateKeyErrorOccursAndLogItAsync()
        {
            //given
            Applicant someApplicant = CreateRandomApplicant();
            string someMessage = GetRandomString();

            DuplicateKeyException duplicateKeyException =
                new DuplicateKeyException(someMessage);

            var alreadyExistApplicantException =
                new AlreadyExistApplicantException(duplicateKeyException);

            var expectedApplicantDependencyValidationExcpetion =
                new ApplicantDependencyValidationExcpetion(alreadyExistApplicantException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertApplicantAsync(someApplicant))
                .ThrowsAsync(duplicateKeyException);

            //when
            ValueTask<Applicant> addApplicantTask =
                this.applicantService.AddApplicantAsync(someApplicant);

            //then
            await Assert.ThrowsAsync<ApplicantDependencyValidationExcpetion>(() =>
            addApplicantTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertApplicantAsync(someApplicant),
                Times.Once);

            this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(
                expectedApplicantDependencyValidationExcpetion))),
                Times.Once);
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            //given
            Applicant someApplicant = CreateRandomApplicant();
            var serviceException = new Exception();

            var failedApplicantServiceException =
                new FailedApplicantServiceException(serviceException);

            var expectedApplicantServiceException =
                new ApplicantServiceException(failedApplicantServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertApplicantAsync(someApplicant))
                .ThrowsAsync(serviceException);

            //when
            ValueTask<Applicant> addApplicantTask =
                this.applicantService.AddApplicantAsync(someApplicant);

            //then
            await Assert.ThrowsAsync<ApplicantServiceException>(() =>
                addApplicantTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertApplicantAsync(someApplicant),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedApplicantServiceException))),
                    Times.Once);
        }
    }
}
