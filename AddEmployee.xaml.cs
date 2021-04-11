using System;
using System.Collections.Generic;
using System.Windows;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Net.Mail;

namespace MotorCarsMoscowD
{

    public partial class AddEmployee : Window
    {
        private Window parent_window;
        private SqlConnection connection;

        public AddEmployee(SqlConnection connection, Window parent_window)
        {
            InitializeComponent();
            this.Show();
            this.parent_window = parent_window;
            this.connection = connection;
            parent_window.Visibility = Visibility.Hidden;
        }

        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            parent_window.Visibility = Visibility.Visible;
            this.Close();

        }

        private void gonext_Click(object sender, RoutedEventArgs e)
        {
            hideAllEr();
            bool er = false;
            string error_msg = "";
            if ((error_msg = check_login(login_text.Text)) != "")
            {
                er = true;
                login_er.Content = error_msg;
                login_er.Visibility = Visibility.Visible;
            }
            if ((error_msg = check_surname(surname.Text)) != "")
            {
                er = true;
                surname_er.Content = error_msg;
                surname_er.Visibility = Visibility.Visible;
            }
            if ((error_msg = check_name(first_name.Text)) != "")
            {
                er = true;
                first_name_er.Content = error_msg;
                first_name_er.Visibility = Visibility.Visible;
            }
            if ((error_msg = check_name(middle_name.Text)) != "")
            {
                er = true;
                middle_name_er.Content = error_msg;
                middle_name_er.Visibility = Visibility.Visible;
            }

            if ((error_msg = check_phone(phone.Text)) != "")
            {
                er = true;
                phone_er.Content = error_msg;
                phone_er.Visibility = Visibility.Visible;
            }

            if ((error_msg = check_dob(dob.DisplayDate)) != "")
            {
                er = true;
                dob_er.Content = error_msg;
                dob_er.Visibility = Visibility.Visible;
            }

            if ((error_msg = check_passport(passport.Text)) != "")
            {
                er = true;
                passport_er.Content = error_msg;
                passport_er.Visibility = Visibility.Visible;
            }

            if ((error_msg = check_email(email.Text)) != "")
            {
                er = true;
                email_er.Content = error_msg;
                email_er.Visibility = Visibility.Visible;
            }

            if ((error_msg = check_password(new_password.Password, re_password.Password)) != "")
            {
                er = true;
                repassword_er.Content = error_msg;
                repassword_er.Visibility = Visibility.Visible;
            }
            if (!er)
            {
                string login_trm = login_text.Text.Trim();
                string surname_trm = surname.Text.Trim();
                string name_trm = first_name.Text.Trim();
                string middle_name_trm = middle_name.Text.Trim();
                DateTime dob_trm = dob.DisplayDate;
                string phone_trm = phone.Text.Trim();
                string passport_trm = passport.Text.Trim();
                string email_trm = email.Text.Trim();
                string new_password_trm = new_password.Password;
                char gender = ((bool)m_gender.IsChecked) ? 'м' : 'ж';

                try {
                    string sqlExpression = " INSERT INTO EMPLOYEES VALUES (@login_value, @surname_value, @name_value, @middle_name_value, @dob_value, @phone_value, @passport_value, @email_value, @gender_value) ";
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

                    sqlExpression = " INSERT INTO USERS VALUES (@login_value2, 2 , @password_value) ";
                    command = new SqlCommand(sqlExpression, connection);
                    SqlParameter login_param2 = new SqlParameter("@login_value2", login_trm);
                    command.Parameters.Add(login_param2);
                    SqlParameter password_param = new SqlParameter("@password_value", new_password_trm);
                    command.Parameters.Add(password_param);
                    command.ExecuteNonQuery();

                    login_text.Text = "";
                    surname.Text = "";
                    first_name.Text = "";
                    middle_name.Text = "";
                    dob.SelectedDate = null;
                    phone.Text = "";
                    passport.Text = "";
                    email.Text = "";
                    new_password.Password = "";
                    re_password.Password = "";

                    MessageBox.Show("Данные добавлены!");
                }

                catch (SqlException en)
                {
                    MessageBox.Show((en.Number).ToString() + " " + en.Message);
                }

            }
        }
        private void hideAllEr()
        {
            login_er.Visibility = Visibility.Hidden;
            surname_er.Visibility = Visibility.Hidden;
            first_name_er.Visibility = Visibility.Hidden;
            middle_name_er.Visibility = Visibility.Hidden;
            dob_er.Visibility = Visibility.Hidden;
            phone_er.Visibility = Visibility.Hidden;
            passport_er.Visibility = Visibility.Hidden;
            email_er.Visibility = Visibility.Hidden;
            repassword_er.Visibility = Visibility.Hidden;
        }




        public static string check_login(string login) {

            if (login.Length == 0)
                return "Значение не задано";
            string login_local = login.Trim();
            if (login_local.Length == 0)
                return "Заданы одни пробелы";
            if (login_local.Length > 50)
                return "Слишком длинное имя";
            Regex regex = new Regex(@"^([a-z0-9])+(_|-)?([a-z0-9])*$");
            if (!regex.IsMatch(login_local))
                return "Недопустимый символ";
            else
                return "";
        }

        public static string check_name(string name)
        {
            if (name.Length == 0)
                return "Значение не задано";
            string name_local = name.Trim();
            if (name_local.Length == 0)
                return "Заданы одни пробелы";
            if (name_local.Length > 50)
                return "Слишком длинное имя";
            Regex regex = new Regex(@"^[A-zА-яЁё]+$");
            if (!regex.IsMatch(name_local))
                return "Недопустимый символ";
            return "";
        }

        public static string check_surname(string name)
        {
            if (name.Length == 0)
                return "Значение не задано";
            string name_local = name.Trim();
            if (name_local.Length == 0)
                return "Заданы одни пробелы";
            if (name_local.Length > 50)
                return "Слишком длинное имя";
            Regex regex = new Regex(@"^([A-zА-яЁё]+)((-|\s)[A-zА-яЁё]+)*$");
            if (!regex.IsMatch(name_local))
                return "Недопустимый символ";
            return "";
        }
        public static string check_dob(DateTime date)
        {
            DateTime now = DateTime.Today;
            int year_dif = now.Year - date.Year;
            if (year_dif < 18 || year_dif > 100)
                return "Неверная дата";
            return "";
        }

        public static string check_phone(string phone)
        {
            if (phone.Length == 0)
                return "Значение не задано";
            string phone_local = phone.Trim();
            if (phone_local.Length == 0)
                return "Заданы одни пробелы";
            if (phone_local.Length != 14)
                return "+7(XXX)XXXXXXX";
            Regex regex = new Regex(@"^\+7\(\d{3}\)\d{7}$");
            if (!regex.IsMatch(phone_local))
                return "Недопустимый символ";
            return "";

            
        }

        public static string check_passport(string passport)
        {

            if (passport.Length == 0)
                return "Значение не задано";
            string passport_local = passport.Trim();
            if (passport_local.Length == 0)
                return "Заданы одни пробелы";
            if (passport_local.Length != 10)
                return "SSSSNNNNNN";
            Regex regex = new Regex(@"^\d{10}$");
            if (!regex.IsMatch(passport_local))
                return "Недопустимый символ";
            return "";
        }

        public static string check_email(string email)
        {

            if (email.Length == 0)
                return "Значение не задано";
            string email_local = email.Trim();
            if (email_local.Length == 0)
                return "Заданы одни пробелы";
            if (email_local.Length > 30)
                return "Слишком длинный адрес";
            try
            {
                MailAddress m = new MailAddress(email);
                return "";
            }
            catch (FormatException)
            {
                return "Неверный адрес!";
            }
        }

        private static string check_password(string new_password, string re_password)
        {
            if (new_password != re_password)
                return "Пароли не совпадают";
            if (new_password.Length == 0)
                return "Значение не задано";  
            if (new_password.Length > 80)
                return "Слишком длинный пароль";
            if (CheckPasswordStrength(new_password) < 50)
                return "Слабый пароль";
            return "";
        }

        public static int CheckPasswordStrength(string password)
        {
            double multi0 = 1.0;
            double multi1;
            double multi2 = 1.0;
            double multi3 = 0;
            int score = 0;
            string[] badPasswords = new string[]
        {
            "123456", "123456789", "qwerty", "111111", "1234567", "666666", "12345678", "7777777", "123321", "0", "654321", "1234567890", "123123", "555555", "vkontakte", "gfhjkm", "159753", "777777", "TempPassWord", "qazwsx", "1q2w3e", "1234", "112233", "121212", "qwertyuiop", "qq18ww899", "987654321", "12345", "zxcvbn", "zxcvbnm", "999999", "samsung", "ghbdtn", "1q2w3e4r", "1111111", "123654", "159357", "131313", "qazwsxedc", "123qwe", "222222", "asdfgh", "333333", "9379992", "asdfghjkl", "4815162342", "12344321", "любовь", "88888888", "11111111", "knopka", "пароль", "789456", "qwertyu", "1q2w3e4r5t", "iloveyou", "vfhbyf", "marina", "password", "qweasdzxc", "10203", "987654", "yfnfif", "cjkysirj", "nikita", "888888", "йцукен", "vfrcbv", "k.,jdm", "qwertyuiop[]", "qwe123", "qweasd", "natasha", "123123123", "fylhtq", "q1w2e3", "stalker", "1111111111", "q1w2e3r4", "nastya", "147258369", "147258", "fyfcnfcbz", "1234554321", "1qaz2wsx", "andrey", "111222", "147852", "genius", "sergey", "7654321", "232323", "123789", "fktrcfylh", "spartak", "admin", "test", "123", "azerty", "abc123", "lol123", "easytocrack1", "hello", "saravn", "holysh!t", "1", "Test123", "tundra_cool2", "456", "dragon", "thomas", "killer", "root", "1111", "pass", "master", "aaaaaa", "a", "monkey", "daniel", "asdasd", "e10adc3949ba59abbe56e057f20f883e", "changeme", "computer", "jessica", "letmein", "mirage", "loulou", "lol", "superman", "shadow", "admin123", "secret", "administrator", "sophie", "kikugalanetroot", "doudou", "liverpool", "hallo", "sunshine", "charlie", "parola", "100827092", "/", "michael", "andrew", "password1", "****you", "matrix", "cjmasterinf", "internet", "hallo123", "eminem", "demo", "gewinner", "pokemon", "abcd1234", "guest", "ngockhoa", "martin", "sandra", "asdf", "hejsan", "george", "qweqwe", "lollipop", "lovers", "q1q1q1", "tecktonik", "naruto", "12", "password12", "password123", "password1234", "password12345", "password123456", "password1234567", "password12345678", "password123456789", "000000", "maximius", "123abc", "baseball1", "football1", "soccer", "princess", "slipknot", "11111", "nokia", "super", "star", "666999", "12341234", "1234321", "135790", "159951", "212121", "zzzzzz", "121314", "134679", "142536", "19921992", "753951", "7007", "1111114", "124578", "19951995", "258456", "qwaszx", "zaqwsx", "55555", "77777", "54321", "qwert", "22222", "33333", "99999", "88888", "66666",
            "iloveu", "пароль"
        };
            foreach (var bp in badPasswords)
            {
                if (password.ToLower().Contains(bp.ToLower()))
                    multi0 = 0.75;
                else
                    if (bp.ToLower() == password.ToLower())
                    multi0 = 0.125;
            }
            List<char> usedChars = new List<char>();
            foreach (var chr in password)
            {
                if (!usedChars.Contains(chr))
                    usedChars.Add(chr);
            }
            multi1 = FrequencyFactor(password.ToLower());

            score += password.Length * 15;

            Dictionary<string, double> patterns = new Dictionary<string, double> { { @"1234567890", 0.0 }, //включает цифры
                                                                         { @"[a-z]", 0.1 }, //буквы низ. регистра
                                                                         { @"[ёа-я]", 0.2 }, //буквы низ. регистра
                                                                         { @"[A-Z]", 0.2 }, //буквы выс. регистра
                                                                         { @"[ЁА-Я]", 0.3 }, //буквы выс. регистра
                                                                         {  "[!,@#\\$%\\^&\\*?_~=;:'\"<>[]()~`\\\\|/]", 0.4 },  //символы
                                                                         { @"[¶©]", 0.5} }; //Спецсимволы

            foreach (var pattern in patterns)
                if (Regex.Matches(password, pattern.Key).Count > 0)
                    multi2 += pattern.Value;

            if (password.Length > 2)
                multi3 += 0;
            if (password.Length > 4)
                multi3 += 0.25;
            if (password.Length > 6)
                multi3 += 0.75;
            if (password.Length > 8)
                multi3 += 1.0;


            return (int)(score * multi0 * multi1 * multi2);
        }

        static double FrequencyFactor(string password)
        {
            HashSet<char> differentSymbols = new HashSet<char>(password.ToCharArray());

            return Map(differentSymbols.Count, 1.0, password.Length, 0.1, 1);
        }

        static double Map(double value, double fromLower, double fromUpper, double toLower, double toUpper)
        {
            return toLower + (value - fromLower) / (fromUpper - fromLower) * (toUpper - toLower);
        }

       
  
    }
}
