USE [master]
GO
/****** Object:  Database [PollosHermanoCoreBancarioDB]    Script Date: 11/11/2021 08:09:22 p. m. ******/
CREATE DATABASE [PollosHermanoCoreBancarioDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PollosHermanoCoreBancarioDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PollosHermanoCoreBancarioDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PollosHermanoCoreBancarioDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PollosHermanoCoreBancarioDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PollosHermanoCoreBancarioDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PollosHermanoCoreBancarioDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PollosHermanoCoreBancarioDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PollosHermanoCoreBancarioDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PollosHermanoCoreBancarioDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PollosHermanoCoreBancarioDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PollosHermanoCoreBancarioDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [PollosHermanoCoreBancarioDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [PollosHermanoCoreBancarioDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PollosHermanoCoreBancarioDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PollosHermanoCoreBancarioDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PollosHermanoCoreBancarioDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PollosHermanoCoreBancarioDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PollosHermanoCoreBancarioDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PollosHermanoCoreBancarioDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PollosHermanoCoreBancarioDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PollosHermanoCoreBancarioDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [PollosHermanoCoreBancarioDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PollosHermanoCoreBancarioDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PollosHermanoCoreBancarioDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PollosHermanoCoreBancarioDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PollosHermanoCoreBancarioDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PollosHermanoCoreBancarioDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PollosHermanoCoreBancarioDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PollosHermanoCoreBancarioDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PollosHermanoCoreBancarioDB] SET  MULTI_USER 
GO
ALTER DATABASE [PollosHermanoCoreBancarioDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PollosHermanoCoreBancarioDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PollosHermanoCoreBancarioDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PollosHermanoCoreBancarioDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PollosHermanoCoreBancarioDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PollosHermanoCoreBancarioDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [PollosHermanoCoreBancarioDB] SET QUERY_STORE = OFF
GO
USE [PollosHermanoCoreBancarioDB]
GO
/****** Object:  Schema [identity]    Script Date: 11/11/2021 08:09:23 p. m. ******/
CREATE SCHEMA [identity]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 11/11/2021 08:09:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CatTipoCuenta]    Script Date: 11/11/2021 08:09:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CatTipoCuenta](
	[Id] [tinyint] NOT NULL,
	[Descripcion] [nvarchar](25) NOT NULL,
 CONSTRAINT [PK_CatTipoCuenta] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 11/11/2021 08:09:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ApellidoPaterno] [nvarchar](25) NOT NULL,
	[Nombre] [nvarchar](25) NOT NULL,
	[ApellidoMaterno] [nvarchar](25) NULL,
	[FechaNacimiento] [date] NOT NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contrato]    Script Date: 11/11/2021 08:09:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contrato](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdPreContrato] [int] NOT NULL,
 CONSTRAINT [PK_Contrato] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cuenta]    Script Date: 11/11/2021 08:09:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cuenta](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdContrato] [int] NOT NULL,
	[IdCliente] [int] NOT NULL,
	[IdTipoCuenta] [tinyint] NOT NULL,
	[NumeroCuenta] [nvarchar](13) NOT NULL,
 CONSTRAINT [PK_Cuenta] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PreContrato]    Script Date: 11/11/2021 08:09:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PreContrato](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NombreProspecto] [nvarchar](25) NOT NULL,
	[FechaNacimientoProspecto] [date] NOT NULL,
	[ApellidoMaternoProspecto] [nvarchar](25) NULL,
	[IdVendedor] [int] NOT NULL,
	[ApellidoPaternoProspecto] [nvarchar](25) NOT NULL,
	[Numero] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_PreContrato] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sucursal]    Script Date: 11/11/2021 08:09:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sucursal](
	[Id] [tinyint] NOT NULL,
	[Nombre] [nvarchar](25) NOT NULL,
	[Direccion] [nvarchar](250) NULL,
 CONSTRAINT [PK_Sucursal] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vendedor]    Script Date: 11/11/2021 08:09:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vendedor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ApellidoPaterno] [nvarchar](25) NOT NULL,
	[Nombre] [nvarchar](25) NOT NULL,
	[IdZona] [int] NOT NULL,
	[ApellidoMaterno] [nvarchar](25) NULL,
 CONSTRAINT [PK_Vendedor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Zona]    Script Date: 11/11/2021 08:09:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Zona](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdSucursal] [tinyint] NOT NULL,
	[Nombre] [nvarchar](25) NOT NULL,
	[Estatus] [bit] NOT NULL,
 CONSTRAINT [PK_Zona] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [identity].[Role]    Script Date: 11/11/2021 08:09:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [identity].[Role](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [identity].[RoleClaim]    Script Date: 11/11/2021 08:09:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [identity].[RoleClaim](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_RoleClaim] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [identity].[User]    Script Date: 11/11/2021 08:09:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [identity].[User](
	[Id] [uniqueidentifier] NOT NULL,
	[Firstname] [nvarchar](max) NOT NULL,
	[FatherLastname] [nvarchar](max) NOT NULL,
	[MotherLastname] [nvarchar](max) NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [identity].[UserClaim]    Script Date: 11/11/2021 08:09:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [identity].[UserClaim](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserClaim] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [identity].[UserLogin]    Script Date: 11/11/2021 08:09:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [identity].[UserLogin](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserLogin] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [identity].[UserRole]    Script Date: 11/11/2021 08:09:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [identity].[UserRole](
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [identity].[UserToken]    Script Date: 11/11/2021 08:09:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [identity].[UserToken](
	[UserId] [uniqueidentifier] NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserToken] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_Contrato_IdPreContrato]    Script Date: 11/11/2021 08:09:23 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_Contrato_IdPreContrato] ON [dbo].[Contrato]
(
	[IdPreContrato] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Cuenta_IdCliente]    Script Date: 11/11/2021 08:09:23 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_Cuenta_IdCliente] ON [dbo].[Cuenta]
(
	[IdCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Cuenta_IdContrato]    Script Date: 11/11/2021 08:09:23 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_Cuenta_IdContrato] ON [dbo].[Cuenta]
(
	[IdContrato] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Cuenta_IdTipoCuenta]    Script Date: 11/11/2021 08:09:23 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_Cuenta_IdTipoCuenta] ON [dbo].[Cuenta]
(
	[IdTipoCuenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PreContrato_IdVendedor]    Script Date: 11/11/2021 08:09:23 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_PreContrato_IdVendedor] ON [dbo].[PreContrato]
(
	[IdVendedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Vendedor_IdZona]    Script Date: 11/11/2021 08:09:23 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_Vendedor_IdZona] ON [dbo].[Vendedor]
(
	[IdZona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Zona_IdSucursal]    Script Date: 11/11/2021 08:09:23 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_Zona_IdSucursal] ON [dbo].[Zona]
(
	[IdSucursal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 11/11/2021 08:09:23 p. m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [identity].[Role]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RoleClaim_RoleId]    Script Date: 11/11/2021 08:09:23 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_RoleClaim_RoleId] ON [identity].[RoleClaim]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 11/11/2021 08:09:23 p. m. ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [identity].[User]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 11/11/2021 08:09:23 p. m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [identity].[User]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserClaim_UserId]    Script Date: 11/11/2021 08:09:23 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_UserClaim_UserId] ON [identity].[UserClaim]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserLogin_UserId]    Script Date: 11/11/2021 08:09:23 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_UserLogin_UserId] ON [identity].[UserLogin]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserRole_RoleId]    Script Date: 11/11/2021 08:09:23 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_UserRole_RoleId] ON [identity].[UserRole]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PreContrato] ADD  DEFAULT (N'') FOR [Numero]
GO
ALTER TABLE [dbo].[Contrato]  WITH CHECK ADD  CONSTRAINT [FK_Contrato_PreContrato_IdPreContrato] FOREIGN KEY([IdPreContrato])
REFERENCES [dbo].[PreContrato] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Contrato] CHECK CONSTRAINT [FK_Contrato_PreContrato_IdPreContrato]
GO
ALTER TABLE [dbo].[Cuenta]  WITH CHECK ADD  CONSTRAINT [FK_Cuenta_CatTipoCuenta_IdTipoCuenta] FOREIGN KEY([IdTipoCuenta])
REFERENCES [dbo].[CatTipoCuenta] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Cuenta] CHECK CONSTRAINT [FK_Cuenta_CatTipoCuenta_IdTipoCuenta]
GO
ALTER TABLE [dbo].[Cuenta]  WITH CHECK ADD  CONSTRAINT [FK_Cuenta_Cliente_IdCliente] FOREIGN KEY([IdCliente])
REFERENCES [dbo].[Cliente] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Cuenta] CHECK CONSTRAINT [FK_Cuenta_Cliente_IdCliente]
GO
ALTER TABLE [dbo].[Cuenta]  WITH CHECK ADD  CONSTRAINT [FK_Cuenta_Contrato_IdContrato] FOREIGN KEY([IdContrato])
REFERENCES [dbo].[Contrato] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Cuenta] CHECK CONSTRAINT [FK_Cuenta_Contrato_IdContrato]
GO
ALTER TABLE [dbo].[PreContrato]  WITH CHECK ADD  CONSTRAINT [FK_PreContrato_Vendedor_IdVendedor] FOREIGN KEY([IdVendedor])
REFERENCES [dbo].[Vendedor] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PreContrato] CHECK CONSTRAINT [FK_PreContrato_Vendedor_IdVendedor]
GO
ALTER TABLE [dbo].[Vendedor]  WITH CHECK ADD  CONSTRAINT [FK_Vendedor_Zona_IdZona] FOREIGN KEY([IdZona])
REFERENCES [dbo].[Zona] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Vendedor] CHECK CONSTRAINT [FK_Vendedor_Zona_IdZona]
GO
ALTER TABLE [dbo].[Zona]  WITH CHECK ADD  CONSTRAINT [FK_Zona_Sucursal_IdSucursal] FOREIGN KEY([IdSucursal])
REFERENCES [dbo].[Sucursal] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Zona] CHECK CONSTRAINT [FK_Zona_Sucursal_IdSucursal]
GO
ALTER TABLE [identity].[RoleClaim]  WITH CHECK ADD  CONSTRAINT [FK_RoleClaim_Role_RoleId] FOREIGN KEY([RoleId])
REFERENCES [identity].[Role] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [identity].[RoleClaim] CHECK CONSTRAINT [FK_RoleClaim_Role_RoleId]
GO
ALTER TABLE [identity].[UserClaim]  WITH CHECK ADD  CONSTRAINT [FK_UserClaim_User_UserId] FOREIGN KEY([UserId])
REFERENCES [identity].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [identity].[UserClaim] CHECK CONSTRAINT [FK_UserClaim_User_UserId]
GO
ALTER TABLE [identity].[UserLogin]  WITH CHECK ADD  CONSTRAINT [FK_UserLogin_User_UserId] FOREIGN KEY([UserId])
REFERENCES [identity].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [identity].[UserLogin] CHECK CONSTRAINT [FK_UserLogin_User_UserId]
GO
ALTER TABLE [identity].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_Role_RoleId] FOREIGN KEY([RoleId])
REFERENCES [identity].[Role] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [identity].[UserRole] CHECK CONSTRAINT [FK_UserRole_Role_RoleId]
GO
ALTER TABLE [identity].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_User_UserId] FOREIGN KEY([UserId])
REFERENCES [identity].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [identity].[UserRole] CHECK CONSTRAINT [FK_UserRole_User_UserId]
GO
ALTER TABLE [identity].[UserToken]  WITH CHECK ADD  CONSTRAINT [FK_UserToken_User_UserId] FOREIGN KEY([UserId])
REFERENCES [identity].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [identity].[UserToken] CHECK CONSTRAINT [FK_UserToken_User_UserId]
GO
/****** Object:  StoredProcedure [dbo].[cspGetCatTipoCuentaList]    Script Date: 11/11/2021 08:09:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[cspGetCatTipoCuentaList]
AS
BEGIN

		SET NOCOUNT ON;

		SELECT 
			T1.[Descripcion]
			, T1.[Id]

		FROM 
			[dbo].[CatTipoCuenta] AS T1 WITH(NOLOCK)


END
GO
/****** Object:  StoredProcedure [dbo].[cspGetClienteList]    Script Date: 11/11/2021 08:09:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[cspGetClienteList]
AS
BEGIN

		SET NOCOUNT ON;

		SELECT 
			T1.[ApellidoPaterno]
			, T1.[Id]
			, T1.[Nombre]
			, T1.[ApellidoMaterno]
			, T1.[FechaNacimiento]

		FROM 
			[dbo].[Cliente] AS T1 WITH(NOLOCK)


END
GO
/****** Object:  StoredProcedure [dbo].[cspGetContratoList]    Script Date: 11/11/2021 08:09:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[cspGetContratoList]
AS
BEGIN

		SET NOCOUNT ON;

		SELECT 
			T2.[Numero] AS IdPreContrato
			, T1.[Id]

		FROM 
			[dbo].[Contrato] AS T1 WITH(NOLOCK)
			INNER JOIN [dbo].[PreContrato] AS T2 WITH(NOLOCK) ON T2.Id=T1.IdPreContrato


END
GO
/****** Object:  StoredProcedure [dbo].[cspGetCuentaList]    Script Date: 11/11/2021 08:09:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[cspGetCuentaList]
AS
BEGIN

		SET NOCOUNT ON;

		SELECT 
			T2.[Id] AS IdContrato
			, T3.[Nombre] AS IdCliente
			, T4.[Descripcion] AS IdTipoCuenta
			, T1.[NumeroCuenta]
			, T1.[Id]

		FROM 
			[dbo].[Cuenta] AS T1 WITH(NOLOCK)
			INNER JOIN [dbo].[Contrato] AS T2 WITH(NOLOCK) ON T2.Id=T1.IdContrato
			INNER JOIN [dbo].[Cliente] AS T3 WITH(NOLOCK) ON T3.Id=T1.IdCliente
			INNER JOIN [dbo].[CatTipoCuenta] AS T4 WITH(NOLOCK) ON T4.Id=T1.IdTipoCuenta


END
GO
/****** Object:  StoredProcedure [dbo].[cspGetPreContratoList]    Script Date: 11/11/2021 08:09:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[cspGetPreContratoList]
AS
BEGIN

		SET NOCOUNT ON;

		SELECT 
			T1.[Id]
			, T1.[NombreProspecto]
			, T1.[FechaNacimientoProspecto]
			, T1.[ApellidoMaternoProspecto]
			, T2.[Nombre] AS IdVendedor
			, T1.[ApellidoPaternoProspecto]
			, T1.[Numero]

		FROM 
			[dbo].[PreContrato] AS T1 WITH(NOLOCK)
			INNER JOIN [dbo].[Vendedor] AS T2 WITH(NOLOCK) ON T2.Id=T1.IdVendedor


END
GO
/****** Object:  StoredProcedure [dbo].[cspGetSucursalList]    Script Date: 11/11/2021 08:09:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[cspGetSucursalList]
AS
BEGIN

		SET NOCOUNT ON;

		SELECT 
			T1.[Nombre]
			, T1.[Direccion]
			, T1.[Id]

		FROM 
			[dbo].[Sucursal] AS T1 WITH(NOLOCK)


END
GO
/****** Object:  StoredProcedure [dbo].[cspGetVendedorList]    Script Date: 11/11/2021 08:09:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[cspGetVendedorList]
AS
BEGIN

		SET NOCOUNT ON;

		SELECT 
			T1.[ApellidoPaterno]
			, T1.[Nombre]
			, T1.[Id]
			, T2.[Nombre] AS IdZona
			, T1.[ApellidoMaterno]

		FROM 
			[dbo].[Vendedor] AS T1 WITH(NOLOCK)
			INNER JOIN [dbo].[Zona] AS T2 WITH(NOLOCK) ON T2.Id=T1.IdZona


END
GO
/****** Object:  StoredProcedure [dbo].[cspGetZonaList]    Script Date: 11/11/2021 08:09:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[cspGetZonaList]
AS
BEGIN

		SET NOCOUNT ON;

		SELECT 
			T1.[Id]
			, T2.[Nombre] AS IdSucursal
			, T1.[Nombre]
			, T1.[Estatus]

		FROM 
			[dbo].[Zona] AS T1 WITH(NOLOCK)
			INNER JOIN [dbo].[Sucursal] AS T2 WITH(NOLOCK) ON T2.Id=T1.IdSucursal


END
GO
USE [master]
GO
ALTER DATABASE [PollosHermanoCoreBancarioDB] SET  READ_WRITE 
GO
