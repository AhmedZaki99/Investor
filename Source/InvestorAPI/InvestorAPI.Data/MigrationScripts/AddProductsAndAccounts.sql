BEGIN TRANSACTION;
GO

CREATE TABLE [Accounts] (
    [AccountId] nvarchar(450) NOT NULL,
    [AccountType] int NOT NULL,
    [ParentAccountId] nvarchar(450) NULL,
    [Name] nvarchar(255) NOT NULL,
    [Description] nvarchar(1023) NULL,
    [Balance] decimal(19,4) NULL,
    [DateCreated] datetime2(3) NOT NULL DEFAULT (GETUTCDATE()),
    [DateModified] datetime2(3) NULL,
    CONSTRAINT [PK_Accounts] PRIMARY KEY ([AccountId]),
    CONSTRAINT [FK_Accounts_Accounts_ParentAccountId] FOREIGN KEY ([ParentAccountId]) REFERENCES [Accounts] ([AccountId])
);
GO

CREATE TABLE [Categories] (
    [CategoryId] nvarchar(450) NOT NULL,
    [Name] nvarchar(255) NOT NULL,
    [Description] nvarchar(1023) NULL,
    [DateCreated] datetime2(3) NOT NULL DEFAULT (GETUTCDATE()),
    [DateModified] datetime2(3) NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([CategoryId])
);
GO

CREATE TABLE [Products] (
    [ProductId] nvarchar(450) NOT NULL,
    [SKU] nvarchar(255) NULL,
    [Quantity] int NOT NULL,
    [ReorderPoint] int NULL,
    [InventoryAccountId] nvarchar(450) NULL,
    [DateCreated] datetime2(3) NOT NULL DEFAULT (GETUTCDATE()),
    [DateModified] datetime2(3) NULL,
    [Name] nvarchar(255) NOT NULL,
    [CategoryId] nvarchar(450) NULL,
    [SalesPrice] decimal(19,4) NULL,
    [SalesDescription] nvarchar(1023) NULL,
    [IncomeAccountId] nvarchar(450) NULL,
    [Cost] decimal(19,4) NULL,
    [PurchaseDescription] nvarchar(1023) NULL,
    [ExpenseAccountId] nvarchar(450) NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([ProductId]),
    CONSTRAINT [FK_Products_Accounts_ExpenseAccountId] FOREIGN KEY ([ExpenseAccountId]) REFERENCES [Accounts] ([AccountId]),
    CONSTRAINT [FK_Products_Accounts_IncomeAccountId] FOREIGN KEY ([IncomeAccountId]) REFERENCES [Accounts] ([AccountId]),
    CONSTRAINT [FK_Products_Accounts_InventoryAccountId] FOREIGN KEY ([InventoryAccountId]) REFERENCES [Accounts] ([AccountId]),
    CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([CategoryId])
);
GO

CREATE TABLE [Services] (
    [ServiceId] nvarchar(450) NOT NULL,
    [DateCreated] datetime2(3) NOT NULL DEFAULT (GETUTCDATE()),
    [DateModified] datetime2(3) NULL,
    [Name] nvarchar(255) NOT NULL,
    [CategoryId] nvarchar(450) NULL,
    [SalesPrice] decimal(19,4) NULL,
    [SalesDescription] nvarchar(1023) NULL,
    [IncomeAccountId] nvarchar(450) NULL,
    [Cost] decimal(19,4) NULL,
    [PurchaseDescription] nvarchar(1023) NULL,
    [ExpenseAccountId] nvarchar(450) NULL,
    CONSTRAINT [PK_Services] PRIMARY KEY ([ServiceId]),
    CONSTRAINT [FK_Services_Accounts_ExpenseAccountId] FOREIGN KEY ([ExpenseAccountId]) REFERENCES [Accounts] ([AccountId]),
    CONSTRAINT [FK_Services_Accounts_IncomeAccountId] FOREIGN KEY ([IncomeAccountId]) REFERENCES [Accounts] ([AccountId]),
    CONSTRAINT [FK_Services_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([CategoryId])
);
GO

CREATE INDEX [IX_Accounts_ParentAccountId] ON [Accounts] ([ParentAccountId]);
GO

CREATE INDEX [IX_Products_CategoryId] ON [Products] ([CategoryId]);
GO

CREATE INDEX [IX_Products_ExpenseAccountId] ON [Products] ([ExpenseAccountId]);
GO

CREATE INDEX [IX_Products_IncomeAccountId] ON [Products] ([IncomeAccountId]);
GO

CREATE INDEX [IX_Products_InventoryAccountId] ON [Products] ([InventoryAccountId]);
GO

CREATE INDEX [IX_Services_CategoryId] ON [Services] ([CategoryId]);
GO

CREATE INDEX [IX_Services_ExpenseAccountId] ON [Services] ([ExpenseAccountId]);
GO

CREATE INDEX [IX_Services_IncomeAccountId] ON [Services] ([IncomeAccountId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220820070157_AddProductsAndAccounts', N'6.0.8');
GO

COMMIT;
GO

