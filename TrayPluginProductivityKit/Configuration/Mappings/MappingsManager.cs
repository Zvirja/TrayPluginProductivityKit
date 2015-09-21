#region Usings

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using TrayPluginProductivityKit.Configuration.Mappings.Metadata;

#endregion

namespace TrayPluginProductivityKit.Configuration.Mappings
{
  public class MappingsManager
  {
    #region Properties

    public static MappingsManager ActualManager { get; set; }


    public List<MappingMetadata> MappingsMetadatas { get; set; }

    #endregion

    #region Methods

    public static void Initialize()
    {
      ActualManager = new MappingsManager();
      ActualManager.InitializeInstance();
    }

    public virtual void InitializeInstance()
    {
      this.MappingsMetadatas = this.ReadMappings();
      if (this.MappingsMetadatas == null)
      {
        this.MappingsMetadatas = new List<MappingMetadata>();
      }
    }

    public virtual void Save()
    {
      this.SaveMappings();
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

    protected virtual List<MappingMetadata> ReadMappings()
    {
      var fileName = this.GetFilePath();
      if (!File.Exists(fileName))
      {
        return null;
      }
      try
      {
        using (var fs = File.Open(fileName, FileMode.Open))
        {
          var result = this.GetSerializer().Deserialize(fs) as List<MappingMetadata>;
          if (result != null)
          {
            return result;
          }
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
        var filePath = this.GetFilePath();
        if (File.Exists(filePath))
        {
          File.Delete(filePath);
        }
        using (var fs = File.Open(filePath, FileMode.Create))
        {
          this.GetSerializer().Serialize(fs, this.MappingsMetadatas);
        }
      }
      catch
      {
      }
    }

    #endregion
  }
}