using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows;


namespace chatClient
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
       
        private async void btnSignin_Click(object sender, RoutedEventArgs e)
        {
            //ChatWindow c = new ChatWindow();
            //c.Show();
            if(txbUsername.Text.Trim()==""|| txbPassword.Password.Trim() == "")
            {
                MessageBox.Show("Tài khoản và mật khẩu không được để trống!");
                return;
            }
               
            try
            {
                var tk = await Service.MakeRequestAsync<Token>("get", "login/" + txbUsername.Text + "/" + txbPassword.Password, null);
                Service.token = tk.token;
                await Service.GetUserInfo();

                Nav wd1 = new Nav();
                wd1.Owner = this;
                wd1.Show();
                this.Hide();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác!");
            }
            
            //MessageBox.Show(tk.token);
          

        }

        private void linkToSignup_Click(object sender, RoutedEventArgs e)
        {
            grSignin.Visibility = Visibility.Hidden;
            grSignup.Visibility = Visibility.Visible;
        }

        private void linkToSignin_Click(object sender, RoutedEventArgs e)
        {
            grSignin.Visibility = Visibility.Visible;
            grSignup.Visibility = Visibility.Hidden;
        }
    }
   
}
