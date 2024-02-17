CREATE TABLE MovieErrorLog(ID INT IDENTITY(1,1),
                        CreateDate DATETIME NOT NULL DEFAULT GETDATE(),
                        Message NVARCHAR(1000));