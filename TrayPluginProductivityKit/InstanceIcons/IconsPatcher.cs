#region Usings

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SIM;
using SIM.Instances;
using SIM.Products;
using SIM.Tool.Plugins.TrayPlugin;
using SIM.Tool.Plugins.TrayPlugin.Resourcing;
using SIM.Tool.Plugins.TrayPlugin.TrayIcon.ContextMenu.Eventing;

#endregion

namespace TrayPluginProductivityKit.InstanceIcons
{
  public class IconsPatcher
  {
    #region Constructors

    static IconsPatcher()
    {
      ActualPatcher = new IconsPatcher();
    }

    #endregion

    #region Properties

    public static IconsPatcher ActualPatcher { get; set; }

    protected Icon DefaultIcon { get; set; }
    protected Dictionary<string, Image> InternalCache { get; set; }
    protected ManualResetEventSlim ProductManagerInitialized { get; set; }

    #endregion

    #region Methods

    public static void Initialize()
    {
      ActualPatcher.InitializeInstance();
    }


    public virtual void InitializeInstance()
    {
      TrayPluginEvents.ContextMenuEntryConstructed += this.OnMenuEntryConstructed;
      ProductManager.ProductManagerInitialized += this.OnProductsInitialized;
      this.DefaultIcon = MultisourceResourcesManager.GetIconResource("scxx", null);
      this.InternalCache = new Dictionary<string, Image>();
      this.ProductManagerInitialized = new ManualResetEventSlim(false);
    }

    protected virtual Image GetIconForInstance(Instance instance)
    {
      string version = instance.Product.ShortVersion;
      string instanceName = instance.Name;

      if (this.InternalCache.ContainsKey(instanceName))
      {
        return this.InternalCache[instanceName];
      }

      Image resolvedImage = null;
      if (!version.IsNullOrEmpty() && version.Length > 1)
      {
        var shortVersion = version.Substring(0, 2);
        var resolvedIcon = MultisourceResourcesManager.GetIconResource("sc" + shortVersion, null);
        if (resolvedIcon != null)
        {
          resolvedImage = resolvedIcon.ToBitmap();
        }
      }

      if (resolvedImage == null && this.DefaultIcon != null)
      {
        resolvedImage = this.DefaultIcon.ToBitmap();
      }

      this.InternalCache.Add(instanceName, resolvedImage);
      return resolvedImage;
    }

    protected virtual void OnMenuEntryConstructed(object sender, MenuEntryConstructedArgs args)
    {
      //Product manager is initialized after this method is usually called. Because of that tray icons might be broken.
      this.ProductManagerInitialized.Wait();
      var relatedInstance = args.Instance;
      if (relatedInstance == null)
      {
        return;
      }
      ToolStripItem menuItem = args.ContextMenuItem;
      var icon = this.GetIconForInstance(relatedInstance);
      if (icon == null)
      {
        return;
      }
      menuItem.Image = icon;
    }

    protected virtual void OnProductsInitialized()
    {
      this.ProductManagerInitialized.Set();
    }

    #endregion
  }
}