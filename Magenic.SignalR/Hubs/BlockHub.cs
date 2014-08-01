using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Magenic.SignalR.Hubs
{
    public class BlockHub : Hub
    {
        public void ImageMoved(int x, int y)
        {
            Clients.Others.MoveImage(x, y);
        }
    }
}