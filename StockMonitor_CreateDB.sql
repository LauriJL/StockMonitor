/****** Object:  Table [dbo].[Currency]    Script Date: 7.12.2020 9.33.00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Currency](
	[ID] [int] NOT NULL,
	[Currency] [nvarchar](5) NOT NULL,
 CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED 
(
	[Currency] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Portfolio]    Script Date: 7.12.2020 9.33.00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Portfolio](
	[ID] [int] IDENTITY(2000,1) NOT NULL,
	[Kayttaja] [nvarchar](20) NULL,
	[Yritys] [nvarchar](20) NULL,
	[MaaraYht] [int] NULL,
	[HankintaArvo] [decimal](19, 2) NULL,
 CONSTRAINT [PK__Portfoli__3214EC27D9FA6BC6] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 7.12.2020 9.33.00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[Kayttaja] [nvarchar](20) NOT NULL,
	[OstoMyynti] [nvarchar](10) NOT NULL,
	[Pvm] [date] NOT NULL,
	[Yritys] [nvarchar](20) NOT NULL,
	[Maara] [int] NOT NULL,
	[MaaraForPortfolio]  AS (case when [OstoMyynti]=N'Myynti' then [Maara]*(-1) else [Maara] end),
	[aHinta] [money] NOT NULL,
	[Total]  AS (case when [OstoMyynti]=N'Myynti' then ([Maara]*[aHinta])*(-1) else [Maara]*[aHinta] end),
	[TotalForPortfolio]  AS (case when [OstoMyynti]=N'Myynti' then ([Maara]*[aHinta])*(-1) else [Maara]*[aHinta] end),
	[Valuutta] [nvarchar](5) NOT NULL,
	[Kurssi] [money] NULL,
	[TotalEuros]  AS (case when [OstoMyynti]=N'Myynti' AND [Kurssi] IS NOT NULL then (([Maara]*[aHinta])/[Kurssi])*(-1) when [OstoMyynti]=N'Osto' AND [Kurssi] IS NOT NULL then ([Maara]*[aHinta])/[Kurssi] when [OstoMyynti]=N'Myynti' AND [Kurssi] IS NULL then ([Maara]*[aHinta])*(-1) else [Maara]*[aHinta] end),
	[Kulut] [money] NOT NULL,
	[Grandtotal]  AS (case when [Kurssi] IS NOT NULL AND [OstoMyynti]=N'Osto' then ([Maara]*[aHinta])/[Kurssi]+[Kulut] when [Kurssi] IS NOT NULL AND [OstoMyynti]=N'Myynti' then (([Maara]*[aHinta])/[Kurssi]-[Kulut])*(-1) when [Kurssi] IS NULL AND [OstoMyynti]=N'Osto' then [Maara]*[aHinta]+[Kulut] when [Kurssi] IS NULL AND [OstoMyynti]=N'Myynti' then ([Maara]*[aHinta]-[Kulut])*(-1)  end),
 CONSTRAINT [PK__Transact__3214EC27926AF230] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransactionTypes]    Script Date: 7.12.2020 9.33.00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionTypes](
	[ID] [int] NOT NULL,
	[Type] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_TransactionTypes] PRIMARY KEY CLUSTERED 
(
	[Type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 7.12.2020 9.33.00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[Role] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[Role] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 7.12.2020 9.33.00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[KayttajaNimi] [nvarchar](20) NOT NULL,
	[Rooli] [nvarchar](20) NOT NULL,
	[Etunimi] [nvarchar](10) NOT NULL,
	[Sukunimi] [nvarchar](30) NULL,
	[Salasana] [nvarchar](10) NOT NULL,
	[Sahkoposti] [nvarchar](30) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[KayttajaNimi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


INSERT [dbo].[Currency] ([ID], [Currency]) VALUES (2, N'EUR')
GO
INSERT [dbo].[Currency] ([ID], [Currency]) VALUES (1, N'USD')
GO
SET IDENTITY_INSERT [dbo].[Portfolio] ON 
GO
INSERT [dbo].[Portfolio] ([ID], [Kayttaja], [Yritys], [MaaraYht], [HankintaArvo]) VALUES (3820, N'Pekka', N'AAPL', 40, CAST(3971.63 AS Decimal(19, 2)))
GO
INSERT [dbo].[Portfolio] ([ID], [Kayttaja], [Yritys], [MaaraYht], [HankintaArvo]) VALUES (3821, N'Pekka', N'MSFT', 50, CAST(7757.09 AS Decimal(19, 2)))
GO
INSERT [dbo].[Portfolio] ([ID], [Kayttaja], [Yritys], [MaaraYht], [HankintaArvo]) VALUES (3822, N'Pekka', N'NOKIA.HE', 50, CAST(125.00 AS Decimal(19, 2)))
GO
INSERT [dbo].[Portfolio] ([ID], [Kayttaja], [Yritys], [MaaraYht], [HankintaArvo]) VALUES (3826, N'Matti', N'NOKIA.HE', 150, CAST(490.00 AS Decimal(19, 2)))
GO
INSERT [dbo].[Portfolio] ([ID], [Kayttaja], [Yritys], [MaaraYht], [HankintaArvo]) VALUES (3827, N'Matti', N'PLUG', 50, CAST(1063.83 AS Decimal(19, 2)))
GO
INSERT [dbo].[Portfolio] ([ID], [Kayttaja], [Yritys], [MaaraYht], [HankintaArvo]) VALUES (3828, N'Matti', N'MSFT', 25, CAST(4454.79 AS Decimal(19, 2)))
GO
INSERT [dbo].[Portfolio] ([ID], [Kayttaja], [Yritys], [MaaraYht], [HankintaArvo]) VALUES (3829, N'SuperUser', N'NOKIA.HE', 150, CAST(510.00 AS Decimal(19, 2)))
GO
INSERT [dbo].[Portfolio] ([ID], [Kayttaja], [Yritys], [MaaraYht], [HankintaArvo]) VALUES (3830, N'SuperUser', N'PLUG', 25, CAST(578.46 AS Decimal(19, 2)))
GO
INSERT [dbo].[Portfolio] ([ID], [Kayttaja], [Yritys], [MaaraYht], [HankintaArvo]) VALUES (3831, N'SuperUser', N'AAPL', 50, CAST(5176.50 AS Decimal(19, 2)))
GO
SET IDENTITY_INSERT [dbo].[Portfolio] OFF
GO
SET IDENTITY_INSERT [dbo].[Transactions] ON 
GO
INSERT [dbo].[Transactions] ([ID], [Kayttaja], [OstoMyynti], [Pvm], [Yritys], [Maara], [aHinta], [Valuutta], [Kurssi], [Kulut]) VALUES (2289, N'Pekka', N'Osto', CAST(N'2020-11-03' AS Date), N'MSFT', 50, 175.0000, N'USD', 1.1280, 8.0000)
GO
INSERT [dbo].[Transactions] ([ID], [Kayttaja], [OstoMyynti], [Pvm], [Yritys], [Maara], [aHinta], [Valuutta], [Kurssi], [Kulut]) VALUES (2290, N'Pekka', N'Osto', CAST(N'2020-10-29' AS Date), N'NOKIA.HE', 50, 2.5000, N'EUR', NULL, 2.5500)
GO
INSERT [dbo].[Transactions] ([ID], [Kayttaja], [OstoMyynti], [Pvm], [Yritys], [Maara], [aHinta], [Valuutta], [Kurssi], [Kulut]) VALUES (2291, N'Pekka', N'Osto', CAST(N'2020-10-28' AS Date), N'AAPL', 50, 109.8500, N'EUR', 1.1280, 6.5000)
GO
INSERT [dbo].[Transactions] ([ID], [Kayttaja], [OstoMyynti], [Pvm], [Yritys], [Maara], [aHinta], [Valuutta], [Kurssi], [Kulut]) VALUES (2295, N'Matti', N'Osto', CAST(N'2020-10-28' AS Date), N'NOKIA.HE', 100, 3.2500, N'EUR', NULL, 2.5500)
GO
INSERT [dbo].[Transactions] ([ID], [Kayttaja], [OstoMyynti], [Pvm], [Yritys], [Maara], [aHinta], [Valuutta], [Kurssi], [Kulut]) VALUES (2296, N'Matti', N'Osto', CAST(N'2020-10-27' AS Date), N'NOKIA.HE', 50, 3.3000, N'EUR', NULL, 2.5500)
GO
INSERT [dbo].[Transactions] ([ID], [Kayttaja], [OstoMyynti], [Pvm], [Yritys], [Maara], [aHinta], [Valuutta], [Kurssi], [Kulut]) VALUES (2298, N'Matti', N'Osto', CAST(N'2020-10-28' AS Date), N'PLUG', 50, 24.0000, N'USD', 1.1280, 2.5500)
GO
INSERT [dbo].[Transactions] ([ID], [Kayttaja], [OstoMyynti], [Pvm], [Yritys], [Maara], [aHinta], [Valuutta], [Kurssi], [Kulut]) VALUES (2299, N'Matti', N'Osto', CAST(N'2020-10-27' AS Date), N'MSFT', 25, 201.0000, N'EUR', 1.1280, 8.0000)
GO
INSERT [dbo].[Transactions] ([ID], [Kayttaja], [OstoMyynti], [Pvm], [Yritys], [Maara], [aHinta], [Valuutta], [Kurssi], [Kulut]) VALUES (2301, N'SuperUser', N'Osto', CAST(N'2020-10-28' AS Date), N'NOKIA.HE', 150, 3.4000, N'EUR', NULL, 6.5000)
GO
INSERT [dbo].[Transactions] ([ID], [Kayttaja], [OstoMyynti], [Pvm], [Yritys], [Maara], [aHinta], [Valuutta], [Kurssi], [Kulut]) VALUES (2303, N'SuperUser', N'Osto', CAST(N'2020-12-04' AS Date), N'PLUG', 25, 26.1000, N'USD', 1.1280, 6.5000)
GO
INSERT [dbo].[Transactions] ([ID], [Kayttaja], [OstoMyynti], [Pvm], [Yritys], [Maara], [aHinta], [Valuutta], [Kurssi], [Kulut]) VALUES (2304, N'SuperUser', N'Osto', CAST(N'2020-12-05' AS Date), N'AAPL', 50, 115.8500, N'USD', 1.1190, 8.0000)
GO
INSERT [dbo].[Transactions] ([ID], [Kayttaja], [OstoMyynti], [Pvm], [Yritys], [Maara], [aHinta], [Valuutta], [Kurssi], [Kulut]) VALUES (2307, N'Pekka', N'Myynti', CAST(N'2020-12-01' AS Date), N'AAPL', 10, 101.2500, N'USD', 1.1280, 8.0000)
GO
SET IDENTITY_INSERT [dbo].[Transactions] OFF
GO
INSERT [dbo].[TransactionTypes] ([ID], [Type]) VALUES (2, N'Myynti')
GO
INSERT [dbo].[TransactionTypes] ([ID], [Type]) VALUES (1, N'Osto')
GO
INSERT [dbo].[UserRoles] ([Role]) VALUES (N'Käyttäjä')
GO
INSERT [dbo].[UserRoles] ([Role]) VALUES (N'Pääkäyttäjä')
GO
INSERT [dbo].[Users] ([KayttajaNimi], [Rooli], [Etunimi], [Sukunimi], [Salasana], [Sahkoposti]) VALUES (N'Lauri', N'Pääkäyttäjä', N'Lauri', N'Leskinen', N'user1', N'lauri@lauri.com')
GO
INSERT [dbo].[Users] ([KayttajaNimi], [Rooli], [Etunimi], [Sukunimi], [Salasana], [Sahkoposti]) VALUES (N'Matti', N'Käyttäjä', N'Matti', N'Meikäläinen', N'matti555', N'matti@matti.fi')
GO
INSERT [dbo].[Users] ([KayttajaNimi], [Rooli], [Etunimi], [Sukunimi], [Salasana], [Sahkoposti]) VALUES (N'Pekka', N'Käyttäjä', N'Pekka', N'Pekkarinen', N'pekka606', N'pekka@pekka.com')
GO
INSERT [dbo].[Users] ([KayttajaNimi], [Rooli], [Etunimi], [Sukunimi], [Salasana], [Sahkoposti]) VALUES (N'SuperUser', N'Pääkäyttäjä', N'Super', N'User', N'superuser', NULL)
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK__Users__Rooli__6442E2C9] FOREIGN KEY([Rooli])
REFERENCES [dbo].[UserRoles] ([Role])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK__Users__Rooli__6442E2C9]
GO
/****** Object:  Trigger [dbo].[TR_DeleteEmptyFromPortfolio]    Script Date: 7.12.2020 9.33.00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE TRIGGER [dbo].[TR_DeleteEmptyFromPortfolio] ON [dbo].[Portfolio]

AFTER INSERT, UPDATE

AS
BEGIN

SET NOCOUNT ON;

    DELETE FROM Portfolio
	WHERE  MaaraYht <= 0;

END
GO
ALTER TABLE [dbo].[Portfolio] ENABLE TRIGGER [TR_DeleteEmptyFromPortfolio]
GO
/****** Object:  Trigger [dbo].[TR_AddToPortfolio]    Script Date: 7.12.2020 9.33.01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Trigger for updating Portfolio table with inserted and updated data from Transactions table
CREATE TRIGGER [dbo].[TR_AddToPortfolio] ON [dbo].[Transactions]

AFTER UPDATE, INSERT

AS
BEGIN

SET NOCOUNT ON;

--Getting User and Comapny from inserted
DECLARE @KayttajaTrans nvarchar (10)
SELECT @KayttajaTrans = Kayttaja
FROM inserted
DECLARE @YritysTrans nvarchar (10)
SELECT @YritysTrans = Yritys
FROM inserted

--Updating MaaraYht and HankintaArvo column values in Portfolio
DECLARE @MaaraForPortfolioSum int
DECLARE @TotalForPortfolioSum money
DECLARE @MaaraYht int
SELECT @MaaraYht = MaaraYht
FROM Portfolio

--Variables for testing purposes
DECLARE @MaaraForPortfolio int
SELECT @MaaraForPortfolio = MaaraForPortfolio
FROM Transactions
DECLARE @TotalForPortfolio money
SELECT @TotalForPortfolio = TotalEuros
FROM Transactions

DECLARE @HankintaArvo money
SELECT @HankintaArvo = HankintaArvo
FROM Portfolio

--TEST PRINTS
PRINT @MaaraForPortfolio
PRINT @TotalForPortfolio
PRINT @MaaraYht
PRINT @HankintaArvo
PRINT @KayttajaTrans
PRINT @YritysTrans

--UPDATE: Company and user EXISTS in Portfolio, update MaaraYht and HankintaArvo columns
IF EXISTS(SELECT * FROM Portfolio, inserted WHERE Portfolio.Kayttaja = inserted.Kayttaja AND Portfolio.Yritys = inserted.Yritys)
	BEGIN
		SET @MaaraForPortfolioSum=(SELECT sum(MaaraForPortfolio) FROM Transactions WHERE Kayttaja = @KayttajaTrans AND Yritys = @YritysTrans)
		SET @TotalForPortfolioSum=(SELECT sum(TotalEuros) FROM Transactions WHERE Kayttaja = @KayttajaTrans AND Yritys = @YritysTrans)
		UPDATE Portfolio SET Portfolio.MaaraYht=@MaaraForPortfolioSum, Portfolio.HankintaArvo=@TotalForPortfolioSum
		FROM inserted
		WHERE Portfolio.Kayttaja = inserted.Kayttaja AND Portfolio.Yritys = inserted.Yritys
	END

--INSERT: Company and user DO NOT EXIST in Portfolio, add values to to Kayttaja,Yritys,MaaraYht and HankintaArvo columns
ELSE
	BEGIN
		INSERT INTO Portfolio
			(Kayttaja,Yritys,MaaraYht,HankintaArvo)
		SELECT
			Kayttaja,Yritys,MaaraForPortfolio,TotalEuros
		FROM inserted
	END
END
GO
ALTER TABLE [dbo].[Transactions] ENABLE TRIGGER [TR_AddToPortfolio]
GO
/****** Object:  Trigger [dbo].[TR_DeleteFromPortfolio]    Script Date: 7.12.2020 9.33.01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[TR_DeleteFromPortfolio] ON [dbo].[Transactions]

AFTER DELETE, INSERT

AS
BEGIN

SET NOCOUNT ON;

--Updating MaaraYht and HankintaArvo column values in Portfolio
DECLARE @MaaraForPortfolioSum int
DECLARE @TotalForPortfolioSum money
DECLARE @MaaraYht int
SELECT @MaaraYht = MaaraYht
FROM Portfolio

--Getting User and Company from deleted
DECLARE @KayttajaTrans nvarchar (10)
SELECT @KayttajaTrans = Kayttaja
FROM deleted
DECLARE @YritysTrans nvarchar (10)
SELECT @YritysTrans = Yritys
FROM deleted

--PRINT @KayttajaTrans
--PRINT @YritysTrans

IF EXISTS(SELECT * FROM Portfolio, deleted WHERE Portfolio.Kayttaja = deleted.Kayttaja AND Portfolio.Yritys = deleted.Yritys)
	BEGIN
		SET @MaaraForPortfolioSum=(SELECT sum(MaaraForPortfolio) FROM Transactions WHERE Kayttaja = @KayttajaTrans AND Yritys = @YritysTrans)
		SET @TotalForPortfolioSum=(SELECT sum(TotalEuros) FROM Transactions WHERE Kayttaja = @KayttajaTrans AND Yritys = @YritysTrans)
		UPDATE Portfolio SET Portfolio.MaaraYht=@MaaraForPortfolioSum, Portfolio.HankintaArvo=@TotalForPortfolioSum
		FROM deleted
		WHERE Portfolio.Kayttaja = deleted.Kayttaja AND Portfolio.Yritys = deleted.Yritys
		DELETE FROM Portfolio WHERE MaaraYht IS NULL
	END
END
GO
ALTER TABLE [dbo].[Transactions] ENABLE TRIGGER [TR_DeleteFromPortfolio]
GO
