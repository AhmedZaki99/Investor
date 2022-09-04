BEGIN TRANSACTION;
GO

CREATE INDEX [IX_Brands_Name] ON [Brands] ([Name]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220902025104_AddBrandNameIndex', N'6.0.8');
GO

COMMIT;
GO

