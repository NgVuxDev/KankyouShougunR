SELECT 
    name 
FROM 
    sys.columns 
WHERE 
object_id = object_id(/*TableName*/'')
 AND is_identity = 0