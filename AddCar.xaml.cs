using System;
using System.IO;
using System.Windows;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MotorCarsMoscowD
{
    public partial class AddCar : Window
    {
        private Window parent_window;
        private SqlConnection connection;
        private Dictionary <string, string> map;
        private Dictionary<string, string> map2;
        private Dictionary<string, string> map3;
        private Dictionary<string, string> map4;
        private Microsoft.Win32.OpenFileDialog dlg;
        private string cur_file_name;

        public AddCar(SqlConnection connection, Window parent_window)
        {
            InitializeComponent();
            this.Show();
            this.parent_window = parent_window;
            this.connection = connection;
            parent_window.Visibility = Visibility.Hidden;
            map = new Dictionary<string, string>();
            map2 = new Dictionary<string, string>();
            map3 = new Dictionary<string, string>();
            map4 = new Dictionary<string, string>();
            dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "images (.jpg)|*.jpg";

            type_engine_text.Items.Add("Бензиновый");
            type_engine_text.Items.Add("Дизельный");

            for (int i = 1; i <= 8; i++)
            {
                seat_count_text.Items.Add($"{i}");
            }

        }
        private void goBack(object sender, RoutedEventArgs e)
        {
            parent_window.Visibility = Visibility.Visible;
            this.Close();

        }
        
        private void brand(object sender, RoutedEventArgs e)
        {
            
            add_brand_but.Visibility = Visibility.Visible;
            add_body_but.Visibility = Visibility.Hidden;
            add_model_but.Visibility = Visibility.Hidden;
            add_engine_but.Visibility = Visibility.Hidden;
            add_color_but.Visibility = Visibility.Hidden;
            add_vehicle_but.Visibility = Visibility.Hidden;
            download.Visibility = Visibility.Hidden;
            path_photo.Visibility = Visibility.Hidden;
            lab_add_photo.Visibility = Visibility.Hidden;

            brand_id_lab.Visibility = Visibility.Visible;
            brand_id_text.Visibility = Visibility.Visible;
            brand_name_lab.Visibility = Visibility.Visible;
            brand_name_text.Visibility = Visibility.Visible;
            //скрыть все элементы
            seat_count_lab.Visibility = Visibility.Hidden;
            seat_count_text.Visibility = Visibility.Hidden;
            add_brand_lab.Visibility = Visibility.Hidden;
            add_brand_text.Visibility = Visibility.Hidden;
            add_model_lab.Visibility = Visibility.Hidden;
            add_model_text.Visibility = Visibility.Hidden;
            add_vin_lab.Visibility = Visibility.Hidden;
            add_vin_text.Visibility = Visibility.Hidden;
            add_year_lab.Visibility = Visibility.Hidden;
            add_year_text.Visibility = Visibility.Hidden;
            add_color_lab.Visibility = Visibility.Hidden;
            add_color_text.Visibility = Visibility.Hidden;
            add_price_lab.Visibility = Visibility.Hidden;
            add_price_text.Visibility = Visibility.Hidden;
            id_color_lab.Visibility = Visibility.Hidden;
            id_color_text.Visibility = Visibility.Hidden;
            name_color_lab.Visibility = Visibility.Hidden;
            name_color_text.Visibility = Visibility.Hidden;
            id_engine_lab.Visibility = Visibility.Hidden;
            id_engine_text.Visibility = Visibility.Hidden;
            type_engine_lab.Visibility = Visibility.Hidden;
            type_engine_text.Visibility = Visibility.Hidden;
            volume_lab.Visibility = Visibility.Hidden;
            volume_text.Visibility = Visibility.Hidden;
            power_lab.Visibility = Visibility.Hidden;
            power_text.Visibility = Visibility.Hidden;
            id_body_lab.Visibility = Visibility.Hidden;
            id_body_text.Visibility = Visibility.Hidden;
            type_body_lab.Visibility = Visibility.Hidden;
            type_body_text.Visibility = Visibility.Hidden;
            model_id_lab.Visibility = Visibility.Hidden;
            model_id_text.Visibility = Visibility.Hidden;
            model_name_lab.Visibility = Visibility.Hidden;
            model_name_text.Visibility = Visibility.Hidden;
            body_lab.Visibility = Visibility.Hidden;
            body_text.Visibility = Visibility.Hidden;
            brand_lab.Visibility = Visibility.Hidden;
            brand_text.Visibility = Visibility.Hidden;
            engine_lab.Visibility = Visibility.Hidden;
            engine_text.Visibility = Visibility.Hidden;
            gabarits_lab.Visibility = Visibility.Hidden;
            length_text.Visibility = Visibility.Hidden;
            width_text.Visibility = Visibility.Hidden;
            height_text.Visibility = Visibility.Hidden;
        }

        private void model(object sender, RoutedEventArgs e)
        {
            add_brand_but.Visibility = Visibility.Hidden;
            add_body_but.Visibility = Visibility.Hidden;
            add_model_but.Visibility = Visibility.Visible;
            add_engine_but.Visibility = Visibility.Hidden;
            add_color_but.Visibility = Visibility.Hidden;
            add_vehicle_but.Visibility = Visibility.Hidden;
            download.Visibility = Visibility.Hidden;
            path_photo.Visibility = Visibility.Hidden;
            lab_add_photo.Visibility = Visibility.Hidden;

            model_id_lab.Visibility = Visibility.Visible;
            model_id_text.Visibility = Visibility.Visible;
            model_name_lab.Visibility = Visibility.Visible;
            model_name_text.Visibility = Visibility.Visible;
            body_lab.Visibility = Visibility.Visible;
            body_text.Visibility = Visibility.Visible; // combo
            brand_lab.Visibility = Visibility.Visible;
            brand_text.Visibility = Visibility.Visible; //combo
            engine_lab.Visibility = Visibility.Visible;
            engine_text.Visibility = Visibility.Visible; //combo
            gabarits_lab.Visibility = Visibility.Visible;
            length_text.Visibility = Visibility.Visible;
            width_text.Visibility = Visibility.Visible;
            height_text.Visibility = Visibility.Visible;
            //скрыть все элементы
            seat_count_lab.Visibility = Visibility.Hidden;
            seat_count_text.Visibility = Visibility.Hidden;
            add_brand_lab.Visibility = Visibility.Hidden;
            add_brand_text.Visibility = Visibility.Hidden;
            add_model_lab.Visibility = Visibility.Hidden;
            add_model_text.Visibility = Visibility.Hidden;
            add_vin_lab.Visibility = Visibility.Hidden;
            add_vin_text.Visibility = Visibility.Hidden;
            add_year_lab.Visibility = Visibility.Hidden;
            add_year_text.Visibility = Visibility.Hidden;
            add_color_lab.Visibility = Visibility.Hidden;
            add_color_text.Visibility = Visibility.Hidden;
            add_price_lab.Visibility = Visibility.Hidden;
            add_price_text.Visibility = Visibility.Hidden;
            id_color_lab.Visibility = Visibility.Hidden;
            id_color_text.Visibility = Visibility.Hidden;
            name_color_lab.Visibility = Visibility.Hidden;
            name_color_text.Visibility = Visibility.Hidden;
            id_engine_lab.Visibility = Visibility.Hidden;
            id_engine_text.Visibility = Visibility.Hidden;
            type_engine_lab.Visibility = Visibility.Hidden;
            type_engine_text.Visibility = Visibility.Hidden;
            volume_lab.Visibility = Visibility.Hidden;
            volume_text.Visibility = Visibility.Hidden;
            power_lab.Visibility = Visibility.Hidden;
            power_text.Visibility = Visibility.Hidden;
            id_body_lab.Visibility = Visibility.Hidden;
            id_body_text.Visibility = Visibility.Hidden;
            type_body_lab.Visibility = Visibility.Hidden;
            type_body_text.Visibility = Visibility.Hidden;
            brand_id_lab.Visibility = Visibility.Hidden;
            brand_id_text.Visibility = Visibility.Hidden;
            brand_name_lab.Visibility = Visibility.Hidden;
            brand_name_text.Visibility = Visibility.Hidden;
            map.Clear();
            map2.Clear();
            engine_text.Items.Clear();
            body_text.Items.Clear();
            brand_text.Items.Clear();

            try
            {


                string sqlExpression = "SELECT id_brand, name_brand FROM BRANDS";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
               

                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                       string local_id_brand = reader.GetString(0);
                       string local_name_brand = reader.GetString(1);
                       brand_text.Items.Add(local_name_brand);
                       map2[local_name_brand] = local_id_brand;


                    }
                    reader.Close();

                }
                else
                {

                    reader.Close();

                }

                sqlExpression = " SELECT id_engine FROM dbo.ENGINES ";
                command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read()) {
                        engine_text.Items.Add(reader.GetString(0));



                    }
                    reader.Close();
                }
                   



                sqlExpression = " SELECT id_body, description_body FROM dbo.BODIES ";
                command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string local_id_body = reader.GetString(0);
                        string local_name_body = reader.GetString(1);
                        body_text.Items.Add(local_name_body);
                        map[local_name_body] = local_id_body;

                    }
                }
                reader.Close();


                
            

            }
            

            catch (SqlException en)
            {
                MessageBox.Show((en.Number).ToString() + " " + en.Message);
            }

        }

        private void engine(object sender, RoutedEventArgs e)
        {
            add_brand_but.Visibility = Visibility.Hidden;
            add_body_but.Visibility = Visibility.Hidden;
            add_model_but.Visibility = Visibility.Hidden;
            add_engine_but.Visibility = Visibility.Visible;
            add_color_but.Visibility = Visibility.Hidden;
            add_vehicle_but.Visibility = Visibility.Hidden;
            download.Visibility = Visibility.Hidden;
            path_photo.Visibility = Visibility.Hidden;
            lab_add_photo.Visibility = Visibility.Hidden;

            id_engine_lab.Visibility = Visibility.Visible;
            id_engine_text.Visibility = Visibility.Visible;
            type_engine_lab.Visibility = Visibility.Visible;
            type_engine_text.Visibility = Visibility.Visible;
            volume_lab.Visibility = Visibility.Visible;
            volume_text.Visibility = Visibility.Visible;
            power_lab.Visibility = Visibility.Visible;
            power_text.Visibility = Visibility.Visible;
            //скрыть все элементы
            seat_count_lab.Visibility = Visibility.Hidden;
            seat_count_text.Visibility = Visibility.Hidden;
            add_brand_lab.Visibility = Visibility.Hidden;
            add_brand_text.Visibility = Visibility.Hidden;
            add_model_lab.Visibility = Visibility.Hidden;
            add_model_text.Visibility = Visibility.Hidden;
            add_vin_lab.Visibility = Visibility.Hidden;
            add_vin_text.Visibility = Visibility.Hidden;
            add_year_lab.Visibility = Visibility.Hidden;
            add_year_text.Visibility = Visibility.Hidden;
            add_color_lab.Visibility = Visibility.Hidden;
            add_color_text.Visibility = Visibility.Hidden;
            add_price_lab.Visibility = Visibility.Hidden;
            add_price_text.Visibility = Visibility.Hidden;
            id_color_lab.Visibility = Visibility.Hidden;
            id_color_text.Visibility = Visibility.Hidden;
            name_color_lab.Visibility = Visibility.Hidden;
            name_color_text.Visibility = Visibility.Hidden;
            id_body_lab.Visibility = Visibility.Hidden;
            id_body_text.Visibility = Visibility.Hidden;
            type_body_lab.Visibility = Visibility.Hidden;
            type_body_text.Visibility = Visibility.Hidden;
            brand_id_lab.Visibility = Visibility.Hidden;
            brand_id_text.Visibility = Visibility.Hidden;
            brand_name_lab.Visibility = Visibility.Hidden;
            brand_name_text.Visibility = Visibility.Hidden;
            model_id_lab.Visibility = Visibility.Hidden;
            model_id_text.Visibility = Visibility.Hidden;
            model_name_lab.Visibility = Visibility.Hidden;
            model_name_text.Visibility = Visibility.Hidden;
            body_lab.Visibility = Visibility.Hidden;
            body_text.Visibility = Visibility.Hidden;
            brand_lab.Visibility = Visibility.Hidden;
            brand_text.Visibility = Visibility.Hidden;
            engine_lab.Visibility = Visibility.Hidden;
            engine_text.Visibility = Visibility.Hidden;
            gabarits_lab.Visibility = Visibility.Hidden;
            length_text.Visibility = Visibility.Hidden;
            width_text.Visibility = Visibility.Hidden;
            height_text.Visibility = Visibility.Hidden;
        }

        private void body(object sender, RoutedEventArgs e)
        {
            add_brand_but.Visibility = Visibility.Hidden;
            add_body_but.Visibility = Visibility.Visible;
            add_model_but.Visibility = Visibility.Hidden;
            add_engine_but.Visibility = Visibility.Hidden;
            add_color_but.Visibility = Visibility.Hidden;
            add_vehicle_but.Visibility = Visibility.Hidden;
            download.Visibility = Visibility.Hidden;
            path_photo.Visibility = Visibility.Hidden;
            lab_add_photo.Visibility = Visibility.Hidden;

            id_body_lab.Visibility = Visibility.Visible;
            id_body_text.Visibility = Visibility.Visible;
            type_body_lab.Visibility = Visibility.Visible;
            type_body_text.Visibility = Visibility.Visible;
            seat_count_lab.Visibility = Visibility.Visible;
            seat_count_text.Visibility = Visibility.Visible;
            //скрыть все элементы
            add_brand_lab.Visibility = Visibility.Hidden;
            add_brand_text.Visibility = Visibility.Hidden;
            add_model_lab.Visibility = Visibility.Hidden;
            add_model_text.Visibility = Visibility.Hidden;
            add_vin_lab.Visibility = Visibility.Hidden;
            add_vin_text.Visibility = Visibility.Hidden;
            add_year_lab.Visibility = Visibility.Hidden;
            add_year_text.Visibility = Visibility.Hidden;
            add_color_lab.Visibility = Visibility.Hidden;
            add_color_text.Visibility = Visibility.Hidden;
            add_price_lab.Visibility = Visibility.Hidden;
            add_price_text.Visibility = Visibility.Hidden;
            id_color_lab.Visibility = Visibility.Hidden;
            id_color_text.Visibility = Visibility.Hidden;
            name_color_lab.Visibility = Visibility.Hidden;
            name_color_text.Visibility = Visibility.Hidden;
            id_engine_lab.Visibility = Visibility.Hidden;
            id_engine_text.Visibility = Visibility.Hidden;
            type_engine_lab.Visibility = Visibility.Hidden;
            type_engine_text.Visibility = Visibility.Hidden;
            volume_lab.Visibility = Visibility.Hidden;
            volume_text.Visibility = Visibility.Hidden;
            power_lab.Visibility = Visibility.Hidden;
            power_text.Visibility = Visibility.Hidden;
            brand_id_lab.Visibility = Visibility.Hidden;
            brand_id_text.Visibility = Visibility.Hidden;
            brand_name_lab.Visibility = Visibility.Hidden;
            brand_name_text.Visibility = Visibility.Hidden;
            model_id_lab.Visibility = Visibility.Hidden;
            model_id_text.Visibility = Visibility.Hidden;
            model_name_lab.Visibility = Visibility.Hidden;
            model_name_text.Visibility = Visibility.Hidden;
            body_lab.Visibility = Visibility.Hidden;
            body_text.Visibility = Visibility.Hidden;
            brand_lab.Visibility = Visibility.Hidden;
            brand_text.Visibility = Visibility.Hidden;
            engine_lab.Visibility = Visibility.Hidden;
            engine_text.Visibility = Visibility.Hidden;
            gabarits_lab.Visibility = Visibility.Hidden;
            length_text.Visibility = Visibility.Hidden;
            width_text.Visibility = Visibility.Hidden;
            height_text.Visibility = Visibility.Hidden;
        }

        private void color(object sender, RoutedEventArgs e)
        {
            add_brand_but.Visibility = Visibility.Hidden;
            add_body_but.Visibility = Visibility.Hidden;
            add_model_but.Visibility = Visibility.Hidden;
            add_engine_but.Visibility = Visibility.Hidden;
            add_color_but.Visibility = Visibility.Visible;
            add_vehicle_but.Visibility = Visibility.Hidden;
            download.Visibility = Visibility.Hidden;
            path_photo.Visibility = Visibility.Hidden;
            lab_add_photo.Visibility = Visibility.Hidden;

            id_color_lab.Visibility = Visibility.Visible;
            id_color_text.Visibility = Visibility.Visible;
            name_color_lab.Visibility = Visibility.Visible;
            name_color_text.Visibility = Visibility.Visible;
            //скрыть все элементы
            seat_count_lab.Visibility = Visibility.Hidden;
            seat_count_text.Visibility = Visibility.Hidden;
            add_brand_lab.Visibility = Visibility.Hidden;
            add_brand_text.Visibility = Visibility.Hidden;
            add_model_lab.Visibility = Visibility.Hidden;
            add_model_text.Visibility = Visibility.Hidden;
            add_vin_lab.Visibility = Visibility.Hidden;
            add_vin_text.Visibility = Visibility.Hidden;
            add_year_lab.Visibility = Visibility.Hidden;
            add_year_text.Visibility = Visibility.Hidden;
            add_color_lab.Visibility = Visibility.Hidden;
            add_color_text.Visibility = Visibility.Hidden;
            add_price_lab.Visibility = Visibility.Hidden;
            add_price_text.Visibility = Visibility.Hidden;
            brand_id_lab.Visibility = Visibility.Hidden;
            brand_id_text.Visibility = Visibility.Hidden;
            brand_name_lab.Visibility = Visibility.Hidden;
            brand_name_text.Visibility = Visibility.Hidden;
            id_engine_lab.Visibility = Visibility.Hidden;
            id_engine_text.Visibility = Visibility.Hidden;
            type_engine_lab.Visibility = Visibility.Hidden;
            type_engine_text.Visibility = Visibility.Hidden;
            volume_lab.Visibility = Visibility.Hidden;
            volume_text.Visibility = Visibility.Hidden;
            power_lab.Visibility = Visibility.Hidden;
            power_text.Visibility = Visibility.Hidden;
            id_body_lab.Visibility = Visibility.Hidden;
            id_body_text.Visibility = Visibility.Hidden;
            type_body_lab.Visibility = Visibility.Hidden;
            type_body_text.Visibility = Visibility.Hidden;
            model_id_lab.Visibility = Visibility.Hidden;
            model_id_text.Visibility = Visibility.Hidden;
            model_name_lab.Visibility = Visibility.Hidden;
            model_name_text.Visibility = Visibility.Hidden;
            body_lab.Visibility = Visibility.Hidden;
            body_text.Visibility = Visibility.Hidden;
            brand_lab.Visibility = Visibility.Hidden;
            brand_text.Visibility = Visibility.Hidden;
            engine_lab.Visibility = Visibility.Hidden;
            engine_text.Visibility = Visibility.Hidden;
            gabarits_lab.Visibility = Visibility.Hidden;
            length_text.Visibility = Visibility.Hidden;
            width_text.Visibility = Visibility.Hidden;
            height_text.Visibility = Visibility.Hidden;
        }

        private void car(object sender, RoutedEventArgs e)
        {
            add_brand_but.Visibility = Visibility.Hidden;
            add_body_but.Visibility = Visibility.Hidden;
            add_model_but.Visibility = Visibility.Hidden;
            add_engine_but.Visibility = Visibility.Hidden;
            add_color_but.Visibility = Visibility.Hidden;
            add_vehicle_but.Visibility = Visibility.Visible;
            download.Visibility = Visibility.Visible;
            path_photo.Visibility = Visibility.Visible;
            lab_add_photo.Visibility = Visibility.Visible;

            add_brand_lab.Visibility = Visibility.Visible;
            add_brand_text.Visibility = Visibility.Visible; //cmb
            add_model_lab.Visibility = Visibility.Visible;
            add_model_text.Visibility = Visibility.Visible; //cmb
            add_vin_lab.Visibility = Visibility.Visible;
            add_vin_text.Visibility = Visibility.Visible; //txt
            add_year_lab.Visibility = Visibility.Visible;
            add_year_text.Visibility = Visibility.Visible; //txt
            add_color_lab.Visibility = Visibility.Visible;
            add_color_text.Visibility = Visibility.Visible; //cmb
            add_price_lab.Visibility = Visibility.Visible;
            add_price_text.Visibility = Visibility.Visible; //txt

            //скрыть все элементы
            seat_count_lab.Visibility = Visibility.Hidden;
            seat_count_text.Visibility = Visibility.Hidden;
            id_color_lab.Visibility = Visibility.Hidden;
            id_color_text.Visibility = Visibility.Hidden;
            name_color_lab.Visibility = Visibility.Hidden;
            name_color_text.Visibility = Visibility.Hidden;
            brand_id_lab.Visibility = Visibility.Hidden;
            brand_id_text.Visibility = Visibility.Hidden;
            brand_name_lab.Visibility = Visibility.Hidden;
            brand_name_text.Visibility = Visibility.Hidden;
            id_engine_lab.Visibility = Visibility.Hidden;
            id_engine_text.Visibility = Visibility.Hidden;
            type_engine_lab.Visibility = Visibility.Hidden;
            type_engine_text.Visibility = Visibility.Hidden;
            volume_lab.Visibility = Visibility.Hidden;
            volume_text.Visibility = Visibility.Hidden;
            power_lab.Visibility = Visibility.Hidden;
            power_text.Visibility = Visibility.Hidden;
            id_body_lab.Visibility = Visibility.Hidden;
            id_body_text.Visibility = Visibility.Hidden;
            type_body_lab.Visibility = Visibility.Hidden;
            type_body_text.Visibility = Visibility.Hidden;
            model_id_lab.Visibility = Visibility.Hidden;
            model_id_text.Visibility = Visibility.Hidden;
            model_name_lab.Visibility = Visibility.Hidden;
            model_name_text.Visibility = Visibility.Hidden;
            body_lab.Visibility = Visibility.Hidden;
            body_text.Visibility = Visibility.Hidden;
            brand_lab.Visibility = Visibility.Hidden;
            brand_text.Visibility = Visibility.Hidden;
            engine_lab.Visibility = Visibility.Hidden;
            engine_text.Visibility = Visibility.Hidden;
            gabarits_lab.Visibility = Visibility.Hidden;
            length_text.Visibility = Visibility.Hidden;
            width_text.Visibility = Visibility.Hidden;
            height_text.Visibility = Visibility.Hidden;
            cur_file_name = "";
            add_year_text.Items.Clear();
            string date_now = DateTime.Now.ToString("yyyy");
            int date_selector;
            Int32.TryParse(date_now, out date_selector);
            for (int i = date_selector -20; i <= date_selector; i++) {
                add_year_text.Items.Add(i.ToString());

            }

            add_brand_text.Items.Clear();
            try
            {
                string sqlExpression = "SELECT id_brand, name_brand FROM BRANDS";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();


                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        string local_id_brand = reader.GetString(0);
                        string local_name_brand = reader.GetString(1);
                        add_brand_text.Items.Add(local_name_brand);
                        map2[local_name_brand] = local_id_brand;


                    }
                    reader.Close();

                }
                else
                {

                    reader.Close();

                }

                add_color_text.Items.Clear();
                sqlExpression = "SELECT id_color, description_color FROM dbo.COLORS ";
                command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string local_id_color = reader.GetString(0);
                        string local_name_color = reader.GetString(1);
                        add_color_text.Items.Add(local_name_color);
                        map4[local_name_color] = local_id_color;

                    }
                }
                reader.Close();






            }
            catch (SqlException er)
            {
                MessageBox.Show(er.ToString());

            }
        }

        private void models_of_brand(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            object brand = add_brand_text.SelectedItem;
            add_model_text.Items.Clear();
            if (brand == null) {
                return;
            }

            string brand_id = map2[brand.ToString()];
            //add_model_text.Items.Clear();
            try
            {
                string sqlExpression = "SELECT id_model, name_model FROM dbo.MODELS WHERE(id_brand = '" + brand_id + " ')";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) {

                    while (reader.Read()) {
                        string id_model = reader.GetString(0);
                        string name_model = reader.GetString(1);
                        map3[name_model] = id_model;
                        add_model_text.Items.Add(name_model);

                    }
                
                }


            } catch (SqlException er) {
                MessageBox.Show(er.Message);
            
            }
           
        }

        public static string check_lwh(string variable)
        {
            if (variable.Length == 0)
                return "Значение не задано";
            string variable_local = variable.Trim();
            if (variable_local.Length == 0)
                return "Заданы одни пробелы";

            Regex regex = new Regex(@"^[1-9][0-9]{3}$");
            if (!regex.IsMatch(variable_local))

                return "Недопустимый символ";

            return "";


        }

        public static string check_id_engine(string variable)
        {
            if (variable.Length == 0)
                return "Значение не задано";
            string variable_local = variable.Trim();
            if (variable_local.Length == 0)
                return "Заданы одни пробелы";

            Regex regex = new Regex(@"^[A-Z0-9]$");
            if (!regex.IsMatch(variable_local))

                return "Недопустимый символ";

            return "";


        }

        public static string check_power(string variable) 
        {
            if (variable.Length == 0)
                return "Значение не задано";
            string variable_local = variable.Trim();
            if (variable_local.Length == 0)
                return "Заданы одни пробелы";

            Regex regex = new Regex(@"^[1-9][0-9]{2}$");
            if (!regex.IsMatch(variable_local))

                return "Недопустимый символ";

            return "";


        }

        public static string check_volume(string variable) 
        {
            if (variable.Length == 0)
                return "Значение не задано";
            string variable_local = variable.Trim();
            if (variable_local.Length == 0)
                return "Заданы одни пробелы";

            Regex regex = new Regex(@"^[1-6]\.[0-9]$");
            if (!regex.IsMatch(variable_local))

                return "Недопустимый символ";

            return "";


        }

        public static string check_vin(string variable)
        {
            if (variable.Length == 0)
                return "Значение не задано";
            string variable_local = variable.Trim();
            if (variable_local.Length == 0)
                return "Заданы одни пробелы";

            Regex regex = new Regex(@"^[A-Z]{10}[0-9]{6}$");
            if (!regex.IsMatch(variable_local))
                return "Недопустимый символ";
            return "";


        }

        public static string check_price(string variable)
        {
            if (variable.Length == 0)
                return "Значение не задано";
            string variable_local = variable.Trim();
            if (variable_local.Length == 0)
                return "Заданы одни пробелы";

            Regex regex = new Regex(@"^[0-9]{1,9}(\.[0-9]{2})?$");
            if (!regex.IsMatch(variable_local))

                return "Недопустимый символ";

            return "";


        }

        public static string check_id(string variable)
        {
            if (variable.Length == 0)
                return "Значение не задано";
            string variable_local = variable.Trim();
            if (variable_local.Length == 0)
                return "Заданы одни пробелы";

            Regex regex = new Regex(@"^[A-Z]{1}$");
            if (!regex.IsMatch(variable_local))

                return "Недопустимый символ";

            return "";


        }

        public static string check_name(string variable)
        {
            if (variable.Length == 0)
                return "Значение не задано";
            string variable_local = variable.Trim();
            if (variable_local.Length == 0)
                return "Заданы одни пробелы";

            Regex regex = new Regex(@"^([A-z]+)((-|\s)[A-z]+)*$");
            if (!regex.IsMatch(variable_local))

                return "Недопустимый символ";

            return "";


        }

        public static string check_color_id(string variable)  
        {
            if (variable.Length == 0)
                return "Значение не задано";
            string variable_local = variable.Trim();
            if (variable_local.Length == 0)
                return "Заданы одни пробелы";

            Regex regex = new Regex(@"^#[0-9A-F]{6}$");
            if (!regex.IsMatch(variable_local)) 

                return "Недопустимый символ";

            return "";


        }

        public static string check_color_name(string variable)
        {
            if (variable.Length == 0)
                return "Значение не задано";
            string variable_local = variable.Trim();
            if (variable_local.Length == 0)
                return "Заданы одни пробелы";

            Regex regex = new Regex(@"^[A-zА-я]+$");
            if (!regex.IsMatch(variable_local))

                return "Недопустимый символ";

            return "";


        }
        
        private void add_brand_but_Click(object sender, RoutedEventArgs e)
        {
            bool er = false;
            string error_msg;

            if ((error_msg = check_id(brand_id_text.Text)) != "")
            {
                er = true;

            }
            if ((error_msg = check_name(brand_name_text.Text)) != "")
            {
                er = true;

            }


            if (!er)
            {

                try
                {

                    string brand_id = brand_id_text.Text;
                    string brand_name = brand_name_text.Text;
                    string sqlExpression = " INSERT INTO BRANDS VALUES (@brand_id_value, @brand_name_value) ";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlParameter brand_id_par = new SqlParameter("@brand_id_value", brand_id);
                    command.Parameters.Add(brand_id_par);
                    SqlParameter brand_name_par = new SqlParameter("@brand_name_value", brand_name);
                    command.Parameters.Add(brand_name_par);
                    command.ExecuteNonQuery();
                    brand_id_text.Text = "";
                    brand_name_text.Text = "";
                    MessageBox.Show("Марка " + brand_name + " добавлена!");
                }

                catch (SqlException en)
                {
                    MessageBox.Show((en.Number).ToString() + " " + en.Message);
                }
            }

        }

        private void add_model_but_Click(object sender, RoutedEventArgs e)
        {

            bool er = false;
            string error_msg;

            if ((error_msg = check_id(model_id_text.Text)) != "")
            {
                er = true;

            }
            if ((error_msg = check_name(model_name_text.Text)) != "")
            {
                er = true;

            }

            if ((error_msg = check_lwh(length_text.Text)) != "")
            {
                er = true;

            }

            if ((error_msg = check_lwh(width_text.Text)) != "")
            {
                er = true;

            }

            if ((error_msg = check_lwh(height_text.Text)) != "")
            {
                er = true;

            }


            if (!er)
            {

                if (body_text.SelectedItem != null && engine_text.SelectedItem != null && brand_text != null)
                {

                    string model_name_local = model_name_text.Text;
                    string model_id = model_id_text.Text;
                    string local_length = length_text.Text; //
                    string local_width = width_text.Text; //
                    string local_height = height_text.Text; //
                    string local_engine = engine_text.Text;
                    string local_brand_id = map2[brand_text.Text];
                    string local_type_body = map[body_text.Text];
                    try
                    {
                        string sqlExpression = " INSERT INTO MODELS VALUES (@id_model_value, @name_model_value, @id_body_value, @id_brand_value, @id_engine_value, @length_value, @width_value, @height_value) ";
                        SqlCommand command = new SqlCommand(sqlExpression, connection);

                        SqlParameter id_model_par = new SqlParameter("@id_model_value", model_id);
                        command.Parameters.Add(id_model_par);

                        SqlParameter model_name_par = new SqlParameter("@name_model_value", model_name_local);
                        command.Parameters.Add(model_name_par);

                        SqlParameter id_body_par = new SqlParameter("@id_body_value", local_type_body);
                        command.Parameters.Add(id_body_par);

                        SqlParameter id_brand_par = new SqlParameter("@id_brand_value", local_brand_id);
                        command.Parameters.Add(id_brand_par);

                        SqlParameter id_engine_par = new SqlParameter("@id_engine_value", local_engine);
                        command.Parameters.Add(id_engine_par);

                        SqlParameter length_par = new SqlParameter("@length_value", local_length);
                        command.Parameters.Add(length_par);

                        SqlParameter width_par = new SqlParameter("@width_value", local_width);
                        command.Parameters.Add(width_par);

                        SqlParameter height_par = new SqlParameter("@height_value", local_height);
                        command.Parameters.Add(height_par);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Модель" + model_name_local + "добавлена");

                    }

                    catch (SqlException en)
                    {
                        MessageBox.Show((en.Number).ToString() + " " + en.Message);
                    }
                }
            }
        }

        private void add_engine_but_Click(object sender, RoutedEventArgs e)
        {
            bool er = false;
            string error_msg;

            if ((error_msg = check_id_engine(id_engine_text.Text)) != "")
            {
                er = true;

            }
            if ((error_msg = check_volume(volume_text.Text)) != "")
            {
                er = true;

            }
            if ((error_msg = check_power(power_text.Text)) != "")
            {
                er = true;

            }

            if (type_engine_text.SelectedItem != null && !er) {
                try
                {
                    string engine_id = id_engine_text.Text;
                    string engine_type = type_engine_text.Text;
                    string engine_volume = volume_text.Text;
                    string engine_power = power_text.Text;

                    string sqlExpression = " INSERT INTO ENGINES VALUES (@id_engine, @type_engine, @volume_engine, @power_engine) ";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlParameter id_par = new SqlParameter("@id_engine", engine_id);
                    command.Parameters.Add(id_par);
                    SqlParameter type_par = new SqlParameter("@type_engine", engine_type);
                    command.Parameters.Add(type_par);
                    SqlParameter volume_par = new SqlParameter("@volume_engine", engine_volume);
                    command.Parameters.Add(volume_par);
                    SqlParameter power_par = new SqlParameter("@power_engine", engine_power);
                    command.Parameters.Add(power_par);
                    command.ExecuteNonQuery();
                    id_engine_text.Text = "";
                    power_text.Text = "";
                    volume_text.Text = "";
                    type_engine_text.Text = "";
                    MessageBox.Show("Двигатель добавлен!");
                }

                catch (SqlException en)
                {
                    MessageBox.Show((en.Number).ToString() + " " + en.Message);
                }
            }
        }

        private void add_body_but_Click(object sender, RoutedEventArgs e)
        {

            bool er = false;
            string error_msg;

            if ((error_msg = check_id_engine(id_body_text.Text)) != "")
            {
                er = true;

            }
            if ((error_msg = check_name(type_body_text.Text)) != "")
            {
                er = true;

            }

            if (seat_count_text.SelectedItem != null && !er) {
                try
                {
                    string body_id = id_body_text.Text;
                    string body_name = type_body_text.Text;
                    string seat_count = seat_count_text.Text;
                    string sqlExpression = " INSERT INTO BODIES VALUES (@body_id_value, @body_name_value, @seat_count_value) ";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);

                    SqlParameter body_id_par = new SqlParameter("@body_id_value", body_id);
                    command.Parameters.Add(body_id_par);
                    SqlParameter body_name_par = new SqlParameter("@body_name_value", body_name);
                    command.Parameters.Add(body_name_par);

                    SqlParameter seat_count_par = new SqlParameter("@seat_count_value", seat_count);
                    command.Parameters.Add(seat_count_par);

                    command.ExecuteNonQuery();
                    id_body_text.Text = "";
                    type_body_text.Text = "";
                    seat_count_text.Text = "";
                    MessageBox.Show("Кузов " + body_name + " добавлен!");
                }

                catch (SqlException en)
                {
                    MessageBox.Show((en.Number).ToString() + " " + en.Message);
                }
            }

        }

        private void add_color_but_Click(object sender, RoutedEventArgs e)
        {
            bool er = false;
            string error_msg;

            if ((error_msg = check_color_id(id_color_text.Text)) != "")
            {
                er = true;

            }
            if ((error_msg = check_color_name(name_color_text.Text)) != "")
            {
                er = true;

            }


            if (!er)
            {

                try
                {
                    string color_id = id_color_text.Text;
                    string color_name = name_color_text.Text;

                    string sqlExpression = " INSERT INTO COLORS VALUES (@color_id_value, @color_name_value) ";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);

                    SqlParameter color_id_par = new SqlParameter("@color_id_value", color_id);
                    command.Parameters.Add(color_id_par);

                    SqlParameter color_name_par = new SqlParameter("@color_name_value", color_name);
                    command.Parameters.Add(color_name_par);

                    command.ExecuteNonQuery();
                    id_color_text.Text = "";
                    name_color_text.Text = "";
                    MessageBox.Show("Цвет " + color_name + " добавлен!");

                }

                catch (SqlException en)
                {
                    MessageBox.Show((en.Number).ToString() + " " + en.Message);
                }

            }
        }

        private void add_vehicle_but_Click(object sender, RoutedEventArgs e)
        {
            if (add_brand_text.SelectedItem != null && add_model_text.SelectedItem != null && add_color_text != null ) {

                bool er = false;
                string error_msg = "";


                FileInfo fInfo = new FileInfo(cur_file_name);
                long numBytes = fInfo.Length;

                if (numBytes > 10485760) {
                    MessageBox.Show("Error download file!");
                    er = true;
                }

                if ((error_msg = check_vin(add_vin_text.Text)) != "")
                {
                    er = true;
                 
                }
                if ((error_msg = check_price(add_price_text.Text)) != "")
                {
                    er = true;
                   
                }
                

                if (!er)
                {
                    string id_vehicle = DateTime.Now.ToString("yyyyddMMss");
                    string brand_id = map2[add_brand_text.Text];
                    string model_id = map3[add_model_text.Text];
                    string color_id = map4[add_color_text.Text];
                    string vin_num = add_vin_text.Text;
                    string year_car = add_year_text.Text;
                    string price_car = add_price_text.Text;

                    byte[] imageData = null;
                    FileStream fStream = new FileStream(cur_file_name, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fStream);
                    imageData = br.ReadBytes((int)numBytes);

                    MessageBox.Show(id_vehicle + brand_id + model_id + color_id + vin_num + year_car + price_car);

                    string sqlExpression = "INSERT INTO dbo.VEHICLES(id_vehicle, id_brand, id_model, vin_key, year_vehicle, id_color, id_status, price_vehicle, image_vehicle) " +
                        " VALUES (@id_vehicle_value, @id_brand_value, @id_model_value, @vin_key_value, @year_vehicle_value, @id_color_value, 'H', @price_vehicle_value, @image_value)";
                    try
                    {
                        SqlCommand command = new SqlCommand(sqlExpression, connection);

                        SqlParameter vehicle_id_par = new SqlParameter("@id_vehicle_value", id_vehicle);
                        command.Parameters.Add(vehicle_id_par);

                        SqlParameter brand_par = new SqlParameter("@id_brand_value", brand_id);
                        command.Parameters.Add(brand_par);

                        SqlParameter model_par = new SqlParameter("@id_model_value", model_id);
                        command.Parameters.Add(model_par);

                        SqlParameter color_par = new SqlParameter("@id_color_value", color_id);
                        command.Parameters.Add(color_par);

                        SqlParameter year_par = new SqlParameter("@year_vehicle_value", year_car);
                        command.Parameters.Add(year_par);

                        SqlParameter vin_par = new SqlParameter("@vin_key_value", vin_num);
                        command.Parameters.Add(vin_par);

                        SqlParameter price_par = new SqlParameter("@price_vehicle_value", price_car);
                        command.Parameters.Add(price_par);

                        SqlParameter image_par = new SqlParameter("@image_value", (object)imageData);
                        command.Parameters.Add(image_par);

                        command.ExecuteNonQuery();
                    }
                    catch (SqlException error) {
                        MessageBox.Show(error.Message);
                    
                    }

                }

            }
        }

        private void download_Click(object sender, RoutedEventArgs e)
        {
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true) {
                 cur_file_name = dlg.FileName;
                path_photo.Text = cur_file_name;
                
            }
        }
    }
}