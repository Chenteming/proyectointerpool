
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 10/02/2010 21:02:29
-- Generated from EDMX file: C:\Users\Martín\Documents\FING\PIS\SVN\trunk\InterpoolCloud\InterpoolCloudWebRole\Data\InterpoolModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [InterpoolDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_GameSuspect_Game]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GamePossibleSuspect] DROP CONSTRAINT [FK_GameSuspect_Game];
GO
IF OBJECT_ID(N'[dbo].[FK_GameSuspect_Suspect]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GamePossibleSuspect] DROP CONSTRAINT [FK_GameSuspect_Suspect];
GO
IF OBJECT_ID(N'[dbo].[FK_GameNodePath]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NodePaths] DROP CONSTRAINT [FK_GameNodePath];
GO
IF OBJECT_ID(N'[dbo].[FK_NodePathFamous_NodePath]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NodePathFamous] DROP CONSTRAINT [FK_NodePathFamous_NodePath];
GO
IF OBJECT_ID(N'[dbo].[FK_NodePathFamous_Famous]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NodePathFamous] DROP CONSTRAINT [FK_NodePathFamous_Famous];
GO
IF OBJECT_ID(N'[dbo].[FK_CityFamous]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Famous] DROP CONSTRAINT [FK_CityFamous];
GO
IF OBJECT_ID(N'[dbo].[FK_FamousNew]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[News] DROP CONSTRAINT [FK_FamousNew];
GO
IF OBJECT_ID(N'[dbo].[FK_NodePathClue_NodePath]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NodePathClue] DROP CONSTRAINT [FK_NodePathClue_NodePath];
GO
IF OBJECT_ID(N'[dbo].[FK_NodePathClue_Clue]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NodePathClue] DROP CONSTRAINT [FK_NodePathClue_Clue];
GO
IF OBJECT_ID(N'[dbo].[FK_ClueCity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Clues] DROP CONSTRAINT [FK_ClueCity];
GO
IF OBJECT_ID(N'[dbo].[FK_ClueFamous]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Clues] DROP CONSTRAINT [FK_ClueFamous];
GO
IF OBJECT_ID(N'[dbo].[FK_CityLevel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cities] DROP CONSTRAINT [FK_CityLevel];
GO
IF OBJECT_ID(N'[dbo].[FK_NodePathCity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NodePaths] DROP CONSTRAINT [FK_NodePathCity];
GO
IF OBJECT_ID(N'[dbo].[FK_GameSuspect1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Suspects] DROP CONSTRAINT [FK_GameSuspect1];
GO
IF OBJECT_ID(N'[dbo].[FK_UserGame]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Games] DROP CONSTRAINT [FK_UserGame];
GO
IF OBJECT_ID(N'[dbo].[FK_CityCityProperty]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CityPropertySet] DROP CONSTRAINT [FK_CityCityProperty];
GO
IF OBJECT_ID(N'[dbo].[FK_NodePathCity1_NodePath]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NodePathPossibleCity] DROP CONSTRAINT [FK_NodePathCity1_NodePath];
GO
IF OBJECT_ID(N'[dbo].[FK_NodePathCity1_City]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NodePathPossibleCity] DROP CONSTRAINT [FK_NodePathCity1_City];
GO
IF OBJECT_ID(N'[dbo].[FK_GameOrderOfArrest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrdersOfArrest] DROP CONSTRAINT [FK_GameOrderOfArrest];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderOfArrestSuspect]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrdersOfArrest] DROP CONSTRAINT [FK_OrderOfArrestSuspect];
GO
IF OBJECT_ID(N'[dbo].[FK_UserLevel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_UserLevel];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Games]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Games];
GO
IF OBJECT_ID(N'[dbo].[NodePaths]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NodePaths];
GO
IF OBJECT_ID(N'[dbo].[Cities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cities];
GO
IF OBJECT_ID(N'[dbo].[Famous]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Famous];
GO
IF OBJECT_ID(N'[dbo].[News]', 'U') IS NOT NULL
    DROP TABLE [dbo].[News];
GO
IF OBJECT_ID(N'[dbo].[Clues]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clues];
GO
IF OBJECT_ID(N'[dbo].[Suspects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Suspects];
GO
IF OBJECT_ID(N'[dbo].[Levels]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Levels];
GO
IF OBJECT_ID(N'[dbo].[CityPropertySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CityPropertySet];
GO
IF OBJECT_ID(N'[dbo].[Parameters]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Parameters];
GO
IF OBJECT_ID(N'[dbo].[Logs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Logs];
GO
IF OBJECT_ID(N'[dbo].[OrdersOfArrest]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrdersOfArrest];
GO
IF OBJECT_ID(N'[dbo].[GamePossibleSuspect]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GamePossibleSuspect];
GO
IF OBJECT_ID(N'[dbo].[NodePathFamous]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NodePathFamous];
GO
IF OBJECT_ID(N'[dbo].[NodePathClue]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NodePathClue];
GO
IF OBJECT_ID(N'[dbo].[NodePathPossibleCity]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NodePathPossibleCity];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [UserIdFacebook] nvarchar(max)  NOT NULL,
    [UserTokenFacebook] nvarchar(max)  NOT NULL,
    [SubLevel] int  NULL,
    [LevelLevelId] int  NOT NULL
);
GO

-- Creating table 'Games'
CREATE TABLE [dbo].[Games] (
    [GameId] int IDENTITY(1,1) NOT NULL,
    [GameTime] smallint  NOT NULL,
    [User_UserId] int  NOT NULL
);
GO

-- Creating table 'NodePaths'
CREATE TABLE [dbo].[NodePaths] (
    [NodePathId] int IDENTITY(1,1) NOT NULL,
    [NodePathOrder] int  NOT NULL,
    [NodePathCurrent] bit  NOT NULL,
    [Game_GameId] int  NOT NULL,
    [City_CityId] int  NOT NULL
);
GO

-- Creating table 'Cities'
CREATE TABLE [dbo].[Cities] (
    [CityId] int IDENTITY(1,1) NOT NULL,
    [CityName] nvarchar(max)  NOT NULL,
    [CityCountry] nvarchar(max)  NOT NULL,
    [CityNumber] int  NOT NULL,
    [Longitud] int  NOT NULL,
    [Latitud] int  NOT NULL,
    [NameFile] nvarchar(max)  NULL,
    [Level_LevelId] int  NOT NULL
);
GO

-- Creating table 'Famous'
CREATE TABLE [dbo].[Famous] (
    [FamousId] int IDENTITY(1,1) NOT NULL,
    [FamousName] nvarchar(max)  NOT NULL,
    [NameFileFamous] nvarchar(max)  NOT NULL,
    [City_CityId] int  NOT NULL
);
GO

-- Creating table 'News'
CREATE TABLE [dbo].[News] (
    [NewId] int IDENTITY(1,1) NOT NULL,
    [NewContent] nvarchar(max)  NOT NULL,
    [Famous_FamousId] int  NOT NULL
);
GO

-- Creating table 'Clues'
CREATE TABLE [dbo].[Clues] (
    [ClueId] int IDENTITY(1,1) NOT NULL,
    [ClueContent] nvarchar(max)  NOT NULL,
    [City_CityId] int  NULL,
    [Famous_FamousId] int  NULL
);
GO

-- Creating table 'Suspects'
CREATE TABLE [dbo].[Suspects] (
    [SuspectId] int IDENTITY(1,1) NOT NULL,
    [SuspectFirstName] nvarchar(max)  NOT NULL,
    [SuspectMusic] nvarchar(max)  NULL,
    [SuspectCinema] nvarchar(max)  NULL,
    [SuspectFacebookId] nvarchar(max)  NOT NULL,
    [SuspectTelevision] nvarchar(max)  NULL,
    [SuspectHometown] nvarchar(max)  NULL,
    [SuspectBirthday] nvarchar(max)  NULL,
    [SuspectLastName] nvarchar(max)  NULL,
    [SuspectGender] nvarchar(max)  NULL,
    [Game_1_GameId] int  NULL
);
GO

-- Creating table 'Levels'
CREATE TABLE [dbo].[Levels] (
    [LevelId] int IDENTITY(1,1) NOT NULL,
    [LevelName] nvarchar(max)  NOT NULL,
    [GroupFacebookId] nvarchar(max)  NOT NULL,
    [LevelNumber] int  NOT NULL
);
GO

-- Creating table 'CityPropertySet'
CREATE TABLE [dbo].[CityPropertySet] (
    [CityPropertyId] int IDENTITY(1,1) NOT NULL,
    [CityPropertyContent] nvarchar(max)  NOT NULL,
    [Dyn] bit  NOT NULL,
    [City_CityId] int  NOT NULL
);
GO

-- Creating table 'Parameters'
CREATE TABLE [dbo].[Parameters] (
    [ParameterId] int IDENTITY(1,1) NOT NULL,
    [ParameterName] nvarchar(max)  NOT NULL,
    [ParameterValue] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Logs'
CREATE TABLE [dbo].[Logs] (
    [LogId] int IDENTITY(1,1) NOT NULL,
    [LogName] nvarchar(max)  NOT NULL,
    [LogType] nvarchar(max)  NOT NULL,
    [LogStackTrace] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'OrdersOfArrest'
CREATE TABLE [dbo].[OrdersOfArrest] (
    [OrderOfArrestId] int IDENTITY(1,1) NOT NULL,
    [GameOrderOfArrest_OrderOfArrest_GameId] int  NOT NULL,
    [Suspect_SuspectId] int  NOT NULL
);
GO

-- Creating table 'GamePossibleSuspect'
CREATE TABLE [dbo].[GamePossibleSuspect] (
    [Game_GameId] int  NOT NULL,
    [PossibleSuspect_SuspectId] int  NOT NULL
);
GO

-- Creating table 'NodePathFamous'
CREATE TABLE [dbo].[NodePathFamous] (
    [NodePath_NodePathId] int  NOT NULL,
    [Famous_FamousId] int  NOT NULL
);
GO

-- Creating table 'NodePathClue'
CREATE TABLE [dbo].[NodePathClue] (
    [NodePath_NodePathId] int  NOT NULL,
    [Clue_ClueId] int  NOT NULL
);
GO

-- Creating table 'NodePathPossibleCity'
CREATE TABLE [dbo].[NodePathPossibleCity] (
    [NodePath_1_NodePathId] int  NOT NULL,
    [PossibleCities_CityId] int  NOT NULL
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

-- Creating primary key on [GameId] in table 'Games'
ALTER TABLE [dbo].[Games]
ADD CONSTRAINT [PK_Games]
    PRIMARY KEY CLUSTERED ([GameId] ASC);
GO

-- Creating primary key on [NodePathId] in table 'NodePaths'
ALTER TABLE [dbo].[NodePaths]
ADD CONSTRAINT [PK_NodePaths]
    PRIMARY KEY CLUSTERED ([NodePathId] ASC);
GO

-- Creating primary key on [CityId] in table 'Cities'
ALTER TABLE [dbo].[Cities]
ADD CONSTRAINT [PK_Cities]
    PRIMARY KEY CLUSTERED ([CityId] ASC);
GO

-- Creating primary key on [FamousId] in table 'Famous'
ALTER TABLE [dbo].[Famous]
ADD CONSTRAINT [PK_Famous]
    PRIMARY KEY CLUSTERED ([FamousId] ASC);
GO

-- Creating primary key on [NewId] in table 'News'
ALTER TABLE [dbo].[News]
ADD CONSTRAINT [PK_News]
    PRIMARY KEY CLUSTERED ([NewId] ASC);
GO

-- Creating primary key on [ClueId] in table 'Clues'
ALTER TABLE [dbo].[Clues]
ADD CONSTRAINT [PK_Clues]
    PRIMARY KEY CLUSTERED ([ClueId] ASC);
GO

-- Creating primary key on [SuspectId] in table 'Suspects'
ALTER TABLE [dbo].[Suspects]
ADD CONSTRAINT [PK_Suspects]
    PRIMARY KEY CLUSTERED ([SuspectId] ASC);
GO

-- Creating primary key on [LevelId] in table 'Levels'
ALTER TABLE [dbo].[Levels]
ADD CONSTRAINT [PK_Levels]
    PRIMARY KEY CLUSTERED ([LevelId] ASC);
GO

-- Creating primary key on [CityPropertyId] in table 'CityPropertySet'
ALTER TABLE [dbo].[CityPropertySet]
ADD CONSTRAINT [PK_CityPropertySet]
    PRIMARY KEY CLUSTERED ([CityPropertyId] ASC);
GO

-- Creating primary key on [ParameterId] in table 'Parameters'
ALTER TABLE [dbo].[Parameters]
ADD CONSTRAINT [PK_Parameters]
    PRIMARY KEY CLUSTERED ([ParameterId] ASC);
GO

-- Creating primary key on [LogId] in table 'Logs'
ALTER TABLE [dbo].[Logs]
ADD CONSTRAINT [PK_Logs]
    PRIMARY KEY CLUSTERED ([LogId] ASC);
GO

-- Creating primary key on [OrderOfArrestId] in table 'OrdersOfArrest'
ALTER TABLE [dbo].[OrdersOfArrest]
ADD CONSTRAINT [PK_OrdersOfArrest]
    PRIMARY KEY CLUSTERED ([OrderOfArrestId] ASC);
GO

-- Creating primary key on [Game_GameId], [PossibleSuspect_SuspectId] in table 'GamePossibleSuspect'
ALTER TABLE [dbo].[GamePossibleSuspect]
ADD CONSTRAINT [PK_GamePossibleSuspect]
    PRIMARY KEY NONCLUSTERED ([Game_GameId], [PossibleSuspect_SuspectId] ASC);
GO

-- Creating primary key on [NodePath_NodePathId], [Famous_FamousId] in table 'NodePathFamous'
ALTER TABLE [dbo].[NodePathFamous]
ADD CONSTRAINT [PK_NodePathFamous]
    PRIMARY KEY NONCLUSTERED ([NodePath_NodePathId], [Famous_FamousId] ASC);
GO

-- Creating primary key on [NodePath_NodePathId], [Clue_ClueId] in table 'NodePathClue'
ALTER TABLE [dbo].[NodePathClue]
ADD CONSTRAINT [PK_NodePathClue]
    PRIMARY KEY NONCLUSTERED ([NodePath_NodePathId], [Clue_ClueId] ASC);
GO

-- Creating primary key on [NodePath_1_NodePathId], [PossibleCities_CityId] in table 'NodePathPossibleCity'
ALTER TABLE [dbo].[NodePathPossibleCity]
ADD CONSTRAINT [PK_NodePathPossibleCity]
    PRIMARY KEY NONCLUSTERED ([NodePath_1_NodePathId], [PossibleCities_CityId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Game_GameId] in table 'GamePossibleSuspect'
ALTER TABLE [dbo].[GamePossibleSuspect]
ADD CONSTRAINT [FK_GameSuspect_Game]
    FOREIGN KEY ([Game_GameId])
    REFERENCES [dbo].[Games]
        ([GameId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [PossibleSuspect_SuspectId] in table 'GamePossibleSuspect'
ALTER TABLE [dbo].[GamePossibleSuspect]
ADD CONSTRAINT [FK_GameSuspect_Suspect]
    FOREIGN KEY ([PossibleSuspect_SuspectId])
    REFERENCES [dbo].[Suspects]
        ([SuspectId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GameSuspect_Suspect'
CREATE INDEX [IX_FK_GameSuspect_Suspect]
ON [dbo].[GamePossibleSuspect]
    ([PossibleSuspect_SuspectId]);
GO

-- Creating foreign key on [Game_GameId] in table 'NodePaths'
ALTER TABLE [dbo].[NodePaths]
ADD CONSTRAINT [FK_GameNodePath]
    FOREIGN KEY ([Game_GameId])
    REFERENCES [dbo].[Games]
        ([GameId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GameNodePath'
CREATE INDEX [IX_FK_GameNodePath]
ON [dbo].[NodePaths]
    ([Game_GameId]);
GO

-- Creating foreign key on [NodePath_NodePathId] in table 'NodePathFamous'
ALTER TABLE [dbo].[NodePathFamous]
ADD CONSTRAINT [FK_NodePathFamous_NodePath]
    FOREIGN KEY ([NodePath_NodePathId])
    REFERENCES [dbo].[NodePaths]
        ([NodePathId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Famous_FamousId] in table 'NodePathFamous'
ALTER TABLE [dbo].[NodePathFamous]
ADD CONSTRAINT [FK_NodePathFamous_Famous]
    FOREIGN KEY ([Famous_FamousId])
    REFERENCES [dbo].[Famous]
        ([FamousId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_NodePathFamous_Famous'
CREATE INDEX [IX_FK_NodePathFamous_Famous]
ON [dbo].[NodePathFamous]
    ([Famous_FamousId]);
GO

-- Creating foreign key on [City_CityId] in table 'Famous'
ALTER TABLE [dbo].[Famous]
ADD CONSTRAINT [FK_CityFamous]
    FOREIGN KEY ([City_CityId])
    REFERENCES [dbo].[Cities]
        ([CityId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CityFamous'
CREATE INDEX [IX_FK_CityFamous]
ON [dbo].[Famous]
    ([City_CityId]);
GO

-- Creating foreign key on [Famous_FamousId] in table 'News'
ALTER TABLE [dbo].[News]
ADD CONSTRAINT [FK_FamousNew]
    FOREIGN KEY ([Famous_FamousId])
    REFERENCES [dbo].[Famous]
        ([FamousId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FamousNew'
CREATE INDEX [IX_FK_FamousNew]
ON [dbo].[News]
    ([Famous_FamousId]);
GO

-- Creating foreign key on [NodePath_NodePathId] in table 'NodePathClue'
ALTER TABLE [dbo].[NodePathClue]
ADD CONSTRAINT [FK_NodePathClue_NodePath]
    FOREIGN KEY ([NodePath_NodePathId])
    REFERENCES [dbo].[NodePaths]
        ([NodePathId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Clue_ClueId] in table 'NodePathClue'
ALTER TABLE [dbo].[NodePathClue]
ADD CONSTRAINT [FK_NodePathClue_Clue]
    FOREIGN KEY ([Clue_ClueId])
    REFERENCES [dbo].[Clues]
        ([ClueId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_NodePathClue_Clue'
CREATE INDEX [IX_FK_NodePathClue_Clue]
ON [dbo].[NodePathClue]
    ([Clue_ClueId]);
GO

-- Creating foreign key on [City_CityId] in table 'Clues'
ALTER TABLE [dbo].[Clues]
ADD CONSTRAINT [FK_ClueCity]
    FOREIGN KEY ([City_CityId])
    REFERENCES [dbo].[Cities]
        ([CityId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ClueCity'
CREATE INDEX [IX_FK_ClueCity]
ON [dbo].[Clues]
    ([City_CityId]);
GO

-- Creating foreign key on [Famous_FamousId] in table 'Clues'
ALTER TABLE [dbo].[Clues]
ADD CONSTRAINT [FK_ClueFamous]
    FOREIGN KEY ([Famous_FamousId])
    REFERENCES [dbo].[Famous]
        ([FamousId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ClueFamous'
CREATE INDEX [IX_FK_ClueFamous]
ON [dbo].[Clues]
    ([Famous_FamousId]);
GO

-- Creating foreign key on [Level_LevelId] in table 'Cities'
ALTER TABLE [dbo].[Cities]
ADD CONSTRAINT [FK_CityLevel]
    FOREIGN KEY ([Level_LevelId])
    REFERENCES [dbo].[Levels]
        ([LevelId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CityLevel'
CREATE INDEX [IX_FK_CityLevel]
ON [dbo].[Cities]
    ([Level_LevelId]);
GO

-- Creating foreign key on [City_CityId] in table 'NodePaths'
ALTER TABLE [dbo].[NodePaths]
ADD CONSTRAINT [FK_NodePathCity]
    FOREIGN KEY ([City_CityId])
    REFERENCES [dbo].[Cities]
        ([CityId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_NodePathCity'
CREATE INDEX [IX_FK_NodePathCity]
ON [dbo].[NodePaths]
    ([City_CityId]);
GO

-- Creating foreign key on [Game_1_GameId] in table 'Suspects'
ALTER TABLE [dbo].[Suspects]
ADD CONSTRAINT [FK_GameSuspect1]
    FOREIGN KEY ([Game_1_GameId])
    REFERENCES [dbo].[Games]
        ([GameId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GameSuspect1'
CREATE INDEX [IX_FK_GameSuspect1]
ON [dbo].[Suspects]
    ([Game_1_GameId]);
GO

-- Creating foreign key on [User_UserId] in table 'Games'
ALTER TABLE [dbo].[Games]
ADD CONSTRAINT [FK_UserGame]
    FOREIGN KEY ([User_UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserGame'
CREATE INDEX [IX_FK_UserGame]
ON [dbo].[Games]
    ([User_UserId]);
GO

-- Creating foreign key on [City_CityId] in table 'CityPropertySet'
ALTER TABLE [dbo].[CityPropertySet]
ADD CONSTRAINT [FK_CityCityProperty]
    FOREIGN KEY ([City_CityId])
    REFERENCES [dbo].[Cities]
        ([CityId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CityCityProperty'
CREATE INDEX [IX_FK_CityCityProperty]
ON [dbo].[CityPropertySet]
    ([City_CityId]);
GO

-- Creating foreign key on [NodePath_1_NodePathId] in table 'NodePathPossibleCity'
ALTER TABLE [dbo].[NodePathPossibleCity]
ADD CONSTRAINT [FK_NodePathCity1_NodePath]
    FOREIGN KEY ([NodePath_1_NodePathId])
    REFERENCES [dbo].[NodePaths]
        ([NodePathId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [PossibleCities_CityId] in table 'NodePathPossibleCity'
ALTER TABLE [dbo].[NodePathPossibleCity]
ADD CONSTRAINT [FK_NodePathCity1_City]
    FOREIGN KEY ([PossibleCities_CityId])
    REFERENCES [dbo].[Cities]
        ([CityId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_NodePathCity1_City'
CREATE INDEX [IX_FK_NodePathCity1_City]
ON [dbo].[NodePathPossibleCity]
    ([PossibleCities_CityId]);
GO

-- Creating foreign key on [GameOrderOfArrest_OrderOfArrest_GameId] in table 'OrdersOfArrest'
ALTER TABLE [dbo].[OrdersOfArrest]
ADD CONSTRAINT [FK_GameOrderOfArrest]
    FOREIGN KEY ([GameOrderOfArrest_OrderOfArrest_GameId])
    REFERENCES [dbo].[Games]
        ([GameId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GameOrderOfArrest'
CREATE INDEX [IX_FK_GameOrderOfArrest]
ON [dbo].[OrdersOfArrest]
    ([GameOrderOfArrest_OrderOfArrest_GameId]);
GO

-- Creating foreign key on [Suspect_SuspectId] in table 'OrdersOfArrest'
ALTER TABLE [dbo].[OrdersOfArrest]
ADD CONSTRAINT [FK_OrderOfArrestSuspect]
    FOREIGN KEY ([Suspect_SuspectId])
    REFERENCES [dbo].[Suspects]
        ([SuspectId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderOfArrestSuspect'
CREATE INDEX [IX_FK_OrderOfArrestSuspect]
ON [dbo].[OrdersOfArrest]
    ([Suspect_SuspectId]);
GO

-- Creating foreign key on [LevelLevelId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_UserLevel]
    FOREIGN KEY ([LevelLevelId])
    REFERENCES [dbo].[Levels]
        ([LevelId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserLevel'
CREATE INDEX [IX_FK_UserLevel]
ON [dbo].[Users]
    ([LevelLevelId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------