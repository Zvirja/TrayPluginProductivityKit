using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using SIM.Tool.Plugins.TrayPlugin.Messaging;
using SIM.Tool.Plugins.TrayPlugin.Messaging.ClickMessages;
using TrayPluginProductivityKit.Helpers;

namespace TrayPluginProductivityKit.SuperClick
{
  public class MouseClickHandlerBase
  {
    private List<Key> keyTriggers;
    private MouseButtons mouseButtonTrigger;
    private bool forInstanceClick;

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

    public bool ForInstanceClick
    {
      get { return forInstanceClick; }
      set
      {
        forInstanceClick = value;
        RecomputeTriggerHash();
      }
    }

    public string TriggerHash { get; set; }
    public string CustomParameters { get; set; }


    protected MouseClickHandlerBase()
    {
      MouseButtonTrigger = MouseButtons.None;
      KeyTriggers = null;
      ForInstanceClick = false;
      RecomputeTriggerHash();
    }

    public virtual bool ProcessClick(ClickDetailsWrapper clickDetails, TrayPluginMessage originalMessage)
    {
      if (!MatchTriggers(clickDetails))
        return false;
      return ProcessClickInternal(clickDetails, originalMessage);
    }

    protected void RecomputeTriggerHash()
    {
      TriggerHash = ClickHelper.GetMouseClickHash(MouseButtonTrigger, KeyTriggers, ForInstanceClick);
    }

    protected virtual bool MatchTriggers(ClickDetailsWrapper clickDetails)
    {
      return TriggerHash.Equals(clickDetails.ClickDetailsHash, StringComparison.OrdinalIgnoreCase);
    }

    protected virtual bool ProcessClickInternal(ClickDetailsWrapper clickDetails, TrayPluginMessage originalMessage)
    {
      return ProcessClickInternal(clickDetails);
    }

    protected virtual bool ProcessClickInternal(ClickDetailsWrapper clickDetails)
    {
      return false;
    }
  }
}
