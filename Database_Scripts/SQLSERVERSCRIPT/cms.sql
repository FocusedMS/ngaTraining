USE [cms];
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

/* Customer */
CREATE TABLE [dbo].[Customer](
	[custId]       INT          NOT NULL,
	[custName]     VARCHAR(30)  NULL,
	[custUserName] VARCHAR(30)  NULL,
	[custPassword] VARCHAR(30)  NULL,   -- note: too short for real hashes; widen later
	[city]         VARCHAR(30)  NULL,
	[state]        VARCHAR(30)  NULL,
	[email]        VARCHAR(30)  NULL,
	[mobileNo]     VARCHAR(20)  NULL,
	CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([custId] ASC)
);
GO

/* Menu */
CREATE TABLE [dbo].[Menu](
	[menuId]     INT IDENTITY(1,1) NOT NULL,
	[ItemName]   VARCHAR(30) NULL,
	[ItemType]   VARCHAR(20) NULL,
	[Price]      NUMERIC(9,2) NULL,
	[Description] VARCHAR(30) NULL,
	[rating]     VARCHAR(10) NULL,
	CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED ([menuId] ASC)
);
GO

/* Orders */
CREATE TABLE [dbo].[Orders](
	[OrderId]       INT IDENTITY(1,1) NOT NULL,
	[custId]        INT NULL,
	[MenuId]        INT NULL,
	[VendorId]      INT NULL,
	[QtyOrd]        INT NULL,
	[BillAmount]    NUMERIC(9,2) NULL,
	[OrderStatus]   VARCHAR(30) NULL,
	[OrderComments] VARCHAR(30) NULL,
	CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED ([OrderId] ASC)
);
GO

/* Vendor */
CREATE TABLE [dbo].[Vendor](
	[VendorId]       INT IDENTITY(1,1) NOT NULL,
	[VendorName]     VARCHAR(30) NULL,
	[VendorUserName] VARCHAR(30) NULL,
	[VendorPassword] VARCHAR(30) NULL,
	[VendorEmail]    VARCHAR(30) NULL,
	[VendorMobile]   VARCHAR(30) NULL,
	CONSTRAINT [PK_Vendor] PRIMARY KEY CLUSTERED ([VendorId] ASC)
);
GO

/* Wallet */
CREATE TABLE [dbo].[Wallet](
	[walletId]     INT IDENTITY(1,1) NOT NULL,
	[custId]       INT NULL,
	[walletType]   VARCHAR(30) NULL,
	[walletAmount] NUMERIC(9,2) NULL,
	CONSTRAINT [PK_Wallet] PRIMARY KEY CLUSTERED ([walletId] ASC)
);
GO

/* Unique username on Vendor */
CREATE UNIQUE NONCLUSTERED INDEX [UQ_VendorUserName] 
ON [dbo].[Vendor]([VendorUserName] ASC);
GO

/* Default + FKs */
ALTER TABLE [dbo].[Orders] ADD  DEFAULT ('PENDING') FOR [OrderStatus];
GO

ALTER TABLE [dbo].[Orders]  WITH CHECK 
ADD CONSTRAINT [FK_Orders_Customer] FOREIGN KEY([custId]) REFERENCES [dbo].[Customer] ([custId]);
GO

ALTER TABLE [dbo].[Orders]  WITH CHECK 
ADD CONSTRAINT [FK_Orders_Menu] FOREIGN KEY([MenuId]) REFERENCES [dbo].[Menu] ([menuId]);
GO

ALTER TABLE [dbo].[Orders]  WITH CHECK 
ADD CONSTRAINT [FK_Orders_Vendor] FOREIGN KEY([VendorId]) REFERENCES [dbo].[Vendor] ([VendorId]);
GO


USE cms;
GO
ALTER TABLE dbo.Customer
ALTER COLUMN custPassword VARCHAR(200) NULL;  -- room for full hash


