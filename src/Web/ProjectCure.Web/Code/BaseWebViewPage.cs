using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectCure.Web.WebPages
{
    /// <summary>
    /// Base ViewPage class
    /// </summary>
    public abstract class BaseWebViewPage<TModel> : WebViewPage<TModel>
    {
        #region Constructors
        #endregion

        #region Member Variables
        #endregion

        #region Properties

        /// <summary>
        /// Gets whether the currently logged on user is an admin
        /// </summary>
        public bool IsAdmin
        {
            get { return User != null && User.Identity != null && User.IsInRole(WebRoles.Admin); }
        }

        public bool IsAuthenticated
        {
            get
            {
                return User != null &&
                       !string.IsNullOrEmpty(User.Identity.Name);
            }
        }

        #endregion

        #region Methods
        #endregion

        #region Events
        #endregion
    }
}