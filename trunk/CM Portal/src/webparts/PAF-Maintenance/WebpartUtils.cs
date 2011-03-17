using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace PAFMAintenance
{
    class WebpartUtils
    {

        // Summary:
        //     Checks if the required fields are not leaved blank by the user.
        //
        // Params:
        //     mandatoryFields :  List<TextBox>
        // Returns:
        //     TRUE if the fields are OK, false otherwise.
        public static Boolean checkMandatoryFields(LinkedList<TextBox> mandatoryFields)
        {
            bool retour = true;

            foreach (TextBox tb in mandatoryFields)
            {
                if (tb.Text == "")
                {
                    retour = false;
                }
            }

            return retour;
        }

        // Summary:
        //     Put all fields to a blank value.
        //
        // Params:
        //     allFields :  List<TextBox>
        public static void resetFields(LinkedList<TextBox> allFields)
        {
            foreach (TextBox tb in allFields)
            {
                tb.Text = "";
            }
        }
    }
}
