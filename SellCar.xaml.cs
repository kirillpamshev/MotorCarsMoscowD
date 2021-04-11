using System.Collections.Generic;
using System.Windows;
using System.Data.SqlClient;

namespace MotorCarsMoscowD
{

    public class sell_cars
    {
       
        public string BRAND { get; set; }
        public string MODEL { get; set; }
        public string BODY { get; set; }
        public string COLOR { get; set; }
        public string YEAR { get; set; }
        public string ENG_TYPE { get; set; }
        public double VOLUME { get; set; }
        public int POWER { get; set; }
        public double PRICE { get; set; }
        public int ID_CAR { get; set; }
       
    }
    public partial class SellCar : Window
    {
        private Window parent_window;
        private SqlConnection connection;
        public SellCar(SqlConnection connection, Window parent_window)
        {
            InitializeComponent();
            this.Show();
            this.parent_window = parent_window;
            this.connection = connection;
            parent_window.Visibility = Visibility.Hidden;
            update_sell_car();

        }

        public void update_sell_car()
        {
            List<sell_cars> sell_record_list = new List<sell_cars>();

            string sqlExpression = "SELECT dbo.BRANDS.name_brand, dbo.MODELS.name_model, dbo.BODIES.description_body, dbo.COLORS.description_color, dbo.VEHICLES.year_vehicle, dbo.ENGINES.type_engine, dbo.ENGINES.volume_engine, dbo.ENGINES.power_engine,"
                 + " dbo.VEHICLES.price_vehicle, dbo.VEHICLES.id_vehicle"
                 + " FROM     dbo.BRANDS INNER JOIN"
                 + " dbo.MODELS ON dbo.BRANDS.id_brand = dbo.MODELS.id_brand INNER JOIN"
                 + " dbo.BODIES ON dbo.MODELS.id_body = dbo.BODIES.id_body INNER JOIN"
                 + " dbo.ENGINES ON dbo.MODELS.id_engine = dbo.ENGINES.id_engine INNER JOIN"
                 + " dbo.VEHICLES ON dbo.MODELS.id_brand = dbo.VEHICLES.id_brand AND dbo.MODELS.id_model = dbo.VEHICLES.id_model INNER JOIN"
                 + " dbo.COLORS ON dbo.VEHICLES.id_color = dbo.COLORS.id_color INNER JOIN"
                 + " dbo.STATUSES ON dbo.VEHICLES.id_status = dbo.STATUSES.id_status"
                 + " WHERE(dbo.STATUSES.description_status = N'В наличии')";

            try
            {
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {


                    while (reader.Read())
                    {

                        sell_cars st_rec = new sell_cars();

                        st_rec.BRAND = reader.GetString(0);
                        st_rec.MODEL = reader.GetString(1);
                        st_rec.BODY = reader.GetString(2);
                        st_rec.COLOR = reader.GetString(3);
                        st_rec.YEAR = reader.GetString(4);
                        st_rec.ENG_TYPE = reader.GetString(5);
                        st_rec.VOLUME = reader.GetDouble(6);
                        st_rec.POWER = reader.GetInt32(7);
                        st_rec.PRICE = (reader.GetSqlMoney(8).ToDouble());
                        st_rec.ID_CAR = reader.GetInt32(9);
                        sell_record_list.Add(st_rec);
                    }
                    reader.Close();

                    grid.ItemsSource = sell_record_list;
                    grid.Columns[0].Header = "Марка";
                    grid.Columns[1].Header = "Модель";
                    grid.Columns[2].Header = "Кузов";
                    grid.Columns[3].Header = "Цвет";
                    grid.Columns[4].Header = "Год выпуска";
                    grid.Columns[5].Header = "Тип двигателя";
                    grid.Columns[6].Header = "Объём";
                    grid.Columns[7].Header = "Мощность";
                    grid.Columns[8].Header = "Цена";
                    grid.Columns[9].Visibility = Visibility.Hidden;

                }
                
                else
                {
                    
                    reader.Close();
                    grid.ItemsSource = null;
                   
                }
            }

            catch (SqlException e)
            {
                MessageBox.Show("Ошибка" + e.ToString());
                this.Close();
            }


        }

        private void goBack(object sender, RoutedEventArgs e)
        {
            parent_window.Visibility = Visibility.Visible;
            this.Close();

        }

        private void sell_act(object sender, RoutedEventArgs e)
        {
            
            sell_cars car = (sell_cars)grid.SelectedItem;
            if (car != null)
            {
                int id_sell_car = car.ID_CAR;
                this.Visibility = Visibility.Hidden;
                new Client(connection, this, id_sell_car);
                
            } 
        }

        private void Details(object sender, RoutedEventArgs e)
        {
            sell_cars car = (sell_cars)grid.SelectedItem;
            if (car != null)
            {
                int id_sell_car = car.ID_CAR;
                this.Visibility = Visibility.Hidden;
                new DetailProduct(connection, this, id_sell_car);

            }
        }
    }
}
