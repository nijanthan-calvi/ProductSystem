USE [ProductDB]
GO
/****** Object:  Table [dbo].[PRODUCT_CATEGORIES]    Script Date: 29-11-2024 11:38:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE PRODUCT_CATEGORIES (
    CATEGORY_ID INT IDENTITY(1,1) PRIMARY KEY, -- Auto-incrementing primary key
    CATEGORY_NAME NVARCHAR(50) NOT NULL UNIQUE -- Unique category name
);
GO
/****** Object:  Table [dbo].[PRODUCTS]    Script Date: 29-11-2024 11:38:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE PRODUCTS (
    PRODUCT_ID INT IDENTITY(1,1) PRIMARY KEY,        -- Auto-incrementing primary key
    PRODUCT_NAME NVARCHAR(100) NOT NULL,             -- Product name
    PRODUCT_DESCRIPTION NVARCHAR(MAX),              -- Product description
    CATEGORY_ID INT NOT NULL,                        -- Foreign key to PRODUCT_CATEGORIES
    PRODUCT_PRICE DECIMAL(18,2) NOT NULL,            -- Product price with two decimal places
    FOREIGN KEY (CATEGORY_ID) REFERENCES PRODUCT_CATEGORIES(CATEGORY_ID) -- Foreign key constraint
);
GO
