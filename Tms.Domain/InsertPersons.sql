INSERT into  [dbo].[Persons](PersonId, Name,Country,Birthday,Gender,Image)  
select 
  x.v.value('@PersonId', 'int') as PersonId, 
    x.v.value('@Name', 'nvarchar(255)') as Name, 
       x.v.value('@Country', 'nvarchar(255)') as Country, 
         x.v.value('@Birthday', 'date') as Birthday, 
           x.v.value('@Gender', 'nvarchar(255)') as Gender, 
                 x.v.value('@Image', 'nvarchar(512)') as Image
from @P0.nodes('/Persons/Person') x(v)



        
