using DevExpress.Web.ASPxEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FOD_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ASPxButton1_Click(object sender, EventArgs e)
    {
        if (ASPxEdit.AreEditorsValid(this, "vgNewTask"))
            ASPxLabel1.Visible = true;
    }
}
