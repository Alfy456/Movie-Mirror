--added column IsUploadedToTelegram
ALTER TABLE Movie ADD IsUploadedToTelegram BIT NOT NULL DEFAULT 0;

--added column CreatedDate

DECLARE @ErrorMessage AS VARCHAR(250)

SET @ErrorMessage = 'Column already exists'

IF NOT EXISTS (SELECT *
               FROM   sys.columns
               WHERE  object_id = Object_id(N'[dbo].[Movie]')
                      AND NAME = 'CreatedDate')
  BEGIN
      ALTER TABLE [dbo].[movie]
        ADD [CreatedDate] DATETIME NULL
  END
ELSE
  BEGIN
      RAISERROR(@ErrorMessage,1,1)
  END 

--added column UpdatedDate

DECLARE @ErrorMessageForCreatedDate AS VARCHAR(250)

SET @ErrorMessageForCreatedDate = 'Column already exists'

IF NOT EXISTS (SELECT *
               FROM   sys.columns
               WHERE  object_id = Object_id(N'[dbo].[Movie]')
                      AND NAME = 'UpdatedDate')
  BEGIN
      ALTER TABLE [dbo].[movie]
        ADD [UpdatedDate] DATETIME NULL
  END
ELSE
  BEGIN
      RAISERROR(@ErrorMessageForCreatedDate,1,1)
  END 