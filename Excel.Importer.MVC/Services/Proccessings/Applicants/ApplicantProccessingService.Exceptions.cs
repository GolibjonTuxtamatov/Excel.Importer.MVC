//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System.Threading.Tasks;
using Excel.Importer.MVC.Models.Foundations.Applicants;
using Excel.Importer.MVC.Models.Foundations.Applicants.Exceptions;
using Xeptions;

namespace Excel.Importer.MVC.Services.Proccessings.Applicants
{
    public partial class ApplicantProccessingService
    {
        private delegate ValueTask<Applicant> ReturningApplicantFunction();

        private async ValueTask<Applicant> TryCatch(ReturningApplicantFunction returningApplicantFunction)
        {
            try
            {
                return await returningApplicantFunction();
            }
            catch (ApplicantValidationException applicantValidationException)
            {
                throw CreateAndLogProccessingValidationException(applicantValidationException);
            }
            catch (ApplicantDependencyExcpetion applicantDependencyExcpetion)
            {
                throw CreateAndLogProccessingDependencyException(applicantDependencyExcpetion);
            }
            catch (ApplicantDependencyValidationExcpetion applicantDependencyValidationException)
            {
                throw CreateAndLogProccessingDependencyValidationException(applicantDependencyValidationException);
            }
            catch (ApplicantServiceException groupServiceException)
            {
                throw CreateAndLogProccessingServiceException(groupServiceException);
            }
        }

        private ApplicantProccessingValidationException CreateAndLogProccessingValidationException(Xeption exception)
        {
            var applicantProccessingValidationException =
                new ApplicantProccessingValidationException(exception.InnerException as Xeption);

            this.loggingBroker.LogError(applicantProccessingValidationException);

            return applicantProccessingValidationException;
        }

        private ApplicantProccessingDependencyException CreateAndLogProccessingDependencyException(Xeption exception)
        {
            var applicantProccessingDepedencyException =
                new ApplicantProccessingDependencyException(exception.InnerException as Xeption);

            this.loggingBroker.LogCritical(applicantProccessingDepedencyException);

            return applicantProccessingDepedencyException;
        }

        private ApplicantProccessingDependencyValidationException CreateAndLogProccessingDependencyValidationException(Xeption exception)
        {
            var applicantProccessingDependencyValidationException =
                new ApplicantProccessingDependencyValidationException(exception.InnerException as Xeption);

            this.loggingBroker.LogError(applicantProccessingDependencyValidationException);

            return applicantProccessingDependencyValidationException;
        }

        private ApplicantProccessingServiceException CreateAndLogProccessingServiceException(Xeption exception)
        {
            var applicantProccessingServiceException =
                new ApplicantProccessingServiceException(exception.InnerException as Xeption);

            this.loggingBroker.LogError(applicantProccessingServiceException);

            return applicantProccessingServiceException;
        }
    }
}
