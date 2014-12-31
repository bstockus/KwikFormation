using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.BindingPaths {
    public static class BindingPathsHelper {

        public static string GetCurrentScopeName(string bindingPath) {
            return bindingPath.Split('.').First();
        }

        public static string GetNextScopeName(string bindingPath) {
            return bindingPath.Substring(GetCurrentScopeName(bindingPath).Length + 1);
        }

    }
}
