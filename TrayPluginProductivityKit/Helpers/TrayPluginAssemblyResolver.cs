using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TrayPluginProductivityKit.Helpers
{
  public class TrayPluginAssemblyResolver
  {
    static Assembly CurrentAssembly { get; set; }
    public static void Initialize()
    {
      CurrentAssembly = Assembly.GetExecutingAssembly();
      AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
    }

    private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
    {
      if(args.RequestingAssembly == null || args.RequestingAssembly == Assembly.GetExecutingAssembly())
        return AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.FullName.StartsWith(args.Name));
      return null;
    }
  }
}
