#region Usings

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using Sitecore.Diagnostics;
using SIM;

#endregion

namespace TrayPluginProductivityKit.Configuration
{
  public static class ConfiguratinConductor
  {
    #region Properties

    public static XmlDocument ProductivityKitConfig
    {
      get
      {
        try
        {
          string location = Assembly.GetExecutingAssembly().Location;
          System.Configuration.Configuration configuration = ConfigurationManager.OpenExeConfiguration(location);
          var section = configuration.GetSection("productivityKit");
          var castedSection = section as AppConfigSectionHandler;
          Assert.IsNotNull(castedSection, "something is wrong with plugin config");
          return castedSection.XmlRepresentation;
        }
        catch (Exception)
        {
          return null;
        }
      }
    }

    #endregion
  }
}