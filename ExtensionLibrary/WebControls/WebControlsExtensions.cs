using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace System.Web.UI.WebControls
{
    public static class WebControlsExtensions
    {
        private static bool TryResetSelectedIndex(this DropDownList ddl)
        {
            if (ddl.Items != null && ddl.Items.Count > 0)
            {
                ddl.SelectedIndex = 0;
                return true;
            }

            return false;
        }
    }
}
