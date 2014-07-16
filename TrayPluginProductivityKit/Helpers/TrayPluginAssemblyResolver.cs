using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TrayPluginProductivityKit.Helpers
{
  public class TrayPluginAssemblyResolver
  {
    #region Properties

    private static Assembly CurrentAssembly { get; set; }

    #endregion

    #region Public Methods and Operators

    public static void Initialize()
    {
      CurrentAssembly = Assembly.GetExecutingAssembly();
      AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
    }

    #endregion

    #region Methods

    private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
    {
      if (args.RequestingAssembly == Assembly.GetExecutingAssembly())
      {
        return AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.FullName.Equals(args.Name, StringComparison.OrdinalIgnoreCase));
      }
      if (args.Name.Equals("TrayPluginProductivityKit"))
      {
        return Assembly.GetExecutingAssembly();
      }
      return null;
    }

    #endregion
  }
}