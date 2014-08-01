
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls.Primitives;

namespace Magenic.SignalR.Wpf
{
    public class MoveThumb : Thumb
    {
        HubConnection hubConnection = new HubConnection("http://localhost:6164/");
        IHubProxy stockTickerHubProxy;

        public MoveThumb()
        {
            DragDelta += new DragDeltaEventHandler(this.MoveThumb_DragDelta);
            Control item = this.DataContext as Control;
            stockTickerHubProxy = hubConnection.CreateHubProxy("BlockHub");
            stockTickerHubProxy.On<int,int>("MoveImage",new Action<int,int>(Move));
            hubConnection.Start();
        }

        private async void MoveThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Control item = this.DataContext as Control;

            if (item != null)
            {
                double left = Canvas.GetLeft(item);
                double top = Canvas.GetTop(item);

                Canvas.SetLeft(item, left + e.HorizontalChange);
                Canvas.SetTop(item, top + e.VerticalChange);

                await stockTickerHubProxy.Invoke("ImageMoved", 
                    Convert.ToInt32(top + e.VerticalChange),
                    Convert.ToInt32(left + e.HorizontalChange));
            }
        }

        private void Move(int x, int y)
        {
            Dispatcher.Invoke(() =>
            {
                Control item = this.DataContext as Control;

                if (item != null)
                {
                    double left = Canvas.GetLeft(item);
                    double top = Canvas.GetTop(item);

                    Canvas.SetLeft(item, y);
                    Canvas.SetTop(item, x);
                }
            });
        }
    }
}
