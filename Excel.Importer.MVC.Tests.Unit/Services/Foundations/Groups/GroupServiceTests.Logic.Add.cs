//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Excel.Importer.MVC.Models.Foundations.Groups;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace Excel.Importer.MVC.Api.Tests.Unit.Services.Foundations.Groups
{
    public partial class GroupServiceTests
    {
        [Fact]
        public async Task ShouldAddGroupAsync()
        {
            //given
            Group randomGroup = CreateRandomGroup();
            Group inputGroup = randomGroup;
            Group storedGroup = inputGroup;
            Group expectedGroup = storedGroup.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertGroupAsync(inputGroup))
                .ReturnsAsync(expectedGroup);

            //when
            Group actualGroup = await this.groupService.AddGroupAsync(inputGroup);

            //then
            actualGroup.Should().BeEquivalentTo(expectedGroup);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertGroupAsync(inputGroup), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();

        }
    }
}
