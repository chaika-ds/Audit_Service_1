﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AuditService.Tests.Resources {
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class TestResources {
        
        private static System.Resources.ResourceManager resourceMan;
        
        private static System.Globalization.CultureInfo resourceCulture;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal TestResources() {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager {
            get {
                if (object.Equals(null, resourceMan)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("AuditService.Tests.Resources.TestResources", typeof(TestResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public static System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        public static string DefaultIndex {
            get {
                return ResourceManager.GetString("DefaultIndex", resourceCulture);
            }
        }
        
        public static string BlockedPlayersLog {
            get {
                return ResourceManager.GetString("BlockedPlayersLog", resourceCulture);
            }
        }
        
        public static byte[] ElasticSearchResponse {
            get {
                object obj = ResourceManager.GetObject("ElasticSearchResponse", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        public static byte[] BlockedPlayersLogResponse {
            get {
                object obj = ResourceManager.GetObject("BlockedPlayersLogResponse", resourceCulture);
                return ((byte[])(obj));
            }
        }
    }
}
