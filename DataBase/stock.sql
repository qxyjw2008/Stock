CREATE TABLE StockCode (
  NAME CHAR(50)  NOT NULL , 
  CODE CHAR(50) PRIMARY KEY NOT NULL );


CREATE TABLE USER (
  USERNAME CHAR(50)  PRIMARY KEY NOT NULL , 
  PASSWORD CHAR(50)  NOT NULL, 
  EMAIL CHAR(50) NOT NULL,
  PHONE CHAR(50));

INSERT INTO StockCode VALUES('通达股份','sz002560')；
INSERT INTO StockCode VALUES('华电国际','sh600027')；
INSERT INTO StockCode VALUES('国海证券','sz000750')
SELECT * FROM StockCode