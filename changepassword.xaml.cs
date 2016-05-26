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

namespace attendance
{
    /// <summary>
    /// Interaction logic for changepassword.xaml
    /// </summary>
    public partial class changepassword : Window
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["attendance.Properties.Settings.attendanceConnectionString"].ConnectionString.ToString());
        public changepassword()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            profile pf = new profile();
            pf.Show();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand("update tl set password='" + passwordBox3.Password + "' where password='" + passwordBox1.Password + "'", con);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            
            cmd.Parameters.AddWithValue("@password",passwordBox1.Password);
           
            cmd.Parameters.AddWithValue("@password", passwordBox3.Password);
            con.Open();
            DataSet ds = new DataSet();
            ad.Fill(ds);
            con.Close();
            MessageBox.Show("password changed successfully");
        }
    }
}
