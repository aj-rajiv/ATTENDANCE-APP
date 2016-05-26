using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Net;


namespace attendance
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Window
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["attendance.Properties.Settings.attendanceConnectionString"].ConnectionString.ToString());
        public login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select count(*) from tl where  email='" + textBox1.Text + "' and password='" + passwordBox1.Password + "'", con);
          
            con.Open();
            int res = (int)cmd.ExecuteScalar();
            con.Close();
            if (res == 1)
            {
                this.Hide();
                profile pf = new profile();
                pf.Show();
            }
            else
            {
                MessageBox.Show("Invalid user");
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Text = "";
            passwordBox1.Password= "";
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            string password = "";
            SqlCommand cmd = new SqlCommand("select password from tl where  email='" + textBox3.Text + "'", con);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            {
              
                cmd.Parameters.AddWithValue("@email", textBox3.Text);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                {
                    if (rd.Read())
                    {
                        password = rd["password"].ToString();
                    }
                }
                con.Close();
            }
            if (!string.IsNullOrEmpty(password))
            {

                MailMessage msg = new MailMessage("admin@gmail.com", textBox3.Text);
                msg.Subject = string.Format("Hi {0},<br/><br/>Your password is {1}.<br/><br/>Thank You.", textBox3.Text, password);
                msg.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                NetworkCredential nc = new NetworkCredential("admin@gmail.com", "senderpassword");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = nc;
                smtp.Send(msg);

                MessageBox.Show("password sent to your email");

            }
            else
            {
                MessageBox.Show("email not registered with us");
            }
        }
    }
}
