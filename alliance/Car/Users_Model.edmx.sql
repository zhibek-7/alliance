
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/20/2016 19:38:20
-- Generated from EDMX file: D:\bektemir_auto\ProjectsAuto\Car\Car\Users_Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [CARS];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FeedBack]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FeedBack];
GO
IF OBJECT_ID(N'[dbo].[State]', 'U') IS NOT NULL
    DROP TABLE [dbo].[State];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [FamilyName] varchar(100)  NOT NULL,
    [FullName] varchar(100)  NOT NULL,
    [UserName] varchar(100)  NOT NULL,
    [ContactPhone] int  NOT NULL,
    [Password] varchar(100)  NOT NULL,
    [Email] varchar(100)  NULL
);
GO

-- Creating table 'FeedBack'
CREATE TABLE [dbo].[FeedBack] (
    [FeedBackId] int IDENTITY(1,1) NOT NULL,
    [FullName] varbinary(50)  NOT NULL,
    [Email] varchar(50)  NOT NULL,
    [MobilleNo] nchar(12)  NOT NULL,
    [StateID] int  NOT NULL,
    [Message] varchar(1000)  NOT NULL
);
GO

-- Creating table 'State'
CREATE TABLE [dbo].[State] (
    [StateId] int IDENTITY(1,1) NOT NULL,
    [StateName] varchar(100)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [UserId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [FeedBackId] in table 'FeedBack'
ALTER TABLE [dbo].[FeedBack]
ADD CONSTRAINT [PK_FeedBack]
    PRIMARY KEY CLUSTERED ([FeedBackId] ASC);
GO

-- Creating primary key on [StateId] in table 'State'
ALTER TABLE [dbo].[State]
ADD CONSTRAINT [PK_State]
    PRIMARY KEY CLUSTERED ([StateId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------