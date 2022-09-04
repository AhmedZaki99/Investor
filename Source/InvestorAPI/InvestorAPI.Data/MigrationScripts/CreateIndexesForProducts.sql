BEGIN TRANSACTION;
GO

CREATE UNIQUE INDEX [IX_ScaleUnits_Name] ON [ScaleUnits] ([Name]);
GO

CREATE UNIQUE INDEX [IX_Products_Code] ON [Products] ([Code]) WHERE [Code] IS NOT NULL;
GO

CREATE INDEX [IX_Products_IsService] ON [Products] ([IsService]);
GO

CREATE UNIQUE INDEX [IX_Products_Name] ON [Products] ([Name]);
GO

CREATE UNIQUE INDEX [IX_Categories_Name] ON [Categories] ([Name]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220903235252_CreateIndexesForProducts', N'6.0.8');
GO

COMMIT;
GO

