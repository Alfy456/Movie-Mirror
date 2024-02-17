
DECLARE @P0 XML='<Movies>
<Movie
MovieId="988535"
Name="Nalla Samayam" 
Language="ml" 
ReleaseDate="2022-12-30T00:00:00" 
Image="/u2WUImijSeeOQ1WajbsezXNRVcP.jpg" 
CreatedDate="2022-12-08T00:00:00+00:00" 
UpdatedDate="2022-12-08T00:00:00+00:00" />
</Movies>'

BEGIN TRY 
MERGE [dbo].[Movie] AS DBM
USING (select 
	x.v.value('@MovieId', 'int') as MovieId, 
	 x.v.value('@Name', 'nvarchar(255)') as Name, 
   x.v.value('@Language', 'nvarchar(255)') as Language, 
  x.v.value('@ReleaseDate', 'date') as ReleaseDate, 
   x.v.value('@CreatedDate', 'date') as CreatedDate,
    x.v.value('@UpdatedDate', 'date') as UpdatedDate,
    x.v.value('@Image', 'nvarchar(512)') as Image
from @P0.nodes('/Movies/Movie') x(v))  AS XMLM
ON  DBM.MovieId = XMLM.MovieId
WHEN MATCHED THEN
  UPDATE SET DBM.ReleaseDate = XMLM.ReleaseDate, 
		DBM.UpdatedDate=XMLM.UpdatedDate
WHEN NOT MATCHED THEN  
  INSERT (MovieId,Name,Language,ReleaseDate,Image,CreatedDate) VALUES (XMLM.MovieId,XMLM.Name,XMLM.Language,XMLM.ReleaseDate,XMLM.Image,XMLM.CreatedDate);
END TRY
BEGIN CATCH
  INSERT INTO MovieErrorLog(MovieId,Message)
  VALUES (1,ERROR_MESSAGE());
END CATCH