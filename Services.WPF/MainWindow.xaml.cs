using Services.Cpp.CLI;
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

namespace Services.WPF
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

        private void getEmployees_Click(object sender, RoutedEventArgs e)
        {
            using (Service wrapper = new Service())
            // Using block is here to make sure we release native memory right away
            {
                MessageBox.Show(wrapper.Get());
            }
        }

        private void addEmployee_Click(object sender, RoutedEventArgs e)
        {
            String str = "{ ";
            str += "\"EmployeeID\": \"" + employeeId.Text;
            str += "\", \"Name\": \"" + name.Text;
            str += "\", \"JoiningDate\": \"" + joiningDate.Text;
            str += "\", \"CompanyName\": \"" + companyName.Text;
            str += "\", \"Address\": \"" + address.Text;
            str += "\" }";

            using (Service wrapper = new Service())
            {
                wrapper.Add(str);
            }
        }

        private void getEmployeeById_Click(object sender, RoutedEventArgs e)
        {
            using (Service wrapper = new Service())
            {
                MessageBox.Show(wrapper.GetById(employeeId.Text));
            }
        }

        private void deleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            using (Service wrapper = new Service())
            {
                wrapper.Delete(employeeId.Text);
            }
        }
    }
}
