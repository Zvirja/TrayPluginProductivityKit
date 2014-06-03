﻿using System;
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
    #region Constructors and Destructors

    public RunPage()
    {
      this.DefaultParams = new Tuple<bool, string>(true, null);
    }

    #endregion

    #region Properties

    protected string CachedForStr { get; set; }
    protected Tuple<bool, string> CachedParams { get; set; }
    protected Tuple<bool, string> DefaultParams { get; set; }

    #endregion

    #region Methods

    protected virtual Tuple<bool, string> GetRunPageParams()
    {
      if (CustomParameters.IsNullOrEmpty())
      {
        return this.DefaultParams;
      }
      while (true)
      {
        if (this.CachedForStr == CustomParameters)
        {
          break;
        }
        this.CachedForStr = CustomParameters;
        this.CachedParams = this.DefaultParams;
        if (this.CachedForStr == null)
        {
          break;
        }
        var splitted = this.CachedForStr.Split(new[]
        {
          '|'
        }, StringSplitOptions.None);
        if (splitted.Length != 2)
        {
          break;
        }
        this.CachedParams = new Tuple<bool, string>(splitted[0].Equals("b", StringComparison.OrdinalIgnoreCase), splitted[1]);
        break;
      }
      return this.CachedParams;
    }

    protected override bool ProcessInstanceClickInternal(Instance instance)
    {
      var runParams = this.GetRunPageParams();
      WindowHelper.OpenInBrowser(instance.GetUrl(runParams.Item2), !runParams.Item1);
      return true;
    }

    #endregion
  }
}