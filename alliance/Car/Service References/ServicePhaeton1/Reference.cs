﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Car.ServicePhaeton1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SearchArticleResult", Namespace="http://schemas.datacontract.org/2004/07/Core.Classes")]
    [System.SerializableAttribute()]
    public partial class SearchArticleResult : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ErrorMessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsErrorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Car.ServicePhaeton1.SearchArticleResults[] SearchArticleResultsField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ErrorMessage {
            get {
                return this.ErrorMessageField;
            }
            set {
                if ((object.ReferenceEquals(this.ErrorMessageField, value) != true)) {
                    this.ErrorMessageField = value;
                    this.RaisePropertyChanged("ErrorMessage");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsError {
            get {
                return this.IsErrorField;
            }
            set {
                if ((this.IsErrorField.Equals(value) != true)) {
                    this.IsErrorField = value;
                    this.RaisePropertyChanged("IsError");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Car.ServicePhaeton1.SearchArticleResults[] SearchArticleResults {
            get {
                return this.SearchArticleResultsField;
            }
            set {
                if ((object.ReferenceEquals(this.SearchArticleResultsField, value) != true)) {
                    this.SearchArticleResultsField = value;
                    this.RaisePropertyChanged("SearchArticleResults");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SearchArticleResults", Namespace="http://schemas.datacontract.org/2004/07/Core.Classes")]
    [System.SerializableAttribute()]
    public partial class SearchArticleResults : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ArticleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string BrandField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string BrandIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CleanArticleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ItemIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ItemNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Article {
            get {
                return this.ArticleField;
            }
            set {
                if ((object.ReferenceEquals(this.ArticleField, value) != true)) {
                    this.ArticleField = value;
                    this.RaisePropertyChanged("Article");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Brand {
            get {
                return this.BrandField;
            }
            set {
                if ((object.ReferenceEquals(this.BrandField, value) != true)) {
                    this.BrandField = value;
                    this.RaisePropertyChanged("Brand");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BrandId {
            get {
                return this.BrandIdField;
            }
            set {
                if ((object.ReferenceEquals(this.BrandIdField, value) != true)) {
                    this.BrandIdField = value;
                    this.RaisePropertyChanged("BrandId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CleanArticle {
            get {
                return this.CleanArticleField;
            }
            set {
                if ((object.ReferenceEquals(this.CleanArticleField, value) != true)) {
                    this.CleanArticleField = value;
                    this.RaisePropertyChanged("CleanArticle");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ItemId {
            get {
                return this.ItemIdField;
            }
            set {
                if ((object.ReferenceEquals(this.ItemIdField, value) != true)) {
                    this.ItemIdField = value;
                    this.RaisePropertyChanged("ItemId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ItemName {
            get {
                return this.ItemNameField;
            }
            set {
                if ((object.ReferenceEquals(this.ItemNameField, value) != true)) {
                    this.ItemNameField = value;
                    this.RaisePropertyChanged("ItemName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SearchProductResult", Namespace="http://schemas.datacontract.org/2004/07/Core.Classes")]
    [System.SerializableAttribute()]
    public partial class SearchProductResult : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ErrorMessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsErrorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Car.ServicePhaeton1.SearchProductResults[] SearchProductResultsField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ErrorMessage {
            get {
                return this.ErrorMessageField;
            }
            set {
                if ((object.ReferenceEquals(this.ErrorMessageField, value) != true)) {
                    this.ErrorMessageField = value;
                    this.RaisePropertyChanged("ErrorMessage");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsError {
            get {
                return this.IsErrorField;
            }
            set {
                if ((this.IsErrorField.Equals(value) != true)) {
                    this.IsErrorField = value;
                    this.RaisePropertyChanged("IsError");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Car.ServicePhaeton1.SearchProductResults[] SearchProductResults {
            get {
                return this.SearchProductResultsField;
            }
            set {
                if ((object.ReferenceEquals(this.SearchProductResultsField, value) != true)) {
                    this.SearchProductResultsField = value;
                    this.RaisePropertyChanged("SearchProductResults");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SearchProductResults", Namespace="http://schemas.datacontract.org/2004/07/Core.Classes")]
    [System.SerializableAttribute()]
    public partial class SearchProductResults : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ArticleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string BrandField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string BrandIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CleanArticleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CountInPackField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ExpectedShipmentDaysField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int GuaranteedShipmentDaysField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsLocalField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PresenceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int PriceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ProductIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private short ProductTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid SupplierIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string WarehouseField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string WarehouseIdField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Article {
            get {
                return this.ArticleField;
            }
            set {
                if ((object.ReferenceEquals(this.ArticleField, value) != true)) {
                    this.ArticleField = value;
                    this.RaisePropertyChanged("Article");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Brand {
            get {
                return this.BrandField;
            }
            set {
                if ((object.ReferenceEquals(this.BrandField, value) != true)) {
                    this.BrandField = value;
                    this.RaisePropertyChanged("Brand");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BrandId {
            get {
                return this.BrandIdField;
            }
            set {
                if ((object.ReferenceEquals(this.BrandIdField, value) != true)) {
                    this.BrandIdField = value;
                    this.RaisePropertyChanged("BrandId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CleanArticle {
            get {
                return this.CleanArticleField;
            }
            set {
                if ((object.ReferenceEquals(this.CleanArticleField, value) != true)) {
                    this.CleanArticleField = value;
                    this.RaisePropertyChanged("CleanArticle");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int CountInPack {
            get {
                return this.CountInPackField;
            }
            set {
                if ((this.CountInPackField.Equals(value) != true)) {
                    this.CountInPackField = value;
                    this.RaisePropertyChanged("CountInPack");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ExpectedShipmentDays {
            get {
                return this.ExpectedShipmentDaysField;
            }
            set {
                if ((this.ExpectedShipmentDaysField.Equals(value) != true)) {
                    this.ExpectedShipmentDaysField = value;
                    this.RaisePropertyChanged("ExpectedShipmentDays");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int GuaranteedShipmentDays {
            get {
                return this.GuaranteedShipmentDaysField;
            }
            set {
                if ((this.GuaranteedShipmentDaysField.Equals(value) != true)) {
                    this.GuaranteedShipmentDaysField = value;
                    this.RaisePropertyChanged("GuaranteedShipmentDays");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsLocal {
            get {
                return this.IsLocalField;
            }
            set {
                if ((this.IsLocalField.Equals(value) != true)) {
                    this.IsLocalField = value;
                    this.RaisePropertyChanged("IsLocal");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Presence {
            get {
                return this.PresenceField;
            }
            set {
                if ((object.ReferenceEquals(this.PresenceField, value) != true)) {
                    this.PresenceField = value;
                    this.RaisePropertyChanged("Presence");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Price {
            get {
                return this.PriceField;
            }
            set {
                if ((this.PriceField.Equals(value) != true)) {
                    this.PriceField = value;
                    this.RaisePropertyChanged("Price");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ProductId {
            get {
                return this.ProductIdField;
            }
            set {
                if ((object.ReferenceEquals(this.ProductIdField, value) != true)) {
                    this.ProductIdField = value;
                    this.RaisePropertyChanged("ProductId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public short ProductType {
            get {
                return this.ProductTypeField;
            }
            set {
                if ((this.ProductTypeField.Equals(value) != true)) {
                    this.ProductTypeField = value;
                    this.RaisePropertyChanged("ProductType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid SupplierId {
            get {
                return this.SupplierIdField;
            }
            set {
                if ((this.SupplierIdField.Equals(value) != true)) {
                    this.SupplierIdField = value;
                    this.RaisePropertyChanged("SupplierId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Warehouse {
            get {
                return this.WarehouseField;
            }
            set {
                if ((object.ReferenceEquals(this.WarehouseField, value) != true)) {
                    this.WarehouseField = value;
                    this.RaisePropertyChanged("Warehouse");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string WarehouseId {
            get {
                return this.WarehouseIdField;
            }
            set {
                if ((object.ReferenceEquals(this.WarehouseIdField, value) != true)) {
                    this.WarehouseIdField = value;
                    this.RaisePropertyChanged("WarehouseId");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServicePhaeton1.ISearchService")]
    public interface ISearchService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISearchService/SearchArticle", ReplyAction="http://tempuri.org/ISearchService/SearchArticleResponse")]
        Car.ServicePhaeton1.SearchArticleResult SearchArticle(System.Guid UserId, string Article);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISearchService/SearchArticle", ReplyAction="http://tempuri.org/ISearchService/SearchArticleResponse")]
        System.Threading.Tasks.Task<Car.ServicePhaeton1.SearchArticleResult> SearchArticleAsync(System.Guid UserId, string Article);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISearchService/SearchProduct", ReplyAction="http://tempuri.org/ISearchService/SearchProductResponse")]
        Car.ServicePhaeton1.SearchProductResult SearchProduct(System.Guid UserId, string ProductId, string CityId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISearchService/SearchProduct", ReplyAction="http://tempuri.org/ISearchService/SearchProductResponse")]
        System.Threading.Tasks.Task<Car.ServicePhaeton1.SearchProductResult> SearchProductAsync(System.Guid UserId, string ProductId, string CityId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISearchServiceChannel : Car.ServicePhaeton1.ISearchService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SearchServiceClient : System.ServiceModel.ClientBase<Car.ServicePhaeton1.ISearchService>, Car.ServicePhaeton1.ISearchService {
        
        public SearchServiceClient() {
        }
        
        public SearchServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SearchServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SearchServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SearchServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Car.ServicePhaeton1.SearchArticleResult SearchArticle(System.Guid UserId, string Article) {
            return base.Channel.SearchArticle(UserId, Article);
        }
        
        public System.Threading.Tasks.Task<Car.ServicePhaeton1.SearchArticleResult> SearchArticleAsync(System.Guid UserId, string Article) {
            return base.Channel.SearchArticleAsync(UserId, Article);
        }
        
        public Car.ServicePhaeton1.SearchProductResult SearchProduct(System.Guid UserId, string ProductId, string CityId) {
            return base.Channel.SearchProduct(UserId, ProductId, CityId);
        }
        
        public System.Threading.Tasks.Task<Car.ServicePhaeton1.SearchProductResult> SearchProductAsync(System.Guid UserId, string ProductId, string CityId) {
            return base.Channel.SearchProductAsync(UserId, ProductId, CityId);
        }
    }
}
