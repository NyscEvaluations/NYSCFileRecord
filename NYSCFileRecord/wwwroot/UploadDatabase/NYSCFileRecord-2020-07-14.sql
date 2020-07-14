/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 7/14/2020 11:00:15 PM ******/
SET ANSI_NULLS ONSET QUOTED_IDENTIFIER ONCREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ProductVersion] [nvarchar](32) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 7/14/2020 11:00:25 PM ******/
SET ANSI_NULLS ONSET QUOTED_IDENTIFIER ONCREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Name] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NormalizedName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ConcurrencyStamp] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 7/14/2020 11:00:25 PM ******/
SET ANSI_NULLS ONSET QUOTED_IDENTIFIER ONCREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ClaimType] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ClaimValue] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 7/14/2020 11:00:28 PM ******/
SET ANSI_NULLS ONSET QUOTED_IDENTIFIER ONCREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Name] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NormalizedName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ConcurrencyStamp] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 7/14/2020 11:00:35 PM ******/
SET ANSI_NULLS ONSET QUOTED_IDENTIFIER ONCREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UserName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NormalizedUserName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Email] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NormalizedEmail] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SecurityStamp] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ConcurrencyStamp] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PhoneNumber] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[City] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[FirstName] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsActive] [bit] NOT NULL,
	[LastName] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Picture] [varbinary](max) NULL,
	[PostalCode] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[State] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[StreetAddress] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Biography] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Course] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Institution] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Level] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Position] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Qualification] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 7/14/2020 11:00:35 PM ******/
SET ANSI_NULLS ONSET QUOTED_IDENTIFIER ONCREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ClaimType] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ClaimValue] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 7/14/2020 11:00:42 PM ******/
SET ANSI_NULLS ONSET QUOTED_IDENTIFIER ONCREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UserName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NormalizedUserName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Email] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NormalizedEmail] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SecurityStamp] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ConcurrencyStamp] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PhoneNumber] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[City] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[FirstName] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsActive] [bit] NOT NULL,
	[LastName] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Picture] [varbinary](max) NULL,
	[PostalCode] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[State] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[StreetAddress] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Biography] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Course] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Institution] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Level] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Position] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Qualification] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 7/14/2020 11:00:42 PM ******/
SET ANSI_NULLS ONSET QUOTED_IDENTIFIER ONCREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ProviderKey] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ProviderDisplayName] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UserId] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 7/14/2020 11:00:48 PM ******/
SET ANSI_NULLS ONSET QUOTED_IDENTIFIER ONCREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Name] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NormalizedName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ConcurrencyStamp] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 7/14/2020 11:00:48 PM ******/
SET ANSI_NULLS ONSET QUOTED_IDENTIFIER ONCREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UserName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NormalizedUserName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Email] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NormalizedEmail] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SecurityStamp] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ConcurrencyStamp] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PhoneNumber] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[City] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[FirstName] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsActive] [bit] NOT NULL,
	[LastName] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Picture] [varbinary](max) NULL,
	[PostalCode] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[State] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[StreetAddress] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Biography] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Course] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Institution] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Level] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Position] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Qualification] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 7/14/2020 11:00:48 PM ******/
SET ANSI_NULLS ONSET QUOTED_IDENTIFIER ONCREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[RoleId] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 7/14/2020 11:00:51 PM ******/
SET ANSI_NULLS ONSET QUOTED_IDENTIFIER ONCREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UserName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NormalizedUserName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Email] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NormalizedEmail] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SecurityStamp] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ConcurrencyStamp] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PhoneNumber] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[City] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[FirstName] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsActive] [bit] NOT NULL,
	[LastName] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Picture] [varbinary](max) NULL,
	[PostalCode] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[State] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[StreetAddress] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Biography] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Course] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Institution] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Level] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Position] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Qualification] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 7/14/2020 11:00:57 PM ******/
SET ANSI_NULLS ONSET QUOTED_IDENTIFIER ONCREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UserName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NormalizedUserName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Email] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NormalizedEmail] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SecurityStamp] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ConcurrencyStamp] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PhoneNumber] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[City] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[FirstName] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsActive] [bit] NOT NULL,
	[LastName] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Picture] [varbinary](max) NULL,
	[PostalCode] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[State] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[StreetAddress] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Biography] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Course] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Institution] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Level] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Position] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Qualification] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 7/14/2020 11:00:57 PM ******/
SET ANSI_NULLS ONSET QUOTED_IDENTIFIER ONCREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[LoginProvider] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Name] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Value] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
/****** Object:  Table [dbo].[FileRecord]    Script Date: 7/14/2020 11:00:59 PM ******/
SET ANSI_NULLS ONSET QUOTED_IDENTIFIER ONCREATE TABLE [dbo].[FileRecord](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CodeNumber] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Name] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Description] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PhoneNumber] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TakenTo] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ReturnedFrom] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CollectingOfficer] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RecordingOfficer] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsActive] [bit] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[CurrentLocation] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateReturned] [datetime2](7) NOT NULL,
	[shelfNumber] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
