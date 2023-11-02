//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System.Linq;
using System.Threading.Tasks;
using Excel.Importer.MVC.Models.Foundations.Groups;
using Excel.Importer.MVC.Services.Foundations.Groups.Exceptions;
using Excel.Importer.MVC.Services.Proccessings.Groups.Exceptions;
using Xeptions;

namespace Excel.Importer.MVC.Services.Proccessings.Groups
{
    public partial class GroupProccessingService
    {
        private delegate ValueTask<Group> ReturningGroupFunction();
        private delegate IQueryable<Group> ReturningGroupsFunction();

        private async ValueTask<Group> TryCatch(ReturningGroupFunction returningGroupFunction)
        {
            try
            {
                return await returningGroupFunction();
            }
            catch (GroupValidationException groupValidationException)
            {
                throw CreateAndLogProccessingValidationException(groupValidationException);
            }
            catch (GroupDependencyException groupDependencyException)
            {
                throw CreateAndLogProccessingDependencyException(groupDependencyException);
            }
            catch (GroupDependencyValidationException groupDependencyValidationException)
            {
                throw CreateAndLogProccessingDependencyValidationException(groupDependencyValidationException);
            }
            catch (GroupServiceException groupServiceException)
            {
                throw CreateAndLogProccessingServiceException(groupServiceException);
            }
        }

        private IQueryable<Group> TryCatch(ReturningGroupsFunction returningGroupsFunction)
        {
            try
            {
                return returningGroupsFunction();
            }
            catch (GroupDependencyException groupdDependencyException)
            {
                throw CreateAndLogProccessingDependencyException(groupdDependencyException);
            }
            catch (GroupServiceException groupServiceException)
            {
                throw CreateAndLogProccessingServiceException(groupServiceException);
            }
        }

        private GroupProccessingValidationException CreateAndLogProccessingValidationException(Xeption exception)
        {
            var groupProccessingValidationException =
                new GroupProccessingValidationException(exception.InnerException as Xeption);

            this.loggingBroker.LogError(groupProccessingValidationException);

            return groupProccessingValidationException;
        }

        private GroupProccessingDepedencyException CreateAndLogProccessingDependencyException(Xeption exception)
        {
            var groupProccessingDepedencyException =
                new GroupProccessingDepedencyException(exception.InnerException as Xeption);

            this.loggingBroker.LogCritical(groupProccessingDepedencyException);

            return groupProccessingDepedencyException;
        }

        private GroupProccessingDependencyValidationException CreateAndLogProccessingDependencyValidationException(
            Xeption exception)
        {
            var groupProccessingDependencyValidationException =
                new GroupProccessingDependencyValidationException(exception.InnerException as Xeption);

            this.loggingBroker.LogError(groupProccessingDependencyValidationException);

            return groupProccessingDependencyValidationException;
        }

        private GroupProccessingServiceException CreateAndLogProccessingServiceException(Xeption exception)
        {
            var groupProccessingServiceException =
                new GroupProccessingServiceException(exception.InnerException as Xeption);

            this.loggingBroker.LogError(groupProccessingServiceException);

            return groupProccessingServiceException;
        }
    }
}
