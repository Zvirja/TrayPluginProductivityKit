using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml;
using System.Xml.Serialization;
using SIM.Base;

namespace TrayPluginProductivityKit.Configuration.Mappings.Metadata
{
  public class MetadataManager
  {
    public readonly string actionsMetadataPath = "productivityKit/clickActions/action";

    public static MetadataManager ActualManager { get; set; }

    public static void Initialize()
    {
      ActualManager = new MetadataManager();
      ActualManager.InitializeInstance();
    }

    public Dictionary<string, ActionMetadata> ActionMetadatas { get; set; }
    public List<MappingMetadata> MappingsMetadatas { get; set; }

    public void InitializeInstance()
    {
      InitializeActionsMetadata();
    }

    protected virtual string GetNodeValue(XmlElement element)
    {
      return element.InnerText;
    }

    protected virtual void InitializeActionsMetadata()
    {
      XmlDocument config = ConfiguratinConductor.ProductivityKitConfig;
      XmlNodeList actionNodes = config.SelectNodes(actionsMetadataPath);
      var result = new Dictionary<string,ActionMetadata>();
      foreach (XmlNode actionNode in actionNodes)
      {
        ParseActionMetadata(actionNode,result);
      }
      ActionMetadatas = result;
    }

    protected virtual void ParseActionMetadata(XmlNode node, Dictionary<string, ActionMetadata> output)
    {
      var typeNode = node.SelectSingleNode("type") as XmlElement;
      var aliasNode = node.SelectSingleNode("alias") as XmlElement;
      var formatNode = node.SelectSingleNode("format") as XmlElement;
      var descriptionNode = node.SelectSingleNode("description") as XmlElement;

      if (typeNode.IsNull() || formatNode.IsNull() || descriptionNode.IsNull() || aliasNode.IsNull())
        return;
      var type = GetNodeValue(typeNode);
      var alias = GetNodeValue(aliasNode);
      var format = GetNodeValue(formatNode);
      var description = GetNodeValue(descriptionNode);

      if (type.IsNullOrEmpty() || alias.IsNullOrEmpty() || format.IsNullOrEmpty() || description.IsNullOrEmpty())
        return;

      var metadata = new ActionMetadata(type, alias, format, description);
      if (!metadata.IsValid)
        return;
      output.Add(alias, metadata);
    }

  }
}
