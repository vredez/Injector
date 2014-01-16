using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Windows.Forms;

namespace Injector
{
    /// <summary>
    /// Analyzes a managed Dll.
    /// </summary>
    public class ManagedDll
    {
        interface IDllAnalyzer
        {
            string[] Analyze(string file);
        }

        class DllAnalyzer : MarshalByRefObject, IDllAnalyzer
        {
            public string[] Analyze(string file)
            {
                try
                {
                    Assembly dll = Assembly.LoadFrom(file);
                    List<string> result = new List<string>();
                   
                    foreach(Type t in dll.GetTypes())
                    {
                        foreach(MethodInfo method in t.GetMethods
                            (
                                BindingFlags.Public | BindingFlags.Static
                            ))
                        {
                            if(method.ReturnType == typeof(int))
                            {
                                ParameterInfo[] args = method.GetParameters();
                                if(args.Length == 1 && args[0].ParameterType == typeof(string))
                                {
                                    result.Add(string.Format("int {0}.{1}(string)", t.FullName, method.Name));
                                }
                            }
                        }
                    }

                    return result.ToArray();
                }
                catch
                {
                    return new string[] { };
                }
            }
        }

        /// <summary>
        /// Private constructor to prevent instanciation.
        /// </summary>
        private ManagedDll() { }

        public static string[] AnalyzeMethods(string dllPath)
        {
            AppDomain temp_domain = AppDomain.CreateDomain(Guid.NewGuid().ToString(), null,
                new AppDomainSetup
                {
                    ApplicationBase = @"C:\tmp",
                    PrivateBinPath = @"C:\tmp",
                    ShadowCopyFiles = "true"
                });

            Type analyzer_type = typeof(DllAnalyzer);
            IDllAnalyzer analyzer = (IDllAnalyzer)temp_domain.CreateInstanceFromAndUnwrap
                (
                    analyzer_type.Assembly.Location,
                    analyzer_type.FullName
                );

            string[] type_names = analyzer.Analyze(dllPath);

            AppDomain.Unload(temp_domain);

            return type_names;
        }
    }
}
