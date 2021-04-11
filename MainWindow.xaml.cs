
using System.Data.SqlClient;
using System.Windows;




namespace MotorCarsMoscowD
{

    public partial class MainWindow : Window
    {
        private SqlConnection connection;
        public MainWindow()
        {
            InitializeComponent();
           

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (login_text.Text.Length > 0 && password_text.Password.Length > 0)
            {
                /*connection.Open();*/
                SqlParameter data_user_par = new SqlParameter("@data_user", login_text.Text);
                SqlParameter pass_word_par = new SqlParameter("@password", password_text.Password);
                string sqlExpression = "SELECT id_user FROM dbo.USERS WHERE(id_user = @data_user AND pass_word = @password)";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add(data_user_par);
                command.Parameters.Add(pass_word_par);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Close();
                    sqlExpression = "SELECT dbo.USERS.id_user, dbo.USERS.id_role, dbo.EMPLOYEES.first_name_employee, " +
                        "dbo.EMPLOYEES.middle_name_employee FROM  dbo.USERS INNER JOIN dbo.EMPLOYEES " +
                        "ON dbo.USERS.id_user = dbo.EMPLOYEES.id_employee  WHERE (dbo.USERS.id_user = @data_user)";
                    command = new SqlCommand(sqlExpression, connection);
                    data_user_par = new SqlParameter("@data_user", login_text.Text);
                    command.Parameters.Add(data_user_par);
                    reader = command.ExecuteReader();

                    reader.Read();
                    
                    string id_user = reader.GetString(0);
                    int id_role = reader.GetInt32(1);
                    string first_name = reader.GetString(2);
                    string middle_name = reader.GetString(3);
                    reader.Close();
                    new menu_actions(connection,  id_user, id_role, first_name, middle_name, this);
                }
                else {
                    er_mes.Visibility = Visibility.Visible;
                    reader.Close();

                }
            }
        }

        

        private void to_connect_Click(object sender, RoutedEventArgs e)
        {

            
                string par_database = name_server.Text;
            if (par_database.Length != 0)
            {
                try
                {
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
                    {
                        DataSource = par_database,
                        InitialCatalog = "MotorCarsMoscow",
                        IntegratedSecurity = true
                    };
                    connection = new SqlConnection(builder.ConnectionString);
                }
                catch (SqlException er) {
                    MessageBox.Show("Ошибка подключения" + er.ToString());
                }
            }
            connection.Open();
            lab_name_server.Visibility = Visibility.Hidden;
            name_server.Visibility = Visibility.Hidden;
            to_connect.Visibility = Visibility.Hidden;

            lab_name_user.Visibility = Visibility.Visible;
            lab_password.Visibility = Visibility.Visible;
            login_text.Visibility = Visibility.Visible;
            button_log_in.Visibility = Visibility.Visible;
            password_text.Visibility = Visibility.Visible;

        }
    }
}
