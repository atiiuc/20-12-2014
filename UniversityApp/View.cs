using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniversityApp
{
    public partial class View : Form
    {
        public View()
        {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            

            string connectionString = @"Data Source = (local)\SQLExpress; Database = UniversityDB; Integrated Security = true";

            //2.build a connection with connection string

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = "Select * from tStudents";
          
            string inputId = idText.Text;
            if (!string.IsNullOrEmpty(inputId))
            {
                 query = "Select * from tStudents where id='"+inputId+"'";

            }
           
              SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader areader = command.ExecuteReader();
            List<Student> studentList = new List<Student>();
            while (areader.Read())
            {
                Student student1 = new Student();
                student1.id = (int) areader["Id"];
                student1.name = (string)areader["Name"];
                student1.address = (string)areader["Adress"];
                student1.phone = (string)areader["PhoneNumber"];
                student1.email = (string)areader["EmailAdress"];
                studentList.Add(student1);

            }

            resultView.Items.Clear();
            foreach (var student in studentList)
            {
                ListViewItem li = new ListViewItem();
                li.Text = Convert.ToString(student.id);
                li.SubItems.Add(student.name);
                li.SubItems.Add(student.address);
                li.SubItems.Add(student.phone);
                li.SubItems.Add(student.email);

                li.Tag = student;
                resultView.Items.Add(li);
            }
           
            connection.Close();

        }

        private void resultView_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem selectedItem = resultView.SelectedItems[0];

            Student selectedStudent = (Student) selectedItem.Tag;

            idLabel.Text = selectedStudent.id.ToString();
            nameTextBox.Text = selectedStudent.name;
            emailTextBox.Text = selectedStudent.email;
            adressTextBox.Text = selectedStudent.address;
            phoneTextBox.Text = selectedStudent.phone;
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source = (local)\SQLExpress; Database = UniversityDB; Integrated Security = true";
            
            SqlConnection connection = new SqlConnection(connectionString);
            
            connection.Open();
            
            string query =
                "update tStudents set Name= '"+nameTextBox.Text+"', Adress = '"+adressTextBox.Text+"',  PhoneNumber = '"+phoneTextBox.Text+"', EmailAdress ='"+emailTextBox.Text+"' where Id = '"+idLabel.Text+"'";

            SqlCommand command = new SqlCommand(query, connection);

           int rowAffected = command.ExecuteNonQuery();
           
            connection.Close();

            if (rowAffected > 0)
            {
                MessageBox.Show("Successfully Saved");
            }
            else
            {
                MessageBox.Show("Error");
            }

        }
    }
}
