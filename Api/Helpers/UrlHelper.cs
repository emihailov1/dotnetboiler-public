using BusinessLogic.Services.Helpers;

namespace Api.Helpers
{
    public class UrlHelper : IUrlHelper
    {
        private Microsoft.AspNetCore.Mvc.IUrlHelper urlHelper;

        public UrlHelper(Microsoft.AspNetCore.Mvc.IUrlHelper urlHelper)
        {
            this.urlHelper = urlHelper;
        }
        public string CreateUserResourceUri(string uri, ResourceUriType type, ResourceParameters parameters)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    parameters.PageNumber--;
                    return urlHelper.Link(uri,
                      parameters
                      );
                case ResourceUriType.NextPage:
                    parameters.PageNumber++;
                    return urlHelper.Link(uri,
                      parameters
                      );
                default:
                    return urlHelper.Link(uri,
                      parameters
                      );
            }
        }
    }

    public interface IUrlHelper
    {
        string CreateUserResourceUri(string uri, ResourceUriType type, ResourceParameters parameters);
    }
}
