﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión del motor en tiempo de ejecución:2.0.50727.3623
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

// 
// This source code was auto-generated by wsdl, Version=2.0.50727.3038.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Web.Services.WebServiceBindingAttribute(Name="WorkFlowcreditos_ServiceSoap", Namespace="http://www.sagradafamilia.hn/")]
public partial class WorkFlowcreditos_Service : System.Web.Services.Protocols.SoapHttpClientProtocol {
    
    private System.Threading.SendOrPostCallback ws_obtener_datos_clienteOperationCompleted;
    
    /// <remarks/>
    public WorkFlowcreditos_Service() {
        this.Url = "http://192.168.2.13/wfc-ws/wfc_Service.asmx";
    }
    
    /// <remarks/>
    public event ws_obtener_datos_clienteCompletedEventHandler ws_obtener_datos_clienteCompleted;
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.sagradafamilia.hn/ws_obtener_datos_cliente", RequestNamespace="http://www.sagradafamilia.hn/", ResponseNamespace="http://www.sagradafamilia.hn/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public string ws_obtener_datos_cliente(int pa_codigo_cliente) {
        object[] results = this.Invoke("ws_obtener_datos_cliente", new object[] {
                    pa_codigo_cliente});
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult Beginws_obtener_datos_cliente(int pa_codigo_cliente, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("ws_obtener_datos_cliente", new object[] {
                    pa_codigo_cliente}, callback, asyncState);
    }
    
    /// <remarks/>
    public string Endws_obtener_datos_cliente(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public void ws_obtener_datos_clienteAsync(int pa_codigo_cliente) {
        this.ws_obtener_datos_clienteAsync(pa_codigo_cliente, null);
    }
    
    /// <remarks/>
    public void ws_obtener_datos_clienteAsync(int pa_codigo_cliente, object userState) {
        if ((this.ws_obtener_datos_clienteOperationCompleted == null)) {
            this.ws_obtener_datos_clienteOperationCompleted = new System.Threading.SendOrPostCallback(this.Onws_obtener_datos_clienteOperationCompleted);
        }
        this.InvokeAsync("ws_obtener_datos_cliente", new object[] {
                    pa_codigo_cliente}, this.ws_obtener_datos_clienteOperationCompleted, userState);
    }
    
    private void Onws_obtener_datos_clienteOperationCompleted(object arg) {
        if ((this.ws_obtener_datos_clienteCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.ws_obtener_datos_clienteCompleted(this, new ws_obtener_datos_clienteCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    public new void CancelAsync(object userState) {
        base.CancelAsync(userState);
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
public delegate void ws_obtener_datos_clienteCompletedEventHandler(object sender, ws_obtener_datos_clienteCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class ws_obtener_datos_clienteCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal ws_obtener_datos_clienteCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
    /// <remarks/>
    public string Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((string)(this.results[0]));
        }
    }
}