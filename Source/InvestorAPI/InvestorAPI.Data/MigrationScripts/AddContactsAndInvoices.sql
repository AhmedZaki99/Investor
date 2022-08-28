BEGIN TRANSACTION;
GO

DROP TABLE [Services];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'Quantity');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Products] ALTER COLUMN [Quantity] float NULL;
GO

ALTER TABLE [Products] ADD [IsService] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Products] ADD [ScaleUnitId] nvarchar(450) NULL;
GO

CREATE TABLE [Addresses] (
    [AddressId] nvarchar(450) NOT NULL,
    [Country] nvarchar(127) NOT NULL,
    [Province] nvarchar(127) NULL,
    [AddressLine1] nvarchar(255) NULL,
    [AddressLine2] nvarchar(255) NULL,
    [City] nvarchar(127) NULL,
    [PostalCode] nvarchar(63) NULL,
    CONSTRAINT [PK_Addresses] PRIMARY KEY ([AddressId])
);
GO

CREATE TABLE [Contacts] (
    [ContactId] nvarchar(450) NOT NULL,
    [FirstName] nvarchar(255) NULL,
    [LastName] nvarchar(255) NULL,
    [Email] nvarchar(255) NULL,
    [Phone] nvarchar(127) NULL,
    [DateCreated] datetime2(3) NOT NULL DEFAULT (GETUTCDATE()),
    [DateModified] datetime2(3) NULL,
    CONSTRAINT [PK_Contacts] PRIMARY KEY ([ContactId])
);
GO

CREATE TABLE [ScaleUnits] (
    [ScaleUnitId] nvarchar(450) NOT NULL,
    [Name] nvarchar(255) NOT NULL,
    [Description] nvarchar(1023) NULL,
    [DateCreated] datetime2(3) NOT NULL DEFAULT (GETUTCDATE()),
    [DateModified] datetime2(3) NULL,
    CONSTRAINT [PK_ScaleUnits] PRIMARY KEY ([ScaleUnitId])
);
GO

CREATE TABLE [Customers] (
    [CustomerId] nvarchar(450) NOT NULL,
    [Name] nvarchar(255) NOT NULL,
    [Notes] nvarchar(1023) NULL,
    [PrimaryContactId] nvarchar(450) NULL,
    [BillingAddressId] nvarchar(450) NULL,
    [DateCreated] datetime2(3) NOT NULL DEFAULT (GETUTCDATE()),
    [DateModified] datetime2(3) NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY ([CustomerId]),
    CONSTRAINT [FK_Customers_Addresses_BillingAddressId] FOREIGN KEY ([BillingAddressId]) REFERENCES [Addresses] ([AddressId]),
    CONSTRAINT [FK_Customers_Contacts_PrimaryContactId] FOREIGN KEY ([PrimaryContactId]) REFERENCES [Contacts] ([ContactId])
);
GO

CREATE TABLE [Vendors] (
    [VendorId] nvarchar(450) NOT NULL,
    [Name] nvarchar(255) NOT NULL,
    [Notes] nvarchar(1023) NULL,
    [ContactId] nvarchar(450) NULL,
    [AddressId] nvarchar(450) NULL,
    [DateCreated] datetime2(3) NOT NULL DEFAULT (GETUTCDATE()),
    [DateModified] datetime2(3) NULL,
    CONSTRAINT [PK_Vendors] PRIMARY KEY ([VendorId]),
    CONSTRAINT [FK_Vendors_Addresses_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [Addresses] ([AddressId]),
    CONSTRAINT [FK_Vendors_Contacts_ContactId] FOREIGN KEY ([ContactId]) REFERENCES [Contacts] ([ContactId])
);
GO

CREATE TABLE [UnitConversion] (
    [UnitConversionId] nvarchar(450) NOT NULL,
    [SourceUnitId] nvarchar(450) NOT NULL,
    [TargetUnitId] nvarchar(450) NOT NULL,
    [ConversionValue] float NOT NULL,
    [Description] nvarchar(1023) NULL,
    CONSTRAINT [PK_UnitConversion] PRIMARY KEY ([UnitConversionId]),
    CONSTRAINT [FK_UnitConversion_ScaleUnits_SourceUnitId] FOREIGN KEY ([SourceUnitId]) REFERENCES [ScaleUnits] ([ScaleUnitId]),
    CONSTRAINT [FK_UnitConversion_ScaleUnits_TargetUnitId] FOREIGN KEY ([TargetUnitId]) REFERENCES [ScaleUnits] ([ScaleUnitId])
);
GO

CREATE TABLE [Invoices] (
    [InvoiceId] nvarchar(450) NOT NULL,
    [CustomerId] nvarchar(450) NULL,
    [DateCreated] datetime2(3) NOT NULL DEFAULT (GETUTCDATE()),
    [DateModified] datetime2(3) NULL,
    [Number] int NULL,
    [IsDue] bit NOT NULL,
    [IsTracked] bit NOT NULL,
    [IsReturn] bit NOT NULL,
    [IssueDate] datetime2(0) NOT NULL,
    [PaymentDue] datetime2(0) NULL,
    [Notes] nvarchar(1023) NULL,
    CONSTRAINT [PK_Invoices] PRIMARY KEY ([InvoiceId]),
    CONSTRAINT [FK_Invoices_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([CustomerId])
);
GO

CREATE TABLE [Bills] (
    [BillId] nvarchar(450) NOT NULL,
    [VendorId] nvarchar(450) NULL,
    [DateCreated] datetime2(3) NOT NULL DEFAULT (GETUTCDATE()),
    [DateModified] datetime2(3) NULL,
    [Number] int NULL,
    [IsDue] bit NOT NULL,
    [IsTracked] bit NOT NULL,
    [IsReturn] bit NOT NULL,
    [IssueDate] datetime2(0) NOT NULL,
    [PaymentDue] datetime2(0) NULL,
    [Notes] nvarchar(1023) NULL,
    CONSTRAINT [PK_Bills] PRIMARY KEY ([BillId]),
    CONSTRAINT [FK_Bills_Vendors_VendorId] FOREIGN KEY ([VendorId]) REFERENCES [Vendors] ([VendorId])
);
GO

CREATE TABLE [InvoiceItems] (
    [InvoiceItemId] nvarchar(450) NOT NULL,
    [InvoiceId] nvarchar(450) NOT NULL,
    [ProductId] nvarchar(450) NOT NULL,
    [Description] nvarchar(1023) NULL,
    [Quantity] int NOT NULL,
    [Price] decimal(19,4) NOT NULL,
    [Amount] AS [Quantity] * [Price],
    CONSTRAINT [PK_InvoiceItems] PRIMARY KEY ([InvoiceItemId]),
    CONSTRAINT [FK_InvoiceItems_Invoices_InvoiceId] FOREIGN KEY ([InvoiceId]) REFERENCES [Invoices] ([InvoiceId]) ON DELETE CASCADE,
    CONSTRAINT [FK_InvoiceItems_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([ProductId]) ON DELETE CASCADE
);
GO

CREATE TABLE [BillItems] (
    [BillItemId] nvarchar(450) NOT NULL,
    [ExpenseCategoryId] nvarchar(450) NOT NULL,
    [BillId] nvarchar(450) NOT NULL,
    [ProductId] nvarchar(450) NOT NULL,
    [Description] nvarchar(1023) NULL,
    [Quantity] int NOT NULL,
    [Price] decimal(19,4) NOT NULL,
    [Amount] decimal(19,4) NOT NULL,
    CONSTRAINT [PK_BillItems] PRIMARY KEY ([BillItemId]),
    CONSTRAINT [FK_BillItems_Accounts_ExpenseCategoryId] FOREIGN KEY ([ExpenseCategoryId]) REFERENCES [Accounts] ([AccountId]) ON DELETE CASCADE,
    CONSTRAINT [FK_BillItems_Bills_BillId] FOREIGN KEY ([BillId]) REFERENCES [Bills] ([BillId]) ON DELETE CASCADE,
    CONSTRAINT [FK_BillItems_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([ProductId]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Products_ScaleUnitId] ON [Products] ([ScaleUnitId]);
GO

CREATE INDEX [IX_BillItems_BillId] ON [BillItems] ([BillId]);
GO

CREATE INDEX [IX_BillItems_ExpenseCategoryId] ON [BillItems] ([ExpenseCategoryId]);
GO

CREATE INDEX [IX_BillItems_ProductId] ON [BillItems] ([ProductId]);
GO

CREATE INDEX [IX_Bills_VendorId] ON [Bills] ([VendorId]);
GO

CREATE INDEX [IX_Customers_BillingAddressId] ON [Customers] ([BillingAddressId]);
GO

CREATE INDEX [IX_Customers_PrimaryContactId] ON [Customers] ([PrimaryContactId]);
GO

CREATE INDEX [IX_InvoiceItems_InvoiceId] ON [InvoiceItems] ([InvoiceId]);
GO

CREATE INDEX [IX_InvoiceItems_ProductId] ON [InvoiceItems] ([ProductId]);
GO

CREATE INDEX [IX_Invoices_CustomerId] ON [Invoices] ([CustomerId]);
GO

CREATE INDEX [IX_UnitConversion_SourceUnitId] ON [UnitConversion] ([SourceUnitId]);
GO

CREATE INDEX [IX_UnitConversion_TargetUnitId] ON [UnitConversion] ([TargetUnitId]);
GO

CREATE INDEX [IX_Vendors_AddressId] ON [Vendors] ([AddressId]);
GO

CREATE INDEX [IX_Vendors_ContactId] ON [Vendors] ([ContactId]);
GO

ALTER TABLE [Products] ADD CONSTRAINT [FK_Products_ScaleUnits_ScaleUnitId] FOREIGN KEY ([ScaleUnitId]) REFERENCES [ScaleUnits] ([ScaleUnitId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220820180513_AddContactsAndInvoices', N'6.0.8');
GO

COMMIT;
GO

