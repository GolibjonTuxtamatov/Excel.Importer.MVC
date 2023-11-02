using Xeptions;

namespace Excel.Importer.MVC.Services.Foundations.Groups.Exceptions
{
    public class GroupServiceException : Xeption
    {
        public GroupServiceException(Xeption innerException)
            : base("Group service error occured, contact support",
                 innerException)
        { }
    }
}
