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
    /// Interaction logic for profile.xaml
    /// </summary>
    public partial class profile : Window
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["attendance.Properties.Settings.attendanceConnectionString"].ConnectionString.ToString());
       
        public profile()
        {
            InitializeComponent();
            SqlCommand cmd = new SqlCommand("select name from emp", con);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            con.Open();
            DataSet ds = new DataSet();
            ad.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                listBox1.Items.Add(ds.Tables[0].Rows[i][1].ToString());
                
            }
            
            con.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            DateTime dt = calendar2.SelectedDate.Value;
            SqlCommand cmd = new SqlCommand("insert into emp(id,name,phone,email,doj)values(@id,@name,@phone,@email,@doj)", con);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@id", textBox1.Text);
            cmd.Parameters.AddWithValue("@name", textBox2.Text);
            cmd.Parameters.AddWithValue("@phone", textBox3.Text);
            cmd.Parameters.AddWithValue("@email", textBox4.Text);
            cmd.Parameters.AddWithValue("@doj",dt.ToShortDateString());
            con.Open();
            DataSet ds = new DataSet();
            ad.Fill(ds);
            con.Close();
            MessageBox.Show("success");
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            DateTime dt = calendar2.SelectedDate.Value;
            string value = "";
            if (checkBox1.IsChecked==true)
            {
                value = "y";
            }
            else
            {
                value = "n";
            }

            SqlCommand cmd = new SqlCommand("insert into att (name,status,date)values(@name,@status,@date)", con);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@name", listBox1.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@status",value);

            cmd.Parameters.AddWithValue("@date", dt.ToShortDateString());
            con.Open();
            DataSet ds = new DataSet();
            ad.Fill(ds);
            con.Close();
            MessageBox.Show("values entered");
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from emp", con);
            con.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            emp.DataContext = rd;
            con.Close();

        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from att", con);
            con.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            att.DataContext = rd;
            con.Close();
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from emp where name='" + textBox6.Text + "", con);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@name", textBox6.Text);
            con.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            en.DataContext = rd;
            con.Close();
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from att where name='" + textBox7.Text + "", con);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@name", textBox7.Text);
            con.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            an.DataContext = rd;
            con.Close();
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            DateTime dt = calendar2.SelectedDate.Value;
            SqlCommand cmd = new SqlCommand("select * from att where date='" +dt.ToShortDateString()+ "", con);
            SqlDataAdapter ad1 = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@date",dt.ToShortDateString());
            con.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            ad.DataContext = rd;
            con.Close();
        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mw = new MainWindow();
            mw.Show();
        }

        private void button10_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            changepassword cp = new changepassword();
            cp.Show();
        }


    }
}
