using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace chatClient
{
    class ContactView : Grid
    {
        public Image AvatarImage { get; set; }
        public Label Name { get; set; }
        public Label Message { get; set; }
        public Label Time { get; set; }
        public string AvatarId { get; set; }
        public string ChatId { get; set; }
        public string ContactId { get; set; }
        public string MessageId { get; set; }
        public bool IsGroup { get; set; }
        public ContactView(int avatarW,string avatarId)
        {

            this.AvatarId = avatarId;

            IsGroup = false;
            this.AvatarImage = new Image();
            this.AvatarImage.Width = avatarW;
            this.AvatarImage.Height = avatarW;
            this.AvatarImage.Stretch = Stretch.Fill;
            this.AvatarImage.Clip = new EllipseGeometry(new Point(20,20),20,20);
            this.AvatarImage.StretchDirection = StretchDirection.Both;
            this.Name = new Label();
            this.Message = new Label();
            this.Time = new Label();
            Name.FontWeight = FontWeights.Bold;
            Time.FontSize = 8;

            ColumnDefinition avatarColumn = new ColumnDefinition();
            ColumnDefinition infoColumns = new ColumnDefinition();
            avatarColumn.Width = new System.Windows.GridLength(avatarW);


            RowDefinition row1 = new RowDefinition();

            this.RowDefinitions.Add(row1);

            this.ColumnDefinitions.Add(avatarColumn);
            this.ColumnDefinitions.Add(infoColumns);

            Grid info = new Grid();
            ColumnDefinition column1 = new ColumnDefinition();
            ColumnDefinition column2 = new ColumnDefinition();
            column2.Width = new System.Windows.GridLength(30);


            RowDefinition r1 = new RowDefinition();
            RowDefinition r2 = new RowDefinition();
            info.RowDefinitions.Add(r1);
            info.RowDefinitions.Add(r2);
            info.ColumnDefinitions.Add(column1);
            info.ColumnDefinitions.Add(column2);
            //info.ShowGridLines = true;

            Grid.SetColumn(this.Name, 0);
            Grid.SetRow(this.Name, 0);

            Grid.SetColumn(this.Time, 1);
            Grid.SetRow(this.Time, 0);

            Grid.SetColumn(this.Message, 0);
            Grid.SetRow(this.Message, 1);
            Grid.SetColumnSpan(this.Message, 2);

            info.Children.Add(this.Name);
            info.Children.Add(this.Time);
            info.Children.Add(this.Message);

            Grid.SetRow(info, 0);
            Grid.SetColumn(info, 1);
            this.Children.Add(info);

            Grid.SetRow(AvatarImage, 0);
            Grid.SetColumn(AvatarImage, 0);
            this.Children.Add(AvatarImage);

            //this.ShowGridLines = true;
           
            //info.ShowGridLines = true;

            this.Loaded += LoadAsync;
        }
        public async void LoadAsync(object sender, EventArgs args)
        {
            if (AvatarId != null&& AvatarId != "")
                AvatarImage.Source = await Service.LoadImage(new Uri(Service.URI + "/file/download/" + AvatarId));
            else
                if(!IsGroup)
                    AvatarImage.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/44.jpg", UriKind.Absolute));
                else
                    AvatarImage.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/11.jpg", UriKind.Absolute));
        }

     
    }
}
