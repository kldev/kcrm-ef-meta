using System;

namespace KCrm.Core.Exceptions {
    public class AppLogicRuleException : Exception {
        public string Code { get; private set; }

        public AppLogicRuleException(string code, string message) : base (message) {
            Code = code;
        }
    }
}
