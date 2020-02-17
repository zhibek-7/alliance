<?xml version="1.0" encoding="utf-8"?>
<edmx:Edmx Version="3.0" xmlns:edmx="http://schemas.microsoft.com/ado/2009/11/edmx">
  <!-- EF Runtime content -->
  <edmx:Runtime>
    <!-- SSDL content -->
    <edmx:StorageModels>
    <Schema Namespace="CARS_Model.Store" Provider="System.Data.SqlClient" ProviderManifestToken="2012" Alias="Self" xmlns:store="http://schemas.microsoft.com/ado/2007/12/edm/EntityStoreSchemaGenerator" xmlns:customannotation="http://schemas.microsoft.com/ado/2013/11/edm/customannotation" xmlns="http://schemas.microsoft.com/ado/2009/11/edm/ssdl">
        <EntityType Name="Aksii">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="int" StoreGeneratedPattern="Identity" Nullable="false" />
          <Property Name="BasePic" Type="varbinary(max)" />
          <Property Name="TitleText" Type="nchar" MaxLength="100" />
          <Property Name="ShortDescription" Type="nchar" MaxLength="500" />
          <Property Name="FullPic" Type="varbinary(max)" />
          <Property Name="FullDescription" Type="varchar" MaxLength="800" />
        </EntityType>
        <EntityType Name="Blog">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="int" StoreGeneratedPattern="Identity" Nullable="false" />
          <Property Name="BasePic" Type="varbinary(max)" />
          <Property Name="TitleText" Type="varchar" MaxLength="50" />
          <Property Name="ShortDescription" Type="varchar" MaxLength="500" />
          <Property Name="FullPic" Type="varbinary(max)" />
          <Property Name="FullDescription" Type="varchar" MaxLength="800" />
        </EntityType>
        <EntityType Name="Brends">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="int" StoreGeneratedPattern="Identity" Nullable="false" />
          <Property Name="BrendName" Type="varchar" MaxLength="50" />
          <Property Name="Discount" Type="int" />
          <Property Name="UserId" Type="int" />
        </EntityType>
        <EntityType Name="Clients">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="int" StoreGeneratedPattern="Identity" Nullable="false" />
          <Property Name="FamilyName" Type="varchar" MaxLength="50" />
          <Property Name="FullName" Type="varchar" MaxLength="50" />
          <Property Name="UserName" Type="varchar" MaxLength="50" />
          <Property Name="ContactPhone" Type="varchar" MaxLength="50" />
          <Property Name="Password" Type="varchar" MaxLength="50" />
          <Property Name="Email" Type="varchar" MaxLength="50" />
        </EntityType>
        <EntityType Name="Contraagent">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="int" Nullable="false" />
          <Property Name="Contragent" Type="varchar" MaxLength="50" />
        </EntityType>
        <EntityType Name="CurrancyCode">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="int" StoreGeneratedPattern="Identity" Nullable="false" />
          <Property Name="Name" Type="varchar" MaxLength="150" />
          <Property Name="Value" Type="float" />
          <Property Name="ValueToSom" Type="float" />
          <Property Name="CodeCur" Type="int" Nullable="false" />
        </EntityType>
        <EntityType Name="DecoratedOrder">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="int" StoreGeneratedPattern="Identity" Nullable="false" />
          <Property Name="Name" Type="nvarchar" MaxLength="500" />
        </EntityType>
        <EntityType Name="Discount">
          <Key>
            <PropertyRef Name="id" />
          </Key>
          <Property Name="id" Type="int" StoreGeneratedPattern="Identity" Nullable="false" />
          <Property Name="Id_Contraget" Type="varchar" MaxLength="100" />
          <Property Name="Name_Contragent" Type="varchar" MaxLength="100" />
          <Property Name="Discount" Type="float" />
          <Property Name="DateBegin" Type="date" />
          <Property Name="DateEnd" Type="date" />
          <Property Name="Name_Artikul" Type="varchar" MaxLength="100" />
          <Property Name="Name_Sklad" Type="varchar" MaxLength="100" />
          <Property Name="Name_Service" Type="varchar" MaxLength="100" />
          <Property Name="LimitPrice" Type="float" />
        </EntityType>
        <EntityType Name="FeedBack">
          <Key>
            <PropertyRef Name="FeedBackId" />
          </Key>
          <Property Name="FeedBackId" Type="int" StoreGeneratedPattern="Identity" Nullable="false" />
          <Property Name="FullName" Type="varchar" MaxLength="50" />
          <Property Name="Email" Type="varchar" MaxLength="50" />
          <Property Name="MobilleNo" Type="varchar" MaxLength="50" />
          <Property Name="StateID" Type="varchar" MaxLength="50" />
          <Property Name="Message" Type="varchar" MaxLength="500" />
        </EntityType>
        <EntityType Name="NameOriginalAuto">
          <Key>
            <PropertyRef Name="id" />
          </Key>
          <Property Name="id" Type="int" StoreGeneratedPattern="Identity" Nullable="false" />
          <Property Name="NameAuto" Type="nvarchar" MaxLength="50" />
        </EntityType>
		 <!--Ошибки, обнаруженные при создании:
предупреждение 6002: В таблице или представлении "allianc2_db.dbo.OnlinePay" не определен первичный ключ. Ключ был выведен, а определение таблицы или представления было создано в режиме только для чтения.-->
        <EntityType Name="OnlinePay">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="int" StoreGeneratedPattern="Identity" Nullable="false" />
          <Property Name="NumOrder" Type="varchar(max)" />
          <Property Name="Sum" Type="float" />
        </EntityType>
        <!--Ошибки, обнаруженные при создании:
предупреждение 6002: В таблице или представлении "allianc2_db.dbo.OrderNowRemoteStore" не определен первичный ключ. Ключ был выведен, а определение таблицы или представления было создано в режиме только для чтения.-->
        <EntityType Name="OrderNowRemoteStore">
          <Key>
            <PropertyRef Name="id" />
          </Key>
          <Property Name="id" Type="int" StoreGeneratedPattern="Identity" Nullable="false" />
          <Property Name="name" Type="varchar(max)" />
        </EntityType>
		<!--Ошибки, обнаруженные при создании:
предупреждение 6002: В таблице или представлении "allianc2_db.dbo.Session" не определен первичный ключ. Ключ был выведен, а определение таблицы или представления было создано в режиме только для чтения.-->
        <EntityType Name="Session">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="int" Nullable="false" />
          <Property Name="Created" Type="smalldatetime" />
          <Property Name="Expires" Type="smalldatetime" />
          <Property Name="LockDate" Type="smalldatetime" />
          <Property Name="LockID" Type="int" />
          <Property Name="Locked" Type="bit" />
          <Property Name="ItemContent" Type="varbinary(max)" />
          <Property Name="UserId" Type="int" />
        </EntityType>
        <EntityType Name="Orders">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="int" StoreGeneratedPattern="Identity" Nullable="false" />
          <Property Name="Code" Type="varchar" MaxLength="500" />
          <Property Name="Brend" Type="varchar" MaxLength="500" />
          <Property Name="Articul" Type="varchar" MaxLength="500" />
          <Property Name="NameGood" Type="varchar" MaxLength="500" />
          <Property Name="Supplies" Type="int" />
          <Property Name="MethodDelivery" Type="varchar" MaxLength="50" />
          <Property Name="Existence" Type="int" />
          <Property Name="Days" Type="varchar" MaxLength="50" />
          <Property Name="V_P" Type="varchar" MaxLength="50" />
          <Property Name="Min_Krat" Type="int" />
          <Property Name="Price" Type="float" />
          <Property Name="Clients" Type="int" />
          <Property Name="NumOrder" Type="varchar" MaxLength="500" />
          <Property Name="Category" Type="varchar" MaxLength="500" />
          <Property Name="VIN" Type="varchar" MaxLength="500" />
          <Property Name="CurrancyCode" Type="varchar" MaxLength="500" />
          <Property Name="Date" Type="date" />
          <Property Name="ViewOfContract" Type="varchar" MaxLength="150" />
          <Property Name="Contragent" Type="nvarchar" MaxLength="150" />
          <Property Name="Status" Type="varchar" MaxLength="150" />
          <Property Name="Service" Type="varchar" MaxLength="150" />
          <Property Name="ReleaseDate" Type="date" />
          <Property Name="NameAuto" Type="varchar" MaxLength="500" />
          <Property Name="ColorOfBody" Type="varchar" MaxLength="500" />
          <Property Name="modProdPeriod" Type="varchar" MaxLength="500" />
          <Property Name="Options" Type="varchar" MaxLength="500" />
          <Property Name="Description" Type="varchar" MaxLength="500" />
          <Property Name="Model" Type="varchar" MaxLength="500" />
          <Property Name="catProdPeriod" Type="varchar" MaxLength="500" />
          <Property Name="ColorOfSalon" Type="varchar" MaxLength="150" />
          <Property Name="BrendAuto" Type="varchar" MaxLength="500" />
          <Property Name="CodeOfBody" Type="varchar" MaxLength="50" />
          <Property Name="Sklad" Type="varchar" MaxLength="50" />
          <Property Name="Discount" Type="float" />
          <Property Name="PriceAfterDiscount" Type="float" />
          <Property Name="DateDiscount" Type="date" />
        </EntityType>
        <EntityType Name="Remnants">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="int" StoreGeneratedPattern="Identity" Nullable="false" />
          <Property Name="Code" Type="varchar" MaxLength="500" />
          <Property Name="Brend" Type="varchar" MaxLength="500" />
          <Property Name="Articul" Type="varchar" MaxLength="500" />
          <Property Name="Name" Type="varchar" MaxLength="500" />
          <Property Name="Supplies" Type="int" />
          <Property Name="MethodDelivery" Type="varchar" MaxLength="50" />
          <Property Name="Existence" Type="int" />
          <Property Name="Days" Type="varchar" MaxLength="500" />
          <Property Name="V_P" Type="varchar" MaxLength="500" />
          <Property Name="Min_Krat" Type="int" />
          <Property Name="Price" Type="float" />
          <Property Name="VIN" Type="varchar" MaxLength="50" />
          <Property Name="CurrancyCode" Type="varchar" MaxLength="50" />
          <Property Name="Date" Type="date" />
          <Property Name="ViewOfContract" Type="varchar" MaxLength="500" />
          <Property Name="Contragent" Type="varchar" MaxLength="500" />
          <Property Name="Status" Type="varchar" MaxLength="500" />
          <Property Name="ReleaseDate" Type="date" />
          <Property Name="NameAuto" Type="varchar" MaxLength="500" />
          <Property Name="ColorOfBody" Type="varchar" MaxLength="500" />
          <Property Name="modProdPeriod" Type="varchar" MaxLength="500" />
          <Property Name="Options" Type="varchar" MaxLength="500" />
          <Property Name="Description" Type="varchar" MaxLength="500" />
          <Property Name="Model" Type="varchar" MaxLength="500" />
          <Property Name="catProdPeriod" Type="varchar" MaxLength="500" />
          <Property Name="ColorOfSalon" Type="varchar" MaxLength="500" />
          <Property Name="BrendAuto" Type="varchar" MaxLength="500" />
          <Property Name="CodeOfBody" Type="varchar" MaxLength="500" />
          <Property Name="NumberOfBody" Type="varchar" MaxLength="500" />
          <Property Name="Sklad" Type="varchar" MaxLength="500" />
        </EntityType>
        <EntityType Name="RemoteStore">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="int" StoreGeneratedPattern="Identity" Nullable="false" />
          <Property Name="Code" Type="varchar" MaxLength="500" />
          <Property Name="Brend" Type="varchar" MaxLength="500" />
          <Property Name="Articul" Type="varchar" MaxLength="500" />
          <Property Name="Name" Type="varchar" MaxLength="500" />
          <Property Name="Supplies" Type="int" />
          <Property Name="MethodDelivery" Type="varchar" MaxLength="150" />
          <Property Name="Existence" Type="int" />
          <Property Name="Days" Type="varchar" MaxLength="50" />
          <Property Name="V_P" Type="varchar" MaxLength="50" />
          <Property Name="Min_Krat" Type="int" />
          <Property Name="Price" Type="float" />
          <Property Name="VIN" Type="varchar" MaxLength="500" />
          <Property Name="CurrancyCode" Type="varchar" MaxLength="500" />
          <Property Name="Date" Type="date" />
          <Property Name="ViewOfContract" Type="varchar" MaxLength="150" />
          <Property Name="Contragent" Type="varchar" MaxLength="150" />
          <Property Name="Status" Type="varchar" MaxLength="150" />
          <Property Name="Service" Type="varchar" MaxLength="150" />
          <Property Name="Sklad" Type="varchar" MaxLength="150" />
          <Property Name="Clients" Type="int" />
        </EntityType>
        <EntityType Name="Sale">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="int" StoreGeneratedPattern="Identity" Nullable="false" />
          <Property Name="Code" Type="varchar" MaxLength="50" />
          <Property Name="Brend" Type="varchar" MaxLength="50" />
          <Property Name="Articul" Type="varchar" MaxLength="50" />
          <Property Name="NameGood" Type="varchar" MaxLength="50" />
          <Property Name="Supplies" Type="int" />
          <Property Name="MethodDelivery" Type="varchar" MaxLength="50" />
          <Property Name="Existence" Type="int" />
          <Property Name="Days" Type="varchar" MaxLength="50" />
          <Property Name="V_P" Type="varchar" MaxLength="50" />
          <Property Name="Min_Krat" Type="int" />
          <Property Name="Price" Type="float" />
          <Property Name="Clients" Type="int" />
          <Property Name="Description" Type="varchar" MaxLength="50" />
          <Property Name="Category" Type="varchar" MaxLength="50" />
          <Property Name="VIN" Type="varchar" MaxLength="50" />
          <Property Name="CurrancyCode" Type="varchar" MaxLength="50" />
          <Property Name="Date" Type="date" />
          <Property Name="Adaptation" Type="varchar" MaxLength="500" />
          <Property Name="Sales" Type="float" />
        </EntityType>
        <EntityType Name="Section">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="int" StoreGeneratedPattern="Identity" Nullable="false" />
          <Property Name="NameOfSection" Type="varchar(max)" />
        </EntityType>
        <EntityType Name="State">
          <Key>
            <PropertyRef Name="StateId" />
          </Key>
          <Property Name="StateId" Type="int" StoreGeneratedPattern="Identity" Nullable="false" />
          <Property Name="StateName" Type="varchar" MaxLength="50" />
        </EntityType>
        <EntityType Name="StatusesL">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="int" StoreGeneratedPattern="Identity" Nullable="false" />
          <Property Name="NameStatus" Type="nvarchar" MaxLength="50" />
          <Property Name="IsCheck" Type="bit" Nullable="false" />
        </EntityType>
        <EntityType Name="Supplies">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="int" Nullable="false" StoreGeneratedPattern="Identity" />
          <Property Name="FIO" Type="varchar" MaxLength="50" />
          <Property Name="NameGood" Type="varchar" MaxLength="50" />
          <Property Name="Phone" Type="varchar" MaxLength="50" />
          <Property Name="Address" Type="varchar" MaxLength="50" />
        </EntityType>
        <EntityType Name="sysdiagrams">
          <Key>
            <PropertyRef Name="diagram_id" />
          </Key>
          <Property Name="name" Type="nvarchar" MaxLength="128" Nullable="false" />
          <Property Name="principal_id" Type="int" Nullable="false" />
          <Property Name="diagram_id" Type="int" StoreGeneratedPattern="Identity" Nullable="false" />
          <Property Name="version" Type="int" />
          <Property Name="definition" Type="varbinary(max)" />
        </EntityType>
        <EntityType Name="UrlAutoOriginal">
          <Key>
            <PropertyRef Name="id" />
          </Key>
          <Property Name="id" Type="int" StoreGeneratedPattern="Identity" Nullable="false" />
          <Property Name="UrlAuto" Type="nvarchar" MaxLength="500" />
        </EntityType>
        <EntityType Name="Users">
          <Key>
            <PropertyRef Name="UserId" />
          </Key>
          <Property Name="UserId" Type="int" StoreGeneratedPattern="Identity" Nullable="false" />
          <Property Name="FamilyName" Type="varchar" MaxLength="50" />
          <Property Name="FullName" Type="varchar" MaxLength="50" />
          <Property Name="UserName" Type="varchar" MaxLength="50" />
          <Property Name="ContactPhone" Type="varchar" MaxLength="50" />
          <Property Name="Password" Type="varchar" MaxLength="50" />
          <Property Name="Email" Type="varchar" MaxLength="50" />
          <Property Name="UserRole" Type="varchar" MaxLength="50" />
          <Property Name="Service" Type="varchar" MaxLength="50" />
          <Property Name="Discount" Type="int" />
          <Property Name="DiscountPhaeton" Type="int" />
          <Property Name="DiscountStore" Type="int" />
        </EntityType>
        <EntityType Name="Vacancies">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="int" StoreGeneratedPattern="Identity" Nullable="false" />
          <Property Name="Title" Type="varchar" MaxLength="50" />
          <Property Name="Demands" Type="varchar(max)" />
          <Property Name="Duties" Type="varchar(max)" />
          <Property Name="WorkConditions" Type="varchar(max)" />
        </EntityType>
        <EntityType Name="ViewOfMade">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="int" StoreGeneratedPattern="Identity" Nullable="false" />
          <Property Name="ViewMade" Type="nvarchar" MaxLength="50" />
        </EntityType>
        <EntityType Name="ViewOfReport">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="int" StoreGeneratedPattern="Identity" Nullable="false" />
          <Property Name="ReportName" Type="varchar" MaxLength="50" />
        </EntityType>
        <Association Name="FK_Brends_Users">
          <End Role="Users" Type="Self.Users" Multiplicity="0..1" />
          <End Role="Brends" Type="Self.Brends" Multiplicity="*" />
          <ReferentialConstraint>
            <Principal Role="Users">
              <PropertyRef Name="UserId" />
            </Principal>
            <Dependent Role="Brends">
              <PropertyRef Name="UserId" />
            </Dependent>
          </ReferentialConstraint>
        </Association>
        <Association Name="FK_Contraagent_Contraagent">
          <End Role="Contraagent" Type="Self.Contraagent" Multiplicity="1" />
          <End Role="Contraagent1" Type="Self.Contraagent" Multiplicity="0..1" />
          <ReferentialConstraint>
            <Principal Role="Contraagent">
              <PropertyRef Name="Id" />
            </Principal>
            <Dependent Role="Contraagent1">
              <PropertyRef Name="Id" />
            </Dependent>
          </ReferentialConstraint>
        </Association>
        <Association Name="FK_Orders_Orders">
          <End Role="Supplies" Type="Self.Supplies" Multiplicity="0..1" />
          <End Role="Orders" Type="Self.Orders" Multiplicity="*" />
          <ReferentialConstraint>
            <Principal Role="Supplies">
              <PropertyRef Name="Id" />
            </Principal>
            <Dependent Role="Orders">
              <PropertyRef Name="Supplies" />
            </Dependent>
          </ReferentialConstraint>
        </Association>
        <Association Name="FK_Orders_Users">
          <End Role="Users" Type="Self.Users" Multiplicity="0..1" />
          <End Role="Orders" Type="Self.Orders" Multiplicity="*" />
          <ReferentialConstraint>
            <Principal Role="Users">
              <PropertyRef Name="UserId" />
            </Principal>
            <Dependent Role="Orders">
              <PropertyRef Name="Clients" />
            </Dependent>
          </ReferentialConstraint>
        </Association>
        <Association Name="FK_Remnants_Supplies">
          <End Role="Supplies" Type="Self.Supplies" Multiplicity="0..1" />
          <End Role="Remnants" Type="Self.Remnants" Multiplicity="*" />
          <ReferentialConstraint>
            <Principal Role="Supplies">
              <PropertyRef Name="Id" />
            </Principal>
            <Dependent Role="Remnants">
              <PropertyRef Name="Supplies" />
            </Dependent>
          </ReferentialConstraint>
        </Association>
        <Association Name="FK_Sale_Supplies">
          <End Role="Supplies" Type="Self.Supplies" Multiplicity="0..1" />
          <End Role="Sale" Type="Self.Sale" Multiplicity="*" />
          <ReferentialConstraint>
            <Principal Role="Supplies">
              <PropertyRef Name="Id" />
            </Principal>
            <Dependent Role="Sale">
              <PropertyRef Name="Supplies" />
            </Dependent>
          </ReferentialConstraint>
        </Association>
        <Function Name="sp_upgraddiagrams" Aggregate="false" BuiltIn="false" NiladicFunction="false" IsComposable="false" ParameterTypeSemantics="AllowImplicitConversion" Schema="dbo" />
        <Function Name="usmSelectAllRemnants" Aggregate="false" BuiltIn="false" NiladicFunction="false" IsComposable="false" ParameterTypeSemantics="AllowImplicitConversion" Schema="dbo">
          <Parameter Name="Brend" Type="varchar(max)" Mode="In" />
        </Function>
        <Function Name="usmSelectCurrancyCode" Aggregate="false" BuiltIn="false" NiladicFunction="false" IsComposable="false" ParameterTypeSemantics="AllowImplicitConversion" Schema="dbo" />
        <Function Name="usmSelectDiscountBrend" Aggregate="false" BuiltIn="false" NiladicFunction="false" IsComposable="false" ParameterTypeSemantics="AllowImplicitConversion" Schema="dbo">
          <Parameter Name="Brend" Type="varchar(max)" Mode="In" />
          <Parameter Name="UserID" Type="int" Mode="In" />
        </Function>
        <Function Name="usmSelectNameOriginalAuto" Aggregate="false" BuiltIn="false" NiladicFunction="false" IsComposable="false" ParameterTypeSemantics="AllowImplicitConversion" Schema="dbo" />
        <Function Name="usmSelectSearch" Aggregate="false" BuiltIn="false" NiladicFunction="false" IsComposable="false" ParameterTypeSemantics="AllowImplicitConversion" Schema="dbo">
          <Parameter Name="Code" Type="varchar(max)" Mode="In" />
          <Parameter Name="BrendProducer" Type="varchar(max)" Mode="In" />
        </Function>
        <Function Name="usmSelectUserID" Aggregate="false" BuiltIn="false" NiladicFunction="false" IsComposable="false" ParameterTypeSemantics="AllowImplicitConversion" Schema="dbo">
          <Parameter Name="UserID" Type="int" Mode="In" />
        </Function>
        <EntityContainer Name="CARS_ModelStoreContainer">
          <EntitySet Name="Aksii" EntityType="Self.Aksii" Schema="dbo" store:Type="Tables" />
          <EntitySet Name="Blog" EntityType="Self.Blog" Schema="dbo" store:Type="Tables" />
          <EntitySet Name="Brends" EntityType="Self.Brends" Schema="dbo" store:Type="Tables" />
          <EntitySet Name="Clients" EntityType="Self.Clients" Schema="dbo" store:Type="Tables" />
          <EntitySet Name="Contraagent" EntityType="Self.Contraagent" Schema="dbo" store:Type="Tables" />
          <EntitySet Name="CurrancyCode" EntityType="Self.CurrancyCode" Schema="dbo" store:Type="Tables" />
          <EntitySet Name="DecoratedOrder" EntityType="Self.DecoratedOrder" Schema="dbo" store:Type="Tables" />
          <EntitySet Name="Discount" EntityType="Self.Discount" Schema="dbo" store:Type="Tables" />
          <EntitySet Name="FeedBack" EntityType="Self.FeedBack" Schema="dbo" store:Type="Tables" />
          <EntitySet Name="NameOriginalAuto" EntityType="Self.NameOriginalAuto" Schema="dbo" store:Type="Tables" />
          <EntitySet Name="Orders" EntityType="Self.Orders" Schema="dbo" store:Type="Tables" />
          <EntitySet Name="Remnants" EntityType="Self.Remnants" Schema="dbo" store:Type="Tables" />
          <EntitySet Name="RemoteStore" EntityType="Self.RemoteStore" Schema="dbo" store:Type="Tables" />
          <EntitySet Name="Sale" EntityType="Self.Sale" Schema="dbo" store:Type="Tables" />
          <EntitySet Name="Section" EntityType="Self.Section" Schema="dbo" store:Type="Tables" />
          <EntitySet Name="State" EntityType="Self.State" Schema="dbo" store:Type="Tables" />
          <EntitySet Name="StatusesL" EntityType="Self.StatusesL" Schema="dbo" store:Type="Tables" />
          <EntitySet Name="Supplies" EntityType="Self.Supplies" Schema="dbo" store:Type="Tables" />
          <EntitySet Name="sysdiagrams" EntityType="Self.sysdiagrams" Schema="dbo" store:Type="Tables" />
          <EntitySet Name="UrlAutoOriginal" EntityType="Self.UrlAutoOriginal" Schema="dbo" store:Type="Tables" />
          <EntitySet Name="Users" EntityType="Self.Users" Schema="dbo" store:Type="Tables" />
          <EntitySet Name="Vacancies" EntityType="Self.Vacancies" Schema="dbo" store:Type="Tables" />
          <EntitySet Name="ViewOfMade" EntityType="Self.ViewOfMade" Schema="dbo" store:Type="Tables" />
          <EntitySet Name="ViewOfReport" EntityType="Self.ViewOfReport" Schema="dbo" store:Type="Tables" />
 
 <EntitySet Name="OnlinePay" EntityType="Self.OnlinePay" store:Type="Tables" store:Schema="dbo">
            <DefiningQuery>SELECT 
    [OnlinePay].[Id] AS [Id], 
    [OnlinePay].[NumOrder] AS [NumOrder], 
    [OnlinePay].[Sum] AS [Sum]
    FROM [dbo].[OnlinePay] AS [OnlinePay]</DefiningQuery>
          </EntitySet>
          <EntitySet Name="OrderNowRemoteStore" EntityType="Self.OrderNowRemoteStore" store:Type="Tables" store:Schema="dbo">
            <DefiningQuery>SELECT 
    [OrderNowRemoteStore].[id] AS [id], 
    [OrderNowRemoteStore].[name] AS [name]
    FROM [dbo].[OrderNowRemoteStore] AS [OrderNowRemoteStore]</DefiningQuery>
          </EntitySet>
          <EntitySet Name="Session" EntityType="Self.Session" store:Type="Tables" store:Schema="dbo">
            <DefiningQuery>SELECT 
    [Session].[Id] AS [Id], 
    [Session].[Created] AS [Created], 
    [Session].[Expires] AS [Expires], 
    [Session].[LockDate] AS [LockDate], 
    [Session].[LockID] AS [LockID], 
    [Session].[Locked] AS [Locked], 
    [Session].[ItemContent] AS [ItemContent], 
    [Session].[UserId] AS [UserId]
    FROM [dbo].[Session] AS [Session]</DefiningQuery>
          </EntitySet>
		  
		  
		 <AssociationSet Name="FK_Brends_Users" Association="Self.FK_Brends_Users">
            <End Role="Users" EntitySet="Users" />
            <End Role="Brends" EntitySet="Brends" />
          </AssociationSet>
          <AssociationSet Name="FK_Contraagent_Contraagent" Association="Self.FK_Contraagent_Contraagent">
            <End Role="Contraagent" EntitySet="Contraagent" />
            <End Role="Contraagent1" EntitySet="Contraagent" />
          </AssociationSet>
          <AssociationSet Name="FK_Orders_Orders" Association="Self.FK_Orders_Orders">
            <End Role="Supplies" EntitySet="Supplies" />
            <End Role="Orders" EntitySet="Orders" />
          </AssociationSet>
          <AssociationSet Name="FK_Orders_Users" Association="Self.FK_Orders_Users">
            <End Role="Users" EntitySet="Users" />
            <End Role="Orders" EntitySet="Orders" />
          </AssociationSet>
          <AssociationSet Name="FK_Remnants_Supplies" Association="Self.FK_Remnants_Supplies">
            <End Role="Supplies" EntitySet="Supplies" />
            <End Role="Remnants" EntitySet="Remnants" />
          </AssociationSet>
          <AssociationSet Name="FK_Sale_Supplies" Association="Self.FK_Sale_Supplies">
            <End Role="Supplies" EntitySet="Supplies" />
            <End Role="Sale" EntitySet="Sale" />
          </AssociationSet>
        </EntityContainer>
      </Schema></edmx:StorageModels>
    <!-- CSDL content -->
    <edmx:ConceptualModels>
      <Schema Namespace="CARS_Model" Alias="Self" annotation:UseStrongSpatialTypes="false" xmlns:annotation="http://schemas.microsoft.com/ado/2009/02/edm/annotation" xmlns:customannotation="http://schemas.microsoft.com/ado/2013/11/edm/customannotation" xmlns="http://schemas.microsoft.com/ado/2009/11/edm">
        <EntityContainer Name="UsersCARSEntities" annotation:LazyLoadingEnabled="true">
          <EntitySet Name="Clients" EntityType="CARS_Model.Clients" />
          <EntitySet Name="FeedBack" EntityType="CARS_Model.FeedBack" />
          <EntitySet Name="State" EntityType="CARS_Model.State" />
          <EntitySet Name="Supplies" EntityType="CARS_Model.Supplies" />
          <EntitySet Name="ViewOfMade" EntityType="CARS_Model.ViewOfMade" />
          <EntitySet Name="ViewOfReport" EntityType="CARS_Model.ViewOfReport" />
          <EntitySet Name="Contraagent" EntityType="CARS_Model.Contraagent" />
          <AssociationSet Name="FK_Contraagent_Contraagent" Association="CARS_Model.FK_Contraagent_Contraagent">
            <End Role="Contraagent" EntitySet="Contraagent" />
            <End Role="Contraagent1" EntitySet="Contraagent" />
          </AssociationSet>
          <EntitySet Name="Sale" EntityType="CARS_Model.Sale" />
          <AssociationSet Name="FK_Sale_Supplies" Association="CARS_Model.FK_Sale_Supplies">
            <End Role="Supplies" EntitySet="Supplies" />
            <End Role="Sale" EntitySet="Sale" />
          </AssociationSet>
          <EntitySet Name="StatusesL" EntityType="CARS_Model.StatusesL" />
          <EntitySet Name="NameOriginalAuto" EntityType="CARS_Model.NameOriginalAuto" />
          <EntitySet Name="UrlAutoOriginal" EntityType="CARS_Model.UrlAutoOriginal" />
          <EntitySet Name="DecoratedOrder" EntityType="CARS_Model.DecoratedOrder" />
          <EntitySet Name="Orders" EntityType="CARS_Model.Orders" />
          <AssociationSet Name="FK_Orders_Orders" Association="CARS_Model.FK_Orders_Orders">
            <End Role="Supplies" EntitySet="Supplies" />
            <End Role="Orders" EntitySet="Orders" />
          </AssociationSet>
          <EntitySet Name="Discount" EntityType="CARS_Model.Discount" />
          <EntitySet Name="RemoteStore" EntityType="CARS_Model.RemoteStore" />
          <EntitySet Name="CurrancyCode" EntityType="CARS_Model.CurrancyCode" />
          <EntitySet Name="Aksii" EntityType="CARS_Model.Aksii" />
          <EntitySet Name="Vacancies" EntityType="CARS_Model.Vacancies" />
          <EntitySet Name="Blog" EntityType="CARS_Model.Blog" />
          <EntitySet Name="Section" EntityType="CARS_Model.Section" />
          <EntitySet Name="Remnants" EntityType="CARS_Model.Remnants" />
          <AssociationSet Name="FK_Remnants_Supplies" Association="CARS_Model.FK_Remnants_Supplies">
            <End Role="Supplies" EntitySet="Supplies" />
            <End Role="Remnants" EntitySet="Remnants" />
          </AssociationSet>
          <EntitySet Name="Users" EntityType="CARS_Model.Users" />
          <EntitySet Name="OnlinePay" EntityType="CARS_Model.OnlinePay" />
          <EntitySet Name="OrderNowRemoteStore" EntityType="CARS_Model.OrderNowRemoteStore" />
          <EntitySet Name="Session" EntityType="CARS_Model.Session" />
          <AssociationSet Name="FK_Orders_Users" Association="CARS_Model.FK_Orders_Users">
            <End Role="Users" EntitySet="Users" />
            <End Role="Orders" EntitySet="Orders" />
          </AssociationSet>
          <EntitySet Name="Brends" EntityType="CARS_Model.Brends" />
          <AssociationSet Name="FK_Brends_Users" Association="CARS_Model.FK_Brends_Users">
            <End Role="Users" EntitySet="Users" />
            <End Role="Brends" EntitySet="Brends" />
          </AssociationSet>
          <FunctionImport Name="usmSelectDiscountBrend" ReturnType="Collection(CARS_Model.usmSelectDiscountBrend_Result)">
            <Parameter Name="Brend" Mode="In" Type="String" />
            <Parameter Name="UserID" Mode="In" Type="Int32" />
          </FunctionImport>
          <FunctionImport Name="usmSelectSearch" ReturnType="Collection(CARS_Model.usmSelectSearch_Result)">
            <Parameter Name="Code" Mode="In" Type="String" />
            <Parameter Name="BrendProducer" Mode="In" Type="String" />
          </FunctionImport>
          <EntitySet Name="sysdiagrams" EntityType="CARS_Model.sysdiagrams" />
          <FunctionImport Name="sp_upgraddiagrams" />
          <FunctionImport Name="usmSelectCurrancyCode" ReturnType="Collection(CARS_Model.usmSelectCurrancyCode_Result)" />
          <FunctionImport Name="usmSelectNameOriginalAuto" ReturnType="Collection(CARS_Model.usmSelectNameOriginalAuto_Result)" />
          <FunctionImport Name="usmSelectUserID" ReturnType="Collection(CARS_Model.usmSelectUserID_Result)">
          <Parameter Name="UserID" Mode="In" Type="Int32" />
          </FunctionImport>
          <FunctionImport Name="usmSelectAllRemnants" ReturnType="Collection(CARS_Model.usmSelectAllRemnants_Result)">
            <Parameter Name="Brend" Mode="In" Type="String" />
          </FunctionImport>
          </EntityContainer>
        <EntityType Name="Clients">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="Int32" Nullable="false" annotation:StoreGeneratedPattern="Identity" />
          <Property Name="FamilyName" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="FullName" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="UserName" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="ContactPhone" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="Password" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="Email" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
        </EntityType>
        <EntityType Name="FeedBack">
          <Key>
            <PropertyRef Name="FeedBackId" />
          </Key>
          <Property Name="FeedBackId" Type="Int32" Nullable="false" annotation:StoreGeneratedPattern="Identity" />
          <Property Name="FullName" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="Email" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="MobilleNo" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="StateID" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="Message" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
        </EntityType>
        <EntityType Name="State">
          <Key>
            <PropertyRef Name="StateId" />
          </Key>
          <Property Name="StateId" Type="Int32" Nullable="false" annotation:StoreGeneratedPattern="Identity" />
          <Property Name="StateName" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
        </EntityType>
        <EntityType Name="Supplies">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="Int32" Nullable="false" annotation:StoreGeneratedPattern="Identity" />
          <Property Name="FIO" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="NameGood" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="Phone" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="Address" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <NavigationProperty Name="Sale" Relationship="CARS_Model.FK_Sale_Supplies" FromRole="Supplies" ToRole="Sale" />
          <NavigationProperty Name="Orders" Relationship="CARS_Model.FK_Orders_Orders" FromRole="Supplies" ToRole="Orders" />
          <NavigationProperty Name="Remnants" Relationship="CARS_Model.FK_Remnants_Supplies" FromRole="Supplies" ToRole="Remnants" />
        </EntityType>
        <EntityType Name="ViewOfMade">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="Int32" Nullable="false" annotation:StoreGeneratedPattern="Identity" />
          <Property Name="ViewMade" Type="String" MaxLength="50" FixedLength="false" Unicode="true" />
        </EntityType>
        <EntityType Name="ViewOfReport">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="Int32" Nullable="false" annotation:StoreGeneratedPattern="Identity" />
          <Property Name="ReportName" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
        </EntityType>
        <EntityType Name="Contraagent">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="Int32" Nullable="false" annotation:StoreGeneratedPattern="Identity" />
          <Property Name="Contragent" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <NavigationProperty Name="Contraagent1" Relationship="CARS_Model.FK_Contraagent_Contraagent" FromRole="Contraagent" ToRole="Contraagent1" />
          <NavigationProperty Name="Contraagent2" Relationship="CARS_Model.FK_Contraagent_Contraagent" FromRole="Contraagent1" ToRole="Contraagent" />
        </EntityType>
        <Association Name="FK_Contraagent_Contraagent">
          <End Type="CARS_Model.Contraagent" Role="Contraagent" Multiplicity="1" />
          <End Type="CARS_Model.Contraagent" Role="Contraagent1" Multiplicity="0..1" />
          <ReferentialConstraint>
            <Principal Role="Contraagent">
              <PropertyRef Name="Id" />
            </Principal>
            <Dependent Role="Contraagent1">
              <PropertyRef Name="Id" />
            </Dependent>
          </ReferentialConstraint>
        </Association>
        <EntityType Name="Sale">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="Int32" Nullable="false" annotation:StoreGeneratedPattern="Identity" />
          <Property Name="Code" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="Brend" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="Articul" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="NameGood" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="Supplies" Type="Int32" />
          <Property Name="MethodDelivery" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="Existence" Type="Int32" />
          <Property Name="Days" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="V_P" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="Min_Krat" Type="Int32" />
          <Property Name="Price" Type="Double" />
          <Property Name="Clients" Type="Int32" />
          <Property Name="Description" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="Category" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="VIN" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="CurrancyCode" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="Date" Type="DateTime" Precision="0" />
          <Property Name="Adaptation" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="Sales" Type="Double" />
          <NavigationProperty Name="Supplies1" Relationship="CARS_Model.FK_Sale_Supplies" FromRole="Sale" ToRole="Supplies" />
        </EntityType>
        <Association Name="FK_Sale_Supplies">
          <End Type="CARS_Model.Supplies" Role="Supplies" Multiplicity="0..1" />
          <End Type="CARS_Model.Sale" Role="Sale" Multiplicity="*" />
          <ReferentialConstraint>
            <Principal Role="Supplies">
              <PropertyRef Name="Id" />
            </Principal>
            <Dependent Role="Sale">
              <PropertyRef Name="Supplies" />
            </Dependent>
          </ReferentialConstraint>
        </Association>
        <EntityType Name="StatusesL">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="Int32" Nullable="false" annotation:StoreGeneratedPattern="Identity" />
          <Property Name="NameStatus" Type="String" MaxLength="50" FixedLength="false" Unicode="true" />
          <Property Name="IsCheck" Type="Boolean" Nullable="false" />
        </EntityType>
        <EntityType Name="NameOriginalAuto">
          <Key>
            <PropertyRef Name="id" />
          </Key>
          <Property Name="id" Type="Int32" Nullable="false" annotation:StoreGeneratedPattern="Identity" />
          <Property Name="NameAuto" Type="String" MaxLength="50" FixedLength="false" Unicode="true" />
        </EntityType>
        <EntityType Name="UrlAutoOriginal">
          <Key>
            <PropertyRef Name="id" />
          </Key>
          <Property Name="id" Type="Int32" Nullable="false" annotation:StoreGeneratedPattern="Identity" />
          <Property Name="UrlAuto" Type="String" MaxLength="500" FixedLength="false" Unicode="true" />
        </EntityType>
        <EntityType Name="DecoratedOrder">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="Int32" Nullable="false" annotation:StoreGeneratedPattern="Identity" />
          <Property Name="Name" Type="String" MaxLength="500" FixedLength="false" Unicode="true" />
        </EntityType>
        <EntityType Name="Orders">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="Int32" Nullable="false" annotation:StoreGeneratedPattern="Identity" />
          <Property Name="Code" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="Brend" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="Articul" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="NameGood" Type="String" MaxLength="500" FixedLength="false" Unicode="false" ConcurrencyMode="None" />
          <Property Name="Supplies" Type="Int32" />
          <Property Name="MethodDelivery" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="Existence" Type="Int32" />
          <Property Name="Days" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="V_P" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="Min_Krat" Type="Int32" />
          <Property Name="Price" Type="Double" />
          <Property Name="Clients" Type="Int32" />
          <Property Name="NumOrder" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="Category" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="VIN" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="CurrancyCode" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="Date" Type="DateTime" Precision="0" />
          <Property Name="ViewOfContract" Type="String" MaxLength="150" FixedLength="false" Unicode="false" />
          <Property Name="Contragent" Type="String" MaxLength="150" FixedLength="false" Unicode="true" />
          <Property Name="Status" Type="String" MaxLength="150" FixedLength="false" Unicode="false" />
          <Property Name="Service" Type="String" MaxLength="150" FixedLength="false" Unicode="false" />
          <Property Name="ReleaseDate" Type="DateTime" Precision="0" />
          <Property Name="NameAuto" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="ColorOfBody" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="modProdPeriod" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="Options" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="Description" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="Model" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="catProdPeriod" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="ColorOfSalon" Type="String" MaxLength="150" FixedLength="false" Unicode="false" />
          <Property Name="BrendAuto" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="CodeOfBody" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <NavigationProperty Name="Supplies1" Relationship="CARS_Model.FK_Orders_Orders" FromRole="Orders" ToRole="Supplies" />
          <Property Name="Sklad" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="Discount" Type="Double" />
          <Property Name="PriceAfterDiscount" Type="Double" />
          <Property Name="DateDiscount" Type="DateTime" Precision="0" />
          <NavigationProperty Name="Users" Relationship="CARS_Model.FK_Orders_Users" FromRole="Orders" ToRole="Users" />
        </EntityType>
        <Association Name="FK_Orders_Orders">
          <End Type="CARS_Model.Supplies" Role="Supplies" Multiplicity="0..1" />
          <End Type="CARS_Model.Orders" Role="Orders" Multiplicity="*" />
          <ReferentialConstraint>
            <Principal Role="Supplies">
              <PropertyRef Name="Id" />
            </Principal>
            <Dependent Role="Orders">
              <PropertyRef Name="Supplies" />
            </Dependent>
          </ReferentialConstraint>
        </Association>
        <EntityType Name="Discount">
          <Key>
            <PropertyRef Name="id" />
          </Key>
          <Property Name="id" Type="Int32" Nullable="false" annotation:StoreGeneratedPattern="Identity" />
          <Property Name="Id_Contraget" Type="String" MaxLength="100" FixedLength="false" Unicode="false" />
          <Property Name="Name_Contragent" Type="String" MaxLength="100" FixedLength="false" Unicode="false" />
          <Property Name="Discount1" Type="Double" />
          <Property Name="DateBegin" Type="DateTime" Precision="0" />
          <Property Name="DateEnd" Type="DateTime" Precision="0" />
          <Property Name="Name_Artikul" Type="String" MaxLength="100" FixedLength="false" Unicode="false" />
          <Property Name="Name_Sklad" Type="String" MaxLength="100" FixedLength="false" Unicode="false" />
          <Property Name="Name_Service" Type="String" MaxLength="100" FixedLength="false" Unicode="false" />
          <Property Name="LimitPrice" Type="Double" />
        </EntityType>
        <EntityType Name="RemoteStore">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="Int32" Nullable="false" annotation:StoreGeneratedPattern="Identity" />
          <Property Name="Code" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="Brend" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="Articul" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="Name" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="Supplies" Type="Int32" />
          <Property Name="MethodDelivery" Type="String" MaxLength="150" FixedLength="false" Unicode="false" />
          <Property Name="Existence" Type="Int32" />
          <Property Name="Days" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="V_P" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="Min_Krat" Type="Int32" />
          <Property Name="Price" Type="Double" />
          <Property Name="VIN" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="CurrancyCode" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="Date" Type="DateTime" Precision="0" />
          <Property Name="ViewOfContract" Type="String" MaxLength="150" FixedLength="false" Unicode="false" />
          <Property Name="Contragent" Type="String" MaxLength="150" FixedLength="false" Unicode="false" />
          <Property Name="Status" Type="String" MaxLength="150" FixedLength="false" Unicode="false" />
          <Property Name="Service" Type="String" MaxLength="150" FixedLength="false" Unicode="false" />
          <Property Name="Sklad" Type="String" MaxLength="150" FixedLength="false" Unicode="false" />
          <Property Name="Client" Type="Int32" />
        </EntityType>
        <EntityType Name="CurrancyCode">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="Int32" Nullable="false" annotation:StoreGeneratedPattern="Identity" />
          <Property Name="Name" Type="String" MaxLength="150" FixedLength="false" Unicode="false" />
          <Property Name="Value" Type="Double" />
          <Property Name="ValueToSom" Type="Double" />
          <Property Name="CodeCur" Type="Int32" Nullable="false" />
        </EntityType>
        <EntityType Name="Aksii">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="Int32" Nullable="false" annotation:StoreGeneratedPattern="Identity" />
          <Property Name="BasePic" Type="Binary" MaxLength="Max" FixedLength="false" />
          <Property Name="TitleText" Type="String" MaxLength="100" FixedLength="true" Unicode="true" />
          <Property Name="ShortDescription" Type="String" MaxLength="500" FixedLength="true" Unicode="true" />
          <Property Name="FullPic" Type="Binary" MaxLength="Max" FixedLength="false" />
          <Property Name="FullDescription" Type="String" MaxLength="800" FixedLength="false" Unicode="false" />
        </EntityType>
        <EntityType Name="Vacancies">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="Int32" Nullable="false" annotation:StoreGeneratedPattern="Identity" />
          <Property Name="Title" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="Demands" Type="String" MaxLength="Max" FixedLength="false" Unicode="false" />
          <Property Name="Duties" Type="String" MaxLength="Max" FixedLength="false" Unicode="false" />
          <Property Name="WorkConditions" Type="String" MaxLength="Max" FixedLength="false" Unicode="false" />
        </EntityType>
        <EntityType Name="Blog">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="Int32" Nullable="false" annotation:StoreGeneratedPattern="Identity" />
          <Property Name="BasePic" Type="Binary" MaxLength="Max" FixedLength="false" />
          <Property Name="TitleText" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="ShortDescription" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="FullPic" Type="Binary" MaxLength="Max" FixedLength="false" />
          <Property Name="FullDescription" Type="String" MaxLength="800" FixedLength="false" Unicode="false" />
        </EntityType>
        <EntityType Name="Section">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="Int32" Nullable="false" annotation:StoreGeneratedPattern="Identity" />
          <Property Name="NameOfSection" Type="String" MaxLength="Max" FixedLength="false" Unicode="false" />
        </EntityType>
        <EntityType Name="Remnants">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="Int32" Nullable="false" annotation:StoreGeneratedPattern="Identity" />
          <Property Name="Code" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="Brend" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="Articul" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="Name" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="Supplies" Type="Int32" />
          <Property Name="MethodDelivery" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="Existence" Type="Int32" />
          <Property Name="Days" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="V_P" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="Min_Krat" Type="Int32" />
          <Property Name="Price" Type="Double" />
          <Property Name="VIN" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="CurrancyCode" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="Date" Type="DateTime" Precision="0" />
          <Property Name="ViewOfContract" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="Contragent" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="Status" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="ReleaseDate" Type="DateTime" Precision="0" />
          <Property Name="NameAuto" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="ColorOfBody" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="modProdPeriod" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="Options" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="Description" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="Model" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="catProdPeriod" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="ColorOfSalon" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="BrendAuto" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="CodeOfBody" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="NumberOfBody" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <Property Name="Sklad" Type="String" MaxLength="500" FixedLength="false" Unicode="false" />
          <NavigationProperty Name="Supplies1" Relationship="CARS_Model.FK_Remnants_Supplies" FromRole="Remnants" ToRole="Supplies" />
        </EntityType>
        <Association Name="FK_Remnants_Supplies">
          <End Type="CARS_Model.Supplies" Role="Supplies" Multiplicity="0..1" />
          <End Type="CARS_Model.Remnants" Role="Remnants" Multiplicity="*" />
          <ReferentialConstraint>
            <Principal Role="Supplies">
              <PropertyRef Name="Id" />
            </Principal>
            <Dependent Role="Remnants">
              <PropertyRef Name="Supplies" />
            </Dependent>
          </ReferentialConstraint>
        </Association>
        <EntityType Name="Users">
          <Key>
            <PropertyRef Name="UserId" />
          </Key>
          <Property Name="UserId" Type="Int32" Nullable="false" annotation:StoreGeneratedPattern="Identity" />
          <Property Name="FamilyName" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="FullName" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="UserName" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="ContactPhone" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="Password" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="Email" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="UserRole" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="Service" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="Discount" Type="Int32" />
          <NavigationProperty Name="Orders" Relationship="CARS_Model.FK_Orders_Users" FromRole="Users" ToRole="Orders" />
          <Property Name="DiscountPhaeton" Type="Int32" />
          <Property Name="DiscountStore" Type="Int32" />
          <NavigationProperty Name="Brends" Relationship="CARS_Model.FK_Brends_Users" FromRole="Users" ToRole="Brends" />
        </EntityType>
        <EntityType Name="OnlinePay">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="Int32" Nullable="false" annotation:StoreGeneratedPattern="Identity" />
          <Property Name="NumOrder" Type="String" MaxLength="Max" FixedLength="false" Unicode="false" />
          <Property Name="Sum" Type="Double" />
        </EntityType>
        <EntityType Name="OrderNowRemoteStore">
          <Key>
            <PropertyRef Name="id" />
          </Key>
          <Property Name="id" Type="Int32" Nullable="false" annotation:StoreGeneratedPattern="Identity" />
          <Property Name="name" Type="String" MaxLength="Max" FixedLength="false" Unicode="false" />
        </EntityType>
        <EntityType Name="Session">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="Int32" Nullable="false" />
          <Property Name="Created" Type="DateTime" Precision="0" />
          <Property Name="Expires" Type="DateTime" Precision="0" />
          <Property Name="LockDate" Type="DateTime" Precision="0" />
          <Property Name="LockID" Type="Int32" />
          <Property Name="Locked" Type="Boolean" />
          <Property Name="ItemContent" Type="Binary" MaxLength="Max" FixedLength="false" />
          <Property Name="UserId" Type="Int32" />
        </EntityType>
        <Association Name="FK_Orders_Users">
          <End Type="CARS_Model.Users" Role="Users" Multiplicity="0..1" />
          <End Type="CARS_Model.Orders" Role="Orders" Multiplicity="*" />
          <ReferentialConstraint>
            <Principal Role="Users">
              <PropertyRef Name="UserId" />
            </Principal>
            <Dependent Role="Orders">
              <PropertyRef Name="Clients" />
            </Dependent>
          </ReferentialConstraint>
        </Association>
        <EntityType Name="Brends">
          <Key>
            <PropertyRef Name="Id" />
          </Key>
          <Property Name="Id" Type="Int32" Nullable="false" annotation:StoreGeneratedPattern="Identity" />
          <Property Name="BrendName" Type="String" MaxLength="50" FixedLength="false" Unicode="false" />
          <Property Name="Discount" Type="Int32" />
          <Property Name="UserId" Type="Int32" />
          <NavigationProperty Name="Users" Relationship="CARS_Model.FK_Brends_Users" FromRole="Brends" ToRole="Users" />
        </EntityType>
        <Association Name="FK_Brends_Users">
          <End Type="CARS_Model.Users" Role="Users" Multiplicity="0..1" />
          <End Type="CARS_Model.Brends" Role="Brends" Multiplicity="*" />
          <ReferentialConstraint>
            <Principal Role="Users">
              <PropertyRef Name="UserId" />
            </Principal>
            <Dependent Role="Brends">
              <PropertyRef Name="UserId" />
            </Dependent>
          </ReferentialConstraint>
        </Association>
        <ComplexType Name="usmSelectDiscountBrend_Result">
          <Property Type="Int32" Name="Id" Nullable="false" />
          <Property Type="String" Name="BrendName" Nullable="true" MaxLength="50" />
          <Property Type="Int32" Name="Discount" Nullable="true" />
          <Property Type="Int32" Name="UserId" Nullable="true" />
        </ComplexType>
        <ComplexType Name="usmSelectSearch_Result">
          <Property Type="Int32" Name="Id" Nullable="false" />
          <Property Type="String" Name="Code" Nullable="true" MaxLength="500" />
          <Property Type="String" Name="Brend" Nullable="true" MaxLength="500" />
          <Property Type="String" Name="Articul" Nullable="true" MaxLength="500" />
          <Property Type="String" Name="Name" Nullable="true" MaxLength="500" />
          <Property Type="Int32" Name="Supplies" Nullable="true" />
          <Property Type="String" Name="MethodDelivery" Nullable="true" MaxLength="50" />
          <Property Type="Int32" Name="Existence" Nullable="true" />
          <Property Type="String" Name="Days" Nullable="true" MaxLength="500" />
          <Property Type="String" Name="V_P" Nullable="true" MaxLength="500" />
          <Property Type="Int32" Name="Min_Krat" Nullable="true" />
          <Property Type="Double" Name="Price" Nullable="true" />
          <Property Type="String" Name="VIN" Nullable="true" MaxLength="50" />
          <Property Type="String" Name="CurrancyCode" Nullable="true" MaxLength="50" />
          <Property Type="DateTime" Name="Date" Nullable="true" />
          <Property Type="String" Name="ViewOfContract" Nullable="true" MaxLength="500" />
          <Property Type="String" Name="Contragent" Nullable="true" MaxLength="500" />
          <Property Type="String" Name="Status" Nullable="true" MaxLength="500" />
          <Property Type="DateTime" Name="ReleaseDate" Nullable="true" />
          <Property Type="String" Name="NameAuto" Nullable="true" MaxLength="500" />
          <Property Type="String" Name="ColorOfBody" Nullable="true" MaxLength="500" />
          <Property Type="String" Name="modProdPeriod" Nullable="true" MaxLength="500" />
          <Property Type="String" Name="Options" Nullable="true" MaxLength="500" />
          <Property Type="String" Name="Description" Nullable="true" MaxLength="500" />
          <Property Type="String" Name="Model" Nullable="true" MaxLength="500" />
          <Property Type="String" Name="catProdPeriod" Nullable="true" MaxLength="500" />
          <Property Type="String" Name="ColorOfSalon" Nullable="true" MaxLength="500" />
          <Property Type="String" Name="BrendAuto" Nullable="true" MaxLength="500" />
          <Property Type="String" Name="CodeOfBody" Nullable="true" MaxLength="500" />
          <Property Type="String" Name="NumberOfBody" Nullable="true" MaxLength="500" />
          <Property Type="String" Name="Sklad" Nullable="true" MaxLength="500" />
        </ComplexType>
        <EntityType Name="sysdiagrams">
          <Key>
            <PropertyRef Name="diagram_id" />
          </Key>
          <Property Name="name" Type="String" Nullable="false" MaxLength="128" FixedLength="false" Unicode="true" />
          <Property Name="principal_id" Type="Int32" Nullable="false" />
          <Property Name="diagram_id" Type="Int32" Nullable="false" annotation:StoreGeneratedPattern="Identity" />
          <Property Name="version" Type="Int32" />
          <Property Name="definition" Type="Binary" MaxLength="Max" FixedLength="false" />
        </EntityType>
        <ComplexType Name="usmSelectCurrancyCode_Result">
          <Property Type="Int32" Name="Id" Nullable="false" />
          <Property Type="String" Name="Name" Nullable="true" MaxLength="150" />
          <Property Type="Double" Name="Value" Nullable="true" />
          <Property Type="Double" Name="ValueToSom" Nullable="true" />
          <Property Type="Int32" Name="CodeCur" Nullable="false" />
        </ComplexType>
        <ComplexType Name="usmSelectNameOriginalAuto_Result">
          <Property Type="Int32" Name="id" Nullable="false" />
          <Property Type="String" Name="NameAuto" Nullable="true" MaxLength="50" />
        </ComplexType>
        <ComplexType Name="usmSelectUserID_Result">
          <Property Type="Int32" Name="UserId" Nullable="false" />
          <Property Type="String" Name="FamilyName" Nullable="true" MaxLength="50" />
          <Property Type="String" Name="FullName" Nullable="true" MaxLength="50" />
          <Property Type="String" Name="UserName" Nullable="true" MaxLength="50" />
          <Property Type="String" Name="ContactPhone" Nullable="true" MaxLength="50" />
          <Property Type="String" Name="Password" Nullable="true" MaxLength="50" />
          <Property Type="String" Name="Email" Nullable="true" MaxLength="50" />
          <Property Type="String" Name="UserRole" Nullable="true" MaxLength="50" />
          <Property Type="String" Name="Service" Nullable="true" MaxLength="50" />
          <Property Type="Int32" Name="Discount" Nullable="true" />
          <Property Type="Int32" Name="DiscountPhaeton" Nullable="true" />
          <Property Type="Int32" Name="DiscountStore" Nullable="true" />
        </ComplexType>
        <ComplexType Name="usmSelectAllRemnants_Result">
          <Property Type="Int32" Name="Id" Nullable="false" />
          <Property Type="String" Name="Code" Nullable="true" MaxLength="500" />
          <Property Type="String" Name="Brend" Nullable="true" MaxLength="500" />
          <Property Type="String" Name="Articul" Nullable="true" MaxLength="500" />
          <Property Type="String" Name="Name" Nullable="true" MaxLength="500" />
          <Property Type="Int32" Name="Supplies" Nullable="true" />
          <Property Type="String" Name="MethodDelivery" Nullable="true" MaxLength="50" />
          <Property Type="Int32" Name="Existence" Nullable="true" />
          <Property Type="String" Name="Days" Nullable="true" MaxLength="500" />
          <Property Type="String" Name="V_P" Nullable="true" MaxLength="500" />
          <Property Type="Int32" Name="Min_Krat" Nullable="true" />
          <Property Type="Double" Name="Price" Nullable="true" />
          <Property Type="String" Name="VIN" Nullable="true" MaxLength="50" />
          <Property Type="String" Name="CurrancyCode" Nullable="true" MaxLength="50" />
          <Property Type="DateTime" Name="Date" Nullable="true" />
          <Property Type="String" Name="ViewOfContract" Nullable="true" MaxLength="500" />
          <Property Type="String" Name="Contragent" Nullable="true" MaxLength="500" />
          <Property Type="String" Name="Status" Nullable="true" MaxLength="500" />
          <Property Type="DateTime" Name="ReleaseDate" Nullable="true" />
          <Property Type="String" Name="NameAuto" Nullable="true" MaxLength="500" />
          <Property Type="String" Name="ColorOfBody" Nullable="true" MaxLength="500" />
          <Property Type="String" Name="modProdPeriod" Nullable="true" MaxLength="500" />
          <Property Type="String" Name="Options" Nullable="true" MaxLength="500" />
          <Property Type="String" Name="Description" Nullable="true" MaxLength="500" />
          <Property Type="String" Name="Model" Nullable="true" MaxLength="500" />
          <Property Type="String" Name="catProdPeriod" Nullable="true" MaxLength="500" />
          <Property Type="String" Name="ColorOfSalon" Nullable="true" MaxLength="500" />
          <Property Type="String" Name="BrendAuto" Nullable="true" MaxLength="500" />
          <Property Type="String" Name="CodeOfBody" Nullable="true" MaxLength="500" />
          <Property Type="String" Name="NumberOfBody" Nullable="true" MaxLength="500" />
          <Property Type="String" Name="Sklad" Nullable="true" MaxLength="500" />
        </ComplexType>
        </Schema>
    </edmx:ConceptualModels>
    <!-- C-S mapping content -->
    <edmx:Mappings>
    <Mapping Space="C-S" xmlns="http://schemas.microsoft.com/ado/2009/11/mapping/cs">
  <EntityContainerMapping StorageEntityContainer="CARS_ModelStoreContainer" CdmEntityContainer="UsersCARSEntities">
          <EntitySetMapping Name="Clients">
            <EntityTypeMapping TypeName="CARS_Model.Clients">
              <MappingFragment StoreEntitySet="Clients">
                <ScalarProperty Name="Email" ColumnName="Email" />
                <ScalarProperty Name="Password" ColumnName="Password" />
                <ScalarProperty Name="ContactPhone" ColumnName="ContactPhone" />
                <ScalarProperty Name="UserName" ColumnName="UserName" />
                <ScalarProperty Name="FullName" ColumnName="FullName" />
                <ScalarProperty Name="FamilyName" ColumnName="FamilyName" />
                <ScalarProperty Name="Id" ColumnName="Id" />
              </MappingFragment>
            </EntityTypeMapping>
          </EntitySetMapping>
          <EntitySetMapping Name="FeedBack">
            <EntityTypeMapping TypeName="CARS_Model.FeedBack">
              <MappingFragment StoreEntitySet="FeedBack">
                <ScalarProperty Name="Message" ColumnName="Message" />
                <ScalarProperty Name="StateID" ColumnName="StateID" />
                <ScalarProperty Name="MobilleNo" ColumnName="MobilleNo" />
                <ScalarProperty Name="Email" ColumnName="Email" />
                <ScalarProperty Name="FullName" ColumnName="FullName" />
                <ScalarProperty Name="FeedBackId" ColumnName="FeedBackId" />
              </MappingFragment>
            </EntityTypeMapping>
          </EntitySetMapping>
          <EntitySetMapping Name="State">
            <EntityTypeMapping TypeName="CARS_Model.State">
              <MappingFragment StoreEntitySet="State">
                <ScalarProperty Name="StateName" ColumnName="StateName" />
                <ScalarProperty Name="StateId" ColumnName="StateId" />
              </MappingFragment>
            </EntityTypeMapping>
          </EntitySetMapping>
          <EntitySetMapping Name="Supplies">
            <EntityTypeMapping TypeName="CARS_Model.Supplies">
              <MappingFragment StoreEntitySet="Supplies">
                <ScalarProperty Name="Address" ColumnName="Address" />
                <ScalarProperty Name="Phone" ColumnName="Phone" />
                <ScalarProperty Name="NameGood" ColumnName="NameGood" />
                <ScalarProperty Name="FIO" ColumnName="FIO" />
                <ScalarProperty Name="Id" ColumnName="Id" />
              </MappingFragment>
            </EntityTypeMapping>
          </EntitySetMapping>
          <EntitySetMapping Name="ViewOfMade">
            <EntityTypeMapping TypeName="CARS_Model.ViewOfMade">
              <MappingFragment StoreEntitySet="ViewOfMade">
                <ScalarProperty Name="ViewMade" ColumnName="ViewMade" />
                <ScalarProperty Name="Id" ColumnName="Id" />
              </MappingFragment>
            </EntityTypeMapping>
          </EntitySetMapping>
          <EntitySetMapping Name="ViewOfReport">
            <EntityTypeMapping TypeName="CARS_Model.ViewOfReport">
              <MappingFragment StoreEntitySet="ViewOfReport">
                <ScalarProperty Name="ReportName" ColumnName="ReportName" />
                <ScalarProperty Name="Id" ColumnName="Id" />
              </MappingFragment>
            </EntityTypeMapping>
          </EntitySetMapping>
          <EntitySetMapping Name="Contraagent">
            <EntityTypeMapping TypeName="CARS_Model.Contraagent">
              <MappingFragment StoreEntitySet="Contraagent">
                <ScalarProperty Name="Contragent" ColumnName="Contragent" />
                <ScalarProperty Name="Id" ColumnName="Id" />
              </MappingFragment>
            </EntityTypeMapping>
          </EntitySetMapping>
          <EntitySetMapping Name="Sale">
            <EntityTypeMapping TypeName="CARS_Model.Sale">
              <MappingFragment StoreEntitySet="Sale">
                <ScalarProperty Name="Sales" ColumnName="Sales" />
                <ScalarProperty Name="Adaptation" ColumnName="Adaptation" />
                <ScalarProperty Name="Date" ColumnName="Date" />
                <ScalarProperty Name="CurrancyCode" ColumnName="CurrancyCode" />
                <ScalarProperty Name="VIN" ColumnName="VIN" />
                <ScalarProperty Name="Category" ColumnName="Category" />
                <ScalarProperty Name="Description" ColumnName="Description" />
                <ScalarProperty Name="Clients" ColumnName="Clients" />
                <ScalarProperty Name="Price" ColumnName="Price" />
                <ScalarProperty Name="Min_Krat" ColumnName="Min_Krat" />
                <ScalarProperty Name="V_P" ColumnName="V_P" />
                <ScalarProperty Name="Days" ColumnName="Days" />
                <ScalarProperty Name="Existence" ColumnName="Existence" />
                <ScalarProperty Name="MethodDelivery" ColumnName="MethodDelivery" />
                <ScalarProperty Name="Supplies" ColumnName="Supplies" />
                <ScalarProperty Name="NameGood" ColumnName="NameGood" />
                <ScalarProperty Name="Articul" ColumnName="Articul" />
                <ScalarProperty Name="Brend" ColumnName="Brend" />
                <ScalarProperty Name="Code" ColumnName="Code" />
                <ScalarProperty Name="Id" ColumnName="Id" />
              </MappingFragment>
            </EntityTypeMapping>
          </EntitySetMapping>
          <EntitySetMapping Name="StatusesL">
            <EntityTypeMapping TypeName="CARS_Model.StatusesL">
              <MappingFragment StoreEntitySet="StatusesL">
                <ScalarProperty Name="IsCheck" ColumnName="IsCheck" />
                <ScalarProperty Name="NameStatus" ColumnName="NameStatus" />
                <ScalarProperty Name="Id" ColumnName="Id" />
              </MappingFragment>
            </EntityTypeMapping>
          </EntitySetMapping>
          <EntitySetMapping Name="NameOriginalAuto">
            <EntityTypeMapping TypeName="CARS_Model.NameOriginalAuto">
              <MappingFragment StoreEntitySet="NameOriginalAuto">
                <ScalarProperty Name="NameAuto" ColumnName="NameAuto" />
                <ScalarProperty Name="id" ColumnName="id" />
              </MappingFragment>
            </EntityTypeMapping>
          </EntitySetMapping>
          <EntitySetMapping Name="UrlAutoOriginal">
            <EntityTypeMapping TypeName="CARS_Model.UrlAutoOriginal">
              <MappingFragment StoreEntitySet="UrlAutoOriginal">
                <ScalarProperty Name="UrlAuto" ColumnName="UrlAuto" />
                <ScalarProperty Name="id" ColumnName="id" />
              </MappingFragment>
            </EntityTypeMapping>
          </EntitySetMapping>
          <EntitySetMapping Name="DecoratedOrder">
            <EntityTypeMapping TypeName="CARS_Model.DecoratedOrder">
              <MappingFragment StoreEntitySet="DecoratedOrder">
                <ScalarProperty Name="Name" ColumnName="Name" />
                <ScalarProperty Name="Id" ColumnName="Id" />
              </MappingFragment>
            </EntityTypeMapping>
          </EntitySetMapping>
          <EntitySetMapping Name="Orders">
            <EntityTypeMapping TypeName="CARS_Model.Orders">
              <MappingFragment StoreEntitySet="Orders">
                <ScalarProperty Name="DateDiscount" ColumnName="DateDiscount" />
                <ScalarProperty Name="PriceAfterDiscount" ColumnName="PriceAfterDiscount" />
                <ScalarProperty Name="Discount" ColumnName="Discount" />
                <ScalarProperty Name="Sklad" ColumnName="Sklad" />
                <ScalarProperty Name="CodeOfBody" ColumnName="CodeOfBody" />
                <ScalarProperty Name="BrendAuto" ColumnName="BrendAuto" />
                <ScalarProperty Name="ColorOfSalon" ColumnName="ColorOfSalon" />
                <ScalarProperty Name="catProdPeriod" ColumnName="catProdPeriod" />
                <ScalarProperty Name="Model" ColumnName="Model" />
                <ScalarProperty Name="Description" ColumnName="Description" />
                <ScalarProperty Name="Options" ColumnName="Options" />
                <ScalarProperty Name="modProdPeriod" ColumnName="modProdPeriod" />
                <ScalarProperty Name="ColorOfBody" ColumnName="ColorOfBody" />
                <ScalarProperty Name="NameAuto" ColumnName="NameAuto" />
                <ScalarProperty Name="ReleaseDate" ColumnName="ReleaseDate" />
                <ScalarProperty Name="Service" ColumnName="Service" />
                <ScalarProperty Name="Status" ColumnName="Status" />
                <ScalarProperty Name="Contragent" ColumnName="Contragent" />
                <ScalarProperty Name="ViewOfContract" ColumnName="ViewOfContract" />
                <ScalarProperty Name="Date" ColumnName="Date" />
                <ScalarProperty Name="CurrancyCode" ColumnName="CurrancyCode" />
                <ScalarProperty Name="VIN" ColumnName="VIN" />
                <ScalarProperty Name="Category" ColumnName="Category" />
                <ScalarProperty Name="NumOrder" ColumnName="NumOrder" />
                <ScalarProperty Name="Clients" ColumnName="Clients" />
                <ScalarProperty Name="Price" ColumnName="Price" />
                <ScalarProperty Name="Min_Krat" ColumnName="Min_Krat" />
                <ScalarProperty Name="V_P" ColumnName="V_P" />
                <ScalarProperty Name="Days" ColumnName="Days" />
                <ScalarProperty Name="Existence" ColumnName="Existence" />
                <ScalarProperty Name="MethodDelivery" ColumnName="MethodDelivery" />
                <ScalarProperty Name="Supplies" ColumnName="Supplies" />
                <ScalarProperty Name="NameGood" ColumnName="NameGood" />
                <ScalarProperty Name="Articul" ColumnName="Articul" />
                <ScalarProperty Name="Brend" ColumnName="Brend" />
                <ScalarProperty Name="Code" ColumnName="Code" />
                <ScalarProperty Name="Id" ColumnName="Id" />
              </MappingFragment>
            </EntityTypeMapping>
          </EntitySetMapping>
          <EntitySetMapping Name="Discount">
            <EntityTypeMapping TypeName="CARS_Model.Discount">
              <MappingFragment StoreEntitySet="Discount">
                <ScalarProperty Name="LimitPrice" ColumnName="LimitPrice" />
                <ScalarProperty Name="Name_Service" ColumnName="Name_Service" />
                <ScalarProperty Name="Name_Sklad" ColumnName="Name_Sklad" />
                <ScalarProperty Name="Name_Artikul" ColumnName="Name_Artikul" />
                <ScalarProperty Name="DateEnd" ColumnName="DateEnd" />
                <ScalarProperty Name="DateBegin" ColumnName="DateBegin" />
                <ScalarProperty Name="Discount1" ColumnName="Discount" />
                <ScalarProperty Name="Name_Contragent" ColumnName="Name_Contragent" />
                <ScalarProperty Name="Id_Contraget" ColumnName="Id_Contraget" />
                <ScalarProperty Name="id" ColumnName="id" />
              </MappingFragment>
            </EntityTypeMapping>
          </EntitySetMapping>
          <EntitySetMapping Name="RemoteStore">
            <EntityTypeMapping TypeName="CARS_Model.RemoteStore">
              <MappingFragment StoreEntitySet="RemoteStore">
                <ScalarProperty Name="Client" ColumnName="Clients" />
                <ScalarProperty Name="Sklad" ColumnName="Sklad" />
                <ScalarProperty Name="Service" ColumnName="Service" />
                <ScalarProperty Name="Status" ColumnName="Status" />
                <ScalarProperty Name="Contragent" ColumnName="Contragent" />
                <ScalarProperty Name="ViewOfContract" ColumnName="ViewOfContract" />
                <ScalarProperty Name="Date" ColumnName="Date" />
                <ScalarProperty Name="CurrancyCode" ColumnName="CurrancyCode" />
                <ScalarProperty Name="VIN" ColumnName="VIN" />
                <ScalarProperty Name="Price" ColumnName="Price" />
                <ScalarProperty Name="Min_Krat" ColumnName="Min_Krat" />
                <ScalarProperty Name="V_P" ColumnName="V_P" />
                <ScalarProperty Name="Days" ColumnName="Days" />
                <ScalarProperty Name="Existence" ColumnName="Existence" />
                <ScalarProperty Name="MethodDelivery" ColumnName="MethodDelivery" />
                <ScalarProperty Name="Supplies" ColumnName="Supplies" />
                <ScalarProperty Name="Name" ColumnName="Name" />
                <ScalarProperty Name="Articul" ColumnName="Articul" />
                <ScalarProperty Name="Brend" ColumnName="Brend" />
                <ScalarProperty Name="Code" ColumnName="Code" />
                <ScalarProperty Name="Id" ColumnName="Id" />
              </MappingFragment>
            </EntityTypeMapping>
          </EntitySetMapping>
          <EntitySetMapping Name="CurrancyCode">
            <EntityTypeMapping TypeName="CARS_Model.CurrancyCode">
              <MappingFragment StoreEntitySet="CurrancyCode">
                <ScalarProperty Name="CodeCur" ColumnName="CodeCur" />
                <ScalarProperty Name="ValueToSom" ColumnName="ValueToSom" />
                <ScalarProperty Name="Value" ColumnName="Value" />
                <ScalarProperty Name="Name" ColumnName="Name" />
                <ScalarProperty Name="Id" ColumnName="Id" />
              </MappingFragment>
            </EntityTypeMapping>
          </EntitySetMapping>
          <EntitySetMapping Name="Aksii">
            <EntityTypeMapping TypeName="CARS_Model.Aksii">
              <MappingFragment StoreEntitySet="Aksii">
                <ScalarProperty Name="FullDescription" ColumnName="FullDescription" />
                <ScalarProperty Name="FullPic" ColumnName="FullPic" />
                <ScalarProperty Name="ShortDescription" ColumnName="ShortDescription" />
                <ScalarProperty Name="TitleText" ColumnName="TitleText" />
                <ScalarProperty Name="BasePic" ColumnName="BasePic" />
                <ScalarProperty Name="Id" ColumnName="Id" />
              </MappingFragment>
            </EntityTypeMapping>
          </EntitySetMapping>
          <EntitySetMapping Name="Vacancies">
            <EntityTypeMapping TypeName="CARS_Model.Vacancies">
              <MappingFragment StoreEntitySet="Vacancies">
                <ScalarProperty Name="WorkConditions" ColumnName="WorkConditions" />
                <ScalarProperty Name="Duties" ColumnName="Duties" />
                <ScalarProperty Name="Demands" ColumnName="Demands" />
                <ScalarProperty Name="Title" ColumnName="Title" />
                <ScalarProperty Name="Id" ColumnName="Id" />
              </MappingFragment>
            </EntityTypeMapping>
          </EntitySetMapping>
          <EntitySetMapping Name="Blog">
            <EntityTypeMapping TypeName="CARS_Model.Blog">
              <MappingFragment StoreEntitySet="Blog">
                <ScalarProperty Name="FullDescription" ColumnName="FullDescription" />
                <ScalarProperty Name="FullPic" ColumnName="FullPic" />
                <ScalarProperty Name="ShortDescription" ColumnName="ShortDescription" />
                <ScalarProperty Name="TitleText" ColumnName="TitleText" />
                <ScalarProperty Name="BasePic" ColumnName="BasePic" />
                <ScalarProperty Name="Id" ColumnName="Id" />
              </MappingFragment>
            </EntityTypeMapping>
          </EntitySetMapping>
          <EntitySetMapping Name="Section">
            <EntityTypeMapping TypeName="CARS_Model.Section">
              <MappingFragment StoreEntitySet="Section">
                <ScalarProperty Name="NameOfSection" ColumnName="NameOfSection" />
                <ScalarProperty Name="Id" ColumnName="Id" />
              </MappingFragment>
            </EntityTypeMapping>
          </EntitySetMapping>
          <EntitySetMapping Name="Remnants">
            <EntityTypeMapping TypeName="CARS_Model.Remnants">
              <MappingFragment StoreEntitySet="Remnants">
                <ScalarProperty Name="Sklad" ColumnName="Sklad" />
                <ScalarProperty Name="NumberOfBody" ColumnName="NumberOfBody" />
                <ScalarProperty Name="CodeOfBody" ColumnName="CodeOfBody" />
                <ScalarProperty Name="BrendAuto" ColumnName="BrendAuto" />
                <ScalarProperty Name="ColorOfSalon" ColumnName="ColorOfSalon" />
                <ScalarProperty Name="catProdPeriod" ColumnName="catProdPeriod" />
                <ScalarProperty Name="Model" ColumnName="Model" />
                <ScalarProperty Name="Description" ColumnName="Description" />
                <ScalarProperty Name="Options" ColumnName="Options" />
                <ScalarProperty Name="modProdPeriod" ColumnName="modProdPeriod" />
                <ScalarProperty Name="ColorOfBody" ColumnName="ColorOfBody" />
                <ScalarProperty Name="NameAuto" ColumnName="NameAuto" />
                <ScalarProperty Name="ReleaseDate" ColumnName="ReleaseDate" />
                <ScalarProperty Name="Status" ColumnName="Status" />
                <ScalarProperty Name="Contragent" ColumnName="Contragent" />
                <ScalarProperty Name="ViewOfContract" ColumnName="ViewOfContract" />
                <ScalarProperty Name="Date" ColumnName="Date" />
                <ScalarProperty Name="CurrancyCode" ColumnName="CurrancyCode" />
                <ScalarProperty Name="VIN" ColumnName="VIN" />
                <ScalarProperty Name="Price" ColumnName="Price" />
                <ScalarProperty Name="Min_Krat" ColumnName="Min_Krat" />
                <ScalarProperty Name="V_P" ColumnName="V_P" />
                <ScalarProperty Name="Days" ColumnName="Days" />
                <ScalarProperty Name="Existence" ColumnName="Existence" />
                <ScalarProperty Name="MethodDelivery" ColumnName="MethodDelivery" />
                <ScalarProperty Name="Supplies" ColumnName="Supplies" />
                <ScalarProperty Name="Name" ColumnName="Name" />
                <ScalarProperty Name="Articul" ColumnName="Articul" />
                <ScalarProperty Name="Brend" ColumnName="Brend" />
                <ScalarProperty Name="Code" ColumnName="Code" />
                <ScalarProperty Name="Id" ColumnName="Id" />
              </MappingFragment>
            </EntityTypeMapping>
          </EntitySetMapping>
          <EntitySetMapping Name="Users">
            <EntityTypeMapping TypeName="CARS_Model.Users">
              <MappingFragment StoreEntitySet="Users">
                <ScalarProperty Name="DiscountStore" ColumnName="DiscountStore" />
                <ScalarProperty Name="DiscountPhaeton" ColumnName="DiscountPhaeton" />
                <ScalarProperty Name="Discount" ColumnName="Discount" />
                <ScalarProperty Name="Service" ColumnName="Service" />
                <ScalarProperty Name="UserRole" ColumnName="UserRole" />
                <ScalarProperty Name="Email" ColumnName="Email" />
                <ScalarProperty Name="Password" ColumnName="Password" />
                <ScalarProperty Name="ContactPhone" ColumnName="ContactPhone" />
                <ScalarProperty Name="UserName" ColumnName="UserName" />
                <ScalarProperty Name="FullName" ColumnName="FullName" />
                <ScalarProperty Name="FamilyName" ColumnName="FamilyName" />
                <ScalarProperty Name="UserId" ColumnName="UserId" />
              </MappingFragment>
            </EntityTypeMapping>
          </EntitySetMapping>
		  
		  
		  
		  
		  <EntitySetMapping Name="OnlinePay">
            <EntityTypeMapping TypeName="CARS_Model.OnlinePay">
              <MappingFragment StoreEntitySet="OnlinePay">
                <ScalarProperty Name="Sum" ColumnName="Sum" />
                <ScalarProperty Name="NumOrder" ColumnName="NumOrder" />
                <ScalarProperty Name="Id" ColumnName="Id" />
              </MappingFragment>
            </EntityTypeMapping>
          </EntitySetMapping>
          <EntitySetMapping Name="OrderNowRemoteStore">
            <EntityTypeMapping TypeName="CARS_Model.OrderNowRemoteStore">
              <MappingFragment StoreEntitySet="OrderNowRemoteStore">
                <ScalarProperty Name="name" ColumnName="name" />
                <ScalarProperty Name="id" ColumnName="id" />
              </MappingFragment>
            </EntityTypeMapping>
          </EntitySetMapping>
          <EntitySetMapping Name="Session">
            <EntityTypeMapping TypeName="CARS_Model.Session">
              <MappingFragment StoreEntitySet="Session">
                <ScalarProperty Name="UserId" ColumnName="UserId" />
                <ScalarProperty Name="ItemContent" ColumnName="ItemContent" />
                <ScalarProperty Name="Locked" ColumnName="Locked" />
                <ScalarProperty Name="LockID" ColumnName="LockID" />
                <ScalarProperty Name="LockDate" ColumnName="LockDate" />
                <ScalarProperty Name="Expires" ColumnName="Expires" />
                <ScalarProperty Name="Created" ColumnName="Created" />
                <ScalarProperty Name="Id" ColumnName="Id" />
              </MappingFragment>
            </EntityTypeMapping>
          </EntitySetMapping>
		  
		  
		  
		  
		  
		  
		  
		  
		  
          <EntitySetMapping Name="Brends">
            <EntityTypeMapping TypeName="CARS_Model.Brends">
              <MappingFragment StoreEntitySet="Brends">
                <ScalarProperty Name="UserId" ColumnName="UserId" />
                <ScalarProperty Name="Discount" ColumnName="Discount" />
                <ScalarProperty Name="BrendName" ColumnName="BrendName" />
                <ScalarProperty Name="Id" ColumnName="Id" />
              </MappingFragment>
            </EntityTypeMapping>
          </EntitySetMapping>
          <FunctionImportMapping FunctionImportName="usmSelectDiscountBrend" FunctionName="CARS_Model.Store.usmSelectDiscountBrend">
            <ResultMapping>
              <ComplexTypeMapping TypeName="CARS_Model.usmSelectDiscountBrend_Result">
                <ScalarProperty Name="Id" ColumnName="Id" />
                <ScalarProperty Name="BrendName" ColumnName="BrendName" />
                <ScalarProperty Name="Discount" ColumnName="Discount" />
                <ScalarProperty Name="UserId" ColumnName="UserId" />
              </ComplexTypeMapping>
            </ResultMapping>
          </FunctionImportMapping>
          <FunctionImportMapping FunctionImportName="usmSelectSearch" FunctionName="CARS_Model.Store.usmSelectSearch">
            <ResultMapping>
              <ComplexTypeMapping TypeName="CARS_Model.usmSelectSearch_Result">
                <ScalarProperty Name="Id" ColumnName="Id" />
                <ScalarProperty Name="Code" ColumnName="Code" />
                <ScalarProperty Name="Brend" ColumnName="Brend" />
                <ScalarProperty Name="Articul" ColumnName="Articul" />
                <ScalarProperty Name="Name" ColumnName="Name" />
                <ScalarProperty Name="Supplies" ColumnName="Supplies" />
                <ScalarProperty Name="MethodDelivery" ColumnName="MethodDelivery" />
                <ScalarProperty Name="Existence" ColumnName="Existence" />
                <ScalarProperty Name="Days" ColumnName="Days" />
                <ScalarProperty Name="V_P" ColumnName="V_P" />
                <ScalarProperty Name="Min_Krat" ColumnName="Min_Krat" />
                <ScalarProperty Name="Price" ColumnName="Price" />
                <ScalarProperty Name="VIN" ColumnName="VIN" />
                <ScalarProperty Name="CurrancyCode" ColumnName="CurrancyCode" />
                <ScalarProperty Name="Date" ColumnName="Date" />
                <ScalarProperty Name="ViewOfContract" ColumnName="ViewOfContract" />
                <ScalarProperty Name="Contragent" ColumnName="Contragent" />
                <ScalarProperty Name="Status" ColumnName="Status" />
                <ScalarProperty Name="ReleaseDate" ColumnName="ReleaseDate" />
                <ScalarProperty Name="NameAuto" ColumnName="NameAuto" />
                <ScalarProperty Name="ColorOfBody" ColumnName="ColorOfBody" />
                <ScalarProperty Name="modProdPeriod" ColumnName="modProdPeriod" />
                <ScalarProperty Name="Options" ColumnName="Options" />
                <ScalarProperty Name="Description" ColumnName="Description" />
                <ScalarProperty Name="Model" ColumnName="Model" />
                <ScalarProperty Name="catProdPeriod" ColumnName="catProdPeriod" />
                <ScalarProperty Name="ColorOfSalon" ColumnName="ColorOfSalon" />
                <ScalarProperty Name="BrendAuto" ColumnName="BrendAuto" />
                <ScalarProperty Name="CodeOfBody" ColumnName="CodeOfBody" />
                <ScalarProperty Name="NumberOfBody" ColumnName="NumberOfBody" />
                <ScalarProperty Name="Sklad" ColumnName="Sklad" />
              </ComplexTypeMapping>
            </ResultMapping>
          </FunctionImportMapping>
          <EntitySetMapping Name="sysdiagrams">
            <EntityTypeMapping TypeName="CARS_Model.sysdiagrams">
              <MappingFragment StoreEntitySet="sysdiagrams">
                <ScalarProperty Name="definition" ColumnName="definition" />
                <ScalarProperty Name="version" ColumnName="version" />
                <ScalarProperty Name="diagram_id" ColumnName="diagram_id" />
                <ScalarProperty Name="principal_id" ColumnName="principal_id" />
                <ScalarProperty Name="name" ColumnName="name" />
              </MappingFragment>
            </EntityTypeMapping>
          </EntitySetMapping>
          <FunctionImportMapping FunctionImportName="sp_upgraddiagrams" FunctionName="CARS_Model.Store.sp_upgraddiagrams" />
          <FunctionImportMapping FunctionImportName="usmSelectCurrancyCode" FunctionName="CARS_Model.Store.usmSelectCurrancyCode">
            <ResultMapping>
              <ComplexTypeMapping TypeName="CARS_Model.usmSelectCurrancyCode_Result">
                <ScalarProperty Name="Id" ColumnName="Id" />
                <ScalarProperty Name="Name" ColumnName="Name" />
                <ScalarProperty Name="Value" ColumnName="Value" />
                <ScalarProperty Name="ValueToSom" ColumnName="ValueToSom" />
                <ScalarProperty Name="CodeCur" ColumnName="CodeCur" />
              </ComplexTypeMapping>
            </ResultMapping>
          </FunctionImportMapping>
          <FunctionImportMapping FunctionImportName="usmSelectNameOriginalAuto" FunctionName="CARS_Model.Store.usmSelectNameOriginalAuto">
            <ResultMapping>
              <ComplexTypeMapping TypeName="CARS_Model.usmSelectNameOriginalAuto_Result">
                <ScalarProperty Name="id" ColumnName="id" />
                <ScalarProperty Name="NameAuto" ColumnName="NameAuto" />
              </ComplexTypeMapping>
            </ResultMapping>
          </FunctionImportMapping>
          <FunctionImportMapping FunctionImportName="usmSelectUserID" FunctionName="CARS_Model.Store.usmSelectUserID">
            <ResultMapping>
              <ComplexTypeMapping TypeName="CARS_Model.usmSelectUserID_Result">
                <ScalarProperty Name="UserId" ColumnName="UserId" />
                <ScalarProperty Name="FamilyName" ColumnName="FamilyName" />
                <ScalarProperty Name="FullName" ColumnName="FullName" />
                <ScalarProperty Name="UserName" ColumnName="UserName" />
                <ScalarProperty Name="ContactPhone" ColumnName="ContactPhone" />
                <ScalarProperty Name="Password" ColumnName="Password" />
                <ScalarProperty Name="Email" ColumnName="Email" />
                <ScalarProperty Name="UserRole" ColumnName="UserRole" />
                <ScalarProperty Name="Service" ColumnName="Service" />
                <ScalarProperty Name="Discount" ColumnName="Discount" />
                <ScalarProperty Name="DiscountPhaeton" ColumnName="DiscountPhaeton" />
                <ScalarProperty Name="DiscountStore" ColumnName="DiscountStore" />
              </ComplexTypeMapping>
            </ResultMapping>
          </FunctionImportMapping>
          <FunctionImportMapping FunctionImportName="usmSelectAllRemnants" FunctionName="CARS_Model.Store.usmSelectAllRemnants">
            <ResultMapping>
              <ComplexTypeMapping TypeName="CARS_Model.usmSelectAllRemnants_Result">
                <ScalarProperty Name="Id" ColumnName="Id" />
                <ScalarProperty Name="Code" ColumnName="Code" />
                <ScalarProperty Name="Brend" ColumnName="Brend" />
                <ScalarProperty Name="Articul" ColumnName="Articul" />
                <ScalarProperty Name="Name" ColumnName="Name" />
                <ScalarProperty Name="Supplies" ColumnName="Supplies" />
                <ScalarProperty Name="MethodDelivery" ColumnName="MethodDelivery" />
                <ScalarProperty Name="Existence" ColumnName="Existence" />
                <ScalarProperty Name="Days" ColumnName="Days" />
                <ScalarProperty Name="V_P" ColumnName="V_P" />
                <ScalarProperty Name="Min_Krat" ColumnName="Min_Krat" />
                <ScalarProperty Name="Price" ColumnName="Price" />
                <ScalarProperty Name="VIN" ColumnName="VIN" />
                <ScalarProperty Name="CurrancyCode" ColumnName="CurrancyCode" />
                <ScalarProperty Name="Date" ColumnName="Date" />
                <ScalarProperty Name="ViewOfContract" ColumnName="ViewOfContract" />
                <ScalarProperty Name="Contragent" ColumnName="Contragent" />
                <ScalarProperty Name="Status" ColumnName="Status" />
                <ScalarProperty Name="ReleaseDate" ColumnName="ReleaseDate" />
                <ScalarProperty Name="NameAuto" ColumnName="NameAuto" />
                <ScalarProperty Name="ColorOfBody" ColumnName="ColorOfBody" />
                <ScalarProperty Name="modProdPeriod" ColumnName="modProdPeriod" />
                <ScalarProperty Name="Options" ColumnName="Options" />
                <ScalarProperty Name="Description" ColumnName="Description" />
                <ScalarProperty Name="Model" ColumnName="Model" />
                <ScalarProperty Name="catProdPeriod" ColumnName="catProdPeriod" />
                <ScalarProperty Name="ColorOfSalon" ColumnName="ColorOfSalon" />
                <ScalarProperty Name="BrendAuto" ColumnName="BrendAuto" />
                <ScalarProperty Name="CodeOfBody" ColumnName="CodeOfBody" />
                <ScalarProperty Name="NumberOfBody" ColumnName="NumberOfBody" />
                <ScalarProperty Name="Sklad" ColumnName="Sklad" />
              </ComplexTypeMapping>
            </ResultMapping>
          </FunctionImportMapping>
        </EntityContainerMapping>
</Mapping></edmx:Mappings>
  </edmx:Runtime>
  <!-- EF Designer content (DO NOT EDIT MANUALLY BELOW HERE) -->
  <Designer xmlns="http://schemas.microsoft.com/ado/2009/11/edmx">
    <Connection>
      <DesignerInfoPropertySet>
        <DesignerProperty Name="MetadataArtifactProcessing" Value="EmbedInOutputAssembly" />
      </DesignerInfoPropertySet>
    </Connection>
    <Options>
      <DesignerInfoPropertySet>
        <DesignerProperty Name="ValidateOnBuild" Value="true" />
        <DesignerProperty Name="EnablePluralization" Value="false" />
        <DesignerProperty Name="IncludeForeignKeysInModel" Value="true" />
        <DesignerProperty Name="UseLegacyProvider" Value="False" />
        <DesignerProperty Name="CodeGenerationStrategy" Value="None" />
      </DesignerInfoPropertySet>
    </Options>
    <!-- Diagram content (shape and connector positions) -->
    <Diagrams></Diagrams>
  </Designer>
</edmx:Edmx>