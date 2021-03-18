
ALTER TABLE product
  ADD
  FullPath varchar(255);

/* proc get path*/
DROP PROCEDURE IF EXISTS Proc_GetPath;
DELIMITER $$
CREATE PROCEDURE Proc_GetPath(IN cat_id char(36), OUT path TEXT)
BEGIN
    DECLARE catname VARCHAR(1000);/*tên để nối*/
    DECLARE temppath TEXT;/*đường dẫn tên rỗng*/
    DECLARE tempparent char(36);/*id cha rỗng*/
    SET max_sp_recursion_depth = 4000;/*max nhập*/
    SELECT p.ProductName, p.ProductIDParent FROM product p WHERE p.ProductID=cat_id INTO catname, tempparent;/*catname = name, tempparent = parent*/
    IF tempparent = '00000000-0000-0000-0000-000000000000'/*Nếu tempparent null*/
    THEN
        SET path = catname;/* thì đường dẫn bằng catname tức tên cha to nhất*/
    ELSE/* Nếu như có cha, thức thằng catname đang chọn là con*/
        CALL Proc_GetPath(tempparent, temppath);/*đệ quy gọi lại với tempparent và path rỗng mới*/
        SET path = CONCAT(temppath, '--> ', catname);/* đường dẫn bằng nối đường dẫn cũ/tên thằng mới*/
    END IF;
END$$
DELIMITER ;

/* function Proc_GetPath */
DROP FUNCTION IF EXISTS Func_GetPath;
DELIMITER $$
CREATE FUNCTION Func_GetPath(cat_id char(36)) RETURNS TEXT DETERMINISTIC
BEGIN
    DECLARE res TEXT;
    CALL Proc_GetPath(cat_id, res);/*gọi proceduce*/
    RETURN res;
END$$
DELIMITER ;

/* đầu ra*/
SELECT p.ProductID, p.ProductName, Func_GetPath(p.ProductID) AS path FROM product p; 


/*update vào côt fullparse*/
UPDATE product p 
  SET p.FullPath = Func_GetPath(p.ProductID) ;
