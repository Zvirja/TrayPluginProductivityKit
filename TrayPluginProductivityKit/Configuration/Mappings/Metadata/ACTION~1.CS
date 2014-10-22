using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrayPluginProductivityKit.SuperClick;

namespace TrayPluginProductivityKit.Configuration.Mappings.Metadata
{
  public class ActionMetadata
  {
    #region Constructors and Destructors

    public ActionMetadata(string actionType, string alias, string parametersFormat, string description)
    {
      this.ActionType = actionType;
      this.Alias = alias;
      this.ParametersFormat = parametersFormat;
      this.Description = description;
    }

    #endregion

    #region Public Properties

    public string ActionType { get; set; }
    public string Alias { get; set; }
    public string Description { get; set; }

    public bool IsValid
    {
      get
      {
        return this.GetNewlyConsntructedAction() != null;
      }
    }

    public string ParametersFormat { get; set; }

    #endregion

    #region Public Methods and Operators

    public MouseClickHandlerBase GetNewlyConsntructedAction()
    {
      try
      {
        return Activator.CreateInstance(Type.GetType(this.ActionType)) as MouseClickHandlerBase;
      }
      catch
      {
        return null;
      }
    }

    #endregion
  }
}