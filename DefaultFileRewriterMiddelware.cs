﻿using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJKP.InAzure.Web
{
    public class DefaultFileRewriterMiddleware : OwinMiddleware
    {
        private readonly FileServerOptions _options;

        /// <summary>
        /// Instantiates the middleware with an optional pointer to the next component.
        /// </summary>
        /// <param name="next"/>
        /// <param name="options"></param>
        public DefaultFileRewriterMiddleware(OwinMiddleware next, FileServerOptions options) : base(next)
        {
            _options = options;
        }

        #region Overrides of OwinMiddleware

        /// <summary>
        /// Process an individual request.
        /// </summary>
        /// <param name="context"/>
        /// <returns/>
        public override async Task Invoke(IOwinContext context)
        {
            IFileInfo fileInfo;
            PathString subpath;

            if (!IsApi(context.Request.Path) && ( !TryMatchPath(context, _options.RequestPath, false, out subpath) ||
                !_options.FileSystem.TryGetFileInfo(subpath.Value, out fileInfo)))
            {
                context.Request.Path = new PathString(_options.RequestPath + "/index.html");
            }

            await Next.Invoke(context);
        }

        #endregion

        internal static bool PathEndsInSlash(PathString path)
        {
            return path.Value.EndsWith("/", StringComparison.Ordinal);
        }

        internal static bool IsApi(PathString path)
        {
            return path.Value.StartsWith("/api", StringComparison.Ordinal) || path.Value.StartsWith("/swagger", StringComparison.Ordinal);
        }

        internal static bool TryMatchPath(IOwinContext context, PathString matchUrl, bool forDirectory, out PathString subpath)
        {
            var path = context.Request.Path;

            if (forDirectory && !PathEndsInSlash(path))
            {
                path += new PathString("/");
            }

            if (path.StartsWithSegments(matchUrl, out subpath))
            {
                return true;
            }
            return false;
        }
    }
}
