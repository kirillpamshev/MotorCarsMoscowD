using System.Windows;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace MotorCarsMoscowD
{
    public class editor_employee
    {
        public string LOGIN { get; set; }
        public string LAST_NAME { get; set; }
        public string FIRST_NAME { get; set; }
        public string MIDDLE_NAME { get; set; }
        public string DOB { get; set; }
        public string PHONE { get; set; }
        public string PASSPORT { get; set; }
        public string EMAIL { get; set; }

    }
    public partial class EditAllProfiles : Window
    {
        private Window parent_window;
        private SqlConnection connection;
        public EditAllProfiles(SqlConnection connection, Window parent_window, int id_role)
        {
            InitializeComponent();
            this.Show();
            this.parent_window = parent_window;
            this.connection = connection;
            parent_window.Visibility = Visibility.Hidden;
            update_employees();



        }


        public void update_employees() {
            List<editor_employee> editor_list = new List<editor_employee>();
            string sqlExpression = "SELECT dbo.EMPLOYEES.id_employee, dbo.EMPLOYEES.last_name_employee, dbo.EMPLOYEES.first_name_employee, dbo.EMPLOYEES.middle_name_employee, dbo.EMPLOYEES.date_birthday_employee, dbo.EMPLOYEES.phone_employee, "
               + " dbo.EMPLOYEES.passport_employee, dbo.EMPLOYEES.email_employee "
               + " FROM     dbo.EMPLOYEES INNER JOIN "
               + "  dbo.USERS ON dbo.EMPLOYEES.id_employee = dbo.USERS.id_user "
               + " WHERE(dbo.USERS.id_role <> 1) ";

            SqlCommand command = new SqlCommand(sqlExpression, connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {


                while (reader.Read())
                {

                    editor_employee st_rec = new editor_employee();

                    st_rec.LOGIN = reader.GetString(0);
                    st_rec.LAST_NAME = reader.GetString(1);
                    st_rec.FIRST_NAME = reader.GetString(2);
                    st_rec.MIDDLE_NAME = reader.GetString(3);
                    st_rec.DOB = (reader.GetDateTime(4)).ToString("dd.MM.yyyy");
                    st_rec.PHONE = reader.GetString(5);
                    st_rec.PASSPORT = reader.GetString(6);
                    st_rec.EMAIL = reader.GetString(7);
                    editor_list.Add(st_rec);



                }
                reader.Close();
                grid.ItemsSource = editor_list;
                grid.Columns[0].Header = "Логин";
                grid.Columns[1].Header = "Фамилия";
                grid.Columns[2].Header = "Имя";
                grid.Columns[3].Header = "Отчество";
                grid.Columns[4].Header = "Дата рождения";
                grid.Columns[5].Header = "Телефон";
                grid.Columns[6].Header = "Паспорт";
                grid.Columns[7].Header = "Почта";


            }
            else
            {

                reader.Close();
                grid.ItemsSource = null;
            }


        }



        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            parent_window.Visibility = Visibility.Visible;
            this.Close();

        }

        private void change(object sender, RoutedEventArgs e)
        {
            editor_employee id_employee = (editor_employee)grid.SelectedItem;
            if (id_employee != null)
            {
                string id_edit = id_employee.LOGIN;
                this.Visibility = Visibility.Hidden;
                new EditorEmp(connection, this, id_edit);

            }
            
        }

    }
}

