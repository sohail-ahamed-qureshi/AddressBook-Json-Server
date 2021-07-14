--UC1 -- create new database for addressbook
Create Database AddressBook;
use AddressBook;

--UC2 -- create new table for address book
create table Contacts(
firstName varchar(255),
lastName varchar(255),
address varchar(255),
city varchar(255),
state varchar(255),
zip int,
phoneNumber bigint,
email varchar(255)
);

--UC3 -- ability to insert new data to table
insert into contacts
values('emily', 'watson', '#54 backstreet', 'NY', 'America', 123654, 09876554321, 'emily@email.com'),
('Dwayne', 'johnson', '#12 SM nagar', 'Mysore', 'Karnataka', 100004, 09876554321, 'johnson@email.com'),
('terisa', 'loui', '#3 AB nagar', 'Noida', 'UP', 543201, 09876554321, 'loui@email.com'),
('grey', 'moron', '#84 MJ nagar', 'NJ', 'London', 129123, 09876554321, 'grey@email.com');

--UC4 -ability to edit existing contact using their name
Update contacts set phoneNumber =  12345567809 where firstName = 'terisa';

--display table
select * from contacts

--UC5 -- ability to delete existing contact using their name
delete from contacts where firstName = 'emily';

--display table
select * from contacts

--UC6 -- ability to retrieve contact using city or state from the table
select * from contacts where city = 'bangalore' or state = 'karnataka';

--UC7 -- ability to get size of address book by city and state
select Count(city) from contacts

select count(state) from contacts

--UC8 -- ability to retrieve enteries sorted alphabetically by Person's name for given city
-- ascending order
select * from contacts where state = 'karnataka' order by firstName desc
-- descending order
select * from contacts where state = 'karnataka' order by firstName asc

--UC9 --ability to identify the relation with Contact person
-- adding column of type Relation to contact by default will be 'friends'
alter table contacts add relation varchar(255) not null default 'friends';
-- updating contact relation column to type 'family'
update contacts set relation = 'colleague',  typeOf = 'Profession' where firstName = 'terisa';
--display table
select * from contacts;

--UC10 --ablity to get count by type in contacts
select Count(relation) from contacts where relation = 'friends' 
select Count(relation) from contacts where relation = 'family' 

select * from contacts;

--UC11 --ablity to add person to both friend and family
update contacts set relation = 'friend',  typeOf = 'family' where firstName = 'grey';



