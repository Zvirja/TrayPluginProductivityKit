using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;
using TrayPluginProductivityKit.Helpers;

namespace TrayPluginProductivityKit.SuperClick
{
  public class MouseClickHandlerBase
  {
    #region Fields

    private bool forInstanceClick;
    private List<Key> keyTriggers;
    private MouseButtons mouseButtonTrigger;

    #endregion

    #region Constructors and Destructors

    protected MouseClickHandlerBase()
    {
      this.MouseButtonTrigger = MouseButtons.None;
      this.KeyTriggers = null;
      this.ForInstanceClick = false;
      this.RecomputeTriggerHash();
    }

    #endregion

    #region Public Properties

    public string CustomParameters { get; set; }

    public bool ForInstanceClick
    {
      get
      {
        return this.forInstanceClick;
      }
      set
      {
        this.forInstanceClick = value;
        this.RecomputeTriggerHash();
      }
    }

    public List<Key> KeyTriggers
    {
      get
      {
        return this.keyTriggers;
      }
      set
      {
        this.keyTriggers = value;
        this.RecomputeTriggerHash();
      }
    }

    public MouseButtons MouseButtonTrigger
    {
      get
      {
        return this.mouseButtonTrigger;
      }
      set
      {
        this.mouseButtonTrigger = value;
        this.RecomputeTriggerHash();
      }
    }

    public string TriggerHash { get; set; }

    #endregion

    #region Public Methods and Operators

    public virtual bool ProcessClick(ClickDetailsWrapper clickDetails)
    {
      if (!this.MatchTriggers(clickDetails))
      {
        return false;
      }
      return this.ProcessClickInternal(clickDetails);
    }

    #endregion

    #region Methods

    protected virtual bool MatchTriggers(ClickDetailsWrapper clickDetails)
    {
      return this.TriggerHash.Equals(clickDetails.ClickDetailsHash, StringComparison.OrdinalIgnoreCase);
    }

    protected virtual bool ProcessClickInternal(ClickDetailsWrapper clickDetails)
    {
      return false;
    }

    protected void RecomputeTriggerHash()
    {
      this.TriggerHash = ClickHelper.GetMouseClickHash(this.MouseButtonTrigger, this.KeyTriggers, this.ForInstanceClick);
    }

    #endregion
  }
}