USE [interpooldb]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CityNewsStatic_City]') AND parent_object_id = OBJECT_ID(N'[dbo].[CityNewsStatic]'))
ALTER TABLE [dbo].[CityNewsStatic] DROP CONSTRAINT [FK_CityNewsStatic_City]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CityNewsStatic_NewsStatic]') AND parent_object_id = OBJECT_ID(N'[dbo].[CityNewsStatic]'))
ALTER TABLE [dbo].[CityNewsStatic] DROP CONSTRAINT [FK_CityNewsStatic_NewsStatic]
GO

USE [interpooldb]
GO

/****** Object:  Table [dbo].[CityNewsStatic]    Script Date: 09/18/2010 17:07:09 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CityNewsStatic]') AND type in (N'U'))
DROP TABLE [dbo].[CityNewsStatic]
GO

USE [interpooldb]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CityCityProperty]') AND parent_object_id = OBJECT_ID(N'[dbo].[CityPropertySet]'))
ALTER TABLE [dbo].[CityPropertySet] DROP CONSTRAINT [FK_CityCityProperty]
GO

USE [interpooldb]
GO

/****** Object:  Table [dbo].[CityPropertySet]    Script Date: 09/18/2010 17:07:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CityPropertySet]') AND type in (N'U'))
DROP TABLE [dbo].[CityPropertySet]
GO

USE [interpooldb]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_FamousNewsStatic_Famous]') AND parent_object_id = OBJECT_ID(N'[dbo].[FamousNewsStatic]'))
ALTER TABLE [dbo].[FamousNewsStatic] DROP CONSTRAINT [FK_FamousNewsStatic_Famous]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_FamousNewsStatic_NewsStatic]') AND parent_object_id = OBJECT_ID(N'[dbo].[FamousNewsStatic]'))
ALTER TABLE [dbo].[FamousNewsStatic] DROP CONSTRAINT [FK_FamousNewsStatic_NewsStatic]
GO

USE [interpooldb]
GO

/****** Object:  Table [dbo].[FamousNewsStatic]    Script Date: 09/18/2010 17:08:10 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FamousNewsStatic]') AND type in (N'U'))
DROP TABLE [dbo].[FamousNewsStatic]
GO

USE [interpooldb]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GameSuspect_Game]') AND parent_object_id = OBJECT_ID(N'[dbo].[GamePossibleSuspect]'))
ALTER TABLE [dbo].[GamePossibleSuspect] DROP CONSTRAINT [FK_GameSuspect_Game]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GameSuspect_Suspect]') AND parent_object_id = OBJECT_ID(N'[dbo].[GamePossibleSuspect]'))
ALTER TABLE [dbo].[GamePossibleSuspect] DROP CONSTRAINT [FK_GameSuspect_Suspect]
GO

USE [interpooldb]
GO

/****** Object:  Table [dbo].[GamePossibleSuspect]    Script Date: 09/18/2010 17:08:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GamePossibleSuspect]') AND type in (N'U'))
DROP TABLE [dbo].[GamePossibleSuspect]
GO

USE [interpooldb]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GameSuspect1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Games]'))
ALTER TABLE [dbo].[Games] DROP CONSTRAINT [FK_GameSuspect1]
GO

USE [interpooldb]
GO

/****** Object:  Table [dbo].[Games]    Script Date: 09/18/2010 17:08:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Games]') AND type in (N'U'))
DROP TABLE [dbo].[Games]
GO


USE [interpooldb]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_FamousNew]') AND parent_object_id = OBJECT_ID(N'[dbo].[News]'))
ALTER TABLE [dbo].[News] DROP CONSTRAINT [FK_FamousNew]
GO

USE [interpooldb]
GO

/****** Object:  Table [dbo].[News]    Script Date: 09/18/2010 17:09:08 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[News]') AND type in (N'U'))
DROP TABLE [dbo].[News]
GO

USE [interpooldb]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CityNewsDynamic]') AND parent_object_id = OBJECT_ID(N'[dbo].[NewsDynamics]'))
ALTER TABLE [dbo].[NewsDynamics] DROP CONSTRAINT [FK_CityNewsDynamic]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_FamousNewsDynamic]') AND parent_object_id = OBJECT_ID(N'[dbo].[NewsDynamics]'))
ALTER TABLE [dbo].[NewsDynamics] DROP CONSTRAINT [FK_FamousNewsDynamic]
GO

USE [interpooldb]
GO

/****** Object:  Table [dbo].[NewsDynamics]    Script Date: 09/18/2010 17:09:20 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NewsDynamics]') AND type in (N'U'))
DROP TABLE [dbo].[NewsDynamics]
GO

USE [interpooldb]
GO

/****** Object:  Table [dbo].[NewsStatics]    Script Date: 09/18/2010 17:09:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NewsStatics]') AND type in (N'U'))
DROP TABLE [dbo].[NewsStatics]
GO


USE [interpooldb]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_NodePathClue_Clue]') AND parent_object_id = OBJECT_ID(N'[dbo].[NodePathClue]'))
ALTER TABLE [dbo].[NodePathClue] DROP CONSTRAINT [FK_NodePathClue_Clue]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_NodePathClue_NodePath]') AND parent_object_id = OBJECT_ID(N'[dbo].[NodePathClue]'))
ALTER TABLE [dbo].[NodePathClue] DROP CONSTRAINT [FK_NodePathClue_NodePath]
GO

USE [interpooldb]
GO

/****** Object:  Table [dbo].[NodePathClue]    Script Date: 09/18/2010 17:09:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NodePathClue]') AND type in (N'U'))
DROP TABLE [dbo].[NodePathClue]
GO
USE [interpooldb]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_NodePathFamous_Famous]') AND parent_object_id = OBJECT_ID(N'[dbo].[NodePathFamous]'))
ALTER TABLE [dbo].[NodePathFamous] DROP CONSTRAINT [FK_NodePathFamous_Famous]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_NodePathFamous_NodePath]') AND parent_object_id = OBJECT_ID(N'[dbo].[NodePathFamous]'))
ALTER TABLE [dbo].[NodePathFamous] DROP CONSTRAINT [FK_NodePathFamous_NodePath]
GO

USE [interpooldb]
GO

/****** Object:  Table [dbo].[NodePathFamous]    Script Date: 09/18/2010 17:09:56 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NodePathFamous]') AND type in (N'U'))
DROP TABLE [dbo].[NodePathFamous]
GO
USE [interpooldb]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GameNodePath]') AND parent_object_id = OBJECT_ID(N'[dbo].[NodePaths]'))
ALTER TABLE [dbo].[NodePaths] DROP CONSTRAINT [FK_GameNodePath]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_NodePathCity]') AND parent_object_id = OBJECT_ID(N'[dbo].[NodePaths]'))
ALTER TABLE [dbo].[NodePaths] DROP CONSTRAINT [FK_NodePathCity]
GO

USE [interpooldb]
GO

/****** Object:  Table [dbo].[NodePaths]    Script Date: 09/18/2010 17:10:08 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NodePaths]') AND type in (N'U'))
DROP TABLE [dbo].[NodePaths]
GO


USE [interpooldb]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserGame]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_UserGame]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserLevel]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_UserLevel]
GO

USE [interpooldb]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 09/18/2010 17:10:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
DROP TABLE [dbo].[Users]
GO

USE [interpooldb]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ClueCity]') AND parent_object_id = OBJECT_ID(N'[dbo].[Clues]'))
ALTER TABLE [dbo].[Clues] DROP CONSTRAINT [FK_ClueCity]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ClueFamous]') AND parent_object_id = OBJECT_ID(N'[dbo].[Clues]'))
ALTER TABLE [dbo].[Clues] DROP CONSTRAINT [FK_ClueFamous]
GO

USE [interpooldb]
GO

/****** Object:  Table [dbo].[Clues]    Script Date: 09/18/2010 17:10:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Clues]') AND type in (N'U'))
DROP TABLE [dbo].[Clues]
GO

USE [interpooldb]
GO

/****** Object:  Table [dbo].[Cities]    Script Date: 09/18/2010 17:11:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Cities]') AND type in (N'U'))
DROP TABLE [dbo].[Cities]
GO


USE [interpooldb]
GO

/****** Object:  Table [dbo].[Famous]    Script Date: 09/18/2010 17:11:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Famous]') AND type in (N'U'))
DROP TABLE [dbo].[Famous]
GO


USE [interpooldb]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GameSuspect1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Games]'))
ALTER TABLE [dbo].[Games] DROP CONSTRAINT [FK_GameSuspect1]
GO

USE [interpooldb]
GO

/****** Object:  Table [dbo].[Games]    Script Date: 09/18/2010 17:11:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Games]') AND type in (N'U'))
DROP TABLE [dbo].[Games]
GO
USE [interpooldb]
GO

/****** Object:  Table [dbo].[Levels]    Script Date: 09/18/2010 17:11:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Levels]') AND type in (N'U'))
DROP TABLE [dbo].[Levels]
GO


USE [interpooldb]
GO

/****** Object:  Table [dbo].[Suspects]    Script Date: 09/18/2010 17:11:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Suspects]') AND type in (N'U'))
DROP TABLE [dbo].[Suspects]
GO

USE [interpooldb]
GO

/****** Object:  Table [dbo].[Suspects]    Script Date: 09/18/2010 17:12:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Suspects]') AND type in (N'U'))
DROP TABLE [dbo].[Suspects]
GO




