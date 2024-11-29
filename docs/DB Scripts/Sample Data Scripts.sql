USE [ProductDB]
GO
SET IDENTITY_INSERT [dbo].[PRODUCT_CATEGORIES] ON 
GO
INSERT [dbo].[PRODUCT_CATEGORIES] ([CATEGORY_ID], [CATEGORY_NAME]) VALUES (1, N'Electronics')
GO
INSERT [dbo].[PRODUCT_CATEGORIES] ([CATEGORY_ID], [CATEGORY_NAME]) VALUES (2, N'Furniture')
GO
INSERT [dbo].[PRODUCT_CATEGORIES] ([CATEGORY_ID], [CATEGORY_NAME]) VALUES (3, N'Appliances')
GO
SET IDENTITY_INSERT [dbo].[PRODUCT_CATEGORIES] OFF
GO
SET IDENTITY_INSERT [dbo].[PRODUCTS] ON 
GO
INSERT [dbo].[PRODUCTS] ([PRODUCT_ID], [PRODUCT_NAME], [PRODUCT_DESCRIPTION], [CATEGORY_ID], [PRODUCT_PRICE]) VALUES (1, N'Laptop', N'A high-performance laptop', 1, CAST(1299.99 AS Decimal(18, 2)))
GO
INSERT [dbo].[PRODUCTS] ([PRODUCT_ID], [PRODUCT_NAME], [PRODUCT_DESCRIPTION], [CATEGORY_ID], [PRODUCT_PRICE]) VALUES (2, N'Desk Chair', N'Ergonomic office chair', 2, CAST(199.99 AS Decimal(18, 2)))
GO
INSERT [dbo].[PRODUCTS] ([PRODUCT_ID], [PRODUCT_NAME], [PRODUCT_DESCRIPTION], [CATEGORY_ID], [PRODUCT_PRICE]) VALUES (3, N'Coffee Maker', N'Automatic coffee brewing machine', 3, CAST(89.99 AS Decimal(18, 2)))
GO
SET IDENTITY_INSERT [dbo].[PRODUCTS] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__PRODUCT___9374460F197DCCE5]    Script Date: 29-11-2024 11:38:44 ******/
ALTER TABLE [dbo].[PRODUCT_CATEGORIES] ADD UNIQUE NONCLUSTERED 
(
	[CATEGORY_NAME] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PRODUCTS]  WITH CHECK ADD FOREIGN KEY([CATEGORY_ID])
REFERENCES [dbo].[PRODUCT_CATEGORIES] ([CATEGORY_ID])
GO
