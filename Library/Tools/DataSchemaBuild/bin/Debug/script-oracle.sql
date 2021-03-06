--
-- Autogenerated Oracle script for database schema project
-- Generated at 2012-12-20T17:48:02
-- Knightrunner.Library.Database.Schema.Oracle.OracleScriptGenerator, Knightrunner.Library.Database.Schema.Oracle, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null

create sequence sr.sr_repository_seq;
create table sr.sr_repository
(
	id_repository number(*,0) not null,
	name varchar2(255) not null,
	created date not null,
	created_by varchar2(255) not null,
	modified date not null,
	modified_by varchar2(255) not null,
	constraint sr_repository_pk primary key (id_repository),
	constraint sr_repository_name_uq unique (name)
);
/
comment on table sr.sr_repository is 'A software repository';
comment on column sr.sr_repository.id_repository is 'Unique ID';
comment on column sr.sr_repository.name is 'A unique name of the repository';
comment on column sr.sr_repository.created is 'Date+time when created';
comment on column sr.sr_repository.created_by is 'Username that created';
comment on column sr.sr_repository.modified is 'Date+time when last modified';
comment on column sr.sr_repository.modified_by is 'Username that last modified';
/
create or replace trigger sr.sr_repository_new
 before
  insert or update
 on sr.sr_repository
referencing new as new old as old
 for each row
 when (nvl(new.id_repository,0) = 0)
begin
  select sr.sr_repository_seq.nextval into :new.id_repository from dual;
end;
/
create sequence sr.sr_software_item_seq;
create table sr.sr_software_item
(
	id_software_item number(*,0) not null,
	id_repository number(*,0) not null,
	name varchar2(1000) not null,
	software_item_type number(3,0) not null,
	status number(3,0) not null,
	id_team number(*,0),
	description varchar2(2000),
	description_url varchar2(1000),
	deployment_directory varchar2(500),
	implementation varchar2(1000),
	source_control_path varchar2(500),
	drop_location varchar2(500),
	build_name varchar2(255),
	log_application_name varchar2(255),
	log_application_version varchar2(255),
	external_id varchar2(100),
	constraint sr_software_item_pk primary key (id_software_item),
	constraint sr_software_item_id_name_uk unique (id_repository, name),
	constraint sr_software_item_repository_fk foreign key (id_repository) references sr.sr_repository (id_repository),
	constraint sr_software_item_id_team_fk foreign key (id_team) references sr.sr_team (id_team)
);
/
comment on table sr.sr_software_item is 'A software item, such as a SF service or a module';
comment on column sr.sr_software_item.id_software_item is 'Unique ID';
comment on column sr.sr_software_item.id_repository is 'ID of the repository that this software item belongs to';
comment on column sr.sr_software_item.name is 'A unique name of the software item within its repository';
comment on column sr.sr_software_item.software_item_type is 'Type of software item: 0=undefined, 1=service, 2=client module, 3=web module';
comment on column sr.sr_software_item.status is 'Current status of the software item: 0=undefined, 1=new, 2=staged, 3=active, 4=deprecated, 5=dying, 6=dead';
comment on column sr.sr_software_item.id_team is 'The team responsible for this service';
comment on column sr.sr_software_item.description is 'A short description of the software item';
comment on column sr.sr_software_item.description_url is 'An URL to detailed information on the software item';
comment on column sr.sr_software_item.deployment_directory is 'A full directory path where the software item is deployed';
comment on column sr.sr_software_item.implementation is 'For SoftwareItemType == Service, this is an optional address (URL) of the implementing service.';
comment on column sr.sr_software_item.source_control_path is 'Full source control path for the code of this software item';
comment on column sr.sr_software_item.drop_location is 'Full directory path of the drop location of this software item';
comment on column sr.sr_software_item.build_name is 'TFS build name of this software item';
comment on column sr.sr_software_item.log_application_name is 'The application name of the software item as it appears in logs';
comment on column sr.sr_software_item.log_application_version is 'The application log version of the software item as it appears in logs';
comment on column sr.sr_software_item.external_id is 'Any external id needed to connect this team to a third party system.';
/
create or replace trigger sr.sr_software_item_new
 before
  insert or update
 on sr.sr_software_item
referencing new as new old as old
 for each row
 when (nvl(new.id_software_item,0) = 0)
begin
  select sr.sr_software_item_seq.nextval into :new.id_software_item from dual;
end;
/
create sequence sr.sr_file_item_seq;
create table sr.sr_file_item
(
	id_file_item number(*,0) not null,
	id_software_item number(*,0) not null,
	file_path varchar2(500) not null,
	version varchar2(255),
	constraint sr_file_item_pk primary key (id_file_item),
	constraint sr_file_item_path_uk unique (id_software_item, file_path),
	constraint sr_file_item_software_item_fk foreign key (id_software_item) references sr.sr_software_item (id_software_item)
);
/
comment on table sr.sr_file_item is 'A file that belongs to a software item';
comment on column sr.sr_file_item.id_file_item is 'Unique ID';
comment on column sr.sr_file_item.id_software_item is 'ID of the software item that owns this file item';
comment on column sr.sr_file_item.file_path is 'Full file path';
comment on column sr.sr_file_item.version is 'Version of the file';
/
create or replace trigger sr.sr_file_item_new
 before
  insert or update
 on sr.sr_file_item
referencing new as new old as old
 for each row
 when (nvl(new.id_file_item,0) = 0)
begin
  select sr.sr_file_item_seq.nextval into :new.id_file_item from dual;
end;
/
create sequence sr.sr_measurement_seq;
create table sr.sr_measurement
(
	id_measurement number(*,0) not null,
	id_software_item number(*,0) not null,
	measurement_type number(3,0) not null,
	created date not null,
	period number(10,0) not null,
	detail varchar2(255),
	measurement_value number(*,0) not null,
	constraint sr_measurement_pk primary key (id_measurement),
	constraint sr_measurement_uk unique (id_software_item, measurement_type, period, detail),
	constraint sr_measurement_fk foreign key (id_software_item) references sr.sr_software_item (id_software_item)
);
/
comment on table sr.sr_measurement is 'A measurement of some of the behaviour and/or use of a software item';
comment on column sr.sr_measurement.id_measurement is 'Unique ID';
comment on column sr.sr_measurement.id_software_item is 'ID of the software item that owns this measurement';
comment on column sr.sr_measurement.measurement_type is 'Type of measurement: 0=undefined, 1=count of PCS lookups, 2=count of calls to a service function.';
comment on column sr.sr_measurement.created is 'Date+time when the measurement was created';
comment on column sr.sr_measurement.period is 'The period that this measurement covers. It is an integer value spanning a whole month calculated as follows: period = year*12 + (month-1)';
comment on column sr.sr_measurement.detail is 'Additional detail breakup of the measurement, like a sub-part of the software item, for example the name of a service method';
comment on column sr.sr_measurement.measurement_value is 'The value of this measurement, like number of times a service was called';
/
create or replace trigger sr.sr_measurement_new
 before
  insert or update
 on sr.sr_measurement
referencing new as new old as old
 for each row
 when (nvl(new.id_measurement,0) = 0)
begin
  select sr.sr_measurement_seq.nextval into :new.id_measurement from dual;
end;
/
create sequence sr.sr_dependency_seq;
create table sr.sr_dependency
(
	id_dependency number(*,0) not null,
	id_software_item_from number(*,0) not null,
	id_software_item_to number(*,0) not null,
	source number(3,0) not null,
	type number(3,0) not null,
	constraint sr_dependency_pk primary key (id_dependency),
	constraint sr_dependency_from_to_uk unique (id_software_item_from, id_software_item_to),
	constraint sr_dependency_from_fk foreign key (id_software_item_from) references sr.sr_software_item (id_software_item),
	constraint sr_dependency_to_fk foreign key (id_software_item_to) references sr.sr_software_item (id_software_item)
);
/
comment on table sr.sr_dependency is 'A software item dependency, records the dependency of one software item on another software item';
comment on column sr.sr_dependency.id_dependency is 'Unique ID';
comment on column sr.sr_dependency.id_software_item_from is 'ID of the software item that holds the dependency';
comment on column sr.sr_dependency.id_software_item_to is 'ID of the software item that id_software_item_from is dependent on';
comment on column sr.sr_dependency.source is 'Source of registration: 0=undefined, 1=agent, 2=manual';
comment on column sr.sr_dependency.type is 'Type of dependency: 0=undefined, 1=static analysis, 2=actual usage';
/
create or replace trigger sr.sr_dependency_new
 before
  insert or update
 on sr.sr_dependency
referencing new as new old as old
 for each row
 when (nvl(new.id_dependency,0) = 0)
begin
  select sr.sr_dependency_seq.nextval into :new.id_dependency from dual;
end;
/
create sequence sr.sr_measurement_bookmark_seq;
create table sr.sr_measurement_bookmark
(
	id_measurement_bookmark number(*,0) not null,
	id_repository number(*,0) not null,
	measurement_type number(3,0) not null,
	last_datetime date not null,
	constraint sr_measurement_bookmark_pk primary key (id_measurement_bookmark),
	constraint sr_measurement_bookmark_uq unique (id_repository, measurement_type),
	constraint sr_measurement_bookmark_fk foreign key (id_repository) references sr.sr_repository (id_repository)
);
/
comment on table sr.sr_measurement_bookmark is 'A measurement bookmark. It is used to store location in data where scanning agents last scanned so they can later resume at this point';
comment on column sr.sr_measurement_bookmark.id_measurement_bookmark is 'Unique ID';
comment on column sr.sr_measurement_bookmark.id_repository is 'ID of the repository that owns this bookmark';
comment on column sr.sr_measurement_bookmark.measurement_type is 'The measurement type of this bookmark (see sr.sr_measurement.measurement_type)';
comment on column sr.sr_measurement_bookmark.last_datetime is 'The last date+time used';
/
create or replace trigger sr.sr_measurement_bookmark_new
 before
  insert or update
 on sr.sr_measurement_bookmark
referencing new as new old as old
 for each row
 when (nvl(new.id_measurement_bookmark,0) = 0)
begin
  select sr.sr_measurement_bookmark_seq.nextval into :new.id_measurement_bookmark from dual;
end;
/
create sequence sr.sr_team_seq;
create table sr.sr_team
(
	id_team number(*,0) not null,
	name varchar2(255) not null,
	active number(1,0) not null,
	contact varchar2(1000),
	external_id varchar2(100),
	constraint sr_team_pk primary key (id_team),
	constraint sr_team_uq unique (name)
);
/
comment on table sr.sr_team is 'A team. Teams do not belong to a repostory, they are global.';
comment on column sr.sr_team.id_team is 'Unique ID';
comment on column sr.sr_team.name is 'A unique name of the team';
comment on column sr.sr_team.active is 'True if we allow new data to use this team, false otherwise.';
comment on column sr.sr_team.contact is 'Contact information such as a list of emails etc.';
comment on column sr.sr_team.external_id is 'Any external id needed to connect this team to a third party system.';
/
create or replace trigger sr.sr_team_new
 before
  insert or update
 on sr.sr_team
referencing new as new old as old
 for each row
 when (nvl(new.id_team,0) = 0)
begin
  select sr.sr_team_seq.nextval into :new.id_team from dual;
end;
/
