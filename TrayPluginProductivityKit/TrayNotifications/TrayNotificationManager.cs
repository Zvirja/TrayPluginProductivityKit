using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SIM.Tool.Plugins.TrayPlugin;
using SIM.Tool.Plugins.TrayPlugin.Resourcing;
using SIM.Tool.Plugins.TrayPlugin.TrayIcon;
using TrayPluginProductivityKit.Helpers;
using TrayPluginProductivityKit.Resources;

namespace TrayPluginProductivityKit.TrayNotifications
{
  public class TrayNotificationManager
  {
    #region Constants

    private const int MsecDefaultTimeout = 500;

    #endregion

    #region Static Fields

    public static TrayNotificationManager ActualManager = new TrayNotificationManager();

    #endregion

    #region Fields

    private CancellationTokenSource m_cancellationSource;

    private Icon m_failedIcon;
    private IconProvider m_iconProvider;
    private Icon m_successIcon;

    #endregion

    #region Public Methods and Operators

    public static void Initialize()
    {
      ActualManager.InitializeInstance();
    }

    public void NotifyFailure(int timeout = 0)
    {
      this.ChangeNotifyIcon(this.m_failedIcon, timeout);
    }

    public void NotifySuccess(int timeout = 0)
    {
      this.ChangeNotifyIcon(this.m_successIcon, timeout);
    }

    #endregion

    #region Methods

    private void ChangeNotifyIcon(Icon newIcon, int timeout = 0)
    {
      if (timeout == 0)
      {
        timeout = MsecDefaultTimeout;
      }

      if (this.m_cancellationSource != null)
      {
        this.m_cancellationSource.Cancel();
      }

      this.m_cancellationSource = new CancellationTokenSource();
      Task.Factory.StartNew(() => this.DoChangeIconWithTimeout(this.m_cancellationSource.Token, newIcon, timeout), this.m_cancellationSource.Token);
    }

    private void DoChangeIconWithTimeout(CancellationToken cancellationToken, Icon newIcon, int sleepTimetout)
    {
      Icon oldIcon = null;
      UISyncContextHolder.UISyncContext.Send(d => oldIcon = this.m_iconProvider.ActualNotifyIcon.Icon, null);
      if (cancellationToken.IsCancellationRequested)
      {
        return;
      }

      UISyncContextHolder.UISyncContext.Send(d => this.m_iconProvider.ActualNotifyIcon.Icon = newIcon, null);
      Thread.Sleep(sleepTimetout);
      if (cancellationToken.IsCancellationRequested)
      {
        return;
      }

      UISyncContextHolder.UISyncContext.Send(d => this.m_iconProvider.ActualNotifyIcon.Icon = oldIcon, null);
    }

    private void InitializeInstance()
    {
      TrayPluginEvents.TrayIconProviderInitialized += this.TrayPluginEventsOnTrayIconProviderInitialized;

      this.m_successIcon = MultisourceResourcesManager.GetIconResource("doneAction", null);
      this.m_failedIcon = MultisourceResourcesManager.GetIconResource("failedAction", null); 
    }

    private void TrayPluginEventsOnTrayIconProviderInitialized(object sender, EventArgs eventArgs)
    {
      this.m_iconProvider = sender as IconProvider;
    }

    #endregion
  }
}