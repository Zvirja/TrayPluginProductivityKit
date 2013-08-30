using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml.Serialization;
using TrayPluginProductivityKit.SuperClick;

namespace TrayPluginProductivityKit.Configuration.Mappings.Metadata
{
  [Serializable]
  public class MappingMetadata
  {
    public string RelatedActionKey { get; set; }
    public string EventName { get; set; }

    public List<string> KeyTriggersStr { get; set; }

    [XmlIgnore]
    public List<Key> KeyTriggers {
      get
      {
        if(KeyTriggersStr == null)
          return null;
        return KeyTriggersStr.Select(ParseKey).ToList();
      } 
    }

    public string MouseTriggerStr
    {
      get { return MouseTrigger.ToString(); }
      set
      {
        MouseButtons result;
        if (MouseButtons.TryParse(value, out result))
          MouseTrigger = result;
        else
          MouseTrigger = MouseButtons.None;
      }
    }

    [XmlIgnore]
    public MouseButtons MouseTrigger { get; set; }

    public string Parameters { get; set; }


    public virtual MouseClickHandlerBase GetProperlyConfiguredHandler()
    {
      var action = GetRelatedAction();
      if (action == null)
        return null;
      action.KeyTriggers = KeyTriggers;
      action.MouseButtonTrigger = MouseTrigger;
      action.CustomParameters = Parameters;
      return action;
    }

    public virtual MouseClickHandlerBase GetRelatedAction()
    {
      Dictionary<string, ActionMetadata> actionMetadatas = MetadataManager.ActualManager.ActionMetadatas;
      if (actionMetadatas.ContainsKey(RelatedActionKey))
        return null;
      return actionMetadatas[RelatedActionKey].GetNewlyConsntructedAction();
    }

    protected virtual Key ParseKey(string key)
    {
      Key result;
      if (Key.TryParse(key, out result))
        return result;
      return Key.Escape;
    }
  }
}
