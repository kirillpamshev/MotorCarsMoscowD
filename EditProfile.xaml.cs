using System.Windows;
using System.Data.SqlClient;

namespace MotorCarsMoscowD
{

    public partial class EditProfile : Window
    {

        private Window parent_window;
        private SqlConnection connection;

        public EditProfile(SqlConnection connection, Window parent_window)
        {
            InitializeComponent();
            this.Show();
            this.parent_window = parent_window;
            this.connection = connection;
            parent_window.Visibility = Visibility.Hidden;
            string sqlExpression = " SELECT dbo.EMPLOYEES.id_employee, dbo.EMPLOYEES.last_name_employee, dbo.EMPLOYEES.first_name_employee, dbo.EMPLOYEES.middle_name_employee, dbo.EMPLOYEES.phone_employee, dbo.EMPLOYEES.passport_employee, "
                     + " dbo.EMPLOYEES.email_employee, dbo.USERS.pass_word, dbo.USERS.id_role "
                     + " FROM            dbo.USERS INNER JOIN "
                     + "  dbo.ROLES ON dbo.USERS.id_role = dbo.ROLES.id_role INNER JOIN "
                     + " dbo.EMPLOYEES ON dbo.USERS.id_user = dbo.EMPLOYEES.id_employee "
                     + " WHERE(dbo.USERS.id_role = 1)";

            try
            {
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    login_text.Text = reader.GetString(0);
                    surname.Text = reader.GetString(1);
                    first_name.Text = reader.GetString(2);
                    middle_name.Text = reader.GetString(3);
                    phone.Text = reader.GetString(4);
                    passport.Text = reader.GetString(5);
                    email.Text = reader.GetString(6);
                    reader.Close();



                }

                else
                {

                    reader.Close();


                }
            }

            catch (SqlException e)
            {
                MessageBox.Show("Ошибка" + e.ToString());
                this.Close();
            }




        }

        private void back_click(object sender, RoutedEventArgs e)
        {
            parent_window.Visibility = Visibility.Visible;
            this.Close();
        }

        private void update_click(object sender, RoutedEventArgs e)
        {

            
                login_er.Visibility = Visibility.Hidden;
                surname_er.Visibility = Visibility.Hidden;
                first_name_er.Visibility = Visibility.Hidden;
                middle_name_er.Visibility = Visibility.Hidden;
                phone_er.Visibility = Visibility.Hidden;
                passport_er.Visibility = Visibility.Hidden;
                email_er.Visibility = Visibility.Hidden;
                bool er = false;
                string error_msg = "";
                if ((error_msg = AddEmployee.check_login(login_text.Text)) != "")
                {
                    er = true;
                    login_er.Content = error_msg;
                    login_er.Visibility = Visibility.Visible;
                }
                if ((error_msg = AddEmployee.check_surname(surname.Text)) != "")
                {
                    er = true;
                    surname_er.Content = error_msg;
                    surname_er.Visibility = Visibility.Visible;
                }
                if ((error_msg = AddEmployee.check_name(first_name.Text)) != "")
                {
                    er = true;
                    first_name_er.Content = error_msg;
                    first_name_er.Visibility = Visibility.Visible;
                }
                if ((error_msg = AddEmployee.check_name(middle_name.Text)) != "")
                {
                    er = true;
                    middle_name_er.Content = error_msg;
                    middle_name_er.Visibility = Visibility.Visible;
                }

                if ((error_msg = AddEmployee.check_phone(phone.Text)) != "")
                {
                    er = true;
                    phone_er.Content = error_msg;
                    phone_er.Visibility = Visibility.Visible;
                }

                if ((error_msg = AddEmployee.check_passport(passport.Text)) != "")
                {
                    er = true;
                    passport_er.Content = error_msg;
                    passport_er.Visibility = Visibility.Visible;
                }

                if ((error_msg = AddEmployee.check_email(email.Text)) != "")
                {
                    er = true;
                    email_er.Content = error_msg;
                    email_er.Visibility = Visibility.Visible;
                }


                if (!er)
                {
                    string login_trm = login_text.Text.Trim();
                    string surname_trm = surname.Text.Trim();
                    string name_trm = first_name.Text.Trim();
                    string middle_name_trm = middle_name.Text.Trim();
                    string phone_trm = phone.Text.Trim();
                    string passport_trm = passport.Text.Trim();
                    string email_trm = email.Text.Trim();


                    try
                    {
                        string sqlExpression = " UPDATE dbo.EMPLOYEES SET dbo.EMPLOYEES.id_employee = @login_value, dbo.EMPLOYEES.last_name_employee = @surname_value, dbo.EMPLOYEES.first_name_employee = @name_value, "
                        + " dbo.EMPLOYEES.middle_name_employee = @middle_name_value, dbo.EMPLOYEES.phone_employee = @phone_value, dbo.EMPLOYEES.passport_employee = @passport_value, "
                        + " dbo.EMPLOYEES.email_employee = @email_value "
                        + " FROM dbo.EMPLOYEES INNER JOIN"
                        + " dbo.USERS ON dbo.EMPLOYEES.id_employee = dbo.USERS.id_user"
                        + " WHERE(dbo.USERS.id_role = 1) ";
                        SqlCommand command = new SqlCommand(sqlExpression, connection);
                        SqlParameter login_param = new SqlParameter("@login_value", login_trm);
                        command.Parameters.Add(login_param);
                        SqlParameter surname_param = new SqlParameter("@surname_value", surname_trm);
                        command.Parameters.Add(surname_param);
                        SqlParameter name_param = new SqlParameter("@name_value", name_trm);
                        command.Parameters.Add(name_param);
                        SqlParameter middle_name_param = new SqlParameter("@middle_name_value", middle_name_trm);
                        command.Parameters.Add(middle_name_param);
                        SqlParameter phone_param = new SqlParameter("@phone_value", phone_trm);
                        command.Parameters.Add(phone_param);
                        SqlParameter passport_param = new SqlParameter("@passport_value", passport_trm);
                        command.Parameters.Add(passport_param);
                        SqlParameter email_param = new SqlParameter("@email_value", email_trm);
                        command.Parameters.Add(email_param);
                        command.ExecuteNonQuery();

                        /* sqlExpression = " UPDATE dbo.USERS SET dbo.USERS.pass_word =  @password_value ";
                         command = new SqlCommand(sqlExpression, connection);*/

                        /*SqlParameter password_param = new SqlParameter("@password_value", new_password_trm);
                        command.Parameters.Add(password_param);*/

                        //command.ExecuteNonQuery();

                        MessageBox.Show("Данные обновлены!");
                    }

                    catch (SqlException en)
                    {
                        MessageBox.Show((en.Number).ToString() + " " + en.Message);
                    }
                }
        }
    }
}

