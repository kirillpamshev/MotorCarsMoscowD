using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Data.SqlClient;

namespace MotorCarsMoscowD
{
    public class clients
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
    public partial class Client : Window
    {
        private Window parent_window;
        private SqlConnection connection;
        private int id_sell_car;
        public Client(SqlConnection connection, Window parent_window, int id_sell_car)
        {
            InitializeComponent();
            this.id_sell_car = id_sell_car;
            this.Show();
            this.parent_window = parent_window;
            this.connection = connection;
            parent_window.Visibility = Visibility.Hidden;
        }


        void checked_new_client(object sender, EventArgs e)
        {
            grid_clients.Visibility = Visibility.Hidden;
            login_lab.Visibility = Visibility.Visible;
            surname_lab.Visibility = Visibility.Visible;
            name_lab.Visibility = Visibility.Visible;
            middle_name_lab.Visibility = Visibility.Visible;
            dob_lab.Visibility = Visibility.Visible;
            phone_lab.Visibility = Visibility.Visible;
            passport_lab.Visibility = Visibility.Visible;
            email_lab.Visibility = Visibility.Visible;
            gender_lab.Visibility = Visibility.Visible;
            login.Visibility = Visibility.Visible;
            surname.Visibility = Visibility.Visible;
            first_name.Visibility = Visibility.Visible;
            middle_name.Visibility = Visibility.Visible;
            dob.Visibility = Visibility.Visible;
            phone.Visibility = Visibility.Visible;
            passport.Visibility = Visibility.Visible;
            email.Visibility = Visibility.Visible;
            m_gender.Visibility = Visibility.Visible;
            w_gender.Visibility = Visibility.Visible;



        }
        void checked_exist_client(object sender, EventArgs e)
        {
            grid_clients.Visibility = Visibility.Visible;
            List<clients> clients_list = new List<clients>();

            string sqlExpression = "SELECT id_client, last_name_client, first_name_client, middle_name_client, date_birthday_client, "
                + " phone_client, passport_client,"
                + " email_client FROM dbo.CLIENTS";


            SqlCommand command = new SqlCommand(sqlExpression, connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {

                while (reader.Read())
                {

                    clients st_rec = new clients();

                    st_rec.LOGIN = reader.GetString(0);
                    st_rec.LAST_NAME = reader.GetString(1);
                    st_rec.FIRST_NAME = reader.GetString(2);
                    st_rec.MIDDLE_NAME = reader.GetString(3);
                    st_rec.DOB = (reader.GetDateTime(4)).ToString("dd.MM.yyyy");
                    st_rec.PHONE = reader.GetString(5);
                    st_rec.PASSPORT = reader.GetString(6);
                    st_rec.EMAIL = reader.GetString(7);
                    clients_list.Add(st_rec);


                }
                reader.Close();

                grid_clients.ItemsSource = clients_list;
                grid_clients.Columns[0].Header = "Логин";
                grid_clients.Columns[1].Header = "Фамилия";
                grid_clients.Columns[2].Header = "Имя";
                grid_clients.Columns[3].Header = "Отчество";
                grid_clients.Columns[4].Header = "Дата рождения";
                grid_clients.Columns[5].Header = "Телефон";
                grid_clients.Columns[6].Header = "Паспорт";
                grid_clients.Columns[6].Header = "Почта";

            }
            else
            {
                MessageBox.Show("Строк не найдено");
                reader.Close();

            }

        }



        private void sell(object sender, RoutedEventArgs e)
        {
            string id_client;
            DateTime local_time = DateTime.Now;
            string order = local_time.ToString("yyyyMMddhhmm") + this.id_sell_car;
            int vehicle = this.id_sell_car;
            string employee = Manager.ID_SELLER;
            DateTime date = DateTime.Now;
            if ((bool)exist_client.IsChecked)
            {
                clients buyer = (clients)grid_clients.SelectedItem;
                if (buyer == null)
                {
                    return;
                }
                id_client = buyer.LOGIN;
            }
            else
            {
                login_er.Visibility = Visibility.Hidden;
                surname_er.Visibility = Visibility.Hidden;
                first_name_er.Visibility = Visibility.Hidden;
                middle_name_er.Visibility = Visibility.Hidden;
                dob_er.Visibility = Visibility.Hidden;
                phone_er.Visibility = Visibility.Hidden;
                passport_er.Visibility = Visibility.Hidden;
                email_er.Visibility = Visibility.Hidden;

                bool er = false;
                string error_msg = "";
                if ((error_msg = AddEmployee.check_login(login.Text)) != "")
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

                if ((error_msg = AddEmployee.check_dob(dob.DisplayDate)) != "")
                {
                    er = true;
                    dob_er.Content = error_msg;
                    dob_er.Visibility = Visibility.Visible;
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
                    string login_trm = login.Text.Trim();
                    string surname_trm = surname.Text.Trim();
                    string name_trm = first_name.Text.Trim();
                    string middle_name_trm = middle_name.Text.Trim();
                    DateTime dob_trm = dob.DisplayDate;
                    string phone_trm = phone.Text.Trim();
                    string passport_trm = passport.Text.Trim();
                    string email_trm = email.Text.Trim();
                    char gender = ((bool)m_gender.IsChecked) ? 'м' : 'ж';
                    id_client = login_trm;


                    try
                    {
                        string sqlExpression = " INSERT INTO CLIENTS VALUES (@login_value, @surname_value, @name_value, @middle_name_value, @dob_value, @phone_value, @passport_value, @email_value, @gender_value) ";
                        SqlCommand command = new SqlCommand(sqlExpression, connection);

                        SqlParameter login_param = new SqlParameter("@login_value", login_trm);
                        command.Parameters.Add(login_param);
                        SqlParameter surname_param = new SqlParameter("@surname_value", surname_trm);
                        command.Parameters.Add(surname_param);
                        SqlParameter name_param = new SqlParameter("@name_value", name_trm);
                        command.Parameters.Add(name_param);
                        SqlParameter middle_name_param = new SqlParameter("@middle_name_value", middle_name_trm);
                        command.Parameters.Add(middle_name_param);
                        SqlParameter dob_param = new SqlParameter("@dob_value", dob_trm);
                        command.Parameters.Add(dob_param);
                        SqlParameter phone_param = new SqlParameter("@phone_value", phone_trm);
                        command.Parameters.Add(phone_param);
                        SqlParameter passport_param = new SqlParameter("@passport_value", passport_trm);
                        command.Parameters.Add(passport_param);
                        SqlParameter email_param = new SqlParameter("@email_value", email_trm);
                        command.Parameters.Add(email_param);
                        SqlParameter gender_param = new SqlParameter("@gender_value", gender);
                        command.Parameters.Add(gender_param);
                        command.ExecuteNonQuery();

                        sqlExpression = " INSERT INTO ORDERS VALUES (@id_order, @id_vehicle, @id_client, @id_employee, @current_date) ";
                        command = new SqlCommand(sqlExpression, connection);
                        SqlParameter id_order_par = new SqlParameter("@id_order", order);
                        command.Parameters.Add(id_order_par);
                        SqlParameter id_vehicle_par = new SqlParameter("@id_vehicle", vehicle);
                        command.Parameters.Add(id_vehicle_par);
                        SqlParameter id_client_par = new SqlParameter("@id_client", id_client);
                        command.Parameters.Add(id_client_par);
                        SqlParameter id_employee_par = new SqlParameter("@id_employee", employee);
                        command.Parameters.Add(id_employee_par);
                        SqlParameter current_date_par = new SqlParameter("@current_date", date);
                        command.Parameters.Add(current_date_par);
                        command.ExecuteNonQuery();

                        sqlExpression = " UPDATE VEHICLES SET id_status = @id_status WHERE id_vehicle = @id_vehicle; ";
                        command = new SqlCommand(sqlExpression, connection);
                        SqlParameter id_status_par = new SqlParameter("@id_status", 'S');
                        command.Parameters.Add(id_status_par);
                        id_vehicle_par = new SqlParameter("@id_vehicle", vehicle);
                        command.Parameters.Add(id_vehicle_par);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Автомобиль продан!");
                        ((SellCar)parent_window).update_sell_car();
                        parent_window.Visibility = Visibility.Visible;
                        this.Close();
                    }

                    catch (SqlException en)
                    {
                        MessageBox.Show((en.Number).ToString() + " " + en.Message);
                    }




                }
                return;
            }

            try
            {

                string sqlExpression = " INSERT INTO ORDERS VALUES (@id_order, @id_vehicle, @id_client, @id_employee, @current_date) ";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter id_order_par = new SqlParameter("@id_order", order);
                command.Parameters.Add(id_order_par);
                SqlParameter id_vehicle_par = new SqlParameter("@id_vehicle", vehicle);
                command.Parameters.Add(id_vehicle_par);
                SqlParameter id_client_par = new SqlParameter("@id_client", id_client);
                command.Parameters.Add(id_client_par);
                SqlParameter id_employee_par = new SqlParameter("@id_employee", employee);
                command.Parameters.Add(id_employee_par);
                SqlParameter current_date_par = new SqlParameter("@current_date", date);
                command.Parameters.Add(current_date_par);
                command.ExecuteNonQuery();

                sqlExpression = " UPDATE VEHICLES SET id_status = @id_status WHERE id_vehicle = @id_vehicle; ";
                command = new SqlCommand(sqlExpression, connection);
                SqlParameter id_status_par = new SqlParameter("@id_status", 'S');
                command.Parameters.Add(id_status_par);
                id_vehicle_par = new SqlParameter("@id_vehicle", vehicle);
                command.Parameters.Add(id_vehicle_par);
                command.ExecuteNonQuery();
                MessageBox.Show("Автомобиль продан!");
                string file_name = order + ".txt";
                StreamWriter file = new StreamWriter(order);
                file.WriteLine("Номер заказа: " + order);
                file.WriteLine("Дата заказа: " + date);
                file.Close();

                ((SellCar)parent_window).update_sell_car();
                parent_window.Visibility = Visibility.Visible;
                this.Close();

            }

            catch (SqlException en)
            {
                MessageBox.Show((en.Number).ToString() + " " + en.Message);
            }


        }

        private void goBack(object sender, RoutedEventArgs e)
        {
            parent_window.Visibility = Visibility.Visible;
            this.Close();

        }


    }
}
