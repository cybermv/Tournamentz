namespace Tournamentz.Host.Extensions
{
    using Api.Controllers.Core;
    using System;
    using System.Web.Http.Controllers;

    public static class HttpControllerContextExtensions
    {
        public static TournamentzApiControllerBase GetTournamentzController(this HttpControllerContext context)
        {
            TournamentzApiControllerBase controller = context.Controller as TournamentzApiControllerBase;

            if (controller == null)
            {
                throw new Exception(string.Format(
                    "The method GetTournamentzController can be used only for controllers deriving from {0}",
                    typeof(TournamentzApiControllerBase).Name));
            }

            return controller;
        }
    }
}