using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_TestControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DataBind(); // Без него не се връзват функциите в ascx-а
    }

    protected void ASPxSpinEditDaysHours_Validation(object sender, DevExpress.Web.ASPxEditors.ValidationEventArgs e)
    {
        e.IsValid = ASPxTextBoxHours.Value != null || ASPxSpinEditDays.Value != null;
        e.ErrorText = "Моля, въведете планираното време!";
    }

    protected string GetDaysValidator()
    {
        string body = string.Format("e.isValid = {0}.GetValue() != null || {1}.GetValue() != null;" +
            "if (e.isValid && !{1}.GetValue())" +
                "ASPxClientEdit.ClearEditorsInContainerById('{2}');",
                ASPxSpinEditDays.ClientID, ASPxTextBoxHours.ClientID, hours.ClientID);
        return "function (s, e) {" + body + "}";
    }

    protected string GetHoursValidator()
    {
        string body =

            string.Format(
            "e.isValid = {0}.GetValue() != null || {1}.GetValue() != null;" +
            "if (e.isValid && !{0}.GetValue())" +
                "ASPxClientEdit.ClearEditorsInContainerById('{2}');",
                ASPxSpinEditDays.ClientID, ASPxTextBoxHours.ClientID, days.ClientID);
        return "function (s, e) {" + body + "}";
    }

    //function (s, e)
    //    {
    //        e.isValid = <%# ASPxSpinEditDays.ClientID %>.GetValue() != null || spinHours.GetValue() != null;
    //        if (e.isValid && !spinHours.GetValue()) {
    //            ASPxClientEdit.ClearEditorsInContainerById('hours');
    //        }
    //    }


    //function (s, e)
    //        {
    //            e.isValid = spinDays.GetValue() != null || spinHours.GetValue() != null;
    //            if (e.isValid && !spinDays.GetValue()) {
    //                ASPxClientEdit.ClearEditorsInContainerById('days');
    //            }
    //        }
}
