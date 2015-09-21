#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

#endregion

namespace TrayPluginProductivityKit.Helpers
{
  public static class UISyncContextHolder
  {
    #region Fields

    public static SynchronizationContext UISyncContext;

    #endregion

    #region Methods

    public static void CatchSyncContext()
    {
      UISyncContext = SynchronizationContext.Current;
    }

    #endregion
  }
}