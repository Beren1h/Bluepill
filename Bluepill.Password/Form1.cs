using Bluepill.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BCrypt.Net;

namespace Bluepill.Password
{
    public partial class Form1 : Form
    {
        private const string CONNECTION = "mongodb://localhost";
        private const string DATABASE = "bluepill";

        private const int WORK_FACTOR = 12;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            var gateway = new UserGateway();

            var uid = txtUid.Text;
            var pwd = txtPwd.Text;

            var hash = BCrypt.Net.BCrypt.HashPassword(pwd, BCrypt.Net.BCrypt.GenerateSalt(WORK_FACTOR));

            gateway.SaveUser(new BluepillUser { Name = uid, Hash = hash,  WorkFactor = WORK_FACTOR });

            

        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            var gateway = new UserGateway();

            var uid = txtUid2.Text;
            var pwd = txtPwd2.Text;
            
            var user = gateway.GetUser(uid);

            if (user != null && BCrypt.Net.BCrypt.Verify(pwd, user.Hash))
            {
                lblPass.Visible = true;
                lblFail.Visible = false;
            }
            else
            {
                lblFail.Visible = true;
                lblPass.Visible = false;
            }
        }

        
    }
}
