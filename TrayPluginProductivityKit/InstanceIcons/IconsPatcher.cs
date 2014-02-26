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
using SIM.Tool.Plugins.TrayPlugin;
using SIM.Tool.Plugins.TrayPlugin.Resourcing;
using SIM.Tool.Plugins.TrayPlugin.TrayIcon.ContextMenu;
using SIM.Tool.Plugins.TrayPlugin.TrayIcon.ContextMenu.Eventing;

namespace TrayPluginProductivityKit.InstanceIcons
{
  public class IconsPatcher
  {
    #region Constructors and Destructors

    static IconsPatcher()
    {
      ActualPatcher = new IconsPatcher();
    }

    #endregion

    #region Public Properties

    public static IconsPatcher ActualPatcher { get; set; }

    #endregion

    #region Properties

    protected Icon DefaultIcon { get; set; }
    protected Dictionary<string, Image> InternalCache { get; set; }
    protected ManualResetEventSlim ProductManagerInitialized { get; set; }

    #endregion

    #region Public Methods and Operators

    public static void Initialize()
    {
      ActualPatcher.InitializeInstance();
    }


    public virtual void InitializeInstance()
    {
      TrayPluginEvents.ContextMenuEntryConstructed += OnMenuEntryConstructed;
      ProductManager.ProductManagerInitialized += OnProductsInitialized;
      DefaultIcon = MultisourceResourcesManager.GetIconResource("scxx", null);
      InternalCache = new Dictionary<string, Image>();
      ProductManagerInitialized = new ManualResetEventSlim(false);
    }

    #endregion

    #region Methods

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

    protected virtual void OnMenuEntryConstructed(object sender, MenuEntryConstructedArgs args)
    {
      //Product manager is initialized after this method is usually called. Because of that tray icons might be broken.
      ProductManagerInitialized.Wait();
      var relatedInstance = args.Instance;
      if (relatedInstance == null)
        return;
      ToolStripItem menuItem = args.ContextMenuItem;
      var icon = GetIconForInstance(relatedInstance);
      if (icon == null)
        return;
      menuItem.Image = icon;
    }

    protected virtual void OnProductsInitialized()
    {
      ProductManagerInitialized.Set();
    }

    #endregion
  }
}