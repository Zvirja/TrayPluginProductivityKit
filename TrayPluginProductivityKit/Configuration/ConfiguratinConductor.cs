using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using SIM.Base;

namespace TrayPluginProductivityKit.Configuration
{
  public static class ConfiguratinConductor
  {
    public static XmlDocument ProductivityKitConfig
    {
      get
      {
        try
        {
          string location = Assembly.GetExecutingAssembly().Location;
          System.Configuration.Configuration configuration = ConfigurationManager.OpenExeConfiguration(location);
          var section = configuration.GetSection("productivityKit");
          var castedSection = section as SectionHandler;
          Assert.IsNotNull(castedSection, "something is wrong with plugin config");
          return castedSection.XmlRepresentation;
        }
        catch (Exception)
        {
          return null;
        }
        
      }
    }

  }
}
