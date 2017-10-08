using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChallengeEpicSpy
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Let's create some viewstate values:
            if (!Page.IsPostBack)
            {
                ViewState.Add("AgentName", ""); //Empty string
                ViewState.Add("Elections", 0.00);  //Starting with zero
                ViewState.Add("Subterfuge", 0.00);  //Starting with zero
                ViewState.Add("Count", 0);  //Counts the assets
                ResultLabel.Text = ""; //Clear the result label to start
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //ResultLabel.Text = ""; //Clear the result label for each new agent
            //Check first to make sure we have an agent name in the box...
            int Elections;
            int Subterfuge;
            int AgentCount;

            if (TextBox1.Text.Length > 0)
            {
                string AgentName = ViewState["AgentName"].ToString();
 
                //check to see if we have values in the box: Not checking for NaN
                if (TextBox2.Text.Length > 0 && TextBox3.Text.Length > 0)
                {
                    //Let's make sure we don't double (or triple or...) count the last agent

                    if (TextBox1.Text.ToString() != AgentName)  //Nothing happens unless this name is different from last
                    {
                        AgentCount = Int16.Parse(ViewState["Count"].ToString()) + 1; //increment asset count
                        ViewState["Count"] = AgentCount;  //Save new asset count
                        ViewState["AgentName"] = TextBox1.Text.ToString();
                        Elections = Int16.Parse(ViewState["Elections"].ToString());
                        Elections += Int16.Parse(TextBox2.Text.ToString());
                        ViewState["Elections"] = Elections;
                        Subterfuge = Int16.Parse(ViewState["Subterfuge"].ToString());
                        Subterfuge += Int16.Parse(TextBox3.Text.ToString());
                        ViewState["Subterfuge"] = Subterfuge;

                        double AvgSubterfuge = (Subterfuge / (double)AgentCount);

                        ResultLabel.Text = "Total Elections Rigged: " + Elections.ToString() + "<br/>";
                        ResultLabel.Text += String.Format("Average Acts of Subterfuge per Asset: {0:0.00}", AvgSubterfuge) + "<br/>";
                        ResultLabel.Text += "(Last Asset you Added: " + TextBox1.Text.ToString() + ")";
                    }
                }
            }
            else
            {
                ResultLabel.Text = "Error: You must enter an Asset Name.";

            }

        }
    }
}