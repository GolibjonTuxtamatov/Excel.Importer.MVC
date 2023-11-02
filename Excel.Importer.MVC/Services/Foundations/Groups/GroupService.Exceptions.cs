//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using System.Linq;
using System.Threading.Tasks;
using EFxceptions.Models.Exceptions;
using Excel.Importer.MVC.Models.Foundations.Groups;
using Excel.Importer.MVC.Services.Foundations.Groups.Exceptions;
using Microsoft.Data.SqlClient;
using Xeptions;

namespace Excel.Importer.MVC.Services.Foundations.Groups
{
    public partial class GroupService
    {
        private delegate ValueTask<Group> ReturningGroupFunctuion();
        private delegate IQueryable<Group> ReturningGroupsFunction();

        private async ValueTask<Group> TryCatch(ReturningGroupFunctuion returningGroupFunctuion)
        {
            try
            {
                return await returningGroupFunctuion();
            }
            catch (NullGroupException nullGroupException)
            {
                throw CreateAndLogValidationException(nullGroupException);
            }
            catch (InvalidGroupException invalidGroupException)
            {
                throw CreateAndLogValidationException(invalidGroupException);
            }
            catch (SqlException sqlException)
            {
                var faildStorageGroupException =
                    new FaildStorageGroupException(sqlException);

                throw CreatAndLogCriticalDependencyException(faildStorageGroupException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistGroupException =
                    new AlreadyExistGroupException(duplicateKeyException);

                throw CreateAndLogDependencyValidationException(alreadyExistGroupException);
            }
            catch (Exception exception)
            {
                var failedServiceException = new FailedServiceException(exception);

                throw CreateAndloGServiceException(failedServiceException);
            }
        }

        private IQueryable<Group> TryCatch(ReturningGroupsFunction returningGroupsFunction)
        {
            try
            {
                return returningGroupsFunction();
            }
            catch (SqlException sqlException)
            {
                var faildStorageGroupException =
                    new FaildStorageGroupException(sqlException);

                throw CreatAndLogCriticalDependencyException(faildStorageGroupException);
            }
            catch (Exception exception)
            {
                var failedServiceException = new FailedServiceException(exception);

                throw CreateAndloGServiceException(failedServiceException);
            }
        }

        private GroupValidationException CreateAndLogValidationException(Xeption exception)
        {
            var groupValidationException =
                    new GroupValidationException(exception);

            this.loggingBroker.LogError(groupValidationException);

            return groupValidationException;
        }

        private GroupDependencyException CreatAndLogCriticalDependencyException(Xeption exception)
        {
            GroupDependencyException groupDependencyException =
                new GroupDependencyException(exception);

            this.loggingBroker.LogCritical(groupDependencyException);

            return groupDependencyException;
        }

        private GroupDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var groupDependencyValidationException = new GroupDependencyValidationException(exception);
            this.loggingBroker.LogError(groupDependencyValidationException);

            return groupDependencyValidationException;
        }

        private GroupServiceException CreateAndloGServiceException(Xeption exception)
        {
            var groupServiceException = new GroupServiceException(exception);
            this.loggingBroker.LogError(groupServiceException);

            return groupServiceException;
        }
    }
}
