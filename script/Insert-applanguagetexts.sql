USE [Demo]
GO

INSERT INTO [dbo].[AppLanguages]
           ([TenantId]
           ,[Name]
           ,[DisplayName]
           ,[Icon]
           ,[IsDisabled]
           ,[CreationTime]
           ,[CreatorId]
           ,[LastModificationTime]
           ,[LastModifierId]
           ,[IsDeleted]
           ,[DeleterId]
           ,[DeletionTime])
     VALUES
           (null
           ,'en'
           ,'English'
           ,'famfamfam-flags us'
           ,0
           ,GETDATE()
           ,'FA655AE4-1E3B-9867-1720-39FB255F1CAE'
           ,null
           ,null
           ,0
           ,null
           ,null)
GO


INSERT INTO [dbo].[AppLanguageTexts]
           ([TenantId]
           ,[LanguageName]
           ,[Source]
           ,[Key]
           ,[Value]
           ,[CreationTime]
           ,[CreatorId]
           ,[LastModificationTime]
           ,[LastModifierId])
     VALUES
           (null
           ,'en'
           ,'Demo'
           ,'Welcome'
           ,'Greet'
           ,GETDATE()
           ,'FA655AE4-1E3B-9867-1720-39FB255F1CAE'
           ,null
           ,null)
GO
INSERT INTO [dbo].[AppLanguageTexts]
           ([TenantId]
           ,[LanguageName]
           ,[Source]
           ,[Key]
           ,[Value]
           ,[CreationTime]
           ,[CreatorId]
           ,[LastModificationTime]
           ,[LastModifierId])
     VALUES
           (null
           ,'en'
           ,'Demo'
           ,'Menu:Incidents'
           ,'Manage Incidents'
           ,GETDATE()
           ,'FA655AE4-1E3B-9867-1720-39FB255F1CAE'
           ,null
           ,null)
GO

INSERT INTO [dbo].[AppLanguageTexts]
           ([TenantId]
           ,[LanguageName]
           ,[Source]
           ,[Key]
           ,[Value]
           ,[CreationTime]
           ,[CreatorId]
           ,[LastModificationTime]
           ,[LastModifierId])
     VALUES
           (null
           ,'en'
           ,'Demo'
           ,'NewIncident'
           ,'Create New Incident'
           ,GETDATE()
           ,'FA655AE4-1E3B-9867-1720-39FB255F1CAE'
           ,null
           ,null)
GO
