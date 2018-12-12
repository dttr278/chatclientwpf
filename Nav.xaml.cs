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
using System.Windows.Shapes;

namespace chatClient
{
    /// <summary>
    /// Interaction logic for Contact.xaml
    /// </summary>
    public partial class Nav : Window
    {
        //Contacts contactsList;
        //Chats chatsList;
        //Chats groupsList;
        string chatMessageId, groupMessageId;
        System.Windows.Threading.DispatcherTimer dispatcherTimer;
        public Nav()
        {
            InitializeComponent();
            this.Loaded += Nav_LoadedAsync;
            this.Loaded += LoadContactsAsync;
            this.Loaded += LoadChatsAsync;
            this.Loaded += LoadGroupsAsync;
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 3);
            dispatcherTimer.Start();
            this.Closing += Nav_Closing;
            
        }

        private void Nav_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            dispatcherTimer.Stop();
        }

        private async void Nav_LoadedAsync(object sender, RoutedEventArgs e)
        {

            tbName.Text = Service.user.name;
            tbUserName.Text = "@" + Service.user.username;
            if (Service.user.avatar != null && Service.user.avatar != "")
            {
                userAvatar.Source = await Service.LoadImage(new Uri(Service.URI + "/file/download/" + Service.user.avatar));
            }
            else
            {
                userAvatar.Source= new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/44.jpg", UriKind.Absolute));
            }

        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            LoadNewGroupAsync();
            LoadNewChatAsync();
        }
        async void LoadContactsAsync(object obj, EventArgs e)
        {
            try
            {
                contactList.Items.Clear();

                Contacts contactsList = await Service.MakeRequestAsync<Contacts>("get", "contacts", null);
                for (int i = 0; i < contactsList.result.Count; i++)
                {
                    contactList.Items.Add(LoadContactView(contactsList.result[i]));
                }
            }
            catch (Exception ev)
            {
                MessageBox.Show(ev.Message);
            }

        }

        private void Ctv_MouseDown(object sender, MouseEventArgs e)
        {
            ContactView cvw = (ContactView)sender;
            ChatWindow c = new ChatWindow(cvw.ChatId,cvw.Name.Content.ToString());
            c.Owner = this;
            c.Show();
        }

        ContactView LoadChatView(Chat c)
        {
            ContactView ctv = new ContactView(40, c.avatar);
            ctv.Width = 230;
            ctv.Height = 50;
            ctv.Name.Content = c.name;
            if (c.seen_time != "")
            {
                TextBlock tb = new TextBlock();
                tb.Text = c.body;
                tb.FontWeight = FontWeights.Bold;
                tb.TextWrapping = TextWrapping.NoWrap;
                ctv.Message.Content = tb;
            }
            else
            {
                ctv.Message.Content = c.body;
            }
           
            ctv.Time.Content = c.send_time;
            ctv.IsGroup = c.is_group;
            ctv.ChatId = c.chat_id;
            ctv.MouseDown += Ctv_MouseDown;
            return ctv;
        }
        ContactView LoadContactView(Contact c)
        {
            ContactView ctv = new ContactView(40, c.avatar);
            ctv.Width = 230;
            ctv.Height = 50;
            ctv.Name.Content = c.name;
            ctv.ChatId = c.chat_id;
            ctv.MouseDown += Ctv_MouseDown;
            return ctv;
        }
        async void LoadChatsAsync(object obj, EventArgs e)
        {
            try
            {
                chatList.Items.Clear();
                Chats chatsList = await Service.MakeRequestAsync<Chats>("get", "chats", null);
                chatMessageId = chatsList.result[0].message_id;
                for (int i = 0; i < chatsList.result.Count; i++)
                {
                    chatList.Items.Add(LoadChatView(chatsList.result[i]));
                }
            }
            catch (Exception ev)
            {
                MessageBox.Show(ev.Message);
            }

        }
        async void LoadGroupsAsync(object obj, EventArgs e)
        {
            try
            {
                groupList.Items.Clear();
                Chats groupsList = await Service.MakeRequestAsync<Chats>("get", "groups", null);
                groupMessageId = groupsList.result[0].message_id;
                for (int i = 0; i < groupsList.result.Count; i++)
                {
                    groupList.Items.Add(LoadChatView(groupsList.result[i]));
                }
            }
            catch (Exception ev)
            {
                MessageBox.Show(ev.Message);
                
            }

        }

        async void LoadNewGroupAsync()
        {
            try
            {
                Chats groupsList = await Service.MakeRequestAsync<Chats>("get", "groups/9999/"+groupMessageId, null);
                if (groupsList.result == null)
                    return;
                groupMessageId = groupsList.result[groupsList.result.Count - 1].message_id;
                for (int i = 0; i < groupsList.result.Count; i++)
                {
                    for(int j=0;j< groupList.Items.Count; j++)
                    {
                        if(((ContactView)groupList.Items[j]).ChatId== groupsList.result[i].chat_id)
                        {
                            groupList.Items.RemoveAt(j);
                        }
                    }
                    groupList.Items.Insert(0,LoadChatView(groupsList.result[i]));
                }
            }
            catch (Exception ev)
            {
                MessageBox.Show(ev.Message);
            }

        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Service.token = null;
            this.Owner.Show();
            this.Close();
        }

        async void LoadNewChatAsync()
        {
            try
            {
                Chats chatsList = await Service.MakeRequestAsync<Chats>("get", "chats/9999/" + chatMessageId, null);
                if (chatsList.result == null)
                    return;
                chatMessageId = chatsList.result[chatsList.result.Count - 1].message_id;
                for (int i = 0; i < chatsList.result.Count; i++)
                {
                    for (int j = 0; j < chatList.Items.Count; j++)
                    {
                        if (((ContactView)chatList.Items[j]).ChatId == chatsList.result[i].chat_id)
                        {
                            chatList.Items.RemoveAt(j);
                        }
                    }
                    chatList.Items.Insert(0,LoadChatView(chatsList.result[i]));
                }
            }
            catch (Exception ev)
            {
                MessageBox.Show(ev.Message);
            }

        }

    }
}
