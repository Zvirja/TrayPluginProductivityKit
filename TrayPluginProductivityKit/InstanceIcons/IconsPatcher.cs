using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SIM.Base;
using SIM.Instances;
using SIM.Products;
using SIM.Tool.Plugins.TrayPlugin.Messaging;
using SIM.Tool.Plugins.TrayPlugin.Resourcing;
using SIM.Tool.Plugins.TrayPlugin.TrayIcon.ContextMenu;

namespace TrayPluginProductivityKit.InstanceIcons
{
  public class IconsPatcher
  {
    #region static part

    public static IconsPatcher ActualPatcher { get; set; }

    static IconsPatcher()
    {
      ActualPatcher = new IconsPatcher();
    }

    public static void Initialize()
    {
      ActualPatcher.InitializeInstance();
    }

    #endregion


    protected Icon DefaultIcon { get; set; }
    protected Dictionary<string, Image> InternalCache { get; set; }
    protected ManualResetEventSlim ProductManagerInitialized { get; set; }


    public virtual void InitializeInstance()
    {
      MessageBus.Subscribe(StandardMessagesKinds.ContextMenuProviderEntryConstructed, OnMenuEntryConstructed);
      ProductManager.ProductManagerInitialized += OnProductsInitialized;
      DefaultIcon = MultisourceResourcesManager.GetIconResource("scxx", null);
      InternalCache = new Dictionary<string, Image>();
      ProductManagerInitialized = new ManualResetEventSlim(false);
    }

    protected virtual void OnProductsInitialized()
    {
      ProductManagerInitialized.Set();
    }

    protected virtual void OnMenuEntryConstructed(object sender, TrayPluginMessage message)
    {
      //Product manager is initialized after this method is usually called. Because of that tray icons might be broken.
      ProductManagerInitialized.Wait();
      var constructingParams = message.Arguments as MenuEntryConstructedMessageParams;
      if (constructingParams == null)
        return;
      ToolStripItem menuItem = constructingParams.ContextMenuItem;
      var relatedInstance = menuItem.Tag as Instance;
      if (relatedInstance == null)
        return;
      var icon = GetIconForInstance(relatedInstance);
      if (icon == null)
        return;
      menuItem.Image = icon;
    }

    protected virtual Image GetIconForInstance(Instance instance)
    {
      string version = instance.Product.ShortVersion;
      string instanceName = instance.Name;
      Image resolvedImage = DefaultIcon != null ? DefaultIcon.ToBitmap() : null;
      while (true)
      {
        if (InternalCache.ContainsKey(instanceName))
          return InternalCache[instanceName];
        if (version.IsNullOrEmpty())
          break;
        if (version.Length > 1)
        {
          var shortVersion = version.Substring(0, 2);
          var resolvedIcon = MultisourceResourcesManager.GetIconResource("sc" + shortVersion, null);
          if (resolvedIcon != null)
            resolvedImage = resolvedIcon.ToBitmap();
        }
        break; 
      }

      InternalCache.Add(instanceName, resolvedImage);
      return resolvedImage;
    }
  }
}
