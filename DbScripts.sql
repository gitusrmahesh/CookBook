
/****** Object:  Table [dbo].[Recipe]    Script Date: 25/09/2023 04:23:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recipe](
	[RecipeId] [int] IDENTITY(1,1) NOT NULL,
	[RecipeTypeId] [int] NULL,
	[RecipeName] [varchar](100) NULL,
	[Source] [varchar](100) NULL,
	[Description] [varchar](max) NULL,
	[Ingredients] [varchar](max) NULL,
 CONSTRAINT [PK__Recipe__FDD988B0B51B3362] PRIMARY KEY CLUSTERED 
(
	[RecipeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecipeType]    Script Date: 25/09/2023 04:23:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecipeType](
	[RecipeTypeId] [int] IDENTITY(1,1) NOT NULL,
	[RecipeTypeName] [varchar](100) NULL,
	[Category] [int] NULL,
 CONSTRAINT [PK__RecipeTy__AFE5A66EFA5B18EA] PRIMARY KEY CLUSTERED 
(
	[RecipeTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[spAddRecipe]    Script Date: 25/09/2023 04:23:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[spAddRecipe]
( 
	@RecipeName VARCHAR(256),           
    @RecipeType VARCHAR(max),          
    @Description VARCHAR(max),
	@Ingredients VARCHAR(max),
	@Category int,
    @Source VARCHAR(256)
)
AS 
BEGIN
SET ANSI_WARNINGS OFF

	INSERT INTO RecipeType(RecipeTypeName,Category)
	 VALUES(@RecipeType,@Category)
	 DECLARE @RecipeTypeId int = (SELECT MAX(RecipeTypeId) from RecipeType);

	 INSERT INTO Recipe(RecipeName,RecipeTypeId,Description,Source,Ingredients)
	 VALUES(@RecipeName,@RecipeTypeId,@Description,@Source,@Ingredients)


SET ANSI_WARNINGS ON
END
GO
/****** Object:  StoredProcedure [dbo].[spDeleteRecipe]    Script Date: 25/09/2023 04:23:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create proc [dbo].[spDeleteRecipe]
(
    @RecipeId INT       
	
)
AS 
BEGIN
	 DELETE FROM Recipe	WHERE RecipeId=@RecipeId
END
GO
/****** Object:  StoredProcedure [dbo].[spGetAllRecipes]    Script Date: 25/09/2023 04:23:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[spGetAllRecipes]
AS 
BEGIN
	 Select RecipeId,RecipeName,RecipeTypeName AS RecipeType,Description,Source,Category,Ingredients 
	 FROM Recipe R	
	 INNER JOIN RecipeType RE ON R.RecipeTypeId=RE.RecipeTypeId
END

GO
/****** Object:  StoredProcedure [dbo].[spGetRecipeById]    Script Date: 25/09/2023 04:23:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[spGetRecipeById]
(
    @RecipeId INT       
	
)
AS 
BEGIN
	 Select RecipeId,RecipeName,RecipeTypeName AS RecipeType,Description,Source,Category,Ingredients 
	 FROM Recipe R	
	 INNER JOIN RecipeType RE ON R.RecipeTypeId=RE.RecipeTypeId
	 WHERE RecipeId=@RecipeId
END
GO
/****** Object:  StoredProcedure [dbo].[spUpdateRecipe]    Script Date: 25/09/2023 04:23:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[spUpdateRecipe]
(
	@RecipeID int,
	@RecipeName VARCHAR(256),           
    @RecipeType VARCHAR(max),          
    @Description VARCHAR(max),
	@Ingredients VARCHAR(max),
	@Category int,
    @Source VARCHAR(256)
)
AS 
BEGIN



UPDATE R
    SET R.RecipeName = @RecipeName,
		R.Ingredients=@Ingredients,
		R.Description=@Description,
		R.Source=@Source
    FROM Recipe AS R
	 INNER JOIN RecipeType As RE ON R.RecipeTypeId = RE.RecipeTypeId
    WHERE R.RecipeID = @RecipeID;

	UPDATE RE
	SET
		RE.RecipeTypeName=@RecipeType,
		RE.Category=@Category
		 FROM Recipe AS R
    INNER JOIN RecipeType As RE ON R.RecipeTypeId = RE.RecipeTypeId
	WHERE R.RecipeID = @RecipeID;

END
GO
