USE [CRMDb]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 24.02.2018 00:09:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Price] [money] NULL,
	[Stock] [int] NULL,
	[About] [varchar](50) NULL,
	[Category] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  StoredProcedure [dbo].[SP_Delete]    Script Date: 24.02.2018 00:09:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_Delete]
@id int
AS
BEGIN
	
	Delete Products where Id=@id
	SET NOCOUNT ON;

   
END

GO
/****** Object:  StoredProcedure [dbo].[SP_Insert]    Script Date: 24.02.2018 00:09:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_Insert]
@Name Varchar(50),
@Price money,
@Stock Int,
@About varchar(50),
@Category varchar(50)
AS
BEGIN
	
	insert Products (Name,Price,Stock,About,Category) values (@Name,@Price,@Stock,@About,@Category)  
	SET NOCOUNT ON;

   
END

GO
/****** Object:  StoredProcedure [dbo].[SP_List]    Script Date: 24.02.2018 00:09:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_List]

AS
BEGIN
	
	Select *from Products
	SET NOCOUNT ON;

   
END

GO
/****** Object:  StoredProcedure [dbo].[SP_ListSearch]    Script Date: 24.02.2018 00:09:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_ListSearch]
	
@search varchar(50)=null
AS
BEGIN

	SET NOCOUNT ON;
	SELECT *from Products where [Name] like '%'+@search+'%'
END

GO
/****** Object:  StoredProcedure [dbo].[SP_Update]    Script Date: 24.02.2018 00:09:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_Update]
@Name Varchar(50),
@Price money,
@Stock Int,
@About varchar(50),
@Category varchar(50),
@id int
AS
BEGIN
	
	Update Products set [Name]=@Name,Price=@Price,Stock=@Stock,About=@About,Category=@Category where Id=@id
	SET NOCOUNT ON;

   
END

GO
