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
    public partial class profileview : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(@"server=DESKTOP-1E3KGVR\SQLEXPRESS;database=proj;integrated security=true");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string s = "select username,email_id,phn_no,country,image,datetime from tab1 where id=" + Session["uid"] + "";
                SqlCommand cm = new SqlCommand(s, con);
                con.Open();
                SqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    TextBox1.Text = dr["username"].ToString();
                    TextBox2.Text = dr["email_id"].ToString();
                    TextBox3.Text = dr["phn_no"].ToString();
                    TextBox4.Text = dr["country"].ToString();
                    Image1.ImageUrl = dr["image"].ToString();
                    TextBox5.Text = dr["datetime"].ToString();
                }
                con.Close();
                bind_grid();
            }

        }
        public void bind_grid()
        {
            string s = "select * from tab1 order by id desc";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(s, con);
            da.Fill(ds);
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GridViewRow rw = GridView1.Rows[e.NewSelectedIndex];
            TextBox1.Text = rw.Cells[4].Text;
            TextBox2.Text = rw.Cells[5].Text;
            TextBox3.Text = rw.Cells[6].Text;
            TextBox4.Text = rw.Cells[7].Text;
            Image1.ImageUrl = rw.Cells[8].Text;
            TextBox5.Text = rw.Cells[9].Text;
            
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int i = e.RowIndex;
            int uid = Convert.ToInt32(GridView1.DataKeys[i].Value);
            string del = "delete from tab1 where id=" + uid + "";
            SqlCommand cmd = new SqlCommand(del, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            bind_grid();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            bind_grid();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int i = e.RowIndex;
            int uid = Convert.ToInt32(GridView1.DataKeys[i].Value);
            TextBox txtusername = (TextBox)GridView1.Rows[i].Cells[4].Controls[0];
            TextBox txtemail_id = (TextBox)GridView1.Rows[i].Cells[5].Controls[0];
            TextBox txtphn_no = (TextBox)GridView1.Rows[i].Cells[6].Controls[0];
            TextBox txtcountry = (TextBox)GridView1.Rows[i].Cells[7].Controls[0];
            string strup = "update tab1 set username='" + txtusername.Text + "', email_id='"+txtemail_id.Text + "',phn_no='"+txtphn_no+"',country='"+txtcountry+"' where id=" + uid + "";
            SqlCommand cmd = new SqlCommand(strup, con);
            con.Open();
              cmd.ExecuteNonQuery();
            con.Close();
            GridView1.EditIndex = -1;
            bind_grid();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }

        
    }
}