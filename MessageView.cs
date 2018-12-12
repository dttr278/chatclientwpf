using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace chatClient
{
    class MessageView:GridList
    {
        public TextBox name,message,time;
        public StackPanel body;
        public MessageView()
        {
            body = new StackPanel();
            name = new TextBox();
            message = new TextBox();
            time = new TextBox();
            name.Background = message.Background = time.Background = Brushes.Transparent;
            name.BorderThickness = message.BorderThickness = time.BorderThickness = new Thickness(0);
            name.IsReadOnly = message.IsReadOnly = time.IsReadOnly = true;
            name.TextWrapping = message.TextWrapping = time.TextWrapping = TextWrapping.Wrap;
            name.FontWeight = FontWeights.Bold;
            time.FontSize = 8;
            time.HorizontalAlignment = HorizontalAlignment.Right;
            time.VerticalAlignment = VerticalAlignment.Bottom;
            body.Children.Add(message);
           
        }
        public void Init()
        {
            this.add(name);
            this.add(body);
            this.add(time);
        }
    }
}
