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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;



namespace attendance
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["attendance.Properties.Settings.attendanceConnectionString"].ConnectionString.ToString());
        public MainWindow()
        {
            InitializeComponent();
            label7.Visibility = Visibility.Hidden;

            label8.Visibility = Visibility.Hidden;

            label9.Visibility = Visibility.Hidden;

            label10.Visibility = Visibility.Hidden;

            label11.Visibility = Visibility.Hidden;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string email = textBox3.Text;
            Regex rx = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match mc = rx.Match(email);
            if (textBox1.Text == "")
            {
                label7.Visibility = Visibility.Visible;
                label7.Content = "*please enter value";
            }
            else if (textBox2.Text == "")
            {
                label8.Visibility = Visibility.Visible;
                label8.Content = "*please enter value";
            }
            else if (textBox3.Text == "")
            {
                label9.Visibility = Visibility.Visible;
                label9.Content = "*please enter value";
            }
            else if (passwordBox1.Password == "")
            {
                label10.Visibility = Visibility.Visible;
                label10.Content = "*please enter value";
            }
            else if (passwordBox2.Password == "")
            {
                label11.Visibility = Visibility.Visible;
                label11.Content = "*please enter value";
            }
            else if (passwordBox2.Password != passwordBox1.Password)
            {
                label11.Visibility = Visibility.Visible;
                label11.Content = "*passwords does not match";
            }

            else if (!mc.Success)
            {

                label9.Visibility = Visibility.Visible;
                label9.Content = "invalid email format";

            }
            else
            {
                SqlCommand cmd = new SqlCommand("insert into tl(name,phone,email,password)values(@name,@phone,@email,@password)", con);
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@name", textBox1.Text);
                cmd.Parameters.AddWithValue("@phone", textBox2.Text);
                cmd.Parameters.AddWithValue("@email", textBox3.Text);
                //cmd.Parameters.AddWithValue("@password", passwordBox1.Password);
                cmd.Parameters.AddWithValue("@password", passwordBox2.Password);
                con.Open();
                DataSet ds = new DataSet();
                ad.Fill(ds);
                con.Close();
                MessageBox.Show("SUCCESS!!!");
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            login lg = new login();
            lg.Show();
        }
    }
}
