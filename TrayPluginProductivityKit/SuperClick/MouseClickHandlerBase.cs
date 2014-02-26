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
      MouseButtonTrigger = MouseButtons.None;
      KeyTriggers = null;
      ForInstanceClick = false;
      RecomputeTriggerHash();
    }

    #endregion

    #region Public Properties

    public string CustomParameters { get; set; }

    public bool ForInstanceClick
    {
      get { return forInstanceClick; }
      set
      {
        forInstanceClick = value;
        RecomputeTriggerHash();
      }
    }

    public List<Key> KeyTriggers
    {
      get { return keyTriggers; }
      set
      {
        keyTriggers = value;
        RecomputeTriggerHash();
      }
    }

    public MouseButtons MouseButtonTrigger
    {
      get { return mouseButtonTrigger; }
      set
      {
        mouseButtonTrigger = value;
        RecomputeTriggerHash();
      }
    }

    public string TriggerHash { get; set; }

    #endregion

    #region Public Methods and Operators

    public virtual bool ProcessClick(ClickDetailsWrapper clickDetails)
    {
      if (!MatchTriggers(clickDetails))
        return false;
      return ProcessClickInternal(clickDetails);
    }

    #endregion

    #region Methods

    protected virtual bool MatchTriggers(ClickDetailsWrapper clickDetails)
    {
      return TriggerHash.Equals(clickDetails.ClickDetailsHash, StringComparison.OrdinalIgnoreCase);
    }

    protected virtual bool ProcessClickInternal(ClickDetailsWrapper clickDetails)
    {
      return false;
    }

    protected void RecomputeTriggerHash()
    {
      TriggerHash = ClickHelper.GetMouseClickHash(MouseButtonTrigger, KeyTriggers, ForInstanceClick);
    }

    #endregion
  }
}