//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Excel.Importer.MVC.Models.Foundations.Applicants;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace Excel.Importer.MVC.Api.Tests.Unit.Services.Foundations.Applicants
{
    public partial class ApplicantServiceTests
    {
        [Fact]
        public async Task ShouldAddApplicantAsync()
        {
            //given
            Applicant randomApplicant = CreateRandomApplicant();
            Applicant inputApplicant = randomApplicant;
            Applicant storedApplicant = inputApplicant;
            Applicant expectedApplicant = storedApplicant.DeepClone();

            this.storageBrokerMock.Setup(broker =>
            broker.InsertApplicantAsync(inputApplicant))
                .ReturnsAsync(storedApplicant);

            //when
            Applicant actualApplicant = await this.applicantService
                .AddApplicantAsync(inputApplicant);

            //then
            actualApplicant.Should().BeEquivalentTo(expectedApplicant);

            this.storageBrokerMock.Verify(broker =>
            broker.InsertApplicantAsync(inputApplicant), Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
