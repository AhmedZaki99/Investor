BEGIN TRANSACTION;
GO

ALTER TABLE [Invoices] DROP CONSTRAINT [FK_Invoices_Customers_CustomerId];
GO

DROP TABLE [BillItems];
GO

DROP TABLE [CreditPayments];
GO

DROP TABLE [CustomerPayments];
GO

DROP TABLE [InvoiceItems];
GO

DROP TABLE [Bills];
GO

DROP TABLE [Customers];
GO

DROP TABLE [Vendors];
GO

DROP INDEX [IX_Products_IsService] ON [Products];
GO

EXEC sp_rename N'[Invoices].[CustomerId]', N'TraderId', N'COLUMN';
GO

EXEC sp_rename N'[Invoices].[IX_Invoices_CustomerId]', N'IX_Invoices_TraderId', N'INDEX';
GO

ALTER TABLE [Invoices] ADD [InvoiceType] int NOT NULL DEFAULT 0;
GO

CREATE TABLE [Items] (
    [Id] nvarchar(450) NOT NULL,
    [InvoiceId] nvarchar(450) NOT NULL,
    [ProductId] nvarchar(450) NOT NULL,
    [Description] nvarchar(1024) NULL,
    [Quantity] int NOT NULL,
    [Price] decimal(19,4) NOT NULL,
    [Amount] AS [Quantity] * [Price],
    CONSTRAINT [PK_Items] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Items_Invoices_InvoiceId] FOREIGN KEY ([InvoiceId]) REFERENCES [Invoices] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Items_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Traders] (
    [Id] nvarchar(450) NOT NULL,
    [TraderType] int NOT NULL,
    [Name] nvarchar(256) NOT NULL,
    [Notes] nvarchar(1024) NULL,
    [ContactId] nvarchar(450) NULL,
    [AddressId] nvarchar(450) NULL,
    [DateCreated] datetime2(3) NOT NULL DEFAULT (GETUTCDATE()),
    [DateModified] datetime2(3) NULL,
    [BusinessId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_Traders] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Traders_Addresses_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [Addresses] ([Id]),
    CONSTRAINT [FK_Traders_Businesses_BusinessId] FOREIGN KEY ([BusinessId]) REFERENCES [Businesses] ([Id]),
    CONSTRAINT [FK_Traders_Contacts_ContactId] FOREIGN KEY ([ContactId]) REFERENCES [Contacts] ([Id])
);
GO

CREATE TABLE [Payments] (
    [Id] nvarchar(450) NOT NULL,
    [TraderId] nvarchar(450) NULL,
    [PaymentType] int NOT NULL,
    [Number] int NULL,
    [Amount] decimal(19,4) NOT NULL,
    [PaymentDate] datetime2(0) NOT NULL,
    [Notes] nvarchar(1024) NULL,
    [PaymentMethodId] nvarchar(450) NOT NULL,
    [DateCreated] datetime2(3) NOT NULL DEFAULT (GETUTCDATE()),
    [DateModified] datetime2(3) NULL,
    [BusinessId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_Payments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Payments_Businesses_BusinessId] FOREIGN KEY ([BusinessId]) REFERENCES [Businesses] ([Id]),
    CONSTRAINT [FK_Payments_PaymentMethods_PaymentMethodId] FOREIGN KEY ([PaymentMethodId]) REFERENCES [PaymentMethods] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Payments_Traders_TraderId] FOREIGN KEY ([TraderId]) REFERENCES [Traders] ([Id])
);
GO

CREATE INDEX [IX_Products_IsService_BusinessId] ON [Products] ([IsService], [BusinessId]);
GO

CREATE UNIQUE INDEX [IX_PaymentMethods_Name] ON [PaymentMethods] ([Name]);
GO

CREATE INDEX [IX_Invoices_InvoiceType_IsReturn_TraderId] ON [Invoices] ([InvoiceType], [IsReturn], [TraderId]);
GO

CREATE UNIQUE INDEX [IX_BusinessTypes_Name] ON [BusinessTypes] ([Name]);
GO

CREATE UNIQUE INDEX [IX_Businesses_Name] ON [Businesses] ([Name]);
GO

CREATE UNIQUE INDEX [IX_Accounts_Name] ON [Accounts] ([Name]);
GO

CREATE INDEX [IX_Items_InvoiceId] ON [Items] ([InvoiceId]);
GO

CREATE INDEX [IX_Items_ProductId] ON [Items] ([ProductId]);
GO

CREATE INDEX [IX_Payments_BusinessId] ON [Payments] ([BusinessId]);
GO

CREATE INDEX [IX_Payments_PaymentMethodId] ON [Payments] ([PaymentMethodId]);
GO

CREATE INDEX [IX_Payments_PaymentType_TraderId] ON [Payments] ([PaymentType], [TraderId]);
GO

CREATE INDEX [IX_Payments_TraderId] ON [Payments] ([TraderId]);
GO

CREATE INDEX [IX_Traders_AddressId] ON [Traders] ([AddressId]);
GO

CREATE INDEX [IX_Traders_BusinessId] ON [Traders] ([BusinessId]);
GO

CREATE INDEX [IX_Traders_ContactId] ON [Traders] ([ContactId]);
GO

CREATE UNIQUE INDEX [IX_Traders_Name] ON [Traders] ([Name]);
GO

CREATE INDEX [IX_Traders_TraderType_BusinessId] ON [Traders] ([TraderType], [BusinessId]);
GO

ALTER TABLE [Invoices] ADD CONSTRAINT [FK_Invoices_Traders_TraderId] FOREIGN KEY ([TraderId]) REFERENCES [Traders] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221003171609_CreateUnifiedTablesForCommonData', N'6.0.9');
GO

COMMIT;
GO

