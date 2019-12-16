USE [OpenMes]
GO

/****** Object:  Table [dbo].[CustInfoTbl]    Script Date: 2019-12-17 ¿ÀÀü 3:30:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CustInfoTbl](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerCode] [varchar](20) NOT NULL,
	[InOutcategory] [varchar](50) NULL,
	[CustomerInOutcategory] [varchar](50) NULL,
	[CustomerVat] [varchar](20) NULL,
	[CustomerName] [varchar](50) NULL,
	[CompanyName] [varchar](50) NULL,
	[RepresentativerName] [varchar](50) NULL,
	[RegistrationNo] [varchar](20) NULL,
	[CorporationNo] [varchar](20) NULL,
	[SubLicenseeNo] [varchar](20) NULL,
	[BusinessType] [varchar](20) NULL,
	[BusinessItem] [varchar](50) NULL,
	[CompanyPhoneNo] [varchar](20) NULL,
	[CompanyFAXNo] [varchar](20) NULL,
	[CompanyAddress] [varchar](100) NULL,
	[PostAddress] [varchar](100) NULL,
	[PostNo] [varchar](10) NULL,
	[HomePage] [varchar](50) NULL,
	[CompanyEmail] [varchar](50) NULL,
	[CustomerMemo] [varchar](255) NULL,
	[Area] [varchar](20) NULL,
	[Bizman] [varchar](50) NULL,
	[PriceGroup] [varchar](50) NULL,
	[RegUser] [varchar](30) NULL,
	[RegDate] [varchar](30) NULL,
	[ModUser] [varchar](30) NULL,
	[ModDate] [varchar](30) NULL,
 CONSTRAINT [PK_CustInfoTbl] PRIMARY KEY CLUSTERED 
(
	[CustomerCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


