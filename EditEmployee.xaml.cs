using System.Windows;
using System.Data.SqlClient;

namespace MotorCarsMoscowD
{

    public partial class EditEmployee : Window
    {

        private Window parent_window;
        private SqlConnection connection;
        public EditEmployee(SqlConnection connection, Window parent_window, int id_role)
        {
            InitializeComponent();
            this.Show();
            this.parent_window = parent_window;
            this.connection = connection;
            parent_window.Visibility = Visibility.Hidden;


            edit_profile.Click += (e, w) => new  EditProfile(connection, this);
            edit_all_profiles.Click += (e, w) => new EditAllProfiles(connection, this, id_role);

           
            
            this.connection = connection;
            if (id_role == 1)
            {

                edit_profile.Visibility = Visibility.Visible;
                edit_all_profiles.Visibility = Visibility.Visible;

            }
            else
            {
               
               

            }
            
            this.Show();



        }

        private void Click_back(object sender, RoutedEventArgs e)
        {
            parent_window.Visibility = Visibility.Visible;
            this.Close();
        }

    }
}
