using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIM.Base;
using SIM.Instances;
using SIM.Tool.Base;

namespace TrayPluginProductivityKit.SuperClick.SpecializedHandlers.InstanceHandlers
{
  public class RunPage : InstanceClickHandlerBase
  {
    protected Tuple<bool, string> DefaultParams { get; set; }
    protected Tuple<bool, string> CachedParams { get; set; }
    protected string CachedForStr { get; set; }


    public RunPage()
    {
      DefaultParams = new Tuple<bool, string>(true, null);
    }

    protected override bool ProcessInstanceClickInternal(Instance instance)
    {
      var runParams = GetRunPageParams();
      WindowHelper.OpenInBrowser(instance.GetUrl(runParams.Item2), !runParams.Item1);
      return true;
    }

    protected virtual Tuple<bool, string> GetRunPageParams()
    {
      if (CustomParameters.IsNullOrEmpty())
        return DefaultParams;
      while (true)
      {
        if (CachedForStr == CustomParameters)
          break;
        CachedForStr = CustomParameters;
        CachedParams = DefaultParams;
        if (CachedForStr == null)
          break;
        var splitted = CachedForStr.Split(new[] { '|' }, StringSplitOptions.None);
        if (splitted.Length != 2)
          break;
        CachedParams = new Tuple<bool, string>(splitted[0].Equals("b",StringComparison.OrdinalIgnoreCase),splitted[1]);
        break;
      }
      return CachedParams;
    }

  }
}
