--//Version 1

declare @errorMessage varchar(max);
declare @errorData varchar(max);

BEGIN TRY 

INSERT into  [dbo].[Movie](MovieId,Name,Language,ReleaseDate,CreatedDate,UpdatedDate,Image)  
select 
    x.v.value('@MovieId', 'int') as MovieId, 
	  x.v.value('@Name', 'nvarchar(max)') as Name, 
    x.v.value('@Language', 'nvarchar(max)') as Language,
    x.v.value('@ReleaseDate', 'date') as ReleaseDate, 
    x.v.value('@CreatedDate', 'date') as CreatedDate,
    x.v.value('@UpdatedDate', 'date') as UpdatedDate,
	  x.v.value('@Image', 'nvarchar(max)') as Image
from @p0.nodes('/Movies/Movie') x(v)       

if(@@ROWCOUNT <> 0)
BEGIN
set @errorMessage ='no error';
END
else
BEGIN
set @errorMessage ='has error';
set @errorData=CONVERT(VARCHAR(max), @p0);
END

END TRY
BEGIN CATCH
set @errorMessage ='has error';
set @errorData=CONVERT(VARCHAR(max), @p0);
END CATCH

insert into ErrorLog(Language,ErrorMessage,ErrorData,Page,CreateDate) values (@language,@errorMessage,@errorData,@page,getdate())

--Please fix the bug with this

--DECLARE @accountId int, @auditDate datetime, @auditCorrelationId uniqueidentifier
 
--SELECT @accountId = x.v.value('@accountId', 'int') from @P0.nodes('/values') x(v)
--SET @auditDate = GETDATE()
--SET @auditCorrelationId = NEWID()
--BEGIN TRY 
--MERGE [dbo].[Movies1] AS DBM
--USING (select 
--    x.v.value('@MovieId', 'nvarchar(max)') as MovieId, 
--	  x.v.value('@Name', 'nvarchar(max)') as Name, 
--    x.v.value('@Language', 'nvarchar(max)') as Language,
--    x.v.value('@ReleaseDate', 'date') as ReleaseDate, 
--    x.v.value('@CreatedDate', 'date') as CreatedDate,
--    x.v.value('@UpdatedDate', 'date') as UpdatedDate,
--	  x.v.value('@Image', 'nvarchar(max)') as Image
--from @P0.nodes('/Movies/Movie') x(v))  AS XMLM
--ON  DBM.MovieId = XMLM.MovieId
--WHEN MATCHED THEN
-- UPDATE SET DBM.UpdatedDate=XMLM.UpdatedDate
--WHEN NOT MATCHED THEN  
--  INSERT (MovieId,Name,Language,ReleaseDate,CreatedDate,UpdatedDate,Image) VALUES (XMLM.MovieId,XMLM.Name,XMLM.Language,XMLM.ReleaseDate,XMLM.CreatedDate,XMLM.UpdatedDate,XMLM.Image);
--if(@@ROWCOUNT <> 0)
--BEGIN
--set @errorMessage ='no error';
--END
--else
--BEGIN
--set @errorMessage ='has error';
--set @errorData=CONVERT(VARCHAR(max), @p0);
--END
-- insert into MovieErrorLog(Language,ErrorMessage,ErrorData,Page,CreateDate) values (@language,@errorMessage,@errorData,@page,getdate())
--END TRY
--BEGIN CATCH
-- insert into MovieErrorLog(Language,ErrorMessage,ErrorData,Page,CreateDate) values (@language,@errorMessage,@errorData,@page,getdate())
--END CATCH
----OUTPUT
--   --@auditCorrelationId AS AuditCorrelationId,
--   --@auditDate AS AuditDate,
--   --CASE $ACTION WHEN 'INSERT' THEN 0 WHEN 'UPDATE' THEN 1 END ChangeType,
--   --@accountId AS AccountId,
--   --inserted.FormEntityId,
--   --inserted.Name,
--   --deleted.Value AS OldValue,
--   --inserted.Value AS NewValue
--   --INTO [forms].[FormAudit]
----;

 





     

