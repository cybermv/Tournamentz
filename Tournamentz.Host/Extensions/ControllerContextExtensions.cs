namespace Tournamentz.Host.Extensions
{
    using Controllers.Core;
    using System;
    using System.Web.Mvc;

    public static class ControllerContextExtensions
    {
        public static TournamentzControllerBase GetTournamentzController(this ControllerContext context)
        {
            TournamentzControllerBase controller = context.Controller as TournamentzControllerBase;

            if (controller == null)
            {
                throw new Exception(string.Format(
                    "The method GetTournamentzController can be used only for controllers deriving from {0}",
                    typeof(TournamentzControllerBase).Name));
            }

            return controller;
        }
    }
}