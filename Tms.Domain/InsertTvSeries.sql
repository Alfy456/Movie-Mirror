INSERT into  [dbo].[TvShow](TvShowId, Name,Language,Status,Premiered,Ended,Image,UpdatedWhen)  
select 
  x.v.value('@TvShowId', 'nvarchar(255)') as TvShowId, 
    x.v.value('@Name', 'nvarchar(255)') as Name, 
       x.v.value('@Language', 'nvarchar(255)') as Language, 
         x.v.value('@Status', 'nvarchar(255)') as Status, 
           x.v.value('@Premiered', 'date') as Premiered, 
                 x.v.value('@Ended', 'date') as Ended, 
                  x.v.value('@Image', 'nvarchar(512)') as Image, 
  x.v.value('@UpdatedWhen', 'date') as UpdatedWhen
from @P0.nodes('/TvShows/TvShow') x(v)



        
