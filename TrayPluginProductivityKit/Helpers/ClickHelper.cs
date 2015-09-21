#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;

#endregion

namespace TrayPluginProductivityKit.Helpers
{
  public static class ClickHelper
  {
    #region Methods

    public static string GetMouseClickHash(MouseButtons mouseButton, List<Key> keys, bool isInstanceClick)
    {
      var result = new StringBuilder();
      result.Append(mouseButton);
      if (keys != null)
      {
        foreach (string key in keys.Select(x => x.ToString()).OrderBy(x => x))
        {
          result.Append(key);
        }
      }

      result.Append(isInstanceClick);
      return result.ToString();
    }

    #endregion
  }
}