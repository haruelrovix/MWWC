using Newtonsoft.Json;
using Services.Cpp.CLI;
using System.Collections.Generic;
using System.Windows;

namespace Services.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// The _employees
        /// </summary>
        private List<Employee> _employees;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow" /> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            GetEmployees();
        }

        /// <summary>
        /// Gets the employees.
        /// </summary>
        private void GetEmployees()
        {
            // Using block is here to make sure we release native memory right away
            using (Service wrapper = new Service())
            {
                string value = wrapper.Get();
                if (value != Properties.Resources.InternalServerError)
                {
                    _employees = JsonConvert.DeserializeObject<List<Employee>>(value);
                    employeesDataGrid.ItemsSource = _employees;
                }
                else
                {
                    ShowErrorMessage(value);
                    MWWC.Close();
                }
            }
        }

        /// <summary>
        /// Constructs the json.
        /// </summary>
        /// <returns></returns>
        private string ConstructJSON()
        {
            return "{ " +
            "\"EmployeeID\": \"" + employeeId.Text +
            "\", \"Name\": \"" + name.Text +
            "\", \"JoiningDate\": \"" + joiningDate.Text +
            "\", \"CompanyName\": \"" + companyName.Text +
            "\", \"Address\": \"" + address.Text +
            "\" }";
        }

        /// <summary>
        /// Handles the Click event of the addEmployee control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void AddEmployee(object sender, RoutedEventArgs e)
        {
            using (Service wrapper = new Service())
            {
                wrapper.Add(ConstructJSON());
            }

            GetEmployees();
        }

        /// <summary>
        /// Handles the Click event of the updateEmployee control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void UpdateEmployee(object sender, RoutedEventArgs e)
        {
            using (Service wrapper = new Service())
            {
                if (!wrapper.Update(ConstructJSON()))
                {
                    ShowErrorMessage();
                }
                else
                {
                    GetEmployees();
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the getEmployeeById control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void GetEmployeeById(object sender, RoutedEventArgs e)
        {
            using (Service wrapper = new Service())
            {
                string json = wrapper.GetById(employeeId.Text);

                Employee employee = JsonConvert.DeserializeObject<Employee>(json);

                if (employee != null)
                {
                    name.Text = employee.Name;
                    joiningDate.Text = employee.JoiningDate;
                    companyName.Text = employee.CompanyName;
                    address.Text = employee.Address;
                }
                else
                {
                    ShowErrorMessage();
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the deleteEmployee control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void DeleteEmployee(object sender, RoutedEventArgs e)
        {
            using (Service wrapper = new Service())
            {
                if (!wrapper.Delete(employeeId.Text))
                {
                    ShowErrorMessage();
                }
                else
                {
                    GetEmployees();
                }
            }
        }

        /// <summary>
        /// Shows the error message.
        /// </summary>
        private void ShowErrorMessage()
        {
            ShowErrorMessage(string.Format(Properties.Resources.CouldNotFindEmployeeWithID, employeeId.Text));
        }

        /// <summary>
        /// Shows the error message.
        /// </summary>
        /// <param name="text">The text.</param>
        private void ShowErrorMessage(string text)
        {
            MessageBox.Show(
                text,
                Properties.Resources.Error,
                MessageBoxButton.OK,
                MessageBoxImage.Error
                );
        }

        /// <summary>
        /// Handles the MouseRightButtonDown event of the Mouse control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs" /> instance containing the event data.</param>
        private void ClearTextBox(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            employeeId.Text = string.Empty;
            name.Text = string.Empty;
            joiningDate.Text = string.Empty;
            companyName.Text = string.Empty;
            address.Text = string.Empty;
        }
    }
}
