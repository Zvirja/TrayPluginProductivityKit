#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SIM.Pipelines;
using SIM.Pipelines.Install;
using SIM.Pipelines.Processors;

#endregion

namespace TrayPluginProductivityKit.InstallCleanup
{
  public static class InstallPipelineInjector
  {
    #region Methods

    public static void InjectCustomPipeline()
    {
      var pipelineDefs = (Dictionary<string, PipelineDefinition>)typeof(PipelineManager).InvokeMember("Definitions", BindingFlags.GetField | BindingFlags.Static | BindingFlags.NonPublic, Type.DefaultBinder, null, null);

      if (pipelineDefs == null)
      {
        return;
      }

      var procDefToInject = new SingleProcessorDefinition();
      procDefToInject.Type = typeof(InstallFilesCleanup);
      procDefToInject.Title = "Performing cleanup";

      PipelineDefinition installPipeline = pipelineDefs["install"];

      foreach (StepDefinition step in installPipeline.Steps)
      {
        foreach (ProcessorDefinition procDef in step.ProcessorDefinitions)
        {
          if (procDef.Type == typeof(Extract))
          {
            procDef.NestedProcessorDefinitions.Add(procDefToInject);
            return;
          }
        }
      }
    }

    #endregion
  }
}