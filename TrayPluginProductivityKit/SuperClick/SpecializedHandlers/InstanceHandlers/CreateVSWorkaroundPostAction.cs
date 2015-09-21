#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using SIM.Instances;
using SIM.Tool.Plugins.TrayPlugin.Resourcing;
using TrayPluginProductivityKit.TrayNotifications;

#endregion

namespace TrayPluginProductivityKit.SuperClick.SpecializedHandlers.InstanceHandlers
{
  public class CreateVSWorkaroundPostAction : InstanceClickHandlerBase
  {
    #region Methods

    protected override bool ProcessInstanceClickInternal(Instance instance)
    {
      string rootPath = instance.RootPath.TrimEnd('\\', '/');
      string format = MultisourceResourcesManager.GetStringResource("VsPostBuildTemplateFormat", null);
      if (format == null)
      {
        TrayNotificationManager.ActualManager.NotifyFailure();
        return false;
      }

      string value = string.Format(format, rootPath);
      Clipboard.SetText(value);
      TrayNotificationManager.ActualManager.NotifySuccess();

      return true;
    }

    #endregion
  }
}