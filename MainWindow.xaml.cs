using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace hegedusm_filmek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        string connStr = "server=localhost;user id=root;password=;database=hegedusm;charset=utf8";

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            lbAdatok.Items.Clear();
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                string sql = "SELECT * FROM filmek";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string sor = $"{reader["filmazon"]};{reader["cim"]};{reader["ev"]};{reader["szines"]};{reader["mufaj"]};{reader["hossz"]}";
                    lbAdatok.Items.Add(sor);
                }
                conn.Close();
            }
        }
        private void LbAdatok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbAdatok.SelectedItem != null)
            {
                string[] adatok = lbAdatok.SelectedItem.ToString().Split(';');
                lbFilmAzon.Content = adatok[0];
                tb1.Text = adatok[1];
                tb2.Text = adatok[2];
                tb3.Text = adatok[3];
                tb4.Text = adatok[4];
                tb5.Text = adatok[5];
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                string sql = $@"
            UPDATE filmek SET
                cim = '{tb2.Text}',
                ev = {tb3.Text},
                szines = '{tb4.Text}',
                mufaj = '{tb5.Text}',
                hossz = {tb5.Text}
            WHERE filmazon = '{lbFilmAzon.Content}'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Sikeres módosítás!");
                Button_Click(null, null);
            }
        }
    }
}
