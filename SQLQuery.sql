create database KeepNoteApp
use KeepNoteApp 

Create Table KeepNote
(
Id int identity  primary key,
Title varchar(50),
Descrip varchar(100),
NDate date 
)

select * from KeepNote 