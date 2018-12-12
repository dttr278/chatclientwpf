using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
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

namespace chatClient
{
    /// <summary>
    /// Interaction logic for ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        string chatId,name,messageId;
        System.Windows.Threading.DispatcherTimer dispatcherTimer;
        public ChatWindow(string cid,string n)
        {
            InitializeComponent();

            chatId = cid;
            name = n;

        //    listMessage.Background = Brushes.Aquamarine;
            //MessageView ms = new MessageView();
            //ms.name.Text = "Do Tan Trung:";
            //ms.message.Text = "hew ahgdsa dhsagdsa dhsagdsa dhsagdhsa";
            //ms.time.Text = "T2 20:02";
            //ms.Width = 200;
            //listMessage.Items.Add(ms);
            this.Loaded += LoadAsync;
            this.Closing += ChatWindow_Closing;
        }
        public async void LoadAsync(object sender, EventArgs args)
        {
            var mss = await Service.MakeRequestAsync<Messages>("get", "messages/"+chatId,null);
            messageId = mss.result[mss.result.Count - 1].message_id;
            for (int i=0;i<mss.result.Count;i++)
            { 
                Message mess = mss.result[i];
                Border ms = await LoadMsAsync(mess);
                listMessage.add(ms);

            }
            lbName.Content = this.name;
            msScroll.ScrollToEnd();
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 3);
            dispatcherTimer.Start();

           
        }

        private void ChatWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            dispatcherTimer.Stop();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            LoadNewMSAsync();
        }
        public async void LoadNewMSAsync()
        {
            try
            {
                var mss = await Service.MakeRequestAsync<Messages>("get", "messages/" + chatId + "/9999/" + messageId, null);
                if (mss.result != null)
                {
                    messageId = mss.result[mss.result.Count - 1].message_id;
                    for (int i = 0; i < mss.result.Count; i++)
                    {
                        Message mess = mss.result[i];
                        Border ms = await LoadMsAsync(mess);
                        listMessage.add(ms);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            

        }
        async Task<Border> LoadMsAsync(Message mess)
        {
            MessageView ms = new MessageView();
            ms.name.Text = mess.name;
            if (mess.file_id.Trim() == "")
                ms.message.Text = mess.body;
            else
            {
                if (mess.body.IndexOf("image") > 0)
                {
                    Image image = new Image();
                    image.MaxWidth = 400;
                    image.Source = await Service.LoadImage(new Uri(Service.URI + "/file/download/" + mess.file_id));
                    Hyperlink link = new Hyperlink();
                    link.NavigateUri = new Uri(Service.URI + "/file/download/" + mess.file_id);
                    link.Inlines.Add(image);
                    link.RequestNavigate += Hyperlink_RequestNavigate;
                    Label lb = new Label();
                    lb.Content = link;
                    ms.body.Children.Add(lb);
                }
                else
                {
                    Hyperlink link = new Hyperlink();
                    link.NavigateUri = new Uri(Service.URI + "/file/download/" + mess.file_id);
                    TextBlock textLink = new TextBlock();
                    textLink.Text = mess.body.Substring(0, mess.body.IndexOf(":"));
                    textLink.TextWrapping = TextWrapping.Wrap;
                    link.Inlines.Add(textLink);
                    Label lb = new Label();
                    lb.Content = link;

                    ms.body.Children.Add(lb);
                    link.RequestNavigate += Hyperlink_RequestNavigate;
                }
            }
            ms.time.Text = mess.send_time;

            Border border = new Border();
            border.BorderThickness = new Thickness(1);
            border.BorderBrush = Brushes.Black;
            border.Margin = new Thickness(10);
            border.Child = ms;

            if (mess.sender.Equals(Service.user.id))
            {
                border.HorizontalAlignment = HorizontalAlignment.Right;
                border.Background = "#FFFFFF".ToBrush();
            }
            else
            {
                border.HorizontalAlignment = HorizontalAlignment.Left;
                border.Background = "#F1F0F0".ToBrush();
            }
            ms.Init();
            return border;
                 
        }

        private async void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            string ms= new TextRange(inputMessage.Document.ContentStart, inputMessage.Document.ContentEnd).Text;
            var rs = await Service.MakeRequestAsync<RsMessage>("post", "sendmessage/" + chatId, new StringContent("{\"message\":\"" + ms + "\"}", Encoding.UTF8, "application/json") );
            listMessage.add(await LoadMsAsync(rs.result));
            msScroll.ScrollToEnd();

            inputMessage.SelectAll();
            inputMessage.Selection.Text = "";
            messageId = rs.result.message_id;
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

    }
}
