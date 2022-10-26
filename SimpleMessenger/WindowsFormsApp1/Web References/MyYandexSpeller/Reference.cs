﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Этот исходный текст был создан автоматически: Microsoft.VSDesigner, версия: 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace WindowsFormsApp1.MyYandexSpeller {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="SpellServiceSoap", Namespace="http://speller.yandex.net/services/spellservice")]
    public partial class SpellService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback checkTextOperationCompleted;
        
        private System.Threading.SendOrPostCallback checkTextsOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public SpellService() {
            this.Url = global::WindowsFormsApp1.Properties.Settings.Default.WindowsFormsApp1_MyYandexSpeller_SpellService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event checkTextCompletedEventHandler checkTextCompleted;
        
        /// <remarks/>
        public event checkTextsCompletedEventHandler checkTextsCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://speller.yandex.net/services/spellservice/checkText", RequestElementName="CheckTextRequest", RequestNamespace="http://speller.yandex.net/services/spellservice", ResponseElementName="CheckTextResponse", ResponseNamespace="http://speller.yandex.net/services/spellservice", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlArrayAttribute("SpellResult")]
        [return: System.Xml.Serialization.XmlArrayItemAttribute("error", IsNullable=false)]
        public SpellError[] checkText(string text, [System.Xml.Serialization.XmlAttributeAttribute()] string lang, [System.Xml.Serialization.XmlAttributeAttribute()] [System.ComponentModel.DefaultValueAttribute(0)] int options, [System.Xml.Serialization.XmlAttributeAttribute()] [System.ComponentModel.DefaultValueAttribute("")] string format) {
            object[] results = this.Invoke("checkText", new object[] {
                        text,
                        lang,
                        options,
                        format});
            return ((SpellError[])(results[0]));
        }
        
        /// <remarks/>
        public void checkTextAsync(string text, string lang, int options, string format) {
            this.checkTextAsync(text, lang, options, format, null);
        }
        
        /// <remarks/>
        public void checkTextAsync(string text, string lang, int options, string format, object userState) {
            if ((this.checkTextOperationCompleted == null)) {
                this.checkTextOperationCompleted = new System.Threading.SendOrPostCallback(this.OncheckTextOperationCompleted);
            }
            this.InvokeAsync("checkText", new object[] {
                        text,
                        lang,
                        options,
                        format}, this.checkTextOperationCompleted, userState);
        }
        
        private void OncheckTextOperationCompleted(object arg) {
            if ((this.checkTextCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.checkTextCompleted(this, new checkTextCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://speller.yandex.net/services/spellservice/checkTexts", RequestElementName="CheckTextsRequest", RequestNamespace="http://speller.yandex.net/services/spellservice", ResponseElementName="CheckTextsResponse", ResponseNamespace="http://speller.yandex.net/services/spellservice", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlArrayAttribute("ArrayOfSpellResult")]
        [return: System.Xml.Serialization.XmlArrayItemAttribute("SpellResult", IsNullable=false)]
        [return: System.Xml.Serialization.XmlArrayItemAttribute("error", IsNullable=false, NestingLevel=1)]
        public SpellError[][] checkTexts([System.Xml.Serialization.XmlElementAttribute("text")] string[] text, [System.Xml.Serialization.XmlAttributeAttribute()] string lang, [System.Xml.Serialization.XmlAttributeAttribute()] [System.ComponentModel.DefaultValueAttribute(0)] int options, [System.Xml.Serialization.XmlAttributeAttribute()] [System.ComponentModel.DefaultValueAttribute("")] string format) {
            object[] results = this.Invoke("checkTexts", new object[] {
                        text,
                        lang,
                        options,
                        format});
            return ((SpellError[][])(results[0]));
        }
        
        /// <remarks/>
        public void checkTextsAsync(string[] text, string lang, int options, string format) {
            this.checkTextsAsync(text, lang, options, format, null);
        }
        
        /// <remarks/>
        public void checkTextsAsync(string[] text, string lang, int options, string format, object userState) {
            if ((this.checkTextsOperationCompleted == null)) {
                this.checkTextsOperationCompleted = new System.Threading.SendOrPostCallback(this.OncheckTextsOperationCompleted);
            }
            this.InvokeAsync("checkTexts", new object[] {
                        text,
                        lang,
                        options,
                        format}, this.checkTextsOperationCompleted, userState);
        }
        
        private void OncheckTextsOperationCompleted(object arg) {
            if ((this.checkTextsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.checkTextsCompleted(this, new checkTextsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://speller.yandex.net/services/spellservice")]
    public partial class SpellError {
        
        private string wordField;
        
        private string[] sField;
        
        private int codeField;
        
        private int posField;
        
        private int rowField;
        
        private int colField;
        
        private int lenField;
        
        /// <remarks/>
        public string word {
            get {
                return this.wordField;
            }
            set {
                this.wordField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("s")]
        public string[] s {
            get {
                return this.sField;
            }
            set {
                this.sField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int code {
            get {
                return this.codeField;
            }
            set {
                this.codeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int pos {
            get {
                return this.posField;
            }
            set {
                this.posField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int row {
            get {
                return this.rowField;
            }
            set {
                this.rowField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int col {
            get {
                return this.colField;
            }
            set {
                this.colField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int len {
            get {
                return this.lenField;
            }
            set {
                this.lenField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void checkTextCompletedEventHandler(object sender, checkTextCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class checkTextCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal checkTextCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public SpellError[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((SpellError[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void checkTextsCompletedEventHandler(object sender, checkTextsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class checkTextsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal checkTextsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public SpellError[][] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((SpellError[][])(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591