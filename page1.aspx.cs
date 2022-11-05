using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace project
{
    public partial class page1 : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(@"server=DESKTOP-1E3KGVR\SQLEXPRESS;database=proj;integrated security=true");
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            Label2.Text = System.DateTime.Now.ToString();
           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Label1.Visible = true; 
            string q = "~/PHS/" + FileUpload1.FileName;
            FileUpload1.SaveAs(MapPath(q));
            string str = "insert into tab1 values('" + TextBox1.Text + "','" + TextBox2.Text + "'," + TextBox3.Text + ",'" + TextBox4.Text + "','" + q + "','"+Label2.Text+"')";
            SqlCommand cmd = new SqlCommand(str,con);
            con.Open();
            int x = cmd.ExecuteNonQuery();
            con.Close();
            if (x != 0)
            {
                Label1.Text = "registered";
                Response.Redirect("login.aspx");
            }
            else
            {
                Label1.Text = "failed";
            }
        }
    }
}