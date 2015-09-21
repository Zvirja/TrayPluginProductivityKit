#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml.Serialization;
using TrayPluginProductivityKit.SuperClick;

#endregion

namespace TrayPluginProductivityKit.Configuration.Mappings.Metadata
{
  [Serializable]
  public class MappingMetadata
  {
    #region Properties

    [XmlElement]
    public string EventName { get; set; }

    [XmlIgnore]
    public List<Key> KeyTriggers
    {
      get
      {
        if (this.KeyTriggersStr == null)
        {
          return null;
        }
        return this.KeyTriggersStr.Select(this.ParseKey).ToList();
      }
    }

    [XmlElement]
    public List<string> KeyTriggersStr { get; set; }

    [XmlIgnore]
    public MouseButtons MouseTrigger { get; set; }

    [XmlElement]
    public string MouseTriggerStr
    {
      get
      {
        return this.MouseTrigger.ToString();
      }
      set
      {
        MouseButtons result;
        if (MouseButtons.TryParse(value, out result))
        {
          this.MouseTrigger = result;
        }
        else
        {
          this.MouseTrigger = MouseButtons.None;
        }
      }
    }

    [XmlElement]
    public string Parameters { get; set; }

    [XmlElement]
    public string RelatedActionKey { get; set; }

    #endregion

    #region Methods

    public virtual MouseClickHandlerBase GetProperlyConfiguredHandler()
    {
      var action = this.GetRelatedAction();
      if (action == null)
      {
        return null;
      }
      action.KeyTriggers = this.KeyTriggers;
      action.MouseButtonTrigger = this.MouseTrigger;
      action.CustomParameters = this.Parameters;
      return action;
    }

    public virtual MouseClickHandlerBase GetRelatedAction()
    {
      Dictionary<string, ActionMetadata> actionMetadatas = MetadataManager.ActualManager.ActionMetadatas;
      if (actionMetadatas.ContainsKey(this.RelatedActionKey))
      {
        return null;
      }
      return actionMetadatas[this.RelatedActionKey].GetNewlyConsntructedAction();
    }

    protected virtual Key ParseKey(string key)
    {
      Key result;
      if (Key.TryParse(key, out result))
      {
        return result;
      }
      return Key.Escape;
    }

    #endregion
  }
}