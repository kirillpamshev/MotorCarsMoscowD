
using System.Windows;
using System.Data.SqlClient;


namespace MotorCarsMoscowD
{

    public partial class menu_actions : Window
    {
        private string id_user;
        private SqlConnection connection;
        MainWindow parent;
        public menu_actions(SqlConnection connection, string id_user, int id_role, string first_name, string middle_name, MainWindow parent)
        {
            
            InitializeComponent();
            this.parent = parent;
            show_information.Click += (e, w) => new ShowStat(connection, this);
            new_employee.Click += (e, w) => new AddEmployee(connection, this);
            edit_employee.Click += (e, w) => new EditEmployee(connection, this, id_role);
            
            add_car.Click += (e, w) => new AddCar(connection, this);
            sell_car.Click += (e, w) => new SellCar(connection, this);

            welcom.Content = $"{first_name} {middle_name}!";
            this.id_user = id_user;
            this.connection = connection;
            if (id_role == 1)
            {
                show_information.Visibility = Visibility.Visible;
                new_employee.Visibility = Visibility.Visible;
                edit_employee.Visibility = Visibility.Visible;

            }
            else {
                Manager.ID_SELLER = this.id_user;
                add_car.Visibility = Visibility.Visible;
                sell_car.Visibility = Visibility.Visible;
                edit_car.Visibility = Visibility.Visible;
                
            }
            parent.Visibility = Visibility.Hidden;
            this.Show();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            parent.Visibility = Visibility.Visible;
            this.Close();
        }

    }
}
