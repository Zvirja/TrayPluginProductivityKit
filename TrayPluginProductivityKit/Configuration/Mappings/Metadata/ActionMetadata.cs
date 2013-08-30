using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrayPluginProductivityKit.SuperClick;

namespace TrayPluginProductivityKit.Configuration.Mappings.Metadata
{
  public class ActionMetadata
  {
    public string ActionType { get; set; }
    public string Description { get; set; }
    public string ParametersFormat { get; set; }
    public string Alias { get; set; }

    public ActionMetadata(string actionType, string alias, string parametersFormat, string description)
    {
      ActionType = actionType;
      Alias = alias;
      ParametersFormat = parametersFormat;
      Description = description;
    }

    public bool IsValid
    {
      get
      {
        return GetNewlyConsntructedAction() != null;
      }
    }

    public MouseClickHandlerBase GetNewlyConsntructedAction()
    {
      try
      {
        return Activator.CreateInstance(Type.GetType(ActionType)) as MouseClickHandlerBase;
      }
      catch
      {
        return null;
      }
    }
  }
}
