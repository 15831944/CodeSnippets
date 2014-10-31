using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using IntranetShared.DbContext.SmartIT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DevTasks_Test : System.Web.UI.Page
{
    SmartITEntities db = new SmartITEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            ASPxGridView1.DataBind();
    }

    protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        e.Result = db.tDevTasks
            .Select(a => new
            {
                a.ID,
                a.OrderID,
                a.Name,
                a.Status,
            });
    }



    protected void ASPxButton1_Init(object sender, EventArgs e)
    {
        ASPxButton button = sender as ASPxButton;
        GridViewDataItemTemplateContainer container = button.NamingContainer as GridViewDataItemTemplateContainer;
        button.ID = "ASPxButton1" + container.KeyValue;
    }

    protected void ASPxLabel1_Init(object sender, EventArgs e)
    {
        ASPxLabel button = sender as ASPxLabel;
        GridViewDataItemTemplateContainer container = button.NamingContainer as GridViewDataItemTemplateContainer;
        button.ID = "ASPxLabel1" + container.KeyValue;
    }

    protected void LinqServerModeDataSource1_Selecting(object sender, DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs e)
    {
        //e.KeyExpression = "ID";
        //e.QueryableSource = 
    }

    protected void ASPxGridView1_DataBinding(object sender, EventArgs e)
    {
        ASPxGridView1.DataSource = db.tDevTasks
            .Select(a => new
            {
                a.ID,
                a.OrderID,
                a.Name,
                a.Status,
            }).ToList();
    }
}
