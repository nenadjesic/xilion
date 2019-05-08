using System;

namespace Xilion.Models.Content
{
    [Serializable]
    public delegate void ContentEventHandler(object sender, ContentEventArgs e);
}