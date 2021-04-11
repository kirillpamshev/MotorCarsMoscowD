using System.IO;
using System.Windows;
using System.Data.SqlClient;
using System;
using System.Windows.Media.Imaging;


namespace MotorCarsMoscowD
{
    public partial class DetailProduct : Window
    {
        private Window parent_window;
        private SqlConnection connection;
        public DetailProduct(SqlConnection connection, Window parent_window, int id_car)
        {
            InitializeComponent();
            this.Show();
            this.parent_window = parent_window;
            this.connection = connection;
            parent_window.Visibility = Visibility.Hidden;
            string sqlExpression = " SELECT dbo.BRANDS.name_brand, dbo.MODELS.name_model, dbo.COLORS.description_color, dbo.BODIES.description_body, dbo.ENGINES.type_engine, dbo.ENGINES.volume_engine, dbo.ENGINES.power_engine, dbo.MODELS.length_body, dbo.MODELS.width_body, "
            + " dbo.MODELS.height_body, dbo.VEHICLES.year_vehicle, dbo.VEHICLES.price_vehicle, dbo.VEHICLES.image_vehicle"
            + " FROM   dbo.BRANDS INNER JOIN "
            + " dbo.MODELS ON dbo.BRANDS.id_brand = dbo.MODELS.id_brand INNER JOIN "
            + "  dbo.ENGINES ON dbo.MODELS.id_engine = dbo.ENGINES.id_engine INNER JOIN "
            + " dbo.VEHICLES ON dbo.MODELS.id_brand = dbo.VEHICLES.id_brand AND dbo.MODELS.id_model = dbo.VEHICLES.id_model INNER JOIN "
            + " dbo.COLORS ON dbo.VEHICLES.id_color = dbo.COLORS.id_color INNER JOIN "
            + "  dbo.BODIES ON dbo.MODELS.id_body = dbo.BODIES.id_body "
            + " WHERE(dbo.VEHICLES.id_vehicle = " + id_car + ") ";

            try
            {
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    lab_brand.Content = reader.GetString(0) + " " + reader.GetString(1) + " " + reader.GetString(10);
                    lab_color.Content = reader.GetString(2);
                    lab_body.Content = reader.GetString(3);
                    lab_eng_type.Content = reader.GetString(4) + " " + reader.GetDouble(5) + " л. " + reader.GetInt32(6) + " л.с.";
                    lab_length.Content = "Габариты " + reader.GetInt32(7) + " " + reader.GetInt32(8) + " " + reader.GetInt32(9);
                    lab_price.Content = "Цена " + (reader.GetSqlMoney(11).ToDouble()) + " рублей ";
                    byte[] img_byte = reader[12] as byte[];

                    if (img_byte != null)
                    {

                        MemoryStream mem_stream = new MemoryStream(img_byte);
                        try
                        {
                            lab_img.Source = BitmapFrame.Create(mem_stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                        }
                        catch (NotSupportedException)
                        {
                            lab_img.Source = null;
                        }
                    }
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

        private void goBack(object sender, RoutedEventArgs e)
        {
            parent_window.Visibility = Visibility.Visible;
            this.Close();
        }
    }
}
