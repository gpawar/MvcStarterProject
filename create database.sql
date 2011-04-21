PRINT N'Creating [dbo].[OrderProducts]...';


GO
CREATE TABLE [dbo].[OrderProducts] (
    [OrderId]   INT NOT NULL,
    [ProductId] INT NOT NULL
);


GO
PRINT N'Creating [dbo].[Orders]...';


GO
CREATE TABLE [dbo].[Orders] (
    [OrderId]   INT         IDENTITY (1, 1) NOT NULL,
    [StateCode] VARCHAR (2) NULL
);


GO
PRINT N'Creating PK_Orders...';


GO
ALTER TABLE [dbo].[Orders]
    ADD CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED ([OrderId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[Products]...';


GO
CREATE TABLE [dbo].[Products] (
    [ProductId] INT          IDENTITY (1, 1) NOT NULL,
    [Name]      VARCHAR (50) NOT NULL,
    [Price]     MONEY        NOT NULL,
    [IsActive]  BIT          NOT NULL
);


GO
PRINT N'Creating PK_Products...';


GO
ALTER TABLE [dbo].[Products]
    ADD CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([ProductId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating DF_Products_IsActive...';


GO
ALTER TABLE [dbo].[Products]
    ADD CONSTRAINT [DF_Products_IsActive] DEFAULT ((1)) FOR [IsActive];


GO
PRINT N'Creating FK_OrderProducts_Products...';


GO
ALTER TABLE [dbo].[OrderProducts] WITH NOCHECK
    ADD CONSTRAINT [FK_OrderProducts_Products] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([ProductId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_OrderProducts_Orders...';


GO
ALTER TABLE [dbo].[OrderProducts] WITH NOCHECK
    ADD CONSTRAINT [FK_OrderProducts_Orders] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders] ([OrderId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Checking existing data against newly created constraints';


GO

ALTER TABLE [dbo].[OrderProducts] WITH CHECK CHECK CONSTRAINT [FK_OrderProducts_Products];

ALTER TABLE [dbo].[OrderProducts] WITH CHECK CHECK CONSTRAINT [FK_OrderProducts_Orders];


GO
