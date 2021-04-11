using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
using System.Windows.Controls;

namespace MotorCarsMoscowD
{
    public class stat_record
    {
        public string ORDER { get; set; }
        public string BRAND { get; set; }
        public string MODEL { get; set; }
        public string YEAR { get; set; }
        public double VOLUME { get; set; }
        public double POWER { get; set; }
        public string COLOR { get; set; }
        public double PRICE { get; set; }
        public string EMPLOYEE { get; set; }
        public string CLIENT { get; set; }


    }


    public class sold_models
    {
        public string BRAND { get; set; }
        public string MODEL { get; set; }
        public int COUNT { get; set; }
       

    }

    public class all_cars_rec
    {
        public string BRAND { get; set; }
        public string MODEL { get; set; }
        public string YEAR { get; set; }
        public string COLOR { get; set; }
        public double VOLUME { get; set; }
        public int POWER { get; set; }
        public double PRICE { get; set; }
        public string STATUS { get; set; }
    }


    public class all_employees
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


    public class stat_employees 
    {
        public string LOGIN { get; set; }
        public string LAST_NAME { get; set; }
        public string INITIALS { get; set; }
        public int COUNT_ORDERS { get; set; }
        public double MONEY { get; set; }
    }

    public partial class ShowStat : Window
    {

      
        private Window parent_window;
        private SqlConnection connection;
        
        
        
        public ShowStat(SqlConnection connection, Window parent_window)
        {
            InitializeComponent();
            this.Show();
            this.parent_window = parent_window;
            this.connection = connection;
            parent_window.Visibility = Visibility.Hidden;


        }

        private void goBack(object sender, RoutedEventArgs e)
        {
            parent_window.Visibility = Visibility.Visible;
            this.Close();

        }


        private void all_cars(object sender, RoutedEventArgs e)
        {
            List<all_cars_rec> all_record_list = new List<all_cars_rec>();

            string sqlExpression = "SELECT        TOP (100) PERCENT dbo.BRANDS.name_brand, dbo.MODELS.name_model, dbo.VEHICLES.year_vehicle, dbo.COLORS.description_color, dbo.ENGINES.volume_engine,  dbo.ENGINES.power_engine,  dbo.VEHICLES.price_vehicle, dbo.STATUSES.description_status, "
                        + " dbo.ENGINES.power_engine "
                        +" FROM            dbo.MODELS INNER JOIN "
                        +" dbo.BRANDS ON dbo.MODELS.id_brand = dbo.BRANDS.id_brand INNER JOIN "
                        +" dbo.VEHICLES ON dbo.MODELS.id_brand = dbo.VEHICLES.id_brand AND dbo.MODELS.id_model = dbo.VEHICLES.id_model INNER JOIN "
                        +"  dbo.STATUSES ON dbo.VEHICLES.id_status = dbo.STATUSES.id_status INNER JOIN "
                        +"  dbo.COLORS ON dbo.VEHICLES.id_color = dbo.COLORS.id_color INNER JOIN "
                        +" dbo.ENGINES ON dbo.MODELS.id_engine = dbo.ENGINES.id_engine "
                        +" GROUP BY dbo.BRANDS.name_brand, dbo.MODELS.name_model, dbo.VEHICLES.year_vehicle, dbo.COLORS.description_color, dbo.STATUSES.description_status, dbo.ENGINES.volume_engine, "
                        + " dbo.ENGINES.power_engine, dbo.VEHICLES.price_vehicle "
                        + "ORDER BY dbo.STATUSES.description_status ";

            
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {


                while (reader.Read())
                {

                    all_cars_rec st_rec = new all_cars_rec();
                   
                    st_rec.BRAND = reader.GetString(0);
                    st_rec.MODEL = reader.GetString(1);
                    st_rec.YEAR = reader.GetString(2);
                    st_rec.COLOR = reader.GetString(3);
                    st_rec.VOLUME = reader.GetDouble(4);
                    st_rec.POWER = reader.GetInt32(5); 
                    st_rec.PRICE = (reader.GetSqlMoney(6)).ToDouble();
                    st_rec.STATUS = reader.GetString(7);
                    all_record_list.Add(st_rec);


                }
                reader.Close();
                
                grid.ItemsSource = all_record_list;
                grid.Columns[0].Header = "Марка";
                grid.Columns[1].Header = "Модель";
                grid.Columns[2].Header = "Год выпуска";
                grid.Columns[3].Header = "Цвет";
                grid.Columns[4].Header = "Объём";
                grid.Columns[5].Header = "Мощность";
                grid.Columns[6].Header = "Цена";
                grid.Columns[7].Header = "Статус";
            }
            else
            {
                MessageBox.Show("Строк не найдено");
                reader.Close();

            }



        }

        private void orders_cars(object sender, RoutedEventArgs e)
        {


           List<stat_record> stat_record_list = new List<stat_record>();
           string sqlExpression = "SELECT dbo.ORDERS.id_order, dbo.BRANDS.name_brand, dbo.MODELS.name_model, dbo.VEHICLES.year_vehicle, dbo.COLORS.description_color, dbo.ENGINES.volume_engine, dbo.ENGINES.power_engine, dbo.VEHICLES.price_vehicle, "
                 + " dbo.EMPLOYEES.id_employee, dbo.CLIENTS.id_client"
                 + " FROM     dbo.VEHICLES INNER JOIN"
                 + " dbo.BRANDS ON dbo.VEHICLES.id_brand = dbo.BRANDS.id_brand INNER JOIN"
                 + " dbo.MODELS ON dbo.VEHICLES.id_brand = dbo.MODELS.id_brand AND dbo.VEHICLES.id_model = dbo.MODELS.id_model AND dbo.BRANDS.id_brand = dbo.MODELS.id_brand INNER JOIN"
                 + " dbo.ORDERS ON dbo.VEHICLES.id_vehicle = dbo.ORDERS.id_vehicle INNER JOIN"
                 + " dbo.EMPLOYEES ON dbo.ORDERS.id_employee = dbo.EMPLOYEES.id_employee INNER JOIN"
                 + " dbo.CLIENTS ON dbo.ORDERS.id_client = dbo.CLIENTS.id_client INNER JOIN"
                 + " dbo.COLORS ON dbo.VEHICLES.id_color = dbo.COLORS.id_color INNER JOIN"
                 + " dbo.ENGINES ON dbo.MODELS.id_engine = dbo.ENGINES.id_engine";

            SqlCommand command = new SqlCommand(sqlExpression, connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {


                while (reader.Read())
                {

                    stat_record st_rec = new stat_record();
                    st_rec.ORDER = reader.GetString(0);
                    st_rec.BRAND = reader.GetString(1);
                    st_rec.MODEL = reader.GetString(2);
                    st_rec.YEAR = reader.GetString(3);
                    st_rec.COLOR = reader.GetString(4);
                    st_rec.VOLUME = reader.GetDouble(5);
                    st_rec.POWER = reader.GetInt32(6);
                    st_rec.PRICE = (reader.GetSqlMoney(7).ToDouble());
                    st_rec.EMPLOYEE = reader.GetString(8);
                    st_rec.CLIENT = reader.GetString(9);
                    stat_record_list.Add(st_rec);

                }
                reader.Close();
                grid.ItemsSource = stat_record_list;

                grid.Columns[0].Header = "Заказ";
                grid.Columns[1].Header = "Марка";
                grid.Columns[2].Header = "Модель";
                grid.Columns[3].Header = "Год выпуска";
                grid.Columns[4].Header = "Мощность";
                grid.Columns[5].Header = "Объём";
                grid.Columns[6].Header = "Цвет";
                grid.Columns[7].Header = "Цена";
                grid.Columns[8].Header = "Продавец";
                grid.Columns[9].Header = "Клиент";

            }
            else
            {
                MessageBox.Show("Строк не найдено");
                reader.Close();

            }



        }


        private void sold_cars(object sender, RoutedEventArgs e)
        {
            List<sold_models> models_record_list = new List<sold_models>();
            string sqlExpression = " SELECT TOP(100) PERCENT dbo.BRANDS.name_brand, dbo.MODELS.name_model, COUNT(dbo.ORDERS.id_order) AS counter "
            + "FROM dbo.MODELS INNER JOIN "
            + " dbo.BRANDS ON dbo.MODELS.id_brand = dbo.BRANDS.id_brand INNER JOIN "
            + " dbo.VEHICLES ON dbo.MODELS.id_brand = dbo.VEHICLES.id_brand AND dbo.MODELS.id_model = dbo.VEHICLES.id_model INNER JOIN "
            + " dbo.ORDERS ON dbo.VEHICLES.id_vehicle = dbo.ORDERS.id_vehicle"
            + " GROUP BY dbo.BRANDS.name_brand, dbo.MODELS.name_model";
            

            SqlCommand command = new SqlCommand(sqlExpression, connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {


                while (reader.Read())
                {

                    sold_models st_rec = new sold_models();
                    
                    st_rec.BRAND = reader.GetString(0);
                    st_rec.MODEL = reader.GetString(1);
                    st_rec.COUNT = reader.GetInt32(2);
                    
                    models_record_list.Add(st_rec);

                    

                }
                reader.Close();
                grid.ItemsSource = models_record_list;
                grid.Columns[0].Header = "Марка";
                grid.Columns[1].Header = "Модель";
                grid.Columns[2].Header = "Количество";
            }
            else
            {
                MessageBox.Show("Строк не найдено");
                reader.Close();

            }
            



        }

        private void sellstat(object sender, RoutedEventArgs e)
        {
            List<stat_employees> stat_prices_orders = new List<stat_employees>();

            string sqlExpression = " SELECT dbo.EMPLOYEES.id_employee, dbo.EMPLOYEES.last_name_employee, LEFT(dbo.EMPLOYEES.first_name_employee, 1) + '.' + LEFT(dbo.EMPLOYEES.middle_name_employee, 1) + '.' AS short_name, COUNT(dbo.ORDERS.id_order) "
               + " AS counter, SUM(dbo.VEHICLES.price_vehicle) AS all_price "
           + " FROM dbo.EMPLOYEES INNER JOIN "
              + " dbo.ORDERS ON dbo.EMPLOYEES.id_employee = dbo.ORDERS.id_employee INNER JOIN "
               + " dbo.VEHICLES ON dbo.ORDERS.id_vehicle = dbo.VEHICLES.id_vehicle "
               + " GROUP BY dbo.EMPLOYEES.id_employee, dbo.EMPLOYEES.last_name_employee, LEFT(dbo.EMPLOYEES.first_name_employee, 1) + '.' + LEFT(dbo.EMPLOYEES.middle_name_employee, 1) + '.' ";

            SqlCommand command = new SqlCommand(sqlExpression, connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {


                while (reader.Read())
                {

                    stat_employees st_rec = new stat_employees();

                    st_rec.LOGIN = reader.GetString(0);
                    st_rec.LAST_NAME = reader.GetString(1);
                    st_rec.INITIALS = reader.GetString(2);
                    st_rec.COUNT_ORDERS = reader.GetInt32(3);
                    st_rec.MONEY = (reader.GetSqlMoney(4)).ToDouble();
                    
                    stat_prices_orders.Add(st_rec);

                }
                reader.Close();
                grid.ItemsSource = stat_prices_orders;
                grid.Columns[0].Header = "Логин";
                grid.Columns[1].Header = "Фамилия";
                grid.Columns[2].Header = "Инициалы";
                grid.Columns[3].Header = "Количество заказов";
                grid.Columns[4].Header = "Прибыль";



            }
            else
            {
                MessageBox.Show("Строк не найдено");
                reader.Close();

            }


        }

        private void all_employees(object sender, RoutedEventArgs e)
        {
            List<all_employees> all_employees_list = new List<all_employees>();
            string sqlExpression = "select * FROM EMPLOYEES";

            SqlCommand command = new SqlCommand(sqlExpression, connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {


                while (reader.Read())
                {

                    all_employees st_rec = new all_employees();

                    st_rec.LOGIN = reader.GetString(0);
                    st_rec.LAST_NAME = reader.GetString(1);
                    st_rec.FIRST_NAME = reader.GetString(2);
                    st_rec.MIDDLE_NAME = reader.GetString(3);
                    st_rec.DOB = (reader.GetDateTime(4)).ToString("dd.MM.yyyy");
                    st_rec.PHONE = reader.GetString(5);
                    st_rec.PASSPORT = reader.GetString(6);
                    st_rec.EMAIL = reader.GetString(7);
                    all_employees_list.Add(st_rec);



                }
                reader.Close();
                grid.ItemsSource = all_employees_list;
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
                MessageBox.Show("Строк не найдено");
                reader.Close();

            }

        }
    }
}
