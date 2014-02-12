using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using SIM.Instances;
using SIM.Tool.Plugins.TrayPlugin.Resourcing;

namespace TrayPluginProductivityKit.InstanceMarking
{
  public class FilesystemMarkingProvider
  {
    #region WinAPI
    [DllImport("Shell32.dll", CharSet = CharSet.Auto)]
    protected static extern UInt32 SHGetSetFolderCustomSettings(ref LPSHFOLDERCUSTOMSETTINGS pfcs, string pszPath,
                                                              UInt32 dwReadWrite);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    protected struct LPSHFOLDERCUSTOMSETTINGS
    {
      public UInt32 dwSize;
      public UInt32 dwMask;
      public IntPtr pvid;
      public string pszWebViewTemplate;
      public UInt32 cchWebViewTemplate;
      public string pszWebViewTemplateVersion;
      public string pszInfoTip;
      public UInt32 cchInfoTip;
      public IntPtr pclsid;
      public UInt32 dwFlags;
      public string pszIconFile;
      public UInt32 cchIconFile;
      public int iIconIndex;
      public string pszLogo;
      public UInt32 cchLogo;
    }
    #endregion


    public virtual bool IsInstanceMarked(Instance instance)
    {
      return File.Exists(GetDesktopIniPath(instance));
    }

    public virtual void UnMarkInstance(Instance instance)
    {
      string rootPath = instance.RootPath;
      File.Delete(GetDesktopIniPath(rootPath));
      ChangeFolderIconNative(rootPath, null);
    }

    public virtual bool MarkInstance(Instance instance)
    {
      var rootPath = instance.RootPath;
      if (!CopyFavIcon(rootPath))
        return false;
      var iconPath = GetFavIconPath(rootPath);
      return ChangeFolderIconNative(rootPath, iconPath);
    }


    protected virtual bool CopyFavIcon(string folderPath)
    {
      string iconPath = GetFavIconPath(folderPath);
      if (File.Exists(iconPath))
        return true;
      try
      {
        var obj = MultisourceResourcesManager.GetIconResource("ProductivityKitFolderFavicon", null);
        if (obj == null)
          return false;
        using (FileStream favIconStream = File.Create(iconPath))
        {
          obj.Save(favIconStream);
        }
        var fi = new FileInfo(iconPath);
        fi.Attributes = FileAttributes.Hidden | FileAttributes.System;
        return true;
      }
      catch
      {
        return false;
      }
    }


    #region Path helpers

    protected virtual string GetDesktopIniPath(Instance instance)
    {
      return GetDesktopIniPath(instance.RootPath);
    }

    protected virtual string GetDesktopIniPath(string rootPath)
    {
      return Path.Combine(rootPath, "desktop.ini");
    }

    #endregion


    protected virtual string GetFavIconPath(Instance instance)
    {
      return GetFavIconPath(instance.RootPath);
    }

    protected virtual string GetFavIconPath(string rootPath)
    {
      return Path.Combine(rootPath, "favicon.ico");
    }

    protected virtual bool ChangeFolderIconNative(string folderPath, string iconPath)
    {
      LPSHFOLDERCUSTOMSETTINGS FolderSettings = new LPSHFOLDERCUSTOMSETTINGS();
      FolderSettings.dwMask = 0x10;
      if (!string.IsNullOrEmpty(iconPath))
      {
        FolderSettings.pszIconFile = iconPath;
        FolderSettings.iIconIndex = 0;
      }
      UInt32 FCS_READ = 0x00000001;
      UInt32 FCS_FORCEWRITE = 0x00000002;
      UInt32 FCS_WRITE = FCS_READ | FCS_FORCEWRITE;

      string pszPath = folderPath;
      UInt32 HRESULT = SHGetSetFolderCustomSettings(ref FolderSettings, pszPath, FCS_WRITE);
      return HRESULT == 0;
    }
  }
}
