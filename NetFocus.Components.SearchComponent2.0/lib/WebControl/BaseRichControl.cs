//------------------------------------------------------------------------------
// Copyright (c) 2000-2003 Microsoft Corporation. All Rights Reserved.
//------------------------------------------------------------------------------

namespace NetFocus.Components.WebControls
{
    using System;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Collections.Specialized;
    using System.Reflection;
	using NetFocus.Components.SearchComponent;

    /// <summary>
    /// ID indicating which rendering path to use.
    /// To create custom IDs, set them starting at MaxPath or higher.
    /// </summary>
    public enum RenderPathID
    {
        /// <summary>
        /// ID for down level browsers.
        /// </summary>
        DownLevelPath   = 0,

        /// <summary>
        /// ID for up level browsers (IE 5.5 and above).
        /// </summary>
        UpLevelPath     = 1,

        /// <summary>
        /// ID for design mode.
        /// </summary>
        DesignerPath    = 2,

        /// <summary>
        /// Use when adding new paths.
        /// </summary>
        MaxPath         = 10
    };

    /// <summary>
    /// Base class that splits rendering into three paths:
    /// UpLevel, DownLevel, and Designer.
    /// 
    /// Provides access to the common files path.
    /// 
    /// Provides a resource manager.
    /// </summary>
    public abstract class BaseRichControl : WebControl
    {
        private const string ConfigName             = "MicrosoftWebControls";
        private const string CommonFilesKey         = "CommonFiles";

        private BrowserLevelChecker _BrowserLevelChecker;
        private static System.Resources.ResourceManager _ResourceManager = null;

        /// <summary>
        /// Initializes a new instance of a BaseRichControl.
        /// </summary>
        public BaseRichControl() : base()
        {
            _BrowserLevelChecker = CreateLevelChecker();
        }

        // --------------------------------------------------------------------
        // Rendering
        // --------------------------------------------------------------------

        /// <summary>
        /// Creates the BrowserLevelChecker object used to determine uplevel 
        /// and downlevel browsers. Override to return a custom BrowserLevelChecker.
        /// Defaults: IE 5.5 and above is uplevel. Everything else is downlevel.
        /// </summary>
        /// <returns>The BrowserLevelChecker object.</returns>
        protected virtual BrowserLevelChecker CreateLevelChecker()
        {
            return new BrowserLevelChecker("ie", 5, 0.5, true);
        }

        /// <summary>
        /// The ID of the rendering path being used.
        /// </summary>
        protected virtual RenderPathID RenderPath
        {
            get 
            {
                if (IsDesignMode)
                {
                    return RenderPathID.DesignerPath;
                }

                if (_BrowserLevelChecker.IsUpLevelBrowser(Context))
                {
                    return RenderPathID.UpLevelPath;
                }

                return RenderPathID.DownLevelPath;
            }
        }

        /// <summary>
        /// true if currently in design mode.
        /// </summary>
        protected bool IsDesignMode
        {
            get { return (Site != null) ? Site.DesignMode : false; }
        }

        /// <summary>
        /// true if using an uplevel browser.
        /// </summary>
        protected virtual bool IsUpLevelBrowser
        {
            get { return RenderPath == RenderPathID.UpLevelPath; }
        }

        /// <summary>
        /// Outputs control content to a provided HtmlTextWriter output stream.
        /// </summary>
        /// <param name="writer">The HtmlTextWriter object that receives the control content.</param>
        protected override void Render(HtmlTextWriter writer)
        {
            // Render based off of the RenderPath ID
            switch (RenderPath)
            {
                case RenderPathID.DownLevelPath:
                    RenderDownLevelPath(writer);
                    break;

                case RenderPathID.UpLevelPath:
                    RenderUpLevelPath(writer);
                    break;

                case RenderPathID.DesignerPath:
                    RenderDesignerPath(writer);
                    break;

                default:
                    base.Render(writer);
                    break;
            }
        }

        /// <summary>
        /// Renders the control for an uplevel browser.
        /// </summary>
        /// <param name="writer">The HtmlTextWriter object that receives the control content.</param>
        protected virtual void RenderUpLevelPath(HtmlTextWriter writer)
        {
            RenderContents(writer);
        }

        /// <summary>
        /// Renders the control for a downlevel browser.
        /// </summary>
        /// <param name="writer">The HtmlTextWriter object that receives the control content.</param>
        protected virtual void RenderDownLevelPath(HtmlTextWriter writer)
        {
            RenderContents(writer);
        }

        /// <summary>
        /// Renders the control for a visual designer.
        /// </summary>
        /// <param name="writer">The HtmlTextWriter object that receives the control content.</param>
        protected virtual void RenderDesignerPath(HtmlTextWriter writer)
        {
            RenderContents(writer);
        }

        // --------------------------------------------------------------------
        // Common Files
        // --------------------------------------------------------------------

        /// <summary>
        /// Adds the common file path to the filename.
        /// </summary>
        /// <param name="filename">The filename to qualify with the common path.</param>
        /// <returns>The full path of the filename with the common path.</returns>
        protected string AddPathToFilename(string filename)
        {
            return AddPathToFilename(Context, filename);
        }

        /// <summary>
        /// Static version of AddPathToFilename so that classes not deriving from
        /// BaseRichControl can still find the common files path.
        /// </summary>
        /// <param name="context">The context from which to get the configuration.</param>
        /// <param name="filename">The filename to qualify with the common path.</param>
        /// <returns>The full path of the filename with the common path.</returns>
        internal string AddPathToFilename(HttpContext context, string filename)
        {
            return Page.Request.Params["datapath"] + @"scripts/" + filename;
        }

//        /// <summary>
//        /// Finds the path for client files used be server controls.
//        /// </summary>
//        /// <param name="context">The context from which to get the configuration.</param>
//        /// <returns>The path name.</returns>
//        private string FindCommonPath(HttpContext context)
//        {
//            // Look at the current configuration for the path
//            if (context != null)
//            {
//                NameValueCollection table = (NameValueCollection)context.GetConfig(ConfigName);
//                if (table != null)
//                {
//                    string path = (string)table[CommonFilesKey];
//                    if (path != null)
//                    {
//                        return CleanupPath(path);
//                    }
//                }
//            }
//
//            // Return the default path with version number
////            Assembly assembly = typeof(BaseRichControl).Assembly;
////            Version version = assembly.GetName().Version;
////
////            return DefaultCommonFilesRoot + version.Major.ToString() + "_" + version.Minor.ToString() + "/";
//
//			return HttpContext.Current.Request.ApplicationPath + webSearchComponent.DataPath + @"scripts/";
//
//        }

        /// <summary>
        /// Ensures that there is a '/' at the end of a path string.
        /// </summary>
        /// <param name="path">The path name to cleanup.</param>
        /// <returns>The cleaned up path name.</returns>
        private static string CleanupPath(string path)
        {
            if (path.Length > 0)
            {
                if (path[path.Length - 1] != '/')
                {
                    path += "/";
                }
            }

            return path;
        }

        // --------------------------------------------------------------------
        // Resources
        // --------------------------------------------------------------------

        /// <summary>
        /// A ResourceManager for all our controls to access resources.
        /// </summary>
        protected static System.Resources.ResourceManager ResourceManager
        {
            get 
            {
                if (_ResourceManager == null)
                {
                    Type ourType = typeof(BaseRichControl);
					_ResourceManager = new System.Resources.ResourceManager(ourType.Namespace + ".Resources.Resources",ourType.Module.Assembly);

                }

                return _ResourceManager;
            }
        }

        /// <summary>
        /// Uses ResourceManager to retrieve string resources.
        /// </summary>
        protected static string GetStringResource(string name)
        {
            return (string)ResourceManager.GetObject(name);
        }

    }
}
