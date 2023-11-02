//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System.Linq;
using System.Threading.Tasks;
using Excel.Importer.MVC.Models.Foundations.Groups;
using Excel.Importer.MVC.Services.Orchestrations.Groups.Exceptions;
using Excel.Importer.MVC.Services.Proccessings.Groups.Exceptions;
using Xeptions;

namespace Excel.Importer.MVC.Services.Orchestrations.Groups
{
    public partial class GroupOrchestrationService
    {
        private delegate ValueTask<Group> ReturningGroupFunction();
        private delegate IQueryable<Group> ReturningGroupsFunction();

        private async ValueTask<Group> TryCatch(ReturningGroupFunction returningGroupFunction)
        {
            try
            {
                return await returningGroupFunction();
            }
            catch (GroupProccessingValidationException groupProccessingValidationException)
            {
                throw CreateAndLogOrchetrationValidationException(groupProccessingValidationException);
            }
            catch (GroupProccessingDepedencyException groupProccessingDependencyException)
            {
                throw CreateAndLogOrchetrationDependencyException(groupProccessingDependencyException);
            }
            catch (GroupProccessingDependencyValidationException groupProccessingDependencyValidationException)
            {
                throw CreateAndLogOrchetrationDependencyValidationException(
                    groupProccessingDependencyValidationException);
            }
            catch (GroupProccessingServiceException groupProccessingServiceException)
            {
                throw CreateAndLogOrchetrationServiceException(groupProccessingServiceException);
            }
        }

        private IQueryable<Group> TryCatch(ReturningGroupsFunction returningGroupsFunction)
        {
            try
            {
                return returningGroupsFunction();
            }
            catch (GroupProccessingDepedencyException groupProccessingDependencyException)
            {
                throw CreateAndLogOrchetrationDependencyException(groupProccessingDependencyException);
            }
            catch (GroupProccessingServiceException groupProccessingServiceException)
            {
                throw CreateAndLogOrchetrationServiceException(groupProccessingServiceException);
            }
        }

        private GroupOrchestrationValidationException CreateAndLogOrchetrationValidationException(Xeption exception)
        {
            var groupOrchestrationValidationException =
                new GroupOrchestrationValidationException(exception.InnerException as Xeption);

            this.loggingBroker.LogError(groupOrchestrationValidationException);

            return groupOrchestrationValidationException;
        }

        private GroupOrchestrationDependencyException CreateAndLogOrchetrationDependencyException(Xeption exception)
        {
            var groupOrchestrationDependencyException =
                new GroupOrchestrationDependencyException(exception.InnerException as Xeption);

            this.loggingBroker.LogCritical(groupOrchestrationDependencyException);

            return groupOrchestrationDependencyException;
        }

        private GroupOrchestratioDependencyValidationException CreateAndLogOrchetrationDependencyValidationException(
            Xeption exception)
        {
            var groupOrchestratioDependencyValidationException =
                new GroupOrchestratioDependencyValidationException(exception.InnerException as Xeption);

            this.loggingBroker.LogError(groupOrchestratioDependencyValidationException);

            return groupOrchestratioDependencyValidationException;
        }

        private GroupOrchestrationServiceException CreateAndLogOrchetrationServiceException(Xeption exception)
        {
            GroupOrchestrationServiceException groupOrchestrationServiceException =
                new GroupOrchestrationServiceException(exception.InnerException as Xeption);

            this.loggingBroker.LogError(groupOrchestrationServiceException);

            return groupOrchestrationServiceException;
        }
    }
}
