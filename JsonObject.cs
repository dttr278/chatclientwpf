using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatClient
{

    class Token
    {
        public string token;
    }
    class Contact
    {
        public string contact_id;
        public string name;
        public string chat_id;
        public string avatar;
    }

    class Contacts
    {
        public List<Contact> result;
    }

    class Chat
    {
        public string contact_id;
        public string name;
        public string chat_id;
        public string avatar;
        public string send_time;
        public string message_id;
        public string body;
        public string sender;
        public string sender_name;
        public bool is_group;
        public string seen_time;

    }
    class Chats
    {
        public List<Chat> result;
    }

    //class Group
    //{
    //    //public string contact_id;
    //    public string name;
    //    public string chat_id;
    //    //public string avatar;
    //    public string send_time;
    //    public string message_id;
    //    public string body;
    //    public string sender;
    //    public string sender_name;
    //    public bool is_group;
    //    public string seen_time;

    //}
    //class Groups
    //{
    //    public List<Group> result;
    //}

    class Search
    {
        public string id;
        public string avatar;
        public string sex;
        public string name;
        public string user_name;
    }
    class Searchs
    {
        public List<Search> result;
    }

    class User
    {
        public string id;
        public string name;
        public string username;
        public string sex;
        public string birthday;
        public string email;
        public string phone;
        public string avatar;
        public string chat_id;
    }
    class GetUser
    {
       public User result;
    }
    class Message
    {
        public string message_id;
        public string body;
        public string send_time;
        public string seen_time;
        public string sender;
        public string name;
        public string file_id;
    }
    class Messages
    {
        public List<Message> result;
    }
    class RsMessage
    {
        public Message result;
    }

}
