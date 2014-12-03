using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ExtensionLibrary.MvcHelpers
{
    public class MvcHelpers
    {
        /// <summary>
        /// Return output when criteria are met
        /// </summary>
        /// <param name="value"></param>
        /// <param name="evaluation"></param>
        /// <returns></returns>
        /// <example>@Html.ActionLink("Create New", "Create").If(User.IsInRole("Administrators"))</example>
        public static MvcHtmlString If(this MvcHtmlString value, bool evaluation)
        {
            return evaluation ? value : MvcHtmlString.Empty;
        }
    }
}
