BEGIN TRANSACTION;
GO

ALTER TABLE [BusinessTypes] ADD [DisableProducts] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [BusinessTypes] ADD [DisableServices] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [BusinessTypes] ADD [NoInventory] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [BusinessTypes] ADD [SalesOnly] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Accounts] ADD [BusinessTypeId] nvarchar(450) NULL;
GO

CREATE INDEX [IX_Accounts_BusinessTypeId] ON [Accounts] ([BusinessTypeId]);
GO

ALTER TABLE [Accounts] ADD CONSTRAINT [FK_Accounts_BusinessTypes_BusinessTypeId] FOREIGN KEY ([BusinessTypeId]) REFERENCES [BusinessTypes] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220904203733_AddRulesToBusinessType', N'6.0.8');
GO

COMMIT;
GO

