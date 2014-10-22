using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TrayPluginProductivityKit.Helpers
{
  public static class UISyncContextHolder
  {
    #region Static Fields

    public static SynchronizationContext UISyncContext;

    #endregion

    #region Public Methods and Operators

    public static void CatchSyncContext()
    {
      UISyncContext = SynchronizationContext.Current;
    }

    #endregion
  }
}