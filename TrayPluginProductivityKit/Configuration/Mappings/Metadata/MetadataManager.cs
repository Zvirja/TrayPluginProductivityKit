using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using SIM.Base;

namespace TrayPluginProductivityKit.Configuration.Mappings.Metadata
{
  public class MetadataManager
  {
    #region Fields

    public readonly string actionsMetadataPath = "productivityKit/clickActions/action";

    #endregion

    #region Public Properties

    public static MetadataManager ActualManager { get; set; }

    public Dictionary<string, ActionMetadata> ActionMetadatas { get; set; }
    public List<MappingMetadata> MappingsMetadatas { get; set; }

    #endregion

    #region Public Methods and Operators

    public static void Initialize()
    {
      ActualManager = new MetadataManager();
      ActualManager.InitializeInstance();
    }

    public void InitializeInstance()
    {
      this.InitializeActionsMetadata();
    }

    #endregion

    #region Methods

    protected virtual string GetNodeValue(XmlElement element)
    {
      return element.InnerText;
    }

    protected virtual void InitializeActionsMetadata()
    {
      XmlDocument config = ConfiguratinConductor.ProductivityKitConfig;
      XmlNodeList actionNodes = config.SelectNodes(this.actionsMetadataPath);
      var result = new Dictionary<string, ActionMetadata>();
      foreach (XmlNode actionNode in actionNodes)
      {
        this.ParseActionMetadata(actionNode, result);
      }
      this.ActionMetadatas = result;
    }

    protected virtual void ParseActionMetadata(XmlNode node, Dictionary<string, ActionMetadata> output)
    {
      var typeNode = node.SelectSingleNode("type") as XmlElement;
      var aliasNode = node.SelectSingleNode("alias") as XmlElement;
      var formatNode = node.SelectSingleNode("format") as XmlElement;
      var descriptionNode = node.SelectSingleNode("description") as XmlElement;

      if (typeNode.IsNull() || formatNode.IsNull() || descriptionNode.IsNull() || aliasNode.IsNull())
      {
        return;
      }
      var type = this.GetNodeValue(typeNode);
      var alias = this.GetNodeValue(aliasNode);
      var format = this.GetNodeValue(formatNode);
      var description = this.GetNodeValue(descriptionNode);

      if (type.IsNullOrEmpty() || alias.IsNullOrEmpty() || format.IsNullOrEmpty() || description.IsNullOrEmpty())
      {
        return;
      }

      var metadata = new ActionMetadata(type, alias, format, description);
      if (!metadata.IsValid)
      {
        return;
      }
      output.Add(alias, metadata);
    }

    #endregion
  }
}