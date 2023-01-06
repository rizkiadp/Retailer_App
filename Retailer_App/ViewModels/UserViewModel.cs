using System;
using System.Windows;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Retailer_App.Setup;
using Retailer_App.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
namespace Retailer_App.ViewModels
{
    public class UserViewModels : INotifyPropertyChanged
    {
        public event Action OnCallBack;
        private readonly Db_Connection dbconn;
        private ObservableCollection<User> collection;
        private User model;
        private bool check;

        private async Task ReadDataAsync()
        {
            dbconn.OpenConnection();
            await Task.Delay(0);
            var query = "SELECT * FROM users";
            var sqlcmd = new SqlCommand(query, dbconn.SqlConnect);
            var sqlresult = sqlcmd.ExecuteReader();
            if (sqlresult.HasRows)
            {
                collection.Clear();
                while (sqlresult.Read())
                {
                    collection.Add(new User
                    {
                        Uid = sqlresult[0].ToString(),
                        Name = sqlresult[1].ToString(),
                        UserName = sqlresult[2].ToString(),
                        Keypass = sqlresult[3].ToString(),
                        Status = (sqlresult[4].ToString() == "1") ?
                        "Active" :
                        "Not Active",
                    });
                }
            }
            dbconn.CloseConnection();
            OnCallBack?.Invoke();

        }
        private async Task UpdateDataAsync()
        {
            if (isValidating())
            {
                dbconn.OpenConnection();
                var state = check ? "1" : "0";
                var query = $"UPDATE users ser" +
                            $"name = '{model.Name}'," +
                            $"username = '{model.UserName}'," +
                            $"Keypass = '{model.Keypass}'," +
                            $"Status = '{state}'," +
                            $"WHERE uid = '{model.Uid}'";
                var sqlcmd = new SqlCommand(query, dbconn.SqlConnect);
                sqlcmd.ExecuteNonQuery();
                dbconn.CloseConnection();
                await ReadDataAsync();
            }
        }
        private async Task DeleteDataAsync()
        {
            if (isValidating())
            {
                var msg = MessageBox.Show("Are You Sure?", "Question",
                            MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (msg == MessageBoxResult.Yes)
                {
                    dbconn.OpenConnection();
                    var query = $"DELETE FROM users WHERE uid = '{model.Uid}'";
                    var sqlcmd = new SqlCommand(query, dbconn.SqlConnect);
                    sqlcmd.ExecuteNonQuery();
                    dbconn.CloseConnection();
                }
                await ReadDataAsync();
            }
        }

        private bool isValidating()
        {
            var flag = true;
            if (model.Name == null)
            {
                MessageBox.Show("Text 1 Cannot Empty", "Warning",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                flag = false;
            } else if (model.UserName == null)
            {
                MessageBox.Show("Text 2 Cannot Empty", "Warning",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
            } else if (model.Keypass == null)
            {
                MessageBox.Show("Text 2 Cannot Empty", "Warning",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                flag = false;
            }
            return flag;
        }
        public RelayCommand InsertCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand SelectCommand { get; set; }

        public ObservableCollection<User> Collection
        {
            get { return collection; 
        }
        set{
                collection = value;
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(null));
            }
}
        public User Model
        {
            get { return model;

            }
            set { 
                if(value != null)
                {
                    IsChecked = (value.Status == "Activate") ? true : false;
                }
                model = value;
                PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(null));
            }
  }

        private bool IsChecked
        {
            get { return check;
            }
            set
            {
                check = value;
                PropertyChanged?.Invoke(this,
               new PropertyChangedEventArgs(null));
            }
        }

        public UserViewModels()
        {
            collection = new ObservableCollection<User>();
            dbconn = new Db_Connection();
            model = new User();

            UpdateCommand = new RelayCommand(async () => await UpdateDataAsync());
            DeleteCommand = new RelayCommand(async () => await DeleteDataAsync());

        }
    public event PropertyChangedEventHandler PropertyChanged;
    }
}

