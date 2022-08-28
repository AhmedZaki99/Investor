BEGIN TRANSACTION;
GO

ALTER TABLE [UnitConversion] DROP CONSTRAINT [FK_UnitConversion_ScaleUnits_SourceUnitId];
GO

ALTER TABLE [UnitConversion] DROP CONSTRAINT [FK_UnitConversion_ScaleUnits_TargetUnitId];
GO

ALTER TABLE [UnitConversion] DROP CONSTRAINT [PK_UnitConversion];
GO

EXEC sp_rename N'[UnitConversion]', N'UnitConversions';
GO

EXEC sp_rename N'[UnitConversions].[IX_UnitConversion_TargetUnitId]', N'IX_UnitConversions_TargetUnitId', N'INDEX';
GO

EXEC sp_rename N'[UnitConversions].[IX_UnitConversion_SourceUnitId]', N'IX_UnitConversions_SourceUnitId', N'INDEX';
GO

ALTER TABLE [Products] ADD [Code] nvarchar(63) NULL;
GO

ALTER TABLE [UnitConversions] ADD CONSTRAINT [PK_UnitConversions] PRIMARY KEY ([UnitConversionId]);
GO

ALTER TABLE [UnitConversions] ADD CONSTRAINT [FK_UnitConversions_ScaleUnits_SourceUnitId] FOREIGN KEY ([SourceUnitId]) REFERENCES [ScaleUnits] ([ScaleUnitId]);
GO

ALTER TABLE [UnitConversions] ADD CONSTRAINT [FK_UnitConversions_ScaleUnits_TargetUnitId] FOREIGN KEY ([TargetUnitId]) REFERENCES [ScaleUnits] ([ScaleUnitId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220824183353_RenameUnitConversionTable', N'6.0.8');
GO

COMMIT;
GO

