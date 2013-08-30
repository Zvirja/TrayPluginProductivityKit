using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml.Serialization;
using TrayPluginProductivityKit.Configuration.Mappings.Metadata;

namespace TrayPluginProductivityKit.Configuration.Mappings
{
  public class MappingsManager
  {
    public static MappingsManager ActualManager { get; set; }

    public static void Initialize()
    {
      ActualManager = new MappingsManager();
      ActualManager.InitializeInstance();
    }


    public List<MappingMetadata> MappingsMetadatas { get; set; }

    public virtual void InitializeInstance()
    {
      MappingsMetadatas = ReadMappings();
      if(MappingsMetadatas == null)
        MappingsMetadatas = new List<MappingMetadata>();
    }

    public virtual void Save()
    {
      SaveMappings();
    }

    protected virtual List<MappingMetadata> ReadMappings()
    {
      var fileName = GetFilePath();
      if (!File.Exists(fileName))
        return null;
      try
      {
        using (var fs = File.Open(fileName,FileMode.Open))
        {
          var result = GetSerializer().Deserialize(fs) as List<MappingMetadata>;
          if (result != null)
            return result;
        }
      }
      catch
      {
      }
      return null;
    }

    protected virtual void SaveMappings()
    {
      try
      {
        var filePath = GetFilePath();
        if(File.Exists(filePath))
          File.Delete(filePath);
        using (var fs = File.Open(filePath,FileMode.Create))
        {
          GetSerializer().Serialize(fs,MappingsMetadatas);
        }
      }
      catch
      {
        
      }
    }

    protected virtual string GetFilePath()
    {
      var directory = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
      return Path.Combine(directory, "mappings.xml");
    }

    protected virtual XmlSerializer GetSerializer()
    {
      return new XmlSerializer(typeof(List<MappingMetadata>));
    }
  }
}
