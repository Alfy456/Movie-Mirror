USE [Tms]
GO

INSERT INTO [dbo].[Job]
           ([Name]
           ,[Status]
           ,[CreatedWhen]
           ,[UpdatedWhen])
     VALUES
           ('Movie','Ready',getdate(),GETDATE())
GO


