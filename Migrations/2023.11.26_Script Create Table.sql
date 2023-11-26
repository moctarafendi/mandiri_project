USE [Mandiri]
GO

/****** Object:  Table [dbo].[BusinessAreaMaster]    Script Date: 11/26/2023 11:49:14 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BusinessAreaMaster](
	[AreaCode] [nvarchar](15) NOT NULL,
	[AreaName] [nvarchar](100) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_Business_Area_Master] PRIMARY KEY CLUSTERED 
(
	[AreaCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[BusinessAreaMaster] ADD  CONSTRAINT [DF_Business_Area_Master_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO

----------------------------------------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[ApplicationUser](
	[UserId] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Password] [text] NULL,
	[Phone] [nvarchar](15) NOT NULL,
	[Role] [nvarchar](15) NOT NULL,
	[BusinessAreaCode] [nvarchar](15) NOT NULL,
	[TokenExpireDate] [datetime] NOT NULL,
	[Token] [text] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_App_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[ApplicationUser] ADD  CONSTRAINT [DF_User_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO


--------------------------------------------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[Employee](
	[EmployeeNo] [nvarchar](15) NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NULL,
	[HireDate] [date] NOT NULL,
	[TerminationDate] [date] NULL,
	[Salary] [decimal](18, 2) NOT NULL,
	[BusinessAreaCode] [nvarchar](15) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EmployeeNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Employee] ADD  CONSTRAINT [DF_Employee_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO

ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Area_Code_2] FOREIGN KEY([BusinessAreaCode])
REFERENCES [dbo].[BusinessAreaMaster] ([AreaCode])
GO

ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Area_Code_2]
GO

--------------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[AnnualReview](
	[ID] [uniqueidentifier] NOT NULL,
	[EmployeeNo] [nvarchar](15) NOT NULL,
	[ReviewDate] [date] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_Annual_Review] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AnnualReview] ADD  CONSTRAINT [DF_Annual_Review_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO

ALTER TABLE [dbo].[AnnualReview] ADD  CONSTRAINT [DF_AnnualReview_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO

ALTER TABLE [dbo].[AnnualReview]  WITH CHECK ADD  CONSTRAINT [FK_Employee_No] FOREIGN KEY([EmployeeNo])
REFERENCES [dbo].[Employee] ([EmployeeNo])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AnnualReview] CHECK CONSTRAINT [FK_Employee_No]
GO


