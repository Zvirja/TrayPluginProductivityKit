using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIM.Tool.Plugins.TrayPlugin.Messaging.ClickMessages;
using TrayPluginProductivityKit.Helpers;

namespace TrayPluginProductivityKit.SuperClick
{
  public class ClickDetailsWrapper
  {
    private string clickDetailsHash;
    public MouseClickDetails ClickDetails { get; set; }
    public bool IsInstanceClick { get; set; }

    public string ClickDetailsHash
    {
      get
      {
        if(clickDetailsHash != null)
          return clickDetailsHash;
        clickDetailsHash = ClickHelper.GetMouseClickHash(ClickDetails.MouseButton, ClickDetails.PressedKeyboardKeys,
          IsInstanceClick);
        return clickDetailsHash;
      }
    }

    public ClickDetailsWrapper(MouseClickDetails clickDetails, bool isInstanceClick)
    {
      ClickDetails = clickDetails;
      IsInstanceClick = isInstanceClick;
    }
  }
}
