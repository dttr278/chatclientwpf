using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace chatClient
{
    class GridList : Grid
    {
        public GridList()
        {
            ColumnDefinition gridCol1 = new ColumnDefinition();
            this.ColumnDefinitions.Add(gridCol1);
        }
        public void add(UIElement element)
        {
            RowDefinition gridRow = new RowDefinition();
            gridRow.Height = GridLength.Auto;

            this.RowDefinitions.Add(gridRow);


            Grid.SetRow(element, this.Children.Count);
            Grid.SetColumn(element, 0);

            this.Children.Add(element);

        }
    }
}
