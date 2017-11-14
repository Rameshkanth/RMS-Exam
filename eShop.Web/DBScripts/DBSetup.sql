USE [eShopDb]
GO

CREATE TABLE [dbo].[Products](
	[Id] [uniqueidentifier] NOT NULL,
	[ProcuctName] [nvarchar](100) NOT NULL,
	[ShortDescription] [nvarchar](200) NULL,
	[Price] [smallmoney] NOT NULL,
 CONSTRAINT [PK__Products__3214EC0742F7DC35] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[FirstName ] [nvarchar](100) NOT NULL,
	[LastName ] [nvarchar](100) NOT NULL,
	[AddressLine1 ] [nvarchar](200) NOT NULL,
	[AddressLine2] [nvarchar](200) NULL,
	[City] [nvarchar](50) NOT NULL,
	[Postcode] [nvarchar](10) NOT NULL,
	[Telephone] [nvarchar](15) NULL,
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

INSERT [dbo].[Products] ([Id], [ProcuctName], [ShortDescription], [Price]) VALUES (N'432f8e2e-57dc-421a-a6dd-3a2deec95c78', N'Product 6', N'Product 6 Description', 60.0000)
GO
INSERT [dbo].[Products] ([Id], [ProcuctName], [ShortDescription], [Price]) VALUES (N'6d3afa66-5d02-451f-901b-3d46e4b34048', N'Product 2', N'Product 2 Description', 200.0000)
GO
INSERT [dbo].[Products] ([Id], [ProcuctName], [ShortDescription], [Price]) VALUES (N'70d28880-6967-4c06-9220-53c8cdf96ad9', N'Product 3', N'Product 3 Description', 250.0000)
GO
INSERT [dbo].[Products] ([Id], [ProcuctName], [ShortDescription], [Price]) VALUES (N'7d29402f-129b-4f35-b392-6060737e0000', N'Product 5', N'Product 5 Description', 540.0000)
GO
INSERT [dbo].[Products] ([Id], [ProcuctName], [ShortDescription], [Price]) VALUES (N'5c9fd43e-8001-421f-9542-6590fc9044ef', N'Product 4', N'Product 4 Description', 240.0000)
GO
INSERT [dbo].[Products] ([Id], [ProcuctName], [ShortDescription], [Price]) VALUES (N'3b6e5020-06f1-491a-8410-d155e9790c55', N'Product 1', N'Product 1 Description', 100.0000)
GO
