using Newtonsoft.Json;
using Services.Cpp.CLI;
using System.Windows;

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
            string str = "{ ";
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
                string json = wrapper.GetById(employeeId.Text);

                Employee employee = JsonConvert.DeserializeObject<Employee>(json);

                name.Text = employee.Name;
                joiningDate.Text = employee.JoiningDate;
                companyName.Text = employee.CompanyName;
                address.Text = employee.Address;
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
