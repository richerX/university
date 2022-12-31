CREATE SCHEMA hospital;

CREATE TABLE hospital.hospital(
    id serial primary key,
    name text,
    number int
);

CREATE TABLE hospital.ward(
    id serial primary key,
    hospital_id int references hospital.hospital (id),
    number int
);

CREATE TABLE hospital.staff(
    id serial primary key,
    hospital_id int references hospital.hospital (id),
    number int,
    name text,
    doctor boolean,
    nurse boolean
);

CREATE TABLE hospital.nurse(
    id serial primary key,
    staff_id int references hospital.staff (id),
    qualification text
);

CREATE TABLE hospital.doctor(
    id serial primary key,
    staff_id int references hospital.staff (id),
    area text,
    degree text
);

CREATE TABLE hospital.patient(
    id serial primary key,
    ward_id int references hospital.ward (id),
    doctor_id int references hospital.doctor (id),
    number text,
    name text,
    illness text,
    treatment_start date,
    treatment_end date
);
