//------------------------------------------------------------------------------
// Copyright (c) 2000-2003 Microsoft Corporation. All Rights Reserved.
//------------------------------------------------------------------------------

namespace NetFocus.Components.WebControls.Design
{
    /// <summary>
    /// Provides an editor for visually picking an image URL.
    /// </summary>
    public class ObjectImageUrlEditor : ObjectUrlEditor
    {
        /// <summary>
        /// Gets the caption for the URL.
        /// </summary>
        protected override string Caption
        {
            get { return DesignUtil.GetStringResource("ImageUrlCaption"); }
        }

        /// <summary>
        /// Gets the filter to use.
        /// </summary>
        protected override string Filter
        {
            get { return DesignUtil.GetStringResource("ImageUrlFilter"); }
        }
    }
}
