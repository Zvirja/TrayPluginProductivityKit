#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIM.Instances;
using SIM.Tool.Plugins.TrayPlugin.Common;
using TrayPluginProductivityKit.Helpers;

#endregion

namespace TrayPluginProductivityKit.SuperClick
{
  public class ClickDetailsWrapper
  {
    #region Fields

    private string clickDetailsHash;

    #endregion

    #region Constructors

    public ClickDetailsWrapper(MouseClickInformation clickDetails, Instance instance, EventArgs rawArgs)
    {
      this.ClickDetails = clickDetails;
      this.Instance = instance;
      this.RawArgs = rawArgs;
      this.IsInstanceClick = instance != null;
    }

    #endregion

    #region Properties

    public MouseClickInformation ClickDetails { get; set; }

    public string ClickDetailsHash
    {
      get
      {
        if (this.clickDetailsHash != null)
        {
          return this.clickDetailsHash;
        }
        this.clickDetailsHash = ClickHelper.GetMouseClickHash(this.ClickDetails.MouseButton, this.ClickDetails.PressedKeyboardKeys, this.IsInstanceClick);
        return this.clickDetailsHash;
      }
    }

    public Instance Instance { get; set; }
    public bool IsInstanceClick { get; set; }
    public EventArgs RawArgs { get; set; }

    #endregion
  }
}